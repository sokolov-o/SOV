using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using SOV.Amur.Meta;

using Npgsql;

namespace SOV.SGMO
{
    public class ProbCatalogRepository : BaseRepository<Prob.Catalog>
    {
        internal ProbCatalogRepository(ADbNpgsql db) : base(db, "public.prob2d_catalog") { }

        public List<Prob.Catalog> SelectByProbs(List<Prob> probs, bool withFK)
        {
            List<Prob.Catalog> ret = ExecQuery<Prob.Catalog>(
                "select * from " + TableName + " where prob2d_id = any(:prob2d_id)",
                new Dictionary<string, object>()
                {
                {"prob2d_id", probs.Select(x=>x.Id).ToList()},
                },
                ParseData);
            if (ret != null && withFK)
            {
                ret.ForEach(x => x.Prob = probs.First(y => y.Id == x.Id));
                return SelectFK(ret);
            }
            return ret;
        }

        internal List<Prob.Catalog> SelectFK(List<Prob.Catalog> ret)
        {
            List<Amur.Meta.Catalog> ctls = Amur.Meta.DataManager.GetInstance().CatalogRepository.Select(ret.Select(x => x.Catalog2.Id).Union(ret.Select(x => x.Catalog1.Id)).ToList());
            ret.ForEach(x => x.Catalog2 = ctls.Find(y => y.Id == x.Catalog2.Id));
            ret.ForEach(x => x.Catalog1 = ctls.Find(y => y.Id == x.Catalog1.Id));

            List<Prob.Catalog.Pile> piles = DataManager.GetInstance().ProbCatalogPileRepository.SelectByCatalogs(ret);
            ret.ForEach(x => x.Piles = piles.FindAll(y => y.ProbCatalog.Id == x.Id));

            return ret;
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Prob.Catalog()
            {
                Id = (int)rdr["id"],
                Catalog1 = new Amur.Meta.Catalog() { Id = (int)rdr["catalog_id_1"] },
                Catalog2 = new Amur.Meta.Catalog() { Id = (int)rdr["catalog_id_2"] },
                Prob = new Prob() { Id = (int)rdr["prob2d_id"] },
                Date = (DateTime)rdr["date"],
                Count = (int)rdr["count_total"]
            };
        }
        /// <summary>
        /// Вставка записи таблицы каталогов блока prob.
        /// </summary>
        /// <param name="item">Каталог для вставки.</param>
        /// <returns>Код созданной записи, который присваивается и члену Id вставляемого экземпляра класса.</returns>
        public int Insert(Prob.Catalog item)
        {
            item.Id = InsertWithReturn(GetFieldDictionary(item, false));
            if (item.Piles != null)
            {
                ProbCatalogPileRepository rep = DataManager.GetInstance().ProbCatalogPileRepository;
                foreach (var pile in item.Piles)
                {
                    rep.Insert(pile);
                }
            }
            return item.Id;

        }
        public Prob.Catalog Select(int probId, int catalog1Id, int catalog2Id, DateTime date)
        {
            List<Prob.Catalog> ret = Select(
                new Dictionary<string, object>()
                {
                    {"prob2d_id", probId},
                    {"catalog_id_1", catalog1Id},
                    {"catalog_id_2", catalog2Id},
                    {"date", date}
                }
            );
            if (ret.Count == 0) return null;
            else if (ret.Count == 1) return ret[0];

            throw new Exception("Нарушение структуры уникального ключа при чтении.");
        }
        public void Update(Prob.Catalog item)
        {
            Update(GetFieldDictionary(item, true));
        }

        static public Dictionary<string, object> GetFieldDictionary(Prob.Catalog item, bool withId)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>()
            {
                {"prob2d_id", item.Prob.Id},
                {"catalog_id_2", item.Catalog2.Id},
                {"catalog_id_1", item.Catalog1.Id},
                {"date", item.Date},
                {"count_total", item.Count}
            };
            if (withId)
                ret.Add("id", item.Id);
            return ret;
        }

    }
}
