using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class SignatureMemberBase<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> :
        MemberBase<TSignatureIdentifier, TSignatureParent>,
        ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>,
        ISignatureMember
        where TSignatureIdentifier :
            ISignatureMemberUniqueIdentifier<TSignatureIdentifier>
        where TSignature :
            ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
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

        public override string ToString()
        {
            return this.UniqueIdentifier.ToString();
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
