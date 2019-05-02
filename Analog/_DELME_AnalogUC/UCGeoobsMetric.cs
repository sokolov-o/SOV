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
    public partial class UCGeoobsMetric : UserControl
    {
        public UCGeoobsMetric()
        {
            InitializeComponent();

            ucGeoobWeight.SetUCOptions(true, true, false, false, true);
        }
        public bool UCToolbarVisible
        {
            set
            {
                toolStrip1.Visible = value;
            }
        }
        public bool UCSaveButtonVisible
        {
            set
            {
                saveButton.Visible = value;
            }
        }

        public void SetDictionaryes(List<Amur.Meta.MathVar> mathVars)
        {
            mathVarBindingSource.DataSource = mathVars.OrderBy(x => x.NameRus).ToList().ToArray();
            metricComboBox.SelectedIndex = -1;
        }

        int _id = -1;
        public _DELME_GeoobsMetric Value
        {
            set
            {
                Clear();

                if (value != null)
                {
                    _id = value.Id;
                    nameTextBox.Text = value.Name;
                    mathVarBindingSource.Position = -1;
                    if (value.MetricMathvarId > 0)
                    {
                        Amur.Meta.MathVar a = ((Amur.Meta.MathVar[])mathVarBindingSource.DataSource).FirstOrDefault(x => x.Id == value.MetricMathvarId);
                        if (a == null) throw new Exception("В выпадающем списке отсутствует метрика id=" + value.MetricMathvarId);
                        metricComboBox.SelectedItem = a;
                    }
                    ucGeoobWeight.Fill(ToDictionaryGeoobWeight(value.GeoobIdWeights), "Регион", "Вес");
                }
            }
            get
            {
                try
                {
                    Dictionary<object, object> geoobs = ucGeoobWeight.GetDictionary();

                    _DELME_GeoobsMetric ret = new _DELME_GeoobsMetric()
                    {
                        Id = _id,
                        Name = nameTextBox.Text,
                        MetricMathvarId = ((Amur.Meta.MathVar)mathVarBindingSource.Current).Id,
                        GeoobIdWeights = geoobs.Count == 0
                            ? null
                            : geoobs.Select(x => new IntDouble() { Int = ((Amur.Meta.GeoObject)x.Key).Id, Double = Common.StrVia.ParseDouble(x.Value.ToString()) })
                           .ToArray()
                    };
                    return ret.GeoobIdWeights == null ? null : ret;
                }
                catch
                {
                    return null;
                }
            }
        }
        public Dictionary<object, object> ToDictionaryGeoobWeight(IntDouble[] items)
        {
            Dictionary<object, object> ret = new Dictionary<object, object>();
            if (items != null)
            {
                List<FERHRI.Amur.Meta.GeoObject> geoobs = FERHRI.Amur.Meta.DataManager.GetInstance().GeoObjectRepository.Select(items.Select(x => x.Int).ToList());
                foreach (var item in items)
                {
                    ret.Add(geoobs.FirstOrDefault(x => x.Id == item.Int), item.Double);
                }
            }
            return ret;
        }

        void Clear()
        {
            nameTextBox.Text = "";
            metricComboBox.SelectedIndex = -1;
            ucGeoobWeight.Clear();
        }
        Common.FormListBox frmSelectGeoob = null;

        private void ucGeoobWeight_UCAddNew()
        {
            if (frmSelectGeoob == null)
            {
                List<Amur.Meta.GeoObject> geoobs = Amur.Meta.DataManager.GetInstance().GeoObjectRepository.SelectByType((int)Amur.Meta.EnumGeoObject.GeoReg);
                frmSelectGeoob = new Common.FormListBox("Геообъект для расчёта метрик", geoobs.ToArray(), "Name", false);
            }
            if (frmSelectGeoob.ShowDialog() == DialogResult.OK)
            {
                object geoob = frmSelectGeoob.GetSelectedItem();
                if (geoob != null)
                {
                    ucGeoobWeight.Add(geoob, (double)1);
                }
            }
        }
        //public delegate void UCItemSavedEventHandler();
        //public event UCItemSavedEventHandler UCItemSaved;

        private void saveButton_Click(object sender, EventArgs e)
        {
            //    try
            //    {
            //        GeoobsMetric value = Value;
            //        if (value == null)
            //        {
            //            MessageBox.Show("Отсутствует значение для сохранения. Возможно, заполнены не всё обязательные поля формы." +
            //                "\nИнформация не сохранена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //        else if (value.Id > 0)
            //            Analog.DataManager.GetInstance().GeoobsMetricRepository.Update(value);
            //        else
            //            Analog.DataManager.GetInstance().GeoobsMetricRepository.Insert(value);

            //        UCItemSaved?.Invoke();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
        }
    }
}
