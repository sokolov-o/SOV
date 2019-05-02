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
    public partial class UCTaskCalcMetricList : UserControl
    {
        public UCTaskCalcMetricList()
        {
            InitializeComponent();
        }
        List<int> _taskCalcMetricId = null;
        public void Fill(List<int> taskCalcMetricId, int? selectedId = null)
        {
            _taskCalcMetricId = taskCalcMetricId;
            _isFilled = true;
            taskBindingSource.DataSource = TaskCalcMetricRepository.SelectView(taskCalcMetricId);
            _isFilled = false;

            infoLabel.Text = taskBindingSource.Count.ToString();
            taskBindingSource_CurrentChanged(null, null);
        }
        public TaskCalcMetric SelectedItem
        {
            get
            {
                if (taskBindingSource.Current != null)
                {
                    return (TaskCalcMetric)taskBindingSource.Current;
                }
                return null;
            }
        }

        private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SelectedItem != null)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucTaskCalcMetric.Value != null)
                {
                    if (ucTaskCalcMetric.Value.Id > 0)
                        DataManager.GetInstance().TaskCalcMetricRepository.Update(ucTaskCalcMetric.Value);
                    else
                    {
                        int i = DataManager.GetInstance().TaskCalcMetricRepository.Insert(ucTaskCalcMetric.Value);
                        if (_taskCalcMetricId != null && !_taskCalcMetricId.Exists(x => x == i))
                            _taskCalcMetricId.Add(i);
                    }
                }

                Fill(_taskCalcMetricId);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении.\n\n" + ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ucTaskCalcMetric.Value = SelectedItem;
        }
        bool _isFilled = false;
        private void taskBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (!_isFilled)
                ucTaskCalcMetric.Value = SelectedItem;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            ucTaskCalcMetric.Value = new TaskCalcMetric();
        }

        private void runTaskButton_Click(object sender, EventArgs e)
        {
            //Task.CalcMetric.Run(SelectedItem);
        }

        private void UCTaskCalcMetricList_Load(object sender, EventArgs e)
        {
            ucTaskCalcMetric.SetActionsList(DataManager.GetInstance().ActionRepository
                .Select().OrderBy(x=>x.Name).ToList());
            ucTaskCalcMetric.SetFieldsList(DataManager.GetInstance().FieldRepository
                .Select().OrderBy(x => x.Name).ToList());
        }
    }
}
