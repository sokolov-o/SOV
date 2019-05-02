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
    public partial class FormGeoobsMetricList : Form
    {
        public FormGeoobsMetricList()
        {
            InitializeComponent();
        }

        public UCGeoobsMetricList.EnumUCType UCType { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Fill4GeoobsMetric(List<_DELME_GeoobsMetric> geoobsMetrics)
        {
            UCType = UCGeoobsMetricList.EnumUCType.GeoobsMetric;
            ucGeoobsMetricList.Fill4GeoobsMetric(geoobsMetrics);
        }
        public void Fill4Field(int fieldId)
        {
            UCType = UCGeoobsMetricList.EnumUCType.GeoobsMetric4Field;
            ucGeoobsMetricList.Fill4Field(fieldId);
        }
        /// <summary>
        /// Выбрать метрику и регионы.
        /// </summary>
        /// <param name="geoobsMetrics">Список выбора.</param>
        /// <returns>Код выбранной записи или null, если отмена.</returns>
        public int? ShowSelect(List<_DELME_GeoobsMetric> geoobsMetrics)
        {
            Text = "Выбрать метрику и регионы";
            button1.Text = "Выбрать";
            button2.Text = "Отменить";
            ucGeoobsMetricList.Fill4GeoobsMetric(geoobsMetrics);

            if (ShowDialog() == DialogResult.OK)
                return ucGeoobsMetricList.CurrentId;
            return null;
        }
        public DialogResult ShowEdit(List<_DELME_GeoobsMetric> geoobsMetrics)
        {
            Text = "Выбор метрики и регионов";
            button1.Text = "Сохранить";
            button2.Text = "Закрыть";
            ucGeoobsMetricList.Fill4GeoobsMetric(geoobsMetrics);

            return ShowDialog();
        }
    }
}
