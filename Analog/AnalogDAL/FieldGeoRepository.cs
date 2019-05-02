using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Analog
{
    public class FieldGeoobsRepository : BaseRepository<FieldGeoobs>
    {
        internal FieldGeoobsRepository(Common.ADbNpgsql db) : base(db, "analog.field_geo") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new FieldGeoobs()
            {
                Id = (int)rdr["id"],
                FieldId = (int)rdr["field_id"],
                GeoobMetricId = (int)rdr["geoobs_metric_id"]
            };
        }
        public List<FieldGeoobs> SelectByField(int fieldId)
        {
            return Select(new Dictionary<string, object>()
            {
                {"field_id", fieldId}
            });
        }
        public int Insert(FieldGeoobs item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"field_id", item.FieldId},
                {"geoobs_metric_id", item.GeoobMetricId}
            };
            return InsertWithReturn(fields);
        }
        public void Update(FieldGeoobs item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"field_id", item.FieldId},
                {"geoobs_metric_id", item.GeoobMetricId}
            };
            Update(fields);
        }
    }
}
