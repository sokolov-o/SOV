using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
//using Viaware.Sakura.Grib;

namespace FERHRI.Sakura.Meta
{
    public class DbListRepository
    {
        ADbMSSql _db;

        public  DbListRepository(ADbMSSql db)
        {
            _db = db;
        }

        public string SelectAttrValue(int dbListId, int attrId)
        {
            using (var cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(
                    "select value from db_attr where id_db_list = " + dbListId + " and id_t_attr  = " + attrId
                    , cnn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                            return rdr[0].ToString();
                        return null;
                    }
                }
            }
        }
    }
}
