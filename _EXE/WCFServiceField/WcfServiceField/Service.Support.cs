using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOV.WcfService.Field.AmurServiceReference;
using SOV.Amur.Meta;

namespace SOV.WcfService.Field
{
    public partial class Service
    {
        /// <summary>
        /// 
        /// Получить записи каталогов исходного метода прогноза 
        /// для записей каталогов прогнозов в точках.
        /// 
        /// </summary>
        /// <param name="siteCatalogs">Записи каталогов метода, производного от исходного.</param>
        private List<Catalog> GetParentFcsCatalogs(List<Catalog> siteCatalogs)
        {
            Check(siteCatalogs);
            List<Variable> variables = _amurClient.GetVariablesByList(_amurServiceHandle, siteCatalogs.Select(x => x.VariableId).ToList());
            // GET PARENT FCS METHOD
            int? parentMethodId = _amurClient.GetMethod(_amurServiceHandle, siteCatalogs[0].MethodId).ParentId;
            if (!parentMethodId.HasValue)
                throw new Exception(string.Format("Для метода {0} отсутствует родитель.", siteCatalogs[0].MethodId));
            MethodExt parentMethodExt = _methodsValid.FirstOrDefault(x => x.Method.Id == parentMethodId);
            Check(parentMethodExt.Method);

            // GET ALL FCS CATALOGS
            List<Catalog> parentMethodCatalogs = _amurClient.GetCatalogList(_amurServiceHandle,
                null, // new List<int>() { fcsSiteId },
                null, // pointCatalogs.Select(x => x.VariableId).Distinct().ToList(),
                new List<int>() { parentMethodExt.Method.Id },
                new List<int>() { parentMethodExt.Method.SourceLegalEntityId.HasValue ? (int)parentMethodExt.Method.SourceLegalEntityId : siteCatalogs[0].SourceId },
                siteCatalogs.Select(x => x.OffsetTypeId).Distinct().ToList(),
                siteCatalogs.Select(x => x.OffsetValue).Distinct().ToList()
                );
            if (parentMethodCatalogs.Count() == 0)
                throw new Exception("(parentMethodCatalogs.Count() != 0)");
            if (parentMethodCatalogs.Select(x => x.SiteId).Distinct().Count() != 1)
                throw new Exception("(fcsCatalogsAll.Select(x => x.SiteId).Distinct().Count() != 1)");
            variables.AddRange(_amurClient.GetVariablesByList(_amurServiceHandle, parentMethodCatalogs.Select(x => x.VariableId).ToList()));

            List<Catalog> parentCatalogs = new List<Catalog>();

            // SIFT FCS CATALOGS
            foreach (var siteCatalog in siteCatalogs)
            {
                List<Catalog> parentCatalog1 = null;
                Variable childVar = variables.Find(x => x.Id == siteCatalog.VariableId);

                // 1. PRECIPITATION
                if (childVar.VariableTypeId == (int)EnumVariableType.Precipitation)
                {
                    parentCatalog1 = parentMethodCatalogs
                        .FindAll(x
                        => variables.Exists(y => y.Id == x.VariableId && y.VariableTypeId == (int)EnumVariableType.Precipitation)
                        && x.OffsetTypeId == siteCatalog.OffsetTypeId
                        && x.OffsetValue == siteCatalog.OffsetValue
                    );
                    Check(parentCatalog1, parentMethodExt.Method, siteCatalog);
                }
                // 2. WIND (BUT NOT GUST)
                //else if (childCatalog.VariableId == (int)EnumVariable.WindDirFcs || childCatalog.VariableId == (int)EnumVariable.WindSpeedFcs)
                else if (childVar.VariableTypeId == (int)EnumVariableType.Direction || childVar.VariableTypeId == (int)EnumVariableType.WindVelocity
                    && childVar.DataTypeId != (int)EnumDataType.Maximum)
                {
                    if (!parentCatalogs.Exists(x => x.VariableId == (int)EnumVariable.UWindFcs))
                    {
                        // X-component
                        parentCatalog1 = parentMethodCatalogs.FindAll(x =>
                            x.VariableId == (int)EnumVariable.UWindFcs &&
                            x.OffsetTypeId == siteCatalog.OffsetTypeId &&
                            x.OffsetValue == siteCatalog.OffsetValue
                        );
                        Check(parentCatalog1, parentMethodExt.Method, siteCatalog);
                        parentCatalogs.Add(parentCatalog1[0]);

                        // Y-component
                        parentCatalog1 = parentMethodCatalogs.FindAll(x
                            => x.VariableId == (int)EnumVariable.VWindFcs
                            && x.OffsetTypeId == siteCatalog.OffsetTypeId
                            && x.OffsetValue == siteCatalog.OffsetValue
                        );
                        Check(parentCatalog1, parentMethodExt.Method, siteCatalog);
                    }
                }
                // 3. OTHER VARS
                else
                {
                    parentCatalog1 = parentMethodCatalogs.FindAll(x
                        => x.VariableId == siteCatalog.VariableId
                        && x.OffsetTypeId == siteCatalog.OffsetTypeId
                        && x.OffsetValue == siteCatalog.OffsetValue
                    );
                    Check(parentCatalog1, parentMethodExt.Method, siteCatalog);
                }

                // ADD CATALOG
                if (parentCatalog1 != null && !parentCatalogs.Exists(x => x.Id == parentCatalog1[0].Id))
                    parentCatalogs.Add(parentCatalog1[0]);
            }
            return parentCatalogs;
        }

