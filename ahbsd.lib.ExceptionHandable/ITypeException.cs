using System;
using System.Runtime.Serialization;
using System.Security;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Interface für <see cref="Type"/>-Spezifische Exceptions.
    /// </summary>
    /// <typeparam name="T">Soll-Type</typeparam>
    public interface ITypeException<T>
    {
        /// <summary>
        /// Gibt den Array der vorhandenen Typen zurück.
        /// </summary>
        /// <value>Ein Array der vorhandenen Typen</value>
        Type[] TypeArray { get; }
        /// <summary>
        /// Gibt den Soll-<see cref="Type"/> zurück.
        /// </summary>
        /// <value>Der Soll-Type.</value>
        Type PlanedType { get; }
        /// <summary>
        /// Gibt den übergebenen <see cref="Type"/> zurück.
        /// </summary>
        /// <value>Der übergebene Type.</value>
        Type Value { get; }
    }

    /// <summary>
    /// Klasse zur Implementierung von <see cref="ITypeException{T}"/>.
    /// </summary>
    /// <typeparam name="T">Der Soll-<see cref="Type"/>.</typeparam>
    [Serializable]
    public class TypeException<T> : Exception, ITypeException<T> 
    {
        #region Implementierung von ITypeException<Exception>
        /// <summary>
        /// Gibt den Soll-<see cref="Type"/> zurück.
        /// </summary>
        /// <value>Der Soll-Type.</value>
        public Type PlanedType => typeof(T); 

        /// <summary>
        /// Gibt den Array der vorhandenen Typen zurück.
        /// </summary>
        /// <value>Ein Array der vorhandenen Typen</value>
        public Type[] TypeArray => Value.GetNestedTypes();

        /// <summary>
        /// Gibt den übergebenen <see cref="Type"/> zurück.
        /// </summary>
        /// <value>Der übergebene Type.</value>
        public Type Value { get; private set; }
        #endregion

        /// <summary>
        /// Konstruktor mit Übergabe eines unpassenden <see cref="Type"/>.
        /// </summary>
        /// <param name="type">Unpassender Type</param>
        public TypeException(Type type)
            : base(string.Format("Der Soll-Type sollte '{0}' sein, '{1}' hat diesen aber nicht in der Vererbungs-Liste.", typeof(T), type))
        {
            Value = type;
        }
        
        /// <summary>
        /// Füllt eine <see cref="SerializationInfo"/> mit den Daten auf, die zum Serialisieren des Zielobjekts erforderlich sind.
        /// </summary>
        /// <param name="info">Die mit Daten zu füllende <see cref="SerializationInfo"/>.</param>
        /// <param name="context">Das Ziel (siehe <see cref="StreamingContext"/>) dieser Serialisierung.</param>
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}