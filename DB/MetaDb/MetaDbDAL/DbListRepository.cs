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
    public class DbListRepository : BaseRepository<Db>
    {
        internal DbListRepository(Common.ADbNpgsql db) : base(db, "meta_db.db_list") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            Db ret = new Db((IdName) DataManager.ParseDataIdName(rdr));
            ret.DbTypeId = (int)rdr["db_type_id"];
            ret.ConnectionString = ADbNpgsql.GetValueString(rdr, "connection_string");
            return ret;
        }
        public int Insert(Db item)
        {
            Dictionary<string, object> fields = IdName.GetFields(item);
            fields.Add("db_type_id", item.DbTypeId);
            fields.Add("connection_string", item.ConnectionString);

            return InsertWithReturn(fields);
        }
    }
}
