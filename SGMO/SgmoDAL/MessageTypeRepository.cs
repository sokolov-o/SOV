using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using SOV.Amur.Meta;
using Npgsql;

namespace SOV.SGMO
{
    public class MessageTypeRepository : BaseRepository<MessageType>
    {

        internal MessageTypeRepository(Common.ADbNpgsql db) : base(db, "message_type") { }

        public static List<MessageType> GetCash()
        {
            return GetCash(DataManager.GetInstance().MessageTypeRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new MessageType()
            {
                Id = (int)rdr["id"],
                Name = rdr["name"].ToString(),
                Description = rdr["description"].ToString()
            };
        }
    }
}
