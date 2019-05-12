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
    public class TrackPartPointsRepository : Common.BaseRepository<TrackPartPoint>
    {
        internal TrackPartPointsRepository(Common.ADbNpgsql db) : base(db, "track_part_point")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new TrackPartPoint()
            {
                Id = (int)rdr["id"],
                TrackPartId = (int)rdr["track_part_id"],
                DateUTC = (DateTime)rdr["date_utc"],
                UTCOffset = (short)rdr["utc_offset"],
                GeoPoint = new Geo.GeoPoint((double)rdr["lat"], (double)rdr["lon"])
            };
        }

        public List<TrackPartPoint> Select(int trackPartId)
        {
            var fields = new Dictionary<string, object>()
                {
                    {"track_part_id", trackPartId}
                };
            return Select(fields);

        }
    }
}
