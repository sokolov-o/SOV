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
    public partial class UCTrackPoints : UserControl
    {
        public UCTrackPoints()
        {
            InitializeComponent();
        }

        List<TrackPoint> items;
        public List<TrackPoint> Items
        {
            get
            {
                return items;
            }
            set
            {
                dgv.DataSource = value?.OrderBy(x => x.DateUTC).ToList();
            }
        }
    }
}
