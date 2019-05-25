using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace Amur.Import
{
    public class AMSDataRepository
    {
        internal AMSDataRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public List<AMSData> Select(int stationTypeId, DateTime dateS, DateTime dateF)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "select d.stationID station_id, s.name station_name, d.meteoElementTypeID variable_id," +
                    " d.observationdate date_obs, d._value value," +
                    " cast(ssLat.Value as float) lat," +
                    " cast(ssLon.Value as float) lon" +
                    " from meteoelement d" +
                    " inner join station s on d.stationID = s.stationID and stationType = 8" +
                    " inner join meteoweb.dbo.station s1 on s1.localId = s.stationId and s1.TypeId = 1" +
                    " inner join meteoweb.dbo.stationsetting ssLat on ssLat.stationId = s1.Id and ssLat.TypeId = 1" +
                    " inner join meteoweb.dbo.stationsetting ssLon on ssLat.stationId = s1.Id and ssLon.TypeId = 2" +
                    " where d.observationdate between @date_s and @date_f and stationType = @station_type_id", cnn))
                {
                    cmd.Parameters.AddWithValue("@date_s", dateS);
                    cmd.Parameters.AddWithValue("@date_f", dateF);
                    cmd.Parameters.AddWithValue("@station_type_id", stationTypeId);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<AMSData> ret = new List<AMSData>();
                        while (rdr.Read())
                        {
                            ret.Add(new AMSData()
                            {
                                StationId = (int)rdr["station_id"],
                                StationName = rdr["station_name"].ToString(),
                                VariableId = (int)rdr["variable_id"],
                                DateObs = (DateTime)rdr["date_obs"],
                                Value = (double)rdr["value"],
                                Lat = (double)rdr["lat"],
                                Lon = (double)rdr["lon"]
                            });
                        }
                        foreach (AMSData item in ret)
                        {
                            item.StationName = item.StationName.Replace('\r', ' ').Replace('\n', ' ').Trim();
                        }
                        return ret;
                    }
                }
            }
        }
    }
}
