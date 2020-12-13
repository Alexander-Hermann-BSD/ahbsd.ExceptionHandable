using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Interface für ExceptionSenderControl.
    /// </summary>
    /// <remarks>z.B.: <see cref="ExceptionSenderControl"/>.</remarks>
    public interface IExceptionSenderControl
    {
        /// <summary>
        /// Gibt die Textbox mit den Details zurück oder setzt sie.
        /// </summary>
        /// <value>Die Textbox mit den Details.</value>
        TextBox Details { get; set; }
        /// <summary>
        /// Gibt das sendende <see cref="object"/> der Exceptions zurück oder setzt es.
        /// </summary>
        /// <value>Das sendende <see cref="object"/> der Exceptions.</value>
        object Sender { get; set; }
        /// <summary>
        /// Gibt das <see cref="IDictionary{DateTime, Exception}"/>, dass alle aufgetretene Exceptions des <see cref="Sender"/>-Objekts beinhaltet zurück oder setzt es.
        /// </summary>
        /// <value>Das <see cref="IDictionary{DateTime, Exception}"/>, dass alle aufgetretene Exceptions des <see cref="Sender"/>-Objekts beinhaltet.</value>
        IDictionary<DateTime, Exception> Values { get; set; }

        /// <summary>
        /// Tritt auf, wenn <see cref="Details"/> geändert wurde.
        /// </summary>
        event EventHandler<ChangeEventArgs<TextBox>> OnDetailsChanged;
        /// <summary>
        /// Tritt auf, wenn <see cref="Sender"/> geändert wurde.
        /// </summary>
        event EventHandler<ChangeEventArgs<object>> OnSenderChanged;

        /// <summary>
        /// Gibt den maximalen Y-Wert zurück.
        /// </summary>
        /// <value>Der maximale Y-Wert.</value>
        int MaxY { get; }
    }
}