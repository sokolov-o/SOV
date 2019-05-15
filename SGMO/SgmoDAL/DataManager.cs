using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SOV.SGMO
{
    public class DataManager : Common.BaseDataManager
    {
        public TrackRepository TrackRepository;
        public TrackPointRepository TrackPointsRepository;
        public DataTrackFcsRepository DataTrackFcsRepository;

        DataManager(string connectionString)
            : base(connectionString)
        {
            TrackRepository = new TrackRepository(this);
            TrackPointsRepository = new TrackPointRepository(this);
            DataTrackFcsRepository = new DataTrackFcsRepository(this);
        }

        static Dictionary<string, DataManager> _dm = new Dictionary<string, DataManager>();
        /// <summary>
        /// Экземпляр со строкой подключения по умолчанию.
        /// </summary>
        /// <returns></returns>
        public static DataManager GetInstance()
        {
            return GetInstance(global::SOV.SGMO.Properties.Settings.Default.ConnectionStringSGMO);
        }

        static public void SetDefaultConnectionString(string cnns)
        {
            SOV.SGMO.Properties.Settings.Default["ConnectionStringSGMO"] = cnns;
        }
        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        /// <returns></returns>
        public static DataManager GetInstance(string connectionString)
        {
            DataManager ret;
            if (!_dm.TryGetValue(connectionString, out ret))
            {
                ret = new DataManager(connectionString);
                _dm.Add(connectionString, ret);
            }
            return ret;
        }
        private MethvarXGrib2Repository _MethvarXGrib2Repository;
        public MethvarXGrib2Repository MethvarXGrib2Repository
        {
            get
            {
                if (_MethvarXGrib2Repository == null)
                    _MethvarXGrib2Repository = new MethvarXGrib2Repository(this);
                return _MethvarXGrib2Repository;
            }
        }
        private DataFcsNode0Repository _DataFcsNode0Repository;
        public DataFcsNode0Repository DataFcsNode0Repository
        {
            get
            {
                if (_DataFcsNode0Repository == null)
                    _DataFcsNode0Repository = new DataFcsNode0Repository(this);
                return _DataFcsNode0Repository;
            }
        }
        private DataFcsNode1Repository _DataFcsNode1Repository;
        public DataFcsNode1Repository DataFcsNode1Repository
        {
            get
            {
                if (_DataFcsNode1Repository == null)
                    _DataFcsNode1Repository = new DataFcsNode1Repository(this);
                return _DataFcsNode1Repository;
            }
        }
        public ProbRepository _ProbRepository;
        public ProbRepository ProbRepository
        {
            get
            {
                if (_ProbRepository == null)
                    _ProbRepository = new ProbRepository(this);
                return _ProbRepository;
            }
        }
        private ProbCatalogRepository _ProbCatalogRepository;
        public ProbCatalogRepository ProbCatalogRepository
        {
            get
            {
                if (_ProbCatalogRepository == null)
                    _ProbCatalogRepository = new ProbCatalogRepository(this);
                return _ProbCatalogRepository;
            }
        }
        private ProbCatalogPileRepository _ProbCatalogPileRepository;
        public ProbCatalogPileRepository ProbCatalogPileRepository
        {
            get
            {
                if (_ProbCatalogPileRepository == null)
                    _ProbCatalogPileRepository = new ProbCatalogPileRepository(this);
                return _ProbCatalogPileRepository;
            }
        }
        private MessageSiteRepository _MessageSiteRepository;
        public MessageSiteRepository MessageSiteRepository
        {
            get
            {
                if (_MessageSiteRepository == null)
                    _MessageSiteRepository = new MessageSiteRepository(this);
                return _MessageSiteRepository;
            }
        }
        private MessageTypeRepository _MessageTypeRepository;
        public MessageTypeRepository MessageTypeRepository
        {
            get
            {
                if (_MessageTypeRepository == null)
                    _MessageTypeRepository = new MessageTypeRepository(this);
                return _MessageTypeRepository;
            }
        }
        private PileRepository _PileRepository;
        public PileRepository PileRepository
        {
            get
            {
                if (_PileRepository == null)
                    _PileRepository = new PileRepository(this);
                return _PileRepository;
            }
        }
        private WarningPileCatalogRepository _WarningPileCatalogRepository;
        public WarningPileCatalogRepository WarningPileCatalogRepository
        {
            get
            {
                if (_WarningPileCatalogRepository == null)
                    _WarningPileCatalogRepository = new WarningPileCatalogRepository(this);
                return _WarningPileCatalogRepository;
            }
        }
    }
}
