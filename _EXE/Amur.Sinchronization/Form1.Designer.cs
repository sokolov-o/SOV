namespace FERHRI.Amur.Sinchronization
{
    partial class Form1
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
            this.sinchronizeHBRButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sinchronizeHBRButton
            // 
            this.sinchronizeHBRButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.sinchronizeHBRButton.Location = new System.Drawing.Point(12, 12);
            this.sinchronizeHBRButton.Name = "sinchronizeHBRButton";
            this.sinchronizeHBRButton.Size = new System.Drawing.Size(256, 23);
            this.sinchronizeHBRButton.TabIndex = 0;
            this.sinchronizeHBRButton.Text = "Синхронизировать БД \"Амур-Хабаровск\" Meta";
            this.sinchronizeHBRButton.UseVisualStyleBackColor = true;
            this.sinchronizeHBRButton.Click += new System.EventHandler(this.VariableSinchButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 376);
            this.Controls.Add(this.sinchronizeHBRButton);
            this.Name = "Form1";
            this.Text = "Синхронизация баз данных \"Амур\"";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button sinchronizeHBRButton;
    }
}

