using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
    /// <typeparam name="TSignature">The type of <see cref="ISignatureMember{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by the current implementation.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of <see cref="ISignatureParameterMember{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of <see cref="ISignatureParent{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// that contains <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    public interface ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent> :
        IParameterMember<TSignature>,
        ISignatureParameterMember
        where TSignature :
            ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
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
