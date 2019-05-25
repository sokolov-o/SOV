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

        public List<TrackPoint> Items
        {
            get
            {
                return (List<TrackPoint>)dgv.DataSource;
            }
            set
            {
                dgv.DataSource = null;
                infoLabel.Text = "...";
                if (value != null)
                {
                    dgv.DataSource = value?.OrderBy(x => x.DateUTC).ToList();
                    infoLabel.Text = "id=" + value[0].TrackId + " (" + value.Count + ")";
                }
            }
        }
    }
}
