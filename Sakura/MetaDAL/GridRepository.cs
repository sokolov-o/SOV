using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Geo;

namespace FERHRI.Sakura.Meta
{
    public class GridRepository 
    {
        ADbMSSql _db;

        internal GridRepository(ADbMSSql db) 
        {
            _db = db;
        }

        public List<Grid> Select(List<int> gridId)
        {
            List<Grid> ret = new List<Grid>();
            using (var cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select * from Grid where id in (" + StrVia.ToString(gridId) + ")", cnn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int? geoRegId = null;
                            if (!rdr.IsDBNull(rdr.GetOrdinal("id_georeg")))
                                geoRegId = (int)rdr["id_georeg"];
                            string description = null;
                            if (!rdr.IsDBNull(rdr.GetOrdinal("DESCRIPTION")))
                                description = rdr["DESCRIPTION"].ToString();

                            int id = (int)rdr["id"];
                            int gridTypeId = (int)rdr["id_grid_type"];
                            int centerId = (int)rdr["id_centers"];
                            char region = rdr["REGION"].ToString()[0];
                            char model = rdr["MODEL"].ToString()[0];
                            int pointsX = (int)double.Parse(rdr.GetDecimal(rdr.GetOrdinal("DIMENSION_X")).ToString());
                            int pointsY = (int)double.Parse(rdr.GetDecimal(rdr.GetOrdinal("DIMENSION_Y")).ToString());
                            double stepX = double.Parse(rdr.GetDecimal(rdr.GetOrdinal("STEP_X")).ToString());
                            double stepY = double.Parse(rdr.GetDecimal(rdr.GetOrdinal("STEP_Y")).ToString());
                            double latStartMin = double.Parse(rdr.GetDecimal(rdr.GetOrdinal("LATITUDE")).ToString());
                            double lonStartMin = double.Parse(rdr.GetDecimal(rdr.GetOrdinal("LONGITUDE")).ToString());

                            switch ((Enums.Geo.Projection)gridTypeId)
                            {
                                case Enums.Geo.Projection.LATLON:
                                    ret.Add(new Grid(id, gridTypeId, pointsY, latStartMin, stepY, pointsX, lonStartMin, stepX));
                                    break;
                                case Enums.Geo.Projection.GAUSS:
                                    ret.Add(new Grid(id, Grid.GAYSS_LAT_94, pointsX, lonStartMin, stepX));
                                    break;
                                default:
                                    throw new Exception("Неизвестная проекция сетки: " + (Enums.Geo.Projection)gridTypeId);
                            }
                        }
                        return ret;
                    }
                }
            }
        }

        public Grid Select(int gridId)
        {
            List<Grid> ret = Select(new List<int>(new int[] { gridId }));
            return ret.Count == 1 ? ret[0] : null;
        }
    }
}
