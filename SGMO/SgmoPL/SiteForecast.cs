﻿using System;
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
    public class SiteForecast
    {
        /// <summary>
        /// Get forecast for sites.
        /// </summary>
        public static List<DataSiteFcs> Get(User user, List<int> siteIds, DateTime dateIniUTC, List<int> methodIds1)
        {
            AmurServiceClient amurClient = new AmurServiceClient(user);
            FieldServiceClient fieldClient = new FieldServiceClient(user);

            // GET CATALOGS

            List<Catalog> allCatalogs = amurClient.client.GetCatalogList(amurClient.h, siteIds, null, methodIds1, null, null, null);
            List<Amur.Meta.Method> methods = amurClient.client.GetMethods(amurClient.h, methodIds1);
            List<DataSiteFcs> ret = new List<DataSiteFcs>();

            // SCAN METHODS

            for (int iMethod = 0; iMethod < methods.Count; iMethod++)
            {
                Console.WriteLine(string.Format("TrackForecast.GET: forecast data for method id={0}...", methods[iMethod].Name));

                List<int> catalogIds = allCatalogs.FindAll(x => x.MethodId == methods[iMethod].Id).Select(x => x.Id).ToList();
                if (catalogIds.Count == 0)
                {
                    Console.WriteLine(string.Format("** По данному треку для метода {0} отсутствуют записи каталога данных мобильного пункта.", methods[iMethod].Name));
                    continue;
                }

                Dictionary<double/*leadTime*/, double[]/*Catalog index*/> fcsData = fieldClient.client.GetSitesForecast(fieldClient.h, dateIniUTC, null, catalogIds.ToArray());

                if (fcsData == null) { Console.WriteLine("* Отсутствуют прогнозы..."); continue; }

                // CONVERT FORECAST DATA 2 List<DataSiteFcs> 

                int iPoint = 0;
                foreach (KeyValuePair<double, double[]> kvp in fcsData)
                {
                    for (int iCatalog = 0; iCatalog < catalogIds.Count; iCatalog++)
                    {
                        ret.Add(new DataSiteFcs
                        {
                            DateIniUTC = dateIniUTC,
                            CatalogId = catalogIds[iCatalog],
                            LeadTime = kvp.Key,
                            Value = kvp.Value[iCatalog]
                        });
                    }
                    iPoint++;
                }
            }
            return ret;
        }
        /// <summary>
        /// Получить корневой маршрут с наследником (частью маршрута) за дату.
        /// </summary>
        /// <param name="parentTrackId"></param>
        /// <param name="dateIni"></param>
        /// <returns></returns>
        static Track GetTrack(int parentTrackId, DateTime dateIni)
        {
            Track parentTrack = DataManager.GetInstance().TrackRepository.Select(parentTrackId);
            parentTrack.Points = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(parentTrack.Id);

            Track childTrack = DataManager.GetInstance().TrackRepository.SelectChilds(parentTrackId, dateIni);
            if (childTrack == null)
                throw new Exception(string.Format("Отсутствует часть трека за дату {1} для трека id=[{1}].", dateIni, parentTrackId));
            childTrack.Points = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(childTrack.Id);
            parentTrack.ChildTracks = new List<Track> { childTrack };

            Console.WriteLine("Track [{0}], part for {1}. {2} points.", childTrack.Name, childTrack.DateSUTC, childTrack.Points.Count);

            return parentTrack;
        }
    }
}