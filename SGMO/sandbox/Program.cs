
using FERHRI.Amur.Meta;
using FERHRI.SGMO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FERHRI.SGMO.WaveProb
{
    class Program
    {
        static int waveHgtId = 1028, waveDirId = 1035, windSpeedId = 1030, windDirId = 1029;
        static void Main(string[] args)
        {
            DateTime dt = new DateTime(2017, 06, 28, 12, 0, 0);

            string oldpath = Environment.GetEnvironmentVariable("path");
            RegisterGDAL(oldpath);

           // var dm = FERHRI.Amur.Meta.DataManager.GetInstance();
            

            ///pile_id->var_id 1d probs
            Dictionary<int, int> pilesToVars = new Dictionary<int, int>();
            pilesToVars.Add(1, waveHgtId);
            pilesToVars.Add(2, waveDirId);
            pilesToVars.Add(5, waveHgtId);

            //2d probs
            Dictionary<int, int> pileToPile = new Dictionary<int, int>();
            pileToPile.Add(1, 2);


            var data = ReadData(string.Format(@"E:\WA{0:yyyyMMddHH}.dat", dt), 35, 55, 127, 145);
            Dictionary<string, List<GeoPoint>> map = GetMap(data, @"C:\Users\gonchukov.ODPP\Documents\ArcGIS\japanSea.shp");


            //regname->varid->ctlid
            Dictionary<string, Dictionary<int, int>> regionVarCtl = FillCtls( map.Keys.ToArray());
            


            // pilesetid->regname->pile
            Dictionary<int, Dictionary<string, Prob1D>> res1day = Get1dProbForOneDay(dt, pilesToVars, data, map, regionVarCtl);

            var sgmoDm = FERHRI.SGMO.DataManager.GetInstance();
            foreach (int pilesetid in res1day.Keys)
            {
                foreach (string regname in res1day[pilesetid].Keys)
                {
                    sgmoDm.Prob1DRepository.Delete(res1day[pilesetid][regname].CatalogId, res1day[pilesetid][regname].UnitIdTime, res1day[pilesetid][regname].NameRus);
                    sgmoDm.Prob1DRepository.Insert(new Prob1D[] { res1day[pilesetid][regname] }.ToList());
                }
            }

            Dictionary<int, Dictionary<string, Prob1D>> res1dec = Get1dProbForDecade(new DateTime(2017, 06, 01));

           
            foreach (int pilesetid in res1dec.Keys)
            {
                foreach (string regname in res1dec[pilesetid].Keys)
                {
                    sgmoDm.Prob1DRepository.Delete(res1dec[pilesetid][regname].CatalogId, res1dec[pilesetid][regname].UnitIdTime, res1dec[pilesetid][regname].NameRus);
                    sgmoDm.Prob1DRepository.Insert(new Prob1D[] { res1dec[pilesetid][regname] }.ToList());
                }
            }



            Environment.SetEnvironmentVariable("path", oldpath);
        }

        private static Dictionary<int, Dictionary<string, Prob1D>> Get1dProbForDecade(DateTime dtStart)
        {
            Dictionary<int, Dictionary<string, Prob1D>> res = new Dictionary<int, Dictionary<string, Prob1D>>();
            List<Dictionary<int, Dictionary<string, Prob1D>>> heap = new List<Dictionary<int, Dictionary<string, Prob1D>>>();

            var data = ReadData(string.Format(@"E:\WA{0:yyyyMMddHH}.dat", dtStart), 35, 55, 127, 145);

            Dictionary<int, int> pilesToVars = new Dictionary<int, int>();
            pilesToVars.Add(1, waveHgtId);
            pilesToVars.Add(2, waveDirId);
            pilesToVars.Add(5, waveHgtId);

            
            Dictionary<string, List<GeoPoint>> map = GetMap(data, @"C:\Users\gonchukov.ODPP\Documents\ArcGIS\japanSea.shp");
            Dictionary<string, Dictionary<int, int>> regionVarCtl = FillCtls(map.Keys.ToArray());
            heap.Add(Get1dProbForOneDay(dtStart, pilesToVars, data, map, regionVarCtl));
            if (dtStart.Day < 20)
            {
                for(int i=1;i<10;i++)
                {
                    var dataDay = ReadData(string.Format(@"E:\WA{0:yyyyMMddHH}.dat", dtStart.AddDays(i)), 35, 55, 127, 145);
                    var mapdt = GetMap(dataDay, @"C:\Users\gonchukov.ODPP\Documents\ArcGIS\japanSea.shp");
                    heap.Add(Get1dProbForOneDay(dtStart.AddDays(i), pilesToVars, dataDay, mapdt, regionVarCtl));
                }
            }
            else
            {
                for(DateTime dt = dtStart.AddDays(1);dt.Month==dtStart.Month;dt=dt.AddDays(1))
                {
                    var dataDay = ReadData(string.Format(@"E:\WA{0:yyyyMMddHH}.dat", dt), 35, 55, 127, 145);
                    var mapdt = GetMap(dataDay, @"C:\Users\gonchukov.ODPP\Documents\ArcGIS\japanSea.shp");
                    heap.Add(Get1dProbForOneDay(dt, pilesToVars, dataDay, mapdt, regionVarCtl));
                }
            }

            foreach (var item in heap)
            {
                foreach (var pilesetid in item.Keys)
                {
                    if (!res.ContainsKey(pilesetid))
                    {
                        res.Add(pilesetid, new Dictionary<string, Prob1D>());
                    }
                    foreach(string regname in item[pilesetid].Keys)
                    {
                        if(!res[pilesetid].ContainsKey(regname))
                        {
                            res[pilesetid].Add(regname, new Prob1D());
                            res[pilesetid][regname].CatalogId = item[pilesetid][regname].CatalogId;
                            res[pilesetid][regname].CountTotal = item[pilesetid][regname].CountTotal;
                            res[pilesetid][regname].NameEng = item[pilesetid][regname].NameEng;
                            res[pilesetid][regname].NameRus = item[pilesetid][regname].NameRus;
                            res[pilesetid][regname].UnitIdTime = 318;
                            foreach(var prob1item in item[pilesetid][regname].Prob1DItems)
                            {
                                res[pilesetid][regname].Prob1DItems.Add(new Prob1D.Prob1DItem() { CountInPile = prob1item.CountInPile, Date=prob1item.Date, PileId=prob1item.PileId });
                            }
                        }
                        else
                        {
                            res[pilesetid][regname].CountTotal += item[pilesetid][regname].CountTotal;
                            foreach (var prob1item in item[pilesetid][regname].Prob1DItems)
                            {
                                var respr1item = res[pilesetid][regname].Prob1DItems.Where(pr => pr.PileId == prob1item.PileId).FirstOrDefault();
                                respr1item.CountInPile += prob1item.CountInPile;
                            }
                        }

                       
                    }
                }
            }
            return res;
        }

        private static Dictionary<string, Dictionary<int, int>> FillCtls(string[] regnames)
        {
            var dm = FERHRI.Amur.Meta.DataManager.GetInstance();
            Dictionary<string, Dictionary<int, int>> regionVarCtl = new Dictionary<string, Dictionary<int, int>>();
            foreach (string regName in regnames)
            {
                regionVarCtl.Add(regName, new Dictionary<int, int>());
                var station = dm.StationRepository.Select(regName);
                var site = dm.SiteRepository.SelectByStations(new int[] { station.Id }.ToList());
                //var ctl = dm.CatalogRepository.Select(site[0].Id, 1028, 109, 777, 0, 0);
                regionVarCtl[regName].Add(1028, dm.CatalogRepository.Select(site[0].Id, 1028, 109, 777, 0, 0).Id);
                regionVarCtl[regName].Add(1035, dm.CatalogRepository.Select(site[0].Id, 1035, 109, 777, 0, 0).Id);
            }

            return regionVarCtl;
        }

        private static Dictionary<int, Dictionary<string, Prob1D>> Get1dProbForOneDay(DateTime dt, Dictionary<int, int> pilesToVars, Dictionary<GeoPoint, Dictionary<int, double>> data, Dictionary<string, List<GeoPoint>> map, Dictionary<string, Dictionary<int, int>> regionVarCtl)
        {
            Dictionary<int, Dictionary<string, Prob1D>> res = new Dictionary<int, Dictionary<string, Prob1D>>();
            var dm = FERHRI.Amur.Meta.DataManager.GetInstance();
            
            var pilesets = dm.PileRepository.SelectPiles(pilesToVars.Keys.ToList());

            foreach (int pilesetId in pilesToVars.Keys)
            {
                if (!res.ContainsKey(pilesetId))
                {
                    res.Add(pilesetId, new Dictionary<string, Prob1D>());
                }
                var pileset = pilesets.Where(p => p.Id == pilesetId).FirstOrDefault();
                int varid = pilesToVars[pilesetId];
                foreach (string regname in map.Keys)
                {
                    if (!res[pilesetId].ContainsKey(regname))
                    {
                        res[pilesetId].Add(regname, new Prob1D());

                    }

                    foreach (var pile in pileset.Piles)
                    {
                        res[pilesetId][regname].Prob1DItems.Add(new Prob1D.Prob1DItem() { Date = dt, PileId = pile.Id, CountInPile = 0 });
                    }
                    foreach (var p in map[regname])
                    {
                        res[pilesetId][regname].CatalogId = regionVarCtl[regname][varid];
                        res[pilesetId][regname].CountTotal++;
                        res[pilesetId][regname].UnitIdTime = 104;
                        res[pilesetId][regname].NameEng = pilesets.Where(ps => ps.Id == pilesetId).FirstOrDefault().NameEng;
                        res[pilesetId][regname].NameRus = pilesets.Where(ps => ps.Id == pilesetId).FirstOrDefault().NameRus;
                        foreach (var pile in pileset.Piles)
                        {
                            if (data[p][varid] > pile.Value1 && data[p][varid] <= pile.Value2)
                            {
                                var probe1dItem = res[pilesetId][regname].Prob1DItems.Where(pi => pi.PileId == pile.Id).FirstOrDefault();
                                probe1dItem.CountInPile++;
                            }
                        }
                    }

                }
            }

            return res;
        }

        private static Dictionary<string, List<GeoPoint>>  GetMap(Dictionary<GeoPoint, Dictionary<int, double>> data, string shapePath )
        {
            var dm = FERHRI.Amur.Meta.DataManager.GetInstance();

            Dictionary<string, OSGeo.OGR.Geometry> regions = ReadShape(shapePath);
            Dictionary<string, List<GeoPoint>>  map = new Dictionary<string, List<GeoPoint>>();
            foreach (var p in data.Keys)
            {
                var point = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbPoint);
                point.AddPoint_2D(p.Longi - 0.001, p.Lati - 0.001);
                foreach (string regName in regions.Keys)
                {
                    if (regions[regName].Intersects(point))
                    {
                        if (!map.ContainsKey(regName))
                        {
                            map.Add(regName, new List<GeoPoint>());
                        }
                        map[regName].Add(p);
                        break;
                    }
                }
            }
            return map;
        }

        private static void RegisterGDAL(string oldpath)
        {
            string path = oldpath + @";e:\bin\gdal\bin\;e:\bin\gdal\bin\gdal\csharp\;";
            Environment.SetEnvironmentVariable("path", path);
            OSGeo.OGR.Ogr.RegisterAll();
        }

        private static Dictionary<string, OSGeo.OGR.Geometry> ReadShape(string path)
        {
            Dictionary<string, OSGeo.OGR.Geometry> regions = new Dictionary<string, OSGeo.OGR.Geometry>();

            var ds = OSGeo.OGR.Ogr.Open(path, 0);
            int cnt = ds.GetLayerCount();
            for (int i = 0; i < cnt; i++)
            {
                var layer = ds.GetLayerByIndex(i);

                int featuresCnt = layer.GetFeatureCount(1);
                for (int f = 0; f < featuresCnt; f++)
                {
                    var feature = layer.GetFeature(f);
                    Console.WriteLine("name_en {0}", feature.GetFieldAsString("NAME_EN"));
                    int fieldCnt = feature.GetFieldCount();

                    var geo = feature.GetGeometryRef();

                    regions.Add(feature.GetFieldAsString("NAME_EN"), geo);


                }


            }

            return regions;
        }

        private static Dictionary<GeoPoint, Dictionary<int, double>> ReadData(string fileName, double minLat=-90, double maxLat=90, double minLon=-180, double maxLon=180)
        {
            Dictionary<GeoPoint, Dictionary<int, double>> data = new Dictionary<GeoPoint, Dictionary<int, double>>();
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
                if (dts == dta)
                {

                    double lati, longi;

                    lati = Convert.ToDouble(vals[6]);
                    longi = Convert.ToDouble(vals[5]);
                    if (lati > minLat && lati < maxLat)
                    {
                        if (longi > minLon && longi < maxLon)
                        {

                            GeoPoint gp = new GeoPoint(lati, longi);
                            if (!data.ContainsKey(gp))
                            {
                                data.Add(gp, new Dictionary<int, double>());
                            }
                            

                            data[gp].Add(1028, Convert.ToDouble(vals[7])); //hmix
                                                                                //data[gp][dta].Add(2, Convert.ToDouble(vals[8])); //t
                                                                                //data[gp][dta].Add(3, Convert.ToDouble(vals[9])); //l
                            data[gp].Add(1035, Convert.ToDouble(vals[10])); //dir

                            //data[gp][dta].Add(1033, Convert.ToDouble(vals[11]));//hwind
                            //data[gp][dta].Add(1026, Convert.ToDouble(vals[12]));
                            ////data[gp][dta].Add(7, Convert.ToDouble(vals[13]));
                            //data[gp][dta].Add(1032, Convert.ToDouble(vals[14]));

                            //data[gp][dta].Add(1027, Convert.ToDouble(vals[15])); //hsw1
                            //data[gp][dta].Add(1049, Convert.ToDouble(vals[16]));
                            ////data[gp][dta].Add(11, Convert.ToDouble(vals[17]));
                            //data[gp][dta].Add(1050, Convert.ToDouble(vals[18]));


                            //data[gp][dta].Add(1111, Convert.ToDouble(vals[19])); //hsw2

                            /////период пика - период системы с наибольшей высотой
                            //data[gp][dta].Add(1051, Convert.ToDouble(vals[20])); //t peak
                            //if (data[gp][dta][1033] > data[gp][dta][1051])
                            //{
                            //    data[gp][dta][1051] = Convert.ToDouble(vals[8]);
                            //}

                            //if (data[gp][dta][1027] > data[gp][dta][1051])
                            //{
                            //    data[gp][dta][1051] = data[gp][dta][1049];
                            //}

                            //data[gp][dta].Add(1048, data[gp][dta][1028] * 2.9);

                            data[gp].Add(1030, Convert.ToDouble(vals[23])); //wind speed
                            data[gp].Add(1029, Convert.ToDouble(vals[24])); //wind direction
                        }
                    }
                }
            }
            return data;
        }
    }
}
