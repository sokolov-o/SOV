using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Sakura.Data
{
    public class ClassSetInt
    {
        public Dictionary<string, int[]> Classes { get; set; }
        public ClassSetInt() { }
        /// <summary>
        /// ВНИМАНИЕ! В конструкторе добавляется ещё один класс NOCLASS, 
        /// в который попадают все int, которых нет в перечислении.
        /// </summary>
        /// <param name="classes"></param>
        public ClassSetInt(Dictionary<string, int[]> classes)
        {
            Classes = classes;
            Classes.Add("NOCLASS", null);
        }
        public string GetClassName(int i)
        {
            return Classes.ElementAt(i).Key;
        }
        public int Count
        {
            get
            {
                return Classes.Count;
            }
        }
        /// <summary>
        /// Class index from 0 or -1 if not exists.
        /// </summary>
        /// <returns></returns>
        public int ClassOf(int classValue)
        {
            for (int i = 0; i < Classes.Count - 1; i++)
            {
                int ret = Classes.ElementAt(i).Value.FirstOrDefault(x => x == classValue);
                if (ret > 0)
                    return i;
            }
            return Classes.Count - 1;
        }
    }

    public class ClassSetBound
    {
        /// <summary>
        /// Больше или равно левой и меньше правой границы = значение в классе
        /// </summary>
        public double[] RightBounds { get; set; }
        string[] ClassNames { get; set; }
        string NameSet { get; set; }

        public ClassSetBound(string SetName, string[] ClassNames, double[] RightBounds)
        {
            this.NameSet = SetName;
            this.RightBounds = RightBounds;
            this.ClassNames = ClassNames;
        }
        public string GetClassName(int i)
        {
            return ClassNames[i];
        }
        public string GetClassNameSet()
        {
            return NameSet;
        }

        /// <summary>
        /// 0.67 sigma;LT-0.67;norm;GE0.67;-0.67,+0.67
        /// </summary>
        static public ClassSetBound Parse(string s)
        {
            string[] ss = s.Split(';');
            string name = ss[0];
            string[] names = new string[ss.Length - 2];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = ss[i + 1];
            }
            double[] bounds = StrVia.ParseDoubleArray(ss[ss.Length - 1].Split(','));
            if (bounds.Length != ss.Length - 3)
                throw new Exception("Формат строки неверен.");
            return new ClassSetBound(ss[0], names, bounds);
        }
        public int Count
        {
            get
            {
                return RightBounds.Length + 1;
            }
        }
        /// <summary>
        /// Class index from 0 or -1 if not exists.
        /// </summary>
        /// <returns></returns>
        public int ClassOf(double value)
        {
            if (value < RightBounds[0]) return 0;
            if (value >= RightBounds[RightBounds.Length - 1]) return Count - 1;

            for (int i = 0; i < RightBounds.Length - 1; i++)
            {
                if (value >= RightBounds[i] && value < RightBounds[i + 1])
                    return i + 1;
            }
            throw new Exception("Agorithmic error.");
        }
    }
}
