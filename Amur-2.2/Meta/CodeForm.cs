using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    public class CodeForm: SOV.Common.IdName
    {
        public CodeForm(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
