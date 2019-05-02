namespace FERHRI.Analog.GUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditTascCalcMetricToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsc = new System.Windows.Forms.ToolStripContainer();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuTabCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTabCloseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.testCalcAngLRFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.TopToolStripPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.tabContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileToolStripMenuItem,
            this.mnuEditToolStripMenuItem,
            this.mnuAngToolStripMenuItem,
            this.testCalcAngLRFToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(913, 24);
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
            this.mnuFileExitToolStripMenuItem.Image = global::FERHRI.Analog.GUI.Properties.Resources.Close_6519;
            this.mnuFileExitToolStripMenuItem.Name = "mnuFileExitToolStripMenuItem";
            this.mnuFileExitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.mnuFileExitToolStripMenuItem.Text = "Выйти";
            this.mnuFileExitToolStripMenuItem.Click += new System.EventHandler(this.mnuFileExitToolStripMenuItem_Click);
            // 
            // mnuEditToolStripMenuItem
            // 
            this.mnuEditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditFieldToolStripMenuItem,
            this.mnuEditTascCalcMetricToolStripMenuItem});
            this.mnuEditToolStripMenuItem.Name = "mnuEditToolStripMenuItem";
            this.mnuEditToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.mnuEditToolStripMenuItem.Text = "Задачи и атрибуты";
            // 
            // mnuEditFieldToolStripMenuItem
            // 
            this.mnuEditFieldToolStripMenuItem.Image = global::FERHRI.Analog.GUI.Properties.Resources.GridDark;
            this.mnuEditFieldToolStripMenuItem.Name = "mnuEditFieldToolStripMenuItem";
            this.mnuEditFieldToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.mnuEditFieldToolStripMenuItem.Text = "Поля...";
            this.mnuEditFieldToolStripMenuItem.Click += new System.EventHandler(this.mnuEditFieldToolStripMenuItem_Click);
            // 
            // mnuEditTascCalcMetricToolStripMenuItem
            // 
            this.mnuEditTascCalcMetricToolStripMenuItem.Name = "mnuEditTascCalcMetricToolStripMenuItem";
            this.mnuEditTascCalcMetricToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.mnuEditTascCalcMetricToolStripMenuItem.Text = "Задачи расчёта метрик";
            this.mnuEditTascCalcMetricToolStripMenuItem.Click += new System.EventHandler(this.mnuEditTascCalcMetricToolStripMenuItem_Click);
            // 
            // mnuAngToolStripMenuItem
            // 
            this.mnuAngToolStripMenuItem.Name = "mnuAngToolStripMenuItem";
            this.mnuAngToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.mnuAngToolStripMenuItem.Text = "Аналоги";
            this.mnuAngToolStripMenuItem.Click += new System.EventHandler(this.mnuAngToolStripMenuItem_Click);
            // 
            // tsc
            // 
            this.tsc.BottomToolStripPanelVisible = false;
            // 
            // tsc.ContentPanel
            // 
            this.tsc.ContentPanel.Controls.Add(this.mainTabControl);
            this.tsc.ContentPanel.Size = new System.Drawing.Size(913, 551);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.LeftToolStripPanelVisible = false;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.RightToolStripPanelVisible = false;
            this.tsc.Size = new System.Drawing.Size(913, 575);
            this.tsc.TabIndex = 2;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tsc.TopToolStripPanel
            // 
            this.tsc.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // mainTabControl
            // 
            this.mainTabControl.ContextMenuStrip = this.tabContextMenuStrip;
            this.mainTabControl.Controls.Add(this.tabPage3);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Multiline = true;
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(913, 551);
            this.mainTabControl.TabIndex = 0;
            // 
            // tabContextMenuStrip
            // 
            this.tabContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTabCloseToolStripMenuItem,
            this.mnuTabCloseAllToolStripMenuItem});
            this.tabContextMenuStrip.Name = "tabContextMenuStrip";
            this.tabContextMenuStrip.Size = new System.Drawing.Size(189, 48);
            // 
            // mnuTabCloseToolStripMenuItem
            // 
            this.mnuTabCloseToolStripMenuItem.Name = "mnuTabCloseToolStripMenuItem";
            this.mnuTabCloseToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.mnuTabCloseToolStripMenuItem.Text = "Закрыть вкладку";
            this.mnuTabCloseToolStripMenuItem.Click += new System.EventHandler(this.mnuTabCloseToolStripMenuItem_Click);
            // 
            // mnuTabCloseAllToolStripMenuItem
            // 
            this.mnuTabCloseAllToolStripMenuItem.Name = "mnuTabCloseAllToolStripMenuItem";
            this.mnuTabCloseAllToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.mnuTabCloseAllToolStripMenuItem.Text = "Закрыть все вкладки";
            this.mnuTabCloseAllToolStripMenuItem.Click += new System.EventHandler(this.mnuTabCloseAllToolStripMenuItem_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(905, 525);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Empty";
            this.tabPage3.ToolTipText = "Журнал г/м полей, для которых рассчитываются метрики аналогичности";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // testCalcAngLRFToolStripMenuItem
            // 
            this.testCalcAngLRFToolStripMenuItem.Name = "testCalcAngLRFToolStripMenuItem";
            this.testCalcAngLRFToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.testCalcAngLRFToolStripMenuItem.Text = "TestCalcAngLRF";
            this.testCalcAngLRFToolStripMenuItem.Click += new System.EventHandler(this.testCalcAngLRFToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 575);
            this.Controls.Add(this.tsc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "АРМ \"Аналог\", ДВНИГМИ, 2018";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.PerformLayout();
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.tabContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEditFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ContextMenuStrip tabContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuTabCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTabCloseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEditTascCalcMetricToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testCalcAngLRFToolStripMenuItem;
    }
}

