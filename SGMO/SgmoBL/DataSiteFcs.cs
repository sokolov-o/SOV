using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.SGMO
{
    public class DataSiteFcs : IdClass
    {
        public int CatalogId { get; set; }
        public DateTime DateIniUTC { get; set; }
        public double LeadTime { get; set; }
        public double Value { get; set; }
    }
}
