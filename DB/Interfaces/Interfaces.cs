using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Geo;

namespace FERHRI.DB
{
    /// <summary>
    /// Выходная продукция прогностических моделей в виде элементов заданных в узлах сетки.
    /// </summary>
    public interface IFcsGrid
    {
        ///// <summary>
        ///// Чтение данных поля для указанной даты, атрибутов данных (параметров, высот и др.), регионов и заблаговременности.
        ///// </summary>
        ///// <param name="dateIni">Дата или исходная дата прогноза для прогностических полей.</param>
        ///// <param name="dataFilter">Все параметры, высоты и проч., если null.</param>
        ///// <param name="gr">Регионы, для которых отбираются узлы поля. Все узлы, если null.</param>
        ///// <param name="fcsTime">Заблаговременность прогноза или все, если null. Для полей без заблаговременности - null.</param>
        ///// <returns></returns>
        //Dictionary<GeoPoint, Dictionary<double/*FcsTime*/, double[/*WaveParams*/]>>[]
        //    ReadPoints(DateTime dateIni, object dataFilter, List<double> leadTimeHours, List<GeoRectangle> grs2Truncate);

        Field[/*leadTime*/][/*Georectangle index*/][/*VarFilter index*/] ReadFieldsInRectangles
            (DateTime dateIni, object dataFilter, List<double> leadTimes,
            List<GeoRectangle> grs2Truncate);

        double[/*leadTime*/][/*GeoPoint index*/][/*VarFilter index*/] ReadValuesAtPoints
            (DateTime dateIni, object dataFilter, List<double> leadTimes,
            List<Geo.GeoPoint> points, EnumPointNearestType nearestType, EnumDistanceType distanceType);

    }
    public interface ICatalog
    {
        Catalog GetCatalog(int catalogId);
    }
    public interface IField
    {
        List<Field> GetFields(object nativeCatalog, DateTime date, GeoRectangle grExtract, double? leadTime = null);
    }
    public interface IDataTimePeriodSF
    {
        /// <summary>
        /// Начало и окончание периода данных, хранящихся в репозитории для записи каталога.
        /// </summary>
        /// <param name="catalogId">Код записи каталога.</param>
        /// <returns>Начало и окончание периода данных или null, если нет данных</returns>
        DateTime[/*Datetime start & finish*/] GetDataTimePeriodSF(int catalogId);
        /// <summary>
        /// Получить список всех дат данных для записи каталога.
        /// </summary>
        /// <param name="catalogId">Код записи каталога.</param>
        /// <returns>Список всех дат данных для записи каталога.</returns>
        List<DateTime> GetDataAllDateList(int catalogId);
    }
}
