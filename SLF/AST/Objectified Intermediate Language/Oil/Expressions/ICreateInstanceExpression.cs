using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        IConstructorInvokeExpression
    {
        /// <summary>
        /// Returns the <see cref="ICreateInstanceMemberAssignmentDictionary"/> 
        /// which relates to the property assignment expressions for 
        /// the <see cref="ICreateInstanceExpression"/>.
        /// </summary>
        ICreateInstanceMemberAssignmentDictionary PropertyAssignments { get; }
    }
}
