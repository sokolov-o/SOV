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
    public class DbInterfaceTypeRepository : BaseRepository<DbInterfaceType>
    {
        internal DbInterfaceTypeRepository(Common.ADbNpgsql db) : base(db, "meta_db.db_interface_type") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return (DbInterfaceType)DataManager.ParseDataIdName(rdr);
        }
        public int Insert(DbInterfaceType item)
        {
            return InsertWithReturn(IdName.GetFields(item));
        }
    }
}
