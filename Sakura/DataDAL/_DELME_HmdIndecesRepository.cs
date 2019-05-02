using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using Viaware.Sakura.Grib;
using Viaware.Sakura.Meta;

namespace Viaware.Sakura.Data
{
    public class _DELME_HmdIndecesRepository : IDbHCR
    {
        ADbMSSql _db;

        public _DELME_HmdIndecesRepository(ADbMSSql db, string dataTableName = "data")
        {
            _db = db;
        }

        public List<DataHCR> Select(List<Catalog> catalogs)
        {
            return Select(catalogs, null, null, null, true, false, false);
        }
        public List<DataHCR> Select(List<Catalog> catalogs, DateTime dateS, DateTime dateF)
        {
            return Select(catalogs, dateS, dateF, null, true, false, false);
        }
        public List<DataHCR> Select(List<Catalog> catalogs, DateTime? dateS, DateTime? dateF,
            int? dbListIdSrc,
            bool isMaxVersion, bool isShowBadValues, bool isShowFKName)
        {
            if (dbListIdSrc != null || isMaxVersion != true || isShowBadValues != false || isShowFKName != false)
                throw new Exception("HmdIndecesRepository: (dbListIdSrc != null || isMaxVersion != true || isShowBadValues != false || isShowFKName == true)");

            List<DataHCR> ret = new List<DataHCR>();

            using (var cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    ADbMSSql.AddParameter(cmd, "@dateS", dateS);
                    ADbMSSql.AddParameter(cmd, "@dateF", dateF);

                    foreach (Catalog catalog in catalogs)
                    {
                        cmd.CommandText = GetSqlSelect(catalog, dateS, dateF);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                ret.Add(new DataHCR()
                                {
                                    Id = -1,
                                    CatalogId = (int)rdr["ctlid"],
                                    Date = (DateTime)rdr["date"],
                                    Value = (double)rdr["value"],
                                    QCL = 0,
                                    Version = 0,
                                    DbListIdSrc = 0,
                                    UserId = 0,
                                });
                            }
                        }
                    }
                    return ret;
                }
            }
        }
        string GetSqlSelect(Catalog catalog, DateTime? dateS, DateTime? dateF)
        {
            string ret = StrVia.GetValue(catalog.DataCnn, "RS");
            if (ret != null)
            {
                ret = catalog.ReplaceSqlAttr(ret);
                if (dateS.HasValue)
                    ret += ((ret.ToLower().IndexOf(" where ") >= 0) ? " and " : " where ") + "date between @dateS and @dateF";
                ret = "select hmdid ctlid, date, value from (" + ret + ") t"; // Для порядка полей
            }
            else
            {
                ret = "select ctlId,[date],[value] from data where ctlId=" + catalog.Id +
                    ((dateS.HasValue) ? " and date between @dateS and @dateF order by date" : "");
            }
            return ret;
        }
    }
}
