using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    public interface IExternTypeReference :
        ITypeReference
    {
        /// <summary>
        /// Returns the <see cref="IType"/> which the <see cref="ITypeReference"/> represents.
        /// </summary> 
        new IExternType TypeInstance { get; }
    }
}
