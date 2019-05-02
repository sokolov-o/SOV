using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.SGMO
{
    /// <summary>
    /// Соответствие записей каталога и наборов градаций.
    /// </summary>
    public class WarningPileCatalog
    {
        public int CatalogId { get; set; }
        public PileSet PileSet { get; set; }
    }
}
