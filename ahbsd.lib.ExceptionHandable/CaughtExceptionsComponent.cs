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

        /// <summary>
        /// Wird aufgerufen, wenn eine neue Exception hinzugefügt wurde.
        /// </summary>
        /// <param name="sender">Das sendende Objekt.</param>
        /// <param name="e">Die Event-Argumente mit der neuen Exception.</param>
        private void CaughtExceptions_OnExceptionAdded(object sender, ExceptionEventArgs e)
        {
            ExceptionAdded?.Invoke(sender, e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Sender hinzugefügt wurde.
        /// </summary>
        /// <param name="sender">Das Sendende Objekt.</param>
        /// <param name="e">Das Event-Argument mit dem neuen Sender.</param>
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
            OnExceptionAdded?.Invoke(sender, e);

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
                OnExceptionAdded?.Invoke(this, exe);
            }
            else
            {
                Dictionary<DateTime, Exception> nd = new Dictionary<DateTime, Exception>();
                EventArgs<object> eo = new EventArgs<object>(this);
                nd.Add(DateTime.Now, ex);
                CaughtExceptions.Add(this, nd);
                SenderAdded?.Invoke(this, eo);
                ExceptionAdded?.Invoke(this, exe);
                OnExceptionAdded?.Invoke(this, exe);
            }
        }

        /// <summary>
        /// Fügt Exception-Event-Argumente hinzu.
        /// </summary>
        /// <param name="easea">Exception-Event-Argumente</param>
        public void AddExceptionEventArgs(ExceptionAndSenderEventArgs easea)
        {
            if (CaughtExceptions.Keys.Contains(easea.Sender))
            {
                CaughtExceptions[easea.Sender].Add(DateTime.Now, easea.Value);
            }
            else
            {
                Dictionary<DateTime, Exception> nd = new Dictionary<DateTime, Exception>();
                EventArgs<object> eo = new EventArgs<object>(easea.Sender);
                nd.Add(DateTime.Now, easea.Value);
                CaughtExceptions.Add(easea.Sender, nd);
                SenderAdded?.Invoke(easea.Sender, eo);
            }

            ExceptionAdded?.Invoke(easea.Sender, easea);
            OnExceptionAdded?.Invoke(easea.Sender, easea);
        }

        /// <summary>
        /// Fügt einen Sender und ein Dictionary mit gefallenen Exceptions hinzu.
        /// </summary>
        /// <param name="key">Sendendes Objekt</param>
        /// <param name="value">Dictionary mit Exceptions.</param>
        public void Add(object key, IDictionary<DateTime, Exception> value)
        {
            ExceptionEventArgs e;
            EventArgs<object> eas;

            if (CaughtExceptions.Keys.Contains(key))
            {
                foreach (KeyValuePair<DateTime, Exception> item in value)
                {
                    if (!CaughtExceptions[key].Keys.Contains(item.Key))
                    {
                        e = new ExceptionEventArgs(item.Value);
                        CaughtExceptions[key].Add(item);
                        ExceptionAdded?.Invoke(key, e);
                        OnExceptionAdded?.Invoke(key, e);
                    }
                }
            }
            else
            {
                eas = new EventArgs<object>(key);
                CaughtExceptions.Add(key, value);
                SenderAdded?.Invoke(key, eas);
                OnSenderAdded?.Invoke(key, eas);

                foreach (var item in value)
                {
                    e = new ExceptionEventArgs(item.Value);
                    ExceptionAdded?.Invoke(key, e);
                    OnExceptionAdded?.Invoke(key, e);
                }
            }
        }

        /// <summary>
        /// Gibt zurück, ob der Schlüssel vorhanden ist.
        /// </summary>
        /// <param name="key">Der Schlüssel.</param>
        /// <returns><c>TRUE</c>, falls vorhanden, ansonsten <c>FALSE</c>.</returns>
        public bool ContainsKey(object key) => CaughtExceptions.Keys.Contains(key);
        /// <summary>
        /// Versucht den Schlüssel und seinen Wert zu entfernen.
        /// </summary>
        /// <param name="key">Der Schlüssel.</param>
        /// <returns><c>TRUE</c> falls erfolgreich, ansonsten <c>FALSE</c>.</returns>
        public bool Remove(object key) => CaughtExceptions.Remove(key);
        /// <summary>
        /// Versucht den Wert eines bestimmten Schlüssels auszugeben.
        /// </summary>
        /// <param name="key">Der Bestimmte Schlüssel.</param>
        /// <param name="value">Der Wert des Bestimmten Schlüssels.</param>
        /// <returns>Bei Erfolg <c>TRUE</c>, ansonsten <c>FALSE</c>.</returns>
        public bool TryGetValue(object key, out IDictionary<DateTime, Exception> value) => CaughtExceptions.TryGetValue(key, out value);
        /// <summary>
        /// Fügt Daten hinzu.
        /// </summary>
        /// <param name="item">Hinzuzufügende Daten.</param>
        public void Add(KeyValuePair<object, IDictionary<DateTime, Exception>> item) => CaughtExceptions.Add(item);
        /// <summary>
        /// Löscht den gesamten Inhalt.
        /// </summary>
        /// <exception cref="NotSupportedException">Falls da irgendwas nicht unterstützt wird.</exception>
        public void Clear() => CaughtExceptions.Clear();
        /// <summary>
        /// Gibt zurück, ob eine Zusammensetzung aus bestimmten Daten vorhanden sind.
        /// </summary>
        /// <param name="item">Eine Zusammensetzung aus bestimmten Daten.</param>
        /// <returns><c>TRUE</c> bei Erfolg, ansonsten <c>FALSE</c>.</returns>
        public bool Contains(KeyValuePair<object, IDictionary<DateTime, Exception>> item) => CaughtExceptions.Contains(item);
        /// <summary>
        /// Kopiert Bestimmte Daten ab einem bestimmten Index hinzu.
        /// </summary>
        /// <param name="array">Bestimmte Daten.</param>
        /// <param name="arrayIndex">Bestimmter Index.</param>
        public void CopyTo(KeyValuePair<object, IDictionary<DateTime, Exception>>[] array, int arrayIndex) => CaughtExceptions.CopyTo(array, arrayIndex);
        /// <summary>
        /// Entfernt einen bestimmten Schlüssel mit Inhalt.
        /// </summary>
        /// <param name="item">Der Bestimmte Schlüssel und sein Inhalt.</param>
        /// <returns><c>TRUE</c> bei Erfolg, ansonsten <c>FALSE</c>.</returns>
        public bool Remove(KeyValuePair<object, IDictionary<DateTime, Exception>> item) => CaughtExceptions.Remove(item);
        /// <summary>
        /// Gibt den Enumerator für die enthaltenen Werte Zurück.
        /// </summary>
        /// <returns>Der Enumerator für die enthaltenen Werte.</returns>
        public IEnumerator<KeyValuePair<object, IDictionary<DateTime, Exception>>> GetEnumerator() => CaughtExceptions.GetEnumerator();
        /// <summary>
        /// Gibt den Enumerator für die enthaltenen Werte Zurück.
        /// </summary>
        /// <returns>Der Enumerator für die enthaltenen Werte.</returns>
        IEnumerator IEnumerable.GetEnumerator() => CaughtExceptions.GetEnumerator();
        #endregion

        /// <summary>
        /// Tritt auf, wenn ein Eintrag ins Event-Log geschrieben wurde.
        /// </summary>
        /// <param name="sender">Sendendes Objekt.</param>
        /// <param name="e">Event-Argumente bzgl. des geschriebenen Eintrags ins Event-Log.</param>
        private void ExceptionEventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
            EntryWritten?.Invoke(sender, e);
        }

        /// <summary>
        /// Gibt den Inhalt als Form zurück.
        /// </summary>
        /// <value>Der Inhalt als Form.</value>
        public Form Details => new FrmCaughtExceptions(CaughtExceptions);
        /// <summary>
        /// Gibt alle Schlüssel zurück.
        /// </summary>
        /// <value>Alle Schlüssel.</value>
        public ICollection<object> Keys => CaughtExceptions.Keys;
        /// <summary>
        /// Gibt alle Werte zurück.
        /// </summary>
        /// <value>Alle Werte.</value>
        public ICollection<IDictionary<DateTime, Exception>> Values => CaughtExceptions.Values;
        /// <summary>
        /// Gibt die Anzahl aller Quellen zurück.
        /// </summary>
        /// <value>Die Anzahl aller Quellen.</value>
        public int Count => CaughtExceptions.Count;
        /// <summary>
        /// Gibt zurück, ob der Inhalt nur gelesen werden kann.
        /// </summary>
        /// <value><c>TRUE</c> wenn der Inhalt nur gelesen werden kann, ansonsten <c>FALSE</c>.</value>
        public bool IsReadOnly => CaughtExceptions.IsReadOnly;

        /// <summary>
        /// Gibt die Liste der gefangenen Exceptions eines bestimmten Senders zurück
        /// </summary>
        /// <param name="key">Der bestimmte Sender</param>
        /// <returns>Die Liste der gefangenen Exceptions eines bestimmten Senders.</returns>
        public IDictionary<DateTime, Exception> this[object key]
        {
            get => CaughtExceptions[key];
            set { } // Absichtlich ohne Funktion.
        }
    }
}
