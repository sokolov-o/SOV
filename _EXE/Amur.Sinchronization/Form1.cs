using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Amur.Sinchronization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Синхронизация переменных по id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VariableSinchButton_Click(object sender, EventArgs e)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Program._AmurServices.SinchronizeVariableHBR();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = cs;
            }
        }
    }
}
