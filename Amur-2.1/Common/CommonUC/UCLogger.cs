using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class UCLogger : UserControl
    {
        public UCLogger()
        {
            InitializeComponent();
            MaxLines = 1000;
        }
        public int MaxLines { get; set; }
        public void AppendLine(string msg)
        {
            if (rtb.Lines.Length - 1 == MaxLines)
            {
                rtb.Lines = rtb.Lines.Skip(1).ToArray();
            }
            rtb.AppendText(msg);
            AppendLine();
        }
        public void AppendLineUrgent(string msg)
        {
            AppendLine("*" + msg);
        }
        public void AppendLineErr(string msg)
        {
            AppendLine("***" + msg);
        }
        public void AppendLine(int n = 1)
        {
            string nl = "";
            for (int i = 0; i < n; i++)
            {
                nl += '\n';
            }
            AppendLine(nl);
        }
    }
}
