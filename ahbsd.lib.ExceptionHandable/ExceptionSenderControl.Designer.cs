namespace ahbsd.lib.ExceptionHandable
{
    partial class ExceptionSenderControl
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSenderType = new System.Windows.Forms.Label();
            this.txtSenderType = new System.Windows.Forms.Label();
            this.pnExceptions = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblSenderType
            // 
            this.lblSenderType.AutoSize = true;
            this.lblSenderType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderType.Location = new System.Drawing.Point(13, 9);
            this.lblSenderType.Name = "lblSenderType";
            this.lblSenderType.Size = new System.Drawing.Size(76, 13);
            this.lblSenderType.TabIndex = 0;
            this.lblSenderType.Text = "Sender-Typ:";
            // 
            // txtSenderType
            // 
            this.txtSenderType.AutoSize = true;
            this.txtSenderType.Location = new System.Drawing.Point(95, 9);
            this.txtSenderType.Name = "txtSenderType";
            this.txtSenderType.Size = new System.Drawing.Size(38, 13);
            this.txtSenderType.TabIndex = 1;
            this.txtSenderType.Text = "Object";
            // 
            // pnExceptions
            // 
            this.pnExceptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnExceptions.AutoScroll = true;
            this.pnExceptions.Location = new System.Drawing.Point(16, 26);
            this.pnExceptions.Name = "pnExceptions";
            this.pnExceptions.Size = new System.Drawing.Size(379, 117);
            this.pnExceptions.TabIndex = 2;
            // 
            // ExceptionSenderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pnExceptions);
            this.Controls.Add(this.txtSenderType);
            this.Controls.Add(this.lblSenderType);
            this.Name = "ExceptionSenderControl";
            this.Size = new System.Drawing.Size(398, 146);
            this.Load += new System.EventHandler(this.ExceptionSenderControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSenderType;
        private System.Windows.Forms.Label txtSenderType;
        private System.Windows.Forms.Panel pnExceptions;
    }
}
