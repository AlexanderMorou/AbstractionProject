using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CompiledIndexerSignatureMemberBase<TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent> :
        IndexerSignatureMemberBase<TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent>,
        ICompiledPropertySignatureMember
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
        where TIndexerMethod :
            class,
            TMethod,
            IPropertySignatureMethodMember
        where TMethod :
            IMethodSignatureMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodSignatureParent<TMethod, TMethodParent>
    {
        private PropertyInfo memberInfo;

        public CompiledIndexerSignatureMemberBase(PropertyInfo memberInfo, TIndexerParent parent)
            : base(parent)
        {
            this.memberInfo = memberInfo;
        }

        #region ICompiledPropertySignatureMember Members

        public PropertyInfo MemberInfo
        {
            get { return this.memberInfo; }
        }

        #endregion

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get
            {
                return this.MemberInfo;
            }
        }

        #endregion

        protected override IType OnGetPropertyType()
        {
            IType t = this.MemberInfo.PropertyType.GetTypeReference();
            if (this.Parent is IGenericType && t.ContainsGenericParameters())
                t = t.Disambiguify(((IGenericType)base.Parent).GenericParameters, null, TypeParameterSources.Type);
            return t;
        }

        protected override bool CanCachePropertyType
        {
            get { return true; }
        }

        protected override string OnGetName()
        {
            return this.MemberInfo.Name;
        }
    }
}
