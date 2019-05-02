using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.SGMO
{
    public class Track1
    {
        public int Id { get; set; }
        public int Track0Id { get; set; }
        public DateTime Date { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int UTCOffset { get; set; }

        public override string ToString()
        {
            return Date + " (" + UTCOffset + ") " + Lat + " x " + Lon + Track0Id;
        }

    }
}
