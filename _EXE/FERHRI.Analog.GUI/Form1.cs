using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Analog.GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void mnuFileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuEditFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string tabName = "Fields";
                if (!mainTabControl.TabPages.ContainsKey(tabName))
                {
                    // TAB
                    TabPage tp = new TabPage("Поля");
                    tp.Name = tabName;
                    tp.ToolTipText = "Журнал г/м полей, для которых рассчитываются метрики аналогичности";

                    // UC 4 TAB
                    UCFieldList uc = new UCFieldList();

                    List<int> ids = MetaDb.DataManager.GetInstance().DbInterfaceRepository.SelectByType((int)MetaDb.Enums.DbInterfaceType.IMeta)
                        .Select(x => x.DbListId).ToList();
                    uc.SetDictionaryes(
                        Analog.DataManager.GetInstance().ActionRepository.Select(),
                        MetaDb.DataManager.GetInstance().DbListRepository.Select(ids)
                    );
                    uc.Dock = DockStyle.Fill;
                    uc.Fill();

                    // ADD UC & TAB
                    tp.Controls.Add(uc);
                    mainTabControl.TabPages.Add(tp);
                    mainTabControl.SelectedTab = tp;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainTabControl.TabPages.Clear();
        }

        private void mnuTabCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTabControl.TabPages.Remove(mainTabControl.SelectedTab);
        }

        private void mnuTabCloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTabControl.TabPages.Clear();
        }

        private void mnuEditTascCalcMetricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tabName = "TascCalcMetrics";
            if (!mainTabControl.TabPages.ContainsKey(tabName))
            {
                // TAB
                TabPage tp = new TabPage("Задачи расчёта метрик");
                tp.Name = tabName;
                tp.ToolTipText = "Задачи расчёта метрик";

                // UC 4 TAB
                UCTaskCalcMetricList uc = new UCTaskCalcMetricList();
                uc.Dock = DockStyle.Fill;
                uc.Fill(null);

                // ADD UC & TAB
                tp.Controls.Add(uc);
                mainTabControl.TabPages.Add(tp);
                mainTabControl.SelectedTab = tp;
            }
        }

        private void mnuAngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tabName = "DataAnalog";
            if (!mainTabControl.TabPages.ContainsKey(tabName))
            {
                // TAB
                TabPage tp = new TabPage("Аналоги");
                tp.Name = tabName;
                tp.ToolTipText = "Данные дат-аналогов с весами";

                // UC 4 TAB
                UCTaskSelectAnalogList uc = new UCTaskSelectAnalogList();
                uc.Dock = DockStyle.Fill;
                uc.Fill(null);

                // ADD UC & TAB
                tp.Controls.Add(uc);
                mainTabControl.TabPages.Add(tp);
                mainTabControl.SelectedTab = tp;
            }
        }

        private void testCalcAngLRFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int geoObjId = 231, varTypeId = 269, unitIdTime = 106, methodId = 1607, timeNumFcs = 1, yearFcs = 2018;

            AnalogLRFStn.CalcAnalogFcs(geoObjId, new List<int>() { varTypeId }, methodId, new DateTime(yearFcs, timeNumFcs, 1));
        }
    }
}
