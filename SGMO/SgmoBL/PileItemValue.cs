using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;

namespace SOV.SGMO
{
    /// <summary>
    /// Градация и значения переменной для неё.
    /// В частном случае, может быть одно значение или ни одного.
    /// </summary>
    public class PileItemValues
    {
        public PileItem PileItem { get; set; }
        public List<double> Values { get; set; }

        public string ToString(EnumLanguage lang, string unitShortNameEng = null)
        {
            return
                this.PileItem.ToString(lang, unitShortNameEng) + " - " +
                (lang == EnumLanguage.Rus ? this.PileItem.PileSet.NameRus : PileItem.PileSet.NameEng) + ". "
            ;
        }
    }
}
