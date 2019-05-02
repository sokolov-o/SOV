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
    public class DbTypeRepository : BaseRepository<DbType>
    {
        internal DbTypeRepository(Common.ADbNpgsql db) : base(db, "meta_db.db_interface_type") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return (DbType)DataManager.ParseDataIdName(rdr);
        }
        public int Insert(DbType item)
        {
            return InsertWithReturn(IdName.GetFields(item));
        }
    }
}
