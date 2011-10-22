using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledTopLevelField :
        CompiledFieldMemberBase<ITopLevelFieldMember, INamespaceParent>,
        ITopLevelFieldMember
    {
        public CompiledTopLevelField(FieldInfo memberInfo, INamespaceParent parent)
            : base(memberInfo, parent)
        {

        }

        #region ITopLevelFieldMember Members

        public IModule DeclaringModule
        {
            get { return this.MemberInfo.Module.GetModuleReference(); }
        }

        #endregion

    }
}
