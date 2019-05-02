﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using System.IO;
using System.Net;

using FERHRI.Geo;
using System.Diagnostics;

namespace FERHRI.DB
{
    public class VanRepository : IFcsGrid
    {
        public string FileFormat { get; set; }
        public string FileDirectory { get; set; }
        /// <summary>
        /// Маска файла, например, если в имени файла указано время начала прогноза.
        /// </summary>
        public string FileNameMask { get; set; }
        public bool IsFileZipped { get; set; }

        public VanRepository(string formatName, string fileDirectory, string fileNameMask, string isFileZipped)
        {
            FileFormat = formatName;
            FileDirectory = fileDirectory;
            FileNameMask = fileNameMask;
            IsFileZipped = int.Parse(isFileZipped) == 0 ? false : true;
        }
        /// <summary>
        /// return Dictionary<GeoPoint, Dictionary<TimeSpan/*FcsTime*/, double[/*WaveParams*/]>>
        /// </summary>
        /// <param name="dateIni">Исх. дата прогноза.</param>
        /// <param name="gr">Геогр. квадрант для прогноза - отбор узлов сетки.</param>
        /// <param name="fcsTime">Заблаговременность прогноза или все заблаговременности.</param>
        /// <returns>
        /// 
        /// Параметры волнения для отдельных заблаговременностей и точек/узлов. 
        /// Dictionary<GeoPoint, Dictionary<TimeSpan/*FcsTime*/, double[/*WaveParams*/]>>
        /// 
        /// либо null, если для указанной dateIni отсутствует файл с прогностическими данными.
        /// 
        /// </returns>
        public Dictionary<GeoPoint, Dictionary<double/*FcsTime*/, double[/*WaveParams*/]>>[] ReadPoints(
           DateTime dateIni, object dataFilter, List<double> leadTimeHours, List<GeoRectangle> grs)
        {
            if (dataFilter != null)
                throw new NotImplementedException("В данной версии ПО фильтр данных должен быть null.");

            FileStream fs = null;
            try
            {
                fs = OpenTempFile(dateIni);
                if (fs == null)
                    return null;
                return Parse(fs, grs, leadTimeHours);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    File.Delete(fs.Name);
                }
            }
        }

        class DataRow2011
        {
            internal double leadTime;
            internal Geo.GeoPoint geoPoint;
            internal double[] values;

            public static readonly string[] WaveParamNames2011 = new string[]
            {
                "Высота смешанного волнения (Hs)",
                "Период смешанного волнения (Ts)",
                "Длина смешанного волнения (Ls)",
                "Направление смешанного волнения (Dirs)",

                "Высота ветрового волнения (Hw)",
                "Период ветрового волнения (Tw)",
                "Длина ветрового волнения (Lw)",
                "Направление ветрового волнения (Dirw)",

                "Высота зыби 1-й системы (Hs1)",
                "Период зыби 1-й системы (Ts1)",
                "Длина зыби 1-й системы (Ls1)",
                "Направление зыби 1-й системы (Dirs1)",

                "Высота зыби 2-й системы (Hs2)",
                "Период зыби 2-й системы (Ts2)",
                "Длина зыби 2-й системы (Ls2)",
                "Направление зыби 2-й системы (Dirs2)",

                "Скорость ветра (WindSpeed)",
                "Напрсаление ветра (WindDir)"
            };

