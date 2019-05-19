using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.SGMO
{
    public class DataSiteFcsRepository : BaseRepository<DataFcs>
    {
        internal DataSiteFcsRepository(Common.ADbNpgsql db) : base(db, "data_site_fcs") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DataFcs()
            {
                Id = (int)rdr["id"],
                CatalogId = (int)rdr["catalog_id"],
                LeadTime = (double)rdr["lead_time"],
                DateIniUTC = (DateTime)rdr["date_ini_utc"],
                Value = (double)rdr["value"]
            };
        }

        public List<DataFcs> Select(List<int> catalogIds, DateTime dateIniUTC)
        {
            var fields = new Dictionary<string, object>()
            {
                {"catalog_id", catalogIds},
                {"date_ini_utc", dateIniUTC}
            };
            return Select(fields);
        }

        public Dictionary<int, DateTime> SelectDateIniUTC4Sites(List<int> siteIds)
        {
            var fields = new Dictionary<string, object>()
            {
                {"site_id", siteIds}
            };
            Dictionary<int, DateTime> res = new Dictionary<int, DateTime>();
            try
            {
                using (NpgsqlConnection cnn = _db.Connection)
                using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct site_id, date_ini_utc from data_site_fcs_view", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    foreach (var field in fields)
                    {
                        cmd.Parameters.AddWithValue(field.Key, field.Value ?? DBNull.Value);
                    }
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            res.Add((int)rdr["site_id"], (DateTime)rdr["date_ini_utc"]);
                        return res;
                    }
                }
            }
            catch (System.Data.Common.DbException e)
            {
                throw new RuDbException(e);
            }
        }

        public void Insert(List<DataFcs> data)
        {
            if (data != null && data.Count > 0)
            {
                List<Dictionary<string, object>> fields = new List<Dictionary<string, object>>();
                foreach (var value in data)
                {
                    fields.Add(new Dictionary<string, object>
                    {
                        { "date_ini_utc" , value.DateIniUTC},
                        { "catalog_id" , value.CatalogId},
                        { "lead_time", value.LeadTime},
                        { "value", value.Value}
                    });
                }
                Insert(fields);
            }
        }

    }
}
