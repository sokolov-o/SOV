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
        public static void Get(int trackId, DateTime dateIni, int pointMethodId)
        {
            Track track = GetTrack(trackId, dateIni);
            FieldServiceReference.GeoPoint[] trackPartPoints = track.TrackParts[0].TrackPartPoints.Select(x => new FieldServiceReference.GeoPoint() { LatGrd = x.GeoPoint.LatGrd, LonGrd = x.GeoPoint.LonGrd }).ToArray();

            List<Catalog> catalogs = Program.clientA.GetCatalogList(Program.ha, new List<int>() { track.SiteId }, null, new List<int>() { pointMethodId }, null, null, null);
            FieldServiceReference.Varoff[] varoffs = catalogs.Select(x => new FieldServiceReference.Varoff() { VariableId = x.VariableId, OffsetTypeId = x.OffsetTypeId, OffsetValue = x.OffsetValue }).ToArray();

            Dictionary<double/*leadTime*/, double[]/*Catalog index*/> dataP = Program.clientF.GetTrackForecast(Program.hf, dateIni, trackPartPoints, pointMethodId, varoffs);
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

        static void PrintDataPoints(DateTime dateIni, FieldServiceReference.Varoff[] varoffs, Dictionary<double/*leadTime*/, double[]/*varoff*/> data)
        {
            Console.WriteLine("\n-- POINTS Date ini {0:yyyy.MM.dd HH}", dateIni);

            if (data == null) { Console.WriteLine("... no data."); return; }

            List<Variable> variables =Program.clientA.GetVariablesByList(Program.ha, varoffs.Select(x => x.VariableId).ToList());
            foreach (KeyValuePair<double, double[]> item in data)
            {
                Console.WriteLine("Lead time {0} h", item.Key);

                if (item.Value == null)
                    Console.WriteLine("... no data.");
                else
                    for (int j = 0; j < item.Value.Length; j++)
                    {
                        Console.WriteLine("{0}  Value {1}.", catalogs[j].Id, item.Value[j], variables.FirstOrDefault(x => x.Id == catalogs[j].VariableId).NameRus);
                    }
            }
        }
    }
}
