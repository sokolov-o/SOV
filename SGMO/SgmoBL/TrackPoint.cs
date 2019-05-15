using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.SGMO
{
    [DataContract]
    public class TrackPoint : IdClass
    {
        public int TrackId { get; set; }
        /// <summary>
        /// Datetime UTC.
        /// </summary>
        [DataMember]
        public DateTime DateUTC { get; set; }
        /// <summary>
        /// Track point.
        /// </summary>
        [DataMember]
        public Geo.GeoPoint GeoPoint { get; set; }
        /// <summary>
        /// UTC offset.
        /// </summary>
        [DataMember]
        public int UTCOffset { get; set; }
    }
}
