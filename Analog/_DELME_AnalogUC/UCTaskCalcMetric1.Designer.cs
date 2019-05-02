namespace FERHRI.Analog
{
    partial class UCTaskCalcMetric1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FERHRI.Analog.Climate climate1 = new FERHRI.Analog.Climate();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.actionComboBox = new System.Windows.Forms.ComboBox();
            this.actionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.yearSTextBox = new System.Windows.Forms.TextBox();
            this.dbInterfaceComboBox = new System.Windows.Forms.ComboBox();
            this.dbInterfaceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.catalogIdTextBox = new System.Windows.Forms.TextBox();
            this.ucIntDouble = new FERHRI.Analog.UCIntDouble();
            this.ucClimate = new FERHRI.Analog.UCClimate();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.needClimateCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timeWindowInterval = new FERHRI.Common.UCInterval();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fieldShowButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.actionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbInterfaceBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(575, 3);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(67, 20);
            this.idTextBox.TabIndex = 0;
            // 
            // nameTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.nameTextBox, 3);
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTextBox.Location = new System.Drawing.Point(69, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(500, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // actionComboBox
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.actionComboBox, 2);
            this.actionComboBox.DataSource = this.actionBindingSource;
            this.actionComboBox.DisplayMember = "Name";
            this.actionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionComboBox.FormattingEnabled = true;
            this.actionComboBox.Location = new System.Drawing.Point(3, 3);
            this.actionComboBox.Name = "actionComboBox";
            this.actionComboBox.Size = new System.Drawing.Size(415, 21);
            this.actionComboBox.TabIndex = 2;
            this.actionComboBox.ValueMember = "Id";
            // 
            // actionBindingSource
            // 
            this.actionBindingSource.DataSource = typeof(FERHRI.Analog.Action);
            // 
            // yearSTextBox
            // 
            this.yearSTextBox.Location = new System.Drawing.Point(122, 4);
            this.yearSTextBox.Name = "yearSTextBox";
            this.yearSTextBox.Size = new System.Drawing.Size(53, 20);
            this.yearSTextBox.TabIndex = 5;
            // 
            // dbInterfaceComboBox
            // 
            this.dbInterfaceComboBox.DataSource = this.dbInterfaceBindingSource;
            this.dbInterfaceComboBox.DisplayMember = "Name";
            this.dbInterfaceComboBox.FormattingEnabled = true;
            this.dbInterfaceComboBox.Location = new System.Drawing.Point(68, 3);
            this.dbInterfaceComboBox.Name = "dbInterfaceComboBox";
            this.dbInterfaceComboBox.Size = new System.Drawing.Size(214, 21);
            this.dbInterfaceComboBox.TabIndex = 6;
            this.dbInterfaceComboBox.ValueMember = "Id";
            // 
            // dbInterfaceBindingSource
            // 
            this.dbInterfaceBindingSource.AllowNew = false;
            this.dbInterfaceBindingSource.DataSource = typeof(FERHRI.Common.IdName);
            // 
            // catalogIdTextBox
            // 
            this.catalogIdTextBox.Location = new System.Drawing.Point(428, 3);
            this.catalogIdTextBox.Name = "catalogIdTextBox";
            this.catalogIdTextBox.Size = new System.Drawing.Size(67, 20);
            this.catalogIdTextBox.TabIndex = 7;
            // 
            // ucIntDouble
            // 
            this.ucIntDouble.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucIntDouble.Location = new System.Drawing.Point(3, 16);
            this.ucIntDouble.Name = "ucIntDouble";
            this.ucIntDouble.Size = new System.Drawing.Size(200, 131);
            this.ucIntDouble.TabIndex = 8;
            // 
            // ucClimate
            // 
            this.ucClimate.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucClimate.Enabled = false;
            this.ucClimate.Location = new System.Drawing.Point(3, 30);
            this.ucClimate.Name = "ucClimate";
            this.ucClimate.Size = new System.Drawing.Size(226, 98);
            this.ucClimate.TabIndex = 9;
            climate1.MathvarXCatalog = null;
            climate1.YearF = 2010;
            climate1.YearS = 1981;
            this.ucClimate.Value = climate1;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 3);
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(215, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 150);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Действие (и климат для него)";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.actionComboBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ucClimate, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.needClimateCheckBox, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(421, 131);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // needClimateCheckBox
            // 
            this.needClimateCheckBox.AutoSize = true;
            this.needClimateCheckBox.Location = new System.Drawing.Point(235, 30);
            this.needClimateCheckBox.Name = "needClimateCheckBox";
            this.needClimateCheckBox.Size = new System.Drawing.Size(86, 17);
            this.needClimateCheckBox.TabIndex = 10;
            this.needClimateCheckBox.Text = "с климатом";
            this.needClimateCheckBox.UseVisualStyleBackColor = true;
            this.needClimateCheckBox.CheckedChanged += new System.EventHandler(this.needClimateCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "Название:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.idTextBox, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(645, 249);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dbInterfaceComboBox);
            this.panel2.Controls.Add(this.catalogIdTextBox);
            this.panel2.Location = new System.Drawing.Point(3, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(499, 27);
            this.panel2.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Id записи каталога поля:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "БД полей:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.ucIntDouble);
            this.groupBox2.Location = new System.Drawing.Point(3, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(206, 150);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сдвиги полей по времени и их веса";
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.yearSTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.timeWindowInterval);
            this.panel1.Location = new System.Drawing.Point(3, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 28);
            this.panel1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Год начала выборки:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Временное окно:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeWindowInterval
            // 
            this.timeWindowInterval.Location = new System.Drawing.Point(281, 3);
            this.timeWindowInterval.Name = "timeWindowInterval";
            this.timeWindowInterval.Size = new System.Drawing.Size(113, 20);
            this.timeWindowInterval.TabIndex = 14;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fieldShowButton,
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.saveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(645, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fieldShowButton
            // 
            this.fieldShowButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.fieldShowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fieldShowButton.Image = global::FERHRI.Analog.Properties.Resources.ViewBox_10697_24;
            this.fieldShowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fieldShowButton.Name = "fieldShowButton";
            this.fieldShowButton.Size = new System.Drawing.Size(23, 22);
            this.fieldShowButton.Text = "toolStripButton1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Поле";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(645, 25);
            this.panel3.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(645, 249);
            this.panel4.TabIndex = 14;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::FERHRI.Analog.Properties.Resources.save_16xLG;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "toolStripButton1";
            this.saveButton.ToolTipText = "Сохранить поле";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // UCField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Name = "UCField";
            this.Size = new System.Drawing.Size(645, 274);
            ((System.ComponentModel.ISupportInitialize)(this.actionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbInterfaceBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox actionComboBox;
        private System.Windows.Forms.BindingSource actionBindingSource;
        private System.Windows.Forms.TextBox yearSTextBox;
        private System.Windows.Forms.ComboBox dbInterfaceComboBox;
        private System.Windows.Forms.BindingSource dbInterfaceBindingSource;
        private System.Windows.Forms.TextBox catalogIdTextBox;
        private UCIntDouble ucIntDouble;
        private UCClimate ucClimate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private Common.UCInterval timeWindowInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox needClimateCheckBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton fieldShowButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton saveButton;
    }
}
