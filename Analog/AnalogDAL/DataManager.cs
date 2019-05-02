using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Analog
{
    public class DataManager : Common.BaseDataManager
    {
        public FieldRepository FieldRepository;
        public ActionRepository ActionRepository;
        public TimeProcessTypeRepository TimeProcessTypeRepository;
        public TaskCalcMetricRepository TaskCalcMetricRepository;
        public TaskSelectAnalogRepository TaskSelectAnalogRepository;
        public DataAnalog0Repository DataAnalog0Repository;
        public DataAnalog1Repository DataAnalog1Repository;

        public DataManager(string connectionString)
            : base(connectionString)
        {
            NpgsqlConnection.MapCompositeGlobally<IntDouble>("task.int_double");
            NpgsqlConnection.MapCompositeGlobally<IntClimate>("task.climate");

            FieldRepository = new FieldRepository(this);
            ActionRepository = new ActionRepository(this);
            TimeProcessTypeRepository = new TimeProcessTypeRepository(this);
            TaskCalcMetricRepository = new TaskCalcMetricRepository(this);
            TaskSelectAnalogRepository = new TaskSelectAnalogRepository(this);
            DataAnalog0Repository = new DataAnalog0Repository(this);
            DataAnalog1Repository = new DataAnalog1Repository(this);
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
            return (DataManager)GetInstance(GetDefaultConnectionString(), Type.GetType("FERHRI.Analog.DataManager"));
        }
        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        public static DataManager GetInstance(string connectionString)
        {
            return (DataManager)GetInstance(connectionString, Type.GetType("FERHRI.Analog.DataManager"));
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
