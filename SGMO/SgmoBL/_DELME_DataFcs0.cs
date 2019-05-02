using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.SGMO
{
    public class _DELME_DataFcs0
    {
        public int Id { get; set; }
        public int Track0Id { get; set; }
        public int MethodId { get; set; }
        public DateTime DateIniUTC { get; set; }
        public DateTime DateInsert { get; set; }

        public List<_DELME_DataFcs1> DataFcs1List { get; set; }
    }
}
