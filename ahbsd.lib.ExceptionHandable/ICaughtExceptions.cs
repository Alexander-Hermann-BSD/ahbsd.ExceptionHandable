using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Interface for a way to hold caught exceptions
    /// </summary>
    public interface ICaughtExceptions : IDictionary<object, IDictionary<DateTime,Exception>>
    {
        /// <summary>
        /// Adds a caught exception and the sending object.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The caught Exception.</param>
        void AddException(object sender, Exception e);
        /// <summary>
        /// Adds an Exception in an <see cref="ExceptionEventArgs"/> and the sending object.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="eea">The caught exception in EventArgs.</param>
        void AddExceptionEventArgs(object sender, ExceptionEventArgs eea);
        /// <summary>
        /// Adds an exception and the sending object in an <see cref="ExceptionAndSenderEventArgs"/>.
        /// </summary>
        /// <param name="easea">The exception and the sending object.</param>
        void AddExceptionEventArgs(ExceptionAndSenderEventArgs easea);

        /// <summary>
        /// Occures, if a new sender is added.
        /// </summary>
        event EventHandler<EventArgs<object>> OnSenderAdded;
        /// <summary>
        /// Occures, if a new <see cref="Exception"/> is added.
        /// </summary>
        event EventHandler<ExceptionEventArgs> OnExceptionAdded;
    }

    /// <summary>
    /// Interface for a <see cref="ICaughtExceptions"/>-Component.
    /// </summary>
    public interface ICaughtExceptionsComponent : ICaughtExceptions, IComponent
    { }

    /// <summary>
    /// Event argument, that contains a fallen Exception
    /// </summary>
    public class ExceptionEventArgs : EventArgs<Exception>
    {

        /// <summary>
        /// Constructure with a caught exception.
        /// </summary>
        /// <param name="e">The caught exception</param>
        public ExceptionEventArgs(Exception e)
            : base(e)
        { }
    }

    /// <summary>
    /// Event argument with a caught exception and the sending object.
    /// </summary>
    public class ExceptionAndSenderEventArgs : ExceptionEventArgs
    {
        /// <summary>
        /// Returns the sending object.
        /// </summary>
        /// <value>The sending object.</value>
        public object Sender { get; private set; }

        /// <summary>
        /// Constructor with the sending object and the caught exception.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The caught exception.</param>
        public ExceptionAndSenderEventArgs(object sender, Exception e)
            : base(e)
        {
            Sender = sender;
        }
    }

    /// <summary>
    /// Caught Exceptions
    /// </summary>
    public class CaughtExceptions : Dictionary<object, IDictionary<DateTime, Exception>>, ICaughtExceptions
    {
        #region implementation of ICaughtExceptions
        /// <summary>
        /// Adds a caught exception and the sending object.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The caught Exception.</param>
        public void AddException(object sender, Exception e)
        {
            EventArgs<object> eao;
            DateTime now = DateTime.Now;
            if (!Keys.Contains(sender))
            {
                eao = new EventArgs<object>(sender);
                Add(sender, new Dictionary<DateTime, Exception>());
                OnSenderAdded?.Invoke(this, eao);
            }

            this[sender].Add(now, e);
        }
        /// <summary>
        /// Adds an Exception in an <see cref="ExceptionEventArgs"/> and the sending object.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="eea">The caught exception in EventArgs.</param>
        public void AddExceptionEventArgs(object sender, ExceptionEventArgs eea)
        {
            AddException(sender, eea.Value);
            OnExceptionAdded?.Invoke(sender, eea);
        }
        /// <summary>
        /// Adds an exception and the sending object in an <see cref="ExceptionAndSenderEventArgs"/>.
        /// </summary>
        /// <param name="easea">The exception and the sending object.</param>
        public void AddExceptionEventArgs(ExceptionAndSenderEventArgs easea)
        {
            AddException(easea.Sender, easea.Value);
            OnExceptionAdded?.Invoke(easea.Sender, easea);
        }


        /// <summary>
        /// Occures, if a new sender is added.
        /// </summary>
        public event EventHandler<EventArgs<object>> OnSenderAdded;

        /// <summary>
        /// Occures, if a new <see cref="Exception"/> is added.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> OnExceptionAdded;
        #endregion
    }
}
