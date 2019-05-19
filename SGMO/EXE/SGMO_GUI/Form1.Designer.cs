namespace SOV.SGMO
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDicWarningPileCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.RefreshTracksButton = new System.Windows.Forms.ToolStripButton();
            this.tvTracks = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tvSites = new System.Windows.Forms.TreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.refreshSitesButton = new System.Windows.Forms.ToolStripButton();
            this.infoSitesLabel = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucTrackPoints = new SOV.SGMO.UCTrackPoints();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucDataTrackForecasts = new SOV.SGMO.UCDataTrackForecasts();
            this.contextMenuStripTrackRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTrackPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTrackChild = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.makeFcsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.siteContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newSiteFcsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStripTrackRoot.SuspendLayout();
            this.contextMenuStripTrackChild.SuspendLayout();
            this.siteContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileToolStripMenuItem,
            this.mnuDicToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(770, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFileToolStripMenuItem
            // 
            this.mnuFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExitToolStripMenuItem});
            this.mnuFileToolStripMenuItem.Name = "mnuFileToolStripMenuItem";
            this.mnuFileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.mnuFileToolStripMenuItem.Text = "Файл";
            // 
            // mnuFileExitToolStripMenuItem
            // 
            this.mnuFileExitToolStripMenuItem.Name = "mnuFileExitToolStripMenuItem";
            this.mnuFileExitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.mnuFileExitToolStripMenuItem.Text = "Выход";
            this.mnuFileExitToolStripMenuItem.Click += new System.EventHandler(this.MnuFileExitToolStripMenuItem_Click);
            // 
            // mnuDicToolStripMenuItem
            // 
            this.mnuDicToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuDicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDicWarningPileCatalogToolStripMenuItem});
            this.mnuDicToolStripMenuItem.Name = "mnuDicToolStripMenuItem";
            this.mnuDicToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.mnuDicToolStripMenuItem.Text = "Справочники";
            this.mnuDicToolStripMenuItem.Visible = false;
            // 
            // mnuDicWarningPileCatalogToolStripMenuItem
            // 
            this.mnuDicWarningPileCatalogToolStripMenuItem.Image = global::SOV.SGMO.Properties.Resources.fire_128x128;
            this.mnuDicWarningPileCatalogToolStripMenuItem.Name = "mnuDicWarningPileCatalogToolStripMenuItem";
            this.mnuDicWarningPileCatalogToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.mnuDicWarningPileCatalogToolStripMenuItem.Text = "Предупреждения для записей каталога данных";
            this.mnuDicWarningPileCatalogToolStripMenuItem.Click += new System.EventHandler(this.MnuDicWarningPileCatalogToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(770, 432);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(255, 432);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(247, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Маршруты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tvTracks, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(241, 400);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshTracksButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(241, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // RefreshTracksButton
            // 
            this.RefreshTracksButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshTracksButton.Image = global::SOV.SGMO.Properties.Resources.Refresh;
            this.RefreshTracksButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshTracksButton.Name = "RefreshTracksButton";
            this.RefreshTracksButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshTracksButton.Text = "Обновить";
            this.RefreshTracksButton.ToolTipText = "Обновить";
            this.RefreshTracksButton.Click += new System.EventHandler(this.RefreshTracksButton_Click);
            // 
            // tvTracks
            // 
            this.tvTracks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTracks.ImageIndex = 0;
            this.tvTracks.ImageList = this.imageList1;
            this.tvTracks.Location = new System.Drawing.Point(3, 28);
            this.tvTracks.Name = "tvTracks";
            this.tvTracks.SelectedImageIndex = 0;
            this.tvTracks.Size = new System.Drawing.Size(235, 369);
            this.tvTracks.TabIndex = 0;
            this.tvTracks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tv_AfterSelect);
            this.tvTracks.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Tv_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "anchor.png");
            this.imageList1.Images.SetKeyName(1, "flag.png");
            this.imageList1.Images.SetKeyName(2, "ruler.png");
            this.imageList1.Images.SetKeyName(3, "DateTime.png");
            this.imageList1.Images.SetKeyName(4, "podcasts.png");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(247, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Пункты";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tvSites, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(241, 400);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tvSites
            // 
            this.tvSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSites.ImageIndex = 0;
            this.tvSites.ImageList = this.imageList1;
            this.tvSites.Location = new System.Drawing.Point(3, 28);
            this.tvSites.Name = "tvSites";
            this.tvSites.SelectedImageIndex = 0;
            this.tvSites.Size = new System.Drawing.Size(235, 369);
            this.tvSites.TabIndex = 1;
            this.tvSites.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvSites_AfterSelect);
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshSitesButton,
            this.infoSitesLabel});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(241, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // refreshSitesButton
            // 
            this.refreshSitesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshSitesButton.Image = global::SOV.SGMO.Properties.Resources.Refresh;
            this.refreshSitesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshSitesButton.Name = "refreshSitesButton";
            this.refreshSitesButton.Size = new System.Drawing.Size(23, 22);
            this.refreshSitesButton.Text = "toolStripButton1";
            this.refreshSitesButton.Click += new System.EventHandler(this.RefreshSitesButton_Click);
            // 
            // infoSitesLabel
            // 
            this.infoSitesLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoSitesLabel.Name = "infoSitesLabel";
            this.infoSitesLabel.Size = new System.Drawing.Size(16, 22);
            this.infoSitesLabel.Text = "...";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(511, 432);
            this.splitContainer2.SplitterDistance = 216;
            this.splitContainer2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucTrackPoints);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 216);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Точки маршрута";
            // 
            // ucTrackPoints
            // 
            this.ucTrackPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTrackPoints.Items = null;
            this.ucTrackPoints.Location = new System.Drawing.Point(3, 16);
            this.ucTrackPoints.Name = "ucTrackPoints";
            this.ucTrackPoints.Size = new System.Drawing.Size(505, 197);
            this.ucTrackPoints.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucDataTrackForecasts);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 212);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Прогноз";
            // 
            // ucDataTrackForecasts
            // 
            this.ucDataTrackForecasts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataTrackForecasts.Location = new System.Drawing.Point(3, 16);
            this.ucDataTrackForecasts.Name = "ucDataTrackForecasts";
            this.ucDataTrackForecasts.Size = new System.Drawing.Size(505, 193);
            this.ucDataTrackForecasts.TabIndex = 0;
            // 
            // contextMenuStripTrackRoot
            // 
            this.contextMenuStripTrackRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTrackPartToolStripMenuItem});
            this.contextMenuStripTrackRoot.Name = "contextMenuStripTrackPart";
            this.contextMenuStripTrackRoot.Size = new System.Drawing.Size(211, 26);
            // 
            // addTrackPartToolStripMenuItem
            // 
            this.addTrackPartToolStripMenuItem.Name = "addTrackPartToolStripMenuItem";
            this.addTrackPartToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.addTrackPartToolStripMenuItem.Text = "Создать часть маршрута";
            this.addTrackPartToolStripMenuItem.Click += new System.EventHandler(this.AddTrackPartToolStripMenuItem_Click);
            // 
            // contextMenuStripTrackChild
            // 
            this.contextMenuStripTrackChild.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeFcsToolStripMenuItem,
            this.deleteTrackToolStripMenuItem});
            this.contextMenuStripTrackChild.Name = "contextMenuStripTrackPart";
            this.contextMenuStripTrackChild.Size = new System.Drawing.Size(173, 48);
            // 
            // makeFcsToolStripMenuItem
            // 
            this.makeFcsToolStripMenuItem.Name = "makeFcsToolStripMenuItem";
            this.makeFcsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.makeFcsToolStripMenuItem.Text = "Прогноз!";
            this.makeFcsToolStripMenuItem.Click += new System.EventHandler(this.NewTrackFcsToolStripMenuItem_Click);
            // 
            // deleteTrackToolStripMenuItem
            // 
            this.deleteTrackToolStripMenuItem.Name = "deleteTrackToolStripMenuItem";
            this.deleteTrackToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.deleteTrackToolStripMenuItem.Text = "Удалить маршрут";
            this.deleteTrackToolStripMenuItem.Click += new System.EventHandler(this.deleteTrackToolStripMenuItem_Click_1);
            // 
            // siteContextMenuStrip
            // 
            this.siteContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSiteFcsToolStripMenuItem});
            this.siteContextMenuStrip.Name = "contextMenuStripTrackPart";
            this.siteContextMenuStrip.Size = new System.Drawing.Size(125, 26);
            // 
            // newSiteFcsToolStripMenuItem
            // 
            this.newSiteFcsToolStripMenuItem.Name = "newSiteFcsToolStripMenuItem";
            this.newSiteFcsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newSiteFcsToolStripMenuItem.Text = "Прогноз!";
            this.newSiteFcsToolStripMenuItem.Click += new System.EventHandler(this.NewSiteFcsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 456);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStripTrackRoot.ResumeLayout(false);
            this.contextMenuStripTrackChild.ResumeLayout(false);
            this.siteContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvTracks;
        private System.Windows.Forms.ToolStripMenuItem mnuDicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDicWarningPileCatalogToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RefreshTracksButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTrackRoot;
        private System.Windows.Forms.ToolStripMenuItem addTrackPartToolStripMenuItem;
        private UCTrackPoints ucTrackPoints;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UCDataTrackForecasts ucDataTrackForecasts;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTrackChild;
        private System.Windows.Forms.ToolStripMenuItem deleteTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeFcsToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton refreshSitesButton;
        private System.Windows.Forms.ToolStripLabel infoSitesLabel;
        private System.Windows.Forms.TreeView tvSites;
        private System.Windows.Forms.ContextMenuStrip siteContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newSiteFcsToolStripMenuItem;
    }
}

