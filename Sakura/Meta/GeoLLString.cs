using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FERHRI.Common;

namespace FERHRI.Sakura.Meta
{
    /// <summary>
    /// Строковое представление координаты широты/долготы в форме градусы, минуты, секунды
    /// с возможностью дробной части на любом уровне.
    /// </summary>
    public class GeoLLString
    {
        string grd = null, min = null, sec = null;
        Enums.Geo.LatOrLon enumLatOrLon;
        /// <summary>
        /// Задаёт (через parce) или возвращает (через toString) строковое представление гео-координаты.
        /// </summary>
        public string GMSString
        {
            get
            {
                return toString(grd, min, sec);
            }
            set
            {
                string[] gms = parce(value, enumLatOrLon);
                if (gms != null)
                {
                    grd = gms[0];
                    min = gms[1];
                    sec = gms[2];
                }
            }
        }
        /// <summary>
        /// Получить составную гео-координату в строковом виде.
        /// </summary>
        /// <param name="grd">Часть - градусы.</param>
        /// <param name="min">Часть - минуты.</param>
        /// <param name="sec">Часть - секунды.</param>
        /// <returns>Координата в строковом виде.</returns>
        public static string toString(string grd, string min, string sec)
        {
            return
                  ((grd == null || grd == "") ? "" : grd.Trim())
                + ((min == null || min == "") ? "" : " " + min.Trim())
                + ((sec == null || sec == "") ? "" : " " + sec.Trim());
        }
        /// <summary>
        /// Получить части координаты из строки общего формата GG.dddddd MM.dddddd SS.dddddd.
        /// Части:
        /// GG.dddddd
        /// GG MM.dddddd
        /// GG MM SS.dddddd
        /// </summary>
        /// <remarks>sov@201008</remarks>
        /// <param name="sCoord">Строковое представление координаты.</param>
        /// <param name="enumGeoLatOrLon">Координата широта или долгота?</param>
        /// <returns>Массив частей координаты.</returns>
        public static string[] parce(string sCoord, Enums.Geo.LatOrLon enumGeoLatOrLon)
        {
            if (sCoord == null || sCoord == "")
                return null;

            String err = "ОШИБКА: неверный формат строки координат №", sGrd, sMin = null, sSec = null;

            String[] gms = sCoord.TrimEnd().Split(' ');
            if (gms.Length > 3) throw new Exception(err + "1: sCoord.Length > 3: " + sCoord);
            sGrd = gms[0];
            if (gms.Length > 1)
            {
                if (sGrd.IndexOf('.') >= 0 && gms[1] != null)
                    throw new Exception(err + "3: " + sCoord);
                sMin = gms[1];
                if (gms.Length > 2)
                {
                    if (sMin.IndexOf('.') >= 0 && gms[2] != null)
                        throw new Exception(err + "4: " + sCoord);
                    sSec = gms[2];
                }
                if (gms.Length > 3)
                    throw new Exception(err + "1: sCoord.Length > 3: " + sCoord);
            }

            if (sGrd != null) testGrd(sGrd, enumGeoLatOrLon);
            if (sMin != null) testMinSec(sMin);
            if (sSec != null) testMinSec(sSec);

            return new string[] { sGrd, sMin, sSec };
        }
        /// <summary>
        /// Тестирование на корректность формата части координаты - градусов.
        /// </summary>
        /// <param name="grd">Часть координаты.</param>
        /// <param name="enumGeoLatOrLon">Координата широта или долгота?</param>
        public static void testGrd(string grd, Enums.Geo.LatOrLon enumGeoLatOrLon)
        {
            double d = StrVia.ParseDouble(grd);
            if ((d < -90 || d > 90) && enumGeoLatOrLon == Enums.Geo.LatOrLon.LAT)
                throw new Exception("(d < -90 || d > 90) && enumGeoLatOrLon == EnumGeo.LatOrLon.LAT: " + grd);
            if ((d < -180 || d > 360) && enumGeoLatOrLon == Enums.Geo.LatOrLon.LON)
                throw new Exception("(d < -180 || d > 360) && enumGeoLatOrLon == EnumGeo.LatOrLon.LON: " + grd);
        }
        /// <summary>
        /// Тестирование на корректность формата части координаты - минут или секунд.
        /// </summary>
        /// <param name="minOrSec">Часть координаты.</param>
        public static void testMinSec(string minOrSec)
        {
            double d = Double.Parse(minOrSec);
            if (d < 0 || d > 60)
                throw new Exception("(d < 0 || d > 60): " + minOrSec);
        }
        /// <summary>
        /// Преобразовать строковые представления частей координаты в градусы.
        /// </summary>
        public static double toGrad(string grd, string min, string sec)
        {
            return Double.Parse(grd)
                + ((min == null) ? 0 : Double.Parse(min)) / 60.0
                + ((sec == null) ? 0 : Double.Parse(sec)) / 3600.0;
        }
        /// <summary>
        /// Парсинг строки широты/долготы.
        /// </summary>
        /// <param name="sCoord">Координата.</param>
        /// <param name="latOrLon">Широта или долгота?</param>
        /// <returns></returns>
        public static double? toGrad(string sCoord, Enums.Geo.LatOrLon latOrLon)
        {
            if (string.IsNullOrEmpty(sCoord))
                return null;
            string[] s = parce(sCoord, latOrLon);
            return toGrad(s[0], s[1], s[2]);
        }
        /// <summary>
        /// Преобразовать строковые представления частей координаты экземпляра класса в градусы.
        /// </summary>
        public double Grad
        {
            get
            {
                return toGrad(grd, min, sec);
            }
        }
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        /// <param name="coord">Строковое представление координаты.</param>
        /// <param name="enumLatOrLon">Координата широта или долгота?</param>
        public GeoLLString(string coord, Enums.Geo.LatOrLon enumLatOrLon)
        {
            this.enumLatOrLon = enumLatOrLon;
            GMSString = coord;
        }
    }
}
