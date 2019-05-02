using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seaware.GribCS.Grib2;
using Seaware.GribCS;

namespace SOV.Grib
{
    public class Grib2Filter
    {
        public int is_discipline;
        public int id_center_id;
        public int pds_product_definition;
        public int pds_parameter_category;
        public int pds_parameter_number;
        public int pds_type_first_fixed_surface;
        public int pds_type_second_fixed_surface;
        public float pds_value_first_fixed_surface;
        public float pds_value_second_fixed_surface;
        public int gds_gdtn;

        public double value_add;
        public double value_multiply;

        public double AcceptAddMultiply2Value(double value)
        {
            return (value + value_add) * value_multiply;
        }
        public float[] AcceptAddMultiply2Value(float[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = (float)((values[i] + value_add) * value_multiply);
            }
            return values;
        }

        public bool Equal(Grib2Record rec)
        {
            if
            (
            id_center_id == rec.ID.Center_id &&
            is_discipline == rec.Is.Discipline &&
            pds_parameter_category == rec.PDS.ParameterCategory &&
            pds_parameter_number == rec.PDS.ParameterNumber &&
            pds_product_definition == rec.PDS.ProductDefinition &&
            pds_type_first_fixed_surface == rec.PDS.TypeFirstFixedSurface &&
            pds_type_second_fixed_surface == rec.PDS.TypeSecondFixedSurface &&
            pds_value_first_fixed_surface == rec.PDS.ValueFirstFixedSurface &&
            pds_value_second_fixed_surface == rec.PDS.ValueSecondFixedSurface
            )
                return true;
            return false;
        }
        public bool Equal(Grib2Filter rec)
        {
            if
            (
            id_center_id == rec.id_center_id &&
            is_discipline == rec.is_discipline &&
            pds_parameter_category == rec.pds_parameter_category &&
            pds_parameter_number == rec.pds_parameter_number &&
            pds_product_definition == rec.pds_product_definition &&
            pds_type_first_fixed_surface == rec.pds_type_first_fixed_surface &&
            pds_type_second_fixed_surface == rec.pds_type_second_fixed_surface &&
            pds_value_first_fixed_surface == rec.pds_value_first_fixed_surface &&
            pds_value_second_fixed_surface == rec.pds_value_second_fixed_surface
            )
                return true;
            return false;
        }
    }
}
