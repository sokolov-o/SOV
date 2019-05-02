using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Seaware.GribCS.Grib2;
using Seaware.GribCS;
using FERHRI.Sakura.Meta;
using FERHRI.Sakura.Grib;
using FERHRI.Common;

namespace FERHRI.Sakura.Data
{
    public class FileGrib2
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="grib2"></param>
        /// <param name="gridTypes"></param>
        /// <param name="centers"></param>
        /// <returns>Object[0] - (float[]) data
        ///          Object[1] - (DateTime) дата старта прогноза
        ///          Object[2] - (int) TimeRangeUnit - тип временного интервала для заблаговременности в терминах Grib2
        ///          Object[3] - (int)ForecastTime заблаговременнность указанного типа</returns>
        public static Object[] GetData(FileStream fs, Grib2 grib2, List<DicItem> gridTypes, List<Center> centers)
        {
            double[] data = null;
            DateTime? dateRef = null;
            int? timeRangeUnit = null;
            int? forecastTime = null;
            Grib2Input gi = new Grib2Input(fs);

            try
            {
                gi.scan(false, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR scan grib2 file:\n" + ex.Message);
                return null;
            }
            for (int i = 0; i < gi.Records.Count; i++)
            {
                Grib2Record rec = (Grib2Record)gi.Records[i];
                Grid grid = GetGrid(rec, gridTypes, centers);
                if (grib2.equals(rec, grid))
                {
                    if (data != null) throw new Exception("More one needed record.");

                    dateRef = rec.ID.RefTime;
                    forecastTime = rec.PDS.ForecastTime;
                    timeRangeUnit = rec.PDS.TimeRangeUnit;

                    Grib2Data gd = new Grib2Data(fs);
                    float[] data1 = gd.getData(rec.getGdsOffset(), rec.getPdsOffset());
                    // TODO: Несовпадение кол. точек сетки и возврата библиотеки Grib2! Решить по-другому.
                    if (data1.Length < grid.PointsQ)
                        throw new Exception("(data1.Length != grid.PointsQ) : " + data1.Length + "!=" + grid.PointsQ);
                    data = new double[grid.PointsQ];
                    for (int j = 0; j < data.Length; j++)
                    {
                        data[j] = data1[j];
                    }
                }
            }
            return data == null ? null : new Object[] { data, dateRef, timeRangeUnit, forecastTime };
        }
        private static Grid GetGrid(Grib2Record rec, List<DicItem> gridTypes, List<Center> centers)
        {
            if (rec.GDS.Gdtn != 0) throw new Exception("rec.GDS.Gdtn != 0 = " + rec.GDS.Gdtn);
            if (rec.GDS.ScanMode != 0) throw new Exception("Unknown GDS.ScanMode=" + rec.GDS.ScanMode);
            if (rec.GDS.Source != 0) throw new Exception("(rec.GDS.Source != 0) = " + rec.GDS.Source);

            DicItem gridType = gridTypes.FirstOrDefault(x => x.Id == rec.GDS.Gdtn);
            Center center = centers.FirstOrDefault(x => (int)x.GribId == rec.ID.Center_id);
            if (gridType == null || center == null)
                throw new Exception("(gridType  == null || center== null)");

            return new Grid(null,
                gridType.Id,
                rec.GDS.Ny, GeoPoint.Grd2Min(rec.GDS.La1), -1 * GeoPoint.Grd2Min(rec.GDS.Dy),
                rec.GDS.Nx, GeoPoint.Grd2Min(rec.GDS.Lo1), GeoPoint.Grd2Min(rec.GDS.Dx),
                "Grid from Grib2 file");
        }
        public static double GetPredictTimeHour(int forecastTime, int timeRangeUnitId)
        {
            switch (timeRangeUnitId)
            {
                case 0: return (double)forecastTime / 60; //minute
                case 1: return (double)forecastTime;  //hour
                case 2: return (double)forecastTime * 24; //day
                default: throw new Exception("Unknown timeRangeUnitId = " + timeRangeUnitId);

            }
        }
    }
}
