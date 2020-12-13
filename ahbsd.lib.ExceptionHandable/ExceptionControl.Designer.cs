namespace ahbsd.lib.ExceptionHandable
{ 
    partial class ExceptionControl
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
            this.components = new System.ComponentModel.Container();
            this.lblZeitpunkt = new System.Windows.Forms.Label();
            this.tt1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblExceptionType = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblZeitpunkt
            // 
            this.lblZeitpunkt.AutoSize = true;
            this.lblZeitpunkt.Location = new System.Drawing.Point(3, 10);
            this.lblZeitpunkt.Name = "lblZeitpunkt";
            this.lblZeitpunkt.Size = new System.Drawing.Size(91, 13);
            this.lblZeitpunkt.TabIndex = 0;
            this.lblZeitpunkt.Tag = "Zeitpunkt";
            this.lblZeitpunkt.Text = "05.07.2018 15:34";
            this.tt1.SetToolTip(this.lblZeitpunkt, "Zeitpunkt des Auftretens");
            // 
            // lblExceptionType
            // 
            this.lblExceptionType.AutoEllipsis = true;
            this.lblExceptionType.Location = new System.Drawing.Point(110, 10);
            this.lblExceptionType.Name = "lblExceptionType";
            this.lblExceptionType.Size = new System.Drawing.Size(132, 13);
            this.lblExceptionType.TabIndex = 1;
            this.lblExceptionType.Text = "Exception";
            this.tt1.SetToolTip(this.lblExceptionType, "Exception-Type");
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.AutoEllipsis = true;
            this.lblMessage.Location = new System.Drawing.Point(248, 10);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(313, 13);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Der Inhalt";
            this.tt1.SetToolTip(this.lblMessage, "Message");
            // 
            // ExceptionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblExceptionType);
            this.Controls.Add(this.lblZeitpunkt);
            this.Name = "ExceptionControl";
            this.Size = new System.Drawing.Size(573, 30);
            this.Load += new System.EventHandler(this.ExceptionControl_Load);
            this.Enter += new System.EventHandler(this.ExceptionControl_Enter);
            this.Leave += new System.EventHandler(this.ExceptionControl_Leave);
            this.MouseEnter += new System.EventHandler(this.ExceptionControl_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblZeitpunkt;
        private System.Windows.Forms.ToolTip tt1;
        private System.Windows.Forms.Label lblExceptionType;
        private System.Windows.Forms.Label lblMessage;
    }
}
