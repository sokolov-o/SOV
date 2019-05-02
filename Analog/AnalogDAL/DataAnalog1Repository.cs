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
    public class DataAnalog1Repository : BaseRepository<DataAnalog0.DataAnalog1>
    {
        internal DataAnalog1Repository(Common.ADbNpgsql db)
            : base(db, "data.ang_1")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DataAnalog0.DataAnalog1
            {
                DataAnalog0Id = (int)rdr["ang_0_id"],
                DateAnalog = (DateTime)rdr["date_ang"],
                Weight = (double)rdr["weight"]
            };
        }
        internal void SelectForDataAnalog0(List<DataAnalog0> dataAnalog0)
        {
            List<DataAnalog0.DataAnalog1> d1 = Select(new Dictionary<string, object>() { { "ang_0_id", dataAnalog0.Select(x => x.Id).ToList() } });
            dataAnalog0.ForEach(x => x.DateWeightAnalogs = d1.FindAll(y => y.DataAnalog0Id == x.Id));
        }
        internal int Insert(DataAnalog0.DataAnalog1 item)
        {
            return InsertWithReturn(new Dictionary<string, object>()
            {
                {"ang_0_id", item.DataAnalog0Id},
                {"date_ang", item.DateAnalog},
                {"weight", item.Weight}
            });
        }
    }
}
