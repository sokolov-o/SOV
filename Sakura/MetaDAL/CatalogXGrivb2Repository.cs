using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Grib;
using FERHRI.Geo;

namespace FERHRI.Sakura.Meta
{
    public class CatalogXGrivb2Repository
    {
        ADbMSSql _db;

        internal CatalogXGrivb2Repository(ADbMSSql db)
        {
            _db = db;
        }

        /// <summary>
        /// Сформировать Grib2 на основе данных записи каталога, через кросстаблицу.
        /// </summary>
        /// <param name="catalog">должен быть с атрибутами.</param>
        /// <returns>Grib2</returns>
        public Grib2 SelectGrib2(Catalog catalog)
        {
            if (catalog.Attr == null)
                throw new Exception("Для создания экз. класса Grib2, требуется атрибут записи каталога " + Enums.TaskAttr.LEVEL_VEL_MULTYP);

            using (var cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT ctlId, grib2CenterId, grib2DisciplineCode, grib2ParameterCategory, "
                    + " grib2ParameterNumber, Grib2FixedSurfaceTypeCode, lvalue "
                    + " FROM vCtlXGrib2CtlParam where grib2CenterId is not null and ctlId = @p1"
                    , cnn))
                {
                    cmd.Parameters.AddWithValue("@p1", catalog.Id);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            // LEVEL VALUE
                            string s = null;
                            catalog.Attr.TryGetValue((int)Enums.TaskAttr.LEVEL_VEL_MULTYP, out s);
                            float multipl = 1;
                            if (s != null)
                            {
                                multipl = (float)StrVia.ParseDouble(s);
                                if (double.IsNaN(multipl))
                                    throw new Exception("(double.IsNaN(multipl))");
                            }
                            float PDS_ValueFirstFixedSurface = catalog.LevelValue * multipl;

                            // PRODUCT DEFINITION
                            if ((int)catalog.ActionId != (int)Enums.Action.VALUE ||
                                (int)catalog.SpaceId != (int)Enums.Space.FIELD ||
                                (int)catalog.FormatId != (int)Enums.Format.GRIB2
                            )
                                throw new Exception("Unknown params for PDSProductDefinition!");
                            int PDSProductDefinition = 0;

                            // Grib2 GDSection

                            Grid grid = DataManager.GetInstance().GridRepository.Select(catalog.GridId);

                            Grib2.GDSSection gds = null;
                            switch (grid.TypeId)
                            {
                                case (int)Enums.Geo.Projection.LATLON:
                                    gds = new Grib2.GDSSection()
                                    {
                                        Gdtn = grid.TypeId,
                                        La1 = grid.LatStartMin / 60,
                                        Lo1 = grid.LonStartMin / 60,
                                        Dx = grid.LonStepMin / 60,
                                        Dy = grid.LatStepMin / 60,
                                        Nx = grid.LonsMin.Length,
                                        Ny = grid.LatsMin.Length
                                    };
                                    break;
                                default:
                                    throw new Exception("Неизвестная проекция/тип сетки для записи каталога: " + catalog);
                            }

                            // GRIB2
                            return new Grib2()
                            {
                                Is = new Grib2.IsSection() { IsDiscipline = rdr.GetInt32(2) },
                                ID = new Grib2.IDSection() { Center_id = rdr.GetInt32(1) },
                                PDS = new Grib2.PDSSection()
                                {
                                    ProductDefinition = PDSProductDefinition,
                                    ParameterCategory = rdr.GetInt32(3),
                                    ParameterNumber = rdr.GetInt32(4),
                                    TypeFirstFixedSurface = rdr.GetInt32(5),
                                    TypeSecondFixedSurface = 255,
                                    ValueFirstFixedSurface = PDS_ValueFirstFixedSurface,
                                    ValueSecondFixedSurface = 0
                                },
                                GDS = gds
                            };
                        }
                        return null;
                    }
                }
            }
        }
    }
}
