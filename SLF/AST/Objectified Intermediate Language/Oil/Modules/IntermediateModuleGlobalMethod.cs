using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Modules;
/*---------------------------------------------------------------------\
| Copyright © 2010 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    public class IntermediateModuleGlobalMethod :
        IntermediateMethodMemberBase<IModuleGlobalMethod, IIntermediateModuleGlobalMethod, IModule, IIntermediateModule>,
        IIntermediateModuleGlobalMethod
    {
        protected internal IntermediateModuleGlobalMethod(IIntermediateModule parent)
            : base(parent)
        {
        }

        protected override IModuleGlobalMethod OnMakeGenericMethod(ITypeCollection genericReplacements)
        {
            return new _GlobalMethodMember(this.Parent, this);
        }

        public override IIntermediateAssembly Assembly
        {
            get { return this.Parent.Parent; }
        }
    }
}