            internal static DataRow2011 ParseLine(string line)
            {
                if (string.IsNullOrEmpty(line))
                    return null;

                line = line.Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                string[] vals = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                DataRow2011 ret = new DataRow2011();

                ret.leadTime = double.Parse(vals[4]);
                ret.geoPoint = new GeoPoint(Convert.ToDouble(vals[6]), Convert.ToDouble(vals[5]));
                ret.values = new double[WaveParamNames2011.Length]; ;

                ret.values[0] = Convert.ToDouble(vals[7]); //hmix
                ret.values[1] = Convert.ToDouble(vals[8]); //t
                ret.values[2] = Convert.ToDouble(vals[9]); //l
                ret.values[3] = Convert.ToDouble(vals[10]); //dir

                ret.values[4] = Convert.ToDouble(vals[11]);//hwind
                ret.values[5] = Convert.ToDouble(vals[12]);
                ret.values[6] = Convert.ToDouble(vals[13]);
                ret.values[7] = Convert.ToDouble(vals[14]);

                ret.values[8] = Convert.ToDouble(vals[15]); //hsw1
                ret.values[9] = Convert.ToDouble(vals[16]);
                ret.values[10] = Convert.ToDouble(vals[17]);
                ret.values[11] = Convert.ToDouble(vals[18]);

                ret.values[12] = Convert.ToDouble(vals[19]); //hsw2
                ret.values[13] = Convert.ToDouble(vals[20]);
                ret.values[14] = Convert.ToDouble(vals[21]);
                ret.values[15] = Convert.ToDouble(vals[22]);

                ret.values[16] = Convert.ToDouble(vals[23]); // wind
                ret.values[17] = Convert.ToDouble(vals[24]);

                return ret;
            }
        }
        Dictionary<GeoPoint, Dictionary<double/*FcsTime*/, double[/*WaveParamNames*/]>>[/*grs*/]
            Parse(FileStream fs, List<GeoRectangle> grs = null, List<double> leadTimeHours = null)
        {
            if (FileFormat != "FILE_VAN2011")
                throw new Exception("Неизвестный формат файла прогноза волнения " + FileFormat);

            Dictionary<GeoPoint, Dictionary<double, double[]>>[] ret = new Dictionary<GeoPoint, Dictionary<double, double[]>>[grs == null ? 1 : grs.Count];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = new Dictionary<GeoPoint, Dictionary<double, double[]>>();
            }

            StreamReader sr = new StreamReader(fs, Encoding.Default, false, 10000000);

            int iRow = 0;
            while (!sr.EndOfStream)
            {
                // READ LINE
                DataRow2011 data = DataRow2011.ParseLine(sr.ReadLine());
                if (data == null)
                    continue;

                // REQUIRED FCS TIME?
                if (leadTimeHours != null && !leadTimeHours.Exists(x => x == data.leadTime))
                    continue;

                // REQUIRED FCS POINT/NODE?
                List<int> iGrs = new List<int>();
                if (grs != null)
                {
                    for (int i = 0; i < grs.Count; i++)
                    {
                        if (grs[i].IsPointBelong(data.geoPoint))
                        {
                            iGrs.Add(i);
                        }
                    }
                }
                else
                    iGrs.Add(0);

                if (iGrs.Count > 0)
                {
                    // ADD DATA 2 RET
                    Dictionary<double, double[]> dicFcsTime = null;
                    if (ret.FirstOrDefault(x => x.TryGetValue(data.geoPoint, out dicFcsTime)) == null)
                    {
                        dicFcsTime = new Dictionary<double, double[]>();
                    }
                    dicFcsTime.Add(data.leadTime, data.values);

                    Dictionary<double, double[]> dicFcsTime1 = null;
                    foreach (var iGR in iGrs)
                    {
                        if (!ret[iGR].TryGetValue(data.geoPoint, out dicFcsTime1))
                            ret[iGR].Add(data.geoPoint, dicFcsTime);
                    }
                }
                iRow++;
            }

            //{
            //    // READ LINE
            //    string line = sr.ReadLine().Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            //    //string line = ss[iRow].Trim();
            //    if (string.IsNullOrEmpty(line))
            //        continue;
            //    string[] vals = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            //    // REQUIRED FCS TIME?
            //    double fcsTime = double.Parse(vals[4]);
            //    if (leadTimeHours != null && !leadTimeHours.Exists(x => x == fcsTime))
            //        continue;

            //    // REQUIRED FCS POINT/NODE?
            //    GeoPoint gp = new GeoPoint(Convert.ToDouble(vals[6]), Convert.ToDouble(vals[5]));
            //    List<int> iGr = new List<int>();
            //    if (grs != null)
            //    {
            //        for (int i = 0; i < grs.Count; i++)
            //        {
            //            if (grs[i].IsPointBelong(gp))
            //            {
            //                iGr.Add(i);
            //            }
            //        }
            //    }
            //    else
            //        iGr.Add(0);

            //    if (iGr.Count > 0)
            //    {
            //        // PARSE DATA
            //        double[] fcsValues = new double[DataRow2011.WaveParamNames2011.Length]; ;

