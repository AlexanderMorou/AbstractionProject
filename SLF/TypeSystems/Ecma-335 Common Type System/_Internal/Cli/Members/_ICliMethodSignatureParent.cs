using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal interface _ICliMethodSignatureParent :
        IMethodSignatureParent
    {
        /// <summary>
        /// Returns the <see cref="_ICliManager"/> associated to the
        /// current <see cref="_ICliMethodSignatureParent"/>.
        /// </summary>
        _ICliManager Manager { get; }
    }
}
