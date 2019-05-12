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
                DateS = (DateTime)rdr["date_s"]
            };
        }

        /// <summary>
        /// Выбрать маршрут.
        /// </summary>
        ////public Track SelectTrack(int trackId, bool fillFK = false)
        ////{
        ////    List<Track> ret = SelectTrack(new List<int>() { trackId }, fillFK);
        ////    return ret.Count == 0 ? null : ret[0];
        ////}
        /// <summary>
        /// Выбрать маршруты.
        /// </summary>
        ////public List<Track> SelectTrack(List<int> trackIds, bool fillFK = false)
        ////{
        ////    List<Track> ret = new List<Track>();

        ////    using (NpgsqlConnection cnn = _db.Connection)
        ////    {
        ////        using (NpgsqlCommand cmd = new NpgsqlCommand("select * from track0 where :id is null or id=any(:id)", cnn))
        ////        {
        ////            cmd.Parameters.AddWithValue("id", trackIds);
        ////            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
        ////            {
        ////                while (rdr.Read())
        ////                {
        ////                    ret.Add(new Track()
        ////                    {
        ////                        Id = (int)rdr["id"],
        ////                        Name = (string)rdr["name"],
        ////                        SiteId = (int)rdr["site_id"]
        ////                    });
        ////                }
        ////            }
        ////        }
        ////    }
        ////    if (fillFK)
        ////    {
        ////        foreach (var item in ret)
        ////        {
        ////            item.Track1List = SelectTrackPartPoints(item.Id);
        ////        }
        ////    }
        ////    return ret;
        ////}
        /// <summary>
        /// Получить координаты и время для трека.
        /// </summary>
        /// <param name="trackPartId"></param>
        /// <returns>Отсортированные по времени координаты трека.</returns>
        ////public List<Track1> SelectTrackPartPoints(int trackPartId)
        ////{
        ////    List<Track1> ret = new List<Track1>();

        ////    using (NpgsqlConnection cnn = _db.Connection)
        ////    {
        ////        using (NpgsqlCommand cmd = new NpgsqlCommand("select * from track1 where track0_id =:track0id", cnn))
        ////        {
        ////            cmd.Parameters.AddWithValue("track0id", trackPartId);
        ////            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
        ////            {
        ////                while (rdr.Read())
        ////                {
        ////                    ret.Add(new Track1()
        ////                    {
        ////                        Id = (int)rdr["id"],
        ////                        Track0Id = (int)rdr["track0_id"],
        ////                        Date = (DateTime)rdr["date"],
        ////                        UTCOffset = (Int16)rdr["utc_offset"],
        ////                        Lat = (double)rdr["lat"],
        ////                        Lon = (double)rdr["lon"]
        ////                    });
        ////                }
        ////            }
        ////        }
        ////    }
        ////    return ret.OrderBy(x => x.Date).ToList();
        ////}
    }
}
