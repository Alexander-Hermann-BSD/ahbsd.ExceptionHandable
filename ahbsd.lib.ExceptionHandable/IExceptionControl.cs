using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Interface, dass den Aufbau einer ExceptionControl deklariert.
    /// </summary>
    public interface IExceptionControl
    {
        /// <summary>
        /// Gibt die Textbox mit den Details zurück.
        /// </summary>
        /// <value>Die Textbox mit den Details.</value>
        TextBox Details { get; }
        /// <summary>
        /// Gibt den Sender zurück.
        /// </summary>
        /// <value>Der Sender</value>
        object Sender { get; }
        /// <summary>
        /// Gibt die Exception und die Auftritt-Zeit zurück.
        /// </summary>
        /// <value>Die Exception und die Auftritt-Zeit.</value>
        KeyValuePair<DateTime, Exception> Value { get; }
    }
}