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

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// <see cref="ICreateInstanceExpression"/>'s member
    /// assignment expression.
    /// </summary>
    public interface ICreateInstanceMemberAssignment
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/>
        /// that designates the name of the property to assign to
        /// <see cref="AssignValue"/>.
        /// </summary>
        string PropertyName { get; set; }
        /// <summary>
        /// Returns/sets the value to assign to the <see cref="PropertyName"/>.
        /// </summary>
        IExpression AssignValue { get; set; }
    }
}
