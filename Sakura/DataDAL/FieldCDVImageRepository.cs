using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Sakura.Grib;
using FERHRI.Sakura.Meta;
using FERHRI.DB;
using FERHRI.Geo;

namespace FERHRI.Sakura.Data
{
    public class FieldCDVImageRepository : ADbMSSql, IField, IDataTimePeriodSF
    {
        public string DataTableName { get; set; }

        public FieldCDVImageRepository(string cnns, int dbListId, string dataTableName = "data")
            : base(cnns, dbListId)
        {
            DataTableName = dataTableName;
        }
        List<Grid> _grids = new List<Grid>();

        public List<Field> GetFields(object nativeCatalog, DateTime date, GeoRectangle grExtract, double? leadTime)
        {
            return SelectFields((Catalog)nativeCatalog, date, leadTime.HasValue ? (int?)(int)leadTime : null, grExtract);
        }
        public List<Field> SelectFields(Catalog catalog, DateTime date, int? predictTime, GeoRectangle grExtract)
        {
            List<Field> ret = new List<Field>();
            using (var cnn = Connection)
            {
                using (SqlCommand cmd = new SqlCommand(
                    "select ctlId, date, value from " + DataTableName + " where ctlId = @ctlId and date = @date"
                    , cnn))
                {
                    cmd.Parameters.AddWithValue("@ctlId", catalog.Id);
                    cmd.Parameters.AddWithValue("@date", date);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Grid grid = _grids.FirstOrDefault(x => x.Id == catalog.GridId);
                            if (grid == null)
                            {
                                grid = Meta.DataManager.GetInstance().GridRepository.Select(catalog.GridId);
                                _grids.Add(grid);
                            }
                            Field field;
                            switch ((FERHRI.Sakura.Meta.Enums.Format)catalog.FormatId)
                            {
                                case FERHRI.Sakura.Meta.Enums.Format.GRIB1:
                                    field = new Field(grid, EnumFieldFormat.GRID, 0.0, (new Grib1(GetByteArray(rdr))).unpackDouble());
                                    break;
                                default:
                                    throw new Exception("Неизвестный формат хранения поля в БД: " + catalog.FormatId);
                            }
                            if ((object)grExtract != null)
                                field = field.Truncate(grExtract);
                            ret.Add(field);
                        }
                        return ret;
                    }
                }
            }
        }
        /// <summary>
        /// Получить из поля текущей записи ридера байтовый массив, содержащий GRIB.
        /// </summary>
        private byte[] GetByteArray(SqlDataReader rdr)
        {
            int iCol = rdr.GetOrdinal("value");
            long i = rdr.GetBytes(iCol, 0, null, 0, 0);
            byte[] b = new byte[i];
            rdr.GetBytes(iCol, 0, b, 0, (int)i);
            return b;
        }
        /// <summary>
        /// Начало и окончание периода данных, хранящихся в репозитории для записи каталога.
        /// </summary>
        /// <param name="catalogId">Код записи каталога.</param>
        /// <returns>Начало и окончание периода данных или null, если нет данных</returns>
        public DateTime[] GetDataTimePeriodSF(int catalogId)
        {
            using (var cnn = Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select min(date) min_date, max(date) max_date from " + DataTableName + " where ctlId = @ctlId", cnn))
                {
                    cmd.Parameters.AddWithValue("@ctlId", catalogId);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        return rdr.IsDBNull(0) ? null : new DateTime[] { (DateTime)rdr[0], (DateTime)rdr[1] };
                    }
                }
            }
        }
        public List<DateTime> GetDataAllDateList(int catalogId)
        {
            using (var cnn = Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select date from " + DataTableName + " where ctlId = @ctlId", cnn))
                {
                    cmd.Parameters.AddWithValue("@ctlId", catalogId);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<DateTime> ret = new List<DateTime>();
                        while (rdr.Read())
                        {
                            ret.Add((DateTime)rdr[0]);
                        }
                        return ret;
                    }
                }
            }
        }
    }
}
