using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Configuration;
using System.Diagnostics;

using FERHRI.WcfService.Field.AmurServiceReference;
//using FERHRI.Amur.Meta;

namespace FERHRI.WcfService.Field
{
    /// <summary>
    /// TODO: !!! перед service-publish убедиться в корректности строки подключения к БД в web.config !!!
    /// </summary>
    public partial class Service : IService
    {
        static string _dbAmurName;
        static AmurServiceReference.ServiceClient _amurClient;
        static long _amurServiceHandle = -100;

        static List<FERHRI.Common.User> _usersAllowed;
        static Dictionary<Method, List<object>/*MethvarXGrib, MethodForecast*/> _methodsAllowed = new Dictionary<Method, List<object>>();

        static Dictionary<long, FERHRI.Common.User> _usersSessions = new Dictionary<long, Common.User>();

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

                FERHRI.SGMO.DataManager.SetDefaultConnectionString(FERHRI.WcfService.Field.Properties.Settings.Default.SGMOConnectionString);
                _dbAmurName = FERHRI.WcfService.Field.Properties.Settings.Default.DB_AMUR_NAME;

                // GET METHODS

                //                List<int> iarr = Common.StrVia.ToArray<int>(System.Configuration.ConfigurationManager.AppSettings["AmurMethodAllowed"]).ToList();
                List<MethodForecast> methForecasts = _amurClient.GetMethodForecastsAll(_amurServiceHandle);

                foreach (Method method in _amurClient.GetMethods(_amurServiceHandle, Common.StrVia.ToArray<int>(System.Configuration.ConfigurationManager.AppSettings["AmurMethodAllowed"]).ToList()))
                {
                    if (method.MethodOutputStoreParameters == null)
                    {
                        System.IO.File.AppendAllText(_logFilePath, string.Format("Method removed due to MethodOutputStoreParameters == null. Method: \n", method));
                    }

                    // ADD METHOD
                    _methodsAllowed.Add(method, new List<object>
                    {
                            FERHRI.SGMO.DataManager.GetInstance().MethvarXGrib2Repository.Select(_dbAmurName, method.Id),
                        methForecasts.FirstOrDefault(x => x.Method.Id == method.Id)
                    });
                }
                // READ USERS ALLOWED
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
            return _methodsAllowed.Keys.ToList();
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

                KeyValuePair<long, FERHRI.Common.User> kvp = _usersSessions.FirstOrDefault(x => x.Value.Name == user.Name);
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
            KeyValuePair<long, FERHRI.Common.User> kvp = _usersSessions.FirstOrDefault(x => x.Value.Name == userName);
            if (kvp.Value != null)
                Close(kvp.Key);
        }
        public FERHRI.Field[/*LeadTime index*/][/*Georectangle index*/][/*Varoff index*/] GetFieldsInRectangles
            (long hSvc, DateTime dateIni, int methodId, List<SGMO.Varoff> varoffs, List<double> leadTimes, List<Geo.GeoRectangle> grs)
        {
            CheckHandle(hSvc);
            Check(leadTimes);

            KeyValuePair<Method, List<object>> method = GetMethod(methodId);

            // SWITCH METHOD OUTPUT STORAGE INTERFACE

            string methOutInterface = GetOutStoreParameter(method.Key, "INTERFACE", true);
            switch (methOutInterface)
            {
                case "IFileFcsGrid":
                    // GET GRID/FIELD REPOSITORY
                    DB.IFcsGrid db = (DB.IFcsGrid)DB.Factory.GetInstance(method.Key.MethodOutputStoreParameters);
                    if (db == null) throw new Exception(string.Format(
                        "Для запрошенного интерфейса {0} метода <{1}> отсутствует репозиторий в классе FERHRI.DB.Factory. " +
                        "Нужно дополнить код фабрики.\n", methOutInterface, method.Key.Name, this));
                    // GET DATA FILTER 
                    object dataFilter = GetDataFilter(method, varoffs);
                    // GET DATA IN REGIONS
                    return db.ReadFieldsInRectangles(dateIni, (object)dataFilter, leadTimes, grs);
                default:
                    throw new Exception(string.Format("Для запрошенного интерфейса {0} метода <{1}> отсутствует обработчик считывания данных в <{2}>.\n",
                        methOutInterface, method.Key.Name, this));
            }
        }

        public double[/*leadTime*/][/*point Catalog index*/] GetValuesAtPoints
            (long hSvc, DateTime dateIni, List<int> pointCatalogsId, int amurSiteAttrTypeLatId, int amurSiteAttrTypeLonId)
        {
            // CHECK INPUT
            CheckHandle(hSvc);
            Check(pointCatalogsId);

            // GET FCS CATALOGS 4 POINT CATALOGS
            //
            // В точках метод прогноза (и записи каталога) является производным от исходного, 
            // родительского (parent) метода прогноза полей г/м элементов.

            List<Catalog> pointCatalogs = _amurClient.GetCatalogListById(_amurServiceHandle, pointCatalogsId)
                .OrderBy(x => pointCatalogsId.IndexOf(x.Id))
                .ToList();
            Check(pointCatalogs);

            // parentMethod
            Method pointMethod = _amurClient.GetMethod(_amurServiceHandle, pointCatalogs[0].MethodId);
            object[] o = GetParentFcsCatalogs(pointCatalogs);
            KeyValuePair<Method, List<object>> parentMethod = (KeyValuePair<Method, List<object>>)o[0];
            List<Catalog> parentCatalogs = (List<Catalog>)o[1];

            // GET parentMethodFcs
            MethodForecast parentMethodFcs = (MethodForecast)parentMethod.Value[1];
            List<double> leadTimes = parentMethodFcs.LeadTimes.ToList();
            Check(leadTimes);

            // GET precipSumResetTime 
            double? precipSumResetTime = null;
            string str = null;
            if (parentMethodFcs.Attr.TryGetValue("precip_reset_time".ToUpper(), out str))
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

            string methOutInterface = GetOutStoreParameter(parentMethod.Key, "INTERFACE", true);
            switch (methOutInterface)
            {
                case "IFileFcsGrid":

                    // GET DATA FILTER
                    object dataFilter = GetDataFilter(parentMethod, GetVaroffs(parentCatalogs));

                    // GET DATA 4 POINTS
                    List<Geo.GeoPoint> points = pointXsites.Values.ToList();

                    // GET ENUMS
                    o = FERHRI.Amur.Meta.Method.GetMethodPostprocessingParams(pointMethod.MethodOutputStoreParameters);
                    if (o == null)
                        throw new Exception("Отсутствуют данные или неизвестное значение параметра [parent_method_data_postprocessing] в поле параметров метода " +
                            (pointMethod.Name + " / " + pointMethod.Id));
                    Geo.EnumPointNearestType nearestType = (Geo.EnumPointNearestType)o[0];
                    Geo.EnumDistanceType distanceType = (Geo.EnumDistanceType)o[1];

                    // GET & READ GRID REPOSITORY

                    DB.IFcsGrid db = (DB.IFcsGrid)DB.Factory.GetInstance(parentMethod.Key.MethodOutputStoreParameters);
                    if (db == null) throw new Exception(string.Format(
                        "Для запрошенного интерфейса {0} метода <{1}> отсутствует репозиторий в классе FERHRI.DB.Factory. " +
                        "Нужно дополнить код фабрики.\n", methOutInterface, parentMethod.Key.Name));
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
                        methOutInterface, parentMethod.Key.Name, this));
            }
        }
    }
}
