using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Geo;

namespace SOV.DB.FileVanWave
{
    public class WaveFileAttr
    {
        public Grid Grid { get; set; }
        public List<double> LeadTimes { get; set; }
    }
}
