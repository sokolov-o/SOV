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
    public partial class UCFieldGeoList : UserControl
    {
        public UCFieldGeoList()
        {
            InitializeComponent();
        }
        public void Fill(List<int> fieldGeoIds = null, int? selectedId = null)
        {
            Clear();

            List<_DELME_FieldGeoobs> fgs = DataManager.GetInstance().FieldGeoobsRepository.Select(fieldGeoIds);
            List<Field> fs = DataManager.GetInstance().FieldRepository.Select(fgs.Select(x => x.FieldId).ToList());
            List<_DELME_GeoobsMetric> gms = DataManager.GetInstance().GeoobsMetricRepository.Select(fgs.Select(x => x.GeoobMetricId).ToList());

            List<Common.IdName> idNames = fgs.Select(x => new Common.IdName()
            {
                Id = x.Id,
                Name = fs.First(y => y.Id == x.FieldId).Name + "=" + gms.First(z => z.Id == x.GeoobMetricId).Name
            }).OrderBy(x => x.Name).ToList();
            idNameBindingSource.DataSource = idNames;

            if (selectedId.HasValue)
                idNameBindingSource.Position = idNameBindingSource.Find("Id", (int)selectedId);

            infoLabel.Text = idNames.Count.ToString();
        }

        internal void Clear()
        {
            idNameBindingSource.Clear();
        }
        //public FieldGeoobs SelectedItem
        //{
        //    get
        //    {
        //        return (idNameBindingSource.Current != null) ? (FieldGeoobs)idNameBindingSource.Current : null;
        //    }
        //    set
        //    {
        //        SelectedId = (value == null) ? null : (int?)value.Id;
        //        //if (value == null)
        //        //    idNameBindingSource.Position = -1;
        //        //else
        //        //{
        //        //    int i = idNameBindingSource.Find("Id", value.Id);
        //        //    idNameBindingSource.Position = (i >= 0) ? i : -1;
        //        //}
        //    }
        //}
        public int? SelectedId
        {
            get
            {
                return (idNameBindingSource.Current != null) ? (int?)((Common.IdName)idNameBindingSource.Current).Id : null;
            }
            set
            {
                idNameBindingSource.Position = -1;
                if (value != null)
                {
                    for (int i = 0; i < idNameBindingSource.Count; i++)
                    {
                        if (((Common.IdName)idNameBindingSource[i]).Id == value)
                        {
                            idNameBindingSource.Position = i;
                            dgv.FirstDisplayedScrollingRowIndex = dgv.SelectedRows[0].Index;
                        }
                    }
                    //int i = idNameBindingSource.Find("Id", (int)value);
                    //idNameBindingSource.Position = (i >= 0) ? i : -1;
                }
            }
        }
        private void UCTaskCalcMetric_Load(object sender, EventArgs e)
        {
            //List<FieldGeoobs> fgs = DataManager.GetInstance().FieldGeoobsRepository.Select();
            //List<Field> fs = DataManager.GetInstance().FieldRepository.Select(fgs.Select(x => x.FieldId).ToList());
            //List<GeoobsMetric> gms = DataManager.GetInstance().GeoobsMetricRepository.Select(fgs.Select(x => x.GeoobMetricId).ToList());

            //List<Common.IdName> idNames = fgs.Select(x => new Common.IdName()
            //{
            //    Id = x.Id,
            //    Name = fs.First(y => y.Id == x.FieldId).Name + "=" + gms.First(z => z.Id == x.GeoobMetricId).Name
            //}).OrderBy(x => x.Name).ToList();
            //fieldGeoDGV.DataSource = idNames;

            //infoLabel.Text = idNames.Count.ToString();
        }
    }
}
