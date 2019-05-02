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
using FERHRI;

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
    public interface IServiceField
    {
        [OperationContract]
        long Open(string userName, string password);
        [OperationContract]
        void Close(long hSvc);
        [OperationContract]
        void CloseByUserName(string userName);
        /// <summary>
        /// Выбрать из полей за указанную исходную дату все точки, принадлежащие заданным регионам.
        /// </summary>
        /// <param name="dateIni"></param>
        /// <param name="gr"></param>
        /// <param name="fcsTime"></param>
        /// <returns></returns>
        [OperationContract]
        FERHRI.Field[][][] GetDataByCatalogs(long hSvc, DateTime dateIni, List<int> catalogIds, List<Geo.GeoRectangle> grs, List<TimeSpan> leadTimes);
        //Dictionary<GeoPoint, Dictionary<TimeSpan/*FcsTime*/, double[/*Variavles values*/]>>[/*GeoRectangle*/]
        //    GetDataByCatalogs(long hSvc, DateTime dateIni, List<int> catalogIds, List<Geo.GeoRectangle> grs, TimeSpan? fcsTime = null);
    }
}
