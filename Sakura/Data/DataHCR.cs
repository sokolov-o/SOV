using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Sakura.Meta;

namespace FERHRI.Sakura.Data
{
    public class DataHCR
    {
        public long Id { get; set; }
        public int CatalogId { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }

        public Enums.QCL QCL { get; set; }
        public byte Version { get; set; }
        public int DbListIdSrc { get; set; }
        public int UserId { get; set; }

        override public string ToString()
        {
            return "" + Value;
        }
    }
}
