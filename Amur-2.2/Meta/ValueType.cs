using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class ValueType : SOV.Common.IdNameDescription
    {
        public ValueType(int id, string name, string description = null)
            : base(id, name, description)
        {
        }
    }
}
