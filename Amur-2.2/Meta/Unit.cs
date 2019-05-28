using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class Unit : Common.IdNameRE
    {
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Abbreviation { get; set; }
        [DataMember]
        public string AbbreviationEng { get; set; }
        [DataMember]
        public double? SIConvertion { get; set; }

        public Unit(int id, string type, string nameRus, string abbr, string nameEng, string abbrEng, double? siConvertion)
            : base(id, nameRus, nameEng)
        {
            Type = type;
            Abbreviation = abbr;
            AbbreviationEng = abbrEng;
            SIConvertion = siConvertion;
        }
        public Unit(int id)
            : base(id, null, null)
        {
        }
    }
}
