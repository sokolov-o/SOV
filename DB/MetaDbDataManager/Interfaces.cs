using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.MetaDb
{
    public interface ICatalog
    {
        Catalog GetCatalog(int catalogId);
    }
}
