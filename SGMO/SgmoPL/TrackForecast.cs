using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.SGMO.FieldServiceReference;
using SOV.SGMO.AmurServiceReference;
using SOV.Common;

namespace SOV.SGMO
{
    public class TrackForecast
    {
        /// <summary>
        /// Get forecast for track part points.
        /// </summary>
        /// <param name="parentTrackId">Track id.</param>
        /// <param name="fcsDateIni">Track part datetime (and forecast datetime ini).</param>
        /// <param name="pointMethodIds">Track part point forecast method id.</param>
        public static List<DataTrackFcs> Get(User user, int parentTrackId, DateTime fcsDateIni, int[] pointMethodIds)
        {
            AmurServiceClient amurClient = new AmurServiceClient(user);
            FieldServiceClient fieldClient = new FieldServiceClient(user);

            // GET SITE TRACK

            Track parentTrack = GetTrack(parentTrackId, fcsDateIni);

            Track childTrack = parentTrack.ChildTracks[0];
            childTrack.Points = childTrack.Points.OrderBy(x => x.DateUTC).ToList(); // !
            Geo.GeoPoint[] childTrackPoints = childTrack.Points.Select(x => new Geo.GeoPoint(x.GeoPoint.LatGrd, x.GeoPoint.LonGrd)).ToArray();

            List<DataTrackFcs> ret = new List<DataTrackFcs>();

            for (int iPointMethodFcs = 0; iPointMethodFcs < pointMethodIds.Length; iPointMethodFcs++)
            {
                int pointMethodId = pointMethodIds[iPointMethodFcs];
                Console.WriteLine(string.Format("TrackForecast.GET: forecast data for method id={0}...", pointMethodId));

                // GET VAROFFS FOR TRACK

                List<Amur.Meta.Catalog> catalogs = amurClient.client.GetCatalogList(amurClient.h, new List<int>() { parentTrack.SiteId }, null, new List<int>() { pointMethodId }, null, null, null);
                if (catalogs.Count == 0)
                {
                    Console.WriteLine(string.Format("** По данному треку для метода {0} отсутствуют записи каталога данных мобильного пункта.", pointMethodId));
                    continue;
                }
                Varoff[] varoffs = catalogs.Select(x => new Varoff() { VariableId = x.VariableId, OffsetTypeId = x.OffsetTypeId, OffsetValue = x.OffsetValue }).ToArray();

                // GET TRACK FORECAST

                Dictionary<double/*leadTime*/, double[]/*Catalog index*/> fcsData = fieldClient.client.GetTrackForecast(fieldClient.h, fcsDateIni, childTrackPoints, pointMethodId, varoffs);
                if (fcsData == null) { Console.WriteLine("* Отсутствуют прогнозы..."); continue; }

                // CONVERT TRACK FORECAST DATA 2 List<DataTrackFcs> 

                if (fcsData.Keys.Min() != 0)
                    throw new Exception("(fcsData.Keys.Min() != 0)");

                foreach (KeyValuePair<double, double[]> kvp in fcsData.OrderBy(x => x.Key))
                {
                    for (int iCatalog = 0; iCatalog < catalogs.Count; iCatalog++)
                    {
                        ret.Add(new DataTrackFcs
                        {
                            TrackPointId = childTrack.Points[(int)kvp.Key].Id,
                            CatalogId = catalogs[iCatalog].Id,
                            LeadTime = kvp.Key,
                            Value = kvp.Value[iCatalog]
                        });
                    }
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
