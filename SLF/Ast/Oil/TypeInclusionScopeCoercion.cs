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

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class TypeInclusionScopeCoercion :
        ITypeInclusionScopeCoercion
    {
        #region ITypeInclusionScopeCoercion Members

        /// <summary>
        /// Returns the <see cref="IType"/> associated to the
        /// type inclusion to the active scope.
        /// </summary>
        public IType IncludedType { get; set; }

        #endregion
    }
}
