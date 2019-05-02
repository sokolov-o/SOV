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
    public class _DELME_DataFcsRepository
    {
        Common.ADbNpgsql _db;
        internal _DELME_DataFcsRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        /// <summary>
        /// Записать шапку прогностических данных.
        /// </summary>
        public int InsertDataFcs0(_DELME_DataFcs0 data)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into data_fcs0 (track_id, date_ini_utc)"
                    + " values (:track_id, :date_ini_utc);"
                    + "select max(id) from data_fcs0;", cnn))
                {
                    cmd.Parameters.AddWithValue("track_id", data.Track0Id);
                    cmd.Parameters.AddWithValue("date_ini_utc", data.DateIniUTC);

                    data.Id = int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            //if (data.DataFcs1List != null)
            //    Insert(data.Id, data.DataFcs1List);
            return data.Id;
        }
        /// <summary>
        /// Записать прогнозы в прогнозопунктах данные.
        /// </summary>
        public void InsertDataFcs1(int dataFcs0Id, List<_DELME_DataFcs1> datas)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into data_fcs1 (data_fcs0_id, fcs_varoff_id, value, lat, lon, lag)"
                    + " values (:data_fcs0_id, :fcs_varoff_id, :value, :lat, :lon, :lag)", cnn))
                {
                    cmd.Parameters.AddWithValue("data_fcs0_id", dataFcs0Id);
                    cmd.Parameters.AddWithValue("fcs_varoff_id", 0);
                    cmd.Parameters.AddWithValue("value", 0.0);
                    cmd.Parameters.AddWithValue("lat", 0.0);
                    cmd.Parameters.AddWithValue("lon", 0.0);
                    cmd.Parameters.AddWithValue("lag", 0.0);

                    foreach (var item in datas)
                    {
                        cmd.Parameters[1].Value = item.VaroffId;
                        cmd.Parameters[2].Value = item.Value;
                        cmd.Parameters[3].Value = item.Point.LatGrd;
                        cmd.Parameters[4].Value = item.Point.LonGrd;
                        cmd.Parameters[5].Value = item.Lag;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<_DELME_DataFcs0> SelectData0(int? track0Id, DateTime? dateIni, bool fillFK = false)
        {
            List<_DELME_DataFcs0> ret = new List<_DELME_DataFcs0>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from data_fcs0 where"
                + " (:track_id is null or track_id = :track_id)"
                + " and (:date_ini_utc is null or date_ini_utc = :date_ini_utc)", cnn))
                {
                    cmd.Parameters.AddWithValue("track_id", track0Id);
                    cmd.Parameters.AddWithValue("date_ini_utc", dateIni);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new _DELME_DataFcs0()
                            {
                                Id = (int)rdr["id"],
                                Track0Id = (int)rdr["track_id"],
                                DateIniUTC = (DateTime)rdr["date_ini_utc"],
                                DateInsert = (DateTime)rdr["date_ins"]
                            });
                        }
                    }
                }
            }
            if (fillFK)
            {
                foreach (var item in ret)
                {
                    item.DataFcs1List = SelectData1(item.Id);
                }
            }
            return ret;
        }
        public List<_DELME_DataFcs1> SelectData1(int dataFcs0Id)
        {
            List<_DELME_DataFcs1> ret = new List<_DELME_DataFcs1>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from data_fcs1 where data_fcs_0_id =:data_fcs_0_id", cnn))
                {
                    cmd.Parameters.AddWithValue("data_fcs_0_id", dataFcs0Id);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new _DELME_DataFcs1()
                            {
                                Id = (int)rdr["id"],
                                VaroffId = (int)rdr["fcs_varoff_id"],
                                DataFcs0Id = (int)rdr["data_fcs_0_id"],
                                Lag = (double)rdr["lag"],
                                Value = (double)rdr["value"],
                                Point = new Geo.GeoPoint((double)rdr["lat"], (double)rdr["lon"])
                            });
                        }
                    }
                }
            }
            return ret;
        }

        public void DeleteData1(int data0Id, int? methodId)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("delete from data_fcs1 where id in (select data_fcs1_id from data_fcs_view where data_fcs0_id = :data_fcs0_id and method_id=:method_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("data_fcs0_id", data0Id);
                    cmd.Parameters.AddWithValue("method_id", methodId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
