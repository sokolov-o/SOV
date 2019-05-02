using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.SGMO
{
    public class MethvarXGrib2Repository
    {
        Common.ADbNpgsql _db;
        internal MethvarXGrib2Repository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        /// <summary>
        /// Выбрать все соответствия переменных и параметров grib2.
        /// </summary>
        public List<MethVaroffXGrib2> Select(string srcName, int? methodId = null)
        {
            List<MethVaroffXGrib2> ret = new List<MethVaroffXGrib2>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from variable_x_grib2"
                    + " where (:src_name is null or src_name = :src_name) and (:method_id is null or method_id = :method_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("src_name", srcName);
                    cmd.Parameters.AddWithValue("method_id", methodId);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            MethVaroffXGrib2 item = new MethVaroffXGrib2()
                            {
                                Id = (int)rdr["id"],

                                SrcName = rdr["src_name"].ToString(),

                                VariableId = (int)rdr["variable_id"],
                                MethodId = (int)rdr["method_id"],
                                OffsetTypeId = (int)rdr["offset_type_id"],
                                OffsetValue = (double)rdr["offset_value"],
                                ExcludeFromFcs = (bool)rdr["is_exclude_from_fcs"],
                                IsActual = (bool)rdr["is_actual"],

                                Grib2Filter = new SOV.Grib.Grib2Filter()
                               {
                                   id_center_id = (int)rdr["grib2_center_id"],
                                   gds_gdtn = (int)rdr["grib2_gds_gdtn"],
                                   is_discipline = (int)rdr["grib2_is_discipline"],
                                   pds_parameter_category = (int)rdr["grib2_pds_parameter_category"],
                                   pds_parameter_number = (int)rdr["grib2_pds_parameter_number"],
                                   pds_product_definition = (int)rdr["grib2_pds_product_definition"],
                                   pds_type_first_fixed_surface = (int)rdr["grib2_pds_type_first_fixed_surface"],
                                   pds_type_second_fixed_surface = (int)rdr["grib2_pds_type_second_fixed_surface"],
                                   pds_value_first_fixed_surface = (float)(double)rdr["grib2_pds_value_first_fixed_surface"],
                                   pds_value_second_fixed_surface = (float)(double)rdr["grib2_pds_value_second_fixed_surface"],
                                   value_add = (double)rdr["value_add"],
                                   value_multiply = (double)rdr["value_multiply"]
                               },
                            };
                            ret.Add(item);

                        }
                    }
                }
            }
            return ret;
        }
    }
}
