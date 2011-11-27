using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
