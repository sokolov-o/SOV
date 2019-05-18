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

        public List<DataTrackFcsExt> Items
        {
            //get
            //{
            //    return dgv.DataSource == null ? null : (List<DataTrackFcsExt>)dgv.DataSource;
            //}
            set
            {
                dgv.DataSource = value?.OrderBy(x => x.DateFcsUTC).ToList();
                infoLabel.Text = (value == null ? 0 : value.Count).ToString();
            }
        }
    }
}
