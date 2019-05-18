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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.tv = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucTrackPoints = new SOV.SGMO.UCTrackPoints();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucDataTrackForecasts = new SOV.SGMO.UCDataTrackForecasts();
            this.contextMenuStripTrackRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTrackPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTrackChild = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeFcsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStripTrackRoot.SuspendLayout();
            this.contextMenuStripTrackChild.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(770, 432);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tv, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(255, 432);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(255, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(50, 22);
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.Location = new System.Drawing.Point(3, 28);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(249, 401);
            this.tv.TabIndex = 0;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tv_AfterSelect);
            this.tv.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Tv_NodeMouseClick);
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
            this.contextMenuStripTrackChild.Size = new System.Drawing.Size(181, 70);
            // 
            // deleteTrackToolStripMenuItem
            // 
            this.deleteTrackToolStripMenuItem.Name = "deleteTrackToolStripMenuItem";
            this.deleteTrackToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteTrackToolStripMenuItem.Text = "Удалить маршрут";
            this.deleteTrackToolStripMenuItem.Click += new System.EventHandler(this.deleteTrackToolStripMenuItem_Click_1);
            // 
            // makeFcsToolStripMenuItem
            // 
            this.makeFcsToolStripMenuItem.Name = "makeFcsToolStripMenuItem";
            this.makeFcsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.makeFcsToolStripMenuItem.Text = "Прогноз!";
            this.makeFcsToolStripMenuItem.Click += new System.EventHandler(this.MakeFcsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 456);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStripTrackRoot.ResumeLayout(false);
            this.contextMenuStripTrackChild.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ToolStripMenuItem mnuDicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDicWarningPileCatalogToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RefreshButton;
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
    }
}

