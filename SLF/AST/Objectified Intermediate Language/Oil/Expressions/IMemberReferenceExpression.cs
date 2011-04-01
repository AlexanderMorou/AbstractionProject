using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for a reference
    /// expression that refers to a <see cref="IMember"/>.
    /// </summary>
    public interface IMemberReferenceExpression :
        INaryOperandExpression,
        IUnaryOperationPrimaryTerm
    {
        /// <summary>
        /// Returns/sets the name of the member to reference.
        /// </summary>
        string Name { get; }
    }
}
