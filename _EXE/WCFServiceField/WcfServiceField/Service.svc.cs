using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Configuration;
using System.Diagnostics;

using SOV.WcfService.Field.AmurServiceReference;
//using SOV.Amur.Meta;

namespace SOV.WcfService.Field
{
    /// <summary>
    /// TODO: !!! перед service-publish убедиться в корректности строки подключения к БД в web.config !!!
    /// </summary>
    public partial class Service : IService
    {
        static string _dbAmurName;
        static AmurServiceReference.ServiceClient _amurClient;
        static long _amurServiceHandle = -100;

        static List<SOV.Common.User> _usersAllowed;
        /// <summary>
        /// Dictionary<Method, List<object>/*MethvarXGrib, MethodForecast*/>
        /// </summary>
        static List<MethodExt> _methodsValid = new List<MethodExt>();
        //static Dictionary<Method, List<object>/*MethvarXGrib, MethodForecast*/> _methodsAllowed = new Dictionary<Method, List<object>>();

        static Dictionary<long, SOV.Common.User> _usersSessions = new Dictionary<long, Common.User>();

        static string _logFilePath = Properties.Settings.Default.LOG_FILE_PATH;//@"E:\FERHRI\Log\WCFServiceFieldLog.txt";
        static bool _isOpening = false;

        /// <summary>
        /// Клнструктор по умолчанию.
        /// </summary>
        public Service()
        {
            // OPEN AMUR SERVICE
            string[] sarr = System.Configuration.ConfigurationManager.AppSettings["ServiceUser"].Split('/');
            _amurClient = new AmurServiceReference.ServiceClient();
            _amurServiceHandle = _amurClient.Open(sarr[0], sarr[1]);
            if (_amurServiceHandle < 0)
                throw new Exception(string.Format("Доступ к сервису БД Амур отклонён для пользователя <{0}>. Код возврата {1}.\n", sarr[0], _amurServiceHandle));

            if (_usersAllowed == null)
            {
                System.IO.File.AppendAllText(_logFilePath, string.Format("Service initializing started at {0}.\n", DateTime.Now));

                SOV.SGMO.DataManager.SetDefaultConnectionString(SOV.WcfService.Field.Properties.Settings.Default.SGMOConnectionString);
                _dbAmurName = SOV.WcfService.Field.Properties.Settings.Default.DB_AMUR_NAME;

                // GET VALID SERVICE METHODS

                List<MethodForecast> methForecasts = _amurClient.GetMethodForecastsAll(_amurServiceHandle);

                foreach (Method method in _amurClient.GetMethods(_amurServiceHandle, Common.StrVia.ToArray<int>(System.Configuration.ConfigurationManager.AppSettings["AmurMethodAllowed"]).ToList()))
                {
                    if (method.MethodOutputStoreParameters == null)
                    {
                        System.IO.File.AppendAllText(_logFilePath, string.Format("Method removed due to MethodOutputStoreParameters == null. Method: \n", method));
                    }

                    _methodsValid.Add(new MethodExt()
                    {
                        Method = method,
                        MethVaroffXGrib2 = SOV.SGMO.DataManager.GetInstance().MethvarXGrib2Repository.Select(_dbAmurName, method.Id),
                        MethodForecast = methForecasts.FirstOrDefault(x => x.Method.Id == method.Id)
                    });
                }

                // GET VALID USERS READ
                sarr = System.Configuration.ConfigurationManager.AppSettings["ServiceUsers"].Split(';');
                _usersAllowed = new List<Common.User>();
                foreach (var item in sarr)
                {
                    string[] s = item.Split('/');
                    if (_amurClient.Open(s[0], s[1]) < 0)
                        System.IO.File.AppendAllText(_logFilePath, string.Format("User {0} is restricted by the Amur service.\n", s[0]));
                    else
                        _usersAllowed.Add(new Common.User(s[0], s[1]));
                }

                System.IO.File.AppendAllText(_logFilePath, string.Format("Service initializing finished at {0}.\n\n", DateTime.Now));
            }
        }

