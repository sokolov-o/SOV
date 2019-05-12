#define trace

using GeographicLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.SGMO
{
    public class Track : IdNameRE
    {
        public int SiteId { get; set; }
        /// <summary>
        /// Дата начала маршрута.
        /// </summary>
        public DateTime DateS { get; set; }
        public override string ToString()
        {
            return Name + " - " + Id;
        }

        //////        /// <summary>
        //////        /// Получить последовательные прогнозопункты от исходной даты в заданные параметром lags моменты времени
        //////        /// для трека.
        //////        /// </summary>
        //////        /// <param name="dateIni">Исх. дата прогноза.</param>
        //////        /// <param name="lags">Массив смещений от dateIni (час) - моменты времени, в которые нужны lat,lon.</param>
        //////        /// <returns>Список прогнозопунктов с заблаговр.</returns>
        //////        public List<PointLag> GetPointLags(DateTime dateIni, double[] lags)
        //////        {
        //////#if trace
        //////            System.IO.StreamWriter sw = new System.IO.StreamWriter("parent.track.csv");
        //////            sw.WriteLine("date;lat;lon");
        //////            foreach (var p in this.Track1List)
        //////            {
        //////                //p.Date = p.Date.AddHours(new Random(p.Date.Day).NextDouble() * 6);
        //////                sw.WriteLine("{0:yyyy-MM-dd HH:mm:ss};{1};{2}", p.Date, p.Lat, p.Lon);
        //////            }
        //////            sw.Close();

        //////#endif
        //////            List<PointLag> ret = new List<PointLag>();
        //////            double[,] ll = Track.GetLatLon4Lags(dateIni, lags,
        //////                Track1List.Select(x => x.Date).ToArray(),
        //////                Track1List.Select(x => x.Lat).ToArray(),
        //////                Track1List.Select(x => x.Lon).ToArray());
        //////            for (int i = 0; i < lags.GetLength(0); i++)
        //////            {
        //////                ret.Add(new PointLag()
        //////                {
        //////                    Point = double.IsNaN(ll[i, 0]) ? null : new Geo.GeoPoint(ll[i, 0], ll[i, 1]),
        //////                    Lag = lags[i]
        //////                });
        //////            }

        //////#if trace
        //////            System.IO.StreamWriter sw2 = new System.IO.StreamWriter("child.track.csv");
        //////            sw2.WriteLine("date;lat;lon");
        //////            foreach (var p in ret)
        //////            {
        //////                if (p.Point != null)
        //////                {
        //////                    sw2.WriteLine("{0:yyyy-MM-dd HH:mm:ss};{1};{2}", dateIni.AddHours(p.Lag), p.Point.LatGrd, p.Point.LonGrd);
        //////                }
        //////            }
        //////            sw2.Close();

        //////#endif
        //////            return ret;
        //////        }
        //////        /// <summary>
        //////        /// Получить последовательные прогнозопункты от исходной даты в заданные параметром lags моменты времени
        //////        /// для начальной точки, направления движения (азимут) и скорости.
        //////        /// </summary>
        //////        /// <param name="dateIni">Исходная дата прогноза.</param>
        //////        /// <param name="lags">Массив смещений от dateIni (час) - моменты времени, в которые нужны lat,lon.</param>
        //////        /// <param name="lat0">Широта точки на исх. дату прогноза</param>
        //////        /// <param name="lon0">Долгота точки на исх. дату прогноза</param>
        //////        /// <param name="azimuth">Азимут перемещения объекта (куда).</param>
        //////        /// <param name="speed">Скорость объекта.</param>
        //////        /// <returns></returns>
        //////        static public double[/*индекс точки*/,/*lat,lon*/] GetLatLon4Lags(DateTime dateIni, double[] lags, double lat0, double lon0, double speed, double azimuth)
        //////        {
        //////            double[,] result = new double[lags.Length, 2];
        //////            Geodesic geod = Geodesic.WGS84;
        //////            for (int i = 0; i < lags.Length; i++)
        //////            {
        //////                double distanceInHour = lags[i] * speed * 1000.0 * 3.6;
        //////                GeodesicLine line = geod.Line(lat0, lon0, azimuth);
        //////                GeodesicData g = line.Position(distanceInHour, GeodesicMask.LATITUDE |
        //////                                                               GeodesicMask.LONGITUDE);
        //////                result[i, 0] = g.lat2;
        //////                result[i, 1] = g.lon2;
        //////            }
        //////            return result;
        //////        }

        //////        /// <summary>
        //////        /// Получить последовательные прогнозопункты от исходной даты в заданные параметром lags моменты времени
        //////        /// для трека, заданного датами и координатами точек (ломанная).
        //////        /// </summary>
        //////        /// <param name="dateIni">Исх. дата прогноза.</param>
        //////        /// <param name="lags">Массив смещений от dateIni (час) - моменты времени, в которые нужны lat,lon.</param>
        //////        /// <param name="trackDates">Даты точек трека.</param>
        //////        /// <param name="trackLats">Широты точек трека.</param>
        //////        /// <param name="trackLons">Долготы точек трека.</param>
        //////        /// <returns></returns>
        //////        static public double[/*индекс точки*/,/*lat,lon*/] GetLatLon4Lags(DateTime dateIni, double[] lags,
        //////            DateTime[/*заданные точки трека*/] trackDates, double[/*широты точек трека*/] trackLats, double[/*долготы точек трека*/] trackLons)
        //////        {
        //////            double[,] result = new double[lags.Length, 2];
        //////            for (int i = 0; i < lags.Length; i++)
        //////            {
        //////                DateTime distDate = dateIni.AddHours(lags[i]);
        //////                //distDate = 
        //////                int kl = 0;
        //////                while (!(distDate > trackDates[kl] && distDate <= trackDates[kl + 1]))
        //////                {
        //////                    kl++;
        //////                    if (kl + 1 >= trackDates.Length)
        //////                    {
        //////                        //Console.WriteLine("Point " + i + " are out of trackDates");
        //////                        result[i, 0] = double.NaN;
        //////                        result[i, 1] = double.NaN;
        //////                        break;
        //////                    }

        //////                }
        //////                if (kl + 1 >= trackDates.Length)
        //////                {
        //////                    continue;
        //////                }
        //////                Geodesic geod = Geodesic.WGS84;
        //////                GeodesicLine line = geod.InverseLine(trackLats[kl], trackLons[kl], trackLats[kl + 1], trackLons[kl + 1],
        //////                                                GeodesicMask.DISTANCE_IN |
        //////                                                GeodesicMask.LATITUDE |
        //////                                                GeodesicMask.LONGITUDE);
        //////                double dist = line.Distance();
        //////                double a = (distDate - trackDates[kl]).TotalSeconds;
        //////                double b = (trackDates[kl + 1] - trackDates[kl]).TotalSeconds;
        //////                double part = a / b;
        //////                double ds = part * dist;
        //////                GeodesicData g = line.Position(ds,
        //////                                              GeodesicMask.LATITUDE |
        //////                                              GeodesicMask.LONGITUDE);
        //////                result[i, 0] = g.lat2;
        //////                result[i, 1] = g.lon2;
        //////            }
        //////            return result;
        //////        }
    }
}
