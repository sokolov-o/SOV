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
    public partial class UCIntDouble : UserControl
    {
        public UCIntDouble()
        {
            InitializeComponent();
        }
        public UCIntDouble(string intColumnName, string doubleColumnName, Dictionary<int, double> values)
        {
            InitializeComponent();

            SetColumnNames(intColumnName, doubleColumnName);
            dgv.Columns[0].HeaderText = intColumnName;
            dgv.Columns[1].HeaderText = doubleColumnName;

            Fill(values);
        }

        public void SetColumnNames(string intColumnName, string doubleColumnName)
        {
            dgv.Columns[0].HeaderText = intColumnName;
            dgv.Columns[1].HeaderText = doubleColumnName;
        }

        public void Fill(Dictionary<int, double> values)
        {
            dgv.Rows.Clear();
            if (values != null)
            {
                foreach (var item in values)
                {
                    dgv.Rows.Add(new object[] { item.Key, item.Value });
                }
            }
        }
        public void Fill(IntDouble[] values)
        {
            dgv.Rows.Clear();
            if (values != null)
            {
                foreach (var item in values)
                {
                    dgv.Rows.Add(new object[] { item.Int, item.Double });
                }
            }
        }
        public Dictionary<int, double> GetIntDoubleDictionary()
        {
            Dictionary<int, double> ret = new Dictionary<int, double>();
            foreach (DataGridViewRow item in dgv.Rows)
            {
                if (item.Cells[0].Value != null)
                    ret.Add(int.Parse(item.Cells[0].Value.ToString()),Common.StrVia.ParseDouble(item.Cells[1].Value.ToString()));
            }
            return ret;
        }
        public List<IntDouble> GetIntDoubleList()
        {
            List<IntDouble> ret = new List<IntDouble>();
            foreach (KeyValuePair<int, double> item in GetIntDoubleDictionary())
            {
                ret.Add(new IntDouble() { Int = item.Key, Double = item.Value });
            }
            return ret;
        }
    }
}
