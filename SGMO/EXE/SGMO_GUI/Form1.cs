﻿using System;
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
                List<Track> tracks = DataManager.GetInstance().TrackRepository.Select();

                TreeNode nodeRoot = new TreeNode("Маршруты") { Name = "traks" };
                foreach (Track track in tracks.Where(x => !x.ParentId.HasValue))
                {
                    TreeNode nodeParentTrack = new TreeNode(track.Name) { Name = track.Id.ToString(), Tag = track };
                    nodeParentTrack.ContextMenuStrip = contextMenuStripTrackPart;

                    AddChildTracks(nodeParentTrack, tracks);

                    nodeRoot.Nodes.Add(nodeParentTrack);
                }

                tv.Nodes.Add(nodeRoot);

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

        void AddChildTracks(TreeNode parentTrack, List<Track> tracks)
        {
            foreach (Track childTrack in tracks.Where(x => x.ParentId == ((Track)parentTrack.Tag).Id))
            {
                parentTrack.Nodes.Add(new TreeNode(childTrack.DateSUTC.ToString("dd.mm.yyyy HH")) { Name = childTrack.Id.ToString(), Tag = childTrack });
            }
        }

        private void Tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tv.SelectedNode = e.Node;
            }
        }

        private void AddTrackPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewTrackPart frm = new FormNewTrackPart((Track)tv.SelectedNode.Tag);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                tv.SelectedNode.Nodes.Clear();
                AddChildTracks(tv.SelectedNode, DataManager.GetInstance().TrackRepository.SelectChilds(((Track)tv.SelectedNode.Tag).Id));
            }
        }

        private void Tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ucTrackPoints.Items = null;
            if (tv.SelectedNode != null && tv.SelectedNode.Tag != null && tv.SelectedNode.Tag.GetType() == typeof(Track))
                ucTrackPoints.Items = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(((Track)tv.SelectedNode.Tag).Id);
        }
    }
}
