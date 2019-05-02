namespace FERHRI.Analog
{
    partial class UCDataAnalogList
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dateIniDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateWeightAnalogsString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateWeightAnalogsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataAnalogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataAnalogBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(252, 221);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(252, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::FERHRI.Analog.Properties.Resources.save_16xLG;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateIniDataGridViewTextBoxColumn,
            this.DateWeightAnalogsString,
            this.DateWeightAnalogsCount});
            this.dgv.DataSource = this.dataAnalogBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 28);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(246, 190);
            this.dgv.TabIndex = 1;
            // 
            // dateIniDataGridViewTextBoxColumn
            // 
            this.dateIniDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dateIniDataGridViewTextBoxColumn.DataPropertyName = "DateIni";
            this.dateIniDataGridViewTextBoxColumn.HeaderText = "Дата исх.";
            this.dateIniDataGridViewTextBoxColumn.Name = "dateIniDataGridViewTextBoxColumn";
            this.dateIniDataGridViewTextBoxColumn.Width = 75;
            // 
            // DateWeightAnalogsString
            // 
            this.DateWeightAnalogsString.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DateWeightAnalogsString.DataPropertyName = "DateWeightAnalogsString";
            this.DateWeightAnalogsString.HeaderText = "Даты и веса аналогов";
            this.DateWeightAnalogsString.Name = "DateWeightAnalogsString";
            this.DateWeightAnalogsString.ReadOnly = true;
            // 
            // DateWeightAnalogsCount
            // 
            this.DateWeightAnalogsCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DateWeightAnalogsCount.DataPropertyName = "DateWeightAnalogsCount";
            this.DateWeightAnalogsCount.HeaderText = "Кол.";
            this.DateWeightAnalogsCount.Name = "DateWeightAnalogsCount";
            this.DateWeightAnalogsCount.ReadOnly = true;
            this.DateWeightAnalogsCount.Width = 54;
            // 
            // dataAnalogBindingSource
            // 
            this.dataAnalogBindingSource.DataSource = typeof(FERHRI.Analog.DataAnalog0);
            // 
            // UCDataAnalogList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCDataAnalogList";
            this.Size = new System.Drawing.Size(252, 221);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataAnalogBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.BindingSource dataAnalogBindingSource;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateWeightAnalogsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateIniDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateWeightAnalogsString;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateWeightAnalogsCount;
    }
}
