namespace FERHRI.Analog
{
    partial class _DELME_UCPrototype
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
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.ucShowButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ucField1 = new FERHRI.Analog.UCField();
            this.tlp.SuspendLayout();
            this.tools.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp
            // 
            this.tlp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlp.ColumnCount = 1;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Controls.Add(this.tools, 0, 0);
            this.tlp.Controls.Add(this.ucField1, 0, 1);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 2;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Size = new System.Drawing.Size(393, 238);
            this.tlp.TabIndex = 0;
            // 
            // tools
            // 
            this.tools.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ucShowButton,
            this.toolStripLabel1});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tools.Size = new System.Drawing.Size(393, 25);
            this.tools.TabIndex = 2;
            this.tools.Text = "toolStrip1";
            // 
            // ucShowButton
            // 
            this.ucShowButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ucShowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ucShowButton.Image = global::FERHRI.Analog.Properties.Resources.ViewBox_10697_24;
            this.ucShowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ucShowButton.Name = "ucShowButton";
            this.ucShowButton.Size = new System.Drawing.Size(23, 22);
            this.ucShowButton.Text = "toolStripButton1";
            this.ucShowButton.Click += new System.EventHandler(this.ucShowButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Поле";
            // 
            // ucField1
            // 
            this.ucField1.Location = new System.Drawing.Point(3, 28);
            this.ucField1.Name = "ucField1";
            this.ucField1.Size = new System.Drawing.Size(8, 8);
            this.ucField1.TabIndex = 3;
            this.ucField1.Value = null;
            // 
            // UCPrototype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tlp);
            this.Name = "UCPrototype";
            this.Size = new System.Drawing.Size(393, 238);
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton ucShowButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private UCField ucField1;
    }
}
