using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Common;
using FERHRI.Amur.Meta;

namespace FERHRI.Analog
{
    public partial class UCGeoobsMetricList : UserControl
    {
        public UCGeoobsMetricList()
        {
            InitializeComponent();
        }
        int _fieldId = -1;

        public enum EnumUCType
        {
            GeoobsMetric,
            GeoobsMetric4Field
        }
        [DefaultValue((int)EnumUCType.GeoobsMetric4Field)]
        public EnumUCType UCType { get; set; }

        public void Fill4Field(int fieldId)
        {
            Clear();
            _fieldId = fieldId;
            _geoobsMetrics = null;
            UCType = EnumUCType.GeoobsMetric4Field;

            List<_DELME_FieldGeoobs> fieldGeoobs = DataManager.GetInstance().FieldGeoobsRepository.SelectByField(fieldId);
            List<_DELME_GeoobsMetric> geoobsMetric = DataManager.GetInstance().GeoobsMetricRepository.Select(fieldGeoobs.Select(x => x.GeoobMetricId).ToList());

            fieldGeoBindingSource.DataSource = fieldGeoobs
                .Select(x => new IdNameRE()
                {
                    Id = x.Id,
                    NameRus = geoobsMetric.FirstOrDefault(z => z.Id == x.GeoobMetricId).Name,
                    NameEng = MathVarRepository.GetCash().FirstOrDefault(y => y.Id == geoobsMetric.FirstOrDefault(z => z.Id == x.GeoobMetricId).MetricMathvarId).Name
                })
                .OrderBy(x => x.Name)
                .ToArray();
        }
        List<_DELME_GeoobsMetric> _geoobsMetrics;
        public void Fill4GeoobsMetric(List<_DELME_GeoobsMetric> geoobsMetrics)
        {
            Clear();
            _fieldId = -1;
            _geoobsMetrics = geoobsMetrics;
            UCType = EnumUCType.GeoobsMetric;

            fieldGeoBindingSource.DataSource = geoobsMetrics
                .Select(x => new IdNameRE()
                {
                    Id = x.Id,
                    NameRus = x.Name,
                    NameEng = MathVarRepository.GetCash().FirstOrDefault(y => y.Id == x.MetricMathvarId).Name
                })
                .OrderBy(x => x.NameEng)
                .ToArray();
        }
        public void SetUCOptions(bool showAddButton, bool showDeleteButton)
        {
            addNewButton.Visible = showAddButton;
            deleteButton.Visible = showDeleteButton;
        }
        public void Clear()
        {
            fieldGeoBindingSource.Clear();
        }
        FormGeoobsMetric __formGeoobsMetric = null;
        FormGeoobsMetric _formGeoobsMetric
        {
            get
            {
                if (__formGeoobsMetric == null)
                {
                    __formGeoobsMetric = new FormGeoobsMetric(Amur.Meta.MathVarRepository.GetCash());
                }
                return __formGeoobsMetric;
            }
        }
        private void addNewButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (UCType)
                {
                    case EnumUCType.GeoobsMetric:
                        if (_formGeoobsMetric.ShowDialog() == DialogResult.OK)
                        {
                            _geoobsMetrics.Add(_formGeoobsMetric.Value);
                            Fill4GeoobsMetric(_geoobsMetrics);
                        }
                        break;
                    case EnumUCType.GeoobsMetric4Field:
                        FormGeoobsMetricList frm = new FormGeoobsMetricList();
                        int? geoobMetricId = frm.ShowSelect(DataManager.GetInstance().GeoobsMetricRepository.Select());
                        if (geoobMetricId != null)
                        {
                            DataManager.GetInstance().FieldGeoobsRepository.Insert(new _DELME_FieldGeoobs() { Id = -1, FieldId = _fieldId, GeoobMetricId = (int)geoobMetricId });
                            Fill4Field(_fieldId);
                        }
                        break;
                    default:
                        MessageBox.Show("Неизвестный тип элемента управления для выполнения действия по кнопке.");
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public int? CurrentId
        {
            get
            {
                if (fieldGeoBindingSource.Current != null)
                    return ((IdNameRE)fieldGeoBindingSource.Current).Id;
                return null;
            }
        }

        private void ucFieldGeoob_UCItemSaved()
        {
            Fill();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int? id = CurrentId;
            if (id.HasValue)
            {
                switch (UCType)
                {
                    case EnumUCType.GeoobsMetric:
                        DataManager.GetInstance().GeoobsMetricRepository.Delete((int)id);
                        if (UCType == EnumUCType.GeoobsMetric) _geoobsMetrics.Remove(_geoobsMetrics.First(x => x.Id == (int)id));
                        break;
                    case EnumUCType.GeoobsMetric4Field:
                        DataManager.GetInstance().FieldGeoobsRepository.Delete((int)id);
                        break;
                    default:
                        MessageBox.Show("Неизвестный тип элемента управления для выполнения действия по кнопке.");
                        break;
                }
                Fill();
            }
        }

        private void Fill()
        {
            switch (UCType)
            {
                case EnumUCType.GeoobsMetric:
                    Fill4GeoobsMetric(DataManager.GetInstance().GeoobsMetricRepository.Select(_geoobsMetrics.Select(x => x.Id).ToList()));
                    break;
                case EnumUCType.GeoobsMetric4Field:
                    Fill4Field(_fieldId);
                    break;
                default:
                    MessageBox.Show("Неизвестный тип элемента управления для выполнения действия по кнопке.");
                    break;
            }
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            int? id = CurrentId;
            if (id.HasValue)
            {
                _DELME_GeoobsMetric gm = null;
                switch (UCType)
                {
                    case EnumUCType.GeoobsMetric:
                        gm = DataManager.GetInstance().GeoobsMetricRepository.Select((int)id);
                        break;
                    case EnumUCType.GeoobsMetric4Field:
                        DataManager.GetInstance().GeoobsMetricRepository.Select(DataManager.GetInstance().FieldGeoobsRepository.Select((int)id).GeoobMetricId);
                        ;
                        break;
                    default:
                        MessageBox.Show("Неизвестный тип элемента управления для выполнения действия по кнопке.");
                        return;
                }
                _formGeoobsMetric.Value = gm;
                if (_formGeoobsMetric.ShowDialog() == DialogResult.OK)
                {
                    Fill();
                }
            }
        }
    }
}
