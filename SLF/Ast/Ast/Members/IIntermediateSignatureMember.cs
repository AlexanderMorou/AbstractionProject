using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a commonly parametered
    /// intermediate member as a signature
    /// </summary>
    /// <typeparam name="TSignatureIdentifier">The kind of identifier used to differentiate the
    /// <typeparamref name="TIntermediateSignature"/> instance from
    /// its siblings.</typeparam>
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
    public interface IIntermediateSignatureMember<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateParameterParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter>,
        IIntermediateMember<TSignatureIdentifier, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateSignatureMember,
        ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
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
    }
    /// <summary>
    /// Defines properties and methods for working with a commonly parametered
    /// intermediate member as a signature
    /// </summary>
    public interface IIntermediateSignatureMember :
        IIntermediateParameterParent,
        IIntermediateMember,
        ISignatureMember
    {
    }
}
