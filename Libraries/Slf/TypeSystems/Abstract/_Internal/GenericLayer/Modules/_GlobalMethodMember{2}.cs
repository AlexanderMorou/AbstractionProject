using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        public _GlobalMethodMember(IModuleGlobalMethod original, IControlledTypeCollection genericReplacements)
            : base(original, genericReplacements)
        {
        }

        protected override IModuleGlobalMethod OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
        {
            throw new NotSupportedException("Closed generic global methods are already generic methods.");
        }

    }
}
