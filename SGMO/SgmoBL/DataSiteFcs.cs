using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.SGMO
{
    public class DataFcs : IdClass
    {
        public DataFcs() { }
        public DataFcs(DataFcs dataFcs)
        {
            Id = dataFcs.Id;
            CatalogId = dataFcs.CatalogId;
            DateIniUTC = dataFcs.DateIniUTC;
            LeadTime = dataFcs.LeadTime;
            UTCOffsetHours = dataFcs.UTCOffsetHours;
            Value = dataFcs.Value;
        }

        public int CatalogId { get; set; }
        public DateTime DateIniUTC { get; set; }
        public double LeadTime { get; set; }
        public double Value { get; set; }
        public int UTCOffsetHours { get; set; }
    }
}
