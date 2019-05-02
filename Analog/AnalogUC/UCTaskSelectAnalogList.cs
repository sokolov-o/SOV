using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Amur.Meta;

namespace FERHRI.Analog
{
    public partial class UCTaskSelectAnalogList : UserControl
    {
        public UCTaskSelectAnalogList()
        {
            InitializeComponent();
        }
        public void Fill(List<int> selectAnalogIds, int? selectedItemId = null)
        {
            List<TaskSelectAnalog> selAnalogs = DataManager.GetInstance().TaskSelectAnalogRepository.Select(selectAnalogIds);
            List<Method> methods = Amur.Meta.DataManager.GetInstance().MethodRepository.Select(selAnalogs.Select(x => x.MethodId).Distinct().ToList());
            List<Unit> units = Amur.Meta.DataManager.GetInstance().UnitRepository.Select(selAnalogs.Select(x => x.TimeId).Distinct().ToList());

            taskSelectAnalogBindingSource.DataSource = selAnalogs.Select(x => new TaskSelectAnalogView()
            {
                Id = x.Id,
                MethodId = x.MethodId,
                TimeId = x.TimeId,
                MethodName = methods.First(y => y.Id == x.MethodId).Name,
                TimeName = units.First(z => z.Id == x.TimeId).Name
            }).OrderBy(x => x.MethodName).ToList();
        }
        TaskSelectAnalog CurTaskSelectAnalog
        {
            get
            {
                if (taskSelectAnalogBindingSource.Current == null)
                    return null;
                return (TaskSelectAnalog)(TaskSelectAnalogView)taskSelectAnalogBindingSource.Current;
            }
        }
        private void taskSelectAnalogBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            TaskSelectAnalog t = CurTaskSelectAnalog;
            if (t == null)
                ucDataAnalogList.Fill(null);
            else
                ucDataAnalogList.Fill(DataManager.GetInstance().DataAnalog0Repository.SelectBySelectAnalogId(t.Id, null));
        }
    }

    public class TaskSelectAnalogView : TaskSelectAnalog
    {
        public string MethodName { get; set; }
        public string TimeName { get; set; }
    }
}
