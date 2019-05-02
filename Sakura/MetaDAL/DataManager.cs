using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Sakura.Meta
{
    public class DataManager : ADbMSSql
    {
        public static string _ConnectionStringDefault = null;
        public static string ConnectionStringDefault
        {
            get
            {
                if (_ConnectionStringDefault == null)
                    _ConnectionStringDefault = Viaware.Sakura.Meta.Properties.Settings.Default.ConnectionStringSakuraMeta;
                return _ConnectionStringDefault;
            }
            set
            {
                _ConnectionStringDefault = value;
            }
        }

        DataManager(string connectionString, int dbListId = (int)Enums.DbList.Hmdic)
            : base(connectionString, dbListId)
        {
        }
        public static DataManager GetInstance(string connectionString = null)
        {
            return connectionString == null ? new DataManager(ConnectionStringDefault) : new DataManager(connectionString);
        }
        //public static DataManager GetInstance()
        //{
        //    return new DataManager(ConnectionStringDefault);
        //}
        /// <summary>
        /// Экземпляр по со строкой подключения умолчанию.
        /// </summary>
        public static DataManager Instance
        {
            get
            {
                return GetInstance(ConnectionStringDefault);
            }
        }
        private CatalogRepository _CatalogRepository;
        public CatalogRepository CatalogRepository
        {
            get
            {
                if (_CatalogRepository == null)
                    _CatalogRepository = new CatalogRepository(this);
                return _CatalogRepository;
            }
        }
        private CatalogXGrivb2Repository _CatalogXGrivb2Repository;
        public CatalogXGrivb2Repository CatalogXGrivb2Repository
        {
            get
            {
                if (_CatalogXGrivb2Repository == null)
                    _CatalogXGrivb2Repository = new CatalogXGrivb2Repository(this);
                return _CatalogXGrivb2Repository;
            }
        }
        private DbListRepository _DbListRepository;
        public DbListRepository DbListRepository
        {
            get
            {
                if (_DbListRepository == null)
                    _DbListRepository = new DbListRepository(this);
                return _DbListRepository;
            }
        }
        private GridRepository _GridRepository;
        public GridRepository GridRepository
        {
            get
            {
                if (_GridRepository == null)
                    _GridRepository = new GridRepository(this);
                return _GridRepository;
            }
        }
        private GeoRegRepository _GeoRegRepository;
        public GeoRegRepository GeoRegRepository
        {
            get
            {
                if (_GeoRegRepository == null)
                    _GeoRegRepository = new GeoRegRepository(this);
                return _GeoRegRepository;
            }
        }
        private CenterRepository _CenterRepository;
        public CenterRepository CenterRepository
        {
            get
            {
                if (_CenterRepository == null)
                    _CenterRepository = new CenterRepository(this);
                return _CenterRepository;
            }
        }
        private GridTypeRepository _GridTypeRepository;
        public GridTypeRepository GridTypeRepository
        {
            get
            {
                if (_GridTypeRepository == null)
                    _GridTypeRepository = new GridTypeRepository(this);
                return _GridTypeRepository;
            }
        }
        private StationRepository _StationRepository;
        public StationRepository StationRepository
        {
            get
            {
                if (_StationRepository == null)
                    _StationRepository = new StationRepository(this);
                return _StationRepository;
            }
        }

        private EntityXEntityExternalRepository _EntityXEntityExternalRepository;
        public EntityXEntityExternalRepository EntityXEntityExternalRepository
        {
            get
            {
                if (_EntityXEntityExternalRepository == null)
                    _EntityXEntityExternalRepository = new EntityXEntityExternalRepository(this);
                return _EntityXEntityExternalRepository;
            }
        }
    }
}
