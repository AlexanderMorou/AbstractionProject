using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a generic base class for working with an intermediate property member.
    /// </summary>
    /// <typeparam name="TProperty">The type of property as it exists int he
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property as it exists
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which owns the properties
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember> :
        IntermediateMemberBase<TPropertyParent, TIntermediatePropertyParent>,
        IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TPropertyParent :
            IPropertyParentType<TProperty, TPropertyParent>
        where TIntermediatePropertyParent :
            TPropertyParent,
            IIntermediatePropertyParentType<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TMethodMember :
            class,
            IIntermediatePropertyMethodMember
    {
        /// <summary>
        /// Data member for <see cref="AccessLevel"/>.
        /// </summary>
        private AccessLevelModifiers accessLevel;
        /// <summary>
        /// Data member for <see cref="InstanceFlags"/>.
        /// </summary>
        private ExtendedInstanceMemberFlags instanceFlags;
        /// <summary>
        /// Data member used for the <see cref="PropertyType"/> property.
        /// </summary>
        private IType propertyType;

        /// <summary>
        /// Data member for <see cref="GetMethod"/>.
        /// </summary>
        private TMethodMember getMethod;

        /// <summary>
        /// Data member for <see cref="SetMethod"/>.
        /// </summary>
        private TMethodMember setMethod;

        /// <summary>
        /// Data member for <see cref="CanRead"/>.
        /// </summary>
        private bool canRead;
        /// <summary>
        /// Data member for <see cref="CanWrite"/>.
        /// </summary>
        private bool canWrite;

        /// <summary>
        /// Creates a new <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the 
        /// unique name of the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.
        /// </param>
        /// <param name="parent">The <typeparamref name="TIntermediatePropertyParent"/>
        /// which contains the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.</param>
        public IntermediatePropertyMember(string name, TIntermediatePropertyParent parent)
            : base(parent)
        {
            base.OnSetName(name);
        }

        /// <summary>
        /// Creates a new <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediatePropertyParent"/>
        /// which contains the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.</param>
        public IntermediatePropertyMember(TIntermediatePropertyParent parent)
            : base(parent)
        {
        }

        #region IIntermediatePropertySignatureMember Members

        /// <summary>
        /// Returns/sets the type that the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
        /// is defined as.
        /// </summary>
        public IType PropertyType
        {
            get
            {
                return this.propertyType;
            }
            set
            {
                this.propertyType = value;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
        /// can be read from.
        /// </summary>
        /// <remarks>If set to false, from true, the <see cref="IIntermediateMethodSignatureMember"/> for the <see cref="GetMethod"/>
        /// will be disposed.</remarks>
        public bool CanRead
        {
            get
            {
                return this.canRead;
            }
            set
            {
                if (!value && this.canRead && this.getMethod != null)
                {
                    this.getMethod.Dispose();
                    this.getMethod = null;
                }
                this.canRead = value;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/> 
        /// can be written to.
        /// </summary>
        /// <remarks>If set to false, from true, the <see cref="IIntermediateMethodSignatureMember"/> for the <see cref="SetMethod"/>
        /// will be disposed.</remarks>
        public bool CanWrite
        {
            get
            {
                return this.canWrite;
            }
            set
            {
                if (!value && this.canWrite && this.setMethod != null)
                {
                    this.setMethod.Dispose();
                    this.setMethod = null;
                }
                this.canWrite = value;
            }
        }

        IIntermediatePropertySignatureMethodMember IIntermediatePropertySignatureMember.GetMethod
        {
            get {
                return this.GetMethod;
            }
        }

        IIntermediatePropertySignatureMethodMember IIntermediatePropertySignatureMember.SetMethod
        {
            get
            {
                return this.SetMethod;
            }
        }

        #endregion

        #region IPropertySignatureMember Members

        IPropertySignatureMethodMember IPropertySignatureMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        #endregion

        protected abstract TMethodMember GetMethodMember(PropertyMethodType methodType);

        #region IIntermediatePropertyMember Members
        IIntermediatePropertyMethodMember IIntermediatePropertyMember.GetMethod
        {
            get
            {
                return this.GetMethod;
            }
        }

        IIntermediatePropertyMethodMember IIntermediatePropertyMember.SetMethod
        {
            get
            {
                return this.SetMethod;
            }
        }
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanRead"/> is false.</remarks>
        public TMethodMember GetMethod
        {
            get {
                if (this.canRead)
                {
                    if (this.getMethod == null)
                        this.getMethod = this.GetMethodMember(PropertyMethodType.GetMethod);
                    return this.getMethod;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanWrite"/> is false.</remarks>
        public TMethodMember SetMethod
        {
            get
            {
                if (this.canWrite)
                {
                    if (this.setMethod == null)
                        this.setMethod = this.GetMethodMember(PropertyMethodType.SetMethod);
                    return this.setMethod;
                }
                else
                    return null;
            }
        }

        #endregion

        #region IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember} Members

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/> is 
        /// abstract (must be implemented, or is not yet 
        /// implemented).
        /// </summary>
        public bool IsAbstract
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Abstract) == ExtendedInstanceMemberFlags.Abstract);
            }
            set
            {
                if (IsAbstract == value)
                    return;
                if (value)
                {
                    if (this.IsStatic)
                        this.IsStatic = false;
                    if (this.IsVirtual)
                        this.IsVirtual = false;
                    if (this.IsOverride)
                        this.IsOverride = false;
                    if (this.IsFinal)
                        this.IsFinal = false;
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Abstract;
                }
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Abstract;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/> is
        /// virtual (can be overridden).
        /// </summary>
        public bool IsVirtual
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Virtual) == ExtendedInstanceMemberFlags.Virtual);
            }
            set
            {
                if (IsVirtual == value)
                    return;
                if (value)
                {
                    if (this.IsStatic)
                        this.IsStatic = false;
                    if (this.IsAbstract)
                        this.IsAbstract = false;
                    if (this.IsOverride)
                        this.IsOverride = false;
                    if (this.IsFinal)
                        this.IsFinal = false;
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Virtual;
                }
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Virtual;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
        /// finalizes the member removing the overrideable 
        /// status.
        /// </summary>
        public bool IsFinal
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Final) == ExtendedInstanceMemberFlags.Final);
            }
            set
            {
                if (IsFinal == value)
                    return;
                if (value)
                {
                    if (this.IsVirtual)
                        this.IsVirtual = false;
                    if (this.IsAbstract)
                        this.IsAbstract = false;
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Final;
                }
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Final;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/> 
        /// is an override of a virtual member.
        /// </summary>
        public bool IsOverride
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Override) == ExtendedInstanceMemberFlags.Override);
            }
            set
            {
                if (IsOverride == value)
                    return;
                if (value)
                {
                    if (this.IsStatic)
                        this.IsStatic = false;
                    if (this.IsAbstract)
                        this.IsAbstract = false;
                    if (this.IsVirtual)
                        this.IsVirtual = false;
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Override;
                }
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Override;
            }
        }

        #endregion

        #region IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember} Members

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
        /// hides the original definition completely.
        /// </summary>
        public bool IsHideBySignature
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.HideBySignature) == ExtendedInstanceMemberFlags.HideBySignature);
            }
            set
            {
                if (value == IsHideBySignature)
                    return;
                if (value)
                {
                    this.instanceFlags |= ExtendedInstanceMemberFlags.HideBySignature;
                }
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.HideBySignature;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/> is
        /// static.
        /// </summary>
        public bool IsStatic
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Static) == ExtendedInstanceMemberFlags.Static);
            }
            set
            {
                if (this.IsStatic == value)
                    return;
                if (value)
                {
                    if (this.IsAbstract)
                        this.IsAbstract = false;
                    if (this.IsVirtual)
                        this.IsVirtual = false;
                    if (this.IsOverride)
                        this.IsOverride = false;
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Static;
                }
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Static;
            }
        }

        #endregion

        #region IInstanceMember Members

        InstanceMemberFlags IInstanceMember.InstanceFlags
        {
            get { return ((InstanceMemberFlags)(this.InstanceFlags)); }
        }

        #endregion

        #region IExtendedInstanceMember Members

        /// <summary>
        /// Returns the <see cref="ExtendedInstanceMemberFlags"/> that determine how the
        /// <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>
        /// is shown in its scope and inherited scopes.
        /// </summary>
        public ExtendedInstanceMemberFlags InstanceFlags
        {
            get { return this.instanceFlags; }
        }

        #endregion

        #region IPropertyMember Members

        IPropertyMethodMember IPropertyMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IPropertyMethodMember IPropertyMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        #endregion

        #region IIntermediateScopedDeclaration Members

        /// <summary>
        /// Returns/sets the access level of the
        /// <see cref="IntermediatePropertyMember{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent, TMethodMember}"/>.
        /// </summary>
        public AccessLevelModifiers AccessLevel
        {
            get
            {
                return this.accessLevel;
            }
            set
            {
                this.accessLevel = value;
            }
        }

        #endregion
    }
}
