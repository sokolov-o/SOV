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
    public class WarningPileCatalogRepository
    {
        ADbNpgsql _db;
        internal WarningPileCatalogRepository(ADbNpgsql db)
        {
            _db = db;
        }
        /// <summary>
        /// Выборка предупреждений для записи каталога данных.
        /// </summary>
        public WarningPileCatalog Select(int catalogId)
        {
            List<WarningPileCatalog> ret = Select(new List<int>() { catalogId });
            return ret == null || ret.Count == 0 ? null : ret[0];
        }
        public List<WarningPileCatalog> Select(List<int> catalogsId)
        {
            List<WarningPileCatalog> ret = new List<WarningPileCatalog>();
            using (var cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from warning_pile_catalog where catalog_id = any(:catalog_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("catalog_id", catalogsId);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new WarningPileCatalog() { CatalogId = (int)rdr["catalog_id"], PileSet = new PileSet() { Id = (int)rdr["pile_set_id"] } });
                        }
                    }
                }
            }
            if (ret != null)
            {
                List<PileSet> pileSets = DataManager.GetInstance().PileRepository.SelectPileSet(ret.Select(x => x.PileSet.Id).Distinct().ToList());
                ret.ForEach(x => x.PileSet = pileSets.Find(y => y.Id == x.PileSet.Id));
            }
            return ret;
        }
    }
}
