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
        public static void Get(int trackId, DateTime dateIni, List<double> leadTimes)
        {
            Console.WriteLine("PROCESS TrackForecast");

            GetTrack(trackId, dateIni);

            //List<FieldServiceReference.GeoPoint> trackPoints;
            //List<Catalog> catalogs = Program.clientA.GetCatalogList(Program.ha,
            //    new List<int>() { 332 }, // Владивосток
            //    null,
            //    new List<int>() { 112 }, //Method  "Ближайший узел GFS 0.25"
            //    null, null, null
            //    );
            //List<Varoff> varoffs = new List<Varoff>();



            //DateTime dateS = DateTime.Now;
            //Console.Write("GetValuesAtPoints started at {0}...", dateS);

            //Dictionary<double/*leadTime*/, double[]/*Catalog index*/> dataP = Program.clientF.GetTrackForecast(Program.hf, dateIni, trackPoints, varoffs);

            //Console.WriteLine("ended at {0}, elapsed {1} minutes.", DateTime.Now, (int)((DateTime.Now - dateS).TotalMinutes));
            //PrintDataPoints(dateIni, catalogs, dataP);
        }
        static void GetTrack(int trackId, DateTime dateIni)
        {
            Track track = DataManager.GetInstance().TrackRepository.Select(trackId);
            TrackPart trackPart = DataManager.GetInstance().TrackPartRepository.Select(trackId, dateIni);
            List<TrackPartPoint> trackPartPoints = DataManager.GetInstance().TrackPartPointsRepository.Select(trackPart.Id);

            Console.WriteLine("Track [{0}], part for {1}. {2} points.", track.Name, trackPart.DateS, trackPartPoints.Count);
        }
    }
}
