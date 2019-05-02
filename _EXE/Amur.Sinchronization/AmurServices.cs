using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.Sinchronization
{
    public class AmurServices
    {
        public FERHRIAmurServiceReference.ServiceClient ClientFERHRI { get; set; }
        public long HandleFERHRI { get; set; }
        public string NameFERHRI = "ferhri.amur";
        /// <summary>
        /// Вынужден использовать для чтения из ferhri.amur, т.к. в сервисе нет некоторых методов чтения.
        /// OSokolov@201710
        /// </summary>
        FERHRI.Amur.Meta.DataManager dmmFERHRI;

        public HBRAmurServiceReference.ServiceClient ClientHBR { get; set; }
        public long HandleHBR { get; set; }
        public string NameHBR = "dvugms.amur";
        /// <summary>
        /// Вынужден использовать для записи в dst-базы, т.к. в сервисах нет методов записи во все справочники Meta.
        /// OSokolov@201710
        /// </summary>
        FERHRI.Amur.Meta.DataManager dmmHBR;
        FERHRI.Social.DataManager dmsHBR;

        AmurServices() { }

        static public AmurServices GetAmurServices()
        {
            Console.Write("Amur services opening...");
            AmurServices ret = new AmurServices();
            Common.User user = new Common.User(Properties.Settings.Default.UserName, Properties.Settings.Default.UserPassword);

            ret.ClientFERHRI = new FERHRIAmurServiceReference.ServiceClient();
            ret.HandleFERHRI = ret.ClientFERHRI.Open(Properties.Settings.Default.UserName, Properties.Settings.Default.UserPassword);
            if (ret.HandleFERHRI < 1)
                throw new Exception("FERHRIClientHandle = " + ret.HandleFERHRI);
            ret.dmmFERHRI = FERHRI.Amur.Meta.DataManager.GetInstance(Common.ADbNpgsql.ConnectionStringUpdateUser(Properties.Settings.Default.FERHRIAmurConnectionString, user));

            ret.ClientHBR = new HBRAmurServiceReference.ServiceClient();
            ret.HandleHBR = ret.ClientHBR.Open(Properties.Settings.Default.UserName, Properties.Settings.Default.UserPassword);
            if (ret.HandleHBR < 1)
                throw new Exception("HBRClientHandle = " + ret.HandleHBR);
            ret.dmmHBR = FERHRI.Amur.Meta.DataManager.GetInstance(Common.ADbNpgsql.ConnectionStringUpdateUser(Properties.Settings.Default.HBRAmurConnectionString, user));
            ret.dmsHBR = FERHRI.Social.DataManager.GetInstance(Common.ADbNpgsql.ConnectionStringUpdateUser(Properties.Settings.Default.HBRAmurConnectionString, user));

            Console.WriteLine(" ок");
            return ret;
        }
        /// <summary>
        /// Синхронизация таблицы variable по ключу, не по Id.
        /// Предварительная синхронизация справочников по Id.
        /// </summary>
        internal void SinchronizeVariableHBR()
        {
            string tableName = NameHBR + ".variable";

            Console.WriteLine("\nSinchronizeVariable() for " + tableName);
            List<FERHRIAmurServiceReference.Variable> fitems = ClientFERHRI.GetVariablesAll(HandleFERHRI);
            List<HBRAmurServiceReference.Variable> hitems = ClientHBR.GetVariablesAll(HandleHBR);

            // Sinchronize dictionaryes - Синхронизация по id и ключам таблиц.
            SinchronizeVariableTypeHBR();
            SinchronizeUnitHBR();
            SinchronizeDataTypeHBR();
            SinchronizeGeneralCategoryHBR();
            SinchronizeSampleMediumHBR();
            SinchronizeValueTypeHBR();

            SinchronizeLegalEntityHBR();
            SinchronizeMethodHBR();
            Console.WriteLine("\n\n");

            List<HBRAmurServiceReference.Variable> hitemsNotExists = hitems.FindAll(x => !fitems.Exists(
               y => y.Id == x.Id
               && y.Name == x.Name
               ));
            if (hitemsNotExists.Count() > 0)
            {
                foreach (var item in hitemsNotExists)
                {
                    Console.WriteLine("{0} {1}", item.Id, item.Name);
                }
                Console.WriteLine(string.Format("****** {0} отличных от центрального хранилища записей в таблице {1}! Это баг...", hitemsNotExists.Count(), tableName));
            }
            // KEY (variable_type_id, time_id, unit_id, data_type_id, general_category_id, sample_medium_id, time_support, value_type_id)
            List<FERHRIAmurServiceReference.Variable> fitemsMissing = fitems.FindAll(x => !hitems.Exists(
                y => y.Id == x.Id
                //y => y.VariableTypeId == x.VariableTypeId
                //&& y.TimeId == x.TimeId
                //&& y.UnitId == x.UnitId
                //&& y.DataTypeId == x.DataTypeId
                //&& y.GeneralCategoryId == x.GeneralCategoryId
                //&& y.SampleMediumId == x.SampleMediumId
                //&& y.TimeSupport == x.TimeSupport
                //&& y.ValueTypeId == x.ValueTypeId
            ));
            Console.WriteLine("\t{0} missing rows in the {1} table.", fitemsMissing.Count(), tableName);
            foreach (var item in fitemsMissing)
            {
                //dmmHBR.VariableRepository.Insert(new Meta.Variable(-1, item.VariableTypeId, item.TimeId, item.UnitId, item.DataTypeId, item.ValueTypeId, item.GeneralCategoryId, item.SampleMediumId, item.TimeSupport, item.Name, item.NameEng, item.CodeNoData, item.CodeErrData));
            }
            Console.WriteLine("SinchronizeVariable() finished.\n\n");
        }
        private void SinchronizeDataTypeHBR()
        {
            string tableName = NameHBR + ".data_type";
            List<FERHRIAmurServiceReference.DataType> fitems = ClientFERHRI.GetDataTypesAll(HandleFERHRI);
            List<HBRAmurServiceReference.DataType> hitems = ClientHBR.GetDataTypesAll(HandleHBR);

            List<HBRAmurServiceReference.DataType> hitemsNotExists = hitems.FindAll(x => !fitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                && y.NameShort == x.NameShort
                ));
            if (hitemsNotExists.Count() > 0)
                throw new Exception(string.Format("{0} отличных от центрального хранилища записей в таблице {1}! Это баг..." + hitemsNotExists.Count(), tableName));

            List<FERHRIAmurServiceReference.DataType> fitemsNotExists = fitems.FindAll(x => !hitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                && y.NameShort == x.NameShort
                ));
            Console.WriteLine("\t{0} missing rows in the {1} table.", fitemsNotExists.Count(), tableName);
            foreach (var item in fitemsNotExists)
            {
                Console.WriteLine("\t\t{0} {1}", item.Id, item.Name);
                dmmHBR.DataTypeRepository.Insert(new Meta.DataType(item.Id, item.Name, item.NameShort, item.Description));
                Console.WriteLine("\t\t\tSinchronized...");
            }
        }

        private void SinchronizeGeneralCategoryHBR()
        {
            string tableName = NameHBR + ".general_category";
            List<FERHRIAmurServiceReference.GeneralCategory> fitems = ClientFERHRI.GetGeneralCategoryesAll(HandleFERHRI);
            List<HBRAmurServiceReference.GeneralCategory> hitems = ClientHBR.GetGeneralCategoryesAll(HandleHBR);

            List<HBRAmurServiceReference.GeneralCategory> hitemsNotExists = hitems.FindAll(x => !fitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                && y.NameShort == x.NameShort
                ));
            if (hitemsNotExists.Count() > 0)
                throw new Exception(string.Format("{0} отличных от центрального хранилища записей в таблице {1}! Это баг..." + hitemsNotExists.Count(), tableName));

            List<FERHRIAmurServiceReference.GeneralCategory> fitemsNotExists = fitems.FindAll(x => !hitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                && y.NameShort == x.NameShort
                ));
            Console.WriteLine("\t{0} missing rows in the {1} table.", fitemsNotExists.Count(), tableName);
            foreach (var item in fitemsNotExists)
            {
                Console.WriteLine("\t\t{0} {1}", item.Id, item.Name);
                dmmHBR.GeneralCategoryRepository.Insert(new Meta.GeneralCategory(item.Id, item.Name, item.NameShort, item.Description));
                Console.WriteLine("\t\t\tSinchronized...");
            }
        }

        private void SinchronizeSampleMediumHBR()
        {
            string tableName = NameHBR + ".sample_medium";
            List<FERHRIAmurServiceReference.SampleMedium> fitems = ClientFERHRI.GetSampleMediumsAll(HandleFERHRI);
            List<HBRAmurServiceReference.SampleMedium> hitems = ClientHBR.GetSampleMediumsAll(HandleHBR);

            List<HBRAmurServiceReference.SampleMedium> hitemsNotExists = hitems.FindAll(x => !fitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                ));
            if (hitemsNotExists.Count() > 0)
                throw new Exception(string.Format("{0} отличных от центрального хранилища записей в таблице {1}! Это баг..." + hitemsNotExists.Count(), tableName));

            List<FERHRIAmurServiceReference.SampleMedium> fitemsNotExists = fitems.FindAll(x => !hitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                ));
            Console.WriteLine("\t{0} missing rows in the {1} table.", fitemsNotExists.Count(), tableName);
            foreach (var item in fitemsNotExists)
            {
                Console.WriteLine("\t\t{0} {1}", item.Id, item.Name);
                dmmHBR.SampleMediumRepository.Insert(new Meta.SampleMedium(item.Id, item.Name, item.Description));
                Console.WriteLine("\t\t\tSinchronized...");
            }
        }

        private void SinchronizeValueTypeHBR()
        {
            string tableName = NameHBR + ".value_type";
            List<FERHRIAmurServiceReference.ValueType> fitems = ClientFERHRI.GetValueTypesAll(HandleFERHRI);
            List<HBRAmurServiceReference.ValueType> hitems = ClientHBR.GetValueTypesAll(HandleHBR);

            List<HBRAmurServiceReference.ValueType> hitemsNotExists = hitems.FindAll(x => !fitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                ));
            if (hitemsNotExists.Count() > 0)
                throw new Exception(string.Format("{0} отличных от центрального хранилища записей в таблице {1}! Это баг..." + hitemsNotExists.Count(), tableName));

            List<FERHRIAmurServiceReference.ValueType> fitemsNotExists = fitems.FindAll(x => !hitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                ));
            Console.WriteLine("\t{0} missing rows in the {1} table.", fitemsNotExists.Count(), tableName);
            foreach (var item in fitemsNotExists)
            {
                Console.WriteLine("\t\t{0} {1}", item.Id, item.Name);
                dmmHBR.ValueTypeRepository.Insert(new Meta.ValueType(item.Id, item.Name, item.NameEng, item.Description));
                Console.WriteLine("\t\t\tSinchronized...");
            }
        }

        private void SinchronizeVariableTypeHBR()
        {
            string tableName = NameHBR + ".variable_type";
            List<FERHRIAmurServiceReference.VariableType> fitems = ClientFERHRI.GetVariableTypesAll(HandleFERHRI);
            List<HBRAmurServiceReference.VariableType> hitems = ClientHBR.GetVariableTypesAll(HandleHBR);

            List<HBRAmurServiceReference.VariableType> hitemsNotExists = hitems.FindAll(x => !fitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                && y.NameShort == x.NameShort
                && y.NameEng == x.NameEng
                ));
            if (hitemsNotExists.Count() > 0)
                throw new Exception(string.Format("{0} отличных от центрального хранилища записей в таблице {1}! Это баг..." + hitemsNotExists.Count(), tableName));

            List<FERHRIAmurServiceReference.VariableType> fitemsNotExists = fitems.FindAll(x => !hitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                && y.NameShort == x.NameShort
                && y.NameEng == x.NameEng
                ));
            Console.WriteLine("\t{0} missing rows in the {1} table.", fitemsNotExists.Count(), tableName);
            foreach (var item in fitemsNotExists)
            {
                Console.WriteLine("\t\t{0} {1}", item.Id, item.Name);
                dmmHBR.VariableTypeRepository.Insert(new Meta.VariableType(item.Id, item.Name, item.NameShort, item.Description));
                Console.WriteLine("\t\t\tSinchronized...");
            }
        }

        private void SinchronizeUnitHBR()
        {
            string tableName = NameHBR + ".unit";
            List<FERHRIAmurServiceReference.Unit> fitems = ClientFERHRI.GetUnitsAll(HandleFERHRI);
            List<HBRAmurServiceReference.Unit> hitems = ClientHBR.GetUnitsAll(HandleHBR);

            List<HBRAmurServiceReference.Unit> hitemsNotExists = hitems.FindAll(x => !fitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                ));
            if (hitemsNotExists.Count() > 0)
                throw new Exception(string.Format("{0} отличных от центрального хранилища записей в таблице {1}! Это баг..." + hitemsNotExists.Count(), tableName));

            List<FERHRIAmurServiceReference.Unit> fitemsNotExists = fitems.FindAll(x => !hitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                ));
            Console.WriteLine("\t{0} missing rows in the {1} table.", fitemsNotExists.Count(), tableName);
            foreach (var item in fitemsNotExists)
            {
                Console.WriteLine("\t\t{0} {1}", item.Id, item.Name);
                dmmHBR.UnitRepository.Insert(new Meta.Unit(item.Id, item.Type, item.Name, item.Abbreviation, item.NameEng, item.AbbreviationEng, item.SIConvertion));
                Console.WriteLine("\t\t\tSinchronized...");
            }
        }
        private void SinchronizeMethodHBR()
        {
            string tableName = NameHBR + ".method";
            List<FERHRIAmurServiceReference.Method> fitems = ClientFERHRI.GetMethodsAll(HandleFERHRI);
            List<HBRAmurServiceReference.Method> hitems = ClientHBR.GetMethodsAll(HandleHBR);

            List<HBRAmurServiceReference.Method> hitemsNotExists = hitems.FindAll(x => !fitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                ));
            if (hitemsNotExists.Count() > 0)
                throw new Exception(string.Format("{0} отличных от центрального хранилища записей в таблице {1}! Это баг..." + hitemsNotExists.Count(), tableName));

            List<FERHRIAmurServiceReference.Method> fitemsNotExists = fitems.FindAll(x => !hitems.Exists(
                y => y.Id == x.Id
                && y.Name == x.Name
                ));
            Console.WriteLine("\t{0} missing rows in the {1} table.", fitemsNotExists.Count(), tableName);

            // WRITE METHODS
            foreach (var item in fitemsNotExists)
            {
                Console.WriteLine("\t\t{0} {1}", item.Id, item.Name);
                Npgsql.NpgsqlTransaction tran = dmmHBR.Connection.BeginTransaction();

                try
                {
                    dmmHBR.MethodRepository.Insert(new Meta.Method()
                    {
                        Id = item.Id,
                        ParentId = item.ParentId,
                        Name = item.Name,
                        Order = item.Order,
                        SourceLegalEntityId = item.SourceLegalEntityId,
                        MethodOutputStoreParameters = item.MethodOutputStoreParameters,
                        Description = item.Description
                    });

                    FERHRI.Amur.Meta.MethodForecast mf = dmmFERHRI.MethodForecastRepository.Select(item.Id);
                    if (mf != null)
                        dmmHBR.MethodForecastRepository.Insert(mf);

                    FERHRI.Amur.Meta.MethodClimate mc = dmmFERHRI.MethodClimateRepository.Select(item.Id);
                    if (mc != null)
                        dmmHBR.MethodClimateRepository.Insert(mc);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                Console.WriteLine("\t\t\tSinchronized...");
            }
        }
        private void SinchronizeLegalEntityHBR()
        {
            string tableName = NameHBR + ".social.legal_entity";
            List<FERHRIAmurServiceReference.LegalEntity> fitems = ClientFERHRI.GetLegalEntityesAll(HandleFERHRI);
            List<FERHRI.Social.LegalEntity> hitems = dmsHBR.LegalEntityRepository.SelectAll(true);

            List<FERHRI.Social.LegalEntity> hitemsNotExists = hitems.FindAll(x => !fitems.Exists(
                y => y.Id == x.Id
                && y.NameRus == x.NameRus
                && y.NameRusShort == x.NameRusShort
                ));
            if (hitemsNotExists.Count() > 0)
                throw new Exception(string.Format("{0} отличных от центрального хранилища записей в таблице {1}! Это баг..." + hitemsNotExists.Count(), tableName));

            List<FERHRIAmurServiceReference.LegalEntity> fitemsNotExists = fitems.FindAll(x => !hitems.Exists(
                y => y.Id == x.Id
                && y.NameRus == x.NameRus
                && y.NameRusShort == x.NameRusShort
                ));
            Console.WriteLine("\t{0} missing rows in the {1} table.", fitemsNotExists.Count(), tableName);
            foreach (var item in fitemsNotExists)
            {
                Console.WriteLine("\t\t{0} {1}", item.Id, item.NameRus);
                dmsHBR.LegalEntityRepository.Insert(
                    new FERHRI.Social.LegalEntity()
                    {
                        Id = item.Id,
                        Type = item.Type,
                        NameRus = item.NameRus,
                        NameEng = item.NameEng,
                        NameRusShort = item.NameRusShort,
                        NameEngShort = item.NameEngShort,
                        AddrId = item.AddrId,
                        AddrAdd = item.AddrAdd,
                        Email = item.Email,
                        Phones = item.Phones,
                        ParentId = item.ParentId,
                        WebSite = item.WebSite
                    });
                Console.WriteLine("\t\t\tSinchronized...");
            }
        }
    }
}
