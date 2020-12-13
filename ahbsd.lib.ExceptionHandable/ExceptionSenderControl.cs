using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Control für Exception Sender
    /// </summary>
    public partial class ExceptionSenderControl : UserControl, IExceptionSenderControl
    {
        /// <summary>
        /// Die Anzahl aller aktuellen <see cref="ExceptionSenderControl"/>s.
        /// </summary>
        /// <remarks>Wird vor Allem zur Automatischen Namens-Gebung benötigt.</remarks>
        private static uint escNr;

        /// <summary>
        /// <see cref="Details"/> als Variable, damit Changes ein Event auslösen können.
        /// </summary>
        private TextBox detailBox;
        /// <summary>
        /// Das <see cref="Sender"/>-<see cref="object"/> als Variable, damit Changes <see cref="Values"/> leert und ein Event auslösen kann.
        /// </summary>
        private object sender;

        /// <summary>
        /// Beinhaltete <see cref="ExceptionControl"/>s.
        /// </summary>
        protected List<ExceptionControl> exceptionControls;

        #region Implementierung von IExceptionSenderControl

        /// <summary>
        /// Tritt auf, wenn <see cref="Details"/> geändert wurde.
        /// </summary>
        public event EventHandler<ChangeEventArgs<TextBox>> OnDetailsChanged;
        /// <summary>
        /// Tritt auf, wenn <see cref="Sender"/> geändert wurde.
        /// </summary>
        public event EventHandler<ChangeEventArgs<object>> OnSenderChanged;

        /// <summary>
        /// Gibt das sendende <see cref="object"/> der Exceptions zurück oder setzt es.
        /// </summary>
        /// <value>Das sendende <see cref="object"/> der Exceptions.</value>
        public Object Sender
        {
            get { return sender; }
            set
            {
                ChangeEventArgs<object> senderChange = new ChangeEventArgs<object>(sender, value);

                if (value != null && !value.Equals(sender))
                {
                    sender = value;
                    if (Values != null)
                    {
                        Values.Clear();
                    }

                    if (exceptionControls != null)
                    {
                        exceptionControls.Clear();
                    }
                    
                    OnSenderChanged?.Invoke(this, senderChange);
                }
            }
        }
        /// <summary>
        /// Gibt das <see cref="IDictionary{DateTime, Exception}"/>, dass alle aufgetretene Exceptions des <see cref="Sender"/>-Objekts beinhaltet zurück oder setzt es.
        /// </summary>
        /// <value>Das <see cref="IDictionary{DateTime, Exception}"/>, dass alle aufgetretene Exceptions des <see cref="Sender"/>-Objekts beinhaltet.</value>
        public IDictionary<DateTime, Exception> Values { get; set; }

        /// <summary>
        /// Gibt die Textbox mit den Details zurück oder setzt sie.
        /// </summary>
        /// <value>Die Textbox mit den Details.</value>
        public TextBox Details
        {
            get { return detailBox; }
            set
            {
                TextBox old = detailBox;

                if (value != null && !value.Equals(detailBox))
                {
                    detailBox = value;

                    OnDetailsChanged(this, new ChangeEventArgs<TextBox>(old, value));
                }
            }
        }

        /// <summary>
        /// Gibt den maximalen Y-Wert zurück.
        /// </summary>
        /// <value>Der maximale Y-Wert.</value>
        public int MaxY { get; private set; }
        #endregion

        /// <summary>
        /// Statischer Konstruktor.
        /// </summary>
        static ExceptionSenderControl()
        {
            escNr = 0;
        }

        /// <summary>
        /// Konstruktor ohne Parameter
        /// </summary>
        public ExceptionSenderControl()
        {
            escNr++;
            InitializeComponent();

            Name = string.Format("esc{0}", escNr);
            Tag = Name;

            exceptionControls = new List<ExceptionControl>();

            OnDetailsChanged += ExceptionSenderControl_OnDetailsChanged;
        }

        /// <summary>
        /// Konstruktor mit Angabe des sendenden Objekts und den dazugehörigen Exceptions.
        /// </summary>
        /// <param name="sender">Das sendende Objekt.</param>
        /// <param name="values">Die dazugehörigen Exceptions.</param>
        public ExceptionSenderControl(object sender, IDictionary<DateTime, Exception> values)
        {
            Control tmpSender;
            ExceptionControl tmpEC;
            escNr++;
            InitializeComponent();
            Sender = sender;
            Values = values;
            MaxY = 0;

            if (Sender != null)
            {
                try
                {
                    tmpSender = (Control)sender;
                }
                catch (Exception)
                {
                    tmpSender = null;
                }
                if (tmpSender != null)
                {
                    Name = String.Format("esc{2}_{0}_{1}", tmpSender.GetType(), tmpSender.Name, escNr);
                }
                else
                {
                    Name = String.Format("esc{1}_{0}", Sender.GetType(), escNr);
                }

                Tag = sender.GetType();
            }

            

            if (Sender != null)
            {
                txtSenderType.Text = Sender.GetType().ToString();

                if (Values != null)
                {
                    exceptionControls = new List<ExceptionControl>(Values.Count);
                    foreach (KeyValuePair<DateTime, Exception> item in Values)
                    {
                        tmpEC = new ExceptionControl(Sender, item);
                        exceptionControls.Add(tmpEC);
                    }
                }
                else
                {
                    exceptionControls = new List<ExceptionControl>();
                }
            }
            else
            {
                exceptionControls = new List<ExceptionControl>();
            }

            OnDetailsChanged += ExceptionSenderControl_OnDetailsChanged;
        }

        private void ExceptionSenderControl_OnDetailsChanged(object sender, ChangeEventArgs<TextBox> e)
        {
            foreach (ExceptionControl ec in exceptionControls)
            {
                if (ec.Details == null || ec.Details.Equals(e.OldValue))
                {
                    ec.Details = e.NewValue;
                }
            }
        }

        private void ExceptionSenderControl_Load(object sender, EventArgs e)
        {
            if (Sender != null)
            {
                txtSenderType.Text = Sender.GetType().ToString();

                pnExceptions.Controls.Clear();
                MaxY = 0;

                foreach (ExceptionControl ec in exceptionControls)
                {
                    ec.Top = MaxY;
                    pnExceptions.Controls.Add(ec);
                    MaxY += ec.Height;
                }
            }
        }
    }
}
