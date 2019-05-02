using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Analog
{
    public partial class FormGeoobsMetric : Form
    {
        public FormGeoobsMetric(List<Amur.Meta.MathVar> mathVars)
        {
            InitializeComponent();

            ucGeoobsMetric.SetDictionaryes(mathVars);
            ucGeoobsMetric.UCToolbarVisible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _DELME_GeoobsMetric value = Value;
            if (value == null)
            {
                MessageBox.Show("Отсутствует значение для сохранения. Возможно, заполнены не всё обязательные поля формы." +
                    "\nИнформация не сохранена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (value.Id > 0)
                Analog.DataManager.GetInstance().GeoobsMetricRepository.Update(value);
            else
            {
                value.Id = Analog.DataManager.GetInstance().GeoobsMetricRepository.Insert(value);
                Value = value;
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public _DELME_GeoobsMetric Value
        {
            get
            {
                return ucGeoobsMetric.Value;
            }
            set
            {
                ucGeoobsMetric.Value = value;
                if (value.Id < 0)
                    Text = "Метрика и регионы" + ((value.Id < 0) ? "(СОЗДАТЬ)" : "");
            }
        }
    }
}
