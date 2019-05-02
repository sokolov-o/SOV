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
    public partial class _UCPrototypeDGV : UserControl
    {
        public _UCPrototypeDGV()
        {
            InitializeComponent();
        }

        public void Fill(List<object> ids, int? selectedId = null)
        {

            infoLabel.Text = dgvBindingSource.Count.ToString();
        }
        public object SelectedItem
        {
            get
            {
                return (dgvBindingSource.Current != null) ? dgvBindingSource.Current : null;
            }
        }

    }
}
