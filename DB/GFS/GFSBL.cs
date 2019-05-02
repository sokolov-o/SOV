using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seaware.GribCS.Grib2;
using Seaware.GribCS;
using FERHRI.Geo;
using FERHRI.Grib;

namespace FERHRI.DB
{
    public class GFS
    {
        /// <summary>
        /// 
        /// field.MetaInfo.Add("ID_RefTime", rec.ID.RefTime);
        /// field.MetaInfo.Add("PDS_TimeRangeUnit", rec.PDS.TimeRangeUnit);
        /// 
        /// </summary>
        static internal Field ToField(Grib2Record rec, float[] data)
        {
            Grid grid = Grib2.GetGrid(rec);
            // TODO: Несовпадение кол. точек сетки и возврата библиотеки Grib2! Решить по-другому.
            //if (data.Length < grid.PointsQ)
            //    throw new Exception("(data1.Length != grid.PointsQ) : " + data.Length + "!=" + grid.PointsQ);
            double[] ddata = new double[grid.PointsQ];
            for (int j = 0; j < ddata.Length; j++)
            {
                ddata[j] = data[j];
            }

            Field field = new Field(grid, EnumFieldFormat.GRID, rec.PDS.ForecastTime, ddata);
            field.MetaInfo.Add("ID_RefTime", rec.ID.RefTime);
            field.MetaInfo.Add("PDS_TimeRangeUnit", rec.PDS.TimeRangeUnit);

            return field;
        }
        /// <summary>
        /// 
        /// field.MetaInfo.Add("ID_RefTime", rec.ID.RefTime);
        /// field.MetaInfo.Add("PDS_TimeRangeUnit", rec.PDS.TimeRangeUnit);
        /// 
        /// </summary>
        /// <param name="gfsRecords">Not null.</param>
        /// <returns></returns>
        static internal List<FERHRI.Field> ToFields(object[][] gfsRecords)
        {
            List<Field> ret = new List<Field>();
            foreach (var item in gfsRecords)
            {
                if (item == null)
                    ret.Add(null);
                else
                    ret.Add(ToField((Grib2Record)item[0], (float[])item[1]));
            }
            return ret;
        }
    }
}
