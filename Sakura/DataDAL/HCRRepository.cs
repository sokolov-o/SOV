using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
//using Viaware.Sakura.Grib;
using FERHRI.Sakura.Meta;

namespace FERHRI.Sakura.Data
{
    public class HCRRepository : IDbHCR
    {
        ADbMSSql _db;

        public HCRRepository(ADbMSSql db, string dataTableName = "data")
        {
            _db = db;
        }

        public List<DataHCR> Select(List<int> catalogId)
        {
            return Select(catalogId, null, null, null, true, false, false);
        }
        public List<DataHCR> Select(List<int> catalogId, DateTime dateS, DateTime dateF)
        {
            return Select(catalogId, dateS, dateF, null, true, false, false);
        }
        public List<DataHCR> Select(List<int> catalogId, DateTime? dateS, DateTime? dateF,
            int? dbListIdSrc,
            bool isMaxVersion, bool isShowBadValues, bool isShowFKName)
        {
            using (var cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand("exec [dbo].[spDataSelect] @inCtlId, @dateS, @dateF,  @dbListIdSrc, @isMaxVersion, @isShowBadValues, @isShowFKName", cnn))
                {
                    ADbMSSql.AddParameter(cmd, "@inCtlId", StrVia.ToString(catalogId));
                    ADbMSSql.AddParameter(cmd, "@dateS", dateS);
                    ADbMSSql.AddParameter(cmd, "@dateF", dateF);
                    ADbMSSql.AddParameter(cmd, "@dbListIdSrc", dbListIdSrc);
                    ADbMSSql.AddParameter(cmd, "@isMaxVersion", isMaxVersion);
                    ADbMSSql.AddParameter(cmd, "@isShowBadValues", isShowBadValues);
                    ADbMSSql.AddParameter(cmd, "@isShowFKName", isShowFKName);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<DataHCR> ret = new List<DataHCR>();
                        while (rdr.Read())
                        {
                            ret.Add(new DataHCR()
                            {
                                Id = (long)rdr["id"],
                                CatalogId = (int)rdr["ctlId"],
                                Date = (DateTime)rdr["date"],
                                Value = (double)rdr["value"],
                                QCL = (FERHRI.Sakura.Meta.Enums.QCL)(byte)rdr["qcl"],
                                Version = (byte)rdr["version"],
                                DbListIdSrc = (int)rdr["dbListIdSrc"],
                                UserId = (short)rdr["userId"],
                            });
                        }
                        return ret;
                    }
                }
            }
        }
    }
}
