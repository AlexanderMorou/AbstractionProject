using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Modules
{
    /// <summary>
    /// Defines properties and methods for working with a global method
    /// defined on an intermediate module.
    /// </summary>
    /// <remarks>Fields defined at the module level exist in the
    /// .sdata portion of the executable and require the information to
    /// be stored at compile time.</remarks>
    public interface IIntermediateModuleGlobalMethod :
        IIntermediateMethodMember<IModuleGlobalMethod, IIntermediateModuleGlobalMethod, IModule, IIntermediateModule>,
        IModuleGlobalMethod
    {
    }
}
