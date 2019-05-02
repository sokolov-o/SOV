using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using FERHRI.Amur.Meta;
using Npgsql;

namespace FERHRI.SGMO
{
    public class MessageSiteRepository : BaseRepository<MessageSite>
    {
        internal MessageSiteRepository(Common.ADbNpgsql db) : base(db, "message_site") { }

        public List<MessageSite> Select(DateTime dateIni, bool isLastMessageOnly, List<int> sitesId, int? methodId,
            Enums.MessageType? messageType, EnumLanguage? lang, EnumDateType? dateTypeFcs)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>()
            {
                {"date_ini", dateIni },
                {"site_id",sitesId},
                {"method_id",methodId},
                {"message_type_id", !messageType.HasValue?(int?)null:(int)messageType},
                {"lang_id",!lang.HasValue?(int?)null:(int)lang},
                {"date_type_id_fcs",!dateTypeFcs.HasValue?(int?)null:(int)dateTypeFcs}
            };

            List<MessageSite> ret = Select(fields);

            if (ret.Count > 0 && isLastMessageOnly)
            {
                List<MessageSite> ret1 = new List<MessageSite>();
                foreach (var siteId in ret.Select(x => x.SiteId).Distinct())
                {
                    int maxId = ret.FindAll(x => x.SiteId == siteId).Max(x => x.Id);
                    ret1.Add(ret.First(x => x.Id == maxId));
                }
                ret = ret1;
            }
            return ret;
            //List<MessageSite> ret = new List<MessageSite>();

            //using (NpgsqlConnection cnn = _db.Connection)
            //{
            //    using (NpgsqlCommand cmd = new NpgsqlCommand(
            //        "select * from message_site" +
            //        " where date_ini = :date_ini)" +
            //        " and (:method_id is null or method_id = any(:method_id))" +
            //        " and (:site_id is null or site_id = any(:site_id))" +
            //        " and (:message_type_id is null or message_type_id = :message_type_id)" +
            //        " and (:date_type_id_fcs is null or date_type_id_fcs = :date_type_id_fcs)" +
            //        " and (:lang_id is null or lang_id = :lang_id)", cnn))
            //    {
            //        cmd.Parameters.AddWithValue("date_ini", dateIni);
            //        cmd.Parameters.AddWithValue("method_id", methodId);
            //        cmd.Parameters.AddWithValue("site_id", sitesId);
            //        cmd.Parameters.AddWithValue("message_type_id", messageType);
            //        cmd.Parameters.AddWithValue("date_type_id_fcs", dateTypeFcs);
            //        cmd.Parameters.AddWithValue("lang_id", lang);

            //        using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                ret.Add((MessageSite)Parse(rdr));
            //            }
            //        }
            //    }
            //}

            //    if (isLastMessageOnly) throw new NotImplementedException("isLastMessageOnly");

            //    return ret;
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new MessageSite()
            {
                Id = (int)rdr["id"],
                MethodId = (int)rdr["method_id"],
                MessageType = (Enums.MessageType)(int)rdr["message_type_id"],
                Language = (Amur.Meta.EnumLanguage)(int)rdr["lang_id"],
                SiteId = (int)rdr["site_id"],

                DateIni = (DateTime)rdr["date_ini"],
                DateFcs = (DateTime)rdr["date_fcs"],
                DateTypeFcs = (EnumDateType)rdr["date_type_id_fcs"],
                Text = rdr["text"].ToString(),
                DateInsert = (DateTime)rdr["date_insert"]
            };
        }
        public int Insert(MessageSite message)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into message_site (method_id, message_type_id, lang_id, site_id, date_ini, date_fcs, date_type_id_fcs, text)" +
                    " values (:method_id, :message_type_id, :lang_id, :site_id, :date_ini, :date_fcs, :date_type_id_fcs, :text); select lastval()", cnn))
                {
                    cmd.Parameters.AddWithValue("method_id", (int)message.MethodId);
                    cmd.Parameters.AddWithValue("message_type_id", (int)message.MessageType);
                    cmd.Parameters.AddWithValue("lang_id", (int)message.Language);
                    cmd.Parameters.AddWithValue("site_id", (int)message.SiteId);
                    cmd.Parameters.AddWithValue("date_ini", message.DateIni);
                    cmd.Parameters.AddWithValue("date_fcs", message.DateFcs);
                    cmd.Parameters.AddWithValue("date_type_id_fcs", (int)message.DateTypeFcs);
                    cmd.Parameters.AddWithValue("text", message.Text);

                    message.Id = int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            return message.Id;
        }
    }
}
