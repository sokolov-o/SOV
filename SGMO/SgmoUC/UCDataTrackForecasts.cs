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
    public partial class UCDataTrackForecasts : UserControl
    {
        public UCDataTrackForecasts()
        {
            InitializeComponent();
        }

        List<DataTrackFcsExt> items;
        public List<DataTrackFcsExt> Items
        {
            get
            {
                return items;
            }
            set
            {
                dgv.DataSource = value?.OrderBy(x => x.DataTrackFcs.LeadTime).ToList();
            }
        }
    }
}
