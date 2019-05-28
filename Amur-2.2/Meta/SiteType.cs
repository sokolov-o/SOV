using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SOV.Common;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class SiteType : IdName
    {
        [DataMember]
        public string NameShort { get; set; }

        public SiteType(int id, string name, string nameShort)
        {
            Id = id;
            Name = name;
            NameShort = nameShort;
        }

        public int GetId()
        {
            return Id;
        }
    }
}
