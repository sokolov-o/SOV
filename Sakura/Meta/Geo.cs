using FERHRI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Sakura.Meta
{
    public class Geo
    {
        /// <summary>
        /// Радиус Земли (м).
        /// </summary>
        public const double R = 6372795.0;
        /// <summary>
        /// Тип расчёта расстояний на сфере.
        /// </summary>
        public enum DistanceType
        {
            /// <summary>
            /// Плоскость.
            /// </summary>
            Plane = 1,
            /// <summary>
            /// Для больших расстояний на сфере?
            /// </summary>
            TheoremCos = 2,
            /// <summary>
            /// По формуле гаверсинусов - для НЕбольших расстояний на сфере?
            /// </summary>
            TheoremHaverSin = 3
        }
        /// <summary>
        /// Расчёт расстояния (m) на сфере с радиусом Земли по заданным (в радианах) координатам двух точек.
        /// Для DistanceType.Plane считаем (тупо) корень из суммы квадратов того, что подали в метод.
        /// </summary>
        /// <param name="lon1rad">радианы</param>
        /// <param name="lon2rad">радианы</param>
        /// <param name="lat1rad">радианы</param>
        /// <param name="lat2rad">радианы</param>
        /// <param name="distanceType">Формула расчёта расстояния.</param>
        /// <returns>Расстояние (m) или double.NaN, если неизвестен DistanceType.</returns>
        static public double SphereDistance(double lon1rad, double lon2rad, double lat1rad, double lat2rad, DistanceType distanceType)
        {
            double dx = lon2rad - lon1rad;
            double dy = lat2rad - lat1rad;
            //if (dx == 0 && dy == 0)
            //    return 0;

            switch (distanceType)
            {
                case DistanceType.Plane:
                    return Vector.uv2Module(dx, dy);
                case DistanceType.TheoremCos:
                    return Geo.R * Math.Acos(Math.Sin(lat1rad) * Math.Sin(lat2rad) + Math.Cos(lat1rad) * Math.Cos(lat2rad) * Math.Cos(dx));
                case DistanceType.TheoremHaverSin:
                    return Geo.R * 2 * Math.Asin(
                        Math.Sqrt(Math.Pow((Math.Sin(dy)) / 2.0, 2) + Math.Cos(lat1rad) * Math.Cos(lat2rad) * Math.Pow(Math.Sin(dx / 2.0), 2))
                    );
                default:
                    return double.NaN;

            }
        }
        /// <summary>
        /// Средневзвешенная линейная интерполяция в заданную координату между точками. Веса - расстояния от заданной до соответствующей точки.
        /// </summary>
        /// <param name="lonrad">Долгота искомой точки (рад).</param>
        /// <param name="latrad">Широта искомой точки (рад).</param>
        /// <param name="values">Значения в точках (рад).</param>
        /// <param name="lonsrad">Долготы точек (рад).</param>
        /// <param name="latsrad">Широты точек (рад).</param>
        /// <param name="distanceType">Тип расчёта расстояния, необходимого для весов.</param>
        /// <returns>Интерполированное значение.</returns>
        static public double Interpolate(double lonrad, double latrad, double[] values, double[] lonsrad, double[] latsrad, DistanceType distanceType)
        {
            double ret = 0, weightSum = 0;

            for (int i = 0; i < values.Length; i++)
            {
                if (lonrad == lonsrad[i] && latrad == latsrad[i])
                    return values[i];

                double w = SphereDistance(lonrad, lonsrad[i], latrad, latsrad[i], distanceType);
                //w = w * w;
                ret += (values[i] / w);
                weightSum += (1 / w);
            }
            return ret / weightSum;
        }
    }
}
