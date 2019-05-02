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
    public partial class UCTaskCalcMetric : UserControl
    {
        public UCTaskCalcMetric()
        {
            InitializeComponent();

            try
            {
                timeProcessTypeBindingSource.DataSource = TimeProcessTypeRepository.GetCash().OrderBy(x => x.Name).ToArray();
                fieldGeoobsViewBindingSource.DataSource =  _DELME_FieldGeoobsRepository.SelectView().ToArray();
            }
            catch { }
        }
        int _id = -1;
        public TaskCalcMetric Value
        {
            set
            {
                Clear();
                if (value != null)
                {
                    _id = value.Id;
                    isActualCheckBox.Checked = value.IsActual;

                    // fieldGeoobseComboBox COMBOBOX
                    fieldGeoobsViewBindingSource.Position = -1;
                    if (value.FieldGeoId > 0)
                    {
                        _DELME_FieldGeoobsView a = ((_DELME_FieldGeoobsView[])fieldGeoobsViewBindingSource.DataSource).FirstOrDefault(x => x.FieldGeoobs.Id == value.FieldGeoId);
                        if (a == null) throw new Exception("В выпадающем списке отсутствует элемент с кодом id=" + value.FieldGeoId);
                        fieldGeoobsComboBox.SelectedItem = a;
                    }

                    // timeProcessTypeComboBox COMBOBOX
                    timeProcessTypeBindingSource.Position = -1;
                    if (value.TimeProcessTypeId > 0)
                    {
                        TimeProcessType a = ((TimeProcessType[])timeProcessTypeBindingSource.DataSource).FirstOrDefault(x => x.Id == value.TimeProcessTypeId);
                        if (a == null) throw new Exception("В выпадающем списке отсутствует элемент с кодом id=" + value.TimeProcessTypeId);
                        timeProcessTypeComboBox.SelectedItem = a;
                    }
                }
            }
            get
            {
                try
                {
                    if (fieldGeoobsViewBindingSource.Count == 0 || timeProcessTypeBindingSource.Count == 0)
                        return null;

                    return new TaskCalcMetric()
                    {
                        Id = _id,
                        FieldGeoId = ((_DELME_FieldGeoobsView)fieldGeoobsComboBox.SelectedItem).FieldGeoobs.Id,
                        TimeProcessTypeId = ((Common.IdName)timeProcessTypeComboBox.SelectedItem).Id,
                        IsActual = isActualCheckBox.Checked
                    };

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        private void Clear()
        {
            _id = -1;
            fieldGeoobsComboBox.SelectedIndex = -1;
            isActualCheckBox.Checked = false;
            timeProcessTypeComboBox.SelectedIndex = -1;
        }

        private void UCTaskCalcMetric_Load(object sender, EventArgs e)
        {
        }
    }
}
