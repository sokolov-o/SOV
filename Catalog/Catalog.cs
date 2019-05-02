using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV
{
    /// <summary>
    /// 
    /// Запись каталога данных из произвольной базы данных:
    /// 
    /// - Sakura.Hmdic.Catalog
    /// - amur.catalog
    /// - etc.
    /// 
    /// </summary>
    public class Catalog : IdName
    {
        public object NativeCatalog { get; set; }
    }
}
