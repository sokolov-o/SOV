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
    public class ProbCatalogPileRepository : BaseRepository<Prob.Catalog.Pile>
    {
        internal ProbCatalogPileRepository(ADbNpgsql db) : base(db, "public.prob2d_pile") { }

        public List<Prob.Catalog.Pile> SelectByCatalogs(List<Prob.Catalog> catalogs)
        {
            List<Prob.Catalog.Pile> ret = ExecQuery<Prob.Catalog.Pile>(
                "select * from " + TableName + " where prob2d_catalog_id = any(:prob2d_catalog_id)",
                new Dictionary<string, object>() { { "prob2d_catalog_id", catalogs.Select(x => x.Id).ToList() } },
                ParseData);

            ret.ForEach(x => x.ProbCatalog = catalogs.First(y => y.Id == x.ProbCatalog.Id));
            return SelectFK(ret);
        }
        public List<Prob.Catalog.Pile> SelectFK(List<Prob.Catalog.Pile> ret)
        {
            List<PileItem> pis = DataManager.GetInstance().PileRepository.SelectPileItemByIds(ret.Select(x => x.PileItem1.Id).Union(ret.Select(x => x.PileItem2.Id)).ToList());
            ret.ForEach(x => x.PileItem1 = pis.Find(y => y.Id == x.PileItem1.Id));
            ret.ForEach(x => x.PileItem2 = pis.Find(y => y.Id == x.PileItem2.Id));

            return ret;
        }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Prob.Catalog.Pile()
            {
                ProbCatalog = new Prob.Catalog() { Id = (int)rdr["prob2d_catalog_id"] },
                PileItem1 = new PileItem() { Id = (int)rdr["pile_item_id_1"] },
                PileItem2 = new PileItem() { Id = (int)rdr["pile_item_id_2"] },
                Count = (int)rdr["count_in_pile"]
            };
        }
        public void Insert(Prob.Catalog.Pile item)
        {
            Insert(GetFieldDictionary(item));
        }
        public void Update(Prob.Catalog.Pile item)
        {
            Update
            (
                new List<Dictionary<string, object>>() { GetFieldDictionary(item) },
                new List<string>() { { "prob2d_catalog_id" }, { "pile_item_id_1" }, { "pile_item_id_2" } }
            );
        }
        public void DeleteByPileCatalogId(int pileCatalogId)
        {
            Delete(new List<Dictionary<string, object>>()
            {
                new Dictionary<string, object>() { {"prob2d_catalog_id", pileCatalogId} }
            });
        }
        static public Dictionary<string, object> GetFieldDictionary(Prob.Catalog.Pile item)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>()
            {
                {"prob2d_catalog_id", item.ProbCatalog.Id},
                {"pile_item_id_1", item.PileItem1.Id},
                {"pile_item_id_2", item.PileItem2.Id},
                {"count_in_pile", item.Count}
            };
            return ret;
        }
        public Prob.Catalog.Pile Select(int probCatalogId, int pileItem1Id, int pileItem2Id)
        {
            List<Prob.Catalog.Pile> ret = Select(
                new Dictionary<string, object>()
                {
                    {"prob2d_catalog_id", probCatalogId},
                    {"pile_item_id_1", pileItem1Id},
                    {"pile_item_id_2", pileItem2Id}
                }
            );
            if (ret.Count == 0) return null;
            else if (ret.Count == 1) return ret[0];

            throw new Exception("Нарушение структуры уникального ключа при чтении.");
        }
    }
}
