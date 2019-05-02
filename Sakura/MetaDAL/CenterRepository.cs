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
    public class CenterRepository : ADbMSSql
    {
        internal CenterRepository(ADbMSSql db)
            : base(db.ConnectionString, db.DbListId)
        {
        }
        public List<Center> Select()
        {
            List<Center> ret = new List<Center>();
            using (var cnn = Connection)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM centers", cnn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new Center()
                            {
                                Id = (int)rdr["ID"],
                                GribId = ADbMSSql.GetIntNullable(rdr, "ID_GRIB"),
                                Name = rdr["Name"].ToString(),
                                Description = rdr["Description"].ToString()
                            });
                        }
                    }
                }
            }
            return ret;
        }
    }
}
