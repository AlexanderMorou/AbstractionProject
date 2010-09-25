using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
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
