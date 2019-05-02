using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.SGMO
{
    public class DataFcsNode0 : IdClass
    {
        public int CatalogId { get; set; }
        public int LeadtimeUnitId { get; set; }
        public DateTime DateIni { get; set; }
        public DateTime DateIsert { get; set; }

        // FK
        public List<DataFcsNode1> DataFcsNode1List;

        public class DataFcsNode1
        {
            public int DataFcsNode0Id { get; set; }
            public double Leadtime { get; set; }
            public double Lat { get; set; }
            public double Lon { get; set; }
            public double Value { get; set; }
        }
    }
}
