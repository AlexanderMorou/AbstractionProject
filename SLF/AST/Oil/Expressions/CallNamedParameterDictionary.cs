using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class CallNamedParameterDictionary :
        ControlledStateDictionary<string, INamedParameterExpression>,
        ICallNamedParameterDictionary
    {
        public CallNamedParameterDictionary()
        {
        }

        #region ICallNamedParameterDictionary Members

        /// <summary>
        /// Adds a <see cref="INamedParameterExpression"/> to the
        /// <see cref="ICallNamedParameterDictionary"/> with the
        /// <paramref name="parameterName"/> and <paramref name="expression"/>
        /// provided.
        /// </summary>
        /// <param name="parameterName">The <see cref="String"/> value
        /// representing the name of the parameter the
        /// <see cref="INamedParameterExpression"/> refers to.</param>
        /// <param name="expression">The <see cref="IExpression"/>
        /// wrapped by the <see cref="INamedParameterExpression"/> which
        /// is to be executed.</param>
        /// <returns>A <see cref="INamedParameterExpression"/> which associates the
        /// <paramref name="expression"/> to the <paramref name="parameterName"/> provided.</returns>
        public INamedParameterExpression Add(string parameterName, IExpression expression)
        {
            var result = new NamedParameterExpression(parameterName, expression);
            this._Add(parameterName, result);
            return result;
        }

        /// <summary>
        /// Adds a <see cref="INamedParameterExpression"/> directly to the
        /// <see cref="ICallNamedParameterDictionary"/>.
        /// </summary>
        /// <param name="expression">The <see cref="INamedParameterExpression"/>
        /// to add.</param>
        public void Add(INamedParameterExpression expression)
        {
            this._Add(expression.Name, expression);
        }

        /// <summary>
        /// Clears the <see cref="ICallNamedParameterDictionary"/>.
        /// </summary>
        public void Clear()
        {
            base._Clear();
        }

        /// <summary>
        /// Removes an element from the <see cref="CallNamedParameterDictionary"/>
        /// by the <paramref name="index"/> provided.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value representing
        /// the ordinal index of the element to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="index"/>
        /// is less than zero or greater than or equal to the number of elements
        /// within the <see cref="CallNamedParameterDictionary"/>.</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentOutOfRangeException("index");
            base._Remove(index);
        }

        /// <summary>
        /// Removes an element from the <see cref="CallNamedParameterDictionary"/>
        /// by the <paramref name="parameterName"/> provided.
        /// </summary>
        /// <param name="parameterName">The <see cref="String"/> value representing the
        /// key of the element to remove.</param>
        /// <returns>true if an element by the <paramref name="parameterName"/> provided exists;
        /// false, otherwise.</returns>
        public bool Remove(string parameterName)
        {
            return this._Remove(parameterName);
        }

        #endregion

    }
}
