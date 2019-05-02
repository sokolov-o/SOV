using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using SOV.Amur.Meta;

namespace SOV.SGMO
{
    /// <summary>
    /// 1D и 2D повторяемости значений по градациям.
    /// 
    /// OSokolov@SOV.ru, 20171101
    /// </summary>
    public class Prob : IdClass
    {
        public PileSet PileSet1 { get; set; }
        public PileSet PileSet2 { get; set; }
        public Unit UnitTime { get; set; }

        public List<Prob.Catalog> Catalogs { get; set; }

        public Prob()
        {
            Catalogs = new List<Prob.Catalog>();
        }

        public class Catalog
        {
            public int Id { get; set; }
            public Prob Prob { get; set; }
            public SOV.Amur.Meta.Catalog Catalog1 { get; set; }
            public SOV.Amur.Meta.Catalog Catalog2 { get; set; }
            public DateTime Date { get; set; }
            /// <summary>
            /// Кол. элементов в подсчёте повторяемостей по градациям.
            /// </summary>
            public int Count { get; set; }

            public List<Pile> Piles { get; set; }

            public class Pile
            {
                public Prob.Catalog ProbCatalog { get; set; }
                public PileItem PileItem1 { get; set; }
                public PileItem PileItem2 { get; set; }
                public int Count { get; set; }
            }
        }

    }
}
