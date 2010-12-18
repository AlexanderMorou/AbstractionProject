using System;
using System.Collections.Generic;
using System.Text;
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

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    /// <summary>
    /// Provides a root level implementation for the generic overlayer to provide
    /// type-replacements for the type-parameters of a 
    /// <see cref="IPropertySignatureMember.PropertyType"/>.
    /// </summary>
    /// <typeparam name="TPropertySignature">The type of <see cref="IPropertySignatureMember{TPropertySignature, TPropertySignatureParent}"/>.</typeparam>
    /// <typeparam name="TPropertySignatureParent">The type of <see cref="IPropertySignatureParentType{TPropertySignature, TPropertySignatureParent}"/>.</typeparam>
    internal abstract class _PropertySignatureBase<TPropertySignature, TPropertySignatureParent> :
        _MemberBase<TPropertySignature, TPropertySignatureParent>,
        IPropertySignatureMember<TPropertySignature, TPropertySignatureParent>
        where TPropertySignature :
            IPropertySignatureMember<TPropertySignature, TPropertySignatureParent>
        where TPropertySignatureParent :
            IPropertySignatureParentType<TPropertySignature, TPropertySignatureParent>
    {
        /// <summary>
        /// Data member for <see cref="GetMethod"/>.
        /// </summary>
        private IPropertySignatureMethodMember getMethod;
        /// <summary>
        /// Data member representing the original get method.
        /// </summary>
        private IPropertySignatureMethodMember _getMethod;
        /// <summary>
        /// Data member for <see cref="SetMethod"/>.
        /// </summary>
        private IPropertySignatureMethodMember setMethod;
        /// <summary>
        /// Data member representing the original set method.
        /// </summary>
        private IPropertySignatureMethodMember _setMethod;

        /// <summary>
        /// Creates a new <see cref="_PropertySignatureBase{TPropertySignature, TPropertySignatureParent}"/>
        /// instance with the <paramref name="original"/> and 
        /// <paramref name="parent"/> instances.
        /// </summary>
        /// <param name="original">The <typeparamref name="TPropertySignature"/> instance
        /// form which the <see cref="_PropertySignatureBase{TPropertySignature, TPropertySignatureParent}"/>.</param>
        /// <param name="parent">The <paramref name="TPropertySignatureParent"/>
        /// to which the <see cref="_PropertySignatureBase{TPropertySignature, TPropertySignatureParent}"/> 
        /// is contained within.</param>
        public _PropertySignatureBase(TPropertySignature original, TPropertySignatureParent parent)
            : base(original, parent)
        {
        }

        #region IPropertySignatureMember Members

        /// <summary>
        /// Returns the <see cref="IMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="_PropertySignatureBase{TPropertySignature, TPropertySignatureParent}"/>.
        /// </summary>
        public IPropertySignatureMethodMember GetMethod
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
        /// <see cref="_PropertySignatureBase{TPropertySignature, TPropertySignatureParent}"/>.
        /// </summary>
        public IPropertySignatureMethodMember SetMethod
        {
            get
            {
                if (!this.Original.CanRead)
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

        protected abstract IPropertySignatureMethodMember OnGetMethod(IPropertySignatureMethodMember originalMethod);

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

        #endregion
    }
}
