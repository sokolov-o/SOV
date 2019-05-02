using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.SGMO
{
    public class _DELME_DataFcs1
    {
        public int Id { get; set; }
        public int DataFcs0Id { get; set; }
        public int VaroffId { get; set; }
        public double Lag { get; set; }

        public Geo.GeoPoint Point { get; set; }
        public double Value { get; set; }

        static public void SetValue(List<_DELME_DataFcs1> data, int dataFcs0Id, Geo.GeoPoint point, double lag, int varoffId, double value)
        {
            data.Find(x => x.DataFcs0Id == dataFcs0Id && x.Point == point && x.Lag == lag && x.VaroffId == varoffId).Value = value;
        }
    }
}
