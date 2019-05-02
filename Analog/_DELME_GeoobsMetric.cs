using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Analog
{
    public class _DELME_GeoobsMetric :IdName
    {
        /// <summary>
        /// Метрика сходства полей в регионах.
        /// </summary>
        public int MetricMathvarId { get; set; }
        /// <summary>
        /// Регионы, для которых рассчитывается метрика сходства.
        /// </summary>
        public IntDouble[] GeoobIdWeights { get; set; }
    }
}
