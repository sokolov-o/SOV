using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGMO_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void mnuFileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuDicWarningPileCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Не реализовано. OSokolov@2017.08");
        }
    }
}
