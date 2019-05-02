using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FERHRI.GISMeteo;
using System.Configuration;

namespace _GISMeteoScaner
{
    class Program
    {
        //static string outFile = @"D:\Temp\FERHRI\GISMeteoScaner.csv";
        static void Main(string[] args)
        {
            GISMeteoRepository gm = GISMeteoRepository.Instance;
            int codeForm;
            List<StationInfo> stInfo;i
            string outFile = ConfigurationManager.AppSettings["OutputDir"] + @"\GISMeteoScaner.csv";

            if (File.Exists(outFile)) File.Delete(outFile);
            StreamWriter sw = new StreamWriter(outFile, true, Encoding.UTF8);
            sw.WriteLine("code_form;station_index;date_min;date_max;param_ids;hours;count_record");

            try
            {
                stInfo = gm.GetStationInfo(codeForm = 16, null);
                sw.WriteLine(StationInfo.ToString(stInfo, codeForm.ToString()));

                stInfo = gm.GetStationInfo(codeForm = 44, null);
                sw.WriteLine(StationInfo.ToString(stInfo, codeForm.ToString()));

                stInfo = gm.GetStationInfo(codeForm = 45, null);
                sw.WriteLine(StationInfo.ToString(stInfo, codeForm.ToString()));
            }
            finally
            {
                sw.Close();
            }
        }
    }
}