        /// <summary>
        /// Проверка набора parentCatalogs на единственный элемент.
        /// </summary>
        /// <param name="parentCatalogs"></param>
        /// <param name="parentMethod"></param>
        /// <param name="childCatalog"></param>
        private void Check(List<Catalog> parentCatalogs, Method parentMethod, Catalog childCatalog)
        {
            if (parentCatalogs == null || parentCatalogs.Count == 0)
            {
                throw new Exception(string.Format("Для записи каталога [{0}] ({1}) не найдена соответствующая ей запись каталога метода [{2}/{3}].",
                    string.Format("{0}.{1}.{2}.{3}.{4}.{5}.{6}", childCatalog.Id, childCatalog.SiteId, childCatalog.VariableId, childCatalog.MethodId, childCatalog.SourceId, childCatalog.OffsetTypeId, childCatalog.OffsetValue),
                    _amurClient.GetVariableById(_amurServiceHandle, childCatalog.VariableId).NameRus,
                    parentMethod.Name, parentMethod.Id));
            }
            if (parentCatalogs.Count != 1)
                throw new Exception(string.Format("Для записи каталога {0} найдено более одной записи каталога метода {2}. Всего найдено {1} записей, а должно быть 1.", childCatalog.Id, parentCatalogs.Count, parentMethod.Id));
        }
        private string GetOutStoreParameter(Method method, string paramName, bool isThrowIfNotExists = false)
        {
            string ret;
            if (!method.MethodOutputStoreParameters.TryGetValue(paramName, out ret))
                throw new Exception(string.Format(
                    "Для запрошенного метода {0} отсутствует поле <{1}> в списке параметров хранилища метода.\n" +
                    "Укажите поле <{1}> для метода в таблице Amur.method.\n", this.ToString(), paramName));
            return ret;
        }

