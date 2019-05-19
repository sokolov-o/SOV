using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.SGMO
{
    public partial class UCDataForecasts : UserControl
    {
        public UCDataForecasts()
        {
            InitializeComponent();
        }

        public List<DataFcsExt> Items
        {
            set
            {
                dgv.DataSource = value?.OrderBy(x => x.DateFcsUTC).ToList();
                infoLabel.Text = (value == null ? 0 : value.Count).ToString();
            }
        }

        private void FilterToolStripButton_Click(object sender, EventArgs e)
        {
            if(splitContainer1.Panel2.Controls.Count==0)
            {
                System.Windows.Forms.Label label1 = new Label();
                label1.AutoSize = true;
                label1.Location = new System.Drawing.Point(3, 0);
                label1.Name = "label1";
                label1.Size = new System.Drawing.Size(0, 13);
                label1.TabIndex = 0;
            }
        }
    }
}
