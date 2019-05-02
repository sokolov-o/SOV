using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.SGMO.WaveProb
{
    public class GeoPoint
    {
        public double Lati { get; set; }
        public double Longi { get; set; }

        public GeoPoint(double lati, double longi)
        {
            this.Lati = lati;
            this.Longi = longi;
        }

        
    }
}
