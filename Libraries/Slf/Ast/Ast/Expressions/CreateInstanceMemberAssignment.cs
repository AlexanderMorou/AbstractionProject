using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public class CreateInstanceUnboundMemberAssignment :
        ICreateInstanceUnboundMemberAssignment
    {
        /// <summary>
        /// Creates a new <see cref="CreateInstanceUnboundMemberAssignment"/>
        /// with the initial <paramref name="propertyName"/> and
        /// <paramref name="assignValue"/> provided.
        /// </summary>
        /// <param name="propertyName">The <see cref="String"/>
        /// value that designates the name of the property to assign to
        /// <paramref name="assignValue"/>.</param>
        /// <param name="assignValue">The <see cref="IExpression"/>
        /// to assign the property relative to <paramref name="propertyName"/>
        /// to.</param>
        public CreateInstanceUnboundMemberAssignment(string propertyName, IExpression assignValue)
        {
            this.Name = propertyName;
            this.AssignValue = assignValue;
        }

        #region ICreateInstanceUnboundMemberAssignment Members
        /// <summary>
        /// Returns/sets the <see cref="String"/>
        /// that designates the name of the property to assign to
        /// <see cref="AssignValue"/>.
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region ICreateInstanceMemberAssignment

        /// <summary>
        /// Returns/sets the value to assign to the <see cref="Name"/>.
        /// </summary>
        public IExpression AssignValue { get; set; }

        public void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
        #endregion

        public override string ToString()
        {
            return string.Format("{0} = {1}", this.Name, this.AssignValue);
        }

    }
}
