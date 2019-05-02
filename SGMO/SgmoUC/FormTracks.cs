using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Common;

namespace FERHRI.SGMO
{
    public partial class FormTracks : Form
    {
        public enum EnumMode
        {
            /// <summary>
            /// Выбор отдельного (одного) маршрута.
            /// </summary>
            SelectOne = 1
        }
        public FormTracks()
        {
            InitializeComponent();
        }
        EnumMode _Mode;
        public EnumMode Mode
        {
            get { return _Mode; }
            set
            {
                _Mode = value;
                switch (value)
                {
                    case EnumMode.SelectOne:
                        Text = "Выбор маршрута";
                        button1.Text = "Выбрать";
                        break;
                }
            }
        }
        public SGMO.Track0 CurrentTrack0
        {
            get
            {
                object curItem = ucTrack0.GetSelectedItem();
                return curItem == null ? null : (SGMO.Track0)curItem;
            }
        }

        private void FormTracks_Load(object sender, EventArgs e)
        {
            try
            {
                List<Track0> tracks = DataManager.GetInstance().TrackRepository.SelectTrack0(null, true);
                ucTrack0.SetDataSource(tracks.ToList<object>(), "Name");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
