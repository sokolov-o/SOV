using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;
using System.Text.RegularExpressions;

namespace FERHRI.Analog
{
    public class FieldRepository : BaseRepository<Field>
    {
        internal FieldRepository(Common.ADbNpgsql db) : base(db, "task.field") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Field()
            {
                Id = (int)rdr["id"],
                Name = rdr["name"].ToString(),
                CatalogId = (int)rdr["catalog_id"],
                CatalogDbInterfaceId = (int)rdr["catalog_db_interface_id"],
            };
        }
        public int Insert(Field item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"name", item.Name},
                {"catalog_id", item.CatalogId},
                {"catalog_db_interface_id", item.CatalogDbInterfaceId},
            };
            return InsertWithReturn(fields);
        }
        public void Update(Field item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name},
                {"catalog_id", item.CatalogId},
                {"catalog_db_interface_id", item.CatalogDbInterfaceId},
            };
            try
            {
                string sql = GetUpdateQuery(new List<Dictionary<string, object>>() { fields }, new List<string>() { "id" });
                using (NpgsqlConnection cnn = _db.Connection)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("", cnn))
                    {
                        foreach (var field in fields)
                        {
                            string key = field.Key.Replace("\"", "");
                            Regex reg = new Regex("([=|\\s|,|\\(]+:)" + key + "([\\s|;|,|\\)]+|$)");
                            sql = reg.Replace(sql, "${1}" + key + 0 + "${2}");
                            cmd.Parameters.AddWithValue(key + 0, field.Value ?? DBNull.Value);
                        }
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Data.Common.DbException e)
            {
                throw new RuDbException(e);
            }

        }
    }
}
