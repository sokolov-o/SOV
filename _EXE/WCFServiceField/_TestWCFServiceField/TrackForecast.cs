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
    }
}
