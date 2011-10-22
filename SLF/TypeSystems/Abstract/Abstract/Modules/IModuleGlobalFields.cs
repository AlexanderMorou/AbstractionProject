using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Modules
{
    /// <summary>
    /// Defines properties and methods for working with a series
    /// of <see cref="IModuleGlobalField"/> instances.
    /// </summary>
    public interface IModuleGlobalFields :
        IFieldMemberDictionary<IModuleGlobalField, IModule>
    {
    }
}
