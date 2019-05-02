using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Sakura.Meta;
using FERHRI.Common;

namespace FERHRI.Sakura.Data
{
    public interface _DELME_IDbFieldCDV
    {
        /// <summary>
        /// Выборка поля указанной заблаговременностью с урезанием региона.
        /// </summary>
        /// <param name="catalog">Запись каталога данных (поля).</param>
        /// <param name="date">Дата поля.</param>
        /// <param name="predictTime">Заблаговременность прогноза.Все, если null.</param>
        /// <param name="grExtract">Регион, до которого усекается исходное поле. Усечение не производится, если null.</param>
        /// <returns></returns>
        List<Field> SelectFields(Catalog catalog, DateTime date, int? predictTime, _DELME_GeoRectangle grExtract);
    }
    public interface IDbHCR
    {
        List<DataHCR> Select(List<int> catalog, DateTime? dateS, DateTime? dateF, int? dbListIdSrc, bool isMaxVersion, bool isShowBadValues, bool isShowFKName);
        List<DataHCR> Select(List<int> catalog, DateTime dateS, DateTime dateF);
        List<DataHCR> Select(List<int> catalog);
    }
}
