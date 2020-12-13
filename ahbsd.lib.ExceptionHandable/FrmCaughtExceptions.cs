using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Formular für die Anzeige von gefangenenen <see cref="Exception"/>s
    /// </summary>
    public partial class FrmCaughtExceptions : Form
    {
        /// <summary>
        /// Die gefangenen Exceptions.
        /// </summary>
        /// <remarks>Siehe <see cref="CaughtExceptions"/>.</remarks>
        private ICaughtExceptions _ce;

        private int pnSenderMaxInnerHeight;

        /// <summary>
        /// Liste der Exception-Sender
        /// </summary>
        protected List<IExceptionSenderControl> exceptionSenders;

        /// <summary>
        /// Gibt die gefangenen Exceptions zurück oder setzt sie.
        /// </summary>
        /// <value>Die gefangenen Exceptions.</value>
        public ICaughtExceptions CE
        {
            get { return _ce; }
            set
            {
                if (value != null && !value.Equals(_ce))
                {
                    _ce = value;

                    FrmCaughtExceptions_Load(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Konstruktor ohne Parameter
        /// </summary>
        public FrmCaughtExceptions()
        {
            InitializeComponent();

            exceptionSenders = new List<IExceptionSenderControl>();
        }

        /// <summary>
        /// Konstruktor mit der Übergabe von gefangenen Exceptions.
        /// </summary>
        /// <param name="ce">Die gefangenen Exceptions.</param>
        public FrmCaughtExceptions(ICaughtExceptions ce)
        {
            InitializeComponent();

            exceptionSenders = new List<IExceptionSenderControl>(ce.Count);
            _ce = ce;
        }


        /// <summary>
        /// Laden von Daten
        /// </summary>
        /// <param name="sender">Sendendes Objekt.</param>
        /// <param name="e">Event-Argumente.</param>
        private void FrmCaughtExceptions_Load(object sender, EventArgs e)
        {
            exceptionSenders.Clear();
            pnSender.Controls.Clear();
            pnSenderMaxInnerHeight = 0;
            ExceptionSenderControl tmpEsc;

            foreach (KeyValuePair<object, IDictionary<DateTime, Exception>> item in _ce)
            {
                tmpEsc = new ExceptionSenderControl(item.Key, item.Value);
                tmpEsc.Details = txtDetail;
                tmpEsc.OnSenderChanged += TmpEsc_OnSenderChanged;
                tmpEsc.OnDetailsChanged += TmpEsc_OnDetailsChanged;
                exceptionSenders.Add(tmpEsc);
                pnSender.Controls.Add(tmpEsc);
            }
        }

        private void TmpEsc_OnDetailsChanged(object sender, ChangeEventArgs<TextBox> e)
        {
            
        }

        private void TmpEsc_OnSenderChanged(object sender, ChangeEventArgs<object> e)
        {
            
        }

        private void pnSender_SizeChanged(object sender, EventArgs e)
        {
            int width = pnSender.Width;
            
            foreach (Control tmpControl in pnSender.Controls)
            {
                tmpControl.Width = width;
            }            
        }

        private void pnSender_ControlAdded(object sender, ControlEventArgs e)
        {
            Control addedControl = e.Control;

            addedControl.Left = 0;
            addedControl.Width = pnSender.Width;

            addedControl.Top = pnSenderMaxInnerHeight;

            pnSenderMaxInnerHeight += addedControl.Height;
        }
    }
}
