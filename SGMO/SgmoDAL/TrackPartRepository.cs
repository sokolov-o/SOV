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
    public class TrackPartRepository : Common.BaseRepository<TrackPart>
    {
        internal TrackPartRepository(Common.ADbNpgsql db) : base(db, "track_part")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new TrackPart()
            {
                Id = (int)rdr["id"],  
                TrackId = (int)rdr["track_id"],
                DateS = (DateTime)rdr["date_s"]
            };
        }

        public TrackPart Select(int trackId, DateTime dateS)
        {
            var fields = new Dictionary<string, object>()
                {
                    {"track_id", trackId},
                    {"date_s", dateS}
                };
            List<TrackPart> ret = Select(fields);
            return ret == null || ret.Count == 0 ? null : ret[0];

        }
    }
}
