using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seaware.GribCS.Grib2;
using Seaware.GribCS;
using SOV;
using SOV.Geo;
using SOV.Grib;

namespace SOV.DB
{
    /// <summary>
    /// Конвертер для записей файла формата grib2 для модели GFS.
    /// 
    /// Почему не в классе Grib2? 
    /// 1. Потому что может быть специфика файлов grib2 в GFS. Наверное...хотя не было пока замечено и можно эти методы перенести в класс Grib2.
    /// 2. Тогда Grib2 должен знать о классах (например Field), знать о которых не его дело.
    /// 
    /// Поэтому, пусть будет здесь...
    /// 
    /// </summary>
    static public class GFS
    {
        /// <summary>
        /// В метаданные поля добавляется:
        /// 
        /// field.MetaInfo.Add("ID_RefTime", rec.ID.RefTime);
        /// field.MetaInfo.Add("PDS_TimeRangeUnit", rec.PDS.TimeRangeUnit);
        /// 
        /// </summary>
        static internal Field ToField(Grib2Record rec, float[] data)
        {
            Grid grid = Grib2.GetGrid(rec);
            // TODO: Несовпадение кол. точек сетки и возврата библиотеки Seaware.GribCS.Grib2! такой вот баг... Решить по-другому?
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
        /// В метаданные поля добавляется:
        /// 
        /// field.MetaInfo.Add("ID_RefTime", rec.ID.RefTime);
        /// field.MetaInfo.Add("PDS_TimeRangeUnit", rec.PDS.TimeRangeUnit);
        /// 
        /// </summary>
        /// <param name="gfsRecords">Not null.</param>
        /// <returns></returns>
        static internal List<Field> ToFields(object[/*grib2filter index*/][/*Grib2Record;float[] data*/] gfsRecords)
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
