using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// <see cref="ICreateInstanceExpression"/>'s member
    /// assignment expression.
    /// </summary>
    public interface ICreateInstanceMemberAssignExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="IMemberReferenceExpression"/>
        /// that designates the property to assign on the 
        /// <see cref="ICreateInstanceExpression.InstanceType"/>.
        /// </summary>
        IMemberReferenceExpression Target { get; set; }
        /// <summary>
        /// Returns/sets the value to assign to the <see cref="Target"/>.
        /// </summary>
        IExpression AssignValue { get; set; }
    }
}
