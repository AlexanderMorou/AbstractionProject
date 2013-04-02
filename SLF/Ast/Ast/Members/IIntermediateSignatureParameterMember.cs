using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the parameter
    /// of a signatured member.
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
    public interface IIntermediateSignatureParameterMember<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateParameterMember<TSignature, TIntermediateSignature>,
        IIntermediateSignatureParameterMember,
        ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
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
    /// Defines properties and methods for working with the parameter of a signatured member.
    /// </summary>
    public interface IIntermediateSignatureParameterMember :
        IIntermediateParameterMember,
        ISignatureParameterMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="IIntermediateSignatureParameterMember"/>.
        /// </summary>
        new IIntermediateSignatureMember Parent { get; }
    }
}
