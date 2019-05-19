using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.SGMO
{
    public partial class FormForecastParameters : Form
    {
        List<Amur.Meta.Method> _methods;
        DateTime? _dateIniUTC;
        public FormForecastParameters(DateTime? dateIniUTC, List<Amur.Meta.Method> methods)
        {
            InitializeComponent();

            _methods = methods;
        }

        private void FormTrackForecastParameters_Load(object sender, EventArgs e)
        {
            methodsListBox.Items.AddRange(_methods.ToArray());
            dateIniUTC.Value = _dateIniUTC.HasValue ? (DateTime)_dateIniUTC : DateTime.Today;
        }

        public DateTime DateIniUTC
        {
            get
            {
                return dateIniUTC.Value;
            }
        }

        public List<Amur.Meta.Method> Methods
        {
            get
            {
                if (methodsListBox.SelectedItems == null || methodsListBox.SelectedItems.Count == 0)
                    return null;

                List<Amur.Meta.Method> ret = new List<Amur.Meta.Method>();
                foreach (var item in methodsListBox.SelectedItems)
                {
                    ret.Add((Amur.Meta.Method)item);
                }
                return ret;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
