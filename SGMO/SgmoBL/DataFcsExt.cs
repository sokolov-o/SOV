using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;

namespace SOV.SGMO
{
    public class DataFcsExt : DataFcs
    {
        public CatalogExt CatalogExt { get; set; }

        public DateTime DateFcsUTC { get { return DateIniUTC.AddHours(LeadTime); } }
        public DateTime DateFcsLOC { get { return DateFcsUTC.AddHours(UTCOffsetHours); } }

        public string SiteName { get { return CatalogExt.Site.Name; } }
        public string VariableName { get { return CatalogExt.Variable.NameRus; } }
        public string MethodName { get { return CatalogExt.Method.Name; } }
        public string OffsetTypeName { get { return CatalogExt.OffsetType.Name; } }
        public double OffsetValue { get { return CatalogExt.Catalog.OffsetValue; } }
    }
}
