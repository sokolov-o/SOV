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
    public class Factory
    {
        static string _SakuraMetaConnectionString = null;
        //static string GetSakuraMetaConnectionString()
        static string SakuraMetaConnectionString
        {
            get
            {
                if (_SakuraMetaConnectionString == null)
                    _SakuraMetaConnectionString = DataManager.GetInstance().DbListRepository.Select((int)Enums.Db.Sakura_Hmdic).ConnectionString;
                return _SakuraMetaConnectionString;
            }
        }
        /// <summary>
        /// Получить репозиторий IField.
        /// </summary>
        /// <param name="dbCatalog"></param>
        /// <param name="catalogField"></param>
        /// <returns></returns>
        public static object GetRepository(Db dbCatalog, Catalog catalogField)
        {
            switch (dbCatalog.DbTypeId)
            {
                case (int)Enums.DbType.Sakura:
                    switch (((Sakura.Meta.Catalog)catalogField.NativeCatalog).DbListId)
                    {
                        case (int)Sakura.Meta.Enums.DbList.HmdGribMonth:

                            string cnns = Sakura.Meta.DataManager.GetInstance(SakuraMetaConnectionString)
                                    .DbListRepository
                                    .SelectAttrValue((int)Sakura.Meta.Enums.DbList.HmdGribMonth, (int)Sakura.Meta.Enums.TaskAttr.SRC_CNN_MSSQL);

                            return Sakura.Data.DataManager.GetInstance(cnns, (int)Sakura.Meta.Enums.DbList.HmdGribMonth).FieldCDVImageRepository;
                        default:
                            throw new Exception("В базе данных типа Enums.DbType.Sakura отсутствует репозиторий для DbList.Id = " + ((Sakura.Meta.Catalog)catalogField.NativeCatalog).DbListId);
                    }
                default:
                    throw new Exception("Неизвестный тип базы данных MetaDb.DbType.Id = " + dbCatalog.DbTypeId);
            }
        }

        public static Catalog GetCatalog(Db catalogDb, int catalogId)
        {
            switch ((Enums.Db)catalogDb.Id)
            {
                case Enums.Db.Sakura_Hmdic:
                    FERHRI.Sakura.Meta.Catalog catalogSakura = Sakura.Meta.DataManager.GetInstance(catalogDb.ConnectionString).CatalogRepository.Select(catalogId, true);
                    return new Catalog() { Id = catalogId, Name = catalogSakura.Name, NativeCatalog = catalogSakura };
            }
            throw new Exception("Неизвестная БД " + (Enums.Db)catalogDb.Id + ".");
        }
        public static DateTime[] GetDataTimePeriodSF(Db dbCatalog, Catalog catalog)
        {
            object rep = GetRepository(dbCatalog, catalog);
            if (rep.GetType().GetInterface(typeof(IDataTimePeriodSF).ToString()) == null)
                throw new Exception("Репозиторий не поддерживает интерфейс IDataTimePeriodSF.");

            return ((IDataTimePeriodSF)rep).GetDataTimePeriodSF(catalog.Id);
        }
    }
}
