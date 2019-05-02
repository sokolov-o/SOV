using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FERHRI.Common;

namespace FERHRI.Sakura.Meta
{

    /// <summary>
    ///Geo point.
    /// </summary>
    [DataContract]
    public class _DELME_GeoPoint : IEquatable<_DELME_GeoPoint>
    {
        /// <summary>
        /// Условие (тип) ближайшей точки.
        /// </summary>
        public enum NearestType
        {
            /// <summary>
            /// Не интерполировать, а выбрать значение ближайшей точки
            /// </summary>
            Nearest = 0,
            /// <summary>
            /// 
            /// </summary>
            Nearest2East = 1,
            /// <summary>
            /// 
            /// </summary>
            Nearest2West = 2,
            /// <summary>
            /// 
            /// </summary>
            Nearest2South = 3,
            /// <summary>
            /// 
            /// </summary>
            Nearest2North = 4,
            /// <summary>
            /// Интерполяция
            /// </summary>
            Interpolate = 5
        }

        /// <summary>
        /// WYSIWYG
        /// </summary>
        public const double GEO_MAXLONGITUDE_GRD = 360;
        public const double GEO_MAXLONGITUDE_MIN = 360*60;
        /// <summary>
        /// WYSIWYG
        /// </summary>
        public const double GEO_MINLONGITUDE_GRD = 0;
        /// <summary>
        /// WYSIWYG
        /// </summary>
        public const double GEO_MAXLATITUDE_GRD = 90;
        /// <summary>
        /// WYSIWYG
        /// </summary>
        public const double GEO_MINLATITUDE_GRD = -90;
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        public _DELME_GeoPoint(double latGrd, double lonGrd)
        {
            init(latGrd, lonGrd);
        }
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        ////public _DELME_GeoPoint(string lat, string lon)
        ////{
        ////    init((new _DELME_GeoLLString(lat, Enums.Geo.LatOrLon.LAT)).Grad, (new _DELME_GeoLLString(lon, Enums.Geo.LatOrLon.LON)).Grad);
        ////}

