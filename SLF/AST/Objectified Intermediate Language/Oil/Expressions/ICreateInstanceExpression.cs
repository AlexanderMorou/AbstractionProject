using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// instance creation expression.
    /// </summary>
    public interface ICreateInstanceExpression :
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// Returns/sets the type of instance to create.
        /// </summary>
        IType InstanceType { get; set; }
        /// <summary>
        /// Returns the <see cref="IExpressionCollection"/>
        /// </summary>
        IExpressionCollection ConstructorParameters { get; }
        /// <summary>
        /// Returns the <see cref="ICollection{T}"/> of <see cref="ICreateInstanceMemberAssignExpression"/>
        /// instances that relates to the property assignment
        /// expressions for the 
        /// <see cref="ICreateInstanceExpression"/>.
        /// </summary>
        ICollection<ICreateInstanceMemberAssignExpression> PropertyAssignments { get; }
    }
}
