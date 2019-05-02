using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Sakura.Meta;
using FERHRI.Common;

namespace FERHRI.Sakura.Data
{
    public class DataManager : ADbMSSql
    {
        static Dictionary<int, string> _dbListConnectionStrings = new Dictionary<int, string>();

        DataManager(string connectionString, int dbListId)
            : base(connectionString, dbListId)
        {
        }
        public static DataManager GetInstance(string connectionString, int dbListId)
        {
            return new DataManager(connectionString, dbListId);
        }
        private HCRRepository _HCRRepository;
        public HCRRepository HCRRepository
        {
            get
            {
                if (_HCRRepository == null)
                    _HCRRepository = new HCRRepository(this);
                return _HCRRepository;
            }
        }
        private FieldCDVImageRepository _FieldCDVImageRepository;
        public FieldCDVImageRepository FieldCDVImageRepository
        {
            get
            {
                if (_FieldCDVImageRepository == null)
                    _FieldCDVImageRepository = new FieldCDVImageRepository(this.ConnectionString, this.DbListId);
                return _FieldCDVImageRepository;
            }
        }
        private FieldClimateRepository _FieldClimateRepository;
        public FieldClimateRepository FieldClimateRepository
        {
            get
            {
                if (_FieldClimateRepository == null)
                    _FieldClimateRepository = new FieldClimateRepository(this);
                return _FieldClimateRepository;
            }
        }
        /// <summary>
        /// Получить репозиторий данных:
        /// </summary>
        /// <param name="dbListId"></param>
        /// <returns></returns>
        public static object GetRepository(int dbListId, User user = null)
        {
            // GET SRC CONNECTION
            string cnns = Meta.DataManager.GetInstance().DbListRepository.SelectAttrValue(dbListId, (int)Meta.Enums.TaskAttr.SRC_CONNECTION);
            if (cnns == null)
                throw new Exception("Отсутствует атрибут SRC_CONNECTION для базы данных DbListId = " + dbListId);
            if (user != null)
                cnns = ADbMSSql.UpdateUser(cnns, user);

            // GET REPOSITORY
            switch ((FERHRI.Sakura.Meta.Enums.DbList)dbListId)
            {
                case FERHRI.Sakura.Meta.Enums.DbList.HCRIndeces: 
                case FERHRI.Sakura.Meta.Enums.DbList.HCRMonth: return Data.DataManager.GetInstance(cnns, dbListId).HCRRepository;

                case FERHRI.Sakura.Meta.Enums.DbList.HmdGribMonth: return Data.DataManager.GetInstance(cnns, dbListId).FieldCDVImageRepository;
                case FERHRI.Sakura.Meta.Enums.DbList.HmdFieldClimate: return Data.DataManager.GetInstance(cnns, dbListId).FieldClimateRepository;
                //case Enums.DbList.FILES_GFS_OMA_5deg:
                //case Enums.DbList.FILES_GFS_OMA_025deg: return new GfsGrib2Repository(dbListId, cnns, user);
                default:
                    return null;
            }
        }
    }
}
