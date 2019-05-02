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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timeProcessTypeComboBox = new System.Windows.Forms.ComboBox();
            this.timeProcessTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.isActualCheckBox = new System.Windows.Forms.CheckBox();
            this.fieldGeoobsComboBox = new System.Windows.Forms.ComboBox();
            this.fieldGeoobsViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeProcessTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldGeoobsViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.timeProcessTypeComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.fieldGeoobsComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.isActualCheckBox, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(396, 76);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // timeProcessTypeComboBox
            // 
            this.timeProcessTypeComboBox.DataSource = this.timeProcessTypeBindingSource;
            this.timeProcessTypeComboBox.DisplayMember = "Name";
            this.timeProcessTypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeProcessTypeComboBox.FormattingEnabled = true;
            this.timeProcessTypeComboBox.Location = new System.Drawing.Point(156, 30);
            this.timeProcessTypeComboBox.Name = "timeProcessTypeComboBox";
            this.timeProcessTypeComboBox.Size = new System.Drawing.Size(237, 21);
            this.timeProcessTypeComboBox.TabIndex = 2;
            this.timeProcessTypeComboBox.ValueMember = "Id";
            // 
            // timeProcessTypeBindingSource
            // 
            this.timeProcessTypeBindingSource.DataSource = typeof(FERHRI.Analog.TimeProcessType);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Тип обработки по времени:";
            // 
            // isActualCheckBox
            // 
            this.isActualCheckBox.AutoSize = true;
            this.isActualCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isActualCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.isActualCheckBox.Location = new System.Drawing.Point(156, 57);
            this.isActualCheckBox.Name = "isActualCheckBox";
            this.isActualCheckBox.Size = new System.Drawing.Size(135, 18);
            this.isActualCheckBox.TabIndex = 5;
            this.isActualCheckBox.Text = "Актуальная задача?";
            this.isActualCheckBox.UseVisualStyleBackColor = true;
            // 
            // fieldGeoobsComboBox
            // 
            this.fieldGeoobsComboBox.DataSource = this.fieldGeoobsViewBindingSource;
            this.fieldGeoobsComboBox.DisplayMember = "Name";
            this.fieldGeoobsComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldGeoobsComboBox.FormattingEnabled = true;
            this.fieldGeoobsComboBox.Location = new System.Drawing.Point(156, 3);
            this.fieldGeoobsComboBox.Name = "fieldGeoobsComboBox";
            this.fieldGeoobsComboBox.Size = new System.Drawing.Size(237, 21);
            this.fieldGeoobsComboBox.TabIndex = 6;
            this.fieldGeoobsComboBox.ValueMember = "Id";
            // 
            // fieldGeoobsViewBindingSource
            // 
            this.fieldGeoobsViewBindingSource.DataSource = typeof(FERHRI.Analog._DELME_FieldGeoobsView);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Поле, метрика, регионы";
            // 
            // UCTaskCalcMetric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCTaskCalcMetric";
            this.Size = new System.Drawing.Size(396, 76);
            this.Load += new System.EventHandler(this.UCTaskCalcMetric_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeProcessTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldGeoobsViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox timeProcessTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource timeProcessTypeBindingSource;
        private System.Windows.Forms.CheckBox isActualCheckBox;
        private System.Windows.Forms.ComboBox fieldGeoobsComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource fieldGeoobsViewBindingSource;
    }
}
