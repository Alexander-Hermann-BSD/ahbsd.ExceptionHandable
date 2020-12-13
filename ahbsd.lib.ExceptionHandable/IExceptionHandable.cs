using System;
using System.ComponentModel;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Interface for exception handlings
    /// </summary>
    public interface IExceptionHandable
    {
        /// <summary>
        /// Returns the caught exceptions.
        /// </summary>
        /// <value>The caught exceptions.</value>
        ICaughtExceptions CaughtExceptions { get; }
        /// <summary>
        /// Occures, if an Exception happens.
        /// </summary>
        /// <example>
        /// ...
        /// try
        /// {
        ///     ...
        /// }
        /// catch (Exception ex)
        /// {
        ///     OnException?.Invoke(this, new ExceptionEventArgs(ex));
        /// }
        /// ...
        /// </example>
        event EventHandler<ExceptionEventArgs> OnException;
    }

    /// <summary>
    /// Interface for Exception handable components.
    /// </summary>
    public interface IExceptionHandableComponent: IExceptionHandable, IComponent
    { }
}
