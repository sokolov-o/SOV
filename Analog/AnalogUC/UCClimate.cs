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
    public partial class UCClimate : UserControl
    {
        public UCClimate()
        {
            InitializeComponent();
        }

        public Climate Value
        {
            set
            {
                if (value == null)
                {
                    ////yearSFInterval.SetInterval(0, 0);
                    catalogIdAvgTextBox.Text = catalogIdStdTextBox.Text = null;
                }
                else
                {
                    Value = null;
                    yearSFInterval.SetInterval(value.YearS, value.YearF);
                    if (value.MathvarXCatalog != null)
                    {
                        catalogIdAvgTextBox.Text = value.MathvarXCatalog[(int)Amur.Meta.EnumMathvar.Avg].ToString();
                        catalogIdStdTextBox.Text = value.MathvarXCatalog[(int)Amur.Meta.EnumMathvar.Std].ToString();
                    }
                }
            }
            get
            {
                decimal[] interval = yearSFInterval.GetInterval();
                Climate ret = new Climate();

                ret.YearS = (int)interval[0];
                ret.YearF = (int)interval[1];
                ret.MathvarXCatalog = string.IsNullOrEmpty(catalogIdAvgTextBox.Text) || string.IsNullOrEmpty(catalogIdStdTextBox.Text)
                    ? null
                    : new Dictionary<int, int>()
                        {
                            { (int)Amur.Meta.EnumMathvar.Avg , int.Parse(catalogIdAvgTextBox.Text)},
                            { (int)Amur.Meta.EnumMathvar.Avg , int.Parse(catalogIdStdTextBox.Text)}
                        };
                return ret;
            }
        }

        private void UCClimate_Load(object sender, EventArgs e)
        {
            yearSFInterval.SetMinMax(new int[] { 1900, DateTime.Today.Year }, new int[] { 1900, DateTime.Today.Year });
            yearSFInterval.SetInterval(1981, 2010);
        }
    }
}
