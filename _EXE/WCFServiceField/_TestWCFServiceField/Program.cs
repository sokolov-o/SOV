using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _TestWCFServiceField.FieldServiceReference;
using _TestWCFServiceField.AmurServiceReference;

namespace _TestWCFServiceField
{
    class Program
    {
        static readonly string userName = "OSokolov";
        static readonly string userPassword = "qq";
        public static AmurServiceReference.ServiceClient clientA;
        public static long ha;
        public static FieldServiceReference.ServiceClient clientF;
        public static long hf;

        static void Main(string[] args)
        {
            #region OPEN SERVICE'S

            clientF = new FieldServiceReference.ServiceClient();
            clientF.CloseByUserName(userName);
            hf = clientF.Open(userName, userPassword);
            Console.WriteLine("ServiceFieldClient handle {0}.", hf);

            clientA = new AmurServiceReference.ServiceClient();
            ha = clientA.Open(userName, userPassword);
            Console.WriteLine("AmurServiceFieldClient {0}.", ha);

            // INCORRECT OPEN SERVICE (4SAMPLE)
            long h1 = clientF.Open("", ""); Console.WriteLine("ServiceFieldClient handle {0}.", h1);
            h1 = clientF.Open(userName, userPassword); Console.WriteLine("ServiceFieldClient handle {0}.", h1);

            #endregion

            DateTime dateIni = new DateTime(2019, 5, 19, 00, 0, 0); //DateTime.Today.AddDays(-1);

            // GET & PRINT FORECAST DATA FROM SERVICE

            try
            {
                // GET & WRITE 2 DB TRACK FORECASTS
                LogStarted("TrackForecast.Get");
                List<SOV.SGMO.DataTrackFcs> dataTrackFcs = TrackForecast.Get(
                    4/*Тайвань - Корсаков, 2019*/,
                    dateIni,
                    new int[]
                    {
                        //112, /*"Ближайший узел GFS 0.25*/
                        //1608  /*"Ближайший узел WAVE.VVO.PACIFIC.0p5"*/

                        113, /*"Интерполяция GFS 0.25*/
                        1609  /*Интерполяция WAVE.VVO.PACIFIC.0p5*/

                    }
                );
                LogEnded("TrackForecast.Get");

                try
                {
                    SOV.SGMO.DataManager.GetInstance().DataTrackFcsRepository.Insert(dataTrackFcs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n*** ОШИБКА: DataTrackFcsRepository.Insert(dataTrackFcs)\n");
                    throw ex;
                }

                // GET SITES FORECASTS
                //GetSiteForecast(dateIni, leadTimes);

                ////////
                //////// GET FIELDS IN REGION
                ////////
                //////List<GeoRectangle> geoRects = new List<GeoRectangle>()
                //////{
                //////    new GeoRectangle()
                //////    {
                //////        NorthWest = new _TestWCFServiceField.FieldServiceReference.GeoPoint() { LatGrd = 60, LonGrd = 110 },
                //////        SouthEast = new _TestWCFServiceField.FieldServiceReference.GeoPoint() { LatGrd = 30, LonGrd = 130 }
                //////    }
                ////// };
                //////List<Catalog> catalogs = clientA.GetCatalogList(ha,
                //////    new List<int>() { 10344 },  // Земной шар
                //////    null,                       // All variables
                //////    new List<int>() { 111 },    // GFS
                //////    null,                       // All sources
                //////    null,                       // All offset types
                //////    null                        // All offset values
                //////    );
                //////FieldServiceReference.Method method = clientF.GetMethods(hf).FirstOrDefault(x => x.Id == catalogs[0].MethodId);
                //////if (method == null)
                //////    throw new Exception(string.Format("Запрошенный метод с кодом {0} не обслуживается сервисом."));
                //////Varoff[] varoffs = catalogs.Select(x => new Varoff() { VariableId = x.VariableId, OffsetTypeId = x.OffsetTypeId, OffsetValue = x.OffsetValue }).ToArray();
                //////Field[/*LeadTime index*/][/*Georectangle index*/][/*Catalog index*/] dataF = clientF.GetFieldsInRectangles(hf, dateIni, method.Id, varoffs, leadTimeHours, geoRects);
                //////PrintDataFields(dateIni, leadTimeHours, geoRects, catalogs, dataF);
                //////dataF = clientF.GetFieldsInRectangles(hf, dateIni.AddDays(100), method.Id, varoffs, leadTimeHours, geoRects);
                //////PrintDataFields(dateIni.AddDays(100), geoRects, catalogs, dataF);
            }
            catch (Exception ex)
            {
                Console.WriteLine("*** " + ex.Message + "\n\n" + ex.ToString());
            }

            // EXIT

            clientF.Close(hf);
            clientF.Close(ha);

            Console.WriteLine("\nPress ENTER...");
            Console.ReadKey();
        }

        static void GetSiteForecast(DateTime dateIni, List<double> leadTimes)
        {
            List<Catalog> catalogs = clientA.GetCatalogList(ha,
                new List<int>() { 332 }, // Владивосток
                null,
                new List<int>() { 112 }, //Method  "Ближайший узел GFS 0.25"
                null, null, null
                );
            //catalogs = catalogs.FindAll(x => x.Id == 10368139 || x.Id == 71629).ToList(); // Осадки, Владивосток

            LogStarted("GetValuesAtPoints");

            Dictionary<double/*leadTime*/, double[]/*Catalog index*/> dataP = clientF.GetSitesForecast(hf, dateIni, leadTimes?.ToArray(), catalogs.Select(x => x.Id).ToArray());

            LogEnded("GetValuesAtPoints");
            PrintDataPoints(dateIni, catalogs, dataP);
        }

        static void PrintDataFields(DateTime dateIni, double[] leadTimeHours, SOV.Geo.GeoRectangle[] grs, List<Catalog> catalogs, Field[][][] data)
        {
            Console.WriteLine("\n-- Forecast as FIELD's from {0:yyyy.MM.dd HH}", dateIni);

            if (data == null)
                Console.WriteLine("... no data.");
            else
            {
                List<Variable> variables = clientA.GetVariablesByList(ha, catalogs.Select(x => x.VariableId).ToList());

                for (int i = 0; i < data.Length; i++)
                {
                    Console.Write("Lead time {0} h", leadTimeHours[i]);

                    if (data[i] == null)
                        Console.WriteLine("... no data.");
                    else
                    {
                        Console.WriteLine();
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            Console.WriteLine("Region {0}", grs[j].NorthWest.LatGrd + "x" + grs[j].NorthWest.LonGrd + "x" + grs[j].SouthEast.LatGrd + "x" + grs[j].SouthEast.LonGrd);

                            // FIELD IS NOT NULL
                            List<int> kNull = new List<int>();
                            for (int k = 0; k < data[i][j].Length; k++)
                            {
                                if (data[i][j][k] == null)
                                {
                                    int kk = k;
                                    kNull.Add(kk);
                                }
                                else
                                {
                                    Console.Write("Catalog {0} [{2}]. {1} points.", catalogs[k].Id, data[i][j][k].Value.Length, variables.FirstOrDefault(y => y.Id == catalogs[k].VariableId).NameRus);
                                    Console.WriteLine("\tAvg {0:.00}, max {1:.00}, min {2:.00}", data[i][j][k].Value.Average(), data[i][j][k].Value.Max(), data[i][j][k].Value.Min());
                                }
                            }
                            // FIELD IS NULL
                            foreach (var k in kNull)
                            {
                                Console.WriteLine("Catalog {0} [{1}] ... no data.", catalogs[k].Id, variables.FirstOrDefault(y => y.Id == catalogs[k].VariableId).NameRus);
                            }
                        }
                    }
                }
            }
        }

