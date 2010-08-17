using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal abstract class SignatureMemberBase<TSignature, TSignatureParameter, TSignatureParent> :
        MemberBase<TSignatureParent>,
        ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>,
        ISignatureMember
        where TSignature :
            ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
    {
        private IParameterMemberDictionary<TSignature, TSignatureParameter> parameters;

        /// <summary>
        /// Creates a new <see cref="SignatureMemberBase{TSignature, TSignatureParameter, TSignatureParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The parent of the 
        /// <see cref="SignatureMemberBase{TSignature, TSignatureParameter, TSignatureParent}"/>.</param>
        public SignatureMemberBase(TSignatureParent parent)
            : base(parent)
        {
        }

        #region IParameterParent<TSignature,TSignatureParameter> Members

        /// <summary>
        /// Returns the dictionary of <typeparamref name="TSignatureParameter"/> instances for the current 
        /// <typeparamref name="TSignatureParent"/> implementation.
        /// </summary>
        public IParameterMemberDictionary<TSignature, TSignatureParameter> Parameters
        {
            get {
                if (this.parameters == null)
                    this.parameters = this.InitializeParameters();
                return this.parameters;
            }
        }

        /// <summary>
        /// Initializes the <see cref="IParameterMemberDictionary{TSignature, TSignatureParameter}"/> used
        /// to store the <typeparamref name="TSignatureParameter"/> instances.
        /// </summary>
        /// <returns>A new <see cref="IParameterMemberDictionary{TSignature, TSignatureParameter}"/> instance.</returns>
        protected abstract IParameterMemberDictionary<TSignature, TSignatureParameter> InitializeParameters();

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return (IParameterMemberDictionary)this.Parameters; }
        }

        #endregion

        /// <summary>
        /// Returns the unique identifier for the current 
        /// <see cref="SignatureMemberBase{TSignature, TSignatureParameter, TSignatureParent}"/> where 
        /// <see cref="DeclarationBase.Name"/> is not enough to distinguish between two <see cref="IDeclaration"/> entities.
        /// </summary>
        public override string UniqueIdentifier
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                bool first = true;
                foreach (TSignatureParameter param in this.Parameters.Values)
                {
                    if (first)
                        first = false;
                    else
                        sb.Append(", ");
                    sb.Append(param.ParameterType.BuildTypeName(true));
                }
                return sb.ToString();
            }
        }
        public override string ToString()
        {
            return this.UniqueIdentifier;
        }

        #region ISignatureMember Members

        public bool LastIsParams
        {
            get
            {
                return this.LastIsParamsImpl;
            }
        }

        #endregion

        protected abstract bool LastIsParamsImpl { get; }

    }
}
