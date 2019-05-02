namespace FERHRI.Analog
{
    partial class UCClimate
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
            this.catalogIdAvgTextBox = new System.Windows.Forms.TextBox();
            this.catalogIdStdTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.yearSFInterval = new FERHRI.Common.UCInterval();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // catalogIdAvgTextBox
            // 
            this.catalogIdAvgTextBox.Location = new System.Drawing.Point(106, 19);
            this.catalogIdAvgTextBox.Name = "catalogIdAvgTextBox";
            this.catalogIdAvgTextBox.Size = new System.Drawing.Size(100, 20);
            this.catalogIdAvgTextBox.TabIndex = 1;
            // 
            // catalogIdStdTextBox
            // 
            this.catalogIdStdTextBox.Location = new System.Drawing.Point(106, 45);
            this.catalogIdStdTextBox.Name = "catalogIdStdTextBox";
            this.catalogIdStdTextBox.Size = new System.Drawing.Size(100, 20);
            this.catalogIdStdTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Годы климата:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.catalogIdAvgTextBox);
            this.groupBox1.Controls.Add(this.catalogIdStdTextBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 71);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Коды записей каталога";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "СКО:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Среднее (норма):";
            // 
            // yearSFInterval
            // 
            this.yearSFInterval.Location = new System.Drawing.Point(91, 3);
            this.yearSFInterval.Name = "yearSFInterval";
            this.yearSFInterval.Size = new System.Drawing.Size(113, 20);
            this.yearSFInterval.TabIndex = 0;
            // 
            // UCClimate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.yearSFInterval);
            this.Name = "UCClimate";
            this.Size = new System.Drawing.Size(222, 101);
            this.Load += new System.EventHandler(this.UCClimate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.UCInterval yearSFInterval;
        private System.Windows.Forms.TextBox catalogIdAvgTextBox;
        private System.Windows.Forms.TextBox catalogIdStdTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}
