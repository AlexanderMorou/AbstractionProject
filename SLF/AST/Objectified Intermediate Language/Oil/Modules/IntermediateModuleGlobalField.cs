using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
/*---------------------------------------------------------------------\
| Copyright © 2010 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    /// <summary>
    /// Provides a base implementation of a global field
    /// defined on an intermediate module.
    /// </summary>
    public class IntermediateModuleGlobalField :
        IntermediateFieldMemberBase<IModuleGlobalField, IIntermediateModuleGlobalField, IModule, IIntermediateModule>,
        IIntermediateModuleGlobalField
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateModuleGlobalField"/> instance
        /// with the <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing the name of the <see cref="IntermediateModuleGlobalField"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateModule"/> to 
        /// which the <see cref="IntermediateModuleGlobalField"/> belongs.</param>
        public IntermediateModuleGlobalField(string name, IIntermediateModule parent)
            : base(name, parent)
        {
        }
    }
}
