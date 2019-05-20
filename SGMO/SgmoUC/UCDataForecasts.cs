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
                dgvData.DataSource = value?.OrderBy(x => x.DateFcsUTC).ToList();
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

        private void DeleteFilterButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvFilter.Rows)
            {
                row.Cells["Like"].Value = null;
            }
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void AcceptFilterButton_Click(object sender, EventArgs e)
        {
            dgvData.SuspendLayout();
            try
            {
                dgvData.ClearSelection();
                foreach (DataGridViewRow rowData in dgvData.Rows)
                {
                    bool visible = true;
                    foreach (DataGridViewRow rowFilter in dgvFilter.Rows)
                    {
                        object like = rowFilter.Cells["Like"].Value;
                        if (like != null && !string.IsNullOrEmpty((string)like))
                        {
                            switch (rowFilter.Cells[0].Value)
                            {
                                case "Переменная":
                                    if (rowData.Cells["variableNameDataGridViewTextBoxColumn"].Value.ToString().ToUpper().IndexOf(like.ToString().ToUpper()) < 0)
                                        visible = false;
                                    break;

                            }
                        }
                    }
                    rowData.Selected = false;
                    rowData.Visible = visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dgvData.ResumeLayout();
            }
        }
    }
}
