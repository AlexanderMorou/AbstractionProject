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
    /// Defines generic properties and methods for working with a commonly parametered
    /// intermediate member as a signature
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
    public interface IIntermediateSignatureMember<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateParameterParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter>,
        IIntermediateMember<TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateSignatureMember,
        ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
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
    /// Defines properties and methods for working with a commonly parametered
    /// intermediate member as a signature
    /// </summary>
    public interface IIntermediateSignatureMember :
        IIntermediateMember,
        IIntermediateParameterParent,
        ISignatureMember
    {
    }
}
