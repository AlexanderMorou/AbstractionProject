using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _EventSignatureMemberBase<TEvent, TEventParameter, TEventParent> :
        _MemberBase<IGeneralSignatureMemberUniqueIdentifier, TEvent, TEventParent>,
        IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {
        private IParameterMemberDictionary<TEvent, TEventParameter> parameters;
        private IMethodSignatureMember onAddMethod;
        private IMethodSignatureMember _onAddMethod;
        private IMethodSignatureMember onRemoveMethod;
        private IMethodSignatureMember _onRemoveMethod;
        internal _EventSignatureMemberBase(TEvent original, TEventParent adjustedParent)
            : base(original, adjustedParent)
        {
        }

        #region IParameterParent<TEvent,TEventParameter> Members

        public IParameterMemberDictionary<TEvent, TEventParameter> Parameters
        {
            get
            {
                if (this.parameters == null)
                    this.parameters = this.InitializeParameters();
                return this.parameters;
            }
        }

        /// <summary>
        /// Initializes the <see cref="Parameters"/> member.
        /// </summary>
        /// <returns>A <see cref="IParameterMemberDictionary{TParent, TParameter}"/>
        /// instance that represents the parameters for the 
        /// <see cref="_EventSignatureMemberBase{TEvent, TEventParameter, TEventParent}"/>.
        /// </returns>
        protected abstract IParameterMemberDictionary<TEvent, TEventParameter> InitializeParameters();

        protected abstract IMethodSignatureMember OnGetMethod(IMethodSignatureMember original);

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

        #region IEventSignatureMember Members

        public IMethodSignatureMember OnAddMethod
        {
            get
            {
                if (this.onAddMethod == null)
                {
                    IMethodSignatureMember origOnAddMethod = this.Original.OnAddMethod;
                    _onAddMethod = origOnAddMethod;
                    _onAddMethod.Disposed += new EventHandler(_onAddMethod_Disposed);
                    onAddMethod = this.OnGetMethod(Original.OnAddMethod);
                }
                return this.onAddMethod;
            }
        }
        void _onAddMethod_Disposed(object sender, EventArgs e)
        {
            if (this._onAddMethod != null)
            {
                _onAddMethod.Disposed -= new EventHandler(_onAddMethod_Disposed);
                this._onAddMethod = null;
            }
            if (this.onAddMethod != null)
            {
                this.onAddMethod.Dispose();
                this.onAddMethod = null;
            }
        }

        public IMethodSignatureMember OnRemoveMethod
        {
            get
            {
                if (this.onRemoveMethod == null)
                {
                    IMethodSignatureMember origOnRemoveMethod = this.Original.OnRemoveMethod;
                    _onRemoveMethod = origOnRemoveMethod;
                    _onRemoveMethod.Disposed += new EventHandler(_onRemoveMethod_Disposed);
                    onRemoveMethod = this.OnGetMethod(Original.OnRemoveMethod);
                }
                return this.onRemoveMethod;
            }
        }
        void _onRemoveMethod_Disposed(object sender, EventArgs e)
        {
            if (this._onRemoveMethod != null)
            {
                _onRemoveMethod.Disposed -= new EventHandler(_onRemoveMethod_Disposed);
                this._onRemoveMethod = null;
            }
            if (this.onRemoveMethod != null)
            {
                this.onRemoveMethod.Dispose();
                this.onRemoveMethod = null;
            }
        }

        public EventSignatureSource SignatureSource
        {
            get { return this.Original.SignatureSource; }
        }

        public IDelegateType SignatureType
        {
            get
            {
                if (Parent is IGenericType)
                {
                    IGenericType parent = ((IGenericType)(this.Parent));
                    if (parent.IsGenericConstruct && !parent.IsGenericDefinition)
                        return (IDelegateType)this.Original.SignatureType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                }
                return this.Original.SignatureType;
            }
        }

        #endregion

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                return TypeSystemIdentifiers.GetSignatureIdentifier(this.Name, this.Parameters.ParameterTypes);
            }
        }

        #region IEventSignatureMember Members


        public IType ReturnType
        {
            get {
                if (Parent is IGenericType)
                {
                    IGenericType parent = ((IGenericType)(this.Parent));
                    if (parent.IsGenericConstruct && !parent.IsGenericDefinition)
                        return (IDelegateType)this.Original.ReturnType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                }
                return this.Original.ReturnType;
            }
        }

        #endregion

        #region IMetadataEntity Members

        public IMetadataCollection Metadata
        {
            get { return this.Original.Metadata; }
        }

        public bool IsDefined(IType metadatumType)
        {
            return this.Original.IsDefined(metadatumType);
        }

        #endregion
    }
}