        public List<Method> GetMethods(long hSvc)
        {
            CheckHandle(hSvc);
            return _methodsValid.Select(x => x.Method).ToList();
        }
        /// <summary>
        /// Открытие рабочей сессии.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <exception cref="Ошибка в имени или пароле пользователя"></exception>
        /// <returns>
        /// Дескриптор открытой сессии - целое число большее 0 или меньше 0 в случае ошибки:
        /// -1 - сервис занят открытием другой сессии;
        /// -2 - Недопустимое имя и/или пароль пользователя;
        /// или Exception()
        /// </returns>
        public long Open(string userName, string password)
        {
            System.IO.File.AppendAllText(_logFilePath, string.Format("User <{0}> open request at {1}.\n", userName, DateTime.Now));

            // Сервис в настоящий момент открываеся другим пользователем?
            if (_isOpening)
            {
                System.IO.File.AppendAllText(_logFilePath, "Сервис в настоящий момент открываеся другим пользователем. Повторите запрос позднее. Отказано в открытии сессии.\n");
                return -1;
            }
            _isOpening = true;

            try
            {
                Common.User user = _usersAllowed.FirstOrDefault(x => x.Name == userName && x.Password == password);
                if (user == null)
                {
                    System.IO.File.AppendAllText(_logFilePath, "Недопустимое имя и/или пароль пользователя. Отказано в открытии сессии.\n");
                    return -2;
                }

                KeyValuePair<long, SOV.Common.User> kvp = _usersSessions.FirstOrDefault(x => x.Value.Name == user.Name);
                if (kvp.Value != null)
                {
                    //System.IO.File.AppendAllText(_logFilePath,
                    //    string.Format("Пользователь <{0}> уже имеет открытую сессию доступа к сервису. Отказано в открытии сессии.", userName));
                    return kvp.Key;
                }

                long hSvc = Math.Abs(DateTime.Now.ToBinary());
                _usersSessions.Add(hSvc, user);
                return hSvc;
            }
            finally
            {
                _isOpening = false;
            }
        }

