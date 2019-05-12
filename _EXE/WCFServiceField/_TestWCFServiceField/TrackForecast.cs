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
        /// <param name="pointMethodId">Track part point forecast method id.</param>
        public static List<DataTrackFcs> Get(int trackId, DateTime dateIni, int pointMethodId)
        {
            // GET TRACK
            Track track = GetTrack(trackId, dateIni);
            List<TrackPartPoint> trackPartPoints = track.TrackParts[0].TrackPartPoints;
            FieldServiceReference.GeoPoint[] trackPartPointsGeo = trackPartPoints.Select(x => new FieldServiceReference.GeoPoint() { LatGrd = x.GeoPoint.LatGrd, LonGrd = x.GeoPoint.LonGrd }).ToArray();

            // GET VAROFFS
            List<Catalog> catalogs = Program.clientA.GetCatalogList(Program.ha, new List<int>() { track.SiteId }, null, new List<int>() { pointMethodId }, null, null, null);
            FieldServiceReference.Varoff[] varoffs = catalogs.Select(x => new FieldServiceReference.Varoff() { VariableId = x.VariableId, OffsetTypeId = x.OffsetTypeId, OffsetValue = x.OffsetValue }).ToArray();

            // GET FORECAST
            Dictionary<double/*leadTime*/, double[]/*Catalog index*/> fcsData = Program.clientF.GetTrackForecast(Program.hf, dateIni, trackPartPointsGeo, pointMethodId, varoffs);

            // CONVERT FORECAST
            List<DataTrackFcs> dataTrackF = new List<DataTrackFcs>();
            int iPoint = 0;
            foreach (KeyValuePair<double, double[]> kvp in fcsData)
            {
                for (int iCatalog = 0; iCatalog < catalogs.Count; iCatalog++)
                {
                    dataTrackF.Add(new DataTrackFcs
                    {
                        TrackPartPointId = trackPartPoints[iPoint].Id,
                        CatalogId = catalogs[iCatalog].Id,
                        LeadTime = kvp.Key,
                        Value = kvp.Value[iCatalog]
                    });
                }
                iPoint++;
            }
            return dataTrackF;
        }

        static Track GetTrack(int trackId, DateTime dateIni)
        {
            Track track = DataManager.GetInstance().TrackRepository.Select(trackId);
            TrackPart trackPart = DataManager.GetInstance().TrackPartRepository.Select(trackId, dateIni);
            List<TrackPartPoint> trackPartPoints = DataManager.GetInstance().TrackPartPointsRepository.Select(trackPart.Id);
            track.TrackParts = new List<TrackPart> { trackPart };
            trackPart.TrackPartPoints = trackPartPoints;

            Console.WriteLine("Track [{0}], part for {1}. {2} points.", track.Name, trackPart.DateS, trackPartPoints.Count);

            return track;
        }
    }
}
