using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for coercing a scope by including
    /// a type by its name.
    /// </summary>
    public interface ITypeInclusionScopeCoercion :
        IScopeCoercion
    {
        /// <summary>
        /// Returns the <see cref="IType"/> associated to the
        /// type inclusion to the active scope.
        /// </summary>
        IType IncludedType { get; set; }
    }
}
