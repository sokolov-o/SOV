namespace SOV.SGMO
{
    partial class UCDataTrackForecasts
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
            this.dataTrackFcsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.trackPointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dataTrackFcsExtBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataTrackFcsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trackPointDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataTrackFcsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPointBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTrackFcsExtBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTrackFcsBindingSource
            // 
            this.dataTrackFcsBindingSource.DataSource = typeof(SOV.SGMO.DataTrackFcs);
            // 
            // trackPointBindingSource
            // 
            this.trackPointBindingSource.DataSource = typeof(SOV.SGMO.TrackPoint);
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataTrackFcsDataGridViewTextBoxColumn,
            this.trackPointDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.dataTrackFcsExtBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(318, 432);
            this.dgv.TabIndex = 0;
            // 
            // dataTrackFcsExtBindingSource
            // 
            this.dataTrackFcsExtBindingSource.DataSource = typeof(SOV.SGMO.DataTrackFcsExt);
            // 
            // dataTrackFcsDataGridViewTextBoxColumn
            // 
            this.dataTrackFcsDataGridViewTextBoxColumn.DataPropertyName = "DataTrackFcs";
            this.dataTrackFcsDataGridViewTextBoxColumn.HeaderText = "DataTrackFcs";
            this.dataTrackFcsDataGridViewTextBoxColumn.Name = "dataTrackFcsDataGridViewTextBoxColumn";
            // 
            // trackPointDataGridViewTextBoxColumn
            // 
            this.trackPointDataGridViewTextBoxColumn.DataPropertyName = "TrackPoint.Name";
            this.trackPointDataGridViewTextBoxColumn.HeaderText = "TrackPoint";
            this.trackPointDataGridViewTextBoxColumn.Name = "trackPointDataGridViewTextBoxColumn";
            // 
            // UCDataTrackForecasts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Name = "UCDataTrackForecasts";
            this.Size = new System.Drawing.Size(318, 432);
            ((System.ComponentModel.ISupportInitialize)(this.dataTrackFcsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPointBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTrackFcsExtBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource trackPointBindingSource;
        private System.Windows.Forms.BindingSource dataTrackFcsBindingSource;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn catalogExtDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource dataTrackFcsExtBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataTrackFcsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trackPointDataGridViewTextBoxColumn;
    }
}
