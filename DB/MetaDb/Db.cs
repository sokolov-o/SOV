using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.MetaDb
{
    public class Db : IdName   
    {
        public int DbTypeId { get; set; }
        public string ConnectionString { get; set; }

        public Db(IdName idName) : base(idName) { }
    }
}
