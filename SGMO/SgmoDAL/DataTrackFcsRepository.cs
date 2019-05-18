using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;
using SOV.Amur.Meta;

namespace SOV.SGMO
{
    public class DataTrackFcsRepository : Common.BaseRepository<DataTrackFcs>
    {
        internal DataTrackFcsRepository(Common.ADbNpgsql db) : base(db, "data_track_fcs")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DataTrackFcs()
            {
                Id = (int)rdr["id"],
                TrackPointId = (int)rdr["track_point_id"],
                CatalogId = (int)rdr["catalog_id"],
                LeadTime = (double)rdr["lead_time"],
                Value = (double)rdr["value"],
                DateInsert = (DateTime)rdr["date_insert"]
            };
        }

        public List<DataTrackFcsExt> SelectExtByTrackPartPointId(List<int> trackPointIds)
        {
            List<DataTrackFcs> dtf = SelectByTrackPointIds(trackPointIds);

            List<int> catalogIds = dtf.Select(x => x.CatalogId).ToList();

            List<CatalogExt> catalogs = Amur.Meta.DataManager.GetInstance().CatalogRepository.SelectExt(catalogIds);
            List<TrackPoint> trackPoints = DataManager.GetInstance().TrackPointsRepository.Select(trackPointIds); ;

            List<DataTrackFcsExt> ret = new List<DataTrackFcsExt>();
            foreach (var item in dtf)
            {
                ret.Add(new DataTrackFcsExt() { DataTrackFcs = item, CatalogExt = catalogs.FirstOrDefault(x => x.Catalog.Id == item.CatalogId), TrackPoint = trackPoints.FirstOrDefault(x => x.Id == item.TrackPointId) });
            }

            return ret;
        }
        public List<DataTrackFcs> SelectByTrackPointIds(List<int> trackPointIds)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>()
                {
                    {"track_point_id", trackPointIds}
                };
            return Select(fields);

        }
        public void Insert(List<DataTrackFcs> data)
        {
            if (data != null && data.Count > 0)
            {
                List<Dictionary<string, object>> fields = new List<Dictionary<string, object>>();
                foreach (var value in data)
                {
                    fields.Add(new Dictionary<string, object>
                {
                    { "track_point_id", value.TrackPointId },
                    { "catalog_id" , value.CatalogId},
                    { "lead_time", value.LeadTime},
                    { "value", value.Value}
                });
                }
                Insert(fields);
            }
        }
    }
}
