using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Geo;

namespace SOV.SGMO
{
    public partial class FormNewTrackPart : Form
    {
        public FormNewTrackPart(Track trackParent)
        {
            InitializeComponent();

            TrackParent = trackParent;
            dateSUTCDateTimePicker.Value = DateTime.Today;
        }

        Track _trackParent = null;
        public Track TrackParent
        {
            get { return _trackParent; }
            set
            {
                if (value.ParentId.HasValue)
                    throw new Exception("Создание части маршрута у части маршрута не допускается в данной версии ПО.");
                _trackParent = value;
            }
        }

        public List<GeoPoint> RefPoints { get; set; }

        private void FormNewTrackPart_Load(object sender, EventArgs e)
        {
            RefPoints = DataManager.GetInstance().TrackPointsRepository
                .SelectByTrackId(TrackParent.Id)
                .OrderBy(x => x.DateUTC)
                .Select(x => x.GeoPoint)
                .ToList();
            refPointsListBox.Items.AddRange(RefPoints.ToArray());
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // CALC POINTS
                List<GeoPoint> refPoints = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(TrackParent.Id).Select(x => x.GeoPoint).ToList();
                GeoPoint startPoint = new GeoPoint(double.Parse(latTextBox.Text), double.Parse(latTextBox.Text));

                List<GeoPoint> points = Track.GetTrackPoints(
                    refPoints,
                    dateSUTCDateTimePicker.Value,
                    startPoint,
                    double.Parse(speedTextBox.Text) * 1000 / 3600,
                    refPointsListBox.SelectedIndex,
                    1,
                    int.Parse(hoursCountTextBox.Text),
                    1
                );

                // INSERT POINTS
                Track childTrack = new Track()
                {
                    Id = -1,
                    DateSUTC = dateSUTCDateTimePicker.Value,
                    SiteId = TrackParent.SiteId,
                    NameRus = string.Format("{0} [{1}]", dateSUTCDateTimePicker.Value.ToString("dd.MM.yyyy HH"), TrackParent.NameRus),
                    NameEng = string.Format("{0} [{1}]", dateSUTCDateTimePicker.Value.ToString("dd.MM.yyyy HH"), TrackParent.NameEng),
                    ParentId = TrackParent.Id
                };
                DataManager.GetInstance().TrackRepository.Insert(childTrack);

                //List<TrackPoint> trackPoints = points.Select(x => new TrackPoint() { GeoPoint = x, TrackId = TrackParent }).ToList();
                //DataManager.GetInstance().Insert()

                DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void LonTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
