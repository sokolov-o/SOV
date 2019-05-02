using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.SGMO
{
    public class TrackRepository
    {
        Common.ADbNpgsql _db;
        internal TrackRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        /// <summary>
        /// Выбрать маршрут.
        /// </summary>
        public Track0 SelectTrack0(int track0Id, bool fillFK = false)
        {
            List<Track0> ret = SelectTrack0(new List<int>() { track0Id }, fillFK);
            return ret.Count == 0 ? null : ret[0];
        }
        /// <summary>
        /// Выбрать маршруты.
        /// </summary>
        public List<Track0> SelectTrack0(List<int> track0Ids, bool fillFK = false)
        {
            List<Track0> ret = new List<Track0>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from track0 where :id is null or id=any(:id)", cnn))
                {
                    cmd.Parameters.AddWithValue("id", track0Ids);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new Track0()
                            {
                                Id = (int)rdr["id"],
                                Name = (string)rdr["name"],
                                SiteId = (int)rdr["site_id"]
                            });
                        }
                    }
                }
            }
            if (fillFK)
            {
                foreach (var item in ret)
                {
                    item.Track1List = SelectTrack1(item.Id);
                }
            }
            return ret;
        }
        /// <summary>
        /// Получить координаты и время для трека.
        /// </summary>
        /// <param name="track0Id"></param>
        /// <returns>Отсортированные по времени координаты трека.</returns>
        public List<Track1> SelectTrack1(int track0Id)
        {
            List<Track1> ret = new List<Track1>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from track1 where track0_id =:track0id", cnn))
                {
                    cmd.Parameters.AddWithValue("track0id", track0Id);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new Track1()
                            {
                                Id = (int)rdr["id"],
                                Track0Id = (int)rdr["track0_id"],
                                Date = (DateTime)rdr["date"],
                                UTCOffset = (Int16)rdr["utc_offset"],
                                Lat = (double)rdr["lat"],
                                Lon = (double)rdr["lon"]
                            });
                        }
                    }
                }
            }
            return ret.OrderBy(x => x.Date).ToList();
        }
    }
}
