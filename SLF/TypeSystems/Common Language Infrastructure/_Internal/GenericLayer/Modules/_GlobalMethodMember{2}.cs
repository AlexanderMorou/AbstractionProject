using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;
/*---------------------------------------------------------------------\
| Copyright © 2010 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Modules
{
    internal class _GlobalMethodMember :
        _MethodMemberBase<IModuleGlobalMethod, IModule>,
        IModuleGlobalMethod
    {
        public _GlobalMethodMember(IModule parent, IModuleGlobalMethod original)
            : base(parent, original)
        {
        }

        protected override IModuleGlobalMethod OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
        {
            throw new NotSupportedException("Closed generic global methods are already generic methods.");
        }
    }
}
