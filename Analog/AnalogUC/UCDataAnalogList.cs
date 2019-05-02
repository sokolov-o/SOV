using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Analog
{
    public partial class UCDataAnalogList : UserControl
    {
        public UCDataAnalogList()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Не реализовано. OSokolov@201806");
            dataAnalogBindingSource.EndEdit();
            dgv.EndEdit();
            foreach (var item in dataAnalogBindingSource.DataMember)
            {
                
            }
        }

        public void Fill(List<DataAnalog0> dataAnalogs)
        {
            dataAnalogBindingSource.DataSource = (dataAnalogs == null) ? null : dataAnalogs.OrderBy(x => x.DateIni);
        }
    }
}
