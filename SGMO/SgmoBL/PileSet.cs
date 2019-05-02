using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Amur.Meta;

namespace FERHRI.SGMO
{
    /// <summary>
    /// Вид предупреждения
    /// </summary>
    public class PileSet
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }

        public List<PileItem> PileItems = new List<PileItem>();
        /// <summary>
        /// Один, экстремальный пайл из набора, в который попадают заданные значения.
        /// В пайле либо все значения, которые в него попали, либо одно - экстремальное.
        /// </summary>
        /// <param name="dataValues">Значения, проверяемые на вхождение в пайл.</param>
        /// <param name="oneExtremalValueOnly">Вернуть либо все значения, которые в попали в пайл, либо одно - экстремальное.</param>
        /// <returns>null, если значения не попадают в градации предупреждений.</returns>
        public PileItemValues GetExtremalPileValues(double[] dataValues, bool oneExtremalValueOnly)
        {
            List<PileItemValues> ret = GetPilesValues(dataValues, oneExtremalValueOnly);
            return ret.FirstOrDefault(x => x.PileItem.OrderBy == ret.Max(y => y.PileItem.OrderBy));
        }
        public PileItemValues GetExtremalPileValuesIndeces(double[] dataValues, bool oneExtremalValueOnly)
        {
            List<PileItemValues> pivs = GetPilesValuesIndeces(dataValues, oneExtremalValueOnly);
            PileItemValues piv = pivs.FirstOrDefault(x => x.PileItem.OrderBy == pivs.Max(y => y.PileItem.OrderBy));
            if (piv != null && oneExtremalValueOnly && piv.Values.Count != 1)
                throw new Exception("ALGORITHMIC ERROR OSokolov@201709: (piv != null && piv.Values.Count != 1)");
            //{
            //    // GET EXTREMALLY VALUE INDEX
            //    int i = (int)piv.Values[0];
            //    foreach (var item in piv.Values)
            //    {
            //        if (piv.PileItem.IsValueMoreExtremally(dataValues[(int)item], dataValues[i]))
            //            i = (int)item;
            //    }
            //    piv.Values = new List<double>() { i };
            //}
            return piv;
        }
        /// <summary>
        /// Все пайлы из набора, в которые попадают заданные значения.
        /// В каждом пайле либо все значения, которые в него попали, либо одно - экстремальное.
        /// </summary>
        /// <param name="dataValues">Значения, проверяемые на вхождение в пайл.</param>
        /// <param name="oneExtremalValueOnly">Вернуть либо все значения, которые в попали в пайл, либо одно - экстремальное.</param>
        /// <returns></returns>
        public List<PileItemValues> GetPilesValues(double[] dataValues, bool oneExtremalValueOnly)
        {
            List<PileItemValues> ret = new List<PileItemValues>();

            foreach (var pile in PileItems)
            {
                List<double> values = pile.GetPileValues(dataValues, oneExtremalValueOnly);
                if (values.Count > 0)
                    ret.Add(new PileItemValues() { PileItem = pile, Values = values });
            }
            return ret;
        }
        public List<PileItemValues> GetPilesValuesIndeces(double[] dataValues, bool oneExtremalValueOnly)
        {
            List<PileItemValues> ret = new List<PileItemValues>();

            foreach (var pile in PileItems)
            {
                List<double> indeces = pile.GetPileValuesIndeces(dataValues, oneExtremalValueOnly);
                if (indeces.Count > 0)
                    ret.Add(new PileItemValues() { PileItem = pile, Values = indeces });
            }
            return ret;
        }

        public string ToString(EnumLanguage lang)
        {
            return (lang == EnumLanguage.Rus ? NameRus : NameEng);
        }

    }
}
