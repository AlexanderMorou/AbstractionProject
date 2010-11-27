using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using System.Reflection.Emit;

namespace AllenCopeland.Abstraction.Slf.Oil
{

    public interface IIntermediateDynamicHandler :
        IIntermediateAssembly,
        IIntermediateMethodParent<IIntermediateDynamicMethod, IIntermediateDynamicMethod, IIntermediateDynamicHandler, IIntermediateDynamicHandler>
    {
        /// <summary>
        /// Returns the <see cref="AssemblyBuilder"/> associated to the 
        /// <see cref="IIntermediateDynamicHandler"/>.
        /// </summary>
        AssemblyBuilder Builder { get; }
    }
}
