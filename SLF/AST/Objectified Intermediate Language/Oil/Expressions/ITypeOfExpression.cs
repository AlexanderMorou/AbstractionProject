using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a type of expression 
    /// which loads the meta-data token of the type in question onto the
    /// stack.
    /// </summary>
    public interface ITypeOfExpression :
        IFusionCommaTargetExpression
    {
        /// <summary>
        /// Returns the <see cref="IType"/> which is represented
        /// under the typeof expression.
        /// </summary>
        IType ReferenceType { get; }
    }
}
