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
        static string userName = "OSokolov";
        static string userPassword = "qq";

        static void Main(string[] args)
        {
            #region OPEN SERVICE'S

            FieldServiceReference.ServiceClient clientF = new FieldServiceReference.ServiceClient();
            clientF.CloseByUserName(userName);
            long hf = clientF.Open(userName, userPassword);
            Console.WriteLine("ServiceFieldClient handle {0}.", hf);

            AmurServiceReference.ServiceClient clientA = new AmurServiceReference.ServiceClient();
            long ha = clientA.Open(userName, userPassword);
            Console.WriteLine("AmurServiceFieldClient {0}.", ha);

            // INCORRECT OPEN SERVICE
            long h1 = clientF.Open("", ""); Console.WriteLine("ServiceFieldClient handle {0}.", h1);
            h1 = clientF.Open(userName, userPassword); Console.WriteLine("ServiceFieldClient handle {0}.", h1);

            #endregion

            // GET & PRINT DATA FROM SERVICE
            try
            {
                #region GET FIELDS IN REGION

                DateTime dateIni = DateTime.Today.AddDays(-1);
                int[] catalogIds = new int[]
                {
                      771921 // Ta, NCEP
                    , 962741 // U-wind, NCEP
                    , 962742 // V-wind, NCEP
                    , 962744 // Tw, NCEP
                    , 962743 // Pmsl, NCEP
                    , 962747 // Gust-wind, NCEP
                    , 962746 // Precipitatio-3h, NCEP
                    , 962745 // Rh-%, NCEP
                };
                GeoRectangle[] grs = new GeoRectangle[]
                {
                    new GeoRectangle() { NorthWest = new GeoPoint() { LatGrd = 60, LonGrd = 110 },  SouthEast = new GeoPoint() { LatGrd = 30, LonGrd = 130 } }
                };
                //double[] leadTimeHours = method.MethodForecast.LeadTimesHours;
                double[] leadTimeHours = new double[] { 0, 12, -24 };

                List<AmurServiceReference.Catalog> catalogs = clientA.GetCatalogListById(ha, catalogIds.ToList());
                FieldServiceReference.Method method = clientF.GetMethods(hf).FirstOrDefault(x => x.Id == catalogs[0].MethodId);
                if (method == null)
                    throw new Exception(string.Format("Запрошенный метод с кодом {0} не обслуживается сервисом."));
                Varoff[] varoffs = catalogs.Select(x => new Varoff() { VariableId = x.VariableId, OffsetTypeId = x.OffsetTypeId, OffsetValue = x.OffsetValue }).ToArray();

                // GET DATA

                Field[/*LeadTime index*/][/*Georectangle index*/][/*Catalog index*/] dataF
                    = clientF.GetFieldsInRectangles(hf, dateIni, method.Id, varoffs, leadTimeHours, grs);
                PrintDataFields(dateIni, leadTimeHours, grs, catalogIds, dataF);

                dataF = clientF.GetFieldsInRectangles(hf, dateIni.AddDays(100), method.Id, varoffs, leadTimeHours, grs);
                PrintDataFields(dateIni.AddDays(100), leadTimeHours, grs, catalogIds, dataF);

                #endregion

                #region GET VALUES AT POINTS

                catalogIds = new int[]
                {
                      962748 // Ta, Владивосток, GFS 0.25
                };
                int fcsSiteId = 10344; // Земной шар
                int siteSysAttrTypeIdLat = 1000; // Широта
                int siteSysAttrTypeIdLon = 1001; // Долгота

                double[/*leadTime*/][/*Catalog index*/] dataP = clientF.GetValuesAtPoints(hf,
                    dateIni, catalogIds, leadTimeHours,
                    fcsSiteId, siteSysAttrTypeIdLat, siteSysAttrTypeIdLon);

                PrintDataPoints(dateIni, leadTimeHours, catalogIds, dataP);

                #endregion
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

        static void PrintDataFields(DateTime dateIni, double[] leadTimeHours, GeoRectangle[] grs, int[] catalogIds, Field[][][] data)
        {
            Console.WriteLine("\n-- FIELDS Date ini {0:yyyy.MM.dd HH}", dateIni);

            if (data == null)
                Console.WriteLine("... no data.");
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine("Lead time {0} h", leadTimeHours[i]);

                    if (data[i] == null)
                        Console.WriteLine("... no data.");
                    else
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            Console.WriteLine("\tRegion {0}", grs[j].NorthWest.LatGrd + "x" + grs[j].NorthWest.LonGrd + "x" + grs[j].SouthEast.LatGrd + "x" + grs[j].SouthEast.LonGrd);
                            for (int k = 0; k < data[i][j].Length; k++)
                            {
                                Console.Write("\t\tCatalog {0}. Field nodes count {1}.", catalogIds[k],
                                    data[i][j][k] == null ? 0 : data[i][j][k].Value.Length);
                                if (data[i][j][k] != null && data[i][j][k].Value.Length > 0)
                                    Console.Write(" Avg {0:.00}, max {1:.00}, min {2:.00}", data[i][j][k].Value.Average(), data[i][j][k].Value.Max(), data[i][j][k].Value.Min());
                                Console.WriteLine();
                            }
                        }
                }
            }
        }

        static void PrintDataPoints(DateTime dateIni, double[] leadTimeHours, int[] catalogIds, double[][] data)
        {
            Console.WriteLine("\n-- POINTS Date ini {0:yyyy.MM.dd HH}", dateIni);

            if (data == null)
                Console.WriteLine("... no data.");
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine("Lead time {0} h", leadTimeHours[i]);

                    if (data[i] == null)
                        Console.WriteLine("... no data.");
                    else
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            Console.WriteLine("\t\tCatalog {0}. Value {1}.", catalogIds[j], data[i][j]);
                        }
                }
            }
        }
    }
}
