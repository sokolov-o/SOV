using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.SGMO;
using FERHRI.Common;
using FERHRI.DB;
using FERHRI.Geo;
using FERHRI;

namespace TestSGMO
{
    class Program
    {
        static void Main(string[] args)
        {
            // GET Grib2XVariable for Amur & GFS
            List<Grib2XVaroff> g2v = DataManager.GetInstance().Grib2XVariableRepository.Select("amur", 102);
            Console.WriteLine("{0} Grib2XVariable's readed...", g2v.Count);

            // GET GFS GRIB2 RECORD
            GfsRepository gfs = new GfsRepository(0.5);
            GeoRectangle grExtract = new GeoRectangle(35, 45, 120, 140, "noname region");
            DateTime dateRef = new DateTime(2017, 2, 1);
            int predictTime = 12;

            //object[][] gfsRec = gfs.Select(g2v.Select(x => x.Grib2Filter).ToList(), new DateTime(2017, 2, 1), 12);
            List<Field> fields = gfs.SelectFields(g2v.Select(x => x.Grib2Filter).ToList(), dateRef, predictTime, grExtract);
            for (int i = 0; i < fields.Count; i++)
            {
                Console.WriteLine("GFS field {0} {1}", i, fields[i] == null ? " is null" : " is ok");
            }
            Console.WriteLine("Fields trancated to {0} points", fields[0].Value.Length);

            // INTERPOLATE 2 POINTS
            List<GeoPoint> points = new List<GeoPoint>()
            {
                new GeoPoint(41.111,121.111),
                new GeoPoint(41.222,121.222)
            };
            double[][] interpols = gfs.SelectValuesAtPoints(g2v.Select(x => x.Grib2Filter).ToList(), dateRef, predictTime,
                points, EnumPointNearestType.Interpolate, EnumDistanceType.TheoremHaverSin);

            Console.WriteLine("\n\nPress ENTER...");
            Console.ReadLine();
        }
    }
}
