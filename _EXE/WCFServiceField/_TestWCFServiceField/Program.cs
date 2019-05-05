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
        static AmurServiceReference.ServiceClient clientA;
        static long ha;
        static FieldServiceReference.ServiceClient clientF;
        static long hf;

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

            // INCORRECT OPEN SERVICE
            long h1 = clientF.Open("", ""); Console.WriteLine("ServiceFieldClient handle {0}.", h1);
            h1 = clientF.Open(userName, userPassword); Console.WriteLine("ServiceFieldClient handle {0}.", h1);

            #endregion

            // GET & PRINT DATA FROM SERVICE
            try
            {
                #region GET FIELDS IN REGION

                DateTime dateIni = new DateTime(2019, 4, 30, 12, 0, 0); //DateTime.Today.AddDays(-1);
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
                    new GeoRectangle() {
                        NorthWest = new _TestWCFServiceField.FieldServiceReference.GeoPoint() { LatGrd = 60, LonGrd = 110 },
                        SouthEast = new _TestWCFServiceField.FieldServiceReference.GeoPoint() { LatGrd = 30, LonGrd = 130 } }
                };
                //double[] leadTimeHours = method.MethodForecast.LeadTimesHours;
                double[] leadTimeHours = new double[] { 0, 12, -24 };

                List<Catalog> catalogs = clientA.GetCatalogListById(ha, catalogIds.ToList());
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
                catalogs = clientA.GetCatalogList(ha,
                    new List<int>() { 332 }, // Владивосток
                    null,
                    new List<int>() { 112 }, //Method  "Ближайший узел GFS 0.25"
                    null, null, null
                    );
                catalogIds = catalogs.Select(x => x.Id).ToArray();
                int fcsSiteId = 10344; // Земной шар
                int siteSysAttrTypeIdLat = 1000; // Широта
                int siteSysAttrTypeIdLon = 1001; // Долгота

                double[/*leadTime*/][/*Catalog index*/] dataP = clientF.GetValuesAtPoints(hf, dateIni, leadTimeHours, catalogIds, siteSysAttrTypeIdLat, siteSysAttrTypeIdLon);
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
                List<Catalog> catalogs = clientA.GetCatalogListById(ha, catalogIds.ToList());
                List<Variable> variables = clientA.GetVariablesByList(ha, catalogs.Select(x => x.VariableId).ToList());

                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine("Lead time {0} h", leadTimeHours[i]);

                    if (data[i] == null)
                        Console.WriteLine("... no data.");
                    else
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            Console.WriteLine("Region {0}", grs[j].NorthWest.LatGrd + "x" + grs[j].NorthWest.LonGrd + "x" + grs[j].SouthEast.LatGrd + "x" + grs[j].SouthEast.LonGrd);
                            for (int k = 0; k < data[i][j].Length; k++)
                            {
                                Console.Write("Catalog {0} {2}. {1} field values.", catalogIds[k],
                                    data[i][j][k] == null ? 0 : data[i][j][k].Value.Length,
                                    variables.FirstOrDefault(y => y.Id == catalogs.FirstOrDefault(x => x.Id == catalogIds[k]).VariableId).NameRus);
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
