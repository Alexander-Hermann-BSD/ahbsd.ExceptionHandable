using System;
using System.Collections;
using System.Collections.Generic;

namespace mbk.lib.ExceptionHandling
{
    /// <summary>
    /// Verallgemeinterte Sammlung von gefangenen Exceptions
    /// </summary>
    public interface ICaughtExceptions : IDictionary<Object, IDictionary<DateTime, Exception>>
    {
        /// <summary>
        /// Tritt auf, wenn ein neuer Sender hinzugefügt wurde.
        /// </summary>
        event EventHandler<EventArgs<object>> OnSenderAdded;
        /// <summary>
        /// Tritt auf, wenn eine neue Exception hinzugefügt wurde.
        /// </summary>
        event EventHandler<CaughtExceptionEventArgs> OnExceptionAdded;

        /// <summary>
        /// Gibt die Anzahl aller Exceptions zurück.
        /// </summary>
        /// <value>Die Anzahl aller Exceptions</value>
        int CountExceptions { get; }
        /// <summary>
        /// Gibt die Anzahl aller Sender zurück.
        /// </summary>
        /// <value>Die Anzahl aller Sender.</value>
        int CountSender { get; }
        /// <summary>
        /// Gibt die Liste der sendenden Objekte zurück.
        /// </summary>
        /// <value>Die Liste der sendenden Objekte.</value>
        IList Sender { get; }

        /// <summary>
        /// Gibt die Anzahl aller Exceptions pro definiertem Sender zurück.
        /// </summary>
        /// <param name="sender">Definierter Sender</param>
        /// <returns>Die Anzahl aller Exceptions</returns>
        int GetCountExceptionsBySender(object sender);
        /// <summary>
        /// Gibt die Liste der Sender zurück, die die übergebene Exception enthalten.
        /// </summary>
        /// <param name="ex">Die zu suchende Exception.</param>
        /// <returns>Die Liste der Sender zurück, die die übergebene Exception enthalten.</returns>
        IList GetSenderByException(Exception ex);

        /// <summary>
        /// Fügt einen Schlüssel und mehrere Werte zum Dictionary hinzu.
        /// </summary>
        /// <param name="key">Der Schlüssel.</param>
        /// <param name="value">Die Werte.</param>
        new void Add(object key, IDictionary<DateTime, Exception> value);

        /// <summary>
        /// Fügt einen Schlüssel und einen Wert zum Dictionary hinzu.
        /// </summary>
        /// <param name="key">Der Schlüssel.</param>
        /// <param name="value">Der Wert.</param>
        void Add(object key, KeyValuePair<DateTime, Exception> value);
    }
}