using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class OffsetType : IdName
    {
        [DataMember]
        public int UnitId { get; set; }

        public OffsetType(int id, string name, int unitId)
        {
            Id = id;
            Name = name;
            UnitId = unitId;
        }
    }
}
