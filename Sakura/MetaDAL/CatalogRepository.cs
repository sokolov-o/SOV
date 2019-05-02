using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Sakura.Meta
{
    public class CatalogRepository
    {
        ADbMSSql _db;

        internal CatalogRepository(ADbMSSql db)
        {
            _db = db;
        }

        public List<Catalog> SelectClimateCatalogs(Catalog catalogSrc, Enums.Action actionClm, Enums.DbList dbListClm, Enums.TimePeriod timeIdClm, Enums.Center? centerId = null)
        {
            return Select(
                (int)dbListClm, catalogSrc.ParamId, catalogSrc.LevelTypeId, (int)actionClm,
                catalogSrc.SpaceId, catalogSrc.GeoRegId,
                (centerId.HasValue) ? (int?)(int)centerId : null,
                null, (int)timeIdClm, catalogSrc.LevelValue, catalogSrc.ActionSub,
                catalogSrc.PredictTime, 1,
                catalogSrc.GridId, catalogSrc.UnqAttr);
        }


        public List<Catalog> Select(
         int? dbListId
        , int? paramId
        , int? levelTypeId
        , int? actionId
        , int? spaceId
        , int? geoRegId
        , int? centerId
        , int? formatId
        , int? timeId
        , int? levelValue
        , string actionSub
        , int? predictTime
        , int? isClimate
        , int? gridId
        , int? unqAttr1
)
        {
            string where = "";

            if (dbListId.HasValue) where += ((where == "") ? "" : " and ") + "id_db_list=" + dbListId;
            if (paramId.HasValue) where += ((where == "") ? "" : " and ") + "id_param=" + paramId;
            if (levelTypeId.HasValue) where += ((where == "") ? "" : " and ") + "id_ltype=" + levelTypeId;
            if (actionId.HasValue) where += ((where == "") ? "" : " and ") + "id_action=" + actionId;
            if (spaceId.HasValue) where += ((where == "") ? "" : " and ") + "id_space=" + spaceId;
            if (geoRegId.HasValue) where += ((where == "") ? "" : " and ") + "id_geoReg=" + geoRegId;
            if (centerId.HasValue) where += ((where == "") ? "" : " and ") + "id_center=" + centerId;
            if (formatId.HasValue) where += ((where == "") ? "" : " and ") + "id_format=" + formatId;
            if (timeId.HasValue) where += ((where == "") ? "" : " and ") + "id_time=" + timeId;
            if (levelValue.HasValue) where += ((where == "") ? "" : " and ") + "lvalue=" + levelValue;
            if (actionSub != null && actionSub != "") where += ((where == "") ? "" : " and ") + "action_sub='" + actionSub + "'";
            if (predictTime.HasValue) where += ((where == "") ? "" : " and ") + "predict_time=" + predictTime;
            if (isClimate.HasValue) where += ((where == "") ? "" : " and ") + "is_climat=" + isClimate;
            if (gridId.HasValue) where += ((where == "") ? "" : " and ") + "id_grid=" + gridId;
            if (unqAttr1.HasValue) where += ((where == "") ? "" : " and ") + "unq_attr1='" + unqAttr1 + "'";

            String sql = "select * from vCtl" + ((where == "") ? "" : " where " + where);

            List<Catalog> ret = new List<Catalog>();

            using (var cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(Parse(rdr));
                        }
                    }
                }
            }
            return ret;
        }

        Catalog Parse(SqlDataReader rdr)
        {
            return new Catalog(
                (int)rdr["id"],
                (int)rdr["id_db_list"],
                (int)rdr["id_param"],
                (int)rdr["id_ltype"],
                (int)rdr["id_action"],
                (int)rdr["id_space"],
                (int)rdr["id_georeg"],
                (int)rdr["id_center"],
                (int)rdr["id_format"],
                (int)rdr["id_time"],
                (int)rdr["lvalue"],
                (int)rdr["predict_time"],
                (byte)rdr["is_climat"],
                (int)rdr["id_grid"],
                (int)rdr["unq_attr1"],

                rdr["DataConnection"].ToString(),
                rdr["action_sub"].ToString(),
                rdr["nameString"].ToString(),

                (DateTime)rdr["date_insert"]);

        }

        public List<Catalog> Select(List<int> catalogId, bool withAttr = false)
        {
            List<Catalog> ret = new List<Catalog>();
            using (var cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand(
                    "select * from vctl where id in (" + StrVia.ToString(catalogId) + ")"
                    , cnn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Catalog catalog = Parse(rdr);
                            ret.Add(catalog);

                            if (withAttr)
                                catalog.Attr = SelectAttr(catalog.Id);
                        }
                        return ret;
                    }
                }
            }
        }
        public Catalog Select(int catalogId, bool withAttr = false)
        {
            List<Catalog> ret = Select(new List<int>(new int[] { catalogId }), withAttr);
            return ret.Count == 1 ? ret[0] : null;
        }

        public Dictionary<int, string> SelectAttr(int catalogId)
        {
            Dictionary<int, string> ret = new Dictionary<int, string>();
            using (var cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select attr_id, value from ctl_attr where ctl_id = " + catalogId, cnn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(rdr.GetInt32(0), rdr.GetString(1));
                        }
                        return ret;
                    }
                }
            }
        }
        /// <summary>
        /// Сформировать Grib2 на основе данных записи каталога, через кросстаблицу.
        /// </summary>
        /// <param name="catalog">должен быть с атрибутами.</param>
        /// <returns>Grib2</returns>
        //public Grib2 SelectGrib2(Catalog catalog)
        //{
        //    if (catalog.Attr == null)
        //        throw new Exception("Для создания экз. класса Grib2, требуется атрибут записи каталога " + Enums.TaskAttr.LEVEL_VEL_MULTYP);
        //    Grid grid = DataManager.GetInstance().GridRepository.Select(catalog.GridId);
        //    Grib2 ret = null;

        //    using (var cnn = _db.Connection)
        //    {
        //        using (SqlCommand cmd = new SqlCommand(
        //            "SELECT ctlId, grib2CenterId, grib2DisciplineCode, grib2ParameterCategory, "
        //            + " grib2ParameterNumber, Grib2FixedSurfaceTypeCode, lvalue "
        //            + " FROM vCtlXGrib2CtlParam where grib2CenterId is not null and ctlId = @p1"
        //            , cnn))
        //        {
        //            cmd.Parameters.AddWithValue("@p1", catalog.Id);
        //            using (SqlDataReader rdr = cmd.ExecuteReader())
        //            {
        //                if (rdr.Read())
        //                {
        //                    // LEVEL VALUE
        //                    string s = null;
        //                    catalog.Attr.TryGetValue((int)Enums.TaskAttr.LEVEL_VEL_MULTYP, out s);
        //                    float multipl = 1;
        //                    if (s != null)
        //                    {
        //                        multipl = (float)StrVia.ParseDouble(s);
        //                        if (double.IsNaN(multipl))
        //                            throw new Exception("(double.IsNaN(multipl))");
        //                    }
        //                    float PDSValueFirstFixedSurface = (int)catalog.LevelValue * multipl;

        //                    // PRODUCT DEFINITION
        //                    if ((int)catalog.ActionId != (int)Enums.Action.VALUE ||
        //                        (int)catalog.SpaceId != (int)Enums.Space.FIELD ||
        //                        (int)catalog.FormatId != (int)Enums.Format.GRIB2
        //                    )
        //                        throw new Exception("Unknown params for PDSProductDefinition!");
        //                    int PDSProductDefinition = 0;

        //                    // GRIB2
        //                    ret = new _DELME_Grib2(
        //                        rdr.GetInt32(2), rdr.GetInt32(1), PDSProductDefinition,
        //                        rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetInt32(5), 255,
        //                        PDSValueFirstFixedSurface, 0, grid, catalog.DataCnn
        //                    );

        //                    if (rdr.Read())
        //                        throw new Exception("Error! Count row in view !=1 vCtlXGrib2CtlParam for ctlId=" + catalog.Id);
        //                }
        //            }
        //        }
        //        return ret;
        //    }
        //}
    }
}