        static void PrintDataPoints(DateTime dateIni, List<Catalog> catalogs, Dictionary<double/*leadTime*/, double[]/*Catalog index*/> data)
        {
            Console.WriteLine("\n-- POINTS Date ini {0:yyyy.MM.dd HH}", dateIni);

            if (data == null)
                Console.WriteLine("... no data.");
            else
            {
                List<Variable> variables = clientA.GetVariablesByList(ha, catalogs.Select(x => x.VariableId).ToList());
                foreach (KeyValuePair<double, double[]> item in data)
                {
                    Console.WriteLine("Lead time {0} h", item.Key);

                    if (item.Value == null)
                        Console.WriteLine("... no data.");
                    else
                        for (int j = 0; j < item.Value.Length; j++)
                        {
                            Console.WriteLine("\t\tCatalog {0} [{2}]  Value {1}.", catalogs[j].Id, item.Value[j], variables.FirstOrDefault(x => x.Id == catalogs[j].VariableId).NameRus);
                        }
                }
            }
        }

        static DateTime dateS = DateTime.Now;
        static void LogStarted(string name)
        {
            dateS = DateTime.Now;
            Console.WriteLine("{0} started at {1}...", name, dateS);
        }
        static void LogEnded(string name)
        {
            Console.WriteLine("{0} ended at {1}, elapsed {2} minutes.", name, DateTime.Now, (int)((DateTime.Now - dateS).TotalMinutes));
        }

    }
}
