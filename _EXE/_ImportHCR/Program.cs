using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportHCR.AmurServiceReference;
using FERHRI.Sakura.Data;
using FERHRI.Sakura.Meta;

namespace ImportHCR
{
    /// <summary>
    /// Импорт данных HCR -> Amur: чтение, преобразование, запись.
    /// Всё в памяти, большие объёмы.
    /// 
    /// X или x в наименованиях переменных и методов означает кросс-словари между HCR & AMUR
    /// 
    /// </summary>
    class Program
    {
        #region >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        const int _HCR_DBLIST_ID = 166;
        const int _HMD_STATIONREGION_ID = 62/*Test SOV*/;//61/*8 ст. ПК Гарцман*/;
        const string _DATE_S = "01.01.2000";
        const string _DATE_F = "10.01.2000 23:59:59";

        const int _HMD_PREDICT_TIME = 0;
        const int _HMD_ISCLIMATE = 0;
        const int _HMD_CTL_UNIQATTR = 0;

        const int _AMUR_METHOD_ID = (int)FERHRI.Amur.Meta.EnumMethod.ObservationInSitu;
        const int _AMUR_SOURCE_ID = 778; // Test source

        const string _HMDIC_ConnectionString = "Data Source=192.168.203.165;Initial Catalog=hmdic;Integrated Security=True";
        /// <summary>
        /// Соответствие параметра Sakura переменной Amur.
        /// </summary>
        static Dictionary<
            int[/*HCR paramId, timeId, spaceId, actionId, gridId,  formatId*/],
            int[/*Amur variable; Is accept utcOffset to loctime*/]> _x_params
            = new Dictionary<int[], int[]>()
        {
            /*          paramId, timeId, spaceId, actionId, gridId,  formatId*/
            // Температура воздуха, сут (ср., мин, макс)
            {new int[6]{3,       19,     5,      5,        70006,  13}, new int []{1015,0}},
            {new int[6]{3,       19,     5,      75,       70006,  13}, new int []{1007,0}},
            {new int[6]{3,       19,     5,      76,       70006,  13}, new int []{1008,0}},
            // Температура воздуха, 8ср
            {new int[6]{3,       59,     5,      31,       70006,  13}, new int []{10,1}}
        };
        static Dictionary<int/*HCR level*/, int/*Amur offset*/> _x_levels = new Dictionary<int, int>() { { 1, 0 } };

        static AmurServiceReference.ServiceClient _client;
        static long _hSvc;
        static FERHRI.Sakura.Meta.DataManager _hmdDM;

        #endregion <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        static void Main(string[] args)
        {
            FERHRI.Sakura.Meta.DataManager.ConnectionStringDefault = _HMDIC_ConnectionString;
            _client = new AmurServiceReference.ServiceClient();
            _hSvc = _client.Open("OSokolov", "qq");

            Run(string.IsNullOrEmpty(_DATE_S) ? null : (DateTime?)DateTime.Parse(_DATE_S),
                string.IsNullOrEmpty(_DATE_S) ? null : (DateTime?)DateTime.Parse(_DATE_F));
        }

