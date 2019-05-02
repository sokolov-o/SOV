using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Sakura.Meta
{
    public class GridTypeRepository : ADbMSSql
    {
        internal GridTypeRepository(ADbMSSql db)
            : base(db.ConnectionString, db.DbListId)
        {
        }

        public List<DicItem> Select()
        {
            List<DicItem> ret = new List<DicItem>();
            using (var cnn = Connection)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM grid_type", cnn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new DicItem()
                            {
                                Id = (int)rdr["ID"],
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
