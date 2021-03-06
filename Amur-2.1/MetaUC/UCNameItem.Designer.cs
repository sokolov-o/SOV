﻿namespace FERHRI.Amur.Meta
{
    partial class UCNameItem
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.langIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.langBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameTypeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nameTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.langBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.langIdDataGridViewTextBoxColumn,
            this.nameTypeIdDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.nameItemBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 28);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(407, 262);
            this.dgv.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(413, 293);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(413, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::FERHRI.Amur.Meta.Properties.Resources.Save_6530;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "toolStripButton1";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Наименование";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // langIdDataGridViewTextBoxColumn
            // 
            this.langIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.langIdDataGridViewTextBoxColumn.DataPropertyName = "LangId";
            this.langIdDataGridViewTextBoxColumn.DataSource = this.langBindingSource;
            this.langIdDataGridViewTextBoxColumn.DisplayMember = "Name";
            this.langIdDataGridViewTextBoxColumn.HeaderText = "Язык";
            this.langIdDataGridViewTextBoxColumn.Name = "langIdDataGridViewTextBoxColumn";
            this.langIdDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.langIdDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.langIdDataGridViewTextBoxColumn.ValueMember = "Id";
            this.langIdDataGridViewTextBoxColumn.Width = 60;
            // 
            // langBindingSource
            // 
            this.langBindingSource.DataSource = typeof(FERHRI.Common.IdName);
            // 
            // nameTypeIdDataGridViewTextBoxColumn
            // 
            this.nameTypeIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nameTypeIdDataGridViewTextBoxColumn.DataPropertyName = "NameTypeId";
            this.nameTypeIdDataGridViewTextBoxColumn.DataSource = this.nameTypeBindingSource;
            this.nameTypeIdDataGridViewTextBoxColumn.DisplayMember = "Name";
            this.nameTypeIdDataGridViewTextBoxColumn.HeaderText = "Тип";
            this.nameTypeIdDataGridViewTextBoxColumn.Name = "nameTypeIdDataGridViewTextBoxColumn";
            this.nameTypeIdDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.nameTypeIdDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.nameTypeIdDataGridViewTextBoxColumn.ValueMember = "Id";
            this.nameTypeIdDataGridViewTextBoxColumn.Width = 51;
            // 
            // nameTypeBindingSource
            // 
            this.nameTypeBindingSource.DataSource = typeof(FERHRI.Common.IdName);
            // 
            // nameItemBindingSource
            // 
            this.nameItemBindingSource.DataSource = typeof(FERHRI.Amur.Meta.NameItem);
            this.nameItemBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.nameItemBindingSource_AddingNew);
            // 
            // UCNameItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCNameItem";
            this.Size = new System.Drawing.Size(413, 293);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.langBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.BindingSource nameItemBindingSource;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.BindingSource langBindingSource;
        private System.Windows.Forms.BindingSource nameTypeBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn langIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn nameTypeIdDataGridViewTextBoxColumn;
    }
}
