using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Infores
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] up = Properties.Settings.Default.UserPassword.Split(new char[] { ';' });
            Common.User user = new Common.User(up[0], up[1]);
            FERHRI.Infores.DataManager.SetDefaultConnectionString(Common.ADbNpgsql.ConnectionStringUpdateUser(Properties.Settings.Default.ConnectionStringSGMO, user));
            FERHRI.Social.DataManager.SetDefaultConnectionString(Common.ADbNpgsql.ConnectionStringUpdateUser(Properties.Settings.Default.ConnectionStringSocial, user));
            FERHRI.Amur.Meta.DataManager.SetDefaultConnectionString(Common.ADbNpgsql.ConnectionStringUpdateUser(Properties.Settings.Default.ConnectionStringAmur, user));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
