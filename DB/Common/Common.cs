using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SOV.DB
{
    public class Common
    {
        /// <summary>
        /// Закачать файл c ftp.
        /// </summary>
        /// <param name="srcFilePathFTP">Source FTP-file.</param>
        /// <param name="dstFilePath">Destination file.</param>
        public static void Download(string srcFilePathFTP, string dstFilePath)
        {
            // Создадим запрос для работы с ftp
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(srcFilePathFTP);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            // и объект ответа сервера
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            // Открываем поток для получения файла
            Stream srcStream = response.GetResponseStream();
            // и поток для записи в файл
            FileStream dstStream = new FileStream(dstFilePath, FileMode.Create);

            // Получаем и сохраняем файл
            byte[] buffer = new byte[1024];
            int bytesread;
            while ((bytesread = srcStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                dstStream.Write(buffer, 0, bytesread);
            }

            // Закрываем все потоки
            srcStream.Close();
            dstStream.Close();
            response.Close();
        }
        public static bool isFTPPath(string path)
        {
            return (path.IndexOf("ftp:") >= 0) ? true : false;
        }
    }
}
