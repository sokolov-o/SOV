using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.DB;

namespace FERHRI.MetaDb
{
    /// <summary>
    /// Менеджер внешних, по отношению к MetaDB, баз данных FERHRI. 
    /// Менеджер работает с интерфейсами для доступа к внешним данным.
    /// </summary>
    public class DataManagerMetaDb
    {
        //public static object GetDataManager(DbList dbList, Common.User user)
        //{
        //    string cnns = (user != null) ? Common.ADbNpgsql.ConnectionStringUpdateUser(dbList.ConnectionString, user) : dbList.ConnectionString;

        //    switch (dbList.DbTypeId)
        //    {
        //        case (int)Enums.DbList.Analog_FERHRI:
        //            return FERHRI.Analog.DataManager.GetInstance(cnns);
        //    }
        //    return null;
        //}
        public static Catalog GetCatalog(DbList dbList, int catalogId)
        {
            switch ((Enums.DbList)dbList.Id)
            {
                case Enums.DbList.Sakura_Hmdic:
                    FERHRI.Sakura.Meta.Catalog catalogSakura = FERHRI.Sakura.Meta.DataManager.GetInstance(dbList.ConnectionString).CatalogRepository.Select(catalogId, true);
                    return new Catalog() { Id = catalogId, Name = catalogSakura.Name, NativeCatalog = catalogSakura };
            }
            throw new Exception("Неизвестная БД " + (Enums.DbList)dbList.Id + ".");
        }
    }
}
