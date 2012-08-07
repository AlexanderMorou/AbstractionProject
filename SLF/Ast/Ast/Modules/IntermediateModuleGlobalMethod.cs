using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Ast.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Modules
{
    public class IntermediateModuleGlobalMethod :
        IntermediateMethodMemberBase<IModuleGlobalMethod, IIntermediateModuleGlobalMethod, IModule, IIntermediateModule>,
        IIntermediateModuleGlobalMethod
    {
        protected internal IntermediateModuleGlobalMethod(IIntermediateModule parent)
            : base(parent)
        {
        }

        protected override IModuleGlobalMethod OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
        {
            return new _GlobalMethodMember(this, genericReplacements);
        }

        public override IIntermediateAssembly Assembly
        {
            get { return this.Parent.Parent; }
        }
    }
}
