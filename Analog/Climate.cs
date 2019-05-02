using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Analog
{
    public class Climate
    {
        public int YearS { get; set; }
        public int YearF { get; set; }
        /// <summary>
        /// FERHRI.Amur.Meta.EnumMathVar
        /// </summary>
        public Dictionary<int, int> MathvarXCatalog { get; set; }

        public IntClimate ToIntClimate()
        {
            return new IntClimate()
            {
                yearS = YearS,
                yearF = YearF,
                avgCatalogId = MathvarXCatalog[(int)Amur.Meta.EnumMathvar.Avg],
                stdCatalogId = MathvarXCatalog[(int)Amur.Meta.EnumMathvar.Avg]
            };
        }
        static public Climate GetClimate(IntClimate item)
        {
            return new Climate()
            {
                YearS = item.yearS,
                YearF = item.yearF,
                MathvarXCatalog = new Dictionary<int, int>()
                 {
                     {(int)Amur.Meta.EnumMathvar.Avg, item.avgCatalogId } ,
                     {(int)Amur.Meta.EnumMathvar.Std, item.stdCatalogId }
                 }
            };  
        }
    }
}
