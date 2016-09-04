namespace TermSim
{
    partial class FrmMain
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
            this.lblSubmit = new System.Windows.Forms.Label();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.lblChevron = new System.Windows.Forms.Label();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblSubmit
            // 
            this.lblSubmit.BackColor = System.Drawing.SystemColors.Control;
            this.lblSubmit.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmit.ForeColor = System.Drawing.Color.Black;
            this.lblSubmit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblSubmit.Location = new System.Drawing.Point(555, 279);
            this.lblSubmit.Name = "lblSubmit";
            this.lblSubmit.Size = new System.Drawing.Size(12, 13);
            this.lblSubmit.TabIndex = 13;
            this.lblSubmit.Text = "»";
            this.lblSubmit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblSubmit.Click += new System.EventHandler(this.lblSubmit_Click);
            // 
            // rtbConsole
            // 
            this.rtbConsole.BackColor = System.Drawing.Color.Black;
            this.rtbConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbConsole.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbConsole.ForeColor = System.Drawing.Color.White;
            this.rtbConsole.Location = new System.Drawing.Point(0, 0);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbConsole.Size = new System.Drawing.Size(567, 279);
            this.rtbConsole.TabIndex = 12;
            this.rtbConsole.Text = "";
            // 
            // lblChevron
            // 
            this.lblChevron.BackColor = System.Drawing.Color.Black;
            this.lblChevron.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChevron.ForeColor = System.Drawing.Color.White;
            this.lblChevron.Location = new System.Drawing.Point(0, 279);
            this.lblChevron.Name = "lblChevron";
            this.lblChevron.Size = new System.Drawing.Size(13, 15);
            this.lblChevron.TabIndex = 11;
            this.lblChevron.Text = "$";
            // 
            // txtCmd
            // 
            this.txtCmd.BackColor = System.Drawing.Color.Black;
            this.txtCmd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCmd.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCmd.ForeColor = System.Drawing.Color.White;
            this.txtCmd.Location = new System.Drawing.Point(13, 279);
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(537, 15);
            this.txtCmd.TabIndex = 10;
            this.txtCmd.WordWrap = false;
            this.txtCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCmd_KeyDown);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 294);
            this.Controls.Add(this.lblSubmit);
            this.Controls.Add(this.rtbConsole);
            this.Controls.Add(this.lblChevron);
            this.Controls.Add(this.txtCmd);
            this.Name = "FrmMain";
            this.Text = "TermSim by Kez";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSubmit;
        public System.Windows.Forms.RichTextBox rtbConsole;
        private System.Windows.Forms.Label lblChevron;
        private System.Windows.Forms.TextBox txtCmd;
    }
}

