using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seaware.GribCS.Grib2;
using Seaware.GribCS;

namespace SOV.Grib
{
    /// <summary>
    /// Фильтр для выборки данных из grib2-файла.
    /// </summary>
    public class Grib2Filter
    {
        /// <summary>
        /// Дисциплина (секция IS).
        /// </summary>
        public int is_discipline;
        /// <summary>
        /// Код центра (секция ID).
        /// </summary>
        public int id_center_id;
        /// <summary>
        /// Код продукта (секция PDS).
        /// </summary>
        public int pds_product_definition;
        /// <summary>
        /// Код категории (секция PDS).
        /// </summary>
        public int pds_parameter_category;
        /// <summary>
        /// Код параметра/переменной (секция PDS).
        /// </summary>
        public int pds_parameter_number;
        /// <summary>
        /// Код типа первой поверхности переменной (секция PDS).
        /// </summary>
        public int pds_type_first_fixed_surface;
        /// <summary>
        /// Код типа второй поверхности переменной (секция PDS).
        /// </summary>
        public int pds_type_second_fixed_surface;
        /// <summary>
        /// Значение первой поверхности переменной (секция PDS).
        /// </summary>
        public float pds_value_first_fixed_surface;
        /// <summary>
        /// Значение второй поверхности переменной (секция PDS).
        /// </summary>
        public float pds_value_second_fixed_surface;
        /// <summary>
        /// Код gdtn (секция GDS).
        /// </summary>
        public int gds_gdtn;

        /// <summary>
        /// Значение для приведения переменной к реальному виду по формуле (value + value_add) * value_multiply.
        /// </summary>
        public double value_add;
        /// <summary>
        /// Значение для приведения переменной к реальному виду по формуле (value + value_add) * value_multiply.
        /// </summary>
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
