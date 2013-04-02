using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    public interface IExternType :
        IType
    {
        /// <summary>
        /// Returns the <see cref="System.Type"/> which the extern refers to.
        /// </summary>
        System.Type Type { get; }
    }
}