        /// <summary>
        /// Закрытие рабочей сессии по её идентификатору.
        /// </summary>
        /// <param name="hSvc">Идентификатор сессии, полученный методом Open.</param>
        public void Close(long hSvc)
        {
            _usersSessions.Remove(hSvc);
            Debug.Write(System.IO.File.ReadAllText(_logFilePath));
        }
        /// <summary>
        /// Закрытие рабочей сессии по имени пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        public void CloseByUserName(string userName)
        {
            KeyValuePair<long, SOV.Common.User> kvp = _usersSessions.FirstOrDefault(x => x.Value.Name == userName);
            if (kvp.Value != null)
                Close(kvp.Key);
        }
        public SOV.Field[/*LeadTime index*/][/*Georectangle index*/][/*Varoff index*/] GetFieldsInRectangles
            (long hSvc, DateTime dateIni, int methodId, List<SGMO.Varoff> varoffs, List<double> leadTimes, List<Geo.GeoRectangle> grs)
        {
            CheckHandle(hSvc);
            Check(leadTimes);

            MethodExt method = GetMethod(methodId);

            // SWITCH METHOD OUTPUT STORAGE INTERFACE

            string methOutInterface = GetOutStoreParameter(method.Method, "INTERFACE", true);
            switch (methOutInterface)
            {
                case "IFileFcsGrid":
                    // GET GRID/FIELD REPOSITORY
                    DB.IFcsGrid db = (DB.IFcsGrid)DB.Factory.GetInstance(method.Method.MethodOutputStoreParameters);
                    if (db == null) throw new Exception(string.Format(
                        "Для запрошенного интерфейса {0} метода <{1}> отсутствует репозиторий в классе SOV.DB.Factory. " +
                        "Нужно дополнить код фабрики.\n", methOutInterface, method.Method.Name, this));
                    // GET DATA FILTER 
                    object dataFilter = GetDataFilter(method, varoffs);
                    // GET DATA IN REGIONS
                    return db.ReadFieldsInRectangles(dateIni, (object)dataFilter, leadTimes, grs);
                default:
                    throw new Exception(string.Format("Для запрошенного интерфейса {0} метода <{1}> отсутствует обработчик считывания данных в <{2}>.\n",
                        methOutInterface, method.Method.Name, this));
            }
        }
        /// <summary>
        /// Получить прогнозы в указанных пунктах.
        /// </summary>
        /// <param name="hSvc">Дескриптор сессии сервиса.</param>
        /// <param name="dateIni">Исходная дата прогноза.</param>
        /// <param name="leadTimes">Заблаговременности прогноза (не null).</param>
        /// <param name="pointCatalogsId">Id записей каталогов данных пунктов с разными координатами.</param>
        /// <param name="amurSiteAttrTypeLatId">Id типа атрибута пункта "широта".</param>
        /// <param name="amurSiteAttrTypeLonId">Id типа атрибута пункта "долгота".</param>
        /// <returns></returns>
        public double[/*leadTime*/][/*point Catalog index*/] GetValuesAtPoints
            (long hSvc, DateTime dateIni, List<double> leadTimes, List<int> pointCatalogsId, int amurSiteAttrTypeLatId, int amurSiteAttrTypeLonId)
        {
            // CHECK INPUT
            CheckHandle(hSvc);
            Check(pointCatalogsId);
            Check(leadTimes);

            // GET FCS CATALOGS 4 POINT CATALOGS
            //
            // Метод прогноза в точке является производным от исходного, 
            // родительского метода прогноза полей г/м элементов.

            List<Catalog> pointCatalogs = _amurClient.GetCatalogListById(_amurServiceHandle, pointCatalogsId)
                .OrderBy(x => pointCatalogsId.IndexOf(x.Id))
                .ToList();
            Check(pointCatalogs);

            // parentMethod
            Method pointMethod = _amurClient.GetMethod(_amurServiceHandle, pointCatalogs[0].MethodId);
            List<Catalog> parentCatalogs = GetParentFcsCatalogs(pointCatalogs);
            MethodExt parentMethodExt = _methodsValid.FirstOrDefault(x => x.Method.Id == parentCatalogs[0].MethodId);

            // GET parentMethodFcs
            //////List<double> leadTimes = parentMethodExt.MethodForecast.LeadTimes;

            // GET precipSumResetTime 
            double? precipSumResetTime = null;
            string str = null;
            if (parentMethodExt.MethodForecast.Attr.TryGetValue("precip_reset_time".ToUpper(), out str))
                precipSumResetTime = double.Parse(str);

            // GET SITES POINTS 4 DATE_INI

            Dictionary<int, AmurServiceReference.GeoPoint> pointXsites1 = _amurClient.GetSitesPoints(_amurServiceHandle,
                pointCatalogs.Select(x => x.SiteId).Distinct().ToList(), dateIni, amurSiteAttrTypeLatId, amurSiteAttrTypeLonId);
            Dictionary<int, Geo.GeoPoint> pointXsites = new Dictionary<int, Geo.GeoPoint>();
            foreach (var item in pointXsites1)
            {
                pointXsites.Add(item.Key, new Geo.GeoPoint(item.Value.LatGrd, item.Value.LonGrd));
            }

            // SWITCH METHOD OUTPUT STORAGE INTERFACE

            string methOutInterface = GetOutStoreParameter(parentMethodExt.Method, "INTERFACE", true);
            switch (methOutInterface)
            {
                case "IFileFcsGrid":

                    // GET DATA FILTER
                    object dataFilter = GetDataFilter(parentMethodExt, GetVaroffs(parentCatalogs));

                    // GET DATA 4 POINTS
                    List<Geo.GeoPoint> points = pointXsites.Values.ToList();

                    // GET ENUMS
                    object[] o = SOV.Amur.Meta.Method.GetMethodPostprocessingParams(pointMethod.MethodOutputStoreParameters);
                    if (o == null)
                        throw new Exception("Отсутствуют данные или неизвестное значение параметра [parent_method_data_postprocessing] в поле параметров метода " +
                            (pointMethod.Name + " / " + pointMethod.Id));
                    Geo.EnumPointNearestType nearestType = (Geo.EnumPointNearestType)o[0];
                    Geo.EnumDistanceType distanceType = (Geo.EnumDistanceType)o[1];

                    // GET & READ GRID REPOSITORY

                    DB.IFcsGrid db = (DB.IFcsGrid)DB.Factory.GetInstance(parentMethodExt.Method.MethodOutputStoreParameters);
                    if (db == null) throw new Exception(string.Format(
                        "Для запрошенного интерфейса {0} метода <{1}> отсутствует репозиторий в классе SOV.DB.Factory. " +
                        "Нужно дополнить код фабрики.\n", methOutInterface, parentMethodExt.Method.Name));
                    double[/*leadTime*/][/*GeoPoint index*/][/*Grib2Filter index*/] parentData = db.ReadValuesAtPoints
                        (dateIni, dataFilter, leadTimes, points, nearestType, distanceType);

                    // CONVERT GRID POINT DATA TO POINT DATA CATALOGS

                    if (parentData != null)
                    {
                        double[/*leadTime*/][/*Catalog index*/] ret = ConvertDataParent2Point(parentData, parentCatalogs, pointCatalogs, pointXsites,
                            leadTimes.ToArray(),
                            precipSumResetTime);
                        return ret;
                    }
                    return null;
                default:
                    throw new Exception(string.Format("Для запрошенного интерфейса {0} метода <{1}> отсутствует обработчик считывания данных в <{2}>.\n",
                        methOutInterface, parentMethodExt.Method.Name, this));
            }
        }

