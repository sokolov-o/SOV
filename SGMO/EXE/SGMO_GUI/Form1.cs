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

            Text = Application.ProductName + " (" + Application.CompanyName + ", v. " + Application.ProductVersion + ")";
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
                    TreeNode nodeTrackRoot = new TreeNode(track.Name) { Name = track.Id.ToString(), Tag = track };
                    nodeTrackRoot.ContextMenuStrip = contextMenuStripTrackRoot;

                    AddChildTracks(nodeTrackRoot, tracks);

                    nodeRoot.Nodes.Add(nodeTrackRoot);
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
            foreach (Track childTrack in tracks.Where(x => x.ParentId == ((Track)parentTrack.Tag).Id).OrderByDescending(x => x.DateSUTC))
            {
                TreeNode childNode = new TreeNode(childTrack.DateSUTC.ToString("dd.MM.yyyy HH")) { Name = childTrack.Id.ToString(), Tag = childTrack };
                childNode.ContextMenuStrip = contextMenuStripTrackChild;
                parentTrack.Nodes.Add(childNode);
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
            RefreshUC();
        }
        private void RefreshUC()
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                ucTrackPoints.Items = null;
                ucDataTrackForecasts.Items = null;
                if (tv.SelectedNode != null && tv.SelectedNode.Tag != null && tv.SelectedNode.Tag.GetType() == typeof(Track))
                {
                    Track track = (Track)tv.SelectedNode.Tag;
                    ucTrackPoints.Items = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(track.Id);

                    List<TrackPoint> trackPoints = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(track.Id);
                    ucDataTrackForecasts.Items = DataManager.GetInstance().DataTrackFcsRepository.SelectExtByTrackPartPointId(
                        trackPoints.Select(x => x.Id).ToList());
                }

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

        private void deleteTrackToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (tv.SelectedNode != null && tv.SelectedNode.Tag != null && tv.SelectedNode.Tag.GetType() == typeof(Track))
            {
                Track track = (Track)tv.SelectedNode.Tag;
                if (DataManager.GetInstance().TrackRepository.SelectChilds(track.Id).Count > 0)
                {
                    MessageBox.Show("Маршрут имеет наследников. Сначала нужно удалить наследников.", "Отмена действия");
                    return;
                }

                DataManager.GetInstance().TrackRepository.Delete(track.Id);
            }
            RefreshUC();
        }

        private void MakeFcsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tv.SelectedNode != null && tv.SelectedNode.Tag != null && tv.SelectedNode.Tag.GetType() == typeof(Track))
            {
                Track track = (Track)tv.SelectedNode.Tag;
                if (!track.ParentId.HasValue)
                {
                    MessageBox.Show("Маршрут не является наследником.", "Отмена действия");
                    return;
                }

                List<int> methodIds = Properties.Settings.Default.TrackForeacstMethodsAvailable.Split(new char[] { ';' }).Select(x => int.Parse(x)).ToList();
                FormTrackForecastParameters frm = new FormTrackForecastParameters(Amur.Meta.DataManager.GetInstance().MethodRepository.Select(methodIds));
                Common.User user = Common.User.Parse(Properties.Settings.Default.User);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Cursor cs = this.Cursor;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        List<DataTrackFcs> dataTrackFcs = TrackForecast.Get(user, (int)track.ParentId, track.DateSUTC, frm.Methods.Select(x => x.Id).ToArray());

                        DataManager.GetInstance().DataTrackFcsRepository.Insert(dataTrackFcs);

                        RefreshUC();
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
    }
}
