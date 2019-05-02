using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.SGMO
{
    public class PointLag
    {
        public Geo.GeoPoint Point { get; set; }
        public double Lag { get; set; }

        public override string ToString()
        {
            return (Point == null ? "null" : Point.ToString()) + ";" + Lag;
        }
    }
}
