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
    public class DataSiteFcsRepository : BaseRepository<DataSiteFcs>
    {
        internal DataSiteFcsRepository(Common.ADbNpgsql db) : base(db, "data_site_fcs") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DataSiteFcs()
            {
                Id = (int)rdr["id"],
                CatalogId = (int)rdr["catalog_id"],
                LeadTime = (double)rdr["lead_time"],
                DateIniUTC = (DateTime)rdr["date_ini_utc"],
                Value = (double)rdr["value"]
            };
        }

        public List<DataSiteFcs> Select(List<int> catalogIds, DateTime dateIniUTC)
        {
            var fields = new Dictionary<string, object>()
            {
                {"catalog_id", catalogIds},
                {"date_ini_utc", dateIniUTC}
            };
            return Select(fields);
        }
        public void Insert(List<DataSiteFcs> data)
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