        public double[/*leadTime*/][/*point*/][/*field catalog*/] GetValuesAtCoords(long hSvc,
            DateTime dateIni, List<GeoPoint> points, List<Catalog> fieldCatalogs,
            Geo.EnumPointNearestType nearestType, Geo.EnumDistanceType distanceType)
        {
            // CHECK INPUT
            CheckHandle(hSvc);
            Check(fieldCatalogs);

            // GET FCS CATALOGS 4 POINT CATALOGS
            //
            // Метод прогноза в точке является производным от исходного, 
            // родительского метода прогноза полей г/м элементов.

            //////List<Catalog> pointCatalogs = _amurClient.GetCatalogListById(_amurServiceHandle, geoPoints)
            //////    .OrderBy(x => geoPoints.IndexOf(x.Id))
            //////    .ToList();
            //////Check(pointCatalogs);

            //////// parentMethod
            //////Method pointMethod = _amurClient.GetMethod(_amurServiceHandle, pointCatalogs[0].MethodId);
            //////object[] o = GetParentFcsCatalogs(pointCatalogs);
            //////KeyValuePair<Method, List<object>> parentMethod = (KeyValuePair<Method, List<object>>)o[0];
            //////List<Catalog> parentCatalogs = (List<Catalog>)o[1];

            // GET parentMethodFcs

            MethodExt methodExt = _methodsValid.FirstOrDefault(x => x.Method.Id == fieldCatalogs[0].MethodId);
            List<double> leadTimes = methodExt.MethodForecast.LeadTimes.ToList();
            Check(leadTimes);

            // GET precipSumResetTime 
            double? precipSumResetTime = null;
            string str = null;
            if (methodExt.MethodForecast.Attr.TryGetValue("precip_reset_time".ToUpper(), out str))
                precipSumResetTime = double.Parse(str);

            //////// GET SITES POINTS 4 DATE_INI

            //////Dictionary<int, AmurServiceReference.GeoPoint> pointXsites1 = _amurClient.GetSitesPoints(_amurServiceHandle,
            //////    pointCatalogs.Select(x => x.SiteId).Distinct().ToList(), dateIni, amurSiteAttrTypeLatId, amurSiteAttrTypeLonId);
            //////Dictionary<int, Geo.GeoPoint> pointXsites = new Dictionary<int, Geo.GeoPoint>();
            //////foreach (var item in pointXsites1)
            //////{
            //////    pointXsites.Add(item.Key, new Geo.GeoPoint(item.Value.LatGrd, item.Value.LonGrd));
            //////}

            // SWITCH METHOD OUTPUT STORAGE INTERFACE

            string methOutInterface = GetOutStoreParameter(methodExt.Method, "INTERFACE", true);
            switch (methOutInterface)
            {
                case "IFileFcsGrid":

                    // GET DATA FILTER
                    object dataFilter = GetDataFilter(methodExt, GetVaroffs(fieldCatalogs));

                    //////// GET DATA 4 POINTS
                    //////List<Geo.GeoPoint> points = pointXsites.Values.ToList();

                    // GET ENUMS
                    //////o = SOV.Amur.Meta.Method.GetMethodPostprocessingParams(pointMethod.MethodOutputStoreParameters);
                    //////if (o == null)
                    //////    throw new Exception("Отсутствуют данные или неизвестное значение параметра [parent_method_data_postprocessing] в поле параметров метода " +
                    //////        (pointMethod.Name + " / " + pointMethod.Id));
                    //////Geo.EnumPointNearestType nearestType = (Geo.EnumPointNearestType)o[0];
                    //////Geo.EnumDistanceType distanceType = (Geo.EnumDistanceType)o[1];

                    // GET & READ GRID REPOSITORY

                    DB.IFcsGrid db = (DB.IFcsGrid)DB.Factory.GetInstance(methodExt.Method.MethodOutputStoreParameters);
                    ////if (db == null) throw new Exception(string.Format(
                    ////    "Для запрошенного интерфейса {0} метода <{1}> отсутствует репозиторий в классе SOV.DB.Factory. " +
                    ////    "Нужно дополнить код фабрики.\n", methOutInterface, methodExt.Method.Name));

                    return db.ReadValuesAtPoints(
                        dateIni, dataFilter, leadTimes,
                        points.Select(x => new SOV.Geo.GeoPoint(x.LatGrd, x.LonGrd)).ToList(),
                        nearestType, distanceType
                    );

                default:
                    throw new Exception(string.Format("Для запрошенного интерфейса {0} метода <{1}> отсутствует обработчик считывания данных в <{2}>.\n",
                        methOutInterface, methodExt.Method.Name, this));
            }
        }

    }
}