        private object GetDataFilter(MethodExt methodExt, List<SGMO.Varoff> varoffs)//, List<object> methodAttr)
        {
            string methodFormat = GetOutStoreParameter(methodExt.Method, "FORMAT", true);
            switch (methodFormat)
            {
                case "GRIB2":

                    if (methodExt.MethVaroffXGrib2 == null)
                        throw new Exception(string.Format("Для запрошенного метода <{0}> с форматом выходных данных {1} отсутствует соответствие с переменными Amur, которые д.б. указаны в БД sgmo табл variable_x_grib2. Источник ошибки {2}\n", methodExt, methodFormat, this));

                    List<Grib.Grib2Filter> grib2Filters = new List<Grib.Grib2Filter>();

                    foreach (var varoff in varoffs)
                    {
                        SGMO.MethVaroffXGrib2 methvarXGrib2 = methodExt.MethVaroffXGrib2.FirstOrDefault(x =>
                            x.SrcName == _dbAmurName
                            && x.MethodId == methodExt.Method.Id
                            && x.VariableId == varoff.VariableId
                            && x.OffsetTypeId == varoff.OffsetTypeId
                            && x.OffsetValue == varoff.OffsetValue
                            );
                        //if (methvarXGrib2 == null)
                        //    throw new Exception(string.Format(
                        //    "Для запрошенного набора varoff [<{0}>]" +
                        //    " отсутствует соответствие с переменными формата {1} метода {3}, которые д.б. указаны в sgmo.variable_x_grib2." +
                        //    "\nИсточник ошибки {2}\n", varoff, methodFormat, this, methodExt));

                        grib2Filters.Add(methvarXGrib2?.Grib2Filter);
                    }
                    return (object)grib2Filters;

                case "FILE_VAN2011":
                    List<int> iClolumns = new List<int>();
                    foreach (var varoff in varoffs)
                    {
                        object o = SGMO.MethVarXVar.GetExtMethodVar(methodExt.Method.Id, varoff);
                        if (o == null) throw new Exception(string.Format(
                            "Для запрошенного набора varoff  отсутствует соответствие с переменными формата {1} метода {3}," +
                            " которые д.б. указаны в классе SGMO.MethVarXVar.\nИсточник ошибки {2}\n",
                            varoff, methodFormat, this, methodExt));
                        // VAN-file column index
                        iClolumns.Add(int.Parse(o.ToString()));
                    }
                    return (object)iClolumns;
                default:
                    throw new Exception(string.Format("При попытке получения фильтра данных (GetDataFilter(...)) для запрошенного метода <{0}> обнаружен неизвестный формат данных {1}. " +
                    "Источник ошибки {2}.\n", methodExt.Method.Name, methodFormat, this));
            }
        }
        private List<SGMO.Varoff> GetVaroffs(List<Catalog> catalogs)
        {
            List<SGMO.Varoff> ret = new List<SGMO.Varoff>();
            foreach (var catalog in catalogs)
            {
                ret.Add(new SGMO.Varoff() { VariableId = catalog.VariableId, OffsetTypeId = catalog.OffsetTypeId, OffsetValue = catalog.OffsetValue });
            }
            return ret;
        }

        private void CheckHandle(long hSvc)
        {
            Common.User user;
            if (!_usersSessions.TryGetValue(hSvc, out user))
                throw new Exception("Указанный идентификатор сессии " + hSvc + " не зарегестрирован в сервисе.");
        }


        private MethodExt GetMethod(int methodId)
        {
            MethodExt method = _methodsValid.FirstOrDefault(x => x.Method.Id == methodId);
            Check(method.Method);

            return method;
        }

