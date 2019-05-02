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
    public class TaskSelectAnalogRepository : BaseRepository<TaskSelectAnalog>
    {
        internal TaskSelectAnalogRepository(Common.ADbNpgsql db)
            : base(db, "task.select_ang")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new TaskSelectAnalog() { Id = (int)rdr["id"], MethodId = (int)rdr["amur_method_id"], TimeId = (int)rdr["amur_time_id"] };
        }
        public List<TaskSelectAnalog> SelectByMethodId(int methodId)
        {
            List<TaskSelectAnalog> ret = Select(new Dictionary<string, object>() { { "amur_method_id", methodId } });

            return ret;
        }
        public int Insert(TaskSelectAnalog item)
        {
            return InsertWithReturn(new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"amur_method_id", item.MethodId},
                {"amur_time_id", item.TimeId}
            });
        }
    }
}
