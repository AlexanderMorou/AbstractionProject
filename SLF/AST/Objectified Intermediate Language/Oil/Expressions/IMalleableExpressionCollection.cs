using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IMalleableExpressionCollection<T> :
        IExpressionCollection<T>
        where T :
            IExpression
    {
        /// <summary>
        /// Adds a <see cref="IExpression"/> to the <see cref="IMalleableExpressionCollection"/>.
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/> 
        /// to add to the <see cref="IMalleableExpressionCollection"/>.</param>
        void Add(T expression);
        /// <summary>
        /// Inserts the <paramref name="expression"/> provided
        /// at the <paramref name="index"/> specified into the <see cref="IMalleableExpressionCollection"/>.
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/> to insert.</param>
        /// <param name="index">The zero-based <see cref="Int32"/> index to place the <paramref name="expression"/>.</param>
        void Insert(T expression, int index);
        /// <summary>
        /// Removes an element of the <see cref="IMalleableExpressionCollection"/> at the
        /// <paramref name="index"/> specified.
        /// </summary>
        /// <param name="index">The zero-based <see cref="Int32"/> index of the 
        /// <see cref="IExpression"/> to remove from the <see cref="IMalleableExpressionCollection"/>.</param>
        void RemoveAt(int index);
        /// <summary>
        /// Removes the specified <paramref name="expression"/> from the 
        /// <see cref="IMalleableExpressionCollection"/>
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/> to add to the 
        /// <see cref="IMalleableExpressionCollection"/>.</param>
        void Remove(T expression);
        /// <summary>
        /// Clears the <see cref="IMalleableExpressionCollection"/> of all
        /// its elements.
        /// </summary>
        void Clear();
        /// <summary>
        /// Returns/sets an <see cref="IExpression"/> element
        /// at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based <see cref="Int32"/> value indicating the index
        /// of the element to retrieve/store.</param>
        /// <returns>A <see cref="IExpression"/> instance at the <paramref name="index"/>
        /// specified.</returns>
        new T this[int index] { get; set; }
    }

    /// <summary>
    /// Defines properties and methods for working with a series of
    /// expressions which can be modified.
    /// </summary>
    public interface IMalleableExpressionCollection :
        IMalleableExpressionCollection<IExpression>
    {
    }
}
