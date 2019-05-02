using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Geo;
using Seaware.GribCS.Grib2;
using Seaware.GribCS;

namespace SOV.Grib
{
    public class Grib2
    {
        public IsSection Is;
        public IDSection ID;
        public PDSSection PDS;
        public GDSSection GDS;

        public class IsSection
        {
            public int IsDiscipline;
        }
        public class IDSection
        {
            public int Center_id;
        }
        public class PDSSection
        {
            public int ProductDefinition;
            public int ParameterCategory;
            public int ParameterNumber;
            public int TypeFirstFixedSurface;
            public int TypeSecondFixedSurface;
            public float ValueFirstFixedSurface;
            public float ValueSecondFixedSurface;
        }
        public class GDSSection
        {
            /// <summary>
            /// Код типа сетки
            /// </summary>
            public int Gdtn = -1;
            public int Ny = -1;
            public int Nx = -1;
            public double La1 = double.NaN;
            public double Lo1 = double.NaN;
            public double Dx = double.NaN;
            public double Dy = double.NaN;
        }
        public static Grid GetGrid(Grib2Record rec)
        {
            //if (rec.GDS.Gdtn != 0) throw new Exception("rec.GDS.Gdtn != 0 = " + rec.GDS.Gdtn);
            //if (rec.GDS.ScanMode != 0) throw new Exception("Unknown GDS.ScanMode=" + rec.GDS.ScanMode);
            //if (rec.GDS.Source != 0) throw new Exception("(rec.GDS.Source != 0) = " + rec.GDS.Source);

            //DicItem gridType = gridTypes.FirstOrDefault(x => x.Id == rec.GDS.Gdtn);
            //Center center = centers.FirstOrDefault(x => (int)x.GribId == rec.ID.Center_id);
            //if (gridType == null || center == null)
            //    throw new Exception("(gridType  == null || center== null)");

            return new Grid(null,
                rec.GDS.Gdtn,
                rec.GDS.Ny, GeoPoint.Grd2Min(rec.GDS.La1), -1 * GeoPoint.Grd2Min(rec.GDS.Dy),
                rec.GDS.Nx, GeoPoint.Grd2Min(rec.GDS.Lo1), GeoPoint.Grd2Min(rec.GDS.Dx),
                "Grid from Grib2 file");
        }
        /// <summary>
        /// Проверка совместимости записей Grib2. ВНИМАНИЕ: без проверки совместимости сеток!
        /// </summary>
        /// <param name="rec1"></param>
        /// <param name="rec2"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static bool IsCompatible(Grib2Record rec1, Grib2Record rec2)
        {
            if (
                    rec1.ID.Center_id == rec2.ID.Center_id &&
                    rec1.Is.Discipline == rec2.Is.Discipline &&
                    rec1.PDS.ParameterCategory == rec2.PDS.ParameterCategory &&
                    rec1.PDS.ParameterNumber == rec2.PDS.ParameterNumber &&
                    rec1.PDS.ProductDefinition == rec2.PDS.ProductDefinition &&
                    rec1.PDS.TypeFirstFixedSurface == rec2.PDS.TypeFirstFixedSurface &&
                    rec1.PDS.TypeSecondFixedSurface == rec2.PDS.TypeSecondFixedSurface &&
                    rec1.PDS.ValueFirstFixedSurface == rec2.PDS.ValueFirstFixedSurface &&
                    rec1.PDS.ValueSecondFixedSurface == rec2.PDS.ValueSecondFixedSurface
                )
                return true;
            return false;
        }

    }
}
