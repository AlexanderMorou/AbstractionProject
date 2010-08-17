using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal interface ICompiledTypeParent :
        ITypeParent
    {
        /// <summary>
        /// Returns the <see cref="Type[]"/> series which is associated
        /// to the underlying implementation of the <see cref="ICompiledTypeParent"/>.
        /// </summary>
        Type[] UnderlyingSystemTypes { get; }
    }
}
