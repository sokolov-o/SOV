namespace SGMO_GUI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDicWarningPileCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(14, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1797, 51);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFileToolStripMenuItem
            // 
            this.mnuFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExitToolStripMenuItem});
            this.mnuFileToolStripMenuItem.Name = "mnuFileToolStripMenuItem";
            this.mnuFileToolStripMenuItem.Size = new System.Drawing.Size(93, 43);
            this.mnuFileToolStripMenuItem.Text = "Файл";
            // 
            // mnuFileExitToolStripMenuItem
            // 
            this.mnuFileExitToolStripMenuItem.Name = "mnuFileExitToolStripMenuItem";
            this.mnuFileExitToolStripMenuItem.Size = new System.Drawing.Size(201, 42);
            this.mnuFileExitToolStripMenuItem.Text = "Выход";
            this.mnuFileExitToolStripMenuItem.Click += new System.EventHandler(this.mnuFileExitToolStripMenuItem_Click);
            // 
            // mnuDicToolStripMenuItem
            // 
            this.mnuDicToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuDicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDicWarningPileCatalogToolStripMenuItem});
            this.mnuDicToolStripMenuItem.Name = "mnuDicToolStripMenuItem";
            this.mnuDicToolStripMenuItem.Size = new System.Drawing.Size(198, 43);
            this.mnuDicToolStripMenuItem.Text = "Справочники";
            // 
            // mnuDicWarningPileCatalogToolStripMenuItem
            // 
            this.mnuDicWarningPileCatalogToolStripMenuItem.Image = global::SGMO_GUI.Properties.Resources.fire_128x128;
            this.mnuDicWarningPileCatalogToolStripMenuItem.Name = "mnuDicWarningPileCatalogToolStripMenuItem";
            this.mnuDicWarningPileCatalogToolStripMenuItem.Size = new System.Drawing.Size(711, 42);
            this.mnuDicWarningPileCatalogToolStripMenuItem.Text = "Предупреждения для записей каталога данных";
            this.mnuDicWarningPileCatalogToolStripMenuItem.Click += new System.EventHandler(this.mnuDicWarningPileCatalogToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv);
            this.splitContainer1.Size = new System.Drawing.Size(1797, 966);
            this.splitContainer1.SplitterDistance = 597;
            this.splitContainer1.SplitterWidth = 9;
            this.splitContainer1.TabIndex = 1;
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(597, 966);
            this.tv.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1797, 1017);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
    }
}

