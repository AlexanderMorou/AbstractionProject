using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides a base implementation of <see cref="ISignatureMemberDictionary{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/>
    /// for working with a series of <typeparamref name="TSignature"/> instances contained by a 
    /// <typeparamref name="TSignatureParent"/>. 
    /// </summary>
    /// <typeparam name="TSignature">The type of <see cref="ISignatureMember{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by the current implementation.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of <see cref="ISignatureParameterMember{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of <see cref="ISignatureParent{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// that contains <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    internal abstract class SignatureMembersBase<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> :
        GroupedMembersBase<TSignatureParent, TSignatureIdentifier, TSignature>,
        ISignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>,
        ISignatureMemberDictionary
        where TSignatureIdentifier :
            ISignatureMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TSignature :
            ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        /// <summary>
        /// Creates a new <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>
        /// with the <paramref name="parent"/> provided
        /// </summary>
        /// <param name="master">The <see cref="FullMembersBase"/> 
        /// which moderates the 
        /// <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TSignatureParent"/> which contains the 
        /// <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>.</param>
        protected SignatureMembersBase(FullMembersBase master, TSignatureParent parent)
            : base(master, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>
        /// initialized to a default state.
        /// </summary>
        /// <param name="master">The <see cref="FullMembersBase"/> 
        /// which moderates the 
        /// <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>.</param>
        internal SignatureMembersBase(FullMembersBase master) :
            base(master)
        {

        }
    }
}
