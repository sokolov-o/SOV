using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    public class CatalogExt
    {
        public Catalog Catalog { get; set; }

        public Site Site { get; set; }
        public Variable Variable { get; set; }
        public Method Method { get; set; }
        public OffsetType OffsetType { get; set; }
    }
}
