using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Analog
{
    /// <summary>
    /// Связь между полем, его регионами и метрикой сходства.
    /// </summary>
    public class FieldGeoobs : IdClass
    {
        public int FieldId { get; set; }
        public int GeoobMetricId { get; set; }
    }
}
