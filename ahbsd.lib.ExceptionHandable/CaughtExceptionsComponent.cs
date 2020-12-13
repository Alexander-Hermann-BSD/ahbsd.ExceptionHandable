using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Komponente für gefangene Exceptions.
    /// </summary>
    public partial class CaughtExceptionsComponent : Component, ISupportInitialize, ICaughtExceptionsComponent
    {
        /// <summary>
        /// Gibt zurück, ob sich das Objekt gerade im Initialisierungsmodus befindet.
        /// </summary>
        /// <value>Befindet sich das Objekt gerade im Initialisierungsmodus?</value>
        protected bool InInitialization { get; private set; }

        #region Implementierung von ISupportInitialize
        /// <summary>
        /// Signalisiert dem Objekt den Start der Initialisierung.
        /// </summary>
        public void BeginInit()
        {
            InInitialization = true;
        }

        /// <summary>
        /// Signalisiert dem Objekt den Abschluss der Initialisierung.
        /// </summary>
        public void EndInit()
        {
            InInitialization = false;
        }
        #endregion

        /// <summary>
        /// Konstruktor ohne Parameter
        /// </summary>
        public CaughtExceptionsComponent()
        {
            InitializeComponent();

            CaughtExceptions = new CaughtExceptions();

            CaughtExceptions.OnSenderAdded += CaughtExceptions_OnSenderAdded;
            CaughtExceptions.OnExceptionAdded += CaughtExceptions_OnExceptionAdded;
        }

        /// <summary>
        /// Konstruktor mit Angabe eines Kontainers.
        /// </summary>
        /// <param name="container">Kontainer, der dieses Objekt enthält.</param>
        public CaughtExceptionsComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            CaughtExceptions = new CaughtExceptions();

            CaughtExceptions.OnSenderAdded += CaughtExceptions_OnSenderAdded;
            CaughtExceptions.OnExceptionAdded += CaughtExceptions_OnExceptionAdded;
        }

        private void CaughtExceptions_OnExceptionAdded(object sender, ExceptionEventArgs e)
        {
            ExceptionAdded?.Invoke(sender, e);
        }

        private void CaughtExceptions_OnSenderAdded(object sender, EventArgs<object> e)
        {
            SenderAdded?.Invoke(CaughtExceptions, new EventArgs<object>(sender));
        }

        #region Implementierung von ICaughtExceptionsComponent
        /// <summary>
        /// Tritt auf, wenn ein neuer Sender hinzugefügt wurde.
        /// </summary>
        public event EventHandler<EventArgs<Object>> SenderAdded;
        /// <summary>
        /// Tritt auf, wenn eine neue Exception hinzugefügt wurde.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> ExceptionAdded;

        /// <summary>
        /// Tritt beim Schreiben eines Eintrags in das Ereignisprotokoll auf dem lokalen Computer ein.
        /// </summary>
        public event EntryWrittenEventHandler EntryWritten;
        /// <summary>
        /// Tritt auf, wenn ein neuer Sender hinzugefügt wurde.
        /// </summary>
        public event EventHandler<EventArgs<object>> OnSenderAdded;


        /// <summary>
        /// Occures, if a new <see cref="Exception"/> is added.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> OnExceptionAdded;

        /// <summary>
        /// Gibt die gefangenen Exceptions zurück oder setzt sie.
        /// </summary>
        /// <value>Die gefangenen Exceptions</value>
        public ICaughtExceptions CaughtExceptions { get; set; }


        /// <summary>
        /// Setz oder gibt zurück, ob Logeinträge gesetzt werden sollen.
        /// </summary>
        /// <value>Sollen Logeinträge gesetzt werden?</value>
        public bool EnableLogentry { get; set; }

        /// <summary>
        /// Setzt oder gibt zurück, ob das <see cref="EventLog"/> <see cref="EventLog.EntryWritten"/> Event-Benachrichtigungen empfängt.
        /// </summary>
        /// <value>Soll das <see cref="EventLog"/> <see cref="EventLog.EntryWritten"/> Event-Benachrichtigungen empfangen?</value>
        /// <exception cref="InvalidOperationException">Das Ereignisprotokoll wird auf einem Remotecomputer gespeichert.</exception>
        public bool EnableRaisingEvents
        {
            get
            {
                return ExceptionEventLog.EnableRaisingEvents;
            }
            set
            {
                if (ExceptionEventLog != null)
                {
                    ExceptionEventLog.EnableRaisingEvents = value;
                }
            }
        }
        /// <summary>
        /// Ruft den Inhalt des Ereignisprotokolls ab.
        /// </summary>
        /// <value>Der Inhalt des Ereignisprotokolls.</value>
        public EventLogEntryCollection Entries => ExceptionEventLog.Entries;

        /// <summary>
        /// Ruft den Namen des Protokolls ab, aus dem gelesen bzw. in das geschrieben werden soll, oder legt diesen fest.
        /// </summary>
        /// <value>
        /// Der Name des Protokolls.
        /// Dies kann das Anwendungs-, das System- bzw. das Sicherheitsprotokoll oder ein benutzerdefinierter Protokollname sein.
        /// Der Standardwert ist eine leere Zeichenfolge ("").
        /// </value>
        public string Log
        {
            get
            {
                return ExceptionEventLog.Log;
            }

            set
            {
                if ((value != null) && !value.Equals(ExceptionEventLog.Log))
                {
                    ExceptionEventLog.Log = value;
                }
            }
        }

        /// <summary>
        /// Ruft den angezeigten Namen des Ereignisprotokolls ab.
        /// </summary>
        /// <value>Ein Name, der das Ereignisprotokoll in der Ereignisanzeige des Systems darstellt.</value>
        /// <exception cref="InvalidOperationException">Das angegebene <see cref="EventLog.Log"/> ist in der Registrierung für diesen Computer nicht vorhanden.</exception>
        public string LogDisplayName => ExceptionEventLog.LogDisplayName;

        /// <summary>
        /// Ruft den zu registrierenden Namen der Quelle ab, die zum Schreiben in das Ereignisprotokoll verwendet werden soll, oder legt diesen fest.
        /// </summary>
        /// <value>Der Name, der im Ereignisprotokoll als Quelle für Einträge registriert ist.Der Standardwert ist eine leere Zeichenfolge ("").</value>
        /// <exception cref="ArgumentException">Der Quellenname führt dazu, dass der Registrierungsschlüsselpfad länger als 254 Zeichen ist.</exception>
        public string Source
        {
            get { return ExceptionEventLog.Source; }
            set
            {
                if (!value.Equals(ExceptionEventLog.Source))
                {
                    ExceptionEventLog.Source = value;
                }
            }
        }

        /// <summary>
        /// Fügt eine gefangene Exception von einem bestimmten Objekt hinzu.
        /// </summary>
        /// <param name="sender">Sendendes Objekt</param>
        /// <param name="e">Event-Argumente mit einer gefangenen Exception</param>
        public void AddExceptionEventArgs(Object sender, ExceptionEventArgs e)
        {
            AddExceptionEventArgs(sender, e, true);
        }

        /// <summary>
        /// Fügt eine gefangene Exception von einem bestimmten Objekt hinzu. 
        /// </summary>
        /// <param name="sender">Sendendes Objekt</param>
        /// <param name="e">Event-Argumente mit einer gefangenen Exception</param>
        /// <param name="writeToLogentry">Soll versucht werden in das Logbuch einzutragen?</param>
        protected void AddExceptionEventArgs(object sender, ExceptionEventArgs e, bool writeToLogentry)
        {
            string fmtMessage = "Ein \"{0}\" ist am {1} aufgetreten:\r\nMessage: {2}\r\nStackTrace:\r\n{3}";
            Exception ex = e.Value;
            DateTime now = DateTime.Now;
            bool IsLogNotEmpty = !ExceptionEventLog.Log.Trim().Equals(string.Empty);

            if (CaughtExceptions.Keys.Contains(sender))
            {
                CaughtExceptions[sender].Add(now, ex);
                ExceptionAdded?.Invoke(sender, e);
            }
            else
            {
                Dictionary<DateTime, Exception> nd = new Dictionary<DateTime, Exception>();
                EventArgs<object> eo = new EventArgs<object>(sender);
                nd.Add(now, ex);
                CaughtExceptions.Add(sender, nd);
                SenderAdded?.Invoke(sender, eo);
                ExceptionAdded?.Invoke(sender, e);
            }
            if (writeToLogentry && EnableLogentry && IsLogNotEmpty)
            {
                try
                {
                    ExceptionEventLog.WriteEntry(string.Format(fmtMessage, ex.GetType(), now, ex.Message, ex.StackTrace), EventLogEntryType.Error, 1, 1, ex.Data.Cast<byte>().ToArray<byte>());
                }
                catch (Exception ex2)
                {
                    AddExceptionEventArgs(this, new ExceptionEventArgs(ex2), false);
                }
            }
        }

        /// <summary>
        /// Fügt eine gefangene Exception von einem bestimmten Objekt hinzu.
        /// </summary>
        /// <param name="sender">Sendendes Objekt</param>
        /// <param name="ex">Gefangenene Exception</param>
        public void AddException(object sender, Exception ex)
        {
            ExceptionEventArgs exe = new ExceptionEventArgs(ex);

            AddExceptionEventArgs(sender, exe);
        }

        /// <summary>
        /// Fügt eine gefangene Exception hinzu.
        /// </summary>
        /// <param name="ex">Gefangenene Exception</param>
        protected void AddException(Exception ex)
        {
            ExceptionEventArgs exe = new ExceptionEventArgs(ex);

            if (CaughtExceptions.Keys.Contains(this))
            {
                CaughtExceptions[this].Add(DateTime.Now, ex);
                ExceptionAdded?.Invoke(this, exe);
            }
            else
            {
                Dictionary<DateTime, Exception> nd = new Dictionary<DateTime, Exception>();
                EventArgs<object> eo = new EventArgs<object>(this);
                nd.Add(DateTime.Now, ex);
                CaughtExceptions.Add(this, nd);
                SenderAdded?.Invoke(this, eo);
                ExceptionAdded?.Invoke(this, exe);
            }
        }

        public void AddExceptionEventArgs(ExceptionAndSenderEventArgs easea)
        {
            throw new NotImplementedException();
        }

        public void Add(object key, IDictionary<DateTime, Exception> value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(object key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(object key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(object key, out IDictionary<DateTime, Exception> value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<object, IDictionary<DateTime, Exception>> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<object, IDictionary<DateTime, Exception>> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<object, IDictionary<DateTime, Exception>>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<object, IDictionary<DateTime, Exception>> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<object, IDictionary<DateTime, Exception>>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion

        private void ExceptionEventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
            EntryWritten?.Invoke(sender, e);
        }

        /// <summary>
        /// Gibt den Inhalt als Form zurück.
        /// </summary>
        /// <value>Der Inhalt als Form.</value>
        public Form Details
        {
            get
            {
                FrmCaughtExceptions result = new FrmCaughtExceptions(CaughtExceptions);
                return result;
            }
        }

        public ICollection<object> Keys => throw new NotImplementedException();

        public ICollection<IDictionary<DateTime, Exception>> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public IDictionary<DateTime, Exception> this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
