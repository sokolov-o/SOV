#define trace

using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.SGMO
{
    public partial class FcsWave
    {
        readonly static int CatalogId_WaveHMix = 1028;

        static internal void GetFcs(TrackFcs fcs)
        {


            DateTime dtIni = fcs.DataFcs0.DateIniUTC;
            Console.WriteLine("{0} downloading file", DateTime.Now);
            string fileName = DownloadFile(dtIni);
            Console.WriteLine("{0} parsing file", DateTime.Now);
            //Console.WriteLine(file.FullName);


            double minLat = fcs.PointLags.Where(p => p.Point != null).Select(p => p.Point.LatGrd).Min() - 1;
            double maxLat = fcs.PointLags.Where(p => p.Point != null).Select(p => p.Point.LatGrd).Max() + 1;
            double minLon = fcs.PointLags.Where(p => p.Point != null).Select(p => p.Point.LonGrd).Min() - 1;
            double maxLon = fcs.PointLags.Where(p => p.Point != null).Select(p => p.Point.LonGrd).Max() + 1;


            Dictionary<Geo.GeoPoint, Dictionary<DateTime, Dictionary<int, double>>> data = ReadData(fileName, minLat, maxLat, minLon, maxLon);
#if trace
            StreamWriter sw = new StreamWriter("selected.csv");
            sw.WriteLine("lon;lat");
            foreach (var gp in data.Keys)
            {
                sw.WriteLine("{0};{1}", gp.LonGrd, gp.LatGrd);
            }
            sw.Close();
#endif
            foreach (var f1 in fcs.DataFcs0.DataFcs1List.Where(f => f.Point != null))
            {

                int varId = fcs.Varoffs.Where(v => v.Id == f1.VaroffId).FirstOrDefault().VariableId;
                //FERHRI.Amur.Meta.VariableRepository vr = new Amur.Meta.VariableRepository();
                var dm = FERHRI.Amur.Meta.DataManager.GetInstance();
                var vriable = dm.VariableRepository.Select(varId);
                Console.WriteLine("point {0}  lag {1} var {2}", f1.Point, f1.Lag, varId);
                var neibs = Calc4Neighbours(f1.Point.LatGrd, f1.Point.LonGrd);
                Dictionary<Geo.GeoPoint, double> distances = new Dictionary<Geo.GeoPoint, double>();

                foreach (var p in neibs)
                {
                    distances.Add(p, Geo.Geo.SphereDistance(f1.Point.LonRadians, p.LonRadians, f1.Point.LatRaians, p.LatRaians, Geo.EnumDistanceType.TheoremCos));
                }
                Dictionary<Geo.GeoPoint, double> values = new Dictionary<Geo.GeoPoint, double>();
                foreach (var p in neibs)
                {
                    values.Add(p, -999.9);
                    if (data.ContainsKey(p))
                    {
                        if (data[p].ContainsKey(dtIni.AddHours(f1.Lag)))
                        {
                            if (data[p][dtIni.AddHours(f1.Lag)].ContainsKey(varId))
                            {
                                values[p] = data[p][dtIni.AddHours(f1.Lag)][varId];
                            }

                        }
                        else
                        {
                            Console.WriteLine("No date fcs.lag={0} fcs.varid={1} fcs.p={2}", f1.Lag, varId, f1.Point);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No point fcs.lag={0} fcs.varid={1} p={2}", f1.Lag, varId, p);
                    }

                }
                f1.Value = vriable.CodeNoData;
                foreach (var kvp in distances.OrderBy(pair => pair.Value))
                {
                    if (values[kvp.Key] != -999.9)
                    {
                        f1.Value = values[kvp.Key];
                        break;
                    }
                }

            }


        }

        private static Dictionary<Geo.GeoPoint, Dictionary<DateTime, Dictionary<int, double>>> ReadData(string fileName, double minLat, double maxLat, double minLon, double maxLon)
        {
            Dictionary<Geo.GeoPoint, Dictionary<DateTime, Dictionary<int, double>>> data = new Dictionary<Geo.GeoPoint, Dictionary<DateTime, Dictionary<int, double>>>();
            FileStream fs = File.OpenRead(fileName);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                string[] vals = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                int year = Convert.ToInt32(vals[0].Substring(0, 4));
                int month = Convert.ToInt32(vals[0].Substring(4, 2));
                int day = Convert.ToInt32(vals[0].Substring(6, 2));
                DateTime dts = new DateTime(year, month, day).AddHours(Convert.ToInt32(vals[1]));
                DateTime dta = dts.AddHours(Convert.ToInt32(vals[4]));

                double lati, longi;

                lati = Convert.ToDouble(vals[6]);
                longi = Convert.ToDouble(vals[5]);
                if (lati > minLat && lati < maxLat)
                {
                    if (longi > minLon && longi < maxLon)
                    {

                        Geo.GeoPoint gp = new Geo.GeoPoint(lati, longi);
                        if (!data.ContainsKey(gp))
                        {
                            data.Add(gp, new Dictionary<DateTime, Dictionary<int, double>>());
                        }
                        if (!data[gp].ContainsKey(dta))
                        {
                            data[gp].Add(dta, new Dictionary<int, double>());
                        }

                        data[gp][dta].Add(1028, Convert.ToDouble(vals[7])); //hmix
                        //data[gp][dta].Add(2, Convert.ToDouble(vals[8])); //t
                        //data[gp][dta].Add(3, Convert.ToDouble(vals[9])); //l
                        data[gp][dta].Add(1035, Convert.ToDouble(vals[10])); //dir

                        data[gp][dta].Add(1033, Convert.ToDouble(vals[11]));//hwind
                        data[gp][dta].Add(1026, Convert.ToDouble(vals[12]));
                        //data[gp][dta].Add(7, Convert.ToDouble(vals[13]));
                        data[gp][dta].Add(1032, Convert.ToDouble(vals[14]));

                        data[gp][dta].Add(1027, Convert.ToDouble(vals[15])); //hsw1
                        data[gp][dta].Add(1049, Convert.ToDouble(vals[16]));
                        //data[gp][dta].Add(11, Convert.ToDouble(vals[17]));
                        data[gp][dta].Add(1050, Convert.ToDouble(vals[18]));


                        data[gp][dta].Add(1111, Convert.ToDouble(vals[19])); //hsw2

                        ///период пика - период системы с наибольшей высотой
                        data[gp][dta].Add(1051, Convert.ToDouble(vals[20])); //t peak
                        if (data[gp][dta][1033] > data[gp][dta][1051])
                        {
                            data[gp][dta][1051] = Convert.ToDouble(vals[8]);
                        }

                        if (data[gp][dta][1027] > data[gp][dta][1051])
                        {
                            data[gp][dta][1051] = data[gp][dta][1049];
                        }

                        data[gp][dta].Add(1048, data[gp][dta][1028] * 2.9);

                        //data[gp][dta].Add(15, Convert.ToDouble(vals[21]));
                        //data[gp][dta].Add(16, Convert.ToDouble(vals[22]));
                    }
                }
            }
            return data;
        }

        private static string DownloadFile(DateTime dtIni)
        {
            string fileName = string.Format("WA{0:yyyyMMddHH}.dat", dtIni);
            string remoteDir = "/home/data/GFS_OPER/FERHRI/HW/New";
            string localDir = @"e:\";
            string result = string.Empty;
            bool needToDownload = true;

            var connectionInfo = new KeyboardInteractiveConnectionInfo("10.11.203.62", "gonchukov");
            connectionInfo.AuthenticationPrompt += ConnectionInfo_AuthenticationPrompt;

            using (var client = new SftpClient(connectionInfo))
            {

                client.Connect();
                try
                {
                    if (client.Exists(remoteDir + "/" + fileName))
                    {
                        var file = client.Get(remoteDir + "/" + fileName);
                        if (File.Exists(localDir + "\\" + fileName))
                        {
                            FileInfo localFile = new FileInfo(localDir + "\\" + fileName);
                            if (localFile.Length == file.Length)
                            {
                                needToDownload = false;
                            }

                        }
                        if (needToDownload)
                        {
                            FileStream fs = new FileStream(localDir + "\\" + fileName, FileMode.Create);

                            client.DownloadFile(file.FullName, fs);
                            fs.Close();
                        }
                        result = localDir + "\\" + fileName;
                        // string content = client.ReadAllText(file.FullName);
                        //file.MoveTo(localDir + "\\" + fileName);
                    }
                }
                finally
                {
                    client.Disconnect();
                }




            }
            return result;

        }

        static double LinearInt(double x1, double y1, double x2, double y2, double x)
        {
            return (x - x1) / (x2 - x1) * (y2 - y1) + y1;
        }

        static Geo.GeoPoint[] Calc4Neighbours(double lati, double longi)
        {
            Geo.GeoPoint[] result = new Geo.GeoPoint[4];

            double roundLati = Math.Round(lati * 2) / 2;
            double roundLongi = Math.Round(longi * 2) / 2;

            result[0] = new Geo.GeoPoint(roundLati, roundLongi);
            result[1] = new Geo.GeoPoint(roundLati + 0.5, roundLongi);
            result[2] = new Geo.GeoPoint(roundLati, roundLongi + 0.5);
            result[3] = new Geo.GeoPoint(roundLati + 0.5, roundLongi + 0.5);

            return result;

        }

        private static void ConnectionInfo_AuthenticationPrompt(object sender, Renci.SshNet.Common.AuthenticationPromptEventArgs e)
        {
            foreach (var prompt in e.Prompts)
            {
                if (prompt.Request.Equals("Password: ", StringComparison.InvariantCultureIgnoreCase))
                {
                    prompt.Response = "gid0ma$0n";
                }
            }
        }

    }
}
