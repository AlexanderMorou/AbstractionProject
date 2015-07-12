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
    /// Defines generic properties and methods for working with a dictionary of members.
    /// </summary>
    /// <typeparam name="TParent">The type of parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of parent in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TItemIdentifier">The kind of identifier used to differentiate the
    /// <typeparamref name="TIntermediateItem"/> instances from one another.</typeparam>
    /// <typeparam name="TItem">The type of member in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateItem">The type of member in the intermediate abstract
    /// syntax tree.</typeparam>
    public interface IIntermediateMemberDictionary<TParent, TIntermediateParent, TItemIdentifier, TItem, TIntermediateItem> :
        IIntermediateDeclarationDictionary<TItemIdentifier, TItem, TIntermediateItem>,
        IMemberDictionary<TParent, TItemIdentifier, TItem>
        where TParent :
            IMemberParent
        where TIntermediateParent :
            TParent,
            IIntermediateMemberParent
        where TItemIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TItem :
            IMember<TItemIdentifier, TParent>
        where TIntermediateItem :
            IIntermediateMember<TItemIdentifier, TParent, TIntermediateParent>,
            TItem
    {
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateParent"/> which contains the <see cref="IIntermediateMemberDictionary{TParent, TIntermediateParent, TItemIdentifier, TItem, TIntermediateItem}"/>.
        /// </summary>
        new TIntermediateParent Parent { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a dictionary of members.
    /// </summary>
    public interface IIntermediateMemberDictionary :
        IMemberDictionary
    {
        /// <summary>
        /// Removes the member with the
        /// <paramref name="uniqueId"/> provided.
        /// </summary>
        /// <param name="uniqueId">The <see cref="IGeneralMemberUniqueIdentifier"/> value
        /// representing the unique identifier of the 
        /// <see cref="IIntermediateMember"/>
        /// to remove.</param>
        /// <returns>true if the member was present and it was
        /// removed; false, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown
        /// when <paramref name="uniqueId"/> is null.</exception>
        bool Remove(IGeneralMemberUniqueIdentifier uniqueId);

        /// <summary>
        /// Removes the <paramref name="member"/>
        /// provided.
        /// </summary>
        /// <param name="member">The <see cref="IIntermediateMember"/>
        /// to remove.</param>
        /// <returns>true if the <paramref name="member"/>
        /// provided exists within the <see cref="IIntermediateGroupedMemberDictionary"/>
        /// and it was removed successfully; false, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="member"/> is null.</exception>
        bool Remove(IIntermediateMember member);
    }
}
