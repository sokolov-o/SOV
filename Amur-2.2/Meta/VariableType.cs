using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class VariableType : SOV.Common.IdNameRE
    {
        public string Description { get; set; }
        public VariableType(int id, string nameRus, string nameEng, string description = null)
            : base(id, nameRus, nameEng)
        {
            Description = description;
        }
    }
}
