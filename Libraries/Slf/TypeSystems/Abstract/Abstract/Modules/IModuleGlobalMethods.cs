using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Modules
{
    /// <summary>
    /// Defines properties and methods for working with the global methods defined on an
    /// <see cref="IModule"/>.
    /// </summary>
    public interface IModuleGlobalMethods :
        IMethodMemberDictionary<IModuleGlobalMethod, IModule>
    {
    }
}
