using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOV.WcfService.Field.AmurServiceReference;

namespace SOV.WcfService.Field
{
    public partial class Service
    {
        //private List<Catalog> GetCatalogs(List<int> catalogIds)
        //{
        //    if (catalogIds == null || catalogIds.Count == 0) throw new Exception(string.Format("Выборка данных. Список записей каталога пуст."));

        //    List<Catalog> catalogs = _amurClient.GetCatalogListById(_amurServiceHandle, catalogIds);

        //    int methodId = catalogs[0].MethodId;
        //    if (catalogs.Exists(x => x.MethodId != methodId))
        //        throw new Exception(string.Format("Запрошенные записи каталога данных относятся к разным методам (Catalog.MethodId), что в данной версии не реализовано."));

        //    return catalogs;
        //}

        /// <summary>
        /// 
        /// Получить записи каталогов исходного метода прогноза 
        /// для записей каталогов прогнозов в точках.
        /// 
        /// </summary>
        /// <param name="childCatalogs">Записи каталогов метода, производного от исходного.</param>
        private List<Catalog> GetParentFcsCatalogs(List<Catalog> childCatalogs)
        {
            Check(childCatalogs);
            List<Catalog> parentCatalogs = new List<Catalog>();

            // GET PARENT FCS METHOD
            int? parentMethodId = _amurClient.GetMethod(_amurServiceHandle, childCatalogs[0].MethodId).ParentId;
            if (!parentMethodId.HasValue)
                throw new Exception(string.Format("Для метода {0} отсутствует родитель.", childCatalogs[0].MethodId));
            MethodExt parentMethodExt = _methodsValid.FirstOrDefault(x => x.Method.Id == parentMethodId);
            Check(parentMethodExt.Method);

            // GET ALL FCS CATALOGS
            List<Catalog> parentMethodCatalogs = _amurClient.GetCatalogList(_amurServiceHandle,
                null, // new List<int>() { fcsSiteId },
                null, // pointCatalogs.Select(x => x.VariableId).Distinct().ToList(),
                new List<int>() { parentMethodExt.Method.Id },
                new List<int>() { parentMethodExt.Method.SourceLegalEntityId.HasValue ? (int)parentMethodExt.Method.SourceLegalEntityId : childCatalogs[0].SourceId },
                childCatalogs.Select(x => x.OffsetTypeId).Distinct().ToList(),
                childCatalogs.Select(x => x.OffsetValue).Distinct().ToList()
                );
            if (parentMethodCatalogs.Count() == 0)
                throw new Exception("(parentMethodCatalogs.Count() != 0)");
            if (parentMethodCatalogs.Select(x => x.SiteId).Distinct().Count() != 1)
                throw new Exception("(fcsCatalogsAll.Select(x => x.SiteId).Distinct().Count() != 1)");

            // SIFT FCS CATALOGS
            foreach (var childCatalog in childCatalogs)
            {
                List<Catalog> parentCatalog1 = null;

                // Specific: wind case
                if (childCatalog.VariableId == (int)SOV.Amur.Meta.EnumVariable.WindDirFcs || childCatalog.VariableId == (int)SOV.Amur.Meta.EnumVariable.WindSpeedFcs)
                {
                    if (!parentCatalogs.Exists(x => x.VariableId == (int)SOV.Amur.Meta.EnumVariable.UWindFcs))
                    {
                        // X-component
                        parentCatalog1 = parentMethodCatalogs.FindAll(x =>
                            x.VariableId == (int)SOV.Amur.Meta.EnumVariable.UWindFcs &&
                            x.OffsetTypeId == childCatalog.OffsetTypeId &&
                            x.OffsetValue == childCatalog.OffsetValue
                        );
                        Check(parentCatalog1, parentMethodExt.Method, childCatalog);
                        parentCatalogs.Add(parentCatalog1[0]);

                        // Y-component
                        parentCatalog1 = parentMethodCatalogs.FindAll(x
                            => x.VariableId == (int)SOV.Amur.Meta.EnumVariable.VWindFcs
                            && x.OffsetTypeId == childCatalog.OffsetTypeId
                            && x.OffsetValue == childCatalog.OffsetValue
                        );
                        Check(parentCatalog1, parentMethodExt.Method, childCatalog);
                    }
                }
                // Not vector-type variables
                else
                {
                    parentCatalog1 = parentMethodCatalogs.FindAll(x
                        => x.VariableId == childCatalog.VariableId
                        && x.OffsetTypeId == childCatalog.OffsetTypeId
                        && x.OffsetValue == childCatalog.OffsetValue
                    );
                    Check(parentCatalog1, parentMethodExt.Method, childCatalog);
                }
                if (parentCatalog1 != null)
                    parentCatalogs.Add(parentCatalog1[0]);
            }
            return parentCatalogs;
        }

