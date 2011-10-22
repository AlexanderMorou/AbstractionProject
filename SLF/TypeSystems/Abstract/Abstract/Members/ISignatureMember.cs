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
    /// Defines generic properties and methods for working with a standard signature member that is a child
    /// to a <typeparamref name="TSignatureParent"/> and contains <typeparamref name="TSignatureParameter"/> 
    /// instances.
    /// </summary>
    /// <typeparam name="TSignature">The type of <see cref="ISignatureMember{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by the current implementation.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of <see cref="ISignatureParameterMember{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of <see cref="ISignatureParent{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// that contains <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    public interface ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> :
        IParameterParent<TSignature, TSignatureParameter>,
        IMember<TSignatureIdentifier, TSignatureParent>,
        ISignatureMember
        where TSignatureIdentifier :
            ISignatureMemberUniqueIdentifier<TSignatureIdentifier>
        where TSignature :
            ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working 
    /// with a commonly parametered member as a 
    /// signature (ie. Constructor, Event, Method,
    /// or Indexer Property).
    /// </summary>
    public interface ISignatureMember :
        IParameterParent,
        IMember
    {
    }
}
