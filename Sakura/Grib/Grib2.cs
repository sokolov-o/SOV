using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seaware.GribCS.Grib2;
using FERHRI.Sakura.Meta;

namespace FERHRI.Sakura.Grib
{
    public class Grib2
    {
        public int IsDiscipline;			// 
        public int IDCenter_id;			// 
        public int PDSProductDefinition;
        public int PDSParameterCategory;
        public int PDSParameterNumber;
        public int PDSTypeFirstFixedSurface;
        public int PDSTypeSecondFixedSurface;
        public float PDSValueFirstFixedSurface;
        public float PDSValueSecondFixedSurface;
        public Grid hmdicGrid;
        public string dataCnn;
        public int GDSGdtn = -1;

        public Grib2(int IsDiscipline, int IDCenter_id,
            int PDSProductDefinition,
            int PDSParameterCategory, int PDSParameterNumber,
            int PDSTypeFirstFixedSurface, int PDSTypeSecondFixedSurface,
            float PDSValueFirstFixedSurface, float PDSValueSecondFixedSurface, Grid hmdicGrid,
            string dataCnn)
        {
            init(IsDiscipline, IDCenter_id, PDSProductDefinition, PDSParameterCategory, PDSParameterNumber,
             PDSTypeFirstFixedSurface, PDSTypeSecondFixedSurface, PDSValueFirstFixedSurface,
             PDSValueSecondFixedSurface, hmdicGrid, dataCnn);
        }
        public Grib2(int IsDiscipline, int IDCenter_id,
            int PDSProductDefinition,
            int PDSParameterCategory, int PDSParameterNumber,
            int PDSTypeFirstFixedSurface, int PDSTypeSecondFixedSurface,
            float PDSValueFirstFixedSurface, float PDSValueSecondFixedSurface, int GDSGdtn)
        {
            init(IsDiscipline, IDCenter_id, PDSProductDefinition, PDSParameterCategory, PDSParameterNumber,
             PDSTypeFirstFixedSurface, PDSTypeSecondFixedSurface, PDSValueFirstFixedSurface,
             PDSValueSecondFixedSurface, hmdicGrid, dataCnn);
            this.GDSGdtn = GDSGdtn;

        }

        public void init(int IsDiscipline, int IDCenter_id,
    int PDSProductDefinition,
    int PDSParameterCategory, int PDSParameterNumber,
    int PDSTypeFirstFixedSurface, int PDSTypeSecondFixedSurface,
    float PDSValueFirstFixedSurface, float PDSValueSecondFixedSurface, Grid hmdicGrid,
    string dataCnn)
        {

            this.IsDiscipline = IsDiscipline;
            this.IDCenter_id = IDCenter_id;
            this.PDSProductDefinition = PDSProductDefinition;
            this.PDSParameterCategory = PDSParameterCategory;
            this.PDSParameterNumber = PDSParameterNumber;
            this.PDSTypeFirstFixedSurface = PDSTypeFirstFixedSurface;
            this.PDSTypeSecondFixedSurface = PDSTypeSecondFixedSurface;
            this.PDSValueFirstFixedSurface = PDSValueFirstFixedSurface;
            this.PDSValueSecondFixedSurface = PDSValueSecondFixedSurface;
            this.hmdicGrid = hmdicGrid;
        }

        public bool equals(Grib2Record rec, Grid grid)
        {
            if (this.hmdicGrid.isCompatible(grid) &&
                    this.IDCenter_id == rec.ID.Center_id &&
                    this.IsDiscipline == rec.Is.Discipline &&
                    this.PDSParameterCategory == rec.PDS.ParameterCategory &&
                    this.PDSParameterNumber == rec.PDS.ParameterNumber &&
                    this.PDSProductDefinition == rec.PDS.ProductDefinition &&
                    this.PDSTypeFirstFixedSurface == rec.PDS.TypeFirstFixedSurface &&
                    this.PDSTypeSecondFixedSurface == rec.PDS.TypeSecondFixedSurface &&
                    this.PDSValueFirstFixedSurface == rec.PDS.ValueFirstFixedSurface &&
                    this.PDSValueSecondFixedSurface == rec.PDS.ValueSecondFixedSurface)
            {
                return true;
            }
            else return false;
        }