        private void Check(List<double> leadTimes)
        {
            if ((object)leadTimes == null)
                throw new Exception("Не указаны прогностические заблаговременности.");
        }
        /// <summary>
        /// Проверка на не null.
        /// </summary>
        /// <param name="method"></param>
        private void Check(Method method)
        {
            if (method == null)
                throw new Exception(string.Format("Запрошенный метод <{0}> не обслуживается сервисом.", method));
        }
        /// <summary>
        /// Проверка на единственность метода прогноза.
        /// </summary>
        /// <param name="catalogs"></param>
        private void Check(List<Catalog> catalogs)
        {
            if (catalogs == null || catalogs.Count == 0) throw new Exception(string.Format("Выборка данных. Список записей каталога пуст."));
            if (catalogs.Exists(x => x.MethodId != catalogs[0].MethodId))
                throw new Exception(string.Format("Запрошенные записи каталога данных относятся к разным методам (Catalog.MethodId), что в данной версии не реализовано."));
        }
        /// <summary>
        /// Проверка на непустой набор.
        /// </summary>
        /// <param name="catalogsId"></param>
        private void Check(List<int> catalogsId)
        {
            if (catalogsId == null || catalogsId.Count == 0) throw new Exception(string.Format("Список кодов записей каталога пуст."));
        }
        /// <summary>
        /// Преобразовать массив данных для записей каталога поля для отдельной точки
        /// в данные для записей каталога пунктов (точек).
        /// 
        /// </summary>
        /// <param name="parentData">Данные, полученные из прогностических полей, для параметров полей.</param>
        /// <param name="ctlParents">Записи каталога для прогностических полей.</param>
        /// <param name="ctlPoints">Записи каталога для пунктов.</param>
        /// <param name="pointXsites">Соответствие координат пунктов их кодам (id).</param>
        /// <param name="leadTimes"></param>
        /// <param name="precipSumResetTime">Временной период накопления осадков. Может быть больше прогностического шага по времени. Если период равен шагу, то значение д.б. равно null.</param>
        /// <returns>Массив прогностических данных для пунктов (точек), соответствующий записям каталога этих точек.</returns>
        private double[/*leadTime*/][/*point Catalog index*/] ConvertDataParent2Point(
            double[/*leadTime*/][/*GeoPoint index*/][/*parent Catalog index*/] parentData,
            List<Catalog> ctlParents,
            List<Catalog> ctlPoints,
            Dictionary<int, Geo.GeoPoint> siteXpoint,
            double[] leadTimes,
            double? precipSumResetTime)
        {
            if (parentData.Length != leadTimes.Length) throw new Exception("(parentData.Length != leadHours.Length)");

            double[/*leadTime*/][/*Catalog of point*/] ret = Common.Support.Allocate(leadTimes.Length, ctlPoints.Count, double.NaN);
            List<Geo.GeoPoint> points = siteXpoint.Values.ToList();

            // GET VARIABLES
            List<Variable> pointVariables = _amurClient.GetVariablesByList(_amurServiceHandle, ctlPoints.Select(x => x.VariableId).Distinct().ToList());

            // GET PARENT FOR PRECIP
            List<Variable> parentVarPrecip = _amurClient.GetVariablesByList(_amurServiceHandle, ctlParents.Select(x => x.VariableId).Distinct().ToList());
            parentVarPrecip = parentVarPrecip.FindAll(x => x.VariableTypeId == (int)EnumVariableType.Precipitation);
            if (parentVarPrecip.Count != 1)
                throw new Exception(string.Format("В прогнозах полей присутствует не единственный тип переменной для осадков. Всего их {0}. Ошибка алгоритма.", parentVarPrecip.Count));
            List<Catalog> parentCatalogPrecips = ctlParents.FindAll(x => x.VariableId == parentVarPrecip[0].Id);
            if (parentCatalogPrecips.Count != 1)
                throw new Exception(string.Format("В прогнозах полей присутствует не единственная запись каталога для осадков. Всего их {0}. Ошибка алгоритма.", parentCatalogPrecips.Count));
            int iParentCatalogPrecip = ctlParents.IndexOf(parentCatalogPrecips[0]);

            //new List<int>() { (int)EnumTime.Second },
            //null,
            //new List<int>() { (int)EnumDataType.Cumulative },
            //null, null, null,
            //new List<int>() { (int)EnumVariableType.Precipitation }

            // SCAN POINTS CATALOGS

            for (int iPC = 0; iPC < ctlPoints.Count; iPC++)
            {
                Catalog pointCatalog = ctlPoints[iPC];
                Variable pointVariable = pointVariables.Find(x => x.Id == pointCatalog.VariableId);
                int iPoint = points.IndexOf(siteXpoint[pointCatalog.SiteId]);
                double[] pointValues;

                // 1. PRECIITATION
                if (pointVariable.VariableTypeId == (int)EnumVariableType.Precipitation)
                {
                    pointValues = ConvertPrecipitation(pointVariable, parentData, iPoint, iParentCatalogPrecip, leadTimes, (double)precipSumResetTime);
                }
                // 2. WIND SPEED
                else if ((pointVariable.VariableTypeId == (int)EnumVariableType.Direction || pointVariable.VariableTypeId == (int)EnumVariableType.WindVelocity)
                    && pointVariable.DataTypeId != (int)EnumDataType.Maximum)
                {
                    pointValues = ConvertUV(ctlParents, parentData, iPoint, pointVariable, pointCatalog.OffsetTypeId, pointCatalog.OffsetValue, leadTimes);
                }
                // 3. OTHER VARS
                else
                {
                    List<Catalog> parentCatalog = ctlParents.FindAll(x
                        => x.VariableId == pointCatalog.VariableId
                        && x.OffsetTypeId == pointCatalog.OffsetTypeId
                        && x.OffsetValue == pointCatalog.OffsetValue
                    );
                    if (parentCatalog.Count > 1)
                    {
                        throw new Exception("(parentCatalog.Count > 1)");
                    }

                    pointValues = Common.Support.Allocate(leadTimes.Length, double.NaN);

                    if (parentCatalog.Count == 1)
                    {
                        int iParentCatalog = ctlParents.IndexOf(parentCatalog[0]);
                        for (int iLT = 0; iLT < leadTimes.Length; iLT++)
                        {
                            pointValues[iLT] = parentData[iLT][iPoint][iParentCatalog];
                        }
                    }
                }
                for (int iLT = 0; iLT < leadTimes.Length; iLT++)
                {
                    ret[iLT][iPC] = pointValues[iLT];
                }
            }
            return ret;
        }

