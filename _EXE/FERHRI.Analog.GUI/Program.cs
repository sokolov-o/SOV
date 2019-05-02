using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Common;

namespace FERHRI.Analog.GUI
{
    static class Program
    {
        static public FERHRI.Common.User User { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Predefinition();

            Application.Run(new Form1());
        }

        /// <summary>
        /// 
        /// - Определить пользователя БД.
        /// - Определить строки подсоединения к БД.
        /// 
        /// </summary>
        static void Predefinition()
        {
            string suser = Properties.Settings.Default.User;
            if (!string.IsNullOrEmpty(suser))
            {
                User = new User(suser.Split(';')[0], suser.Split(';')[1]);
            }

            // GET USER
            using (FormUserPassword frm = new Common.FormUserPassword(User))
            {
                if (frm.ShowDialog() != DialogResult.OK || frm.User == null) return;

                User = frm.User;
            }
            
            // UPDATE CONNECTION STRINGS
            MetaDb.DataManager.SetDefaultConnectionString(ADbNpgsql.ConnectionStringUpdateUser(Properties.Settings.Default.MetaDbConnectionString, User));
            MetaDb.DataManager MetaDbDataManager = MetaDb.DataManager.GetInstance();

            Amur.Meta.DataManager.SetDefaultConnectionString(
                ADbNpgsql.ConnectionStringUpdateUser(MetaDbDataManager.DbListRepository.Select((int)MetaDb.Enums.Db.Amur_FERHRI).ConnectionString, User));
            Analog.DataManager.SetDefaultConnectionString(
                ADbNpgsql.ConnectionStringUpdateUser(MetaDbDataManager.DbListRepository.Select((int)MetaDb.Enums.Db.Analog_FERHRI).ConnectionString, User));
        }
    }
}
