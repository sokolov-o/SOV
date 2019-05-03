using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOV.WcfService.Field.AmurServiceReference
{
    public partial class GeoPoint
    {
        SOV.Geo.GeoPoint ToSOVGeoGeoPoint(GeoPoint point)
        {
            return new SOV.Geo.GeoPoint(point.LatGrd, point.LonGrd);
        }
    }
}