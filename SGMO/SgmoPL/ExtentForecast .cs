using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.SGMO.FieldServiceReference;
using SOV.SGMO.AmurServiceReference;
using SOV.Common;
using SOV.Amur.Meta;

namespace SOV.SGMO
{
    public class ExtentForecast
    {
        /// <summary>
        /// Get forecast for sites.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="extentSiteIds">Коды пунктов типа геообъект (площадь), по-которым осуществляется прогноз.</param>
        /// <param name="dateIniUTC">Исх. дата прогноза.</param>
        /// <param name="methodId">Код метода прогноза полей: GFS, WRF etc.</param>
        /// <returns></returns>
        public static List<DataFcs> Get(User user, List<int> extentSiteIds, DateTime dateIniUTC, int methodId)
        {
            throw new NotImplementedException();

            ////AmurServiceClient amurClient = new AmurServiceClient(user);

            ////// GET CATALOGS, METHODS, UTCOFFSET

            ////GeoRectangle[] geoRects = new GeoRectangle[]
            ////{
            ////                    new GeoRectangle()
            ////                    {
            ////                        NorthWest = new _TestWCFServiceField.FieldServiceReference.GeoPoint() { LatGrd = 60, LonGrd = 110 },
            ////                        SouthEast = new _TestWCFServiceField.FieldServiceReference.GeoPoint() { LatGrd = 30, LonGrd = 130 }
            ////                    }
            ////    };
            ////List<Catalog> catalogs = amurClient.client.GetCatalogList(amurClient.h,
            ////    new List<int>() { 10344 },  // Земной шар
            ////    null,                       // All variables
            ////    new List<int>() { methodId },    // f.e. GFS
            ////    null,                       // All sources
            ////    null,                       // All offset types
            ////    null                        // All offset values
            ////    );
            ////FieldServiceReference.Method method = clientF.GetMethods(hf).FirstOrDefault(x => x.Id == catalogs[0].MethodId);
            ////if (method == null)
            ////    throw new Exception(string.Format($"Запрошенный метод с кодом {} не обслуживается сервисом."));

            ////Varoff[] varoffs = catalogs.Select(x => new Varoff() { VariableId = x.VariableId, OffsetTypeId = x.OffsetTypeId, OffsetValue = x.OffsetValue }).ToArray();
            ////double[] leadTimeHours = new double[] { 0, 6 };





            ////List<Amur.Meta.Method> methods = amurClient.client.GetMethods(amurClient.h, methodIds);
            ////List<Catalog> allCatalogs = amurClient.client.GetCatalogList(amurClient.h, extentSiteIds, null, methodIds, null, null, null);

            ////List<int> siteUTCOffsets = new List<int>();
            ////for (int iSite = 0; iSite < extentSiteIds.Count; iSite++)
            ////{
            ////    EntityAttrValue eav = amurClient.client.GetSiteAttrValue(amurClient.h, extentSiteIds[iSite], (int)EnumSiteAttrType.UTCOffset, DateTime.Today);
            ////    if (eav == null || string.IsNullOrEmpty(eav.Value))
            ////        throw new Exception($"В БД Амур у пункта отсутствует атрибут UTCOffset (site.id = {extentSiteIds[iSite]}).");
            ////    siteUTCOffsets.Add(int.Parse(eav.Value));
            ////}

            ////List<DataFcs> ret = new List<DataFcs>();

            ////// SCAN METHODS

            ////FieldServiceClient fieldClient = new FieldServiceClient(user);

            ////for (int iMethod = 0; iMethod < methods.Count; iMethod++)
            ////{
            ////    Console.WriteLine(string.Format($"AreaForecast.GET: forecast for method id={methods[iMethod].Name}..."));

            ////    List<int> catalogIds = allCatalogs.FindAll(x => x.MethodId == methods[iMethod].Id).Select(x => x.Id).ToList();
            ////    if (catalogIds.Count == 0)
            ////    {
            ////        Console.WriteLine(string.Format("** По данному треку для метода {0} отсутствуют записи каталога данных мобильного пункта.", methods[iMethod].Name));
            ////        continue;
            ////    }

            ////    Dictionary<double/*leadTime*/, double[]/*Catalog index*/> fcsData = fieldClient.client.GetExtentForecast(fieldClient.h, dateIniUTC, null, 
            ////        methods[iMethod], catalogIds.ToArray());

            ////    if (fcsData == null) { Console.WriteLine("* Отсутствуют прогнозы..."); continue; }

            ////    // CONVERT FORECAST DATA 2 List<DataSiteFcs> 

            ////    int iPoint = 0;
            ////    foreach (KeyValuePair<double, double[]> kvp in fcsData)
            ////    {
            ////        for (int iCatalog = 0; iCatalog < catalogIds.Count; iCatalog++)
            ////        {
            ////            int siteId = allCatalogs.FirstOrDefault(x => x.Id == catalogIds[iCatalog]).SiteId;

            ////            ret.Add(new DataFcs
            ////            {
            ////                DateIniUTC = dateIniUTC,
            ////                CatalogId = catalogIds[iCatalog],
            ////                LeadTime = kvp.Key,
            ////                Value = kvp.Value[iCatalog],
            ////                UTCOffsetHours = siteUTCOffsets[extentSiteIds.IndexOf(siteId)]
            ////            });
            ////        }
            ////        iPoint++;
            ////    }
            ////}
            ////return ret;
        }
    }
}