        public bool equals(Grib2Record rec)
        {

            if (this.GDSGdtn == rec.GDS.Gdtn &&
                this.IDCenter_id == rec.ID.Center_id &&
                    this.IsDiscipline == rec.Is.Discipline &&
                    this.PDSParameterCategory == rec.PDS.ParameterCategory &&
                    this.PDSParameterNumber == rec.PDS.ParameterNumber &&
                    this.PDSProductDefinition == rec.PDS.ProductDefinition &&
                    this.PDSTypeFirstFixedSurface == rec.PDS.TypeFirstFixedSurface &&
                    this.PDSTypeSecondFixedSurface == rec.PDS.TypeSecondFixedSurface &&
                    this.PDSValueFirstFixedSurface == rec.PDS.ValueFirstFixedSurface &&
                    this.PDSValueSecondFixedSurface == rec.PDS.ValueSecondFixedSurface)
            {
                return true;
            }
            else return false;
        }
        public static string ToString(Grib2Record rec)
        {
            string str = "";

            str = rec.Is.DisciplineName + "\t" + rec.Is.GribEdition + "\t" + rec.Is.GribLength + "\t|\t";
            str = str + rec.ID.Center_idName + "\t" + rec.ID.ProductStatusName + "\t" + rec.ID.ProductType +
                " (" + rec.ID.ProductTypeName + ")\t" +
                rec.ID.RefTime + "\t" + rec.ID.SignificanceOfRTName;
            str += "\t" + rec.GDS.Altitude + "\t" +
                rec.GDS.Angle + "\t" +
                rec.GDS.CheckSum + "\t" +
                rec.GDS.Dstart + "\t" +
                rec.GDS.Dx + "\t" +
                rec.GDS.Dy + "\t" +
                rec.GDS.EarthRadius + "\t" +
                rec.GDS.Factor + "\t" +
                rec.GDS.Gdtn + "\t" +
                rec.GDS.getShapeName() + "\t" +
                rec.GDS.GetType() + "\t" +
              rec.GDS.Iolon + "\t" +
              rec.GDS.J + "\t" +
              rec.GDS.K + "\t" +
              rec.GDS.La1 + "\t" +
              rec.GDS.La2 + "\t" +
              rec.GDS.Lad + "\t" +
              rec.GDS.Lap + "\t" +
              rec.GDS.Latin1 + "\t" +
              rec.GDS.Latin2 + "\t" +
              rec.GDS.Lo1 + "\t" +
              rec.GDS.Lo2 + "\t" +
              rec.GDS.Lop + "\t" +
              rec.GDS.Lov + "\t" +
              rec.GDS.M + "\t" +
              rec.GDS.MajorAxis + "\t" +
              rec.GDS.Method + "\t" +
              rec.GDS.MinorAxis + "\t" +
              rec.GDS.Mode + "\t" +
              rec.GDS.N + "\t" +
              rec.GDS.N2 + "\t" +
              rec.GDS.N3 + "\t" +
              rec.GDS.Name + "\t" +
              rec.GDS.Nb + "\t" +
              rec.GDS.Nd + "\t" +
              rec.GDS.Ni + "\t" +
              rec.GDS.Nr + "\t" +
              rec.GDS.NumberPoints + "\t" +
              rec.GDS.Nx + "\t" +
              rec.GDS.Ny + "\t" +
              rec.GDS.Olon + "\t" +
              rec.GDS.Order + "\t" +
              rec.GDS.PoleLat + "\t" +
              rec.GDS.PoleLon + "\t" +
              rec.GDS.Position + "\t" +
              rec.GDS.ProjectionCenter + "\t" +
              rec.GDS.Resolution + "\t" +
              rec.GDS.Rotationangle + "\t" +
              rec.GDS.ScanMode + "\t" +
              rec.GDS.Shape + "\t" +
              rec.GDS.Source + "\t" +
              rec.GDS.SpLat + "\t" +
              rec.GDS.SpLon + "\t" +
              rec.GDS.Subdivisionsangle + "\t" +
              rec.GDS.Xo + "\t" +
              rec.GDS.Xp + "\t" +
              rec.GDS.Yo + "\t" +
              rec.GDS.Yp;
            str += "\t|\t" + rec.PDS.AnalysisGenProcess + "\t" +
                rec.PDS.BackGenProcess + "\t" +
                rec.PDS.Coordinates + "\t" +
                rec.PDS.ForecastTime + "\t" +
                rec.PDS.getProductDefinitionName() + "\t" +
                rec.PDS.getTimeRangeUnitName() + "\t" +
                rec.PDS.HoursAfter + "\t" +
                rec.PDS.MinutesAfter + "\t" +
                rec.PDS.ParameterCategory + "\t" +
                rec.PDS.ParameterNumber + "\t" +
                rec.PDS.ProductDefinition + "\t" +
                rec.PDS.TimeRangeUnit + "\t" +
                rec.PDS.TypeFirstFixedSurface + "\t" +
                rec.PDS.TypeFirstFixedSurfaceName + "\t" +
                rec.PDS.TypeGenProcess + "\t" +
                rec.PDS.TypeSecondFixedSurface + "\t" +
                rec.PDS.TypeSecondFixedSurfaceName + "\t" +
                rec.PDS.ValueFirstFixedSurface + "\t" +
                rec.PDS.ValueSecondFixedSurface;
            return str;
        }
    }
}
