using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    /// <summary>
    /// Defines generic properties and methods for 
    /// working with a compiled property member.
    /// </summary>
    /// <typeparam name="TMethod">The type of <see cref="IMethodMember{TMethod, TMethodParent}"/> 
    /// which is used for the <see cref="CompiledPropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}.GetMethod"/>
    /// and <see cref="CompiledPropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}.SetMethod"/>.</typeparam>
    /// <typeparam name="TProperty">The type of property used in the current implementation.</typeparam>
    /// <typeparam name="TPropertyParent">The type of parent that contains the <see cref="IPropertyMember"/> 
    /// instances in the current implementation.</typeparam>
    internal abstract class CompiledPropertyMemberBase<TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent> :
        PropertyMemberBase<TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent>,
        ICompiledPropertyMember
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
        where TPropertyMethod :
            class,
            TMethod,
            IExtendedInstanceMember,
            IPropertyMethodMember
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParentType<TProperty, TPropertyParent>
    {
        private PropertyInfo memberInfo;

        public CompiledPropertyMemberBase(PropertyInfo memberInfo, TPropertyParent parent)
            : base(parent)
        {
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

        protected override sealed TPropertyMethod OnGetMethod(PropertyMethodType methodType)
        {
            MethodInfo mi = null;
            switch (methodType)
            {
                case PropertyMethodType.GetMethod:
                    mi = this.MemberInfo.GetGetMethod(true);
                    break;
                case PropertyMethodType.SetMethod:
                    mi = this.MemberInfo.GetSetMethod(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("methodType");
            }
            if (mi == null)
                return null;
            return this.OnGetMethod(methodType, mi);
        }

        public override string UniqueIdentifier
        {
            get { return this.Name; }
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

        /// <summary>
        /// Obtains a <typeparamref name="TPropertyMethod"/> for the
        /// current <see cref="CompiledPropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// with the <paramref name="memberInfo"/> provided.
        /// </summary>
        /// <param name="memberInfo">The <see cref="MethodInfo"/> of the 
        /// method.</param>
        /// <returns>An <see cref="IMethodMember"/></returns><remarks>Used for <see cref="IPropertyMember.GetMethod"/> and
        /// <paramref name="IPropertyMember.SetMethod"/></remarks>
        protected abstract TPropertyMethod OnGetMethod(PropertyMethodType methodType, MethodInfo memberInfo);

        protected override string OnGetName()
        {
            return this.MemberInfo.Name;
        }

        public override bool IsStatic
        {
            get {
                if (this.CanRead)
                {
                    return this.GetMethod.IsStatic;
                }
                else if (this.CanWrite)
                {
                    return this.SetMethod.IsStatic;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool IsVirtual
        {
            get
            {
                if (this.CanRead)
                {
                    return this.GetMethod.IsVirtual;
                }
                else if (this.CanWrite)
                {
                    return this.SetMethod.IsVirtual;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool IsHideBySignature
        {
            get
            {
                if (this.CanRead)
                {
                    return this.GetMethod.IsHideBySignature;
                }
                else if (this.CanWrite)
                {
                    return this.SetMethod.IsHideBySignature;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool IsFinal
        {
            get
            {
                if (this.CanRead)
                {
                    return this.GetMethod.IsFinal;
                }
                else if (this.CanWrite)
                {
                    return this.SetMethod.IsFinal;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool IsOverride
        {
            get
            {
                if (this.CanRead)
                {
                    return this.GetMethod.IsOverride;
                }
                else if (this.CanWrite)
                {
                    return this.SetMethod.IsOverride;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool IsAbstract
        {
            get
            {
                if (this.CanRead)
                {
                    return this.GetMethod.IsAbstract;
                }
                else if (this.CanWrite)
                {
                    return this.SetMethod.IsAbstract;
                }
                else
                {
                    return false;
                }
            }
        }

        protected override AccessLevelModifiers AccessLevelImpl
        {
            get { return this.MemberInfo.GetAccessModifiers(); }
        }
    }
}
