using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>,
        IMemberDictionary<TSignatureParent, TSignature>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
    {
        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, ITypeCollection search);
        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, ITypeCollection)"/> where strict is true.</remarks>
        IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection search);
        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, params IType[] search);
        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, IType[])"/> where strict is true.</remarks>
        IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, params IType[] search);


        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="genericParameters">The number of generic parameters the <typeparamref name="TSignature"/> instances
        /// contain.</param>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, ITypeCollection search);
        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="genericParameters">The number of generic parameters the <typeparamref name="TSignature"/> instances
        /// contain.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, ITypeCollection)"/> where strict is true.</remarks>
        IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, ITypeCollection search);
        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="genericParameters">The number of generic parameters the <typeparamref name="TSignature"/> instances
        /// contain.</param>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, params IType[] search);
        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="genericParameters">The number of generic parameters the <typeparamref name="TSignature"/> instances
        /// contain.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, IType[])"/> where strict is true.</remarks>
        IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, params IType[] search);

    }

    /// <summary>
    /// Defines generic properties and methods for working with a dictionary
    /// of method signatures.
    /// </summary>
    /// <typeparam name="TSignature">The type of signature member in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of signature parent in the current implementation.</typeparam>
    public interface IMethodSignatureMemberDictionary<TSignature, TSignatureParent> :
        IMethodSignatureMemberDictionary<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>,
        IGroupedMemberDictionary<TSignatureParent, TSignature>
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
        /// <summary>
        /// Searches for <see cref="IMethodSignatureMember"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/> of <see cref="IType"/> elements representing the type-replacements for the generic method.
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary"/> of 
        /// <see cref="IMethodSignatureMember"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        IFilteredSignatureMemberDictionary Find(string name, ITypeCollection genericParameters, bool strict, ITypeCollection search);
        /// <summary>
        /// Searches for <see cref="IMethodSignatureMember"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/> of <see cref="IType"/> elements representing the type-replacements for the generic method.
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary"/> of 
        /// <see cref="IMethodSignatureMember"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, ITypeCollection)"/> where strict is true.</remarks>
        IFilteredSignatureMemberDictionary Find(string name, ITypeCollection genericParameters, ITypeCollection search);
        /// <summary>
        /// Searches for <see cref="IMethodSignatureMember"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/> of <see cref="IType"/> elements representing the type-replacements for the generic method.
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary"/> of 
        /// <see cref="IMethodSignatureMember"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        IFilteredSignatureMemberDictionary Find(string name, ITypeCollection genericParameters, bool strict, params IType[] search);
        /// <summary>
        /// Searches for <see cref="IMethodSignatureMember"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/> of <see cref="IType"/> elements representing the type-replacements for the generic method.
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary"/> of 
        /// <see cref="IMethodSignatureMember"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, IType[])"/> where strict is true.</remarks>
        IFilteredSignatureMemberDictionary Find(string name, ITypeCollection genericParameters, params IType[] search);

        /// <summary>
        /// Searches for <see cref="IMethodSignatureMember"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary"/> of 
        /// <see cref="IMethodSignatureMember"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        IFilteredSignatureMemberDictionary Find(string name, bool strict, ITypeCollection search);
        /// <summary>
        /// Searches for <see cref="IMethodSignatureMember"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary"/> of 
        /// <see cref="IMethodSignatureMember"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, ITypeCollection)"/> where strict is true.</remarks>
        IFilteredSignatureMemberDictionary Find(string name, ITypeCollection search);
        /// <summary>
        /// Searches for <see cref="IMethodSignatureMember"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary"/> of 
        /// <see cref="IMethodSignatureMember"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        IFilteredSignatureMemberDictionary Find(string name, bool strict, params IType[] search);
        /// <summary>
        /// Searches for <see cref="IMethodSignatureMember"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary"/> of 
        /// <see cref="IMethodSignatureMember"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, IType[])"/> where strict is true.</remarks>
        IFilteredSignatureMemberDictionary Find(string name, params IType[] search);
    }
}
