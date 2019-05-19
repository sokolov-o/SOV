using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;

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

        private void RefreshTracksButton_Click(object sender, EventArgs e)
        {
            tvTracks.Nodes.Clear();

            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                List<Track> tracks = DataManager.GetInstance().TrackRepository.Select();

                foreach (Track track in tracks.Where(x => !x.ParentId.HasValue))
                {
                    TreeNode nodeTrackRoot = new TreeNode(track.Name) { Name = track.Id.ToString(), Tag = track, ImageIndex = 0 };
                    nodeTrackRoot.ContextMenuStrip = contextMenuStripTrackRoot;

                    AddChildTracks(nodeTrackRoot, tracks);

                    tvTracks.Nodes.Add(nodeTrackRoot);
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

        void AddChildTracks(TreeNode parentTrack, List<Track> tracks)
        {
            foreach (Track childTrack in tracks.Where(x => x.ParentId == ((Track)parentTrack.Tag).Id).OrderByDescending(x => x.DateSUTC))
            {
                TreeNode childNode = new TreeNode(childTrack.DateSUTC.ToString("dd.MM.yyyy HH"))
                {
                    Name = childTrack.Id.ToString(),
                    Tag = childTrack,
                    ImageIndex = 3,
                    SelectedImageIndex = 4
                };
                childNode.ContextMenuStrip = contextMenuStripTrackChild;
                parentTrack.Nodes.Add(childNode);
            }
        }

        private void Tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvTracks.SelectedNode = e.Node;
            }
        }

        private void AddTrackPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewTrackPart frm = new FormNewTrackPart((Track)tvTracks.SelectedNode.Tag);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                tvTracks.SelectedNode.Nodes.Clear();
                AddChildTracks(tvTracks.SelectedNode, DataManager.GetInstance().TrackRepository.SelectChilds(((Track)tvTracks.SelectedNode.Tag).Id));
            }
        }

        private void Tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshTrackUC();
        }
        private void RefreshTrackUC()
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                ucTrackPoints.Items = null;
                ucDataTrackForecasts.Items = null;
                if (tvTracks.SelectedNode != null && tvTracks.SelectedNode.Tag != null && tvTracks.SelectedNode.Tag.GetType() == typeof(Track))
                {
                    Track track = (Track)tvTracks.SelectedNode.Tag;
                    ucTrackPoints.Items = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(track.Id);

                    List<TrackPoint> trackPoints = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(track.Id);
                    List<DataTrackFcsExt> data = DataManager.GetInstance().DataTrackFcsRepository.SelectExtByTrackPartPointId(trackPoints.Select(x => x.Id).ToList());
                    ucDataTrackForecasts.Items = data;
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
            if (tvTracks.SelectedNode != null && tvTracks.SelectedNode.Tag != null && tvTracks.SelectedNode.Tag.GetType() == typeof(Track))
            {
                Track track = (Track)tvTracks.SelectedNode.Tag;
                if (DataManager.GetInstance().TrackRepository.SelectChilds(track.Id).Count > 0)
                {
                    MessageBox.Show("Маршрут имеет наследников. Сначала нужно удалить наследников.", "Отмена действия");
                    return;
                }

                DataManager.GetInstance().TrackRepository.Delete(track.Id);
            }
            RefreshTrackUC();
        }

        private void NewTrackFcsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvTracks.SelectedNode != null && tvTracks.SelectedNode.Tag != null && tvTracks.SelectedNode.Tag.GetType() == typeof(Track))
            {
                Track track = (Track)tvTracks.SelectedNode.Tag;
                if (!track.ParentId.HasValue)
                {
                    MessageBox.Show("Маршрут не является наследником.", "Отмена действия");
                    return;
                }

                List<int> methodIds = Properties.Settings.Default.ForeacstMethodsAvailable.Split(new char[] { ';' }).Select(x => int.Parse(x)).ToList();
                FormForecastParameters frm = new FormForecastParameters(track.DateSUTC, Amur.Meta.DataManager.GetInstance().MethodRepository.Select(methodIds));
                Common.User user = Common.User.Parse(Properties.Settings.Default.User);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Cursor cs = this.Cursor;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        List<DataTrackFcs> dataTrackFcs = TrackForecast.Get(user, (int)track.ParentId, track.DateSUTC, frm.Methods.Select(x => x.Id).ToArray());

                        DataManager.GetInstance().DataTrackFcsRepository.Insert(dataTrackFcs);

                        RefreshTrackUC();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshTracksButton_Click(sender, e);
            RefreshSitesButton_Click(sender, e);
        }

        private void RefreshSitesButton_Click(object sender, EventArgs e)
        {
            tvSites.Nodes.Clear();

            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                List<Site> sites = DataManager.GetInstance().SiteRepository.SelectAmurSites();

                foreach (Site site in sites.OrderBy(x => x.Name))
                {
                    TreeNode siteNode = new TreeNode(site.Name) { Name = site.Id.ToString(), Tag = site, ImageIndex = 1, SelectedImageIndex = 1 };
                    siteNode.ContextMenuStrip = siteContextMenuStrip;

                    foreach (KeyValuePair<int, DateTime> item in DataManager.GetInstance().DataSiteFcsRepository.SelectDateIniUTC4Sites(new List<int> { site.Id }))
                    {
                        TreeNode dateNode = new TreeNode(item.Value.ToString("dd.MM.yyyy HH")) { Name = site.Id.ToString(), Tag = item, ImageIndex = 3, SelectedImageIndex = 3 };
                        siteNode.Nodes.Add(dateNode);
                    }

                    tvSites.Nodes.Add(siteNode);
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

        private void AddSiteChildNodes(TreeNode siteNode)
        {
            Site site = (Site)siteNode.Tag;
            foreach (KeyValuePair<int, DateTime> item in DataManager.GetInstance().DataSiteFcsRepository.SelectDateIniUTC4Sites(new List<int> { site.Id }))
            {
                TreeNode node = new TreeNode(item.Value.ToString("dd.MM.yyyy HH")) { Name = site.Id.ToString(), Tag = item, ImageIndex = 3, SelectedImageIndex = 3 };
                siteNode.Nodes.Add(node);
            }
        }

        private void NewSiteFcsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvSites.SelectedNode != null && tvSites.SelectedNode.Tag != null && tvSites.SelectedNode.Tag.GetType() == typeof(Site))
            {
                Site site = (Site)tvSites.SelectedNode.Tag;

                List<int> methodIds = Properties.Settings.Default.ForeacstMethodsAvailable.Split(new char[] { ';' }).Select(x => int.Parse(x)).ToList();
                FormForecastParameters frm = new FormForecastParameters(null, Amur.Meta.DataManager.GetInstance().MethodRepository.Select(methodIds));
                Common.User user = Common.User.Parse(Properties.Settings.Default.User);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Cursor cs = this.Cursor;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        List<DataFcs> dataFcs = SiteForecast.Get(user, new List<int> { site.Id }, frm.DateIniUTC, frm.Methods.Select(x => x.Id).ToList());

                        DataManager.GetInstance().DataSiteFcsRepository.Insert(dataFcs);

                        RefreshSiteUC();
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

        private void TvSites_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshSiteUC();
        }
        private void RefreshSiteUC()
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                //ucTrackPoints.Visible = false;
                //ucDataTrackForecasts.Items = null;
                //if (tvTracks.SelectedNode != null && tvTracks.SelectedNode.Tag != null && tvTracks.SelectedNode.Tag.GetType() == typeof(Track))
                //{
                //    Track track = (Track)tvTracks.SelectedNode.Tag;
                //    ucTrackPoints.Items = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(track.Id);

                //    List<TrackPoint> trackPoints = DataManager.GetInstance().TrackPointsRepository.SelectByTrackId(track.Id);
                //    ucDataTrackForecasts.Items = DataManager.GetInstance().DataTrackFcsRepository.SelectExtByTrackPartPointId(
                //        trackPoints.Select(x => x.Id).ToList());
                //}

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
