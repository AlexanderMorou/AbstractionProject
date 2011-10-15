using System;
using System.Collections.Generic;
using System.Text;
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
                return AstIdentifier.Signature(this.Name, this.Parameters.ParameterTypes);
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
    }
}
