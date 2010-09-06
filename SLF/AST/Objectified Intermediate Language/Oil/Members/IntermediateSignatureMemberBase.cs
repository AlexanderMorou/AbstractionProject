using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides an implementation of an intermediate signature member.
    /// </summary>
    /// <typeparam name="TSignature">The type of the signature as defined in the 
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The type of the signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of the parameters of the
    /// <typeparamref name="TSignature"/> in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParameter">The type of the parameters of the
    /// <typeparamref name="TIntermediateSignature"/> in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParent">The type, in the
    /// abstract type system, which contains the signatured elements.</typeparam>
    /// <typeparam name="TIntermediateSignatureParent">The type, in the intermediate abstract syntax tree,
    /// which contains the signatured elements.</typeparam>
    public abstract class IntermediateSignatureMemberBase<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateParameterParentMemberBase<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent,TIntermediateSignatureParent>,
        IIntermediateSignatureMember<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        where TSignature :
            ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateSignatureMember<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            TSignatureParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateSignatureMemberBase{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateSignatureParent"/> which
        /// contains the <see cref="IntermediateSignatureMemberBase{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>.</param>
        protected IntermediateSignatureMemberBase(TIntermediateSignatureParent parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateSignatureMemberBase{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the unique identifier of the 
        /// <see cref="IntermediateSignatureMemberBase{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateSignatureParent"/> which
        /// contains the <see cref="IntermediateSignatureMemberBase{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>.</param>
        protected IntermediateSignatureMemberBase(string name, TIntermediateSignatureParent parent)
            : base(name, parent)
        {
        }

        public override string UniqueIdentifier
        {
            get
            {
                return string.Format("{0}{1}", base.UniqueIdentifier, UniqueIdentifier_Parameters());

            }
        }

        private string uniqueIdentifier_Parameters_Cache = null;

        protected string UniqueIdentifier_Parameters()
        {
            if (this.Parameters.Changed || uniqueIdentifier_Parameters_Cache == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("(");
                bool first = true;
                foreach (var param in this.Parameters.Values)
                {
                    if (first)
                        first = false;
                    else
                        sb.Append(", ");
                    sb.Append(param.UniqueIdentifier);
                }
                sb.Append(")");
                uniqueIdentifier_Parameters_Cache = sb.ToString();
            }

            return uniqueIdentifier_Parameters_Cache;
        }
    }
}
