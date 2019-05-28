using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class DataType : SOV.Common.IdNameDescription
    {
        public DataType(int id, string name, string description = null)
            :base(id, name, description)
        {
        }
    }
}
