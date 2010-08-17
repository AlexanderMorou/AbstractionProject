using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract partial class _IndexerSignatureMemberBase<TIndexer, TIndexerParent> :
        _MemberBase<TIndexer, TIndexerParent>,
        IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {
        private _Parameters parameters;
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
        protected _IndexerSignatureMemberBase(TIndexer original, TIndexerParent adjustedParent)
            : base(original, adjustedParent)
        {
        }

        #region IParameterParent<TIndexer,IIndexerSignatureParameterMember<TIndexer,TIndexerParent>> Members

        public IParameterMemberDictionary<TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>> Parameters
        {
            get
            {
                if (this.parameters == null)
                    this.parameters = new _Parameters(this, this.Original.Parameters);
                return this.parameters;
            }
        }

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return ((IParameterMemberDictionary)(this.Parameters)); }
        }

        public bool LastIsParams
        {
            get { return this.Original.LastIsParams; }
        }

        #endregion

        #region IPropertySignatureMember Members

        /// <summary>
        /// Returns the <see cref="IMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="_IndexerSignatureMemberBase{TIndexer, TIndexerParent}"/>.
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
        /// <see cref="_IndexerSignatureMemberBase{TIndexer, TIndexerParent}"/>.
        /// </summary>
        public IPropertySignatureMethodMember SetMethod
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

        public IType PropertyType
        {
            get
            {
                if (Parent is IGenericType)
                {
                    IGenericType parent = ((IGenericType)(this.Parent));
                    if (parent.IsGenericType && !parent.IsGenericTypeDefinition)
                        return this.Original.PropertyType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                }
                return this.Original.PropertyType;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="_IndexerSignatureMemberBase{TIndexer, TIndexerParent}"/>
        /// can be read from.
        /// </summary>
        public bool CanRead
        {
            get { return this.Original.CanRead; }
        }

        /// <summary>
        /// Returns whether the <see cref="_IndexerSignatureMemberBase{TIndexer, TIndexerParent}"/> 
        /// can be written to.
        /// </summary>
        public bool CanWrite
        {
            get { return this.Original.CanWrite; }
        }

        #endregion
    }
}
