using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    /// <summary>
    /// Defines properties and methods for working with a global
    /// data field defined on an intermediate module.
    /// </summary>
    public interface IIntermediateModuleGlobalField :
        IIntermediateFieldMember<IModuleGlobalField, IIntermediateModuleGlobalField, IModule, IIntermediateModule>,
        IModuleGlobalField
    {
        /// <summary>
        /// Returns/sets the <see cref="Byte"/> array which denotes
        /// the information associated to the field in the .sdata portion
        /// of the PE.
        /// </summary>
        byte[] Data { get; set; }
    }
}
