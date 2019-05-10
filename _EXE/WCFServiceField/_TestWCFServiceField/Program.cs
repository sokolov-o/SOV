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

            // INCORRECT OPEN SERVICE (4SAMPLE)
            long h1 = clientF.Open("", ""); Console.WriteLine("ServiceFieldClient handle {0}.", h1);
            h1 = clientF.Open(userName, userPassword); Console.WriteLine("ServiceFieldClient handle {0}.", h1);

            #endregion

            DateTime dateIni = new DateTime(2019, 5, 4, 12, 0, 0); //DateTime.Today.AddDays(-1);
            double[] leadTimes = null;
            GeoRectangle[] geoRects = new GeoRectangle[]
               {
                    new GeoRectangle()
                    {
                        NorthWest = new _TestWCFServiceField.FieldServiceReference.GeoPoint() { LatGrd = 60, LonGrd = 110 },
                        SouthEast = new _TestWCFServiceField.FieldServiceReference.GeoPoint() { LatGrd = 30, LonGrd = 130 }
                    }
                };
            List<Catalog> catalogs;

            // GET & PRINT FORECAST DATA FROM SERVICE

            try
            {
                ////////
                //////// GET FIELDS IN REGION
                ////////
                //////catalogs = clientA.GetCatalogList(ha,
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

                //
                // GET VALUES AT POINTS
                //
                catalogs = clientA.GetCatalogList(ha,
                    new List<int>() { 332 }, // Владивосток
                    null,
                    new List<int>() { 112 }, //Method  "Ближайший узел GFS 0.25"
                    null, null, null
                    );
                int siteSysAttrTypeIdLat = 1000; // Широта
                int siteSysAttrTypeIdLon = 1001; // Долгота

                DateTime dateS = DateTime.Now;
                Console.Write("GetValuesAtPoints started at {0}...", dateS);

                Dictionary<double/*leadTime*/, double[]/*Catalog index*/> dataP = clientF.GetValuesAtPoints(hf, dateIni, leadTimes, catalogs.Select(x => x.Id).ToArray(), siteSysAttrTypeIdLat, siteSysAttrTypeIdLon);

                Console.WriteLine("ended at {0}, elapsed {1} minutes.", DateTime.Now, (int)((DateTime.Now - dateS).TotalMinutes));
                PrintDataPoints(dateIni, catalogs, dataP);
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

        static void PrintDataFields(DateTime dateIni, double[] leadTimeHours, GeoRectangle[] grs, List<Catalog> catalogs, Field[][][] data)
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
                foreach (KeyValuePair<double, double[]> item in data)
                {
                    Console.WriteLine("Lead time {0} h", item.Key);

                    if (item.Value == null)
                        Console.WriteLine("... no data.");
                    else
                        for (int j = 0; j < item.Value.Length; j++)
                        {
                            Console.WriteLine("\t\tCatalog {0}. Value {1}.", catalogs[j].Id, item.Value[j]);
                        }
                }
            }
        }
    }
}
