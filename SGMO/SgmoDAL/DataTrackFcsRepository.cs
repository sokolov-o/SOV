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
    public class DataTrackFcsRepository : Common.BaseRepository<DataTrackFcs>
    {
        internal DataTrackFcsRepository(Common.ADbNpgsql db) : base(db, "data_track_fcs")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DataTrackFcs()
            {
                Id = (int)rdr["id"],
                TrackPartPointId = (int)rdr["track_part_point_id"],
                CatalogId = (int)rdr["catalog_id"],
                LeadTime = (double)rdr["lead_time"],
                Value = (double)rdr["value"],
                DateInsert = (DateTime)rdr["date_insert"]
            };
        }

        public List<DataTrackFcs> SelectByTrackPartPointId(int trackPartPointId)
        {
            var fields = new Dictionary<string, object>()
                {
                    {"track_part_point_id", trackPartPointId}
                };
            return Select(fields);

        }
        public void Insert(List<DataTrackFcs> data)
        {
            List<Dictionary<string, object>> fields = new List<Dictionary<string, object>>();
            foreach (var value in data)
            {
                fields.Add(new Dictionary<string, object>
                {
                    { "track_part_point_id", value.TrackPartPointId },
                    { "catalog_id" , value.TrackPartPointId },
                    { "lead_time", value.TrackPartPointId },
                    { "value", value.TrackPartPointId },
                    { "date_insert", value.TrackPartPointId }
                });
            }
            Insert(fields);
        }

    }
}
