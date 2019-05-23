using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amur.Import
{
    public class AMSData
    {
        /// <summary>
        /// Amur <-> AMSData
        /// </summary>
        public static Dictionary<int[/*id, offset_type_id,offset_value*/], int> CrossVariableId = new Dictionary<int[], int>()
        {
            {new int[]{1227,100,2  },181},
            {new int[]{1226,100,2  },182},
            {new int[]{1225,100,2  },180},
            {new int[]{10,  100,2  }  ,5},
            { new int[]{7,  100,10 }  ,2},
            {new int[]{1,   100,10 }  ,1},
            {new int[]{8,   100,10 }  ,3}
        };
        /// <summary>
        /// Amur <-> AMSData
        /// </summary>
        public static Dictionary<int, int> CrossSiteTypeId = new Dictionary<int, int>()
        {
            {13,8}
        };

        public int StationId { get; set; }
        public string StationName { get; set; }
        public int VariableId { get; set; }

        public DateTime DateObs { get; set; }
        public double Value { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
