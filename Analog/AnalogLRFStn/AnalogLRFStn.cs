using SakuraMeta = FERHRI.Sakura.Meta;
using SakuraData = FERHRI.Sakura.Data;
using SakuraDateTimeProcess = FERHRI.Sakura.DateTimeProcess.DateTimeProcess;
using AmurMeta = FERHRI.Amur.Meta;
using AmurData = FERHRI.Amur.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Analog
{
    public class AnalogLRFStn
    {


        public static void CalcAnalogFcs(int geoObjId, List<int> varTypeList, int methodId, DateTime dateIn)
        {

            SakuraMeta.Enums.Action actPar = SakuraMeta.Enums.Action.AVG;//action который прописан в Ctl Hmdic

            AmurMeta.DataManager amurDmMeta = AmurMeta.DataManager.GetInstance();
            AmurData.DataManager amurDmData = AmurData.DataManager.GetInstance();
            SakuraMeta.DataManager sakuraDmMeta = SakuraMeta.DataManager.GetInstance();
            DataManager analogDm = DataManager.GetInstance();

            int stnTypeGO = 12;
            int stnType = 1;//Метео станция
            int siteType = 1;
            //Определяем метод прогноза
            AmurMeta.MethodForecast methodForecast = amurDmMeta.MethodForecastRepository.Select(methodId);
            if (methodForecast == null)
                throw new Exception("MethodId=" + methodId + " not forecast!");

            //определяем TimePeriod Аналогов и select_angId
            int timeIdAng;
            int selAngId;
            List<TaskSelectAnalog> tsa = analogDm.TaskSelectAnalogRepository.SelectByMethodId((int)methodForecast.Method.ParentId);
            if (tsa.Count != 1)
                throw new Exception("ERROR!");
            timeIdAng = tsa[0].TimeId;
            selAngId = tsa[0].Id;
            int? tmp;
            #region определяем тип временного  периода Аналогов в hmdic переменных
            tmp = sakuraDmMeta.EntityXEntityExternalRepository.SelectOurEntityId((int)SakuraMeta.Enums.EntityType.TimePeriod,
                (int)SakuraMeta.Enums.DbList.Amur, timeIdAng);
            if (tmp == null)
                throw new Exception("Нет соответствия для timeId=" + timeIdAng);
            SakuraMeta.Enums.TimePeriod timePeriodAng = (SakuraMeta.Enums.TimePeriod)(int)tmp;
            #endregion
            #region определяем тип временного  периода прогностических заблаговременностей в hmdic переменных
            tmp = sakuraDmMeta.EntityXEntityExternalRepository.SelectOurEntityId((int)SakuraMeta.Enums.EntityType.TimePeriod,
                (int)SakuraMeta.Enums.DbList.Amur, methodForecast.LeadTimeUnitId);
            if (tmp == null)
                throw new Exception("Нет соответствия для timeId=" + methodForecast.LeadTimeUnitId);
            SakuraMeta.Enums.TimePeriod leadTimePeriod = (SakuraMeta.Enums.TimePeriod)(int)tmp;
            #endregion
            //Считываем все станции для данного геообъекта
            List<AmurMeta.StationGeoObject> stationsInGO = amurDmMeta.StationGeoObjectRepository.SelectByGeoObjects(new List<int>() { geoObjId });
            //выбираем из станций метку гео-объекта
            List<AmurMeta.Station> stnGO = amurDmMeta.StationRepository.Select(stationsInGO.Select(t => t.StationId).ToList()).Where(q => q.TypeId == stnTypeGO).ToList();
            if (stnGO.Count != 1)//если метка гео-объекта не одна - ошибка
                throw new Exception("Не верное кол-во меток гео-объекта. Всего " + stnGO.Count + ", должна быть 1!");
            List<AmurMeta.Station> stnList = amurDmMeta.StationRepository.Select(stationsInGO.Select(t => t.StationId).ToList()).Where(q => q.TypeId == stnType).ToList();
            List<AmurMeta.Site> siteList = amurDmMeta.SiteRepository.SelectByStations(stnList.Select(t => t.Id).ToList());
            //Список индексов станции для данного гео-объекта
            List<int> stnIndList = siteList.Where(t => t.SiteTypeId == siteType).Select(q => int.Parse(q.SiteCode)).ToList();
            siteList = amurDmMeta.SiteRepository.SelectByStations(new List<int>() { stnGO[0].Id });
            if (siteList.Count != 1)
                throw new Exception("Не верное кол-во site для гео-объекта. Всего " + siteList.Count + ", должно быть 1!");
            List<AmurMeta.Variable> varList = amurDmMeta.VariableRepository.Select(varTypeList, null, null, null, null, null, null, new List<int>() { 3 }/*forecast*/);


            Dictionary<int, SakuraMeta.Enums.Action> dicActAmur2Sakura = sakuraDmMeta.EntityXEntityExternalRepository.Select((int)SakuraMeta.Enums.EntityType.Action,
                (int)SakuraMeta.Enums.DbList.Amur).ToDictionary(t => t.ExtEntityId, q => (SakuraMeta.Enums.Action)q.OurEntityId);

            Dictionary<SakuraMeta.Enums.Action, AmurMeta.Catalog> dicActSakuraCtl = new Dictionary<SakuraMeta.Enums.Action, Amur.Meta.Catalog>();
            //Для каждого типа временного интервала и типа переменной считаем все отдльно
            foreach (var varListTime in varList.GroupBy(t => new { t.TimeId, t.ValueTypeId }))
            {
                //тип временного интервала данных
                int unitIdTime = varListTime.Key.TimeId;
                //тип variable
                int varTypeId = varListTime.Key.ValueTypeId;
                //находим все catalog для заданного региона, переменных и расчетного метода
                List<AmurMeta.Catalog> ctl = amurDmMeta.CatalogRepository.Select(new List<int>() { siteList[0].Id }, varListTime.Select(t => t.Id).ToList(),
                new List<int>() { methodId }, null, null, (double?)null);
                if (ctl.Count == 0)
                    continue;
                //заполняем словарь dicActSakuraCtl Dictionary<SakuraMeta.Enums.Action, AmurMeta.Catalog>
                foreach (var v in varListTime)
                {
                    var ctlVar = ctl.Where(t => t.VariableId == v.Id).ToList();
                    if (ctlVar.Count != 1)
                        continue;
                    SakuraMeta.Enums.Action actSakura;
                    if (dicActAmur2Sakura.TryGetValue(v.DataTypeId, out actSakura))
                        dicActSakuraCtl.Add(actSakura, ctlVar[0]);
                }

                #region определяем тип временного периода в hmdic переменных
                tmp = sakuraDmMeta.EntityXEntityExternalRepository.SelectOurEntityId((int)SakuraMeta.Enums.EntityType.TimePeriod,
                    (int)SakuraMeta.Enums.DbList.Amur, unitIdTime);
                if (tmp == null)
                {
                    Console.WriteLine("Нет соответствия для timeId=" + unitIdTime);
                    //throw new Exception("Нет соответствия для timeId=" + unitIdTime);
                    continue;
                }
                SakuraMeta.Enums.TimePeriod timePeriod = (SakuraMeta.Enums.TimePeriod)(int)tmp;
                #endregion
                #region определяем  параметр в hmdic переменных для variable
                tmp = sakuraDmMeta.EntityXEntityExternalRepository.SelectOurEntityId((int)SakuraMeta.Enums.EntityType.Parameter,
                        (int)SakuraMeta.Enums.DbList.Amur, varTypeId);
                if (tmp == null)
                {
                    Console.WriteLine("Нет соответствия для timeId=" + unitIdTime);
                    //    throw new Exception("Нет соответствия для variable_type=" + varTypeId);
                    continue;
                }
                SakuraMeta.Enums.Parameter param = (SakuraMeta.Enums.Parameter)(int)tmp;
                #endregion


                int timeNumIni = SakuraDateTimeProcess.GetTimeNum(dateIn, (int)timePeriodAng);
                int yearIni = SakuraDateTimeProcess.GetYear(dateIn, (int)timePeriodAng);
                DateTime dateIniS = SakuraDateTimeProcess.GetDateTimeStart(yearIni, timeNumIni, (int)timePeriodAng);
                DateTime dateIniF = SakuraDateTimeProcess.Add(dateIniS, (int)timePeriodAng, 1).AddMilliseconds(-1);

                DataAnalog0 dang0 = analogDm.DataAnalog0Repository.SelectBySelectAnalogId(selAngId, dateIniS);
                Dictionary<int, double> dicYearsWeight = new Dictionary<int, double>();
                //заполняем словарь годов-Аналогов с весами
                foreach (var da1 in dang0.DateWeightAnalogs)
                {
                    int yearAng = SakuraDateTimeProcess.GetYear(da1.DateAnalog, (int)timePeriodAng);
                    int timeNumAng = SakuraDateTimeProcess.GetTimeNum(da1.DateAnalog, (int)timePeriodAng);
                    if (timeNumAng != timeNumIni)
                        throw new Exception("timeNumAng != timeNumIni");
                    dicYearsWeight.Add(yearAng, da1.Weight);
                }
                List<AmurData.DataForecast> dataValues = new List<AmurData.DataForecast>();
                DateTime dateIniFcsS = SakuraDateTimeProcess.GetDateTimeStart(dateIniS, (int)timePeriod);
                while (dateIniFcsS <= dateIniF)
                {
                    int timeNumFcsIni = SakuraDateTimeProcess.GetTimeNum(dateIniFcsS, (int)timePeriod);
                    int yearFcsIni = SakuraDateTimeProcess.GetYear(dateIniFcsS, (int)timePeriod);
                    int countStnData;

                    foreach (int timeShift in methodForecast.LeadTimes)
                    {
                        int timeNumFcs = SakuraDateTimeProcess.GetTimeNum(dateIniFcsS, (int)timePeriod);
                        DateTime dateFcs = SakuraDateTimeProcess.GetDateTimeStart(
                            SakuraDateTimeProcess.Add(dateIniFcsS, (int)leadTimePeriod, timeShift), (int)timePeriod);

                        Dictionary<SakuraMeta.Enums.Action, double> dicCalc = CalcAnalogFcs(sakuraDmMeta, timePeriod, timeNumFcs,
                            leadTimePeriod, (int)timeShift, dicYearsWeight,
                            stnIndList, param, actPar, dicActSakuraCtl.Keys.ToList(), out countStnData);
                        foreach (SakuraMeta.Enums.Action act in dicActSakuraCtl.Keys)
                        {
                            if (dicCalc.ContainsKey(act) && !double.IsNaN(dicCalc[act]))
                            {
                                AmurData.DataForecast dv = new AmurData.DataForecast(-1, timeShift, dateFcs, dateIniS, dicCalc[act], DateTime.Now);
                                dataValues.Add(dv);
                            }
                        }

                    }
                }
                amurDmData.DataForecastRepository.Insert(dataValues);

            }










            //List<AmurData.DataForecast> dataValues = new List<AmurData.DataForecast>();
            //foreach (SakuraMeta.Enums.Action act in dicActSakuraCtl.Keys)
            //{
            //    if (dicCalc.ContainsKey(act) && !double.IsNaN(dicCalc[act]))
            //    {
            //        AmurData.DataValue dv = new Amur.Data.DataValue(-1, dicActSakuraCtl[act].Id, dicCalc[act], dateFcs, dateFcs, 0, 0);
            //        dataValues.Add(dv);
            //    }
            //}
            //amurDmData.DataValueRepository.Insert(dataValues);
            //Console.WriteLine();

        }

        /// <summary>
        /// Посчитать прогностическое значение
        /// </summary>
        /// <param name="timePeriod"></param>
        /// <param name="timeNum"></param>
        /// <param name="yearsAngList"></param>
        /// <param name="weightAng"></param>
        /// <param name="regId"></param>
        /// <param name="stnIndList"></param>
        /// <param name="param"></param>
        /// <param name="actPar"></param>
        /// <param name="listAct"></param>
        /// <param name="countStnData"></param>
        /// <returns></returns>
        /// 
        private static Dictionary<SakuraMeta.Enums.Action, double> CalcAnalogFcs(SakuraMeta.DataManager dmMeta, SakuraMeta.Enums.TimePeriod timePeriodData,
            int timeNumIniFcs, SakuraMeta.Enums.TimePeriod leadTimePeriod, int timeShift,
            Dictionary<int/*year*/, double/*weight*/> dicYearsWeight,
            List<int> stnIndList, SakuraMeta.Enums.Parameter param, SakuraMeta.Enums.Action actPar,
            List<SakuraMeta.Enums.Action> listAct, out int countStnData)
        {

            //брать из hmdic!!!
            List<int> dbList = new List<int>() { (int)SakuraMeta.Enums.DbList.HCRMonth };

            Dictionary<int/*dbListId*/, FERHRI.Sakura.Data.DataManager> dicDmData = new Dictionary<int, Sakura.Data.DataManager>();

            foreach (int dbListId in dbList.Distinct())
            {
                string constr = dmMeta.DbListRepository.SelectAttrValue(dbListId, (int)SakuraMeta.Enums.TaskAttr.SRC_CONNECTION);
                if (constr != null)
                {
                    dicDmData.Add(dbListId, FERHRI.Sakura.Data.DataManager.GetInstance(constr, dbListId));

                }
            }

            SakuraMeta.Enums.LevelType levType = SakuraMeta.Enums.LevelType.LND;
            int levelValue = 0;
            int gridId = 70006;
            SakuraMeta.Enums.Action actInStn = SakuraMeta.Enums.Action.AVG_WEIGHT;

            countStnData = 0;


            Dictionary<SakuraMeta.Enums.Action, double> ret = new Dictionary<Sakura.Meta.Enums.Action, double>();
            List<double[]> dataCalcAngStn = new List<double[]>();
            foreach (int stnInd in stnIndList)
            {
                SakuraMeta.Station stn = dmMeta.StationRepository.GetStation(stnInd, SakuraMeta.Enums.StationType.HMStation);
                if (stn == null)
                    continue;
                List<Geo.GeoRectangle> grList = dmMeta.GeoRegRepository.SelectByNameList(new List<string>() { stn.Id.ToString() });
                if (grList.Count != 1)
                    continue;

                List<SakuraMeta.Catalog> ctlList = dmMeta.CatalogRepository.Select(null, (int)param, (int)levType, (int)actPar,
                    (int)SakuraMeta.Enums.Space.STATION, grList[0].Id, null, (int)SakuraMeta.Enums.Format.RDB,
                    (int)timePeriodData, levelValue, null, 0, 0, gridId, null).Where(t => dbList.Exists(q => q == t.DbListId)).ToList();

                if (ctlList.Count != 1)
                    continue;
                FERHRI.Sakura.Data.DataManager dmData;
                if (!dicDmData.TryGetValue(ctlList[0].DbListId, out dmData))
                    continue;
                List<SakuraData.DataHCR> data = dmData.HCRRepository.Select(new List<int>() { ctlList[0].Id });


                //var dataSel = data.Where(t => dicYearsWeight.Keys.ToList().Exists(q => q == SakuraDateTimeProcess.GetYear(t.Date, (int)timePeriod))
                //       && SakuraDateTimeProcess.GetTimeNum(t.Date, (int)timePeriod) == timeNumFcs && !double.IsNaN(t.Value)).ToList();

                List<SakuraData.DataHCR> dataSel = new List<SakuraData.DataHCR>();
                List<double> weights = new List<double>();
                foreach (int y in dicYearsWeight.Keys)
                {
                    DateTime dateAngIni = SakuraDateTimeProcess.GetDateTimeStart(y, timeNumIniFcs, (int)timePeriodData);
                    DateTime dateFcs = SakuraDateTimeProcess.GetDateTimeStart(SakuraDateTimeProcess.Add(dateAngIni, (int)leadTimePeriod, timeShift), (int)timePeriodData);
                    var d = data.Where(t => t.Date == dateFcs).ToList();
                    if (d.Count != 1)
                        continue;
                    dataSel.Add(d[0]);
                    weights.Add(dicYearsWeight[y]);
                }
                double[] dataStnAct = calcData(dataSel.Select(t => t.Value).ToArray(), actInStn, weights.ToArray());
                if (dataStnAct[1] == 0)
                    continue;
                dataCalcAngStn.Add(dataStnAct);
            }
            countStnData = dataCalcAngStn.Count;
            if (countStnData > 0)
            {
                List<double> dcasValue = dataCalcAngStn.Select(t => t[0]).ToList();
                double countAngAvg = dataCalcAngStn.Average(t => t[1]);
                for (int i = 0; i < listAct.Count; i++)
                    ret.Add(listAct[i], calcElemAct(dcasValue, listAct[i])[0]);
            }
            return ret;
        }



        /// <summary>
        /// Выбрать из исходных данных только те, которые удовлетворяют timeNum, year для заданного временного интервала,
        /// и применить к ним указанное математическое действие
        /// </summary>
        /// <param name="data">данные</param>
        /// <param name="act">мат. действие над выбранными данными</param>
        /// <param name="weight">веса данных </param>
        /// <returns></returns>
        private static double[] calcData(double[] data, SakuraMeta.Enums.Action act, double[] weight)
        {


            switch (act)
            {
                case SakuraMeta.Enums.Action.AVG_WEIGHT:
                    return Common.MathSupport.CalcAvgWeight(data, weight);
                case SakuraMeta.Enums.Action.AVG:
                case SakuraMeta.Enums.Action.MIN:
                case SakuraMeta.Enums.Action.MAX:
                case SakuraMeta.Enums.Action.SUM:
                    return calcElemAct(data.ToList(), act);
                default:
                    throw new Exception("Unsupported action = " + act);
            }
        }
        /// <summary>
        /// Элементарные действия над массивом данных
        /// </summary>
        /// <param name="data"></param>
        /// <param name="act"></param>
        /// <returns>value,countData</returns>
        private static double[] calcElemAct(List<double> data, SakuraMeta.Enums.Action act)
        {
            var calcData = data.Where(t => !double.IsNaN(t)).ToList();
            switch (act)
            {
                case SakuraMeta.Enums.Action.AVG:
                    return new double[] { calcData.Average(), calcData.Count };
                case SakuraMeta.Enums.Action.MIN:
                    return new double[] { calcData.Min(), calcData.Count };
                case SakuraMeta.Enums.Action.MAX:
                    return new double[] { calcData.Max(), calcData.Count };
                case SakuraMeta.Enums.Action.SUM:
                    return new double[] { calcData.Sum(), calcData.Count };
                default:
                    throw new Exception("Unsupported action = " + act);
            }

        }


    }

}
