using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur;

namespace SOV.SGMO
{
    public partial class UCMessageSite : UserControl
    {
        public UCMessageSite()
        {
            InitializeComponent();
        }
        public static string DATETIME_FORMAT = "dd.MM.yyyy HH";
        public void Fill(MessageSite value)
        {
            idTextBox.Text = null;
            messageTypeComboBox.SetSelectedId(-777);
            langTypeComboBox.SetSelectedId(-777);

            siteTextBox.Text = null;
            methodTextBox.Text = null;

            messageTextBox.Text = null;

            dateIniTextBox.Text = null;
            dateFcsLlabel.Text = null;
            dateFcsTextBox.Text = null;
            dateInsertTextBox.Text = null;
            leadHoursTextBox.Text = null;

            if (value != null)
            {
                idTextBox.Text = value.Id.ToString();
                messageTypeComboBox.SetSelectedId((int)value.MessageType);
                langTypeComboBox.SetSelectedId((int)value.Language);

                siteTextBox.Text = Amur.Meta.Site.GetName(Amur.Meta.DataManager.GetInstance().SiteRepository.Select(value.SiteId), 2);
                siteTextBox.Tag = value.SiteId;

                methodTextBox.Text = SOV.Amur.Meta.DataManager.GetInstance().MethodRepository.Select(value.MethodId).Name;
                methodTextBox.Tag = value.MethodId;

                messageTextBox.Text = value.Text;

                dateIniTextBox.Text = value.DateIni.ToString(DATETIME_FORMAT);
                dateFcsLlabel.Text = "Прогноз на (" + value.DateTypeFcs + ")";
                dateFcsTextBox.Text = value.DateFcs.ToString(DATETIME_FORMAT);
                dateInsertTextBox.Text = value.DateInsert.ToString(DATETIME_FORMAT + ":mm");
                leadHoursTextBox.Text = (value.DateFcs - value.DateIni).TotalHours.ToString();
            }
        }
        public void Clear()
        {
            Fill(null);
        }

        private void UCMessageSite_Load(object sender, EventArgs e)
        {
            messageTypeComboBox.DataSource(SOV.SGMO.MessageTypeRepository.GetCash()
                .Select(x => new Common.IdName() { Id = x.Id, Name = x.Name })
                .OrderBy(x => x.Name).ToArray());

            List<SOV.Common.IdName> langs = new List<Common.IdName>();
            foreach (var i in Enum.GetValues(typeof(SOV.Amur.Meta.EnumLanguage)))
            {
                langs.Add(new Common.IdName() { Id = (int)i, Name = ((Amur.Meta.EnumLanguage)i).ToString() });
            }
            langTypeComboBox.DataSource(langs.ToArray());
        }
    }
}
