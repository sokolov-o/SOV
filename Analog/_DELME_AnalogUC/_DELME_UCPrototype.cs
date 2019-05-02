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
    public partial class _DELME_UCPrototype : UserControl
    {
        public _DELME_UCPrototype()
        {
            InitializeComponent();
        }
        RowStyle row = null;
        private void ucShowButton_Click(object sender, EventArgs e)
        {
            //tlp.SuspendLayout();
            if (row == null)
            {
                row = tlp.RowStyles[1];
                tlp.RowStyles.Remove(row);
            }
            else
            {
                tlp.RowStyles.Add(row);
                row = null;
            }
            //tlp.ResumeLayout();
            tlp.Parent.Parent.PerformLayout();
        }
    }
}
