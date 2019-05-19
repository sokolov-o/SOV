using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.SGMO
{
    public class DataTrackFcs : DataFcs
    {
        public int TrackPointId { get; set; }
        //public int CatalogId { get; set; }
        //public double LeadTime { get; set; }
        //public double Value { get; set; }
        public DateTime DateInsert { get; set; }
    }
}
