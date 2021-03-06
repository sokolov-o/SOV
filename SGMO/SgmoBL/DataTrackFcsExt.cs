﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;

namespace SOV.SGMO
{
    public class DataTrackFcsExt
    {
        public DataTrackFcs DataTrackFcs { get; set; }
        public TrackPoint TrackPoint { get; set; }
        public CatalogExt CatalogExt { get; set; }

        public double LeadTime { get { return DataTrackFcs.LeadTime; } }
        public double Value { get { return DataTrackFcs.Value; } }
        public DateTime DateIniUTC { get { return DataTrackFcs.DateIniUTC; } }
        public DateTime DateFcsUTC { get { return TrackPoint.DateUTC; } }
        public DateTime DateFcsLOC { get { return TrackPoint.DateUTC.AddHours(TrackPoint.UTCOffsetHours); } }

        public string SiteName { get { return CatalogExt.Site.Name; } }
        public string VariableName { get { return CatalogExt.Variable.NameRus; } }
        public string MethodName { get { return CatalogExt.Method.Name; } }
        public string OffsetTypeName { get { return CatalogExt.OffsetType.Name; } }
        public double OffsetValue { get { return CatalogExt.Catalog.OffsetValue; } }
    }
}
