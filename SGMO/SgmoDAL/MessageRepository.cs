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
    public class MessageRepository
    {
        Common.ADbNpgsql _db;
        internal MessageRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public List<Message> Select(DateTime dateIni)
        {
            return Select(dateIni, dateIni);
        }
        public List<Message> Select(DateTime dateIniS, DateTime dateIniF)
        {
            List<Message> ret = new List<Message>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from warning_text where date_ini between :date_ini_s and :date_ini_f", cnn))
                {
                    cmd.Parameters.AddWithValue("date_ini_s", dateIniS);
                    cmd.Parameters.AddWithValue("date_ini_f", dateIniF);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((Message)Parse(rdr));
                        }
                    }
                }
            }
            if (ret.Count > 0)
            {
                List<FERHRI.Amur.Meta.Catalog> catalogs = FERHRI.Amur.Meta.DataManager.GetInstance().CatalogRepository.Select(ret.Select(x => x.Catalog.Id).ToList());
                foreach (var item in ret)
                {
                    item.Catalog = catalogs.First(x => x.Id == item.Catalog.Id);
                }
            }
            return ret;
        }
        public static object Parse(NpgsqlDataReader rdr)
        {
            return new Message()
            {
                Id = (int)rdr["id"],
                DateIni = (DateTime)rdr["date_ini"],
                Catalog = new Amur.Meta.Catalog() { Id = (int)rdr["catalog_id"] }, // Внимание!!! Далее нужно выбрать необходимые записи каталогов.
                MessageType = (Enums.MessageType)(int)rdr["message_type_id"],
                Text = rdr["text"].ToString(),
                SourceInfo = rdr["source_info"].ToString(),
            };
        }
        public int Insert(Message message)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into message (date_ini, catalog_id, message_type_id, text, source_info)" +
                    " values (:date_ini, :catalog_id, :message_type_id, :text, :source_info); select lastval()", cnn))
                {
                    cmd.Parameters.AddWithValue("date_ini", message.DateIni);
                    cmd.Parameters.AddWithValue("catalog_id", message.Catalog.Id);
                    cmd.Parameters.AddWithValue("message_type_id", (int)message.MessageType);
                    cmd.Parameters.AddWithValue("text", message.Text);
                    cmd.Parameters.AddWithValue("source_info", message.SourceInfo);

                    message.Id = int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            return message.Id;
        }

        public void Delete(DateTime dateIni)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("delete from message where date_ini = :date_ini", cnn))
                {
                    cmd.Parameters.AddWithValue("date_ini", dateIni);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(DateTime dateIni, int catalogId, string sourceInfo)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("delete from message where date_ini = :date_ini" +
                    " and catalog_id = :catalog_id and source_info = :source_info", cnn))
                {
                    cmd.Parameters.AddWithValue("date_ini", dateIni);
                    cmd.Parameters.AddWithValue("catalog_id", catalogId);
                    cmd.Parameters.AddWithValue("source_info", sourceInfo);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
