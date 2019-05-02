using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.SGMO
{
    /// <summary>
    /// Предупреждения для точек.
    /// </summary>
    public class WarningGeoPoint
    {
        public Geo.GeoPoint GeoPoint { get; set; }
        public PileItemValues PileItemValues { get; set; }
    }
}
