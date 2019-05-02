using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Sakura.Meta
{
    public class SakuraDb
    {
        public int DbListId { get; set; }
        public string ConnectionString { get; set; }
        public User User { get; set; }

        public SakuraDb(int DbListId, string ConnectionString, User User)
        {
            this.DbListId = DbListId;
            this.ConnectionString = ConnectionString;
            this.User = User;
        }
        public bool IsEqual(SakuraDb db1, SakuraDb db2)
        {
            if (db1.DbListId != db2.DbListId
                || db1.ConnectionString != db2.ConnectionString
                || !User.IsEqual(db1.User, db2.User)
                )
                return false;
            return true;
        }
    }
}
