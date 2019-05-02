using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Analog
{
    public class TaskSelectAnalog:Common.IdClass
    {
        /// <summary>
        /// amur.method.id
        /// </summary>
        public int MethodId { get; set; }
        /// <summary>
        /// amur.unit.id
        /// </summary>
        public int TimeId { get; set; }
    }
}