        internal static void Run(DateTime? dateMeteoS, DateTime? dateMeteoF)
        //, int hcrStationRegionId, int[] hcrParameters,            int? hcrDbListIdSrc = null,            int amurSourceId = 3 // ВНИИГМИ-МЦД (Amur.VVO dics)
        {
            #region DECLARE
            // Amur Sites
            Dictionary<Site, double/*utc_offset*/> a_sites = new Dictionary<Site, double>();
            // Amur Stations
            List<AmurServiceReference.Station> a_stations = new List<AmurServiceReference.Station>();

            _hmdDM = FERHRI.Sakura.Meta.DataManager.Instance;
            #endregion DECLARE

            try
            {
                // GET SAKURA GEOREGS
                List<FERHRI.Sakura.Meta.Station> hmdSations = _hmdDM.StationRepository.SelectStations4Region(_HMD_STATIONREGION_ID);
                // TODO: Не верно, нужны georeg.id 
                List<FERHRI.Sakura.Meta.GeoRectangle> hmdGRs = _hmdDM.GeoRegRepository.SelectByNameList(hmdSations.Select(x => x.Index.ToString()).ToList());
                Console.WriteLine("{0} stations & {1} georegions readed.", hmdSations.Count, hmdGRs.Count);

                // GET HMD & AMUR CATALOGS
                List<FERHRI.Sakura.Meta.Catalog> hmdCatalogs = GetSakuraCatalogs(hmdGRs);
                List<object[]> amurCatalogs = GetAmurCatalogs(hmdCatalogs, hmdGRs);
                if (amurCatalogs.Count == 0) throw new Exception("x_catalogs.Count == 0");

                // GET AMUR UTC OFFSETS
                List<AmurServiceReference.EntityAttrValue> utcOffsets = new List<AmurServiceReference.EntityAttrValue>(
                    _client.GetSitesAttrValues(_hSvc, amurCatalogs.Select(x => ((AmurServiceReference.Catalog)x[0]).SiteId).ToArray(), new int[] { 1003 }, null));

                // GET AMUR VARIABLES
                List<Variable> amurVars = _client.GetVariablesByList(_hSvc, amurCatalogs.Select(x => ((AmurServiceReference.Catalog)x[0]).VariableId).Distinct().ToArray()).ToList();

                // READ HCR DATA

                FERHRI.Sakura.Data.HCRRepository hcrRep = (FERHRI.Sakura.Data.HCRRepository)FERHRI.Sakura.Data.DataManager.GetRepository(_HCR_DBLIST_ID);
                List<DataHCR> dataHCRs = hcrRep.Select(hmdCatalogs.Select(x => x.Id).ToList(), dateMeteoS, dateMeteoF, null, true, false, false);

                // SCAN DATA ITEMS
                DataValue[] amurData = new DataValue[dataHCRs.Count];
                int i = 0;
                foreach (DataHCR hcrData in dataHCRs)
                {
                    #region CONVERT DATA HCR -> AMUR

                    // GET DATES: UTC & LOC
                    int j = hmdCatalogs.IndexOf(hmdCatalogs.FirstOrDefault(x => x.Id == hcrData.CatalogId));
                    FERHRI.Amur.Meta.Catalog amurCatalog = (FERHRI.Amur.Meta.Catalog)amurCatalogs[j][0];
                    bool isAcceptUTCOffset2LOC = (bool)amurCatalogs[j][1];
                    DateTime evDate = (DateTime)utcOffsets.FindAll(x => x.DateS <= hcrData.Date && x.EntityId == amurCatalog.SiteId).Max(x => x.DateS);
                    float utcOffset = float.Parse(utcOffsets.First(x => x.DateS == evDate && x.EntityId == amurCatalog.SiteId).Value);

                    // CREATE AMUR DATA
                    amurData[i++] = new DataValue()
                    {
                        Id = -1,
                        CatalogId = amurCatalog.Id,
                        UTCOffset = utcOffset,
                        DateUTC = hcrData.Date,
                        DateLOC = isAcceptUTCOffset2LOC ? hcrData.Date.AddHours(utcOffset) : hcrData.Date,
                        FlagAQC =
                              hcrData.QCL == Enums.QCL.BAD_AUTO || hcrData.QCL == Enums.QCL.BAD_SYNOP ? (byte)FERHRI.Amur.Meta.EnumFlagAQC.Error
                            : hcrData.QCL == Enums.QCL.GOOD_AUTO || hcrData.QCL == Enums.QCL.GOOD_SYNOP ? (byte)FERHRI.Amur.Meta.EnumFlagAQC.Success
                            : (byte)FERHRI.Amur.Meta.EnumFlagAQC.NoAQC,
                        Value = hcrData.Value
                    };
                    #endregion CONVERT DATA HCR -> AMUR
                }

                _client.SaveDataValueList(_hSvc, amurData, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("\n\nPRESS ENTER...");
            Console.ReadLine();
        }
        /// <summary>
        /// Получить список записей каталога исходной БД HCR.
        /// </summary>
        /// <returns></returns>
        private static List<FERHRI.Sakura.Meta.Catalog> GetSakuraCatalogs(List<FERHRI.Sakura.Meta.GeoRectangle> grs)
        {
            Console.WriteLine("GetHCRCatalogs...");
            List<FERHRI.Sakura.Meta.Catalog> ret = new List<FERHRI.Sakura.Meta.Catalog>();

            // READ CATALOGS 
            //DbHmdCatalog hmdCatalogDb = new DbHmdCatalog(
            //    _dbHmdic.getDbAttrValue((int)Viaware.Sakura.Enums.DbList.Hmdic,
            //    (int)Viaware.Sakura.Enums.TaskAttr.SRC_CNN_MSSQL)
            //    );
            for (int iParam = 0; iParam < _x_params.Count; iParam++)
            {
                // READ CATALOGS FOR ALL STATIONS

                /* 0 paramId, 1 timeId, 2 spaceId, 3 actionId, 4 gridId, 5 formatId*/
                int? paramId = _x_params.Keys.ElementAt(iParam)[0];
                int? timeId = _x_params.Keys.ElementAt(iParam)[1];
                int? spaceId = _x_params.Keys.ElementAt(iParam)[2];
                int? actionId = _x_params.Keys.ElementAt(iParam)[3];
                int? gridId = _x_params.Keys.ElementAt(iParam)[4];
                int? formatId = _x_params.Keys.ElementAt(iParam)[5];
                int? centerId = null;
                int? georegId = null;
                int? levelTypeId = null;
                int? levelValue = null;
                string actionSub = null;
                int? gridTypeId = null;

                List<FERHRI.Sakura.Meta.Catalog> ctls =
                    _hmdDM.CatalogRepository.Select(_HCR_DBLIST_ID,
                        paramId,// == -1 ? null : paramId,
                        levelTypeId,// == -1 ? null : levelTypeId,
                        actionId,// == -1 ? null : actionId,
                        spaceId,// == -1 ? null : spaceId,
                        georegId,// == -1 ? null : georegId,
                        centerId,// == -1 ? null : centerId,
                        formatId,// == -1 ? null : formatId,
                        timeId,// == -1 ? null : timeId,
                        levelValue,
                        actionSub,
                        _HMD_PREDICT_TIME,
                        _HMD_ISCLIMATE,
                        gridId,//== -1 ? null : gridId,
                        _HMD_CTL_UNIQATTR
                    );
                Console.WriteLine("\t{0} catalogs readed.", ctls == null ? 0 : ctls.Count);

                // SELECT STATIONS & LEVELTYPES
                ctls = ctls.Where(x => grs.Exists(y => y.Id == x.GeoRegId)).ToList();
                Console.WriteLine("\t\t{0} after the stations exluded.", ctls.Count);
                ctls = ctls.Where(x => _x_levels.Keys.ToList().Exists(y => y == x.LevelTypeId)).ToList();
                Console.WriteLine("\t\t{0} after the levels exluded.", ctls.Count);

                ret.AddRange(ctls);
            }
            Console.WriteLine("\t{0} catalogs total.", ret.Count);

            return ret;
        }
        /// <summary>
        /// Получить записи каталога БД Амур, соответствующие
        /// записям каталога БД Hmdic (Sakura)
        /// </summary>
        /// <param name="hmdCatalogs"></param>
        /// <returns></returns>
        private static List<object[/*ImportHCR.AmurServiceReference.Catalog;IsAcceptUTCOffset4LOC*/]> GetAmurCatalogs
            (List<FERHRI.Sakura.Meta.Catalog> hmdCatalogs, List<FERHRI.Sakura.Meta.GeoRectangle> hmdGRs)
        {
            Console.WriteLine("GetAmurCatalogs...");
            List<object[]> ret = new List<object[]>();
            // GET SITES

            Dictionary<int, int> x_grId_siteId = new Dictionary<int, int>();
            foreach (var gr in hmdGRs)
            {
                ImportHCR.AmurServiceReference.Site[] sites = _client.GetSitesByStation(_hSvc,
                    _client.GetStationByIndex(_hSvc, gr.Name).Id,
                    (int)FERHRI.Amur.Meta.EnumStationType.MeteoStation);
                if (sites.Length != 1)
                    throw new Exception("Не найден пункт БД АМУР для станции " + gr.Name + " или количество пунктов " + sites.Length + " более 1.");

                x_grId_siteId.Add(gr.Id, sites[0].Id);
            }

            // GET CATALOGS

            foreach (var c in hmdCatalogs)
            {
                // GET VARIABLE & IsAcceptUTCOffset4LOC
                int varId = 0;
                bool IsAcceptUTCOffset4LOC = false;
                /* 0 paramId, 1 timeId, 2 spaceId, 3 actionId, 4 gridId, 5 formatId*/
                foreach (var kvp in _x_params)
                {
                    if (kvp.Key[0] == c.ParamId && kvp.Key[1] == c.TimeId
                        && kvp.Key[2] == c.SpaceId && kvp.Key[3] == c.ActionId
                        && kvp.Key[4] == c.GridId && kvp.Key[5] == c.FormatId)
                    {
                        varId = kvp.Value[0];
                        IsAcceptUTCOffset4LOC = kvp.Value[1] == 1 ? true : false;
                    }
                }
                if (varId == 0) throw new Exception(String.Format(
                     "(!_x_params.TryGetValue(key, out varId) :\nparamId {0} timeId {1} spaceId {2} actionId {3} gridId {4} formatId {5}",
                     c.ParamId, c.TimeId, c.SpaceId, c.ActionId, c.GridId, c.FormatId));

                // GET || CREATE CATALOG
                AmurServiceReference.Catalog[] aCatalog1 = _client.GetCatalogList(_hSvc,
                    new int[] { x_grId_siteId[c.GeoRegId] },
                    new int[] { varId },
                    new int[] { _AMUR_METHOD_ID },
                    new int[] { _AMUR_SOURCE_ID },
                    new int[] { _x_levels[c.LevelTypeId] },
                    new double[] { (double)c.LevelValue });
                AmurServiceReference.Catalog aCatalog;
                if (aCatalog1.Length == 1)
                    aCatalog = aCatalog1[0];
                else
                {
                    aCatalog = new AmurServiceReference.Catalog()
                    {
                        Id = -1,
                        SiteId = x_grId_siteId[c.GeoRegId],
                        VariableId = varId,
                        MethodId = _AMUR_METHOD_ID,
                        SourceId = _AMUR_SOURCE_ID,
                        OffsetTypeId = _x_levels[c.LevelTypeId],
                        OffsetValue = c.LevelValue
                    };
                    aCatalog = _client.SaveCatalog(_hSvc, aCatalog);
                    Console.WriteLine("\t***Создана запись каталога БД АМУР.");
                }

                ret.Add(new object[] { aCatalog, IsAcceptUTCOffset4LOC });
            }
            if (ret.Count != hmdCatalogs.Count)
                Console.WriteLine(string.Format("\t***Не найдена {0} записей каталога БД АМУР.", hmdCatalogs.Count - ret.Count));
            else
                Console.WriteLine(string.Format("\tok"));
            return ret;
        }
    }
}
