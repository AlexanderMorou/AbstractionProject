using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a base implementation of an intermediate signature parameter member.
    /// </summary>
    /// <typeparam name="TSignatureIdentifier">The kind of identifier used to differentiate the
    /// <typeparamref name="TIntermediateSignature"/> instance from
    /// its siblings.</typeparam>
    /// <typeparam name="TSignature">The type of 
    /// <see cref="ISignatureMember{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The type of 
    /// <see cref="IIntermediateSignatureMember{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> 
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of 
    /// <see cref="ISignatureParameterMember{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParameter">The type of 
    /// <see cref="IIntermediateSignatureParameterMember{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> 
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParent">The type of 
    /// <see cref="ISignatureParent{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParent">The type of 
    /// <see cref="IIntermediateSignatureParent{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> 
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateSignatureParameterMemberBase<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateParameterMemberBase<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter>,
        IIntermediateSignatureParameterMember<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureIdentifier :
            ISignatureMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TSignature :
            ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignature :
            IIntermediateSignatureMember<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
            TSignature
        where TSignatureParameter :
            ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParameter :
            IIntermediateSignatureParameterMember<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParameter
        where TSignatureParent :
            ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            IIntermediateSignatureParent<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParent
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateSignatureParameterMemberBase{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> with the
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateSignature"/>
        /// which contains the <see cref="IntermediateSignatureParameterMemberBase{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/></param>
        /// <param name="identityManager">The <see cref="IIntermediateIdentityManager"/>
        /// which is responsible for maintaining type identity within the current type
        /// model.</param>
        public IntermediateSignatureParameterMemberBase(TIntermediateSignature parent, IIntermediateAssembly identityManager) :
            base(parent, identityManager)
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
