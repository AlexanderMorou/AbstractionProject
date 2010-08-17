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
    /// Provides a base implementation of an intermediate signature parameter member.
    /// </summary>
    /// <typeparam name="TSignature">The type of 
    /// <see cref="ISignatureMember{TSignature, TSignatureParameter, TSignatureParent}"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The type of 
    /// <see cref="IIntermediateSignatureMember{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> 
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of 
    /// <see cref="ISignatureParameterMember{TSignature, TSignatureParameter, TSignatureParent}"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParameter">The type of 
    /// <see cref="IIntermediateSignatureParameterMember{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> 
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParent">The type of 
    /// <see cref="ISignatureParent{TSignature, TSignatureParameter, TSignatureParent}"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParent">The type of 
    /// <see cref="IIntermediateSignatureParent{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> 
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateSignatureParameterMemberBase<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateParameterMemberBase<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter>,
        IIntermediateSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
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
        /// Creates a new <see cref="IntermediateSignatureParameterMemberBase{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> with the
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">Tbe <typeparamref name="TIntermediateSignature"/>
        /// which contains the <see cref="IntermediateSignatureParameterMemberBase{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/></param>
        public IntermediateSignatureParameterMemberBase(TIntermediateSignature parent) :
            base(parent)
        {
        }

        #region ISignatureParameterMember Members

        ISignatureMember ISignatureParameterMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IIntermediateSignatureParameterMember Members

        IIntermediateSignatureMember IIntermediateSignatureParameterMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion
    }
}
