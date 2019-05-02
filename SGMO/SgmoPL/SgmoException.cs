using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.SGMO
{
    public class SgmoException : Exception
    {
        public SgmoException(string mess)
            : base(mess)
        {
        }
    }
}
