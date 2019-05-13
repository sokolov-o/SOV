using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _TestWCFServiceField.AmurServiceReference;
using SOV.SGMO;

namespace _TestWCFServiceField
{
    public class TrackForecast
    {
        /// <summary>
        /// Get forecast for track part points.
        /// </summary>
        /// <param name="trackId">Track id.</param>
        /// <param name="dateIni">Track part datetime (and forecast datetime ini).</param>
        /// <param name="pointMethodIds">Track part point forecast method id.</param>
        public static List<DataTrackFcs> Get(int trackId, DateTime dateIni, int[] pointMethodIds)
        {
            // GET SITE TRACK

            Track track = GetTrack(trackId, dateIni);
            List<TrackPartPoint> trackPartPoints = track.TrackParts[0].TrackPartPoints;
            FieldServiceReference.GeoPoint[] trackPartPointsGeo = trackPartPoints.Select(x => new FieldServiceReference.GeoPoint() { LatGrd = x.GeoPoint.LatGrd, LonGrd = x.GeoPoint.LonGrd }).ToArray();

            List<DataTrackFcs> ret = new List<DataTrackFcs>();

            for (int iPointMethodFcs = 0; iPointMethodFcs < pointMethodIds.Length; iPointMethodFcs++)
            {
                int pointMethodId = pointMethodIds[iPointMethodFcs];
                Console.WriteLine(string.Format("TrackForecast.GET: forecast data for method id={0}...", pointMethodId));

                // GET VAROFFS FOR TRACK

                List<Catalog> catalogs = Program.clientA.GetCatalogList(Program.ha, new List<int>() { track.SiteId }, null, new List<int>() { pointMethodId }, null, null, null);
                if (catalogs.Count == 0)
                {
                    Console.WriteLine(string.Format("** По данному треку для метода {0} отсутствуют записи каталога мобильного пункта.", pointMethodId));
                    continue;
                }
                FieldServiceReference.Varoff[] varoffs = catalogs.Select(x => new FieldServiceReference.Varoff() { VariableId = x.VariableId, OffsetTypeId = x.OffsetTypeId, OffsetValue = x.OffsetValue }).ToArray();

                // GET TRACK FORECAST

                Dictionary<double/*leadTime*/, double[]/*Catalog index*/> fcsData = Program.clientF.GetTrackForecast(Program.hf, dateIni, trackPartPointsGeo, pointMethodId, varoffs);
                if (fcsData == null) { Console.WriteLine("* Отсутствуют прогнозы..."); continue; }

                // CONVERT TRACK FORECAST DATA 2 List<DataTrackFcs> 

                int iPoint = 0;
                foreach (KeyValuePair<double, double[]> kvp in fcsData)
                {
                    for (int iCatalog = 0; iCatalog < catalogs.Count; iCatalog++)
                    {
                        ret.Add(new DataTrackFcs
                        {
                            TrackPartPointId = trackPartPoints[iPoint].Id,
                            CatalogId = catalogs[iCatalog].Id,
                            LeadTime = kvp.Key,
                            Value = kvp.Value[iCatalog]
                        });
                    }
                    iPoint++;
                }
            }
            return ret;
        }

        static Track GetTrack(int trackId, DateTime dateIni)
        {
            Track track = DataManager.GetInstance().TrackRepository.Select(trackId);
            TrackPart trackPart = DataManager.GetInstance().TrackPartRepository.Select(trackId, dateIni);
            List<TrackPartPoint> trackPartPoints = DataManager.GetInstance().TrackPartPointsRepository.Select(trackPart.Id);
            track.TrackParts = new List<TrackPart> { trackPart };
            trackPart.TrackPartPoints = trackPartPoints;

            Console.WriteLine("Track [{0}], part for {1}. {2} points.", track.Name, trackPart.DateSUTC, trackPartPoints.Count);

            return track;
        }
    }
}
