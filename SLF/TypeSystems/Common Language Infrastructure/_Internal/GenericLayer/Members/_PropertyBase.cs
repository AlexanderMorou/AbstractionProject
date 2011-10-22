using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    /// <summary>
    /// Provides a root level implementation for the generic overlayer to provide
    /// type-replacements for the type-parameters of a 
    /// <see cref="IPropertySignatureMember.PropertyType"/>.
    /// </summary>
    /// <typeparam name="TProperty">The type of <see cref="IPropertyMember{TProperty, TPropertyParent}"/>.</typeparam>
    /// <typeparam name="TPropertyParent">The type of <see cref="IPropertyParent{TProperty, TPropertyParent}"/>.</typeparam>
    internal abstract class _PropertyBase<TProperty, TPropertyParent> :
        _MemberBase<IGeneralMemberUniqueIdentifier, TProperty, TPropertyParent>,
        IPropertyMember<TProperty, TPropertyParent>
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
    {
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
        /// <summary>
        /// Data member for <see cref="GetMethod"/>.
        /// </summary>
        private IPropertyMethodMember getMethod;
        /// <summary>
        /// Data member representing the original get method.
        /// </summary>
        private IPropertyMethodMember _getMethod;
        /// <summary>
        /// Data member for <see cref="SetMethod"/>.
        /// </summary>
        private IPropertyMethodMember setMethod;
        /// <summary>
        /// Data member representing the original set method.
        /// </summary>
        private IPropertyMethodMember _setMethod;

        /// <summary>
        /// Creates a new <see cref="_PropertyBase{TProperty, TPropertyParent}"/>
        /// instance with the <paramref name="original"/> and 
        /// <paramref name="parent"/> instances.
        /// </summary>
        /// <param name="original">The <typeparamref name="TProperty"/> instance
        /// form which the <see cref="_PropertyBase{TProperty, TPropertyParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TPropertyParent"/>
        /// to which the <see cref="_PropertyBase{TProperty, TPropertyParent}"/> 
        /// is contained within.</param>
        public _PropertyBase(TProperty original, TPropertyParent parent) 
            : base(original, parent)
        {
        }

        #region IPropertyMember Members

        /// <summary>
        /// Returns the <see cref="IMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="_PropertyBase{TProperty, TPropertyParent}"/>.
        /// </summary>
        public IPropertyMethodMember GetMethod
        {
            get
            {
                if (!this.Original.CanRead)
                    return null;
                if (this.getMethod == null)
                {
                    this.getMethod = this.OnGetMethod(this.Original.GetMethod);
                    this._getMethod = this.Original.GetMethod;
                    this._getMethod.Disposed += new EventHandler(GetMethod_Disposed);
                }
                return this.getMethod;
            }
        }

        /// <summary>
        /// Returns the <see cref="IMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="_PropertyBase{TProperty, TPropertyParent}"/>.
        /// </summary>
        public IPropertyMethodMember SetMethod
        {
            get
            {
                if (!this.Original.CanWrite)
                    return null;
                if (this.setMethod == null)
                {
                    this.setMethod = this.OnGetMethod(this.Original.SetMethod);
                    this._setMethod = this.Original.SetMethod;
                    this._setMethod.Disposed += new EventHandler(SetMethod_Disposed);
                }
                return this.setMethod;
            }
        }

        protected abstract IPropertyMethodMember OnGetMethod(IPropertyMethodMember originalMethod);

        void GetMethod_Disposed(object sender, EventArgs e)
        {
            if (this.getMethod != null)
            {
                this._getMethod.Disposed -= new EventHandler(GetMethod_Disposed);
                this.getMethod.Dispose();
                this._getMethod = null;
                this.getMethod = null;
            }
        }

        void SetMethod_Disposed(object sender, EventArgs e)
        {
            if (this.setMethod != null)
            {
                this._setMethod.Disposed -= new EventHandler(SetMethod_Disposed);
                this.setMethod.Dispose();
                this._setMethod = null;
                this.setMethod = null;
            }
        }

        #endregion

        #region IPropertySignatureMember Members

        /// <summary>
        /// Returns the type that the <see cref="IPropertySignatureMember"/> is defined as.
        /// </summary>
        public IType PropertyType
        {
            get
            {
                if (Parent is IGenericType)
                {
                    IGenericType parent = ((IGenericType)(this.Parent));
                    if (parent.IsGenericConstruct && !parent.IsGenericDefinition)
                        return this.Original.PropertyType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                }
                return this.Original.PropertyType;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="IPropertySignatureMember"/>
        /// can be read from.
        /// </summary>
        public bool CanRead
        {
            get { return this.Original.CanRead; }
        }

        /// <summary>
        /// Returns whether the <see cref="IPropertySignatureMember"/> 
        /// can be written to.
        /// </summary>
        public bool CanWrite
        {
            get { return this.Original.CanWrite; }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        #endregion

        #region IExtendedInstanceMember Members

        public ExtendedInstanceMemberFlags InstanceFlags
        {
            get { return this.Original.InstanceFlags; }
        }

        public bool IsStatic
        {
            get { return this.Original.IsStatic; }
        }

        public bool IsAbstract
        {
            get { return this.Original.IsAbstract; }
        }

        public bool IsVirtual
        {
            get { return this.Original.IsVirtual; }
        }

        public bool IsHideBySignature
        {
            get { return this.Original.IsHideBySignature; }
        }

        public bool IsFinal
        {
            get { return this.Original.IsFinal; }
        }

        public bool IsOverride
        {
            get { return this.Original.IsOverride; }
        }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return this.Original.AccessLevel; }
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            if (this.getMethod != null)
            {
                this.getMethod.Dispose();
                this.getMethod = null;
            }
            if (this._getMethod != null)
            {
                this._getMethod.Disposed -= GetMethod_Disposed;
                this._getMethod = null;
            }
            if (this.setMethod != null)
            {
                this.setMethod.Dispose();
                this.setMethod = null;
            }
            if (this._setMethod != null)
            {
                this._setMethod.Disposed -= SetMethod_Disposed;
                this._setMethod = null;
            }
        }
        #region IInstanceMember Members

        InstanceMemberFlags IInstanceMember.InstanceFlags
        {
            get { return (InstanceMemberFlags)this.InstanceFlags; }
        }

        #endregion


        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = AstIdentifier.Member(this.Name);
                return this.uniqueIdentifier;
            }
        }
    }
}
