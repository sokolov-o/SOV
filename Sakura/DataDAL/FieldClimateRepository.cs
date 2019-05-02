using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Sakura.Grib;
using FERHRI.Sakura.Meta;

namespace FERHRI.Sakura.Data
{
    public class FieldClimateRepository : ADbMSSql
    {
        public FieldClimateRepository(ADbMSSql db, string dataTableName = "data")
            : base(db.ConnectionString, db.DbListId)
        {
        }
        public List<ClimateHeader> SelectHeader(Catalog catalog, int? yearS = null, int? yearF = null, string hours = "-1")
        {
            List<ClimateHeader> ret = new List<ClimateHeader>();
            using (var cnn = Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select * from data0 where ctlId=@ctlId"
                    + " and (@hours is null or hours=@hours)"
                    + " and (@yearS is null or yearSClm=@yearS)"
                    + " and (@yearF is null or yearFClm=@yearF)", cnn))
                {
                    cmd.Parameters.AddWithValue("@ctlId", catalog.Id);
                    AddParameter(cmd, "@hours", hours);
                    AddParameter(cmd, "@yearS", yearS);
                    AddParameter(cmd, "@yearF", yearF);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new ClimateHeader()
                                {
                                    Id = (int)rdr["id"],
                                    Catalog = catalog,
                                    YearS = (int)rdr["yearsclm"],
                                    YearF = (int)rdr["yearfclm"],
                                    TimeNums = rdr["hours"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            return ret;
        }

        public List<ClimateData> SelectData(ClimateHeader climateHeader, int? timeNum = null)
        {
            List<ClimateData> ret = new List<ClimateData>();
            using (var cnn = Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select value, timeNum, countProc from data1 where data0Id = @data0Id"
                + " and (@timeNum is null or timeNum = @timeNum)", cnn))
                {
                    cmd.Parameters.AddWithValue("@data0Id", climateHeader.Id);
                    ADbMSSql.AddParameter(cmd, "@timeNum", timeNum);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        double[] field;
                        while (rdr.Read())
                        {
                            byte[] buf = new byte[rdr.GetBytes(0, 0, null, 0, 0)];
                            if (rdr.GetBytes(0, 0, buf, 0, buf.Length) != buf.Length)
                                throw new Exception("ALGORITHMIC ERROR.");

                            switch (climateHeader.Catalog.FormatId)
                            {
                                case (int)FERHRI.Sakura.Meta.Enums.Format.GRIDasFLOAT:
                                    field = Support.ByteFloat2Double(buf, (int)FERHRI.Sakura.Meta.Enums.ByteOrder.BIG_ENDIAN);
                                    break;
                                case (int)FERHRI.Sakura.Meta.Enums.Format.GRIB1:
                                    field = (new Grib1(buf)).unpackDouble();
                                    break;
                                default:
                                    throw new Exception("Неизвестный климатического формат поля. Catalog [" + climateHeader.Catalog + "]");
                            }
                            ret.Add(new ClimateData()
                                {
                                    ClimateHeader = climateHeader,
                                    TmeNum = (int)rdr["timeNum"],
                                    Count = (double)rdr["countProc"],
                                    Value = field
                                });
                        }
                    }
                }
            }
            return ret;
        }
    }
}
