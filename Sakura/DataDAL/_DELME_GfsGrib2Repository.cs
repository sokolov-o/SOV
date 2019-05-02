using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viaware.Sakura.Meta;
using FERHRI.Common;
using System.IO;
using System.Net;
using Viaware.Sakura.Grib;

namespace Viaware.Sakura.Data
{
    public class _DELME_GfsGrib2Repository : SakuraDb, IDbFieldCDV
    {
        List<Grid> _bufGrids = new List<Grid>();

        public _DELME_GfsGrib2Repository(int DbListId, string DirPath, User User)
            : base(DbListId, DirPath, User)
        {
        }
        /// <summary>
        /// Выбрать прогностическое или диагностическое поле.
        /// </summary>
        /// <param name="catalog">Запись каталога.</param>
        /// <param name="dateFcs">Дата прогноза. Не исходная дата, с которой рассчитываются прогнозы.</param>
        /// <param name="predictTime">Заблаговременность даты прогноза (час). Не должно быть null.</param>
        /// <param name="grExtract">Регион, до которого усекается исходное поле. Усечение не производится, если null.</param>
        /// <returns>Список полей различной заблаговременности.</returns>
        public List<Field> SelectFields(Catalog catalog, DateTime dateFcs, int? predictTime, GeoRectangle grExtract)
        {
            // RESTRICTIONS
            if (!predictTime.HasValue) throw new Exception("При работе с данными GFS необходимо явно указывать заблаговременность.");
            if ((Enums.TimePeriod)catalog.TimeId != Enums.TimePeriod.HOUR) throw new Exception("ctlRec.TimeId!= Enums.TimePeriod.HOUR");

            Grib2 grib2 = Meta.DataManager.GetInstance().CatalogRepository.SelectGrib2(catalog);
            string tempFileName = Path.GetTempFileName();
            int predictHour = (int)predictTime;
            FileStream fs = null;

            // GET GRIB2 FILE & Parse it.
            try
            {
                fs = GetFileGrib(catalog, tempFileName, DateTimeProcess.Add(dateFcs, (Enums.TimePeriod)catalog.TimeId, -1 * predictHour), predictHour);
                if (fs != null)
                {
                    Object[] dataDatePT = FileGrib2.GetData(fs, grib2,
                        Meta.DataManager.GetInstance().GridTypeRepository.Select(),
                        Meta.DataManager.GetInstance().CenterRepository.Select().FindAll(x => x.GribId.HasValue));
                    if (dataDatePT != null)
                    {
                        DateTime resDate = (DateTime)dataDatePT[1];
                        int prdHour = (int)FileGrib2.GetPredictTimeHour((int)dataDatePT[3], (int)dataDatePT[2]);
                        // TODO: ИСПРАВИТЬ на использование timePeriod!!!!
                        if (dateFcs.Ticks != resDate.AddHours(predictHour).Ticks && predictHour != prdHour)
                            throw new Exception("Ошибка алгоритма. (dateFcs.Ticks != resDate.AddHours(predictHour).Ticks && predictHour != prdHour)");

                        Field field = new Field(
                            GetGrid(catalog.GridId),
                            Enums.FieldFormat.GRID,
                            predictHour,
                            (double[])dataDatePT[0]
                        );
                        if ((object)grExtract != null)
                            field = field.Extract(grExtract);
                        return new List<Field>() { field };
                    }
                }
                return null;
            }
            finally
            {
                if (fs != null) fs.Close();
                File.Delete(tempFileName);
            }
        }
        private Grid GetGrid(int gridId)
        {
            Grid grid = _bufGrids.FirstOrDefault(x => x.Id == gridId);
            if (grid == null)
            {
                grid = Meta.DataManager.GetInstance().GridRepository.Select(gridId);
                _bufGrids.Add(grid);
            }
            return grid;
        }
        private string GetPath(Catalog Catalog)
        {
            switch (DbListId)
            {
                case (int)Enums.DbList.FILES_GFS_OMA_5deg:
                    return StrVia.GetValue(Catalog.DataCnn, "uri");
                default:
                    throw new Exception("Unknown FileStoreType");
            }

        }
        public static bool isFTPPath(string path)
        {
            return (path.IndexOf("ftp:") >= 0) ? true : false;
        }
        /// <summary>
        /// Возвращает по дате название директория на ftp этой даты
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetDirForDate(DateTime date)
        {
            switch (DbListId)
            {
                case (int)Enums.DbList.FILES_GFS_OMA_5deg:
                    return "gfs." + date.ToString("yyyyMMddHH");
                default:
                    throw new Exception("Unknown dbListId = " + DbListId);
            }
        }
        /// <summary>
        /// Возвращает имя файла по дате, predictTime
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pridictTime"></param>
        /// <returns></returns>
        public string GetFileNameForDate(DateTime date, int pridictTime)
        {
            switch (DbListId)
            {
                case (int)Enums.DbList.FILES_GFS_OMA_5deg:
                    if (date >= new DateTime(2015, 1, 14, 12, 0, 0))
                        return String.Format("gfs.t{0:D2}z.pgrb2.0p50.f{1:D3}", date.Hour, pridictTime);
                    else
                        return String.Format("gfs.t{0:D2}z.pgrb2f{1:D2}", date.Hour, pridictTime);
                case (int)Enums.DbList.FILES_GFS_OMA_025deg:
                    return String.Format("gfs.t{0:D2}z.pgrb2.0p25.f{1:D3}", date.Hour, pridictTime);
                default:
                    throw new Exception("Unknown DbListId = " + DbListId);
            }
        }
        /// <summary>
        /// Функция получает список файлов в заданной директории ftp-сервера, 
        /// если дириктории не существует возвращает null
        /// </summary>
        /// <param name="uriDir">ftp dir.</param>
        /// <returns>Files name list.</returns>
        private static List<string> GetFileList(string uriDir)
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
        /// копирование файла Grib для конкретной даты и predictTime
        /// </summary>
        /// <param name="Catalog"></param>
        /// <param name="tempFileName"></param>
        /// <param name="date">дата старта прогноза</param>
        /// <param name="predictHour">заблаговременность в часах, по ней ищется файл(фирурирует в названии!!!)</param>
        /// <returns></returns>
        protected FileStream GetFileGrib(Catalog Catalog, string tempFileName, DateTime date, int predictHour)
        {
            //            Enums.FileStoreType fileStoreTypeStr = getFileStoreType(Catalog);
            string path = GetPath(Catalog);
            return GetFileGrib(path, tempFileName, date, predictHour);
        }

