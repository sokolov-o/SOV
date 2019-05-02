using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Geo;
using FERHRI.Common;

namespace FERHRI.Sakura.Meta
{
    public class GeoRegRepository : ADbMSSql
    {
        internal GeoRegRepository(ADbMSSql db)
            : base(db.ConnectionString, db.DbListId)
        {
        }

        public List<GeoRectangle> SelectByNameList(List<string> geoRegNames)
        {
            List<GeoRectangle> ret = new List<GeoRectangle>();
            using (var cnn = Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select * from GeoRegion where name = @name", cnn))
                {
                    cmd.Parameters.AddWithValue("@name", "");
                    foreach (var item in geoRegNames)
                    {
                        cmd.Parameters[0].Value = item;
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                ret.Add(Parse(rdr));
                            }
                        }
                    }
                }
            }
            return ret;
        }
        public List<GeoRectangle> SelectByIdList(List<int> geoRegId)
        {
            List<GeoRectangle> ret = new List<GeoRectangle>();
            using (var cnn = Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select * from GeoRegion where id in (" + StrVia.ToString(geoRegId) + ")", cnn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(Parse(rdr));
                        }
                    }
                }
            }
            return ret;
        }
        GeoRectangle Parse(SqlDataReader rdr)
        {
            return new GeoRectangle(
                (double)rdr["South"],
                (double)rdr["North"],
                (double)rdr["West"],
                (double)rdr["East"],
                rdr["Name"].ToString(),
                (int)rdr["ID"]);
        }
        public GeoRectangle SelectGeoReg(int grId)
        {
            List<GeoRectangle> ret = SelectByIdList(new List<int>(new int[] { grId }));
            return ret.Count == 1 ? ret[0] : null;
        }
    }
}
