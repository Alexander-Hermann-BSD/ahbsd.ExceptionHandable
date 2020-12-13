using System;

namespace ahbsd.lib.ExceptionHandable
{
    /// <summary>
    /// Attribute für ExceptionHandable Klassen
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public class ExceptionHandableAttribute : Attribute, IExceptionType
    {
        #region Implementierung von IExceptionType
        /// <summary>
        /// Gibt den Exception-Typ zurück.
        /// </summary>
        /// <value>Der Exception-Typ</value>
        public Type ExceptionType
        {
            get; private set;
        }
        #endregion

        /// <summary>
        /// Konstruktor ohne Parameter
        /// </summary>
        public ExceptionHandableAttribute()
        {
            ExceptionType = typeof(Exception);
        }

        /// <summary>
        /// Konstruktor mit Angabe einer Exeption
        /// </summary>
        /// <param name="e">Zu überprüfende Exception</param>
        public ExceptionHandableAttribute(Exception e)
        {
            ExceptionType = ExceptionTyp.GetExceptionType(e);
        }

        /// <summary>
        /// Konstruktor mit Angabe eines Types
        /// </summary>
        /// <param name="et">Type</param>
        public ExceptionHandableAttribute(Type et)
        {
            ExceptionType = et;
        }


    }

    /// <summary>
    /// Interface um einen bestimmten Exception-Type zurück zu geben.
    /// </summary>
    /// <typeparam name="ET">Gewählter Exception-Typ</typeparam>
    public interface IExceptionType<ET> where ET: Exception
    {
        /// <summary>
        /// Gibt den Exception-Typ zurück.
        /// </summary>
        /// <value>Der Exception-Typ</value>
        Type ExceptionType { get; }
    }


    /// <summary>
    /// Interface um einen bestimmten Exception-Type zurück zu geben.
    /// </summary>
    public interface IExceptionType : IExceptionType<Exception> { }

    /// <summary>
    /// Klasse, die den Typ einer Exception zurück gibt.
    /// </summary>
    /// <typeparam name="ET">Gewählter Exception-Typ</typeparam>
    public class ExceptionTyp<ET> : IExceptionType<ET> where ET: Exception
    {
        /// <summary>
        /// Statische Methode um den <see cref="System.Type"/> einer <see cref="Exception"/> zurück zu geben.
        /// </summary>
        /// <param name="ex"><see cref="Exception"/> von der der <see cref="System.Type"/> zurück gegeben werden soll.</param>
        /// <returns>>Der <see cref="System.Type"/> der übergebenen <see cref="Exception"/>.</returns>
        public static Type GetExceptionType(ET ex)
        {
            return ex.GetType();
        }

        #region Implementierung von IExceptionType
        /// <summary>
        /// Gibt den Exception-Typ zurück.
        /// </summary>
        /// <value>Der Exception-Typ</value>
        public Type ExceptionType => Type;
        #endregion
        
        /// <summary>
        /// Gibt den <see cref="System.Type"/> der übergebenen <see cref="System.Exception"/> zurück.
        /// </summary>
        /// <value>Der <see cref="System.Type"/> der übergebenen <see cref="System.Exception"/>.</value>
        public Type Type { get; private set; }

        /// <summary>
        /// Konstruktor mit Angabe der zu untersuchenden <see cref="System.Exception"/>.
        /// </summary>
        /// <param name="ex">Der zu untersuchende Exception-Type.</param>
        public ExceptionTyp(ET ex)
        {
            Type = GetExceptionType(ex);
        }

        /// <summary>
        /// Konstruktor mit sofortiger Angabe des Types.
        /// </summary>
        /// <param name="et">Zu verwendender Type</param>
        /// <exception cref="TypeException{T}">Wenn der übergebene <see cref="System.Type"/> im Parameter er nicht ET als vererbte Klasse enthält.</exception>
        public ExceptionTyp(Type et)
        {
            Type[] nestedTypes = et.GetNestedTypes();
            bool containsException = false;

            foreach (Type t in nestedTypes)
            {
                containsException |= t.Equals(typeof(Exception));
            }

            if (containsException)
            {
                Type = et;
            }
            else
            {
                throw new TypeException<ET>(et);
            }
        }
    }

    /// <summary>
    /// Klasse, die den Typ einer Exception zurück gibt.
    /// </summary>
    public class ExceptionTyp : ExceptionTyp<Exception>, IExceptionType
    {
        /// <summary>
        /// Konstruktor mit Angabe der zu untersuchenden <see cref="Exception"/>.
        /// </summary>
        /// <param name="ex">Die zu untersuchende <see cref="Exception"/>.</param>
        public ExceptionTyp(Exception ex)
            :base(ex)
        { }
    }


}