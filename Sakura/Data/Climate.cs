using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using FERHRI.Sakura.Meta;

namespace FERHRI.Sakura.Data
{
    [DataContract]
    public class ClimateHeader
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Catalog Catalog { get; set; }
        [DataMember]
        public int YearS { get; set; }
        [DataMember]
        public int YearF { get; set; }
        [DataMember]
        public string TimeNums { get; set; }
    }
    [DataContract]
    public class ClimateData
    {
        [DataMember]
        public ClimateHeader ClimateHeader { get; set; }
        [DataMember]
        public int TmeNum { get; set; }
        [DataMember]
        public double Count { get; set; }
        [DataMember]
        public double[] Value { get; set; }
    }
}
