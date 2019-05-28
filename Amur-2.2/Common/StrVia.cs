using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOV.Common
{
    public class StrVia
    {
        static public List<int> ToListInt(string strDelimited, char dev = ',')
        {
            return string.IsNullOrEmpty(strDelimited.Trim()) ? null : strDelimited.Split(dev).Select(x => int.Parse(x)).ToList();
        }
        static public string ToString(List<int> list, char dev = ',')
        {
            if (list == null || list.Count == 0) return null;

            string ret = string.Concat(list.Select(x => x.ToString() + dev));
            ret.Remove(ret.Length - 1);
            return ret;
        }
    }
}
