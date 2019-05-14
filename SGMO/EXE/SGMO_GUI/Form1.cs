using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.SGMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MnuFileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MnuDicWarningPileCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Не реализовано. OSokolov@2017.08");
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            tv.Nodes.Clear();

            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                TreeNode node = new TreeNode("Маршруты") { Name = "traks" };
                foreach (Track track in DataManager.GetInstance().TrackRepository.Select())
                {
                    TreeNode nodeTrack = new TreeNode(track.Name) { Name = track.Id.ToString(), Tag = track };

                    foreach (TrackPart trackPart in DataManager.GetInstance().TrackPartRepository.SelectByTrack(track.Id))
                    {
                        TreeNode nodeTrackPart = new TreeNode(trackPart.DateSUTC.ToString("dd.mm.yyyy HH")) { Name = trackPart.Id.ToString(), Tag = trackPart };
                        nodeTrack.Nodes.Add(nodeTrackPart);
                    }

                    node.Nodes.Add(nodeTrack);
                }

                tv.Nodes.Add(node);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = cs;
            }
        }
    }
}
