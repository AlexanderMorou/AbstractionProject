using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a parameter for a 
    /// <typeparamref name="TSignature"/>.
    /// </summary>
    /// <typeparam name="TSignature">The type of <see cref="ISignatureMember{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by the current implementation.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of <see cref="ISignatureParameterMember{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of <see cref="ISignatureParent{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// that contains <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    public interface ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> :
        IParameterMember<TSignature>,
        ISignatureParameterMember
        where TSignatureIdentifier :
            ISignatureMemberUniqueIdentifier
        where TSignature :
            ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignatureIdentifier,TSignature, TSignatureParameter, TSignatureParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a parameter of a <see cref="ISignatureMember"/>
    /// implementation.
    /// </summary>
    public interface ISignatureParameterMember :
        IParameterMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="ISignatureParameterMember"/>.
        /// </summary>
        new ISignatureMember Parent { get; }
    }
}
