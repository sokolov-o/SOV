namespace SOV.SGMO
{
    partial class UCDataForecasts
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripLabel();
            this.trackPointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataFcsExtBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.leadTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siteNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateIniUTCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateFcsUTCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateFcsLOCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.variableNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetTypeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackPointBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataFcsExtBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.leadTimeDataGridViewTextBoxColumn,
            this.siteNameDataGridViewTextBoxColumn,
            this.dateIniUTCDataGridViewTextBoxColumn,
            this.dateFcsUTCDataGridViewTextBoxColumn,
            this.dateFcsLOCDataGridViewTextBoxColumn,
            this.variableNameDataGridViewTextBoxColumn,
            this.offsetTypeNameDataGridViewTextBoxColumn,
            this.offsetValueDataGridViewTextBoxColumn,
            this.methodNameDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn2});
            this.dgv.DataSource = this.dataFcsExtBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 28);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.Size = new System.Drawing.Size(609, 401);
            this.dgv.TabIndex = 0;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(615, 432);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(615, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // infoLabel
            // 
            this.infoLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(16, 22);
            this.infoLabel.Text = "...";
            // 
            // trackPointBindingSource
            // 
            this.trackPointBindingSource.DataSource = typeof(SOV.SGMO.TrackPoint);
            // 
            // dataFcsExtBindingSource
            // 
            this.dataFcsExtBindingSource.DataSource = typeof(SOV.SGMO.DataFcsExt);
            // 
            // leadTimeDataGridViewTextBoxColumn
            // 
            this.leadTimeDataGridViewTextBoxColumn.DataPropertyName = "LeadTime";
            this.leadTimeDataGridViewTextBoxColumn.HeaderText = "LeadTime";
            this.leadTimeDataGridViewTextBoxColumn.Name = "leadTimeDataGridViewTextBoxColumn";
            this.leadTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // siteNameDataGridViewTextBoxColumn
            // 
            this.siteNameDataGridViewTextBoxColumn.DataPropertyName = "SiteName";
            this.siteNameDataGridViewTextBoxColumn.HeaderText = "SiteName";
            this.siteNameDataGridViewTextBoxColumn.Name = "siteNameDataGridViewTextBoxColumn";
            this.siteNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateIniUTCDataGridViewTextBoxColumn
            // 
            this.dateIniUTCDataGridViewTextBoxColumn.DataPropertyName = "DateIniUTC";
            this.dateIniUTCDataGridViewTextBoxColumn.HeaderText = "DateIniUTC";
            this.dateIniUTCDataGridViewTextBoxColumn.Name = "dateIniUTCDataGridViewTextBoxColumn";
            this.dateIniUTCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateFcsUTCDataGridViewTextBoxColumn
            // 
            this.dateFcsUTCDataGridViewTextBoxColumn.DataPropertyName = "DateFcsUTC";
            this.dateFcsUTCDataGridViewTextBoxColumn.HeaderText = "DateFcsUTC";
            this.dateFcsUTCDataGridViewTextBoxColumn.Name = "dateFcsUTCDataGridViewTextBoxColumn";
            this.dateFcsUTCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateFcsLOCDataGridViewTextBoxColumn
            // 
            this.dateFcsLOCDataGridViewTextBoxColumn.DataPropertyName = "DateFcsLOC";
            this.dateFcsLOCDataGridViewTextBoxColumn.HeaderText = "DateFcsLOC";
            this.dateFcsLOCDataGridViewTextBoxColumn.Name = "dateFcsLOCDataGridViewTextBoxColumn";
            this.dateFcsLOCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // variableNameDataGridViewTextBoxColumn
            // 
            this.variableNameDataGridViewTextBoxColumn.DataPropertyName = "VariableName";
            this.variableNameDataGridViewTextBoxColumn.HeaderText = "VariableName";
            this.variableNameDataGridViewTextBoxColumn.Name = "variableNameDataGridViewTextBoxColumn";
            this.variableNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // offsetTypeNameDataGridViewTextBoxColumn
            // 
            this.offsetTypeNameDataGridViewTextBoxColumn.DataPropertyName = "OffsetTypeName";
            this.offsetTypeNameDataGridViewTextBoxColumn.HeaderText = "OffsetTypeName";
            this.offsetTypeNameDataGridViewTextBoxColumn.Name = "offsetTypeNameDataGridViewTextBoxColumn";
            this.offsetTypeNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // offsetValueDataGridViewTextBoxColumn
            // 
            this.offsetValueDataGridViewTextBoxColumn.DataPropertyName = "OffsetValue";
            this.offsetValueDataGridViewTextBoxColumn.HeaderText = "OffsetValue";
            this.offsetValueDataGridViewTextBoxColumn.Name = "offsetValueDataGridViewTextBoxColumn";
            this.offsetValueDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // methodNameDataGridViewTextBoxColumn
            // 
            this.methodNameDataGridViewTextBoxColumn.DataPropertyName = "MethodName";
            this.methodNameDataGridViewTextBoxColumn.HeaderText = "MethodName";
            this.methodNameDataGridViewTextBoxColumn.Name = "methodNameDataGridViewTextBoxColumn";
            this.methodNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "UTCOffsetHours";
            this.dataGridViewTextBoxColumn2.HeaderText = "UTCOffsetHours";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // UCDataForecasts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCDataForecasts";
            this.Size = new System.Drawing.Size(615, 432);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackPointBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataFcsExtBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource trackPointBindingSource;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn catalogExtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trackPointDataGridViewTextBoxColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel infoLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn uTCOffsetHoursDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateInsertDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leadTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateIniUTCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateFcsUTCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateFcsLOCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn variableNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetTypeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn methodNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.BindingSource dataFcsExtBindingSource;
    }
}
