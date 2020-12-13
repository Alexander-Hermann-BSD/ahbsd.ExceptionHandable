namespace ahbsd.lib.ExceptionHandable
{
    partial class FrmCaughtExceptions
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
            this.pnSender = new System.Windows.Forms.Panel();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.cntExceptions = new System.Windows.Forms.SplitContainer();
            this.lblDetail = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cntExceptions)).BeginInit();
            this.cntExceptions.Panel1.SuspendLayout();
            this.cntExceptions.Panel2.SuspendLayout();
            this.cntExceptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnSender
            // 
            this.pnSender.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnSender.Location = new System.Drawing.Point(6, 25);
            this.pnSender.Name = "pnSender";
            this.pnSender.Size = new System.Drawing.Size(244, 269);
            this.pnSender.TabIndex = 0;
            this.pnSender.SizeChanged += new System.EventHandler(this.pnSender_SizeChanged);
            this.pnSender.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.pnSender_ControlAdded);
            // 
            // lblInfo1
            // 
            this.lblInfo1.AutoSize = true;
            this.lblInfo1.Location = new System.Drawing.Point(3, 9);
            this.lblInfo1.Name = "lblInfo1";
            this.lblInfo1.Size = new System.Drawing.Size(44, 13);
            this.lblInfo1.TabIndex = 1;
            this.lblInfo1.Text = "Sender:";
            // 
            // txtDetail
            // 
            this.txtDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDetail.Location = new System.Drawing.Point(3, 25);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(497, 269);
            this.txtDetail.TabIndex = 2;
            // 
            // cntExceptions
            // 
            this.cntExceptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cntExceptions.Location = new System.Drawing.Point(0, 0);
            this.cntExceptions.Name = "cntExceptions";
            // 
            // cntExceptions.Panel1
            // 
            this.cntExceptions.Panel1.Controls.Add(this.lblInfo1);
            this.cntExceptions.Panel1.Controls.Add(this.pnSender);
            // 
            // cntExceptions.Panel2
            // 
            this.cntExceptions.Panel2.Controls.Add(this.lblDetail);
            this.cntExceptions.Panel2.Controls.Add(this.txtDetail);
            this.cntExceptions.Size = new System.Drawing.Size(760, 297);
            this.cntExceptions.SplitterDistance = 253;
            this.cntExceptions.TabIndex = 3;
            // 
            // lblDetail
            // 
            this.lblDetail.AutoSize = true;
            this.lblDetail.Location = new System.Drawing.Point(4, 9);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(42, 13);
            this.lblDetail.TabIndex = 3;
            this.lblDetail.Text = "Details:";
            // 
            // FrmCaughtExceptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 297);
            this.Controls.Add(this.cntExceptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmCaughtExceptions";
            this.Text = "Aufgefangene Exceptions";
            this.Load += new System.EventHandler(this.FrmCaughtExceptions_Load);
            this.cntExceptions.Panel1.ResumeLayout(false);
            this.cntExceptions.Panel1.PerformLayout();
            this.cntExceptions.Panel2.ResumeLayout(false);
            this.cntExceptions.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cntExceptions)).EndInit();
            this.cntExceptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnSender;
        private System.Windows.Forms.Label lblInfo1;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.SplitContainer cntExceptions;
        private System.Windows.Forms.Label lblDetail;
    }
}