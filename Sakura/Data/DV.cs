using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Sakura.Data
{
    public class DV
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
    public class DVCollection : List<DV>
    {
        public DVCollection()
            : base(new List<DV>())
        {
        }
        public DVCollection(List<DV> dvs)
            : base(dvs)
        {
        }

        public void ToFile(string path, char splitter)
        {
            string format = "{0};{1}".Replace(';', splitter);
            StreamWriter sw = new StreamWriter(path, false);
            try
            {
                foreach (var item in this)
                {
                    sw.WriteLine(format, item.Date, item.Value.ToString());
                }
            }
            finally
            {
                sw.Close();
            }

        }
    }
}
