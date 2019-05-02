using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FERHRI;
using FERHRI.Geo;
using FERHRI.WcfService.Field.AmurServiceReference;

namespace FERHRI.WcfService.Field
{
    /// <summary>
    /// Сервис доступа к полям  - к данным в узлах пространственной сетки.
    /// Поддерживаются диагностические и прогностические поля.
    /// 
    /// OSokolov@ferhri.ru
    /// 2017.08 - ...
    /// </summary>
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        long Open(string userName, string password);
        /// <summary>
        /// Закрытие рабочей сессии по её идентификатору.
        /// </summary>
        /// <param name="hSvc">Идентификатор сессии, полученный методом Open.</param>
        [OperationContract]
        void Close(long hSvc);
        /// <summary>
        /// Закрытие рабочей сессии по имени пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        [OperationContract]
        void CloseByUserName(string userName);
        /// <summary>
        /// Получить прогностические поля для указанных записей каталога и заблаговременностей в указанных регионах.
        /// Return: Field[/*LeadTime index*/][/*Georectangle index*/][/*Catalog index*/]
        /// </summary>
        /// <param name="hSvc">Идентификатор рабочей сессии.</param>
        /// <param name="dateIni">Исходная дата прогноза.</param>
        /// <param name="catalogIds">Список кодов записей каталога (id), определяющих прогностический набор данных в виде полей г/м элементов. 
        /// Все записи должны иметь один и тотже метод прогноза. 
        /// Не может быть null.</param>
        /// <param name="grs">Регионы отбора данных в узлах поля. Если null, отбираются все узлы.</param>
        /// <param name="leadTimes">Заблаговременности для выборки. Не может быть null.</param>
        /// <returns>Массив полей Field[/*LeadTime index*/][/*Georectangle index*/][/*Catalog index*/]</returns>
        [OperationContract]
        FERHRI.Field[/*LeadTime index*/][/*Georectangle index*/][/*Catalog index*/] GetFieldsInRectangles
            (long hSvc, DateTime dateIni, int methodId, List<SGMO.Varoff> varoffs, List<double> leadTimes, List<Geo.GeoRectangle> grs);
        /// <summary>
        /// Получить прогностические значения переменных для указанных записей каталога и заблаговременностей в указанных точках.
        /// </summary>
        /// <param name="dateIni"></param>
        /// <param name="catalogIds"></param>
        /// <param name="leadTimes"></param>
        /// <param name="nearestType"></param>
        /// <param name="distanceType"></param>
        /// <returns>double[/*leadTime*/][/*Catalog index*/]</returns>
        [OperationContract]
        double[/*leadTime*/][/*Catalog index*/] GetValuesAtPoints
            (long hSvc, DateTime dateIni, List<int> pointCatalogsId, /*List<double> leadTimes,*/ int amurSiteAttrTypeLatId, int amurSiteAttrTypeLonId);
        //(long hSvc, DateTime dateIni, List<int> catalogIds, List<double> leadTimes,
        //EnumPointNearestType nearestType, EnumDistanceType distanceType,
        //int fcsSiteId, /*int fcsMethodId,*/ int amurSiteAttrTypeLatId, int amurSiteAttrTypeLonId);
        /// <summary>
        /// Получить все методы (производства данных и информации), обслуживаемые сервисом.
        /// Методы соответствуют методам технологии ДВНИГМИ "Амур".
        /// </summary>
        /// <param name="hSvc">Идентификатор рабочей сессии.</param>
        /// <returns></returns>
        [OperationContract]
        List<Method> GetMethods(long hSvc);
    }
}
