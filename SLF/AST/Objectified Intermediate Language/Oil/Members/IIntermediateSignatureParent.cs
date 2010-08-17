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
    /// Defines generic properties and methods for working with the parent
    /// of a series of signature members.
    /// </summary>
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
    public interface IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>,
        IIntermediateSignatureParent
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
