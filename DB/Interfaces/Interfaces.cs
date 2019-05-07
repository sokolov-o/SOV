using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Geo;

namespace SOV.DB
{
    /// <summary>
    /// Выходная продукция прогностических моделей в виде элементов заданных в узлах сетки.
    /// </summary>
    public interface IFcsGrid
    {
        /// <summary>
        /// Чтение данных в узлах поля указанного региона для указанной даты, заблаговременности и фильтра данных (переменных, уровней и др.).
        /// </summary>
        /// <param name="dateIni">Дата или исходная дата прогноза для прогностических полей.</param>
        /// <param name="dataFilter">Фильтр данных: параметры, высоты и проч. Внимание! Допускаются null значения элементов фильтра. 
        /// В этом случае отбор данных производиться не будет и на выходе тоже null.</param>
        /// <param name="grs2Truncate">Регионы, для которых отбираются узлы поля. Все узлы, если null.</param>
        /// <param name="leadTimes">Заблаговременность прогноза или все, если null. Для полей без заблаговременности - null.</param>
        /// <returns>Field[/*leadTime*/][/*Georectangle*/][/*Data filter index*/]</returns>
        Field[/*leadTime*/][/*Georectangle*/][/*Data filter index*/] ReadFieldsInRectangles(DateTime dateIni, object dataFilter, List<double> leadTimes, List<GeoRectangle> grs2Truncate);
        /// <summary>
        /// Чтение данных поля в указанных точках для указанной даты, заблаговременности и фильтра данных (переменных, уровней и др.).
        /// </summary>
        /// <param name="dateIni">Дата или исходная дата прогноза для прогностических полей.</param>
        /// <param name="dataFilter">Фильтр данных: параметры, высоты и проч. Внимание! Допускаются null значения элементов фильтра. 
        /// В этом случае отбор данных производиться не будет и на выходе тоже null.</param>
        /// <param name="grs2Truncate">Регионы, для которых отбираются узлы поля. Все узлы, если null.</param>
        /// <param name="leadTimes">Заблаговременность прогноза или все, если null. Для полей без заблаговременности - null.</param>
        /// <returns>double[/*leadTime*/][/*GeoPoint index*/][/*Data filter index*/]</returns>
        double[/*leadTime*/][/*GeoPoint index*/][/*Data filter index*/] ReadValuesAtPoints(DateTime dateIni, object dataFilter, List<double> leadTimes, List<Geo.GeoPoint> points, EnumPointNearestType nearestType, EnumDistanceType distanceType);

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