            //        fcsValues[0] = Convert.ToDouble(vals[7]); //hmix
            //        fcsValues[1] = Convert.ToDouble(vals[8]); //t
            //        fcsValues[2] = Convert.ToDouble(vals[9]); //l
            //        fcsValues[3] = Convert.ToDouble(vals[10]); //dir

            //        fcsValues[4] = Convert.ToDouble(vals[11]);//hwind
            //        fcsValues[5] = Convert.ToDouble(vals[12]);
            //        fcsValues[6] = Convert.ToDouble(vals[13]);
            //        fcsValues[7] = Convert.ToDouble(vals[14]);

            //        fcsValues[8] = Convert.ToDouble(vals[15]); //hsw1
            //        fcsValues[9] = Convert.ToDouble(vals[16]);
            //        fcsValues[10] = Convert.ToDouble(vals[17]);
            //        fcsValues[11] = Convert.ToDouble(vals[18]);

            //        fcsValues[12] = Convert.ToDouble(vals[19]); //hsw2
            //        fcsValues[13] = Convert.ToDouble(vals[20]);
            //        fcsValues[14] = Convert.ToDouble(vals[21]);
            //        fcsValues[15] = Convert.ToDouble(vals[22]);

            //        fcsValues[16] = Convert.ToDouble(vals[23]); // wind
            //        fcsValues[17] = Convert.ToDouble(vals[24]);

            //        // ADD DATA 2 RET
            //        Dictionary<double, double[]> dicFcsTime = null;
            //        if (ret.FirstOrDefault(x => x.TryGetValue(gp, out dicFcsTime)) == null)
            //        {
            //            dicFcsTime = new Dictionary<double, double[]>();
            //        }
            //        dicFcsTime.Add(fcsTime, fcsValues);

