using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Component for Exception Handling
    /// </summary>
    public class CaughtExceptionComponent : Component, ICaughtExceptions
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public CaughtExceptionComponent()
        {
            Cs = new CaughtExceptions();

            Cs.OnSenderAdded += Cs_OnSenderAdded;
        }

        /// <summary>
        /// Constructor with an owning container.
        /// </summary>
        /// <param name="container">An owning container.</param>
        public CaughtExceptionComponent(IContainer container)
        {
            Cs = new CaughtExceptions();

            if (container != null)
            {
                container.Add(this);
            }

            Cs.OnSenderAdded += Cs_OnSenderAdded;
        }

        private void Cs_OnSenderAdded(object sender, EventArgs<object> e)
        {
            OnSenderAdded?.Invoke(sender, e);
        }

        /// <summary>
        /// Inner CaughtExceptions
        /// </summary>
        protected CaughtExceptions Cs { get; set; }

        #region implementation of ICaughtExceptions
        
        /// <summary>
        /// Keys
        /// </summary>
        public ICollection<object> Keys => ((ICaughtExceptions)Cs).Keys;
        /// <summary>
        /// Values
        /// </summary>
        public ICollection<IDictionary<DateTime, Exception>> Values => ((ICaughtExceptions)Cs).Values;
        /// <summary>
        /// Amount of <see cref="KeyValuePair{TKey, TValue}"/>s.
        /// </summary>
        public int Count => ((ICaughtExceptions)Cs).Count;
        /// <summary>
        /// Gives back, if it's <c>readonly</c>.
        /// </summary>
        public bool IsReadOnly => ((ICaughtExceptions)Cs).IsReadOnly;
        /// <summary>
        /// Gives back the values of a key or sets the value of a key.
        /// </summary>
        /// <param name="key">a key</param>
        /// <returns>an <see cref="IDictionary{DateTime, Exception}"/>-interface for the given key.</returns>
        public IDictionary<DateTime, Exception> this[object key] { get => ((ICaughtExceptions)Cs)[key]; set => ((ICaughtExceptions)Cs)[key] = value; }

        /// <summary>
        /// Adds a caught exception and the sending object.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The caught Exception.</param>
        public void AddException(object sender, Exception e)
        {
            Cs.AddException(sender, e);
            OnExceptionAdded?.Invoke(sender, new ExceptionEventArgs(e));
        }
        /// <summary>
        /// Adds an Exception in an <see cref="ExceptionEventArgs"/> and the sending object.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="eea">The caught exception in EventArgs.</param>
        public void AddExceptionEventArgs(object sender, ExceptionEventArgs eea)
        {
            AddException(sender, eea.Value);
        }
        /// <summary>
        /// Adds an exception and the sending object in an <see cref="ExceptionAndSenderEventArgs"/>.
        /// </summary>
        /// <param name="easea">The exception and the sending object.</param>
        public void AddExceptionEventArgs(ExceptionAndSenderEventArgs easea)
        {
            AddException(easea.Sender, easea.Value);
        }

        /// <summary>
        /// Adds an <see cref="IDictionary{DateTime, Exception}"/> to a key.
        /// </summary>
        /// <param name="key">a key</param>
        /// <param name="value">a <see cref="IDictionary{DateTime, Exception}"/></param>
        public void Add(object key, IDictionary<DateTime, Exception> value)
        {
            EventArgs<object> sea;
            ExceptionEventArgs e;

            if (!ContainsKey(key))
            {
                sea = new EventArgs<object>(key);

                ((ICaughtExceptions)Cs).Add(key, value);
                OnSenderAdded?.Invoke(this, sea);
            }
            else
            {
                foreach (KeyValuePair<DateTime, Exception> item in value)
                {
                    e = new ExceptionEventArgs(item.Value);
                    Cs[key].Add(item.Key, item.Value);
                    OnExceptionAdded?.Invoke(key, e);
                }
            }
            
        }

        /// <summary>
        /// Gives back, if a given key exists in this component.
        /// </summary>
        /// <param name="key">the given key</param>
        /// <returns><c>TRUE</c> if the key exists, otherwise <c>FALSE</c>.</returns>
        public bool ContainsKey(object key)
        {
            return ((ICaughtExceptions)Cs).ContainsKey(key);
        }

        /// <summary>
        /// Removes a given key (and it's values).
        /// </summary>
        /// <param name="key">the given key.</param>
        /// <returns><c>TRUE</c> if the key exists, otherwise <c>FALSE</c>.</returns>
        public bool Remove(object key)
        {
            return ((ICaughtExceptions)Cs).Remove(key);
        }

        /// <summary>
        /// Trys to get a value of a key.
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        /// <returns><c>TRUE</c> if the key exists, otherwise <c>FALSE</c>.</returns>
        public bool TryGetValue(object key, out IDictionary<DateTime, Exception> value)
        {
            return ((ICaughtExceptions)Cs).TryGetValue(key, out value);
        }

        /// <summary>
        /// Adds a given <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The given <see cref="KeyValuePair{TKey, TValue}"/>.</param>
        public void Add(KeyValuePair<object, IDictionary<DateTime, Exception>> item)
        {
            EventArgs<object> sea;
            ExceptionEventArgs e;

            if (!ContainsKey(item.Key))
            {
                sea = new EventArgs<object>(item.Key);
                ((ICaughtExceptions)Cs).Add(item);
                OnSenderAdded?.Invoke(this, sea);
            }
            else
            {
                foreach (KeyValuePair<DateTime,Exception> i in item.Value)
                {
                    e = new ExceptionEventArgs(i.Value);
                    Cs[item.Key].Add(i.Key, i.Value);
                    OnExceptionAdded?.Invoke(item.Key, e);
                }
            }
        }

        /// <summary>
        /// Clears the inner<see cref="ICaughtExceptions"/>.
        /// </summary>
        public void Clear()
        {
            ((ICaughtExceptions)Cs).Clear();
        }

        /// <summary>
        /// Returns if a specific item is contained.
        /// </summary>
        /// <param name="item">The specific item.</param>
        /// <returns><c>TRUE</c> if the item is contained, otherwise <c>FALSE</c>.</returns>
        public bool Contains(KeyValuePair<object, IDictionary<DateTime, Exception>> item)
        {
            return ((ICaughtExceptions)Cs).Contains(item);
        }

        /// <summary>
        /// Copies the inner <see cref="ICaughtExceptions"/> to a specific array.
        /// </summary>
        /// <param name="array">The specific array.</param>
        /// <param name="arrayIndex">The array index, where to start.</param>
        public void CopyTo(KeyValuePair<object, IDictionary<DateTime, Exception>>[] array, int arrayIndex)
        {
            ((ICaughtExceptions)Cs).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes a specific item.
        /// </summary>
        /// <param name="item">The specific item to remove.</param>
        /// <returns><c>TRUE</c> if item existed, othewise <c>FALSE</c>.</returns>
        public bool Remove(KeyValuePair<object, IDictionary<DateTime, Exception>> item)
        {
            return ((ICaughtExceptions)Cs).Remove(item);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<KeyValuePair<object, IDictionary<DateTime, Exception>>> GetEnumerator()
        {
            return ((ICaughtExceptions)Cs).GetEnumerator();
        }

        /// <summary>
        /// Gets an <see cref="IEnumerator"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICaughtExceptions)Cs).GetEnumerator();
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
