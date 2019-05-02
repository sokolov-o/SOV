namespace FERHRI.Analog
{
    partial class UCTaskCalcMetric
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.actionComboBox = new System.Windows.Forms.ComboBox();
            this.actionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ucTimeShiftWeights = new FERHRI.Analog.UCIntDouble();
            this.ucClimate = new FERHRI.Analog.UCClimate();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.needClimateCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.fieldAngComboBox = new System.Windows.Forms.ComboBox();
            this.fieldAngBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.timeWindowInterval = new FERHRI.Common.UCInterval();
            this.label2 = new System.Windows.Forms.Label();
            this.fieldIniComboBox = new System.Windows.Forms.ComboBox();
            this.fieldIniBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.isActualCheckBox = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.infoLabel = new System.Windows.Forms.ToolStripLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.actionBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldAngBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldIniBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.nameTextBox, 3);
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTextBox.Location = new System.Drawing.Point(203, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(435, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // actionComboBox
            // 
            this.actionComboBox.DataSource = this.actionBindingSource;
            this.actionComboBox.DisplayMember = "Name";
            this.actionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionComboBox.FormattingEnabled = true;
            this.actionComboBox.Location = new System.Drawing.Point(3, 3);
            this.actionComboBox.Name = "actionComboBox";
            this.actionComboBox.Size = new System.Drawing.Size(157, 21);
            this.actionComboBox.TabIndex = 2;
            this.actionComboBox.ValueMember = "Id";
            // 
            // actionBindingSource
            // 
            this.actionBindingSource.DataSource = typeof(FERHRI.Analog.Action);
            // 
            // ucTimeShiftWeights
            // 
            this.ucTimeShiftWeights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTimeShiftWeights.Location = new System.Drawing.Point(3, 16);
            this.ucTimeShiftWeights.Name = "ucTimeShiftWeights";
            this.ucTimeShiftWeights.Size = new System.Drawing.Size(362, 188);
            this.ucTimeShiftWeights.TabIndex = 8;
            // 
            // ucClimate
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.ucClimate, 2);
            this.ucClimate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucClimate.Enabled = false;
            this.ucClimate.Location = new System.Drawing.Point(3, 30);
            this.ucClimate.Name = "ucClimate";
            this.ucClimate.Size = new System.Drawing.Size(249, 103);
            this.ucClimate.TabIndex = 9;
            climate1.MathvarXCatalog = null;
            climate1.YearF = 2010;
            climate1.YearS = 1981;
            this.ucClimate.Value = climate1;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(377, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 155);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Действие (и климат для него)";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.actionComboBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ucClimate, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.needClimateCheckBox, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(255, 136);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // needClimateCheckBox
            // 
            this.needClimateCheckBox.AutoSize = true;
            this.needClimateCheckBox.Location = new System.Drawing.Point(166, 3);
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
            this.label1.Size = new System.Drawing.Size(98, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "Название задачи:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.fieldAngComboBox, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.fieldIniComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.isActualCheckBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(641, 300);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // fieldAngComboBox
            // 
            this.fieldAngComboBox.DataSource = this.fieldAngBindingSource;
            this.fieldAngComboBox.DisplayMember = "Name";
            this.fieldAngComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldAngComboBox.FormattingEnabled = true;
            this.fieldAngComboBox.Location = new System.Drawing.Point(469, 29);
            this.fieldAngComboBox.Name = "fieldAngComboBox";
            this.fieldAngComboBox.Size = new System.Drawing.Size(169, 21);
            this.fieldAngComboBox.TabIndex = 21;
            this.fieldAngComboBox.ValueMember = "Id";
            // 
            // fieldAngBindingSource
            // 
            this.fieldAngBindingSource.AllowNew = false;
            this.fieldAngBindingSource.DataSource = typeof(FERHRI.Analog.Field);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(377, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 27);
            this.label4.TabIndex = 20;
            this.label4.Text = "Поля аналогов:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 3);
            this.groupBox2.Controls.Add(this.ucTimeShiftWeights);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 207);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сдвиги полей по времени и их веса";
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.timeWindowInterval);
            this.panel1.Location = new System.Drawing.Point(3, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 28);
            this.panel1.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Временное окно для полей аналогов:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeWindowInterval
            // 
            this.timeWindowInterval.Location = new System.Drawing.Point(207, 3);
            this.timeWindowInterval.Name = "timeWindowInterval";
            this.timeWindowInterval.Size = new System.Drawing.Size(113, 20);
            this.timeWindowInterval.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 27);
            this.label2.TabIndex = 18;
            this.label2.Text = "Поля исходные:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldIniComboBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.fieldIniComboBox, 2);
            this.fieldIniComboBox.DataSource = this.fieldIniBindingSource;
            this.fieldIniComboBox.DisplayMember = "Name";
            this.fieldIniComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldIniComboBox.FormattingEnabled = true;
            this.fieldIniComboBox.Location = new System.Drawing.Point(107, 29);
            this.fieldIniComboBox.Name = "fieldIniComboBox";
            this.fieldIniComboBox.Size = new System.Drawing.Size(264, 21);
            this.fieldIniComboBox.TabIndex = 19;
            this.fieldIniComboBox.ValueMember = "Id";
            // 
            // fieldIniBindingSource
            // 
            this.fieldIniBindingSource.AllowNew = false;
            this.fieldIniBindingSource.DataSource = typeof(FERHRI.Analog.Field);
            // 
            // isActualCheckBox
            // 
            this.isActualCheckBox.AutoSize = true;
            this.isActualCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isActualCheckBox.Location = new System.Drawing.Point(107, 3);
            this.isActualCheckBox.Name = "isActualCheckBox";
            this.isActualCheckBox.Size = new System.Drawing.Size(90, 20);
            this.isActualCheckBox.TabIndex = 22;
            this.isActualCheckBox.Text = "актуальная?";
            this.isActualCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton,
            this.infoLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(641, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
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
            // infoLabel
            // 
            this.infoLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(28, 22);
            this.infoLabel.Text = "info";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(641, 25);
            this.panel3.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(641, 300);
            this.panel4.TabIndex = 14;
            // 
            // UCTaskCalcMetric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Name = "UCTaskCalcMetric";
            this.Size = new System.Drawing.Size(641, 325);
            ((System.ComponentModel.ISupportInitialize)(this.actionBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldAngBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldIniBindingSource)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox actionComboBox;
        private System.Windows.Forms.BindingSource actionBindingSource;
        private UCIntDouble ucTimeShiftWeights;
        private UCClimate ucClimate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private Common.UCInterval timeWindowInterval;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox needClimateCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripLabel infoLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox fieldIniComboBox;
        private System.Windows.Forms.ComboBox fieldAngComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource fieldIniBindingSource;
        private System.Windows.Forms.BindingSource fieldAngBindingSource;
        private System.Windows.Forms.CheckBox isActualCheckBox;
    }
}
