﻿namespace SOV.Amur.Data
{
    partial class UCDataTable
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDataTable));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.optionsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dataFilterToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.add2ChartToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.infoToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.showHideEmptyRowsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveEditedToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.exportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.addColumnForecastButton = new System.Windows.Forms.ToolStripButton();
            this.viewTypeToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.timeBackwardToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.datesComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.sitesComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.varsComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.timeForwardToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dateTypeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDataDetail = new SOV.Amur.Data.UCDataDetails();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ucChart = new SOV.Amur.Data.UCChartDV();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 48);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.Size = new System.Drawing.Size(567, 245);
            this.dgv.TabIndex = 0;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgv.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEnter);
            this.dgv.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_ColumnHeaderMouseDoubleClick);
            this.dgv.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_RowHeaderMouseClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(573, 296);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripButton,
            this.optionsToolStripButton,
            this.dataFilterToolStripButton,
            this.add2ChartToolStripButton,
            this.infoToolStripLabel,
            this.showHideEmptyRowsToolStripButton,
            this.saveEditedToolStripButton,
            this.exportToolStripButton,
            this.addColumnForecastButton,
            this.viewTypeToolStripComboBox,
            this.toolStripSeparator1,
            this.timeBackwardToolStripButton,
            this.datesComboBox,
            this.sitesComboBox,
            this.varsComboBox,
            this.timeForwardToolStripButton,
            this.dateTypeToolStripButton,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(573, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // refreshToolStripButton
            // 
            this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripButton.Image = global::SOV.Amur.Data.Properties.Resources.refresh_16xLG;
            this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshToolStripButton.Name = "refreshToolStripButton";
            this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.refreshToolStripButton.Text = "toolStripButton1";
            this.refreshToolStripButton.ToolTipText = "Повторить запрос данных";
            this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
            // 
            // optionsToolStripButton
            // 
            this.optionsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.optionsToolStripButton.Image = global::SOV.Amur.Data.Properties.Resources.Property_501;
            this.optionsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionsToolStripButton.Name = "optionsToolStripButton";
            this.optionsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.optionsToolStripButton.Text = "toolStripButton1";
            this.optionsToolStripButton.ToolTipText = "Опции отображения данных";
            this.optionsToolStripButton.Click += new System.EventHandler(this.optionsToolStripButton_Click);
            // 
            // dataFilterToolStripButton
            // 
            this.dataFilterToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dataFilterToolStripButton.Image = global::SOV.Amur.Data.Properties.Resources.FilteredObject_13400_16x_MD;
            this.dataFilterToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dataFilterToolStripButton.Name = "dataFilterToolStripButton";
            this.dataFilterToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.dataFilterToolStripButton.Text = "toolStripButton1";
            this.dataFilterToolStripButton.ToolTipText = "Установить фильтр данных";
            this.dataFilterToolStripButton.Click += new System.EventHandler(this.dataFilterToolStripButton_Click);
            // 
            // add2ChartToolStripButton
            // 
            this.add2ChartToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.add2ChartToolStripButton.Image = global::SOV.Amur.Data.Properties.Resources.graph_performanceChart_5171_16x_LG;
            this.add2ChartToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.add2ChartToolStripButton.Name = "add2ChartToolStripButton";
            this.add2ChartToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.add2ChartToolStripButton.Text = "Добавить на график";
            this.add2ChartToolStripButton.Click += new System.EventHandler(this.add2ChartToolStripButton_Click);
            // 
            // infoToolStripLabel
            // 
            this.infoToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoToolStripLabel.Name = "infoToolStripLabel";
            this.infoToolStripLabel.Size = new System.Drawing.Size(10, 22);
            this.infoToolStripLabel.Text = ".";
            // 
            // showHideEmptyRowsToolStripButton
            // 
            this.showHideEmptyRowsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showHideEmptyRowsToolStripButton.Image = global::SOV.Amur.Data.Properties.Resources.Forward;
            this.showHideEmptyRowsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showHideEmptyRowsToolStripButton.Name = "showHideEmptyRowsToolStripButton";
            this.showHideEmptyRowsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.showHideEmptyRowsToolStripButton.Text = "Отобразить/скрыть";
            this.showHideEmptyRowsToolStripButton.ToolTipText = "Отобразить/скрыть строки без значений (пустые)";
            this.showHideEmptyRowsToolStripButton.Click += new System.EventHandler(this.showHideEmptyRowsToolStripButton_Click);
            // 
            // saveEditedToolStripButton
            // 
            this.saveEditedToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveEditedToolStripButton.Enabled = false;
            this.saveEditedToolStripButton.Image = global::SOV.Amur.Data.Properties.Resources.Save_6530;
            this.saveEditedToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveEditedToolStripButton.Name = "saveEditedToolStripButton";
            this.saveEditedToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveEditedToolStripButton.Text = "toolStripButton1";
            this.saveEditedToolStripButton.ToolTipText = "Сохранить изменения";
            this.saveEditedToolStripButton.Click += new System.EventHandler(this.saveEditedToolStripButton_Click);
            // 
            // exportToolStripButton
            // 
            this.exportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportToolStripButton.Image = global::SOV.Amur.Data.Properties.Resources.Exportfilter_10563_32;
            this.exportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportToolStripButton.Name = "exportToolStripButton";
            this.exportToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.exportToolStripButton.Text = "Экспорт данных в файл";
            this.exportToolStripButton.Click += new System.EventHandler(this.exportToolStripButton_Click);
            // 
            // addColumnForecastButton
            // 
            this.addColumnForecastButton.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.addColumnForecastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addColumnForecastButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.addColumnForecastButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addColumnForecastButton.Image = ((System.Drawing.Image)(resources.GetObject("addColumnForecastButton.Image")));
            this.addColumnForecastButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addColumnForecastButton.Name = "addColumnForecastButton";
            this.addColumnForecastButton.Size = new System.Drawing.Size(39, 22);
            this.addColumnForecastButton.Text = "FCS+";
            this.addColumnForecastButton.ToolTipText = "Добавить прогноз";
            this.addColumnForecastButton.Click += new System.EventHandler(this.addColumnForecastButton_Click);
            // 
            // viewTypeToolStripComboBox
            // 
            this.viewTypeToolStripComboBox.BackColor = System.Drawing.SystemColors.Info;
            this.viewTypeToolStripComboBox.Items.AddRange(new object[] {
            "Пункты и переменные",
            "Пункты по времени",
            "Отдельный пункт"});
            this.viewTypeToolStripComboBox.Name = "viewTypeToolStripComboBox";
            this.viewTypeToolStripComboBox.Size = new System.Drawing.Size(150, 25);
            this.viewTypeToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.viewTypeToolStripComboBox_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // timeBackwardToolStripButton
            // 
            this.timeBackwardToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.timeBackwardToolStripButton.Image = global::SOV.Amur.Data.Properties.Resources.arrow_back_color_16xLG;
            this.timeBackwardToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.timeBackwardToolStripButton.Name = "timeBackwardToolStripButton";
            this.timeBackwardToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.timeBackwardToolStripButton.Text = "toolStripButton1";
            this.timeBackwardToolStripButton.ToolTipText = "Назад";
            this.timeBackwardToolStripButton.Click += new System.EventHandler(this.timeBackwardToolStripButton_Click);
            // 
            // datesComboBox
            // 
            this.datesComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.datesComboBox.Name = "datesComboBox";
            this.datesComboBox.Size = new System.Drawing.Size(130, 25);
            this.datesComboBox.SelectedIndexChanged += new System.EventHandler(this.dateListComboBox_SelectedIndexChanged);
            // 
            // sitesComboBox
            // 
            this.sitesComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.sitesComboBox.Name = "sitesComboBox";
            this.sitesComboBox.Size = new System.Drawing.Size(121, 23);
            this.sitesComboBox.Visible = false;
            this.sitesComboBox.SelectedIndexChanged += new System.EventHandler(this.sitesComboBox_SelectedIndexChanged);
            // 
            // varsComboBox
            // 
            this.varsComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.varsComboBox.Name = "varsComboBox";
            this.varsComboBox.Size = new System.Drawing.Size(121, 23);
            this.varsComboBox.Visible = false;
            this.varsComboBox.SelectedIndexChanged += new System.EventHandler(this.varsComboBox_SelectedIndexChanged);
            // 
            // timeForwardToolStripButton
            // 
            this.timeForwardToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.timeForwardToolStripButton.Image = global::SOV.Amur.Data.Properties.Resources.arrow_Forward_color_16xLG;
            this.timeForwardToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.timeForwardToolStripButton.Name = "timeForwardToolStripButton";
            this.timeForwardToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.timeForwardToolStripButton.Text = "toolStripButton1";
            this.timeForwardToolStripButton.ToolTipText = "Вперёд";
            this.timeForwardToolStripButton.Click += new System.EventHandler(this.timeForwardToolStripButton_Click);
            // 
            // dateTypeToolStripButton
            // 
            this.dateTypeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dateTypeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("dateTypeToolStripButton.Image")));
            this.dateTypeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dateTypeToolStripButton.Name = "dateTypeToolStripButton";
            this.dateTypeToolStripButton.Size = new System.Drawing.Size(33, 19);
            this.dateTypeToolStripButton.Text = "ВСВ";
            this.dateTypeToolStripButton.ToolTipText = "Время ВСВ или ЛОК?";
            this.dateTypeToolStripButton.Click += new System.EventHandler(this.dateTypeToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(915, 296);
            this.splitContainer1.SplitterDistance = 573;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucDataDetail);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 296);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "История значения";
            // 
            // ucDataDetail
            // 
            this.ucDataDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataDetail.Location = new System.Drawing.Point(3, 16);
            this.ucDataDetail.Name = "ucDataDetail";
            this.ucDataDetail.ShowAQC = true;
            this.ucDataDetail.ShowDerived = true;
            this.ucDataDetail.Size = new System.Drawing.Size(332, 277);
            this.ucDataDetail.TabIndex = 0;
            this.ucDataDetail.UCCurrentDataValueActualizedEvent += new SOV.Amur.Data.UCDataDetails.UCCurrentDataValueActualizedEventHandler(this.ucDataDetail_UCCurrentDataValueActualizedEvent);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucChart);
            this.splitContainer2.Size = new System.Drawing.Size(915, 426);
            this.splitContainer2.SplitterDistance = 296;
            this.splitContainer2.TabIndex = 0;
            // 
            // ucChart
            // 
            this.ucChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChart.Location = new System.Drawing.Point(0, 0);
            this.ucChart.Name = "ucChart";
            this.ucChart.Size = new System.Drawing.Size(915, 126);
            this.ucChart.TabIndex = 0;
            // 
            // UCDataTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.splitContainer2);
            this.Name = "UCDataTable";
            this.Size = new System.Drawing.Size(915, 426);
            this.Load += new System.EventHandler(this.UCDataTableF2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton dataFilterToolStripButton;
        private System.Windows.Forms.ToolStripButton refreshToolStripButton;
        private System.Windows.Forms.ToolStripLabel infoToolStripLabel;
        private System.Windows.Forms.ToolStripButton saveEditedToolStripButton;
        private System.Windows.Forms.ToolStripButton optionsToolStripButton;
        private System.Windows.Forms.ToolStripButton exportToolStripButton;
        private System.Windows.Forms.ToolStripComboBox datesComboBox;
        private System.Windows.Forms.ToolStripButton dateTypeToolStripButton;
        private System.Windows.Forms.ToolStripButton timeForwardToolStripButton;
        private System.Windows.Forms.ToolStripButton timeBackwardToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox varsComboBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private UCDataDetails ucDataDetail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripComboBox viewTypeToolStripComboBox;
        private System.Windows.Forms.ToolStripComboBox sitesComboBox;
        private System.Windows.Forms.ToolStripButton add2ChartToolStripButton;
        private UCChartDV ucChart;
        private System.Windows.Forms.ToolStripButton addColumnForecastButton;
        private System.Windows.Forms.ToolStripButton showHideEmptyRowsToolStripButton;
    }
}
