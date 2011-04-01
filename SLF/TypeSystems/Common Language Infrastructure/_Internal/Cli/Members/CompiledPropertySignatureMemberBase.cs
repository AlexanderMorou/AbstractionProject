using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CompiledPropertySignatureMemberBase<TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent> :
        PropertySignatureMemberBase<TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent>
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParentType<TProperty, TPropertyParent>
        where TMethod :
            IMethodSignatureMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodSignatureParent<TMethod, TMethodParent>
        where TPropertyMethod :
            class,
            TMethod,
            IPropertySignatureMethodMember
    {
        /// <summary>
        /// Creates a new <see cref="CompiledPropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TPropertyParent"/>
        /// which owns the <see cref="CompiledPropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>.</param>
        protected CompiledPropertySignatureMemberBase(TPropertyParent parent, PropertyInfo memberInfo)
            : base(parent)
        {
            this.MemberInfo = memberInfo;
        }

        protected PropertyInfo MemberInfo { get; private set; }

        protected override IType OnGetPropertyType()
        {
            return this.MemberInfo.PropertyType.GetTypeReference();
        }

        protected override bool CanCachePropertyType
        {
            get { return true; }
        }

        protected override string OnGetName()
        {
            return this.MemberInfo.Name;
        }

        public override string UniqueIdentifier
        {
            get { return this.Name; }
        }

        /// <summary>
        /// Obtains a <typeparamref name="TPropertyMethod"/> for the
        /// current <see cref="CompiledPropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// with the <paramref name="memberInfo"/> provided.
        /// </summary>
        /// <param name="memberInfo">The <see cref="MethodInfo"/> of the 
        /// method.</param>
        /// <returns>An <see cref="IMethodMember"/></returns><remarks>Used for <see cref="IPropertySignatureMember.GetMethod"/> and
        /// <see cref="IPropertySignatureMember.SetMethod"/></remarks>
        protected abstract TPropertyMethod OnGetMethod(PropertyMethodType methodType, MethodInfo memberInfo);

        protected override sealed TPropertyMethod OnGetMethod(PropertyMethodType methodType)
        {
            MethodInfo mi = null;
            switch (methodType)
            {
                case PropertyMethodType.GetMethod:
                    this.MemberInfo.GetGetMethod(true);
                    break;
                case PropertyMethodType.SetMethod:
                    this.MemberInfo.GetSetMethod(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("methodType");
            }
            if (mi == null)
                return null;
            return this.OnGetMethod(methodType, mi);
        }
        #region ICompiledPropertyMember Members


        public T GetValue<T>(object target)
        {
            if (this.CanRead)
                return (T)this.MemberInfo.GetValue(target, null);
            else
                throw new NotSupportedException("Property is write-only.");
        }

        public T GetValue<T>()
        {
            if (this.CanRead)
                return (T)this.MemberInfo.GetValue(null, null);
            else
                throw new NotSupportedException("Property is write-only.");
        }

        public object GetValue(object target)
        {
            if (this.CanRead)
                return this.MemberInfo.GetValue(target, null);
            else
                throw new NotSupportedException("Property is write-only.");
        }

        public object GetValue()
        {
            if (this.CanRead)
                return this.MemberInfo.GetValue(null, null);
            else
                throw new NotSupportedException("Property is write-only.");
        }

        public void SetValue(object target, object value)
        {
            if (this.CanWrite)
                this.MemberInfo.SetValue(target, value, null);
            else
                throw new NotSupportedException("Property is read-only");
        }

        public void SetValue(object value)
        {
            if (this.CanWrite)
                this.MemberInfo.SetValue(null, value, null);
            else
                throw new NotSupportedException("Property is read-only");
        }

        #endregion

    }
}
