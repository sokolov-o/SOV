using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI
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
