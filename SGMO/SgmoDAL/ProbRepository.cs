using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Amur.Meta;

using Npgsql;

namespace FERHRI.SGMO
{
    public class ProbRepository : BaseRepository<Prob>
    {
        internal ProbRepository(ADbNpgsql db) : base(db, "public.prob2d") { }

        static public Dictionary<string, object> GetFieldDictionary(Prob item, bool withId)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>()
            {
                {"pile_set_id_2", item.PileSet2.Id},
                {"pile_set_id_1", item.PileSet1.Id},
                {"unit_id_time", item.UnitTime.Id}
            };
            if (withId)
                ret.Add("id", item.Id);
            return ret;
        }

        public Prob SelectByPileSetIds(int pileSet2Id, int pileSet1Id, int unitTimeId, bool withFK)
        {
            List<Prob> ret = ExecQuery<Prob>(
                "select * from " + TableName + " where pile_set_id_2 = :pile_set_id_2 and pile_set_id_1 = :pile_set_id_1 and unit_id_time = :unit_id_time",
                new Dictionary<string, object>()
                {
                {"pile_set_id_2", pileSet2Id},
                {"pile_set_id_1", pileSet1Id},
                {"unit_id_time", unitTimeId}
                },
                ParseData);
            if (ret != null && withFK)
            {
                return SelectFK(ret)[0];
            }
            return null;
        }

        public List<Prob> SelectFK(List<Prob> ret)
        {
            List<PileSet> pss = DataManager.GetInstance().PileRepository.SelectPileSet(ret.Select(x => x.PileSet2.Id).Union(ret.Select(x => x.PileSet1.Id)).ToList());
            ret.ForEach(x => x.PileSet2 = pss.Find(y => y.Id == x.PileSet2.Id));
            ret.ForEach(x => x.PileSet1 = pss.Find(y => y.Id == x.PileSet1.Id));

            List<Unit> us = FERHRI.Amur.Meta.DataManager.GetInstance().UnitRepository.Select();
            ret.ForEach(x => x.UnitTime = us.Find(y => y.Id == x.UnitTime.Id));

            List<Prob.Catalog> ctls = DataManager.GetInstance().ProbCatalogRepository.SelectByProbs(ret, true);
            ret.ForEach(x => x.Catalogs = ctls.FindAll(y => y.Prob.Id == x.Id));

            return ret;
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Prob()
            {
                Id = (int)rdr["id"],
                PileSet2 = new PileSet() { Id = (int)rdr["pile_set_id_2"] },
                PileSet1 = new PileSet() { Id = (int)rdr["pile_set_id_1"] },
                Catalogs = null,
                UnitTime = new Unit((int)rdr["unit_id_time"])
            };
        }

        public int Insert(Prob item)
        {
            item.Id = InsertWithReturn(GetFieldDictionary(item, false));
            if (item.Catalogs != null)
            {
                ProbCatalogRepository rep = DataManager.GetInstance().ProbCatalogRepository;
                foreach (var catalog in item.Catalogs)
                {
                    rep.Insert(catalog);
                }
            }
            return item.Id;
        }
    }
}
