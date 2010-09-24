using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides a base abstract implementation of 
    /// <see cref="IPropertyMember{TProperty, TPropertyParent}"/> 
    /// to work with a property member.
    /// </summary>
    /// <typeparam name="TProperty">The type of property used in the current implementation.</typeparam>
    /// <typeparam name="TPropertyParent">The type of parent that contains the <see cref="IPropertyMember"/> 
    /// instances in the current implementation.</typeparam>
    /// <typeparam name="TMethod">The type of method used in the current implementation.</typeparam>
    /// <typeparam name="TMethodParent">The type which owns the <typeparamref name="TMethod"/>
    /// instances in the current implementation.</typeparam>
    /// <typeparam name="TPropertyMethod">The type which acts as the 
    /// property method members.</typeparam>
    internal abstract class PropertyMemberBase<TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent> :
        MemberBase<TPropertyParent>,
        IPropertyMember<TProperty, TPropertyParent>
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParentType<TProperty, TPropertyParent>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
        where TPropertyMethod :
            TMethod, 
            IPropertyMethodMember
    {
        /// <summary>
        /// Data member for <see cref="PropertyType"/>.
        /// </summary>
        private IType propertyType;
        private TPropertyMethod getMethod;
        private TPropertyMethod setMethod;
        public PropertyMemberBase(TPropertyParent parent)
            : base(parent)
        {
        }

        #region IPropertySignatureMember Members

        /// <summary>
        /// Returns the type that the 
        /// <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> 
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
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// can be read from.
        /// </summary>
        public virtual bool CanRead
        {
            get { return this.GetMethod != null; }
        }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> 
        /// can be written to.
        /// </summary>
        public bool CanWrite
        {
            get { return this.SetMethod != null; }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.GetMethod
        {
            get { 
                return this.GetMethod; }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// can cache the <see cref="PropertyType"/>.
        /// </summary>
        protected abstract bool CanCachePropertyType { get; }

        #endregion

        #region IPropertyMember Members

        /// <summary>
        /// Returns the <see cref="IPropertyMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>.
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

        IPropertyMethodMember IPropertyMember.GetMethod
        {
            get {
                return this.GetMethod;
            }
        }

        /// <summary>
        /// Returns the <see cref="IPropertyMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>.
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

        IPropertyMethodMember IPropertyMember.SetMethod
        {
            get
            {
                return this.SetMethod;
            }
        }

        /// <summary>
        /// Obtains the <see cref="IType"/> that the
        /// <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> 
        /// is defined as.
        /// </summary>
        /// <returns>An <see cref="IType"/> that relates to the
        /// type associated to the 
        /// <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>.</returns>
        protected abstract IType OnGetPropertyType();

        /// <summary>
        /// Obtains the <typeparamref name="TPropertyMethod"/> for the <paramref name="methodType"/> provided.
        /// </summary>
        /// <param name="methodType">The type of property method to obtain the property wrapper 
        /// method for.</param>
        /// <returns>An instance of <typeparamref name="TPropertyMethod"/>
        /// if successful.</returns>
        protected abstract TPropertyMethod OnGetMethod(PropertyMethodType methodType);

        #endregion

        public override string ToString()
        {
            return string.Format("{0} {1} {{ {2}{3}{4} }}", this.PropertyType.Name, this.Name, this.CanRead ? "get;" : string.Empty, this.CanRead && this.CanWrite ? " " : string.Empty, this.CanWrite ? "set;" : string.Empty);
        }

        #region IExtendedInstanceMember Members

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> is
        /// static.
        /// </summary>
        public abstract bool IsStatic { get; }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> is
        /// virtual (can be overridden).
        /// </summary>
        public abstract bool IsVirtual { get; }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// hides the original definition completely.
        /// </summary>
        public abstract bool IsHideBySignature { get; }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// finalizes the member removing the overrideable 
        /// status.
        /// </summary>
        public abstract bool IsFinal { get; }

        /// <summary>
        /// Returns whether the <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/> 
        /// is an override of a virtual member.
        /// </summary>
        public abstract bool IsOverride { get; }

        /// <summary>
        /// Returns whether the
        /// <see cref="PropertyMemberBase{TProperty, TPropertyParent, TPropertyMethod, TMethod, TMethodParent}"/>
        /// is abstract (must be implemented, or is
        /// not yet implemented).
        /// </summary>
        public abstract bool IsAbstract { get; }

        /// <summary>
        /// Returns the <see cref="ExtendedInstanceMemberFlags"/> that determine how the
        /// <see cref="IExtendedInstanceMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        public ExtendedInstanceMemberFlags InstanceFlags
        {
            get
            {
                ExtendedInstanceMemberFlags imfs = ExtendedInstanceMemberFlags.None;
                if (this.IsStatic)
                    imfs |= ExtendedInstanceMemberFlags.Static;
                if (this.IsVirtual)
                    imfs |= ExtendedInstanceMemberFlags.Virtual;
                if (this.IsOverride)
                    imfs |= ExtendedInstanceMemberFlags.Override;
                if (this.IsFinal)
                    imfs |= ExtendedInstanceMemberFlags.Final;
                if (this.IsHideBySignature)
                    imfs |= ExtendedInstanceMemberFlags.HideBySignature;
                if (this.IsAbstract)
                    imfs |= ExtendedInstanceMemberFlags.Abstract;
                return imfs;
            }
        }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return this.AccessLevelImpl; }
        }

        #endregion

        protected abstract AccessLevelModifiers AccessLevelImpl { get; }
        #region IInstanceMember Members

        InstanceMemberFlags IInstanceMember.InstanceFlags
        {
            get { return (InstanceMemberFlags)this.InstanceFlags; }
        }

        #endregion
    }
}
