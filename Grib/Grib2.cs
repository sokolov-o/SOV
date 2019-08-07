using SOV.Geo;
using Seaware.GribCS.Grib2;

namespace SOV.Grib
{
    public class Grib2
    {
        /// <summary>
        /// TABLE 0 - Part 1
        /// NATIONAL/INTERNATIONAL ORIGINATING CENTERS  (Assigned By The WMO)
        /// (PDS Octet 5)
        /// </summary>
        public enum PDS_Centers
        {
            NOAA_FSL = 59 // NOAA Forecast Systems Lab, Boulder CO
        }
        public static Grid GetGrid(Grib2Record rec)
        {
            // TODO: было -1*GeoPoint.Grd2Min(rec.GDS.Dy)
            return new Grid(null,
                rec.GDS.Gdtn,
                rec.GDS.Ny, GeoPoint.Grd2Min(rec.GDS.La1), GeoPoint.Grd2Min(rec.GDS.Dy),
                rec.GDS.Nx, GeoPoint.Grd2Min(rec.GDS.Lo1), GeoPoint.Grd2Min(rec.GDS.Dx),
                "Grid from Grib2 file");
        }
    }
}
