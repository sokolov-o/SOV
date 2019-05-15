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
    public class TrackRepository : Common.BaseRepository<Track>
    {
        internal TrackRepository(Common.ADbNpgsql db) : base(db, "track")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Track()
            {
                Id = (int)rdr["id"],
                SiteId = (int)rdr["site_id"],
                NameRus = rdr["name"].ToString(),
                NameEng = rdr["name_eng"].ToString(),
                DateSUTC = (DateTime)rdr["date_s_utc"],
                ParentId = ADbNpgsql.GetValueInt(rdr, "parent_id")
            };
        }
        public List<Track> SelectChilds(int parentTrackId)
        {
            var fields = new Dictionary<string, object>()
                {
                    {"parent_id", parentTrackId}
                };
            return Select(fields);

        }
        public int Insert(Track newTrack)
        {
            var fields = new Dictionary<string, object>()
            {
                {"site_id", newTrack.SiteId},
                {"name", newTrack.NameRus},
                {"name_eng", newTrack.NameEng},
                {"date_s_utc", newTrack.DateSUTC},
                {"parent_id", newTrack.ParentId}
            };
            return InsertWithReturn(fields);

        }
    }
}
