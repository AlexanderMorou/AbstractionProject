using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides an internal base class for method signature members.
    /// </summary>
    /// <typeparam name="TSignature">The type of method signature in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The typw which owns the
    /// <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    internal abstract class MethodSignatureMemberBase<TSignature, TSignatureParent> :
        MethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {
        /// <summary>
        /// Creates a new <see cref="MethodSignatureMemberBase{TSignature, TSignatureParent}"/>
        /// </summary>
        /// <param name="parent">The <typeparamref name="TSignatureParent"/> which owns
        /// the <see cref="MethodSignatureMemberBase{TSignature, TSignatureParent}"/>.</param>
        protected MethodSignatureMemberBase(TSignatureParent parent)
            : base(parent)
        {
        }

    }
}
