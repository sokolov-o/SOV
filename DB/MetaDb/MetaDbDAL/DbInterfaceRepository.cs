using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.MetaDb
{
    public class DbInterfaceRepository : BaseRepository<DbInterface>
    {
        internal DbInterfaceRepository(Common.ADbNpgsql db) : base(db, "meta_db.db_interface") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DbInterface()
            {
                Id = (int)rdr["id"],
                DbListId = (int)rdr["db_list_id"],
                DbInterfaceTypeId = (int)rdr["db_interface_type_id"]
            };
        }
        public int Insert(DbInterface item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"db_list_id", item.DbListId},
                {"db_interface_type_id", item.DbInterfaceTypeId}
            };
            return InsertWithReturn(fields);
        }
        public List<DbInterface> SelectByType(int dbInterfaceTypeId)
        {
            var fields = new Dictionary<string, object>() { { "db_interface_type_id", dbInterfaceTypeId} };
            return Select(fields);
        }
    }
}
