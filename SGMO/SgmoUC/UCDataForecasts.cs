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
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            if (!splitContainer1.Panel2Collapsed)
            {
                if (dgvFilter.RowCount == 0)
                {
                    int i = dgvFilter.Rows.Add(new DataGridViewRow());
                    dgvFilter.Rows[i].Cells[0].Value = "Переменная";
                }
            }
        }
    }
}
