using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    /// <summary>
    /// Defines properties and methods for working with the global
    /// data fields defined on an intermediate module.
    /// </summary>
    public interface IIntermediateModuleGlobalFieldDictionary :
        IIntermediateFieldMemberDictionary<IModuleGlobalField, IIntermediateModuleGlobalField, IModule, IIntermediateModule>,
        IModuleGlobalFields
    {
    }
}
