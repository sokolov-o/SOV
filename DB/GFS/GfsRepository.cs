using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

using SOV.Common;
using SOV.Grib;
using SOV.Geo;
using SOV;
using Seaware.GribCS.Grib2;
using Seaware.GribCS;
using System.Diagnostics;

namespace SOV.DB
{
    public class GfsRepository : IFcsGrid
    {
        public string FileFormat { get; set; }
        public string FileDirectory { get; set; }
        /// <summary>
        /// Маска для поддиректория (на основе даты).
        /// </summary>
        public string FileSubDirMask { get; set; }
        /// <summary>
        /// Маска файла на основе даты (срока) и заблаговременности (час)
        /// </summary>
        public string FileNameMask { get; set; }
        public bool IsFileZipped { get; set; }

        public GfsRepository(string formatName, string fileDirectory, string fileSubDirMask, string fileNameMask, string isFileZipped)
        {
            FileFormat = formatName;
            FileDirectory = fileDirectory;
            FileSubDirMask = fileSubDirMask;
            FileNameMask = fileNameMask;
            IsFileZipped = int.Parse(isFileZipped) == 0 ? false : true;
        }

        //////User _user;
        //////double _gfsDxDy;
        ///////// <summary>
        ///////// Конструктор.
        ///////// </summary>
        ///////// <param name="gfsDxDy">Шаг сетки (град).</param>
        ///////// <param name="user">Пользователь.</param>
        //////public GfsRepository(double gfsDxDy, User user = null)
        //////{
        //////    _user = user;
        //////    _gfsDxDy = gfsDxDy;
        //////}

