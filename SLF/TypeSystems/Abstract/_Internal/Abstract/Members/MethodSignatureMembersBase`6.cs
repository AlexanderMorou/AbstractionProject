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
    internal class MethodSignatureMembersBase<TSignatureParameter, TSignature, TSignatureParent> :
        SignatureMembersBase<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>,
        IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>,
        IMethodSignatureMemberDictionary
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        /// <summary>
        /// Creates a new <see cref="MethodSignatureMembersBase{TSignatureParameter, TSignature, TSignatureParent}"/> initialized to a default
        /// state.
        /// </summary>
        internal MethodSignatureMembersBase(FullMembersBase master)
            : base(master)
        {
        }

        /// <summary>
        /// Creates a new <see cref="MethodSignatureMembersBase{TSignatureParameter, TSignature, TSignatureParent}"/> with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TSignatureParent"/> which contains the 
        /// <see cref="MethodSignatureMembersBase{TSignatureParameter, TSignature, TSignatureParent}"/>.</param>
        internal MethodSignatureMembersBase(FullMembersBase master, TSignatureParent parent) :
            base(master, parent)
        {

        }

        #region IMethodSignatureMemberDictionary Members

        int IMethodSignatureMemberDictionary.IndexOf(IMethodSignatureMember method)
        {
            if (!(method is TSignature))
                return -1;
            return this.IndexOf((TSignature)(method));
        }

        #endregion
    }
}
