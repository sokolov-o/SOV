using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.MetaDb
{
    public class DataManager : Common.BaseDataManager
    {
        const string DATA_MANAGER_TYPE = "FERHRI.MetaDb.DataManager";

        public DbInterfaceRepository DbInterfaceRepository;
        public DbInterfaceTypeRepository DbInterfaceTypeRepository;
        public DbListRepository DbListRepository;
        public DbTypeRepository DbTypeRepository;

        public DataManager(string connectionString)
            : base(connectionString)
        {
            DbInterfaceRepository = new DbInterfaceRepository(this);
            DbInterfaceTypeRepository = new DbInterfaceTypeRepository(this);
            DbListRepository = new DbListRepository(this);
            DbTypeRepository = new DbTypeRepository(this);
        }

        // --------------------------------------------------------
        public static string GetDefaultConnectionString()
        {
            return Properties.Settings.Default.ConnectionString;
        }

        public static void SetDefaultConnectionString(string cnns)
        {
            Properties.Settings.Default["ConnectionString"] = cnns;
        }

        /// <summary>
        /// Экземпляр со строкой подключения по умолчанию.
        /// </summary>
        public static DataManager GetInstance()
        {
            return (DataManager)GetInstance(GetDefaultConnectionString(), Type.GetType(DATA_MANAGER_TYPE));
        }
        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        public static DataManager GetInstance(string connectionString)
        {
            return (DataManager)GetInstance(connectionString, Type.GetType(DATA_MANAGER_TYPE));
        }
        public static object ParseDataIdName(NpgsqlDataReader rdr)
        {
            return new IdName()
            {
                Id = (int)rdr["id"],
                Name = rdr["name"].ToString()
            };
        }
    }
}
