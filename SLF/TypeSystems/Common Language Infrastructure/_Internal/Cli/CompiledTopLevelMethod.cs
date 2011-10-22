using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledTopLevelMethod :
        CompiledMethodMemberBase<ITopLevelMethodMember, INamespaceParent>,
        ITopLevelMethodMember
    {

        public CompiledTopLevelMethod(MethodInfo memberInfo, INamespaceParent parent)
            : base(memberInfo, parent)
        {
        }

        protected override ITopLevelMethodMember OnMakeGenericClosure(ITypeCollectionBase genericReplacements)
        {
            return new _TopLevelMethod(this, genericReplacements);
        }
        #region ITopLevelMethodMember Members

        public IModule DeclaringModule
        {
            get {
                return this.MemberInfo.Module.GetModuleReference();
            }
        }

        public string FullName
        {
            get { return this.MemberInfo.Name; }
        }

        #endregion


    }
}
