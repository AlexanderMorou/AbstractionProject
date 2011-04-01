using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Modules
{
    internal class CompiledModuleGlobalMethod :
        CompiledMethodMemberBase<IModuleGlobalMethod, IModule>,
        _IGenericMethodRegistrar,
        IModuleGlobalMethod
    {

        internal CompiledModuleGlobalMethod(MethodInfo method, ICompiledModule module)
            : base(method, module)
        {
            
        }

        protected override IModuleGlobalMethod OnMakeGenericClosure(ITypeCollectionBase genericReplacements)
        {
            return new _GenericMethod(this, genericReplacements);
        }

        private class _GenericMethod :
            _MethodMemberBase<IModuleGlobalMethod, IModule>,
            IModuleGlobalMethod
        {
            internal _GenericMethod(CompiledModuleGlobalMethod original, ITypeCollectionBase genericParameters)
                : base(original, genericParameters)
            {
            }

            protected override IModuleGlobalMethod OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
            {
                throw new InvalidOperationException();
            }
        }

        protected override AccessLevelModifiers AccessLevelImpl
        {
            get { return this.MemberInfo.GetAccessModifiers(); }
        }

    }
}
