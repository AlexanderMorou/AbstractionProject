﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CompiledIndexerMemberBase<TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent> :
        IndexerMemberBase<TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent>,
        ICompiledPropertyMember
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIndexerMethod :
            class,
            TMethod,
            IExtendedInstanceMember,
            IPropertyMethodMember
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        private PropertyInfo memberInfo;
        private bool lastIsParams;
        public CompiledIndexerMemberBase(PropertyInfo memberInfo, TIndexerParent parent)
            : base(parent)
        {
            this.lastIsParams = memberInfo.LastParameterIsParams();
            this.memberInfo = memberInfo;
        }

        protected override bool CanCachePropertyType
        {
            get { return true; }
        }

        protected override IType OnGetPropertyType()
        {
            IType t = this.MemberInfo.PropertyType.GetTypeReference();
            if (this.Parent is IGenericType && t.ContainsGenericParameters())
                t = t.Disambiguify(((IGenericType)base.Parent).GenericParameters, null, TypeParameterSources.Type);
            return t;
        }

        protected override AccessLevelModifiers AccessLevelImpl
        {
            get { return this.MemberInfo.GetAccessModifiers(); }
        }

        public override bool IsStatic
        {
            get
            {
                if (this.CanRead)
                    return this.GetMethod.IsStatic;
                else if (this.CanWrite)
                    return this.SetMethod.IsStatic;
                else
                    return false;
            }
        }

        public override bool IsVirtual
        {
            get
            {
                if (this.CanRead)
                    return this.GetMethod.IsVirtual;
                else if (this.CanWrite)
                    return this.SetMethod.IsVirtual;
                else
                    return false;
            }
        }

        public override bool IsHideBySignature
        {
            get
            {
                if (this.CanRead)
                    return this.GetMethod.IsHideBySignature;
                else if (this.CanWrite)
                    return this.SetMethod.IsHideBySignature;
                else
                    return false;
            }
        }

        public override bool IsFinal
        {
            get
            {
                if (this.CanRead)
                    return this.GetMethod.IsFinal;
                else if (this.CanWrite)
                    return this.SetMethod.IsFinal;
                else
                    return false;
            }
        }

        public override bool IsOverride
        {
            get
            {
                if (this.CanRead)
                    return this.GetMethod.IsOverride;
                else if (this.CanWrite)
                    return this.SetMethod.IsOverride;
                else
                    return false;
            }
        }

        public override bool IsAbstract
        {
            get
            {
                if (this.CanRead)
                    return this.GetMethod.IsAbstract;
                else if (this.CanWrite)
                    return this.SetMethod.IsAbstract;
                else
                    return false;
            }
        }

        protected override string OnGetName()
        {
            return this.MemberInfo.Name;
        }

        #region ICompiledPropertyMember Members

        public PropertyInfo MemberInfo
        {
            get { return this.memberInfo; }
        }

        #endregion

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get { return this.MemberInfo; }
        }

        #endregion

        protected override bool LastIsParamsImpl
        {
            get { return this.lastIsParams; }
        }

        protected override IParameterMemberDictionary<TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>> InitializeParameters()
        {
            return new ParameterDictionary(((TIndexer)(object)(this)), this.MemberInfo.GetIndexParameters());
        }
    }
}