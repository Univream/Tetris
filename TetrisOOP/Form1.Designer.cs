namespace TetrisOOP
{
    partial class Form1
    {

        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnRight = new System.Windows.Forms.Button();
            this.BtnLeft = new System.Windows.Forms.Button();
            this.BtnPause = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnDrop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnRight
            // 
            this.BtnRight.Location = new System.Drawing.Point(117, 12);
            this.BtnRight.Name = "BtnRight";
            this.BtnRight.Size = new System.Drawing.Size(75, 33);
            this.BtnRight.TabIndex = 0;
            this.BtnRight.TabStop = false;
            this.BtnRight.Text = "R";
            this.BtnRight.UseVisualStyleBackColor = true;
            this.BtnRight.Click += new System.EventHandler(this.BtnRight_Click);
            // 
            // BtnLeft
            // 
            this.BtnLeft.Location = new System.Drawing.Point(10, 12);
            this.BtnLeft.Name = "BtnLeft";
            this.BtnLeft.Size = new System.Drawing.Size(75, 33);
            this.BtnLeft.TabIndex = 1;
            this.BtnLeft.TabStop = false;
            this.BtnLeft.Text = "L";
            this.BtnLeft.UseVisualStyleBackColor = true;
            this.BtnLeft.Click += new System.EventHandler(this.BtnLeft_Click);
            // 
            // BtnPause
            // 
            this.BtnPause.Location = new System.Drawing.Point(66, 417);
            this.BtnPause.Name = "BtnPause";
            this.BtnPause.Size = new System.Drawing.Size(75, 54);
            this.BtnPause.TabIndex = 3;
            this.BtnPause.TabStop = false;
            this.BtnPause.Text = "Stop";
            this.BtnPause.UseVisualStyleBackColor = true;
            this.BtnPause.Click += new System.EventHandler(this.BtnPause_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(10, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 284);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(190, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 284);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Location = new System.Drawing.Point(11, 410);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 1);
            this.panel3.TabIndex = 6;
            // 
            // BtnDrop
            // 
            this.BtnDrop.Location = new System.Drawing.Point(66, 51);
            this.BtnDrop.Name = "BtnDrop";
            this.BtnDrop.Size = new System.Drawing.Size(75, 50);
            this.BtnDrop.TabIndex = 7;
            this.BtnDrop.TabStop = false;
            this.BtnDrop.Text = "Drop";
            this.BtnDrop.UseVisualStyleBackColor = true;
            this.BtnDrop.Click += new System.EventHandler(this.BtnDrop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 483);
            this.Controls.Add(this.BtnDrop);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnPause);
            this.Controls.Add(this.BtnLeft);
            this.Controls.Add(this.BtnRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnRight;
        private System.Windows.Forms.Button BtnLeft;
        private System.Windows.Forms.Button BtnPause;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnDrop;
    }
}

