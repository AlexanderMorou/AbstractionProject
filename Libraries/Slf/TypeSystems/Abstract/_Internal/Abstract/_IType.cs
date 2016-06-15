using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal interface _IType :
        IType
    {
        /// <summary>
        /// Returns the internal <see cref="TypeArrayCache"/>.
        /// </summary>
        TypeArrayCache ArrayCache { get; }
        /// <summary>
        /// Returns the internal <see cref="TypeModifiedCache"/>.
        /// </summary>
        TypeModifiedCache ModifiedTypeCache { get; }
    }
}
