using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Provides a base implementation of a <see cref="ICreateInstanceMemberAssignment"/>
    /// which provides properties and methods for working with a
    /// <see cref="ICreateInstanceExpression"/>'s member
    /// assignment expression.
    /// </summary>
    public class CreateInstanceMemberAssignment :
        ICreateInstanceMemberAssignment
    {
        /// <summary>
        /// Creates a new <see cref="CreateInstanceMemberAssignment"/>
        /// with the initial <paramref name="propertyName"/> and
        /// <paramref name="assignValue"/> provided.
        /// </summary>
        /// <param name="propertyName">The <see cref="String"/>
        /// value that designates the name of the property to assign to
        /// <paramref name="assignValue"/>.</param>
        /// <param name="assignValue">The <see cref="IExpression"/>
        /// to assign the property relative to <paramref name="propertyName"/>
        /// to.</param>
        public CreateInstanceMemberAssignment(string propertyName, IExpression assignValue)
        {
            this.PropertyName = propertyName;
            this.AssignValue = assignValue;
        }

        #region ICreateInstanceMemberAssignment Members

        /// <summary>
        /// Returns/sets the <see cref="String"/>
        /// that designates the name of the property to assign to
        /// <see cref="AssignValue"/>.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Returns/sets the value to assign to the <see cref="PropertyName"/>.
        /// </summary>
        public IExpression AssignValue { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} = {1}", this.PropertyName, this.AssignValue);
        }
    }
}
