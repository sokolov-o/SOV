using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Определение элемента категории данных.
    /// </summary>
    [DataContract]
    public class CategoryDefinition
    {
        [DataMember]
        public int CategoryId = -1;
        [DataMember]
        public int CategoryItemId = -1;
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public double? Value1 { get; set; }
        [DataMember]
        public double? Value2 { get; set; }
    }
}
