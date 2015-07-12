using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a property signature
    /// member dictionary.
    /// </summary>
    /// <typeparam name="TSignatureParameter">The type of parameter for the <typeparamref name="TSignature"/>
    /// instance.</typeparam>
    /// <typeparam name="TSignature">The type of signature used in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of signature parent which contains the 
    /// <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    public interface IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent> :
        ISignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>,
        IMemberDictionary<TSignatureParent, IGeneralGenericSignatureMemberUniqueIdentifier, TSignature>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
    }

    /// <summary>
    /// Defines generic properties and methods for working with a dictionary
    /// of method signatures.
    /// </summary>
    /// <typeparam name="TSignature">The type of signature member in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of signature parent in the current implementation.</typeparam>
    public interface IMethodSignatureMemberDictionary<TSignature, TSignatureParent> :
        IMethodSignatureMemberDictionary<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>,
        IGroupedMemberDictionary<TSignatureParent, IGeneralGenericSignatureMemberUniqueIdentifier, TSignature>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a dictionary of method signatures.
    /// </summary>
    public interface IMethodSignatureMemberDictionary :
        ISignatureMemberDictionary,
        IGroupedMemberDictionary
    {
        /// <summary>
        /// Returns the index of the <paramref name="method"/> provided.
        /// </summary>
        /// <param name="method">The <see cref="IMethodSignatureMember"/> in the <see cref="IMethodSignatureMemberDictionary"/> to return
        /// the index of.</param>
        /// <returns>An <see cref="Int32"/> value representing the index of the <paramref name="method"/> in the
        /// <see cref="IMethodSignatureMemberDictionary"/>, if present; -1, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="method"/> is null.</exception>
        int IndexOf(IMethodSignatureMember method);
    }
}
