using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class GeneralCategory: SOV.Common.IdNameDescription
    {
        public GeneralCategory(int id, string name, string description = null)
               : base(id, name, description)
        {
        }
    }
}