        static public string Splitter = "x";
        /// <summary>
        /// Парсинг строкового представления гео-точки с разделителем "х"
        /// </summary>
        /// <param name="geoPointToString">Строка.</param>
        /// <param name="isMinute">ture, если в строке представлены координаты в минутах. В противном случае - в градусах.</param>
        /// <returns>Гео-точку.</returns>
        static public _DELME_GeoPoint Parse(string geoPointToString, bool isMinute)
        {
            string[] s = geoPointToString.Split(Splitter[0]);
            if (isMinute)
            {
                return new _DELME_GeoPoint(int.Parse(s[0]), int.Parse(s[1]));
            }
            else
            {
                return new _DELME_GeoPoint(float.Parse(s[0]), float.Parse(s[1]));
            }
        }
        /// <summary>
        /// Строка в градусах
        /// </summary>
        /// <param name="geoPointToString"></param>
        /// <returns></returns>
        static public _DELME_GeoPoint Parse(string geoPointToString)
        {
            return Parse(geoPointToString, false);
        }
        /// <summary>
        /// Перевод градусов в минуты.
        /// </summary>
        /// <param name="grd">Градусы.</param>
        /// <returns>Минуты.</returns>
        public static double Grd2Min(double grd)
        {
            return grd * 60;
        }
        /// <summary>
        /// Перевод минут в градусы.
        /// </summary>
        /// <param name="min">Минуты.</param>
        /// <returns>Градусы.</returns>
        public static double Min2Grd(double min)
        {
            return min / 60;
        }
        /// <summary>
        /// Возвращает строку, представляющую текущий экземпляр класса.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        public override string ToString()
        {
            return (double)LatGrd + Splitter + (double)LonGrd;
        }
        /// <summary>
        /// Возвращает строку, представляющую текущий экземпляр класса в минутах.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        public string ToStringMin()
        {
            return LatMin + Splitter + LonMin;
        }
        /// <summary>
        /// Возвращает или задаёт широту точки в градусах.
        /// </summary>
        [DataMember]
        public double LatGrd { get; set; }
        /// <summary>
        /// Возвращает или задаёт долготу точки в градусах.
        /// </summary>
        [DataMember]
        public double LonGrd{ get; set; }
        /// <summary>
        /// Возвращает или задаёт широту точки в минутах.
        /// </summary>
        public double LatMin
        {
            get
            {
                return Grd2Min(LatGrd);
            }
            set
            {
                LatGrd = (float)Min2Grd(value);
            }
        }
        /// <summary>
        /// Возвращает целые градусы широты точки.
        /// </summary>
        public int LatPartGradInt
        {
            get
            {
                return (int)LatGrd;
            }
        }
        /// <summary>
        /// Возвращает целые градусы долготы точки.
        /// </summary>
        public int LonPartGradInt
        {
            get
            {
                return (int)LonGrd;
            }
        }
        /// <summary>
        /// Возвращает минуты широты точки.
        /// </summary>
        public double LatPartMinFloat
        {
            get
            {
                return (LatGrd - LatPartGradInt) * 60f;
            }
        }
        /// <summary>
        /// Широта в радианах.
        /// </summary>
        public double LatRaians
        {
            get
            {
                return Vector.grad2Radians(LatGrd);
            }
        }
        /// <summary>
        /// Долгота в радианах.
        /// </summary>
        public double LonRadians
        {
            get
            {
                return Vector.grad2Radians(LonGrd);
            }
        }
        /// <summary>
        /// Возвращает минуты долготы точки.
        /// </summary>
        public double LonPartMinFloat
        {
            get
            {
                return (LonGrd - LonPartGradInt) * 60f;
            }
        }
        /// <summary>
        /// Возвращает целые минуты широты точки.
        /// </summary>
        public int LatPartMinInt
        {
            get
            {
                return (int)LatPartMinFloat;
            }
        }
        /// <summary>
        /// Возвращает целые минуты долготы точки.
        /// </summary>
        public int LonPartMinInt
        {
            get
            {
                return (int)LonPartMinFloat;
            }
        }
        /// <summary>
        /// Возвращает или задаёт долготу точки в минутах.
        /// </summary>
        public double LonMin
        {
            get
            {
                return Grd2Min(LonGrd);
            }
            set
            {
                LonGrd = Min2Grd(value);
            }
        }
        static public bool Test(double lat, double lon)
        {
            if (lon <= GEO_MAXLONGITUDE_GRD && lon >= GEO_MINLONGITUDE_GRD && lat <= GEO_MAXLATITUDE_GRD && lat >= GEO_MINLATITUDE_GRD)
                return true;
            return false;
        }
        private void init(double lat, double lon)
        {
            if (!Test(lat, lon))
                throw new Exception("Point is wrong: " + lat + ";" + lon);
            this.LatGrd = lat;
            this.LonGrd = lon;

        }
        /// <summary>
        /// Преобразование частей гео-координаты в доли градусов.
        /// </summary>
        /// <param name="grd">Часть - градусы.</param>
        /// <param name="min">Часть - минуты.</param>
        /// <param name="sec">Часть - секунды.</param>
        /// <returns>Градусы.</returns>
        public static double toDouble(int grd, int? min, int? sec)
        {
            return (double)grd + ((min == null) ? 0 : ((double)min) / 60) + ((sec == null) ? 0 : ((double)sec) / 3600);
        }
        /// <summary>
        /// Преобразование строкового представления гео-координаты в доли градусов.
        /// </summary>
        /// <param name="format">Формат строки.</param>
        /// <param name="value">Строка.</param>
        /// <returns>Градусы.</returns>
        public static double toDouble(Enums.Geo.Formats format, string value)
        {
            double d;
            string[] s = value.Trim().Split(new char[] { ' ' });

            switch (format)
            {
                case Enums.Geo.Formats.GRDDEC:
                    return double.Parse(value);
                case Enums.Geo.Formats.GRD_MINDEC:
                    if (s.Length != 2)
                    {
                        throw new Exception("WRONG value " + value.Trim() + " for src format " + format.ToString());
                    }
                    int i = s[1].IndexOf('.');
                    if (i > -1)
                    {
                        i = int.Parse(s[1].Substring(0, i));
                        if (i > 59 || i < 0)
                        {
                            throw new Exception("WRONG value " + value.Trim() + " for src format " + format.ToString());
                        }
                    }
                    d = double.Parse(s[1]) / 60.0;
                    return double.Parse(s[0] + "." + (int)(d * 10000000));
                case Enums.Geo.Formats.GRD_MIN_SEC:
                    return double.Parse(s[0]) + float.Parse(s[1]) / 60 + float.Parse(s[2]) / 3600;
                default:
                    throw new Exception("UNKNOWN format: " + format.ToString());
            }
        }
        /// <summary>
        /// Преобразование строкового представления гео-координаты из одного формата (представления) в другой.
        /// </summary>
        /// <param name="srcFormat">Исхолный формат строки.</param>
        /// <param name="value">Исходная строка гео-координаты.</param>
        /// <param name="dstFormat">Результирующий формат строки.</param>
        /// <returns>Строка в результирующем формате.</returns>
        public static string toString(Enums.Geo.Formats srcFormat, string value, Enums.Geo.Formats dstFormat)
        {
            if (srcFormat == dstFormat)
                return value;
            else
                return toString(toDouble(srcFormat, value), dstFormat);
        }
        /// <summary>
        /// Преобразование гео-координаты в градусах в строковое представление.
        /// </summary>
        /// <param name="value">Исходная строка гео-координаты.</param>
        /// <param name="dstFormat">Результирующий формат строки.</param>
        /// <returns>Строка в результирующем формате.</returns>
        public static string toString(double value, Enums.Geo.Formats dstFormat)
        {
            switch (dstFormat)
            {
                case Enums.Geo.Formats.GRD_MINDEC:
                    return ((int)value).ToString() + " " + ((value - (int)value) * 60.0).ToString("F04");
                case Enums.Geo.Formats.GRDDEC:
                    return value.ToString();
                default:
                    throw new Exception("UNKNOWN dst format: " + dstFormat.ToString());
            }
        }
        /// <summary>
        /// Получить часть координаты.
        /// </summary>
        /// <param name="grad">Координата в градусах.</param>
        /// <param name="gms"></param>
        /// <returns></returns>
        public static double getField(double grad, Enums.Geo.GrdMinSec gms)
        {
            switch (gms)
            {
                case Enums.Geo.GrdMinSec.GRD:
                    return (double)grad;
                case Enums.Geo.GrdMinSec.MIN:
                    return ((double)grad - (int)grad) * 60.0;
                case Enums.Geo.GrdMinSec.SEC:
                    double min = getField(grad, Enums.Geo.GrdMinSec.MIN);
                    return (min - (int)min) * 60.0;
            }
            throw new Exception("UNKNOWN EnumGeo.GrdMinSec: " + gms);
        }

        public double getDistMin(_DELME_GeoPoint gp)
        {

            double dx = Math.Abs(this.LonMin - gp.LonMin);
            if (dx > 180) dx = 360 - dx;
            double dy = Math.Abs(this.LatMin - gp.LatMin);
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        public bool Equals(_DELME_GeoPoint other)
        {
            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;
            return LatGrd.Equals(other.LatGrd) && LonGrd.Equals(other.LonGrd);
        }
        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null. 
            int hashGeoPointsLatGrd = LatGrd.GetHashCode();

            //Get hash code for the Code field. 
            int hashGeoPointsLonGrd = LonGrd.GetHashCode();

            //Calculate the hash code for the product. 
            return hashGeoPointsLatGrd ^ hashGeoPointsLonGrd;
        }
    }
}