        //////private object[/*Method parentMethod;List<Catalog> parentCatalogs;parentMethodForecast*/] GetParentFcsCatalogs(List<Catalog> childCatalogs)
        //////{
        //////    Check(childCatalogs);
        //////    List<Catalog> parentCatalogs = new List<Catalog>();

        //////    // GET PARENT FCS METHOD
        //////    int? parentMethodId = _amurClient.GetMethod(_amurServiceHandle, childCatalogs[0].MethodId).ParentId;
        //////    if (!parentMethodId.HasValue)
        //////        throw new Exception(string.Format("Для метода {0} отсутствует родитель.", childCatalogs[0].MethodId));
        //////    MethodExt parentMethodExt = _methodsValid.FirstOrDefault(x => x.Method.Id == parentMethodId);
        //////    Check(parentMethodExt.Method);

        //////    // GET ALL FCS CATALOGS
        //////    List<Catalog> parentMethodCatalogs = _amurClient.GetCatalogList(_amurServiceHandle,
        //////        null, // new List<int>() { fcsSiteId },
        //////        null, // pointCatalogs.Select(x => x.VariableId).Distinct().ToList(),
        //////        new List<int>() { parentMethodExt.Method.Id },
        //////        new List<int>() { parentMethodExt.Method.SourceLegalEntityId.HasValue ? (int)parentMethodExt.Method.SourceLegalEntityId : childCatalogs[0].SourceId },
        //////        childCatalogs.Select(x => x.OffsetTypeId).Distinct().ToList(),
        //////        childCatalogs.Select(x => x.OffsetValue).Distinct().ToList()
        //////        );
        //////    if (parentMethodCatalogs.Count() == 0)
        //////        throw new Exception("(parentMethodCatalogs.Count() != 0)");
        //////    if (parentMethodCatalogs.Select(x => x.SiteId).Distinct().Count() != 1)
        //////        throw new Exception("(fcsCatalogsAll.Select(x => x.SiteId).Distinct().Count() != 1)");

        //////    // SIFT FCS CATALOGS
        //////    foreach (var childCatalog in childCatalogs)
        //////    {
        //////        List<Catalog> parentCatalog1 = null;

        //////        // Specific: wind case
        //////        if (childCatalog.VariableId == (int)SOV.Amur.Meta.EnumVariable.WindDirFcs || childCatalog.VariableId == (int)SOV.Amur.Meta.EnumVariable.WindSpeedFcs)
        //////        {
        //////            if (!parentCatalogs.Exists(x => x.VariableId == (int)SOV.Amur.Meta.EnumVariable.UWindFcs))
        //////            {
        //////                // X-component
        //////                parentCatalog1 = parentMethodCatalogs.FindAll(x =>
        //////                    x.VariableId == (int)SOV.Amur.Meta.EnumVariable.UWindFcs &&
        //////                    x.OffsetTypeId == childCatalog.OffsetTypeId &&
        //////                    x.OffsetValue == childCatalog.OffsetValue
        //////                );
        //////                Check(parentCatalog1, parentMethodExt.Method, childCatalog);
        //////                parentCatalogs.Add(parentCatalog1[0]);

