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
    public class TrackPointRepository : Common.BaseRepository<TrackPoint>
    {
        internal TrackPointRepository(Common.ADbNpgsql db) : base(db, "track_point")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new TrackPoint()
            {
                Id = (int)rdr["id"],
                TrackId = (int)rdr["track_id"],
                DateUTC = (DateTime)rdr["date_utc"],
                UTCOffset = (short)rdr["utc_offset"],
                GeoPoint = new Geo.GeoPoint((double)rdr["lat"], (double)rdr["lon"])
            };
        }

        public List<TrackPoint> SelectByTrackId(int trackId)
        {
            var fields = new Dictionary<string, object>()
                {
                    {"track_id", trackId}
                };
            return Select(fields);

        }
    }
}
