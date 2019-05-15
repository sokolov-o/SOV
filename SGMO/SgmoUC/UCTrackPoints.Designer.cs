namespace SOV.SGMO
{
    partial class UCTrackPoints
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
            this.trackPointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dateUTCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.geoPointDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uTCOffsetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPointBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateUTCDataGridViewTextBoxColumn,
            this.geoPointDataGridViewTextBoxColumn,
            this.uTCOffsetDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.trackPointBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(318, 432);
            this.dgv.TabIndex = 0;
            // 
            // trackPointBindingSource
            // 
            this.trackPointBindingSource.DataSource = typeof(SOV.SGMO.TrackPoint);
            // 
            // dateUTCDataGridViewTextBoxColumn
            // 
            this.dateUTCDataGridViewTextBoxColumn.DataPropertyName = "DateUTC";
            this.dateUTCDataGridViewTextBoxColumn.HeaderText = "DateUTC";
            this.dateUTCDataGridViewTextBoxColumn.Name = "dateUTCDataGridViewTextBoxColumn";
            // 
            // geoPointDataGridViewTextBoxColumn
            // 
            this.geoPointDataGridViewTextBoxColumn.DataPropertyName = "GeoPoint";
            this.geoPointDataGridViewTextBoxColumn.HeaderText = "GeoPoint";
            this.geoPointDataGridViewTextBoxColumn.Name = "geoPointDataGridViewTextBoxColumn";
            // 
            // uTCOffsetDataGridViewTextBoxColumn
            // 
            this.uTCOffsetDataGridViewTextBoxColumn.DataPropertyName = "UTCOffset";
            this.uTCOffsetDataGridViewTextBoxColumn.HeaderText = "UTCOffset";
            this.uTCOffsetDataGridViewTextBoxColumn.Name = "uTCOffsetDataGridViewTextBoxColumn";
            // 
            // UCTrackPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Name = "UCTrackPoints";
            this.Size = new System.Drawing.Size(318, 432);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPointBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.BindingSource trackPointBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateUTCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn geoPointDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uTCOffsetDataGridViewTextBoxColumn;
    }
}
