#define trace

using GeographicLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.SGMO
{
    public class TrackPart : IdClass
    {
        public int TrackId { get; set; }
        public DateTime DateSUTC { get; set; }

        public List<TrackPartPoint> TrackPartPoints { get; set; }
    }
}
