using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.SGMO;
using FERHRI.Geo;
using FERHRI.Amur.Meta;
using System.IO;

namespace FERHRI.SGMO
{
    class Program
    {
        static string ConnectionStringSGMO = "Server=10.11.203.20;Port=5432;User Id=postgres;Password=qq;Database=sgmo;";
        static string ConnectionStringTask = "Server=10.11.203.20;Port=5432;User Id=postgres;Password=qq;Database=ferhri.task;";
        static string ConnectionStringAmur = "Server=10.11.203.20;Port=5432;User Id=postgres;Password=qq;Database=ferhri.amur;";
       


        static void Main(string[] args)
        {
            // SET CONNECTIONS
            FERHRI.SGMO.DataManager.SetDefaultConnectionString(ConnectionStringSGMO);
            FERHRI.Amur.Meta.DataManager.SetDefaultConnectionString(ConnectionStringAmur);
            FERHRI.Task.DataManager.ConnectionStringDefault = ConnectionStringTask;
           
            bool isDeletePreviousFcs = FERHRI.SGMO.Settings.Default.isDeletePreviousFcs;


            System.Timers.Timer timer = new System.Timers.Timer(10 * 60 * 1000);
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
           // ProcessForecastSourcesFromIssues(isDeletePreviousFcs);
           

            //try
            //{
            //    // SET FCS PARAMETERS
            //   // ProcessForecastSourcesFromSettings(isDeletePreviousFcs);
            //    ProcessForecastSourcesFromIssues(isDeletePreviousFcs);
            //}
            //catch (SgmoException ex)
            //{
            //    Console.WriteLine(ex.Message + "\n\n" + ex.ToString());
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("****** " + ex.Message + "\n\n" + ex.ToString());
            //}
            // EXIT
            Console.WriteLine("\n\nPress ENTER...");
            Console.ReadLine();
        
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = sender as System.Timers.Timer;
            timer.Enabled = false;
            Console.WriteLine("{0} timer started", DateTime.Now);
            bool isDeletePreviousFcs = FERHRI.SGMO.Settings.Default.isDeletePreviousFcs;
            try
            {
                // SET FCS PARAMETERS
                // ProcessForecastSourcesFromSettings(isDeletePreviousFcs);
                ProcessForecastSourcesFromIssues(isDeletePreviousFcs);
            }
            catch (SgmoException ex)
            {
                Console.WriteLine(ex.Message + "\n\n" + ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("****** " + ex.Message + "\n\n" + ex.ToString());
            }
            Console.WriteLine("{0} timer ended", DateTime.Now);
            timer.Enabled = true;
        }

        private static void ProcessForecastSourcesFromSettings(bool isDeletePreviousFcs)
        {
            Dictionary<int/*Track*/, int[/*Forecasting methods 4 track*/]> tracksAndMethods = new Dictionary<int, int[]>();
            tracksAndMethods = GetTracksAndMethods();

            //DateTime fcsDateIni = new DateTime(2017, 4, 10); //TrackFcs.GetCurFcsDateIni(methodId);


            // GET FCS
            foreach (KeyValuePair<int, int[]> kvp in tracksAndMethods)
            {

                foreach (int methodId in kvp.Value)
                {
                    DateTime fcsDateIni = TrackFcs.GetCurFcsDateIni(methodId);
                    fcsDateIni = new DateTime(2017, 4, 12);
                    Track0 track = FERHRI.SGMO.DataManager.GetInstance().TrackRepository.SelectTrack0(kvp.Key, true);
                    TrackFcs fcs = new TrackFcs(fcsDateIni, methodId, track, isDeletePreviousFcs);
                    fcs.GetForecast(FERHRI.SGMO.Settings.Default.gfsDxDy);
                    Console.WriteLine("Nan fcs = {0} of {1}", fcs.DataFcs0.DataFcs1List.Where(f => double.IsNaN(f.Value)).Count(), fcs.DataFcs0.DataFcs1List.Count);
                    fcs.WriteForecast();
                }
            }
        }

        private static void ProcessForecastSourcesFromIssues(bool isDeletePreviousFcs)
        {
            var dm = Task.DataManager.GetInstance();
            var tasks = dm.TaskRepository.SelectChilds(21).Where(t => t.TaskType.Id == 2).ToList();

            var issues = dm.IssueRepository.Select().Where(i => i.DateIni > DateTime.Now.AddDays(-7)).ToList();

            foreach (var issue in issues)
            {
                var issueDocs = dm.IssueDocRepository.SelectByIssueId(issue.Id).ToList();
                foreach (var issueDoc in issueDocs.Where(i => !i.DateIssue.HasValue))
                {
                    if (issueDoc.TaskDocGeoob != null)
                    {
                        if (issueDoc.TaskDocGeoob.TaskDoc != null)
                        {
                            if (issueDoc.TaskDocGeoob.TaskDoc.Doc != null)
                            {
                                if (issueDoc.TaskDocGeoob.TaskDoc.Doc.DocTypeId == 12)
                                {
                                    var catalogList = dm.TaskDocGeoobCatalogRepository.Select(issueDoc.TaskDocGeoob.Id).Where(c => c.CatalogTypeId == 1).Select(c => c.CatalogId).ToList();
                                    var ctls = FERHRI.Amur.Meta.DataManager.GetInstance().CatalogRepository.Select(catalogList).GroupBy(c => c.MethodId);
                                    foreach (var methodGroup in ctls)
                                    {
                                        int methodId = methodGroup.Key;
                                        if ( methodId == 105 || methodId==102)
                                        {
                                            DateTime fcsDateIni = issue.DateIni;
                                            Console.WriteLine("methodId={0} dtIni={1}", methodId, fcsDateIni);
                                            Track0 track = FERHRI.SGMO.DataManager.GetInstance().TrackRepository.SelectTrack0(issueDoc.TaskDocGeoob.Geoob.Id, true);

                                            try
                                            {
                                                TrackFcs fcs = new TrackFcs(fcsDateIni, methodId, track, isDeletePreviousFcs);
                                                fcs.GetForecast(FERHRI.SGMO.Settings.Default.gfsDxDy);
                                                int nans = fcs.DataFcs0.DataFcs1List.Where(f => double.IsNaN(f.Value)).Count();
                                                int nPoints = fcs.PointLags.Count;
                                                int nNullPoints = fcs.PointLags.Where(p => p.Point == null).Count();
                                                int nAcceptableNans = nPoints * nNullPoints * fcs.Varoffs.Count;
                                                int alls = fcs.DataFcs0.DataFcs1List.Count;
                                                Console.WriteLine("Nan fcs = {0} of {1}", nans, alls);
                                                Console.WriteLine("AccpetableNan fcs = {0} of {1}", nAcceptableNans, alls);
                                                if ((nans - nAcceptableNans) < (alls / 4) || (DateTime.UtcNow-fcsDateIni).TotalHours > 36)
                                                {
                                                    Console.WriteLine("Save forecasts");
                                                    fcs.WriteForecast();
                                                    issueDoc.DateIssue = DateTime.UtcNow;
                                                    var dic = new Dictionary<int, DateTime>();
                                                    dic.Add(issueDoc.Id, DateTime.UtcNow);
                                                    dm.IssueDocRepository.UpdateDateIssue(dic);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Do not save forecasts");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("****** " + ex.Message + "\n\n");
                                                continue;
                                            }
                                        }
                                        
                                    }


                                }
                            }
                        }
                    }
                }
            }
        }

        private static Dictionary<int, int[]> GetTracksAndMethods()
        {
            Dictionary<int/*Track*/, int[/*Forecasting methods 4 track*/]> ret = new Dictionary<int, int[]>();
            foreach (var item in FERHRI.SGMO.Settings.Default.TracksAndMethods.Split(new char[] { ';' }))
            {
                if (string.IsNullOrEmpty(item)) continue;

                string[] s = item.Split(new char[] { '/' });

                int[] methodIds = new int[s.Length - 1];
                for (int i = 1; i < s.Length; i++)
                {
                    methodIds[i - 1] = int.Parse(s[i]);
                }
                ret.Add(int.Parse(s[0]), methodIds);
            }
            return ret;
        }

        private static Dictionary<int, int[]> GetTracksAndMethods(string settingString)
        {
            Dictionary<int/*Track*/, int[/*Forecasting methods 4 track*/]> ret = new Dictionary<int, int[]>();
            foreach (var item in settingString.Split(new char[] { ';' }))
            {
                if (string.IsNullOrEmpty(item)) continue;

                string[] s = item.Split(new char[] { '/' });

                int[] methodIds = new int[s.Length - 1];
                for (int i = 1; i < s.Length; i++)
                {
                    methodIds[i - 1] = int.Parse(s[i]);
                }
                ret.Add(int.Parse(s[0]), methodIds);
            }
            return ret;
        }
    }
}
