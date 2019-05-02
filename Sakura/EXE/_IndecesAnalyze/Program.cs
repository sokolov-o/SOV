using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Sakura.Meta;
using FERHRI.Sakura.Data;
using System.IO;

namespace _IndecesAnalyze
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ЧТЕНИЕ ПАРАМЕТРОВ ЗАДАЧИ App.congig

            // REPOSITORIES
            string[] ss;
            string cnnsMeta = global::Sakura.IndecesAnalyze.Properties.Settings.Default.MetaConnectionString;
            Console.WriteLine(cnnsMeta);
            FERHRI.Sakura.Meta.DataManager.ConnectionStringDefault = cnnsMeta;

            // DATESF & ACTION TIME
            ss = ConfigurationManager.AppSettings["DateSF"].Split('-');
            DateTime[] dateSF = new DateTime[] { DateTime.Parse(ss[0]), DateTime.Parse(ss[1]) };
            Console.WriteLine("\nПериод расчётов: {0}-{1}", dateSF[0].ToString("dd.MM.yyyy"), dateSF[1].ToString("dd.MM.yyyy"));

            Enums.TimePeriod actionTime = (Enums.TimePeriod)int.Parse(ConfigurationManager.AppSettings["ActionTimeId"]);

            // YEAR CLASSES
            Console.WriteLine("Классы и годы:");
            Dictionary<string, int[]> clsyear = new Dictionary<string, int[]>();
            string s = "";
            for (int i = 0; s != null; i++)
            {

                s = ConfigurationManager.AppSettings["Class" + i];
                Console.WriteLine("\t" + s);

                if (s != null)
                {
                    ss = s.Split(';');
                    clsyear.Add(ss[0], StrVia.ToListInt(ss[1], ",").ToArray());
                }
            }
            ClassSetInt yearClasses = new ClassSetInt(clsyear);

            // INDECES CLASSES
            ClassSetBound indClasses = ClassSetBound.Parse(ConfigurationManager.AppSettings["Indeces.ClassSetBound"]);

            // INDECES CATALOGS & REPO
            Dictionary<string, int?> ixs = StrVia.ToDictionarySI(ConfigurationManager.AppSettings["Indeces.CatalogId"]);
            List<Catalog> ixCatalogs = new List<Catalog>();
            List<IDbHCR> ixRepositories = new List<IDbHCR>();
            //Enums.TimePeriod ixTime = Enums.TimePeriod.UNKNOWN;
            FERHRI.Sakura.Meta.DataManager dmMeta = FERHRI.Sakura.Meta.DataManager.GetInstance();

            for (int i = 0; i < ixs.Count; i++)
            {
                Catalog ctl = dmMeta.CatalogRepository.Select((int)ixs.ElementAt(i).Value, true);
                //if (i == 0) ixTime = (Enums.TimePeriod)ctl.TimeId;

                if (ctl.TimeId != (int)actionTime) throw new Exception("(ctl.TimeId != (int)actionTime)");
                if (ixCatalogs.Exists(x => x.TimeId != ctl.TimeId)) throw new Exception("(indCatalogs.Exists(x => x.TimeId != ctl.TimeId))");

                ixCatalogs.Add(ctl);

                IDbHCR iDbHCR = (IDbHCR)FERHRI.Sakura.Data.DataManager.GetRepository(ctl.DbListId);
                if (iDbHCR == null)
                    throw new Exception("Неизвестный код базы данных (dbListId = " + ctl.DbListId + ") для менеджера данных FERHRI.Sakura.Data.DataManager.");
                ixRepositories.Add(iDbHCR);
            }

            // CLIMATE
            ss = ConfigurationManager.AppSettings["ClmYearSF"].Split('-');
            int[] clmYearSF = new int[] { int.Parse(ss[0]), int.Parse(ss[1]) };
            Console.WriteLine("\nКлимат: {0}-{1}", clmYearSF[0], clmYearSF[1]);

            // OUTPUT
            string outputDir = ConfigurationManager.AppSettings["OutputDir"];

            #endregion ЧТЕНИЕ ПАРАМЕТРОВ ЗАДАЧИ App.congig

            #region READ && NORMALIZE DATA

            List<DataHCR>[] datas = new List<DataHCR>[ixCatalogs.Count];
            for (int i = 0; i < ixCatalogs.Count; i++)
            {
                datas[i] = ixRepositories[i].Select(new List<Catalog>(new Catalog[] { ixCatalogs[i] }), dateSF[0], dateSF[1]);

                for (int j = 0; j < DateTimeVia.GetTimeNumMax(actionTime); j++)
                {
                    List<DataHCR> datastn = datas[i].FindAll(x => DateTimeVia.GetTimeNum(x.Date, actionTime) == (j + 1));
                    double[] avgstd = Support.AvgStdev(datastn
                        .Where(x => x.Date.Year >= clmYearSF[0] && x.Date.Year <= clmYearSF[1])
                        .Select(x => x.Value).ToArray());
                    for (int k = 0; k < datastn.Count; k++)
                    {
                        datastn[k].Value = (datastn[k].Value - avgstd[0]) / avgstd[1];
                    }
                }
            }

            #endregion READ && NORMALIZE DATA

            int[/*Catalog*/,/*ClassYear*/,/*TimeNum*/,/*ClassIndex*/] cls = new int
                [ixCatalogs.Count, yearClasses.Count, DateTimeVia.GetTimeNumMax(actionTime), indClasses.Count];

            for (int iCatalog = 0; iCatalog < ixCatalogs.Count; iCatalog++)
            {
                DateTimeVia date = new DateTimeVia(dateSF[0], actionTime, actionTime);
                while (date.Date <= dateSF[1])
                {
                    Console.WriteLine(date.Date.ToString("dd.MM.yyyy"));

                    int iClass = yearClasses.ClassOf(date.Year);
                    int iTimeNum = date.TimeNum - 1;

                    DataHCR data = datas[iCatalog].FirstOrDefault(x => x.Date == date.Date);
                    if (data == null)
                    {
                        Console.WriteLine("\t scipped...");
                    }
                    else
                    {
                        int iClassInd = indClasses.ClassOf(data.Value);

                        cls[iCatalog, iClass >= 0 ? iClass : yearClasses.Count, iTimeNum, iClassInd]++;
                    }
                    date.NextTimePeriod();
                }
            }
            Console.WriteLine("\r\n" + ToString(cls, ixCatalogs, indClasses, yearClasses));
            ToFile(outputDir + "\\Ix_YearClasses.csv", ToString(cls, ixCatalogs, indClasses, yearClasses));
        }

        private static string ToString(int[, , ,] cls, List<Catalog> ctls, ClassSetBound indClassSet, ClassSetInt yearClassSet)
        {
            char spl = ';';
            string ret = "Index;YearClass;TimeNum;IndexClass;Count;IndexClassSet\r";
            for (int iCatalog = 0; iCatalog <= cls.GetUpperBound(0); iCatalog++)
            {
                for (int iClassYear = 0; iClassYear <= cls.GetUpperBound(1); iClassYear++)
                {
                    for (int iTimeNum = 0; iTimeNum <= cls.GetUpperBound(2); iTimeNum++)
                    {
                        for (int iClassInd = 0; iClassInd <= cls.GetUpperBound(3); iClassInd++)
                        {
                            ret += ctls[iCatalog].Name.Replace(";", "|")
                                + spl + yearClassSet.GetClassName(iClassYear)
                                + spl + (iTimeNum + 1)
                                + spl + indClassSet.GetClassName(iClassInd)
                                + spl + cls[iCatalog, iClassYear, iTimeNum, iClassInd]
                                + spl + indClassSet.GetClassNameSet()
                                + "\r";
                        }
                    }
                }
            }
            return ret;
        }
        public static void ToFile(string path, string s)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("windows-1251"));
            try
            {
                sw.WriteLine(s);
            }
            finally
            {
                sw.Close();
            }
        }
        private static string _DELME_ToString1(int[, , ,] cls)
        {
            string ret = "";
            for (int iCatalog = 0; iCatalog <= cls.GetUpperBound(0); iCatalog++)
            {
                ret = "Index " + iCatalog + "\n\r";
                for (int iClassYear = 0; iClassYear <= cls.GetUpperBound(1); iClassYear++)
                {
                    ret += "ClassYear " + iClassYear + "\n\r";
                    for (int iTimeNum = 0; iTimeNum <= cls.GetUpperBound(2); iTimeNum++)
                    {
                        ret += iTimeNum;
                        for (int iClassInd = 0; iClassInd <= cls.GetUpperBound(3); iClassInd++)
                        {
                            ret += "\t" + cls[iCatalog, iClassYear, iTimeNum, iClassInd];
                        }
                        ret += "\n\r";
                    }
                }
            }
            return ret;
        }
    }
}
