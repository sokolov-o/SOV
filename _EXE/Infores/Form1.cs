using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Amur.Meta;

namespace FERHRI.Infores
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

        private void Form1_Load(object sender, EventArgs e)
        {
            inforesFilterUC.Option = new UCInforesFilter.Options()
            {
                CatalogFilterOptions = new Amur.Meta.UCCatalogFilter.Options()
                {
                    ShowAGSSites = new bool[] { true, false, true },
                    ShowAGSVariables = new bool[] { true, false, true },
                    ShowAGSMethods = new bool[] { true, false, true },
                    ShowAGSSources = new bool[] { true, false, true },
                    ShowAGSOffsetTypes = new bool[] { true, false, true }
                }
            };

            DataManager dm = DataManager.GetInstance();

            inforesFilterUC.Fill(
                dm.SiteRepository.Select(),
                dm.VariableRepository.Select(),
                dm.MethodRepository.Select(),
                dm.SourceRepository.Select(),
                dm.OffsetTypeRepository.Select()
            );

        }
    }
}