        private double[] ConvertUV(List<Catalog> ctlParents, double[][][] parentData, int iPoint, Variable pointVar, int pointOffsetTypeId, double pointOffsetValue, double[] leadTimeHours)
        {
            double[] ret = Common.Support.Allocate(leadTimeHours.Length, double.NaN);

            int iParentCatalogU = ctlParents.IndexOf(ctlParents.First(x
                => x.VariableId == (int)EnumVariable.UWindFcs
                && x.OffsetTypeId == pointOffsetTypeId
                && x.OffsetValue == pointOffsetValue
            ));
            int iParentCatalogV = ctlParents.IndexOf(ctlParents.First(x
                => x.VariableId == (int)EnumVariable.VWindFcs
                && x.OffsetTypeId == pointOffsetTypeId
                && x.OffsetValue == pointOffsetValue
            ));

            for (int iLT = 0; iLT < leadTimeHours.Length; iLT++)
            {
                double u = parentData[iLT][iPoint][iParentCatalogU];
                double v = parentData[iLT][iPoint][iParentCatalogV];
                if (double.IsNaN(u) || double.IsNaN(v))
                    throw new Exception(string.Format("Не число для U,V ветера GFS: {0} {1}", u, v));

                double dir = Common.Vector.uv2Azimuth(u, v);
                double mod = Common.Vector.uv2Module(u, v);

                if (pointVar.VariableTypeId == (int)EnumVariableType.Direction)
                    ret[iLT] = dir;
                if (pointVar.VariableTypeId == (int)EnumVariableType.WindVelocity)
                    ret[iLT] = mod;
            }
            return ret;
        }

        private double[] ConvertPrecipitation(Variable pointVariable, double[][][] parentData, int iPoint, int iParentCatalogPrecip, double[] leadTimeHours, double resetTimeHours)
        {
            if (pointVariable.TimeId != (int)EnumTime.Second)
                throw new Exception("(pointVariable.TimeId != (int)EnumTime.Second)");

            double[] parentPrecip = Common.Support.Allocate(leadTimeHours.Length, double.NaN);
            for (int i = 0; i < leadTimeHours.Length; i++)
            {
                parentPrecip[i] = parentData[i][iPoint][iParentCatalogPrecip];
            }

            return GetPrecip4LeadTimeStep(parentPrecip, leadTimeHours, pointVariable.TimeSupport, resetTimeHours);
        }

