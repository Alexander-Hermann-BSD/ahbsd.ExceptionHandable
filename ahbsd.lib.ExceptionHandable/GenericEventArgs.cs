using System;

namespace ahbsd.lib
{
    /// <summary>
    /// Generic event arguments.
    /// </summary>
    /// <typeparam name="T">The given class.</typeparam>
    public class EventArgs<T> : EventArgs
    {
        /// <summary>
        /// Constructor with a value of type T.
        /// </summary>
        /// <param name="value">The given value.</param>
        public EventArgs(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns the given value.
        /// </summary>
        /// <value>The given Value.</value>
        public T Value { get; private set; }
    }

    /// <summary>
    /// Generic event argument for changing values.
    /// </summary>
    /// <typeparam name="T">The given class.</typeparam>
    public class ChangeEventArgs<T> : EventArgs, IChangeEventArgs<T>
    {
        /// <summary>
        /// Constructor withe the old and new value.
        /// </summary>
        /// <param name="oldV">The old value.</param>
        /// <param name="newV">The new value.</param>
        public ChangeEventArgs(T oldV, T newV)
        {
            OldValue = oldV;
            NewValue = newV;
        }

        /// <summary>
        /// Returns the old value.
        /// </summary>
        /// <value>The old value.</value>
        public T OldValue { get; private set; }
        /// <summary>
        /// Returns the new value.
        /// </summary>
        /// <value>The new value.</value>
        public T NewValue { get; private set; }
    }

    /// <summary>
    /// Event argument for changing values.
    /// </summary>
    public class ChangeEventArgs : ChangeEventArgs<object>, IChangeEventArgs
    {
        /// <summary>
        /// Constructor withe the old and new value.
        /// </summary>
        /// <param name="oldV">The old value.</param>
        /// <param name="newV">The new value.</param>
        public ChangeEventArgs(object oldV, object newV)
            : base(oldV, newV)
        { }
    }

    /// <summary>
    /// An EventHandler for changing values.
    /// </summary>
    /// <typeparam name="T">The given class.</typeparam>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The changing value.</param>
    /// <example>
    /// Usable e.g. for changing value events:
    /// public event ChangeEventHandler{string} OnStringValueChange;
    /// </example>
    public delegate void ChangeEventHandler<T>(object sender, ChangeEventArgs<T> e);
    /// <summary>
    /// An EventHandler for changing values.
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The changing value.</param>
    /// <example>
    /// Usable e.g. for changing value events:
    /// public event ChangeEventHandler OnValueChange;
    /// </example>
    public delegate void ChangeEventHandler(object sender, ChangeEventArgs e);
}
