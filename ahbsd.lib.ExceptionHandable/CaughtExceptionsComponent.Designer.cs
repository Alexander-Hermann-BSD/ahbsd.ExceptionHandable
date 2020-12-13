namespace ahbsd.lib.ExceptionHandable
{
    partial class CaughtExceptionsComponent
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
            this.ExceptionEventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionEventLog)).BeginInit();
            // 
            // ExceptionEventLog
            // 
            this.ExceptionEventLog.Log = "Application";
            this.ExceptionEventLog.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.ExceptionEventLog_EntryWritten);
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionEventLog)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog ExceptionEventLog;
    }
}
