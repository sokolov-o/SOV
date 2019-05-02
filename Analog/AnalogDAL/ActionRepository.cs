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
    public class ActionRepository : BaseRepository<Action>
    {
        internal ActionRepository(Common.ADbNpgsql db)
            : base(db, "task.action")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Action((IdName)DataManager.ParseDataIdName(rdr));
        }
        public int Insert(Action item)
        {
            return InsertWithReturn(new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name}
            });
        }
    }
}
