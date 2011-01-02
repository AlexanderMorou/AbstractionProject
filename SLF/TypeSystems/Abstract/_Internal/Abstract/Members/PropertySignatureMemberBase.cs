using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides an internal base for property signature members to derive from.
    /// </summary>
    /// <typeparam name="TProperty">The type of property signature in the current implementation.</typeparam>
    /// <typeparam name="TPropertyParent">The type of property signature parent in the abstract type
    /// system.</typeparam>
    /// <typeparam name="TMethod">The type of method signature used in the current implementation.</typeparam>
    /// <typeparam name="TMethodParent">The type which owns the <typeparamref name="TMethod"/>
    /// instances in the current implementation.</typeparam>
    /// <typeparam name="TPropertyMethod">The type which acts as the 
    /// property signature method members.</typeparam>
    internal abstract partial class PropertySignatureMemberBase<TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent> :
        MemberBase<TPropertyParent>,
        IPropertySignatureMember<TProperty, TPropertyParent>
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParentType<TProperty, TPropertyParent>
        where TMethod :
            IMethodSignatureMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodSignatureParent<TMethod, TMethodParent>
        where TPropertyMethod :
            TMethod,
            IPropertySignatureMethodMember
    {
        /// <summary>
        /// Data member for <see cref="PropertyType"/>.
        /// </summary>
        private IType propertyType;
        /// <summary>
        /// Data member for <see cref="GetMethod"/>.
        /// </summary>
        private TPropertyMethod getMethod;
        /// <summary>
        /// Data member for <see cref="SetMethod"/>.
        /// </summary>
        private TPropertyMethod setMethod;

        /// <summary>
        /// Creates a new <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TPropertyParent"/> 
        /// which contians the <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>.</param>
        protected PropertySignatureMemberBase(TPropertyParent parent)
            : base(parent)
        {
        }

        #region IPropertySignatureMember Members

        /// <summary>
        /// Returns the type that the 
        /// <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> 
        /// is defined as.
        /// </summary>
        public IType PropertyType
        {
            get {
                if (this.CanCachePropertyType)
                {
                    if (this.propertyType == null)
                        propertyType = this.OnGetPropertyType();
                    return this.propertyType;
                }
                else
                    return this.OnGetPropertyType();
            }
        }

        /// <summary>
        /// Returns whether the <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// can be read from.
        /// </summary>
        public virtual bool CanRead
        {
            get { return this.GetMethod != null; }
        }

        /// <summary>
        /// Returns whether the <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> 
        /// can be written to.
        /// </summary>
        public bool CanWrite
        {
            get { return this.SetMethod != null; }
        }

        /// <summary>
        /// Returns the <typeparamref name="TPropertyMethod"/>
        /// which represents the get method of the 
        /// <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>.
        /// </summary>
        public TPropertyMethod GetMethod
        {
            get
            {
                if (this.getMethod == null)
                    this.getMethod = this.OnGetMethod(PropertyMethodType.GetMethod);
                return this.getMethod;
            }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.GetMethod
        {
            get
            {
                return this.GetMethod;
            }
        }

        /// <summary>
        /// Returns the <typeparamref name="TPropertyMethod"/>
        /// which represents the set method of the 
        /// <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>.
        /// </summary>
        public TPropertyMethod SetMethod
        {
            get
            {
                if (this.setMethod == null)
                    this.setMethod = this.OnGetMethod(PropertyMethodType.SetMethod);
                return this.setMethod;
            }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.SetMethod
        {
            get
            {
                return this.SetMethod;
            }
        }

        #endregion

        /// <summary>
        /// Obtains the <typeparamref name="TPropertyMethod"/> for the <paramref name="methodType"/> provided.
        /// </summary>
        /// <param name="methodType">The type of property method to obtain the property wrapper 
        /// method for.</param>
        /// <returns>An instance of <typeparamref name="TPropertyMethod"/>
        /// if successful.</returns>
        protected abstract TPropertyMethod OnGetMethod(PropertyMethodType methodType);

        /// <summary>
        /// Obtains the <see cref="IType"/> that the
        /// <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> 
        /// is defined as.
        /// </summary>
        /// <returns>An <see cref="IType"/> that relates to the
        /// type associated to the 
        /// <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>.</returns>
        protected abstract IType OnGetPropertyType();

        /// <summary>
        /// Returns whether the <see cref="PropertySignatureMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// can cache the <see cref="PropertyType"/>.
        /// </summary>
        protected abstract bool CanCachePropertyType { get; }

    }
}
