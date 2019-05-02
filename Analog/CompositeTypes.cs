using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Analog
{
    public struct IntDouble
    {
        public int Int;
        public double Double;

        public static List<IntDouble> GetList(Dictionary<int, double> items)
        {
            List<IntDouble> ret = new List<IntDouble>();
            foreach (var item in items)
            {
                ret.Add(new IntDouble() { Int = item.Key, Double = item.Value });
            }
            return ret.Count == 0 ? null : ret;
        }
        public static Dictionary<int, double> GetDictionaryIntDouble(int[] ints, double[] doubles)
        {
            if (ints.Length != doubles.Length) throw new Exception("(ints.Length != doubles.Length)");

            Dictionary<int, double> ret = new Dictionary<int, double>();

            for (int i = 0; i < ints.Length; i++)
            {
                ret.Add(ints[i], doubles[i]);
            }
            return ret;
        }
    }

    //public struct DateDouble
    //{
    //    public DateTime Date;
    //    public double Double;

    //    public static Dictionary<DateTime, double> GetDictionary(DateDouble[] items)
    //    {
    //        if (items == null)
    //            return null;

    //        Dictionary<DateTime, double> ret = new Dictionary<DateTime, double>();

    //        for (int i = 0; i < items.Length; i++)
    //        {
    //            ret.Add(items[i].Date, items[i].Double);
    //        }
    //        return ret;
    //    }
    //}

    public struct IntClimate
    {
        public int yearS;
        public int yearF;
        public int avgCatalogId;
        public int stdCatalogId;
    }
}
