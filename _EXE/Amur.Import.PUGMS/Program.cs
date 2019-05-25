﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amur.Import.PUGMSServiceReference;
using Amur.Import.AmurServiceReference;

namespace Amur.Import
{
    /// <summary>
    /// 
    /// Импорт данных БД ПУГМС -> БД Амур
    /// http://192.168.116.90:8033/HydroService.svc
    /// 
    /// Нотация префикса наименований в коде: 
    ///     p - Hydro DB ПУГМС , 
    ///     a - Amur DB ДВНИГМИ.
    /// 
    /// OSokolov@201709
    /// </summary>
    class Program
    {
        internal static HydroServiceClient hClient;// = new HydroServiceClient();
        internal static ServiceClient aClient = new ServiceClient();
        internal static long aHandle = aClient.Open("OSokolov", "qq");

        static void Main(string[] args)
        {
            // SOV.2019
            DateTime dateS = new DateTime(2019, 1, 1);
            ImportAMSData(13/*Пыль*/, dateS, dateS.AddHours(1));

            // UpdateCurve(); // 2017

            Console.WriteLine("Press ENTER...");
            Console.ReadLine();
        }

        private static void ImportAMSData(int siteTypeId, DateTime dateS, DateTime dateF)
        {
            DateTime dateSS = DateTime.Now;
            Console.WriteLine("ImportAMSData started at {0}", dateSS);

            // READ AMSDATA FROM PUGMS DB
            AMSDataRepository amsDb = new AMSDataRepository(Amur.Import.Properties.Settings.Default.AMSDataConnectionString);
            List<AMSData> amsDatas = amsDb.Select(AMSData.CrossSiteTypeId[siteTypeId], dateS, dateF);

            // CONVERT AMSDATA 2 AMUR DATA VALUE

            List<AmurServiceReference.Site> sites = aClient.GetSitesByType(aHandle, siteTypeId);
            Dictionary<int, int> siteUTSOffsets = new Dictionary<int, int>();
            List<AmurServiceReference.Catalog> catalogs = new List<Catalog>();

            List<AmurServiceReference.DataValue> dataAmur = new List<AmurServiceReference.DataValue>();
            foreach (AMSData amsData in amsDatas)
            {
                // GET OR CREATE SITE
                string code = amsData.StationId + "AMS";
                List<AmurServiceReference.Site> sites1 = sites.FindAll(x => x.Code == code && x.Name == amsData.StationName);
                if (sites1.Count > 1) throw new Exception("(site.Count > 1)");

                AmurServiceReference.Site site = null;
                if (sites1.Count == 0)
                {
                    site = new AmurServiceReference.Site()
                    {
                        Code = code,
                        AddrRegionId = 23, // Приморский
                        Name = amsData.StationName,
                        OrgId = 243, // Примгидромет
                        TypeId = siteTypeId,
                        Lat = amsData.Lat,
                        Lon = amsData.Lon
                    };

                    // Insert the new site

                    site.Id = aClient.SaveSite(aHandle, site);
                    aClient.SaveSiteAttribute(aHandle, new EntityAttrValue() // UTSOffset
                    { EntityId = site.Id, DateS = new DateTime(2014, 1, 1), AttrTypeId = 1003, Value = "10" });

                    sites.Add(site);
                    siteUTSOffsets.Add(site.Id, 10);
                }
                else if (sites1.Count == 1)
                {
                    site = sites1[0];
                }
                else
                    throw new Exception("(sites1.Count != 1)");

                if (!siteUTSOffsets.TryGetValue(site.Id, out int siteUTSOffset))
                {
                    siteUTSOffset = int.Parse(aClient.GetSiteAttrValue(aHandle, site.Id, 1003, DateTime.Now).Value);
                    siteUTSOffsets.Add(site.Id, siteUTSOffset);
                }

                // GET OR CREATE CATALOG
                int[] varoff = AMSData.CrossVariableId.Keys.FirstOrDefault(x => AMSData.CrossVariableId[x] == amsData.VariableId);
                AmurServiceReference.Catalog catalog = new Catalog()
                {
                    SiteId = site.Id,
                    VariableId = varoff[0],
                    MethodId = 0, // Наблюдения
                    SourceId = 243, // Примгидромет 
                    OffsetTypeId = varoff[1],
                    OffsetValue = varoff[2]

                };
                List<Catalog> catalogs1 = catalogs.FindAll(x =>
                    x.SiteId == catalog.SiteId
                    && x.VariableId == catalog.VariableId
                    && x.MethodId == catalog.MethodId
                    && x.SourceId == catalog.SourceId
                    && x.OffsetTypeId == catalog.OffsetTypeId
                    && x.OffsetValue == catalog.OffsetValue
                    );

                if (catalogs1.Count == 0)
                {
                    Catalog catalog1 = aClient.GetCatalog(aHandle, catalog.SiteId, catalog.VariableId, catalog.OffsetTypeId, catalog.MethodId, catalog.SourceId, catalog.OffsetValue);
                    if (catalog1 == null)
                        catalog = aClient.SaveCatalog(aHandle, catalog);
                    else
                        catalog = catalog1;

                    catalogs.Add(catalog);
                }
                else if (catalogs1.Count == 1)
                {
                    catalog = catalogs1[0];
                }
                else
                    throw new Exception("(catalog1.Count > 1)");

                // CREATE DATA VALUE

                dataAmur.Add(new AmurServiceReference.DataValue()
                {
                    CatalogId = catalog.Id,
                    UTCOffset = siteUTSOffset,
                    DateLOC = amsData.DateObs,
                    DateUTC = amsData.DateObs.AddHours(-siteUTSOffset),
                    FlagAQC = 0,
                    Value = amsData.Value
                }
                );
            }
            if (dataAmur.Count > 0)
                aClient.SaveDataValueList(aHandle, dataAmur, null);

            Console.WriteLine("ImportAMSData ended at {0}, {1} min elapsed.", DateTime.Now, (DateTime.Now - dateSS).TotalMinutes);
        }

        static void UpdateCurve()
        {
            //////AmurServiceReference.Curve acurve = Program.aClient.GetCurveByCatalog(Program.aHandle, 1191420, 1191421,
            //////    new List<DateTime> { DateTime.Parse("9.08.2016"), DateTime.Parse("11.08.2016") });

            //var site = hClient.GetSite("5160", 2, false);
            //Curve curve = hClient.GetCurve(site.SiteId, 2, 14);

            // Получить П-сайты, имеющие кривые.
            Dictionary<PUGMSServiceReference.Site, PUGMSServiceReference.Curve> psitesCurves = PUGMSDB.RepositoryPUGMS.GetSitesCurves();

            // Получить соответствующие им А-сайты
            Dictionary<AmurServiceReference.Site, PUGMSServiceReference.Curve> asitesCurves = AMURDB.RepositoryAmur.GetSitesCurves(psitesCurves);

            // Обновить кривые БД Амур
            AMURDB.RepositoryAmur.InsertUpdateDbAmur(asitesCurves);
        }
    }
}
