using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Analog
{
    public class TimeProcessTypeRepository : BaseRepository<TimeProcessType>
    {
        internal TimeProcessTypeRepository(Common.ADbNpgsql db)
            : base(db, "task.time_process_type")
        {
        }
        public static List<TimeProcessType> GetCash()
        {
            return GetCash(DataManager.GetInstance().TimeProcessTypeRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            IdName idn = (IdName)DataManager.ParseDataIdName(rdr);
            return new TimeProcessType() { Id = idn.Id, Name = idn.Name, Description = rdr["description"].ToString() };
        }
        public int Insert(TimeProcessType item)
        {
            return InsertWithReturn(new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name},
                {"description", item.Name}
            });
        }
    }
}
