using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Sakura.Grib
{
    public class Grib1
    {
        byte[] i_s = null; byte[] pds = null; byte[] gds = null; byte[] bms = null; byte[] bds = null; byte[] es = null;

        public Grib1(byte[] i_s, byte[] pds, byte[] gds, byte[] bms, byte[] bds, byte[] es)
        {
            this.i_s = i_s;
            this.pds = pds;
            this.gds = gds;
            this.bms = bms;
            this.bds = bds;
            this.es = es;
        }
        public Grib1(byte[] gribRecord)
        {
            int i, j = 0;
            i_s = new byte[8];
            for (i = 0, j = 0; i < i_s.Length; i++, j++)
            {
                i_s[i] = gribRecord[j];
            }
            int iPDS = j;
            pds = new byte[(int)Grib1.getLengthPDS(gribRecord, iPDS)];
            int length = pds.Length;
            for (i = 0; i < pds.Length; i++, j++)
            {
                pds[i] = gribRecord[j];
            }
            gds = new byte[Grib1.getLengthGDS(gribRecord, iPDS, gribRecord, j)];
            for (i = 0; i < gds.Length; i++, j++)
            {
                gds[i] = gribRecord[j];
            }
            bms = new byte[Grib1.getLengthBMS(gribRecord, iPDS, gribRecord, j)];
            for (i = 0; i < bms.Length; i++, j++)
            {
                bms[i] = gribRecord[j];
            }
            bds = new byte[Grib1.getLengthBDS(gribRecord, j)];
            for (i = 0; i < bds.Length; i++, j++)
            {
                bds[i] = gribRecord[j];
            }
            es = new byte[4];
            for (i = 0; i < es.Length; i++, j++)
            {
                es[i] = gribRecord[j];
            }
        }
        public short timeRangeIndicator
        {
            get
            {
                return (short)pds[20];
            }
        }
        public short timePeriodOfTime1
        {
            get
            {
                if (timeRangeIndicator == 10)
                {
                    return (short)(((short)pds[18]) * 256 + (int)pds[19]);
                }
                else
                    return (short)pds[18];
            }
        }
        public short center
        {
            get
            {
                return (short)pds[4];
            }
        }
        public short timePeriodOfTime2
        {
            get
            {
                return (short)pds[19];
            }
        }
        public short numbersInAverage
        {
            get
            {
                return (short)(((short)pds[21]) * 256 + (int)pds[22]);
            }
        }
        public short timeForecastTimeUnit
        {
            get
            {
                return (short)pds[17];
            }
        }
        public int Length
        {
            get
            {
                return i_s.Length
                    + pds.Length
                    + ((gds == null) ? 0 : gds.Length)
                    + ((bms == null) ? 0 : bms.Length)
                    + bds.Length
                    + es.Length;
            }
        }
        public int LevelType
        {
            get
            {
                return (short)pds[9];
            }
        }
        public int levelContens
        {
            get
            {
                return (((short)pds[10]) << 8) + pds[11];
            }
        }
        public int parameter
        {
            get
            {
                return (short)pds[8];
            }
        }
        public int dataRepresentationType
        {
            get
            {
                return (short)gds[5];
            }
        }
        public int points_q
        {
            get
            {
                return points_x * points_y;
            }
        }
        public int points_x
        {
            get
            {
                return (short)gds[6] * 256 + gds[7];
            }
        }
        public int points_y
        {
            get
            {
                return (short)gds[8] * 256 + gds[9];
            }
        }
        /// <summary>
        ///	GDS Octets 26 - 27  
        ///	Regular Lat/Lon Grid:
        ///		Dj - Latitudinal Direction Increment (same units as La1 = millydegreeses)
        ///			(if not given, all bits set = 1)
        ///	or Gaussian Grid:
        ///		N - number of latitude circles between a pole and the equator.
        /// </summary>
        /// <returns>lat_step or lat_q or float.MaxValue if unknown this.data_repr_type</returns>
        public float lat_step
        {
            get
            {
                long i = (gds[25] > 127) ? (128 - (Int32)gds[25]) * 256L - gds[26] : (Int32)gds[25] * 256L + gds[26];
                switch (dataRepresentationType)
                {
                    // LATLON
                    case 0:
                        return i / (float)1000.0;
                    // GAUSSIAN
                    case 4:
                        return i;
                    // Mercator
                    case 1:
                        return (Int32)gds[31] * 256L * 256L + gds[32] * 256L + gds[33];
                    default:
                        throw new Exception("switch (dataRepresentationType)");
                }
            }
        }
        public float lon_step // в градусах
        {
            get
            {
                if ((int)gds[23] == 256 && (int)gds[23] == 256) throw new Exception("GRBE_DiDjNotGiven");
                switch (dataRepresentationType)
                {
                    case 0: // LatLonGridPrj	:
                    case 4: // GaussianGrid	:
                        return ((gds[23] > 127) ? (128 - (Int32)gds[23]) * 256 - gds[24] : (Int32)gds[23] * 256 + gds[24]) / 1000F;
                    case 1: // MercatorPrj	: 
                        return (Int32)gds[28] * 256L * 256L + gds[29] * 256L + gds[30];
                    default: throw new Exception("GRBE_Value_GDS6Tab6_DataReprType");
                }
            }
        }
        public bool isI2East
        {
            get
            {
                switch (dataRepresentationType)
                {
                    case 0: // LatLonGridPrj	:
                    case 1: // MercatorPrj	: 
                    case 4: // GaussianGrid	:
                    case 5: //PolarPrj		:
                    case 3: //LambertPrj		:
                        return ((gds[27] & 0x80) != 0) ? false : true;
                    default: throw new Exception("GRBE_Value_GDS6Tab8_DataReprType");
                }
            }
        }
        public bool isJ2North
        {
            get
            {
                switch (dataRepresentationType)
                {
                    case 0: // LatLonGridPrj	:
                    case 4: // GaussianGrid	:
                    case 5: //PolarPrj		:
                    case 3: //LambertPrj		:
                        return ((gds[27] & 0x40) != 0) ? true : false;
                    default: throw new Exception("GRBE_Value_GDS6Tab8_DataReprType");
                }
            }
        }
        public long la1
        {
            get
            {
                switch (dataRepresentationType)
                {
                    case 0: //LatLonGridPrj
                    case 4: // GaussianGrid	:
                        return (gds[10] > 127) ?
                            (128L - (Int32)gds[10]) * 256L * 256L - gds[11] * 256L - gds[12]
                        :
                        (Int32)gds[10] * 256L * 256L + gds[11] * 256L + gds[12];
                    default:
                        throw new Exception("GRBE_Value_GDS6Tab6_DataReprType");
                }
            }
        }
        public long la2
        {
            get
            {
                switch (dataRepresentationType)
                {
                    case 0: // LatLonGridPrj	:
                    case 1: // MercatorPrj	: 
                    case 4: // GaussianGrid	:
                        return (gds[17] > 127)
                            ? (128L - (Int32)gds[17]) * 256L * 256L - gds[18] * 256L - gds[19]
                        : (Int32)gds[17] * 256L * 256L + gds[18] * 256L + gds[19];
                    default:
                        throw new Exception("GRBE_Value_GDS6Tab6_DataReprType");
                }
            }
        }
        public long lo1
        {
            get
            {
                return (gds[13] > 127) ?
                    (128L - (Int32)gds[13]) * 256L * 256L - gds[14] * 256L - gds[15]
                :
                (Int32)gds[13] * 256L * 256L + gds[14] * 256L + gds[15];
            }
        }
        public long lo2
        {
            get
            {
                switch (dataRepresentationType)
                {
                    case 0: // LatLonGridPrj	:
                    case 1: // MercatorPrj	: 
                    case 4: // GaussianGrid	:
                        return (gds[20] > 127)
                            ? (128L - (Int32)gds[20]) * 256L * 256L - gds[21] * 256L - gds[22]
                        : (Int32)gds[20] * 256L * 256L + gds[21] * 256L + gds[22];
                    default:
                        throw new Exception("GRBE_Value_GDS6Tab6_DataReprType");
                }
            }
        }
        /// <summary>
        /// Широта в градусах
        /// </summary>
        public float lat_north
        {
            get
            {
                return ((isJ2North) ? la2 : la1) / (float)1000.0;
            }
        }
        /// <summary>
        /// Широта в градусах
        /// </summary>
        public float lat_south
        {
            get
            {
                return ((isJ2North) ? la1 : la2) / (float)1000.0;
            }
        }
        /// <summary>
        /// Долгота в градусах (-180 +180)
        /// </summary>
        public float lon_east
        {
            get
            {
                return ((isI2East) ? lo2 : lo1) / (float)1000.0;
            }
        }
        /// <summary>
        /// Долгота в градусах (-180 +180)
        /// </summary>
        public float lon_west
        {
            get
            {
                return ((isI2East) ? lo1 : lo2) / (float)1000.0;
            }
        }
        public static long getLengthBDS(byte[] bds, int i)
        {
            return (long)bds[i + 0] * 256L * 256L + bds[i + 1] * 256L + bds[i + 2];
        }
        public static long getLengthBMS(byte[] pds, int i, byte[] bms, int j)
        {
            if (!isBMSIncluded(pds, i)) return 0;
            return (long)bms[j + 0] * 256L * 256L + bms[j + 1] * 256L + bms[j + 2];
        }
        public static long getLengthGDS(byte[] pds, int i, byte[] gds, int j)
        {
            if (!isGDSIncluded(pds, i)) return 0;
            return (long)gds[j + 0] * 256L * 256L + (long)gds[j + 1] * 256L + gds[j + 2];
        }
        public static long getLengthPDS(byte[] pds, int i)
        {
            return pds[i + 0] * 256L * 256L + pds[i + 1] * 256L + pds[i + 2];
        }
        public static bool isBMSIncluded(byte[] pds, int i)
        {
            return ((pds[i + 7] & 0x40) != 0) ? true : false;
        }
        public static bool isGDSIncluded(byte[] pds, int i)
        {
            return ((pds[i + 7] & 0x80) != 0) ? true : false;
        }
        //public Grid getGrid()
        //{
        //    return new Grid(
        //        null,
        //        //null, 
        //        this.dataRepresentationType,
        //        center, '\0', '\0',
        //        points_x, points_y,
        //        GeoPoint.Grd2Min(lon_step) * ((this.isI2East) ? 1 : -1),
        //        (this.dataRepresentationType == 4) ? lat_step : GeoPoint.Grd2Min(lat_step) * ((this.isJ2North) ? 1 : -1),
        //        GeoPoint.Grd2Min(la1 / 1000d),
        //        GeoPoint.Grd2Min(lo1 / 1000d),
        //        "From Grib1"
        //    );
        //}
        short dataPointBitsQ
        {
            get { return (short)bds[10]; }
        }
        double IBM2Float(byte[] ibm_, int i)
        {
            bool positive;
            int power;
            uint abspower;
            long mant;
            double value, exp;

            positive = (ibm_[i + 0] & 0x80) == 0;
            mant = ((long)ibm_[i + 1] << 16) + ((long)ibm_[i + 2] << 8) + (long)ibm_[i + 3];
            power = (int)(ibm_[i + 0] & 0x7f) - 64;
            abspower = (uint)((power > 0) ? power : -power);       /* calc exp */
            exp = 16.0;
            value = 1.0;
            while (abspower != 0)
            {
                if ((abspower & 1) != 0)
                {
                    value *= exp;
                }
                exp = exp * exp;
                abspower >>= 1;
            }

            if (power < 0) value = 1.0 / value;
            value = value * mant / 16777216.0;
            if (!positive) value = -value;
            return value;
        }
        short scaleE
        {
            get
            {
                return
                    (short)(((bds[4] & 0x80) != 0)// Is Negative value?
                    ? (128 - bds[4]) * 256 - bds[5]
                    : bds[4] * 256 + bds[5]);
            }
        }
        short scaleD
        {
            get
            {
                if (pds[26] > 127)               /*  Scale factor D */
                    return (short)((short)(128 - pds[26]) * 256L - pds[27]);
                else
                    return (short)(((short)pds[26]) * 256 + pds[27]);
            }
        }

        public float[] unpack()
        {
            // Alloc data
            long n = points_x * points_y;
            float[] pfData = new float[n];

            // Defs
            long li;
            int j, k, n_bits = dataPointBitsQ;
            int map_mask = 128, bit_mask = 128;
            float[] flt = pfData;
            double ref_ = IBM2Float(bds, 6);
            double ScaleE = Math.Pow(2.0, (double)scaleE),
                ScaleD = Math.Pow(10.0, (double)scaleD);
            int iBMS = 0, iBDS = 0;

            // Main loop
            for (li = 0L; li < n; li++)
            {
                // Bitmap present
                if (isBMSIncluded(pds, 0))
                {
                    j = (bms[iBMS + 6] & map_mask);
                    if ((map_mask >>= 1) == 0)
                    {
                        map_mask = 128;
                        iBMS++;
                    }
                    if (j == 0)
                    {
                        flt[li] = float.MaxValue;
                        continue;
                    }
                }
                // Value exist -> make float
                j = 0;
                k = n_bits;
                while (k != 0)
                {
                    if (k >= 8 && bit_mask == 128)
                    {
                        j = 256 * j + bds[iBDS + 11];
                        iBDS++;
                        k -= 8;
                    }
                    else
                    {
                        j = j + j + (((bds[iBDS + 11] & bit_mask) != 0) ? 1 : 0);
                        if ((bit_mask >>= 1) == 0)
                        {
                            iBDS++;
                            bit_mask = 128;
                        }
                        k--;
                    }
                }
                flt[li] = (float)((ref_ + ScaleE * j) / ScaleD);
            }
            return pfData;
        }
        public double[] unpackDouble()
        {
            // Alloc data
            long n = points_x * points_y;
            double[] pfData = new double[n];

            // Defs
            long li;
            int j, k, n_bits = dataPointBitsQ;
            int map_mask = 128, bit_mask = 128;
            double[] flt = pfData;
            double ref_ = IBM2Float(bds, 6);
            double ScaleE = Math.Pow(2.0, (double)scaleE),
                ScaleD = Math.Pow(10.0, (double)scaleD);
            int iBMS = 0, iBDS = 0;

            // Main loop
            for (li = 0L; li < n; li++)
            {
                // Bitmap present
                if (isBMSIncluded(pds, 0))
                {
                    j = (bms[iBMS + 6] & map_mask);
                    if ((map_mask >>= 1) == 0)
                    {
                        map_mask = 128;
                        iBMS++;
                    }
                    if (j == 0)
                    {
                        flt[li] = float.MaxValue;
                        continue;
                    }
                }
                // Value exist -> make float
                j = 0;
                k = n_bits;
                while (k != 0)
                {
                    if (k >= 8 && bit_mask == 128)
                    {
                        j = 256 * j + bds[iBDS + 11];
                        iBDS++;
                        k -= 8;
                    }
                    else
                    {
                        j = j + j + (((bds[iBDS + 11] & bit_mask) != 0) ? 1 : 0);
                        if ((bit_mask >>= 1) == 0)
                        {
                            iBDS++;
                            bit_mask = 128;
                        }
                        k--;
                    }
                }
                flt[li] = ((ref_ + ScaleE * j) / ScaleD);
            }
            return pfData;
        }
        /// <summary>
        /// Всё в кодах Grib!
        /// </summary>
        public override string ToString()
        {
            return
                 "TIME["
                    + timeForecastTimeUnit
                    + ";" + timePeriodOfTime1 + ";" + timePeriodOfTime2 + ";" + timeRangeIndicator
                    + ";" + numbersInAverage
                    + ";" + dateTime.ToString()
                    + "]"
                + "PARAM["
                    + center + ";" + parameter + ";" + LevelType + ";" + levelContens
                    + "]"
                + "\n\tGRID["
                    + " DRT " + dataRepresentationType
                    + " lat("
                        + lat_south  // в градусах
                        + " " + lat_north
                    + ")"
                    + " lon("
                        + lon_west
                        + " " + lon_east
                    + ")"
                    + " step("
                        + lat_step
                        + " " + lon_step
                    + ")"
                    + " pnt("
                        + points_x
                        + " " + points_y
                        + " " + points_q
                    + ")"
                    + " dir(" + isJ2North + " " + isI2East + ")"
                    + "]"
            ;
        }
        public short yearYY
        {
            get { return (short)pds[12]; }
        }
        public short year
        {
            get
            {
                short Year = yearYY;
                return (short)((century - 1) * 100 + yearYY);
                //return (short)(Year + ((Year > 0 && Year <= 21) ? 2000 : 1900));
            }
        }
        public short century
        {
            get { return (short)pds[24]; }
        }
        public short month
        {
            get { return (short)pds[13]; }
        }
        public short dayOfMonth
        {
            get { return (short)pds[14]; }
        }
        public short hour
        {
            get { return (short)pds[15]; }
        }
        public short minute
        {
            get { return (short)pds[16]; }
        }

        public DateTime dateTime
        {
            get
            {
                return new DateTime(year, month, dayOfMonth, hour, minute, 0, 0, 0);
            }
        }
        public byte[] ByteArray
        {
            get
            {
                byte[] ret = new byte[Length];
                int j = 0;
                j = Support.Copy(ret, j, i_s, 0, i_s.Length);
                j = Support.Copy(ret, j, pds, 0, pds.Length);
                if (gds != null) { j = Support.Copy(ret, j, gds, 0, gds.Length); }
                if (bms != null) { j = Support.Copy(ret, j, bms, 0, bms.Length); }
                j = Support.Copy(ret, j, bds, 0, bds.Length);
                j = Support.Copy(ret, j, es, 0, es.Length);
                return ret;

            }
        }
    }
}