        //////                // Y-component
        //////                parentCatalog1 = parentMethodCatalogs.FindAll(x
        //////                    => x.VariableId == (int)SOV.Amur.Meta.EnumVariable.VWindFcs
        //////                    && x.OffsetTypeId == childCatalog.OffsetTypeId
        //////                    && x.OffsetValue == childCatalog.OffsetValue
        //////                );
        //////                Check(parentCatalog1, parentMethodExt.Method, childCatalog);
        //////            }
        //////        }
        //////        // Not vector-type variables
        //////        else
        //////        {
        //////            parentCatalog1 = parentMethodCatalogs.FindAll(x
        //////                => x.VariableId == childCatalog.VariableId
        //////                && x.OffsetTypeId == childCatalog.OffsetTypeId
        //////                && x.OffsetValue == childCatalog.OffsetValue
        //////            );
        //////            Check(parentCatalog1, parentMethodExt.Method, childCatalog);
        //////        }
        //////        if (parentCatalog1 != null)
        //////            parentCatalogs.Add(parentCatalog1[0]);
        //////    }
        //////    return new object[] { parentMethodExt, parentCatalogs };
        //////}

        /// <summary>
        /// Проверка набора parentCatalogs на единственный элемент.
        /// </summary>
        /// <param name="parentCatalogs"></param>
        /// <param name="parentMethod"></param>
        /// <param name="childCatalog"></param>
        private void Check(List<Catalog> parentCatalogs, Method parentMethod, Catalog childCatalog)
        {
            if (parentCatalogs == null || parentCatalogs.Count == 0)
                throw new Exception(string.Format("Для записи каталога {0} не найдена соответствующая ей запись каталога метода {1}.", childCatalog.Id, parentMethod.Id));
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
                        if (methvarXGrib2 == null) throw new Exception(string.Format(
                            "Для запрошенного набора varoff [<{0}>]" +
                            " отсутствует соответствие с переменными формата {1} метода {3}, которые д.б. указаны в sgmo.variable_x_grib2." +
                            "\nИсточник ошибки {2}\n", varoff, methodFormat, this, methodExt));

                        grib2Filters.Add(methvarXGrib2.Grib2Filter);
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
        /// <param name="parentCatalogs">Записи каталога для прогностических полей.</param>
        /// <param name="pointCatalogs">Записи каталога для пунктов.</param>
        /// <param name="pointXsites">Соответствие координат пунктов их кодам (id).</param>
        /// <param name="leadTimes"></param>
        /// <param name="precipSumResetTime">Временной период накопления осадков. Может быть больше прогностического шага по времени. Если период равен шагу, то значение д.б. равно null.</param>
        /// <returns>Массив прогностических данных для пунктов (точек), соответствующий записям каталога этих точек.</returns>
        private double[/*leadTime*/][/*point Catalog index*/] ConvertDataParent2Point(
            double[/*leadTime*/][/*GeoPoint index*/][/*parent Catalog index*/] parentData,
            List<Catalog> parentCatalogs,
            List<Catalog> pointCatalogs,
            Dictionary<int, Geo.GeoPoint> siteXpoint,
            double[] leadTimes,
            double? precipSumResetTime)
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
                        prec = GetPrecip4LeadTimeStep(prec, leadTimes, (double)precipSumResetTime);
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
        /// <param name="leadTimes">Заблаговременности с постоянным шагом, например, 3 часа.</param>
        /// <param name="restTime">Интервал в пределах которого происходит накопление осадков в модели (в ед. изм. прогностического шага).</param>
        /// <returns>Осадки для указанных заблаговременностей, накопленные между заблаговременностями.</returns>
        static public double[] GetPrecip4LeadTimeStep(double[] gfsPrecips, double[] leadTimes, double restTime)
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