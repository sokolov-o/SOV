namespace SOV.SGMO
{
    partial class FormNewTrackPart
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateSUTCDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.latTextBox = new System.Windows.Forms.TextBox();
            this.lonTextBox = new System.Windows.Forms.TextBox();
            this.speedTextBox = new System.Windows.Forms.TextBox();
            this.refPointsListBox = new System.Windows.Forms.ListBox();
            this.hoursCountTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.utcOffsetTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateSUTCDateTimePicker
            // 
            this.dateSUTCDateTimePicker.CustomFormat = "dd.MM.yyyy HH";
            this.dateSUTCDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateSUTCDateTimePicker.Location = new System.Drawing.Point(53, 25);
            this.dateSUTCDateTimePicker.Name = "dateSUTCDateTimePicker";
            this.dateSUTCDateTimePicker.Size = new System.Drawing.Size(106, 20);
            this.dateSUTCDateTimePicker.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(67, 330);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(148, 330);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // latTextBox
            // 
            this.latTextBox.Location = new System.Drawing.Point(121, 67);
            this.latTextBox.Name = "latTextBox";
            this.latTextBox.Size = new System.Drawing.Size(100, 20);
            this.latTextBox.TabIndex = 3;
            this.latTextBox.Text = "42";
            // 
            // lonTextBox
            // 
            this.lonTextBox.Location = new System.Drawing.Point(339, 67);
            this.lonTextBox.Name = "lonTextBox";
            this.lonTextBox.Size = new System.Drawing.Size(100, 20);
            this.lonTextBox.TabIndex = 4;
            this.lonTextBox.Text = "139";
            // 
            // speedTextBox
            // 
            this.speedTextBox.Location = new System.Drawing.Point(121, 93);
            this.speedTextBox.Name = "speedTextBox";
            this.speedTextBox.Size = new System.Drawing.Size(100, 20);
            this.speedTextBox.TabIndex = 5;
            this.speedTextBox.Text = "10";
            // 
            // refPointsListBox
            // 
            this.refPointsListBox.FormattingEnabled = true;
            this.refPointsListBox.Location = new System.Drawing.Point(339, 93);
            this.refPointsListBox.Name = "refPointsListBox";
            this.refPointsListBox.Size = new System.Drawing.Size(120, 225);
            this.refPointsListBox.TabIndex = 6;
            // 
            // hoursCountTextBox
            // 
            this.hoursCountTextBox.Location = new System.Drawing.Point(123, 119);
            this.hoursCountTextBox.Name = "hoursCountTextBox";
            this.hoursCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.hoursCountTextBox.TabIndex = 7;
            this.hoursCountTextBox.Text = "120";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Широта (градусы)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Долгота (градусы)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Скорость (км/час):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Часов вперед:";
            // 
            // utcOffsetTextBox
            // 
            this.utcOffsetTextBox.Location = new System.Drawing.Point(157, 162);
            this.utcOffsetTextBox.Name = "utcOffsetTextBox";
            this.utcOffsetTextBox.Size = new System.Drawing.Size(100, 20);
            this.utcOffsetTextBox.TabIndex = 12;
            this.utcOffsetTextBox.Text = "11";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "ВСВ смещение (час):";
            // 
            // FormNewTrackPart
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.utcOffsetTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hoursCountTextBox);
            this.Controls.Add(this.refPointsListBox);
            this.Controls.Add(this.speedTextBox);
            this.Controls.Add(this.lonTextBox);
            this.Controls.Add(this.latTextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateSUTCDateTimePicker);
            this.Name = "FormNewTrackPart";
            this.Text = "FormNewTrackPart";
            this.Load += new System.EventHandler(this.FormNewTrackPart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateSUTCDateTimePicker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox latTextBox;
        private System.Windows.Forms.TextBox lonTextBox;
        private System.Windows.Forms.TextBox speedTextBox;
        private System.Windows.Forms.ListBox refPointsListBox;
        private System.Windows.Forms.TextBox hoursCountTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox utcOffsetTextBox;
        private System.Windows.Forms.Label label5;
    }
}