using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.MetaDb
{
    public class Enums
    {
        public enum DbType
        {
            Sakura = 1,
            PostgreSQL = 2
        }
        public enum Db
        {
            Amur_FERHRI = 3,
            Sakura_HmdFieldNNRP_Month = 2,
            Sakura_Hmdic = 1,
            MetaDb_FERHRI = 4,
            Analog_FERHRI = 5
        }
        public enum DbInterfaceType
        {
            IField = 1,
            IPunct = 3,
            IMeta = 4
        }
        public enum DbInterface
        {
            Sakura_Catalog = 5
        }
    }
}