        private double[/*leadTime*/][/*point Catalog index*/] _DELME_ConvertDataParent2Point(double[/*leadTime*/][/*GeoPoint index*/][/*parent Catalog index*/] parentData, List<Catalog> parentCatalogs, List<Catalog> pointCatalogs, Dictionary<int, Geo.GeoPoint> siteXpoint, double[] leadTimes, double? precipSumResetTime)
        {
            if (parentData.Length != leadTimes.Length) throw new Exception("(parentData.Length != leadHours.Length)");

            double[/*leadTime*/][/*point Catalog index*/] ret = Common.Support.Allocate(leadTimes.Length, pointCatalogs.Count, double.NaN);
            List<Geo.GeoPoint> points = siteXpoint.Values.ToList();

            // FOR PRECIPITATION

            double leadTimeStep = leadTimes[1] - leadTimes[0];
            int precipVariableId = -1;
            if (leadTimeStep == 3 && precipSumResetTime == 6)
                precipVariableId = (int)Amur.Meta.EnumVariable.PrecipHour03Fcs;
            else
                throw new Exception("Не удалось определить код переменной для прогностических осадков: " + leadTimeStep + ";" + precipSumResetTime);

            // SCAN POINTS CATALOGS

            for (int iPC = 0; iPC < pointCatalogs.Count; iPC++)
            {
                Catalog pointCatalog = pointCatalogs[iPC];
                int iPoint = points.IndexOf(siteXpoint[pointCatalog.SiteId]);

                List<Catalog> parentCatalog = parentCatalogs.FindAll(x
                    => x.VariableId == pointCatalog.VariableId
                    && x.OffsetTypeId == pointCatalog.OffsetTypeId
                    && x.OffsetValue == pointCatalog.OffsetValue
                );
                int iParentCatalog = -1;
                if (parentCatalog.Count == 1)
                    iParentCatalog = parentCatalogs.IndexOf(parentCatalog[0]);
                //int iParentCatalog = parentCatalogs.IndexOf(parentCatalogs.First(x
                //    => x.VariableId == pointCatalog.VariableId
                //    && x.OffsetTypeId == pointCatalog.OffsetTypeId
                //    && x.OffsetValue == pointCatalog.OffsetValue
                //));

                double[] prec = Common.Support.Allocate(leadTimes.Length, double.NaN);

                for (int iLT = 0; iLT < leadTimes.Length; iLT++)
                {
                    // IF PRECIPITATION - collect precipitation for further processing
                    if (pointCatalog.VariableId == precipVariableId)
                        prec[iLT] = parentData[iLT][iPoint][iParentCatalog];
                    else
                        // NOT PRECIPITATION
                        switch (pointCatalog.VariableId)
                        {
                            case (int)SOV.Amur.Meta.EnumVariable.WindDirFcs:
                            case (int)SOV.Amur.Meta.EnumVariable.WindSpeedFcs:

                                int iParentCatalogU = parentCatalogs.IndexOf(parentCatalogs.First(x
                                    => x.VariableId == (int)SOV.Amur.Meta.EnumVariable.UWindFcs
                                    && x.OffsetTypeId == pointCatalog.OffsetTypeId
                                    && x.OffsetValue == pointCatalog.OffsetValue
                                ));
                                int iParentCatalogV = parentCatalogs.IndexOf(parentCatalogs.First(x
                                    => x.VariableId == (int)SOV.Amur.Meta.EnumVariable.VWindFcs
                                    && x.OffsetTypeId == pointCatalog.OffsetTypeId
                                    && x.OffsetValue == pointCatalog.OffsetValue
                                ));

                                double u = parentData[iLT][iPoint][iParentCatalogU];
                                double v = parentData[iLT][iPoint][iParentCatalogV];
                                if (!double.IsNaN(u) && !double.IsNaN(v))
                                {
                                    double dir = Common.Vector.uv2Azimuth(u, v);
                                    double mod = Common.Vector.uv2Module(u, v);

                                    if (pointCatalog.VariableId == (int)SOV.Amur.Meta.EnumVariable.WindDirFcs)
                                        ret[iLT][iPC] = dir;
                                    if (pointCatalog.VariableId == (int)SOV.Amur.Meta.EnumVariable.WindSpeedFcs)
                                        ret[iLT][iPC] = mod;
                                }
                                else
                                    throw new Exception(string.Format("Не число для U,V ветера GFS: {0} {1}", u, v));
                                break;
                            default:
                                ret[iLT][iPC] = parentData[iLT][iPoint][iParentCatalog];
                                break;
                        }
                }
                // IF PRECIPITATION - process rest time period
                if (pointCatalog.VariableId == precipVariableId)
                {
                    if (precipSumResetTime.HasValue)
                        prec = _DELME_GetPrecip4LeadTimeStep(prec, leadTimes, (double)precipSumResetTime);
                    for (int iLT = 0; iLT < leadTimes.Length; iLT++)
                    {
                        ret[iLT][iPC] = prec[iLT];
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Преобразование накопленных за заданное время осадков, например, в GFS, к осадкам за шаг прогноза (определение разностей).
        /// Производится обнуление для отрицательных сумм осадков.
        /// 
        /// </summary>
        /// <param name="gfsPrecips">Осадки с заданными заблаговременностями.</param>
        /// <param name="leadTimesHours">Заблаговременности с постоянным шагом, например, 3 часа.</param>
        /// <param name="restTimeHours">Интервал в пределах которого происходит накопление осадков в модели (в ед. изм. прогностического шага).</param>
        /// <returns>Осадки для указанных заблаговременностей, накопленные между заблаговременностями.</returns>
        static public double[] GetPrecip4LeadTimeStep(double[] gfsPrecips, double[] leadTimesHours, int precipAccumSeconds, double restTimeHours)
        {
            double[] ret = SOV.Common.Support.Allocate(leadTimesHours.Length, double.NaN);
            if (leadTimesHours.Length < 2 || gfsPrecips.Length != leadTimesHours.Length || (leadTimesHours[0] != 0)) return ret;

            double leadTimeAccumulated = 0;
            int leadTimesQ = 0;

            for (int i = 1; i < leadTimesHours.Length; i++)
            {
                string msg = string.Format("GetPrecip4LeadTimeStep: {0} {1}\n", leadTimesHours[i], gfsPrecips[i]);
                System.IO.File.AppendAllText(_logFilePath, msg);

                leadTimeAccumulated += ((leadTimesHours[i] - leadTimesHours[i - 1]) * 3600);
                leadTimesQ++;

                if (precipAccumSeconds < leadTimeAccumulated)
                {
                    leadTimeAccumulated = 0;
                    leadTimesQ = 0;
                    continue;
                }
                if (precipAccumSeconds == leadTimeAccumulated)
                {
                    double precipValue = double.NaN;
                    // Если предыдущая забл кратна времени релаксации осадков
                    if (Math.IEEERemainder(leadTimesHours[i - leadTimesQ], restTimeHours) == 0)
                    {
                        precipValue = gfsPrecips[i];
                    }
                    else
                    {
                        precipValue = gfsPrecips[i] - gfsPrecips[i - leadTimesQ];
                        if (precipValue < -0.99)
                            throw new Exception(string.Format(
                                "Осадки предыдущей заблаговременности ({0}) больше, чем осадки текущей ({1}) на {2}: {3} vs {4}",
                                leadTimesHours[i - 1], leadTimesHours[i], -precipValue, gfsPrecips[i], gfsPrecips[i - 1]
                                ));
                    }
                    precipValue = (precipValue < 0) ? 0 : precipValue;
                    ret[i] = precipValue;

                    leadTimeAccumulated = 0;
                    leadTimesQ = 0;
                }
            }
            return ret;
        }
        static public double[] _DELME_GetPrecip4LeadTimeStep(double[] gfsPrecips, double[] leadTimes, double restTime)
        {
            ////if (leadTimes.Length < 2 || gfsPrecips.Length != leadTimes.Length) return null;

            double[] ret = SOV.Common.Support.Allocate(leadTimes.Length, double.NaN);
            double leadTimeStep = leadTimes[1] - leadTimes[0];

            if (leadTimes[0] != 0 && ((int)(leadTimes[0] / restTime)) * restTime + leadTimeStep == leadTimes[0])
                ret[0] = gfsPrecips[0];

            for (int i = 1; i < leadTimes.Length; i++)
            {
                if (leadTimes[i] - leadTimeStep != leadTimes[i - 1])
                    throw new Exception("Нарушен шаг заблаговременностей прогноза: " + leadTimes[i] + " - " + leadTimes[i - 1] + " != " + leadTimeStep);

                if (((int)(leadTimes[i] / restTime)) * restTime + leadTimeStep == leadTimes[i])
                {
                    ret[i] = gfsPrecips[i];
                }
                else
                {
                    double precip4LagStep = gfsPrecips[i] - gfsPrecips[i - 1];
                    if (precip4LagStep < -0.99)
                        throw new Exception(string.Format(
                            "Осадки предыдущей заблаговременности ({0}) больше, чем осадки текущей ({1}) на {2}: {3} vs {4}",
                            leadTimes[i - 1], leadTimes[i], -precip4LagStep, gfsPrecips[i], gfsPrecips[i - 1]
                            ));
                    else if (precip4LagStep < 0)
                        precip4LagStep = 0;
                    ret[i] = precip4LagStep;
                }
            }
            return ret;
        }
    }
}