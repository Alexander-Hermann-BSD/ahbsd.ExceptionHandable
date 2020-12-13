using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Control, das eine Exception mit dem dazugehörigen Datum anzeigt, so wie den dazugehörigen Sender beinhaltet.
    /// </summary>
    public partial class ExceptionControl : UserControl, IExceptionControl
    {
        /// <summary>
        /// Anzahl der vorhandenen <see cref="ExceptionControl"/>s.
        /// </summary>
        /// <remarks>Wird für die Automatische Namensgebung benötigt.</remarks>
        private static uint ecNr;

        /// <summary>
        /// Statischer Konstruktor.
        /// </summary>
        static ExceptionControl()
        {
            ecNr = 0;
        }
        /// <summary>
        /// Konstruktor ohne Parameter
        /// </summary>
        public ExceptionControl()
        {
            InitializeComponent();

            Value = new KeyValuePair<DateTime, Exception>();

            ecNr++;
        }

        /// <summary>
        /// Konstruktor mit Angabe des Senders und der Exception incl. Auftritt-Zeit.
        /// </summary>
        /// <param name="sender">Sendendes Objekt</param>
        /// <param name="val">Exception incl. Auftritt-Zeit</param>
        public ExceptionControl(object sender, KeyValuePair<DateTime, Exception> val)
        {
            InitializeComponent();
            Value = val;
            Sender = sender;

            ecNr++;
        }

        #region Implementierung von IExceptionControl
        /// <summary>
        /// Gibt die Exception und die Auftritt-Zeit zurück.
        /// </summary>
        /// <value>Die Exception und die Auftritt-Zeit.</value>
        public KeyValuePair<DateTime, Exception> Value { get; internal set; }

        /// <summary>
        /// Gibt den Sender zurück.
        /// </summary>
        /// <value>Der Sender</value>
        public object Sender { get; internal set; }

        /// <summary>
        /// Gibt die Textbox mit den Details zurück.
        /// </summary>
        /// <value>Die Textbox mit den Details.</value>
        public TextBox Details { get; internal set; }
        #endregion

        private void ExceptionControl_Load(object sender, EventArgs e)
        {
            string tmpName;
            if (lblZeitpunkt != null)
            {
                lblZeitpunkt.Text = Value.Key.ToString();
            }


            if (Value.Value != null)
            {
                lblExceptionType.Text = Value.Value.GetType().ToString();
                lblMessage.Text = Value.Value.Message;

                tmpName = string.Format("ec{0}_{1}_{2}", ecNr, Sender, Value.Value.GetType());
                
            }
            else
            {
                tmpName = string.Format("ec{0}", ecNr);
            }

            Name = tmpName;
            Tag = Sender;
        }

        private void ExceptionControl_Enter(object sender, EventArgs e)
        {
            if (Details != null && Value.Value != null)
            {
                Details.Text = Value.Value.StackTrace;
            }
        }

        private void ExceptionControl_Leave(object sender, EventArgs e)
        {
            if (Details != null)
            {
                Details.Text = string.Empty;
            }
        }
    }
}
