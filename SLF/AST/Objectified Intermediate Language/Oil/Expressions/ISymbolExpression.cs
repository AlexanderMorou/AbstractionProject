using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// symbol expression whose intent is inferred
    /// upon the active scope of use.
    /// </summary>
    /// <example>
    /// "Console".Call("WriteLine", ...);
    /// </example>
    public interface ISymbolExpression :
        IMemberParentReferenceExpression,
        IFusionCommaTargetExpression, 
        IFusionTypeCollectionTargetExpression,
        IFusionTermExpression,
        IUnaryOperationPrimaryTerm
    {
        /// <summary>
        /// Returns the <see cref="IExpression"/>
        /// from which the <see cref="ISymbolExpression"/>
        /// is connected to.
        /// </summary>
        IMemberParentReferenceExpression Source { get; }
        /// <summary>
        /// Returns/sets the value associated with the symbol.
        /// </summary>
        string Symbol { get; }
    }
}
