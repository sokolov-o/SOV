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
    public class _DELME_FieldGeoobsView : Common.IdName
    {
        public _DELME_FieldGeoobs FieldGeoobs { get; set; }
        public Field Field { get; set; }
        public _DELME_GeoobsMetric GeoobsMetric { get; set; }

        public override string ToString()
        {
            return Field.Name + "=" + GeoobsMetric.Name;
        }
    }
}
