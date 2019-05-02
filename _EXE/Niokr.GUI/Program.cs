using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Common;

namespace FERHRI.Niokr
{
    static class Program
    {
        static public string ConnectionStringAmur { get; set; }
        static public string ConnectionStringNiokr { get; set; }
        static public User User { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // USER & connection string

            string suser = global::FERHRI.Niokr.Properties.Settings.Default.User;
            if (!string.IsNullOrEmpty(suser))
            {
                User = new Common.User(suser.Split(';')[0], suser.Split(';')[1]);
            }

            Common.FormUserPassword frm = new Common.FormUserPassword(
                StrVia.ToDictionaryPairs(global::FERHRI.Niokr.Properties.Settings.Default.AmurConnectionString, '/'),
                User);
            if (frm.ShowDialog() != DialogResult.OK) return;
            User = frm.User;
            if (User == null)
                return;

            ConnectionStringAmur = frm.ConnectionString;
            ConnectionStringNiokr = new string(ConnectionStringAmur.ToArray());
            ConnectionStringNiokr = Common.ADbNpgsql.ConnectionStringUpdateDataBase(ConnectionStringNiokr, "niokr");

            // SCHEMAS CONNECTION STRINGS
            Amur.Meta.DataManager.SetDefaultConnectionString(ConnectionStringAmur);
            DataManager.SetDefaultConnectionString(ConnectionStringNiokr);

            Application.Run(new FERHRI.Niokr.Rid.FormRidTree());
            //Application.Run(new FormTheams(true));
        }
    }
}