        public FileStream GetFileGrib(string path, string tempFileName, DateTime date, int predictHour)
        {
            string dir = GetDirForDate(date);
            string fileName = GetFileNameForDate(date, predictHour);
            string fullFileName = Path.Combine(new string[] { path, dir, fileName });
            if (isFTPPath(path))
            {

                List<string> pathList = GetFileList(path);
                bool isPathExist = false;
                foreach (string pathDir in pathList)
                {
                    if (pathDir.Contains(dir))
                    {
                        isPathExist = true;
                        break;
                    }
                }

                if (!isPathExist)
                    return null;
                //проверяем наличие файла
                if (!isFileExist(Path.Combine(path, dir), fileName))
                    return null;
                Download(fullFileName, tempFileName);
            }
            else
            {
                //Path.PathSeparator
                if (!Directory.Exists(Path.Combine(path, dir)))
                    return null;
                if (!File.Exists(fullFileName))
                    return null;
                File.Copy(fullFileName, tempFileName, true);
            }
            return File.OpenRead(tempFileName);
        }
        public static bool isFileExist(string uriPath, string fileName)
        {
            List<string> pathList = GetFileList(uriPath);
            bool isFileExist = false;
            foreach (string pathDir in pathList)
            {
                if (pathDir.EndsWith(fileName))
                {
                    isFileExist = true;
                    break;
                }
            }
            return isFileExist;
        }
        /// <summary>
        /// Функция закачки файла c ftp.
        /// </summary>
        /// <param name="srcFTPFilePath">Source FTP-file.</param>
        /// <param name="dstFilePath">Destination file.</param>
        static void Download(string srcFTPFilePath, string dstFilePath)
        {
            // Создадим запрос для работы с ftp
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(srcFTPFilePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            // и объект ответа сервера
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            // Открываем поток для получения файла
            Stream stream = response.GetResponseStream();

            // и поток для записи в файл
            FileStream filestream = new FileStream(dstFilePath, FileMode.Create);

            // Получаем и сохрпняем файл
            byte[] buffer = new byte[1024];
            int bytesread;
            do
            {
                bytesread = stream.Read(buffer, 0, buffer.Length);
                filestream.Write(buffer, 0, bytesread);
            }
            while (bytesread > 0);

            // Закрываем все потоки и выводим сообщение о завершении
            stream.Close();
            filestream.Close();
            response.Close();
            //Console.WriteLine("Download complete");
        }
    }
}
