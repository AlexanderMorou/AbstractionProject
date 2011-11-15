using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the parent
    /// of a series of signature members.
    /// </summary>
    /// <typeparam name="TSignatureIdentifier">The kind of identifier used to differentiate the
    /// <typeparamref name="TIntermediateSignature"/> instance from
    /// its siblings.</typeparam>
    /// <typeparam name="TSignature">The type of signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The type of signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParameter">The type used to represent the parameters
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParameter">The type used to represent the parameters
    /// of the signature in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParent">The type of signature parent in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParent">The type of signature parent
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateSignatureParent<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>,
        IIntermediateSignatureParent
        where TSignatureIdentifier :
            ISignatureMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TSignature :
            ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateSignatureMember<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateSignatureParameterMember<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            IIntermediateSignatureParent<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParent
    {
    }
    /// <summary>
    /// Defines properties and methods for working with the parent of a
    /// series of signature members.
    /// </summary>
    public interface IIntermediateSignatureParent :
        IIntermediateMemberParent,
        ISignatureParent
    {
    }
}
