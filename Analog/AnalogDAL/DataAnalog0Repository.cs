using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Analog
{
    public class DataAnalog0Repository : BaseRepository<DataAnalog0>
    {
        internal DataAnalog0Repository(Common.ADbNpgsql db)
            : base(db, "data.ang_0")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DataAnalog0()
            {
                Id = (int)rdr["id"],
                SelectAnalogId = (int)rdr["select_ang_id"],
                DateIni = (DateTime)rdr["date_ini"]
            };
        }
        /// <summary>
        /// Выборка вместе с DataAnalog1.
        /// </summary>
        /// <param name="selectAnalogId"></param>
        /// <param name="dateIni"></param>
        /// <returns></returns>
        public List<DataAnalog0> SelectBySelectAnalogId(int selectAnalogId, DateTime? dateIni)
        {
            List<DataAnalog0> ret = Select(new Dictionary<string, object>() { { "select_ang_id", selectAnalogId }, { "date_ini", dateIni } });
            if (ret.Count > 0)
                DataManager.GetInstance().DataAnalog1Repository.SelectForDataAnalog0(ret);
            return ret;
        }
        /// <summary>
        /// Выборка вместе с DataAnalog1.
        /// </summary>
        public DataAnalog0 SelectBySelectAnalogId(int selectAnalogId, DateTime dateIni)
        {
            List<DataAnalog0> ret = SelectBySelectAnalogId(selectAnalogId, (DateTime?)dateIni);
            return ret.Count == 0 ? null : ret[0];
        }
        public int Insert(DataAnalog0 item)
        {
            return InsertWithReturn(new Dictionary<string, object>()
            {
                {"select_ang_id", item.SelectAnalogId},
                {"date_ini", item.DateIni}
            });
        }
    }
}
