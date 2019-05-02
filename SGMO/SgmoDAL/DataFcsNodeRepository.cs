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
    public class DataFcsNode0Repository : BaseRepository<DataFcsNode0>
    {
        internal DataFcsNode0Repository(Common.ADbNpgsql db) : base(db, "data_fcs_node0") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DataFcsNode0()
            {
                Id = (int)rdr["id"],
                CatalogId = (int)rdr["catalog_id"],
                LeadtimeUnitId = (int)rdr["leadtime_unit_id"],
                DateIni = (DateTime)rdr["date_ini"],
                DateIsert = (DateTime)rdr["date_insert"]
            };
        }

        public List<DataFcsNode0> Select(int catalogId, DateTime dateIniS, DateTime dateIniF, int leadTimeUnitId, bool withDataFcsNode1)
        {
            var fields = new Dictionary<string, object>()
            {
                {"catalog_id", catalogId},
                {"date_ini_s", dateIniS},
                {"date_ini_f", dateIniF},
                {"leadtime_unit_id", leadTimeUnitId}
            };

            string sql = "select * from " + base.TableName +
                "where catalog_id=:catalog_id and (date_ini between :date_ini_s and :date_ini_f) and leadtime_unit_id = :leadtime_unit_id";
            List<DataFcsNode0> ret = base.SelectAllFields(sql, fields);

            if (ret.Count > 0 && withDataFcsNode1)
            {
                foreach (var item in ret)
                {
                    item.DataFcsNode1List = DataManager.GetInstance().DataFcsNode1Repository.SelectByDataFcsNode0Id(item.Id);
                }
            }

            return ret;
        }
        public DataFcsNode0 Select(int catalogId, DateTime dateIni, int leadTimeUnitId, bool withDataFcsNode1)
        {
            List<DataFcsNode0> ret = Select(catalogId, dateIni, dateIni, leadTimeUnitId, withDataFcsNode1);

            if (ret.Count > 1)
                throw new Exception("(ret.Count > 1)");

            return ret.Count == 0 ? null : ret[0];
        }

        /// <summary>
        /// Записать шапку прогностических данных и данные, если они есть.
        /// </summary>
        public int Insert(DataFcsNode0 data)
        {
            var fields = new Dictionary<string, object>()
            {
                {"catalog_id", data.CatalogId},
                {"date_ini", data.DateIni},
                {"leadtime_unit_id", data.LeadtimeUnitId}
            };
            int id = InsertWithReturn(fields);
            if (data.DataFcsNode1List != null)
            {
                data.DataFcsNode1List.ForEach(x => x.DataFcsNode0Id = id);
                DataManager.GetInstance().DataFcsNode1Repository.Insert(data.DataFcsNode1List);
            }
            return id;
        }
        /// <summary>
        /// Каскадное удаление.
        /// </summary>
        /// <param name="catalogId"></param>
        /// <param name="dateIni"></param>
        /// <param name="leadTimeUnitId"></param>
        public void Delete(int catalogId, DateTime dateIni, int leadTimeUnitId)
        {
            var fields = new Dictionary<string, object>()
            {
                {"catalog_id", catalogId},
                {"date_ini", dateIni},
                {"leadtime_unit_id", leadTimeUnitId}
            };
            Delete(fields);
        }
    }

    public class DataFcsNode1Repository : BaseRepository<DataFcsNode0.DataFcsNode1>
    {
        internal DataFcsNode1Repository(Common.ADbNpgsql db) : base(db, "data_fcs_node1") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DataFcsNode0.DataFcsNode1()
            {
                DataFcsNode0Id = (int)rdr["data_fcs_node0_id"],
                Leadtime = (double)rdr["leadtime"],
                Lat = (double)rdr["lat"],
                Lon = (double)rdr["lon"],
                Value = (double)rdr["lat"]
            };
        }

        public List<DataFcsNode0.DataFcsNode1> SelectByDataFcsNode0Id(int dataFcsNode0Id)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>()
            {
                {"data_fcs_node0_id", dataFcsNode0Id }
            };
            return Select(fields);
        }
        /// <summary>
        /// Записать шапку прогностических данных и данные, если они есть.
        /// </summary>
        public void Insert(List<DataFcsNode0.DataFcsNode1> datas)
        {

            List<Dictionary<string, object>> fields = new List<Dictionary<string, object>>();
            foreach (var data in datas)
            {
                fields.Add(
                    new Dictionary<string, object>()
                    {
                        {"data_fcs_node0_id", data.DataFcsNode0Id},
                        {"leadtime", data.Leadtime},
                        {"lat", data.Lat},
                        {"lon", data.Lon},
                        {"value", data.Value}
                    }
                );
            };
            Insert(fields);
        }
    }
}
