using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.SGMO
{
    public class SiteRepository : Common.BaseRepository<SiteRepository.Id>
    {
        internal SiteRepository(Common.ADbNpgsql db) : base(db, "site")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Id()
            {
                Id = (int)rdr["id"]
            };
        }
        public List<Amur.Meta.Site> SelectAmurSites()
        {
            List<Id> ids = base.Select();
            return Amur.Meta.DataManager.GetInstance().SiteRepository.Select(ids.Select(x=>x.Id).ToList());
        }

        public class Id : IdClass { }
    }
}
