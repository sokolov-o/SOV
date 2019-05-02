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
    public class PileRepository
    {
        ADbNpgsql _db;
        internal PileRepository(ADbNpgsql db)
        {
            _db = db;
        }
        public PileSet SelectPileSet(int warn0Id)
        {
            List<PileSet> ret = SelectPileSet(new List<int>() { warn0Id });
            return ret.Count == 0 ? null : ret[0];
        }
        /// Выборка предупреждений вместе с их значениями.
        /// <summary>
        /// Выборка предупреждений вместе с их значениями.
        /// </summary>
        /// <param name="pileSetIds"></param>
        /// <returns></returns>
        public List<PileSet> SelectPileSet(List<int> pileSetIds = null)
        {
            List<PileItem> pileItems = SelectPileItemByPileSetIds(pileSetIds);

            using (var cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from pile_set where :id is null or id = any(:id)", cnn))
                {
                    cmd.Parameters.AddWithValue("id", pileSetIds);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<PileSet> ret = new List<PileSet>();
                        while (rdr.Read())
                        {
                            PileSet pileSet = new PileSet()
                            {
                                Id = (int)rdr["id"],
                                NameRus = rdr["name_rus"].ToString(),
                                NameEng = rdr["name_eng"].ToString(),
                            };
                            pileSet.PileItems = pileItems.FindAll(x => x.PileSet.Id == pileSet.Id);
                            pileSet.PileItems.ForEach(x => x.PileSet = pileSet);
                            ret.Add(pileSet);
                        }
                        return ret;
                    }
                }
            }
        }
        /// <summary>
        /// Выборка градаций для наборов.
        /// </summary>
        private List<PileItem> SelectPileItemByPileSetIds(List<int> pileSetIds = null)
        {
            using (var cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from pile_item where :pile_set_id is null or pile_set_id = any(:pile_set_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("pile_set_id", pileSetIds);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<PileItem> ret = new List<PileItem>();
                        while (rdr.Read())
                        {
                            ret.Add((PileItem)ParseData(rdr));
                        }
                        return ret;
                    }
                }
            }
        }
        public List<PileItem> SelectPileItemByIds(List<int> ids = null)
        {
            using (var cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from pile_item where id = any(:id)", cnn))
                {
                    cmd.Parameters.AddWithValue("id", ids);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<PileItem> ret = new List<PileItem>();
                        while (rdr.Read())
                        {
                            ret.Add((PileItem)ParseData(rdr));
                        }
                        return ret;
                    }
                }
            }
        }
        protected object ParseData(NpgsqlDataReader rdr)
        {
            return new PileItem()
            {
                Id = (int)rdr["id"],
                NameRus = rdr["name_rus"].ToString(),
                NameEng = rdr["name_eng"].ToString(),
                TimeId = (int)rdr["time_id"],
                PileSet = new PileSet() { Id = (int)rdr["pile_set_id"] },
                OrderBy = (short)rdr["orderby"],

                Condition1 = rdr["condition1"].ToString(),
                Value1 = (double)rdr["value1"],
                Condition2 = ADbNpgsql.GetValueString(rdr, "condition2"),
                Value2 = ADbNpgsql.GetValueDouble(rdr, "value2")
            };
        }

    }
}
