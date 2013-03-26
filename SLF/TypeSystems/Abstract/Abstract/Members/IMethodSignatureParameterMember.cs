using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the parameter of a method
    /// signature.
    /// </summary>
    /// <typeparam name="TSignature">The type of signature used as a parent of the parameters in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TSignatureParent">The type of parent which contains the <typeparamref name="TSignature"/>
    /// instances.</typeparam>
    public interface IMethodSignatureParameterMember<TSignature, TSignatureParent> :
        IMethodSignatureParameterMember<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {
    }
    /// <summary>
    /// Defines generic properties and methods for working with
    /// the parameter of a method signature.
    /// </summary>
    /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
    /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
    /// <typeparam name="TSignatureParent">The parent that contains the <typeparamref name="TSignature"/> 
    /// instances.</typeparam>
    /// <remarks>Common interface for full method signature members and 
    /// method members.</remarks>
    public interface IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent> :
        ISignatureParameterMember<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>,
        IMethodSignatureParameterMember
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with the parameter of a method signature.
    /// </summary>
    public interface IMethodSignatureParameterMember :
        ISignatureParameterMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="IMethodSignatureParameterMember"/>.
        /// </summary>
        new IMethodSignatureMember Parent { get; }
    }

}
