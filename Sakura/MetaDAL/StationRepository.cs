using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
//using Viaware.Sakura.Grib;

namespace FERHRI.Sakura.Meta
{
    public class StationRepository
    {
        ADbMSSql _db;

        public StationRepository(ADbMSSql db)
        {
            _db = db;
        }

        public List<Station> SelectStations4Region(int regionId)
        {
            List<Station> ret = new List<Station>();
            using (SqlConnection cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select s.* from stations s inner join sttionsregions sr" +
                    " on s.id = sr.stationsId where regionsId = @regionId", cnn))
                {
                    cmd.Parameters.AddWithValue("@regionId", regionId);
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


        public Station GetStation(int stIndex, Enums.StationType stationTypeId)
        {
            Station ret = null;
            using (SqlConnection cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select * from stations where st_index = @stIndex and stationTypeId = @stationTypeId", cnn))
                {
                    cmd.Parameters.AddWithValue("@stIndex", stIndex);
                    cmd.Parameters.AddWithValue("@stationTypeId", stationTypeId);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                            ret = Parse(rdr);
                    }
                }
            }
            return ret;
        }

        Station Parse(SqlDataReader rdr)
        {
            return new Station(
                (int)rdr["id"],
                (int)rdr["st_index"],
                (Enums.StationType)(int)rdr["stationTypeId"],
                (int)rdr["orgId"], rdr["stnname"].ToString(),
                (rdr["lat"] == DBNull.Value) ? null : (float?)(double?)rdr["lat"],
                (rdr["lon"] == DBNull.Value) ? null : (float?)(double?)rdr["lon"],
                (rdr["coord_num"] == DBNull.Value) ? null : (int?)rdr["coord_num"],
                (rdr["timezone"] == DBNull.Value) ? null : (int?)(byte)rdr["timezone"]
            );
        }
    }
}
