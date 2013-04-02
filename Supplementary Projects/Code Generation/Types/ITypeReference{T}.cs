using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    public interface ITypeReference<T> :
        ITypeReference
        where T :
            IType
    {
        /// <summary>
        /// Returns the <typeparamref name="T"/> 
        /// </summary>
        T TypeReference { get; }

    }
}