            //        Dictionary<double, double[]> dicFcsTime1 = null;
            //        foreach (var i in iGr)
            //        {
            //            if (!ret[i].TryGetValue(gp, out dicFcsTime1))
            //                ret[i].Add(gp, dicFcsTime);
            //        }
            //    }
            //    iRow++;
            //}
            return ret;
        }

        Field[/*leadTime*/][/*Georectangle index*/][/*Variable index*/]
            ParseAsField(FileStream fs, List<int> varIndeces, List<double> leadTimeHours = null, List<GeoRectangle> grs = null)
        {
            if (FileFormat != "FILE_VAN2011")
                throw new Exception("Неизвестный формат файла прогноза волнения " + FileFormat);

            // GET FILE ATTR: GRID & LEADTIMES
            object[] o = GetFileAttr(fs);
            fs.Seek(0, SeekOrigin.Begin);
            Grid gridSrc = (Grid)o[0];
            if (leadTimeHours == null)
                leadTimeHours = (List<double>)o[1];
            List<Grid> gridDsts = new List<Grid>();
            if (grs == null)
                gridDsts.Add(gridSrc);
            else
            {
                foreach (var gr in grs)
                {
                    gridDsts.Add(gridSrc.Truncate2(gr));
                }
            }

            Field[/*leadTime*/][/*Georectangle index*/][/*Variable index*/] ret = new Field[leadTimeHours.Count][][];//, grs == null ? 1 : grs.Count, varIndeces.Count];

            StreamReader sr = new StreamReader(fs, Encoding.Default, false, 10000000);

            int iRow = 0;
            while (!sr.EndOfStream)
            {
                // READ LINE
                DataRow2011 data = DataRow2011.ParseLine(sr.ReadLine());
                if (data == null)
                    continue;

                // REQUIRED FCS TIME?
                int iLT = leadTimeHours.IndexOf(data.leadTime);
                if (iLT >= 0)
                {
                    if (ret[iLT] == null) ret[iLT] = new Field[gridDsts.Count][];

                    // REQUIRED FCS POINT/NODE?
                    for (int iGR = 0; iGR < gridDsts.Count; iGR++)
                    {
                        if (ret[iLT][iGR] == null) ret[iLT][iGR] = new Field[varIndeces.Count];

                        int iPoint = gridDsts[iGR].IndexOf(data.geoPoint);
                        if (iPoint >= 0)
                        {
                            foreach (int iVar in varIndeces)
                            {
                                if (ret[iLT][iGR][iVar] == null)
                                    ret[iLT][iGR][iVar] = new Field(gridDsts[iGR], EnumFieldFormat.GRID, leadTimeHours[iLT],
                                    Support.Allocate(gridDsts[iGR].PointsQ, double.NaN));

                                ret[iLT][iGR][iVar].Value[iPoint] = data.values[iVar];
                            }
                        }
                    }
                }
                iRow++;
            }
            return ret;
        }
        /// <summary>
        /// Предполагается регулярная сетка с севера на юг, с запада на восток.
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public object[] GetFileAttr(FileStream fs)
        {
            if (FileFormat != "FILE_VAN2011")
                throw new Exception("Неизвестный формат файла прогноза волнения " + FileFormat);

            List<double> leadTimeHours = new List<double>();
            List<double> lats = new List<double>();
            List<double> lons = new List<double>();

            // SCAN FILE
            StreamReader sr = new StreamReader(fs, Encoding.Default, false, 10000000);
            while (!sr.EndOfStream)
            {
                // READ LINE
                DataRow2011 data = DataRow2011.ParseLine(sr.ReadLine());
                if (data != null)
                {
                    // ADD LEAD TIME
                    if (!leadTimeHours.Exists(x => x == data.leadTime))
                        leadTimeHours.Add(data.leadTime);

                    // ADD GRID NODE
                    if (!lats.Exists(x => x == data.geoPoint.LatGrd)) lats.Add(data.geoPoint.LatGrd);
                    if (!lons.Exists(x => x == data.geoPoint.LonGrd)) lons.Add(data.geoPoint.LonGrd);
                }
            }

            // CREATE GRID FROM LATLONS
            lats = lats.OrderByDescending(x => x).ToList();
            lons = lons.OrderBy(x => x).ToList();
            double dlat = lats[1] - lats[0];
            double dlon = lons[1] - lons[0];
            if (Math.IEEERemainder((lats[lats.Count - 1] - lats[0]), dlat) != 0)
                throw new Exception(string.Format("Get FILE_VAN2011 grid error lat: {0} {1} {2}", lats[lats.Count - 1], lats[0], dlat));
            if (Math.IEEERemainder((lons[lons.Count - 1] - lons[0]), dlon) != 0)
                throw new Exception(string.Format("Get FILE_VAN2011 grid error lon: {0} {1} {2}", lons[lons.Count - 1], lons[0], dlon));

            Grid grid = new Grid(null, (int)Geo.EnumProjection.LATLON,
                lats.Count, lats[0] * 60, dlat * 60,
                lons.Count, lons[0] * 60, dlon * 60,
                "Generated from FILE_VAN2011");

            return new object[] { grid, leadTimeHours.OrderBy(x => x).ToList() };
        }

        bool isFTPPath(string path)
        {
            return (path.IndexOf("ftp") == 0) ? true : false;
        }

        FileStream OpenTempFile(DateTime dateIni)
        {
            string tempFileName = Path.GetTempFileName();
            string iniFileName = Path.Combine(FileDirectory, string.Format(FileNameMask, dateIni));
            //try
            //{
            if (isFTPPath(iniFileName))
                DB.Common.Download(iniFileName, tempFileName);
            else
                File.Copy(iniFileName, tempFileName, true);

            return File.OpenRead(tempFileName);
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public FERHRI.Field[][][] ReadFieldsInRectangles(DateTime dateIni, object dataFilter, List<double> leadTimeHours, List<GeoRectangle> grs2Truncate)
        {
            // DATA FILTER
            List<int> varIndeces;
            if (dataFilter == null)
            {
                if (FileFormat != "FILE_VAN2011")
                    throw new Exception("Неизвестный формат файла прогноза волнения " + FileFormat);

                varIndeces = new List<int>();
                for (int i = 0; i < DataRow2011.WaveParamNames2011.Length; i++)
                {
                    varIndeces.Add(i);
                }
            }
            else
                varIndeces = (List<int>)dataFilter;

            FileStream fs = null;
            try
            {
                fs = OpenTempFile(dateIni);
                if (fs == null)
                    return null;
                return ParseAsField(fs, varIndeces, leadTimeHours, grs2Truncate);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    File.Delete(fs.Name);
                }
            }
        }


        public double[][][] ReadValuesAtPoints(DateTime dateIni, object dataFilter, List<double> leadTimes, List<GeoPoint> points, EnumPointNearestType nearestType, EnumDistanceType distanceType)
        {
            throw new NotImplementedException();
        }
    }
}