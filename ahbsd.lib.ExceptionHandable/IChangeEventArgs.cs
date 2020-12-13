namespace ahbsd.lib
{
    /// <summary>
    /// Interface for event arguments of changed values.
    /// </summary>
    /// <typeparam name="T">Given class</typeparam>
    public interface IChangeEventArgs<T>
    {
        /// <summary>
        /// Returns the old value.
        /// </summary>
        /// <value>The old value.</value>
        T OldValue { get; }
        /// <summary>
        /// Returns the new value.
        /// </summary>
        /// <value>The new value.</value>
        T NewValue { get; }
    }

    /// <summary>
    /// Interface for event arguments of changed values.
    /// </summary>
    public interface IChangeEventArgs : IChangeEventArgs<object> { }
}