        /// <summary>
        /// GET GRIB2 FILE & Parse it.
        /// </summary>
        /// <param name="g2filter">Фильтр записей файла grib2</param>
        /// <param name="dateIni">Исх. дата прогноза.</param>
        /// <param name="predictTime">Заблаговременность.</param>
        /// <returns>Данные или null, если файл не существует.</returns>
        public Object[/*grib2filter index*/][/*Grib2Record;float[] data*/] Select(List<Grib2Filter> g2filter, DateTime dateIni, int predictTime)
        {
            FileStream fs = null;

            try
            {
                fs = OpenTempFileGrib(dateIni, predictTime);
                return (fs == null) ? null : GetData(fs, g2filter);

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

        static Object[/*grib2filter index*/][/*Grib2Record;float[] data*/] GetData(FileStream fs, List<Grib2Filter> grib2filters)
        {
            Grib2Input gi = new Grib2Input(fs);

            try
            {
                gi.scan(false, false);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR scan GFS grib2 file.", ex);
            }

            Object[/*grib2filter index*/][/*Grib2Record;float[] data*/] ret = new Object[grib2filters.Count][];
            for (int i = 0; i < gi.Records.Count; i++)
            {
                Grib2Record rec = (Grib2Record)gi.Records[i];
                for (int j = 0; j < grib2filters.Count; j++)
                {
                    if (grib2filters[j].Equal(rec))
                    {
                        if (ret[j] != null) throw new Exception("More one record founded in grib2-file.");

                        float[] data = (new Grib2Data(fs)).getData(rec.getGdsOffset(), rec.getPdsOffset());
                        ret[j] = new Object[] { rec, grib2filters[j].AcceptAddMultiply2Value(data) };
                    }
                }
            }
            return ret;
        }
        //////private string GetPath()
        //////{
        //////    if (_gfsDxDy == 0.5) return SOV.DB.Properties.Settings.Default.GFSFTPPath05full;
        //////    if (_gfsDxDy == 0.25) return SOV.DB.Properties.Settings.Default.GFSFTPPath025;
        //////    throw new Exception("Unknown GFS path for Dx = " + _gfsDxDy);
        //////}

        /// <summary>
        /// Возвращает по дате название директория на ftp этой даты
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        string GetDirForDate(DateTime date)
        {
            //if (_gfsDxDy == 0.5 || _gfsDxDy == 0.25)
            //    return "gfs." + date.ToString("yyyyMMddHH");
            //throw new Exception("Unknown GFS directory for date for Dx = " + _gfsDxDy);

            return string.Format(FileSubDirMask, date);
        }
        /// <summary>
        /// Возвращает имя файла по дате, predictTime
        /// </summary>
        /// <param name="date"></param>
        /// <param name="leadTime"></param>
        /// <returns></returns>
        string GetFileNameForDate(DateTime date, int leadTime)
        {
            //if (_gfsDxDy == 0.5)
            //{
            //    if (date >= new DateTime(2015, 1, 14, 12, 0, 0))
            //        return String.Format("gfs.t{0:D2}z.pgrb2.0p50.f{1:D3}", date.Hour, lag);
            //    else
            //        return String.Format("gfs.t{0:D2}z.pgrb2f{1:D2}", date.Hour, lag);
            //}
            //else if (_gfsDxDy == 0.25)
            //{
            //    return String.Format("gfs.t{0:D2}z.pgrb2.0p25.f{1:D3}", date.Hour, lag);
            //}
            //throw new Exception("Unknown GFS file name for date for Dx = " + _gfsDxDy);

            return String.Format(FileNameMask, date, leadTime);
        }
        /// <summary>
        /// Функция получает список файлов в заданной директории ftp-сервера, 
        /// если дириктории не существует возвращает null
        /// </summary>
        /// <param name="uriDir">ftp dir.</param>
        /// <returns>Files name list.</returns>
        private static List<string> GetFTPFileList(string uriDir)
        {
            List<string> ret = new List<string>();

            try
            {
                // Создадим запрос для работы с ftp
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uriDir);
                // Выбираем получение детального списка файлов
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                // Выводим полученный список
                string str;// = reader.ReadLine();
                while ((str = reader.ReadLine()) != null)
                {
                    ret.Add(str);
                    //str = reader.ReadLine();
                }
                return ret;
            }
            catch { return null; }
        }

        /// <summary>
        /// Копирование и открытие на чтение файла Grib для конкретной даты и predictTime
        /// </summary>
        /// <param name="tempFileName"></param>
        /// <param name="date">дата старта прогноза</param>
        /// <param name="predictHour">заблаговременность в часах, по ней ищется файл (фирурирует в названии файла)</param>
        /// <returns>FileStream или null, если директорий или файл не существует.</returns>
        FileStream OpenTempFileGrib(DateTime date, int predictHour)
        {
            try
            {
                string tempFileName = Path.GetTempFileName();
                string filePath4Lag = Path.Combine(new string[] { FileDirectory, GetDirForDate(date), GetFileNameForDate(date, predictHour) });
                if (Common.isFTPPath(FileDirectory))
                {
                    //// Существует путьи директория для даты прогноза?
                    //List<string> pathList = GetFTPFileList(path);
                    //if (pathList == null)
                    //    throw new Exception("Путь к GFS файлам не существует - " + path);
                    //if (!pathList.Exists(x => x.Contains(dir4Date)))
                    //    return null;

                    //// Существует файл для заблаговремености?
                    //if (!isFileExist(Path.Combine(path, dir4Date), fileName4Lag))
                    //    return null;

                    // Закачиваем файл
                    if (CheckDirForReady(Path.Combine(FileDirectory, GetDirForDate(date))))
                    {
                        DB.Common.Download(filePath4Lag, tempFileName);
                    }
                }
                else
                {
                    //if (!Directory.Exists(Path.Combine(path, dir4Date)) || !File.Exists(filePath4Lag)) return null;
                    File.Copy(filePath4Lag, tempFileName, true);
                }
                return File.OpenRead(tempFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        static bool isFileExist(string uriPath, string fileName)
        {
            List<string> pathList = GetFTPFileList(uriPath);
            return pathList.Exists(x => x.EndsWith(fileName));
        }

        static bool CheckDirForReady(string dirPath)
        {
            bool ret = false;
            // Создадим запрос для работы с ftp
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirPath + @"/ready");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            // и объект ответа сервера
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            // Открываем поток для получения файла
            Stream srcStream = response.GetResponseStream();

            // и поток для записи в файл
            // FileStream dstStream = new FileStream(dstFilePath, FileMode.Create);

            // Получаем и сохраняем файл
            byte[] buffer = new byte[1024];
            int bytesread;
            while ((bytesread = srcStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                //dstStream.Write(buffer, 0, bytesread);
            }
            ret = true;
            // Закрываем все потоки
            srcStream.Close();
            //  dstStream.Close();
            response.Close();
            return ret;
        }
        public DateTime GetMaxDateIni()
        {
            // Определить максимальную дату прогноза
            List<string> pathList = (Common.isFTPPath(FileDirectory)) ? GetFTPFileList(FileDirectory) : Directory.GetFiles(FileDirectory).ToList();
            if (pathList == null)
                throw new Exception("Путь к GFS файлам не существует - " + FileDirectory);

            DateTime dateIniMax = DateTime.MinValue;
            foreach (var item in pathList)
            {
                DateTime dateIni = DateTimeProcess.Parse(item.Split('.')[1], "yyyyMMddHH");
                if (dateIni > dateIniMax)
                    dateIniMax = dateIni;
            }
            return dateIniMax;
        }
        public double GetMaxLag(DateTime dateIni)
        {
            // Определить максимальную заблаговременность прогноза
            string path = Path.Combine(new string[] { FileDirectory, GetDirForDate(dateIni) });
            List<string> pathList = (Common.isFTPPath(FileDirectory)) ? GetFTPFileList(FileDirectory) : Directory.GetFiles(FileDirectory).ToList();

            int lagMax = int.MinValue;
            foreach (var item in pathList)
            {
                int lag = int.Parse(item.Split('.')[5].Substring(1, 3));
                if (lag > lagMax) lagMax = lag;
            }
            return lagMax;
        }

        public Field[/*Grib2Filter index*/] SelectFields(List<Grib2Filter> g2filter, DateTime dateIni, int predictTime, GeoRectangle gr2Truncate)
        {
            Field[][] ret = SelectFields(g2filter, dateIni, predictTime, (object)gr2Truncate == null ? null : new List<GeoRectangle>() { gr2Truncate });
            return ret[0];
        }
        public Field[/*Georectangle index*/][/*Grib2Filter index*/] SelectFields(List<Grib2Filter> g2filter, DateTime dateIni, int leadTime, List<GeoRectangle> grs2Truncate)
        {
            // READ GRIB FILE 
            Object[/*grib2filter index*/][/*Grib2Record;float[] data*/] gribData = Select(g2filter, dateIni, leadTime);
            if (gribData == null)
                return null;

            List<Field> fields = GFS.ToFields(gribData);
            if (fields.Count == 0)
                return null;

            Field[][] ret = null;
            if ((object)grs2Truncate != null)
            {
                ret = new Field[grs2Truncate.Count][];
                for (int i = 0; i < grs2Truncate.Count; i++)
                {
                    ret[i] = Field.Truncate(fields, grs2Truncate[i]);
                }
            }
            else
            {
                ret = new Field[][] { fields.ToArray() };
            }
            return ret;
        }

        public Field[/*leadTime*/][/*Georectangle index*/][/*Grib2Filter index*/] ReadFieldsInRectangles
            (DateTime dateIni, object dataFilter, List<double> leadTime, List<GeoRectangle> grs2Truncate)
        {
            List<Grib2Filter> g2Filter = (List<Grib2Filter>)dataFilter;
            Field[/*leadTime*/][/*Georectangle index*/][/*Grib2Filter index*/] ret = new Field[leadTime.Count][][];//, grs2Truncate.Count, g2Filter.Count];
            bool isNull = true;
            for (int i = 0; i < leadTime.Count; i++)
            {
                Debug.WriteLine(string.Format("{0}: Read DateIni {1} LeadTime {2}", this, dateIni, (int)leadTime[i]));

                ret[i] = SelectFields(g2Filter, dateIni, (int)leadTime[i], grs2Truncate);

                if (ret[i] != null) isNull = false;
            }
            return isNull ? null : ret;
        }

        public Field[/*leadTime*/][/*Georectangle index*/][/*Grib2Filter index*/] _DELME_ReadFieldsInRectangles
            (DateTime dateIni, object dataFilter, List<double> leadTime, List<GeoRectangle> grs2Truncate)
        {
            List<Grib2Filter> g2Filter = (List<Grib2Filter>)dataFilter;
            Field[/*leadTime*/][/*Georectangle index*/][/*Grib2Filter index*/] ret = new Field[leadTime.Count][][];
            bool isNull = true;
            for (int i = 0; i < leadTime.Count; i++)
            {
                Debug.WriteLine(string.Format("{0}: Read DateIni {1} LeadTime {2}", this, dateIni, (int)leadTime[i]));

                ret[i] = SelectFields(g2Filter, dateIni, (int)leadTime[i], grs2Truncate);
                if (isNull) isNull = ret[i] == null;
            }
            return isNull ? null : ret;
        }

        /// <summary>
        /// Прочитать значения заданных в фильтре переменных в указанных точках.
        /// Значения в точках м.б. интерполированными или взятыми из ближайшего узла.
        /// 
        /// Return: значения в массиве double[/*g2filter index*/][/*point index*/].
        /// </summary>
        /// <param name="g2filter"></param>
        /// <param name="dateIni">Исх. дата прогноза</param>
        /// <param name="predictTime"></param>
        /// <param name="points"></param>
        /// <param name="nearestType"></param>
        /// <param name="distanceType"></param>
        /// <returns></returns>
        public double[/*g2filter*/][/*points*/] ReadValuesAtPoints(DateTime dateIni, List<Grib2Filter> g2filter, int predictTime,
            List<GeoPoint> points, EnumPointNearestType nearestType, EnumDistanceType distanceType)
        {
            Object[/*grib2filter index*/][/*Grib2Record;float[] data*/] gfsRecords = Select(g2filter, dateIni, predictTime);

            if (gfsRecords != null)
            {
                List<Field> fields = GFS.ToFields(gfsRecords);

                double[][] ret = new double[g2filter.Count][];

                for (int i = 0; i < g2filter.Count; i++)
                {
                    ret[i] = (fields[i] != null)
                        ? fields[i].Interpolate(points, nearestType, distanceType)
                        : Support.Allocate(points.Count, double.NaN);
                }
                return ret;
            }
            return null;
        }

        public double[/*leadTime*/][/*GeoPoint index*/][/*Grib2Filter index*/] ReadValuesAtPoints(DateTime dateIni, object dataFilter, List<double> leadTimes,
            List<GeoPoint> points, EnumPointNearestType nearestType, EnumDistanceType distanceType)
        {
            // CHECK INPUT

            if (dataFilter == null)
                throw new Exception("Не определён фильтр при вызове метода чтения данных из полей GRIB2.");
            if (dataFilter.GetType() != typeof(List<Grib2Filter>))
                throw new Exception(string.Format("Указан фильтр типа {0}. Должен быть типа {1}.", dataFilter.GetType(), typeof(List<Grib2Filter>)));

            // SCAN LEADTIMES

            List<Grib2Filter> grib2Filter = (List<Grib2Filter>)dataFilter;
            double[][][] ret = SOV.Common.Support.Allocate(leadTimes.Count, points.Count, grib2Filter.Count, double.NaN);
            bool isNull = true;

            for (int i = 0; i < leadTimes.Count; i++)
            {
                double[][] d = ReadValuesAtPoints(dateIni, grib2Filter, (int)leadTimes[i], points, nearestType, distanceType);

                if (d != null)
                {
                    for (int j = 0; j < grib2Filter.Count; j++)
                    {
                        for (int k = 0; k < points.Count; k++)
                        {
                            ret[i][k][j] = d[j][k];
                        }
                    }
                    isNull = false;
                }
            }
            return isNull ? null : ret;
        }
    }
}
