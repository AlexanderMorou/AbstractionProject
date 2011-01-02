using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of 
    /// grouped members in an intermediate context.
    /// </summary>
    /// <typeparam name="TMemberParent">The type of <see cref="IMemberParent"/>
    /// used in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMemberParent">The type of
    /// <see cref="IIntermediateMemberParent"/> in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TMember">The type of<see cref="IMember{TParent}"/> 
    /// used in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMember">The type of 
    /// <see cref="IIntermediateMember{TParent, TIntermediateParent}"/>
    /// used in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGroupedMemberDictionary<TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember> :
        IIntermediateMemberDictionary<TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember>,
        IGroupedMemberDictionary<TMemberParent, TMember>
        where TMemberParent :
            IMemberParent
        where TIntermediateMemberParent :
            TMemberParent,
            IIntermediateMemberParent
        where TMember :
            IMember<TMemberParent>
        where TIntermediateMember :
            TMember,
            IIntermediateMember<TMemberParent, TIntermediateMemberParent>
    {

        /// <summary>
        /// Removes the member with the
        /// <paramref name="uniqueId"/> provided.
        /// </summary>
        /// <param name="uniqueId">The <see cref="String"/> value
        /// representing the unique identifier of the 
        /// <typeparamref name="TIntermediateMember"/>
        /// to remove.</param>
        /// <returns>true if the member was present and it was
        /// removed; false, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown
        /// when <paramref name="uniqueId"/> is null.</exception>
        bool Remove(string uniqueId);

        /// <summary>
        /// Removes the <paramref name="member"/>
        /// provided.
        /// </summary>
        /// <param name="member">The <typeparamref name="TIntermediateMember"/>
        /// to remove.</param>
        /// <returns>true if the <paramref name="member"/>
        /// provided exists within the <see cref="IIntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// and it was removed successfully; false, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="member"/> is null.</exception>
        bool Remove(TIntermediateMember member);
    }
    /// <summary>
    /// Defines properties and methods for working with a series of
    /// grouped members in an intermediate context.
    /// </summary>
    public interface IIntermediateGroupedMemberDictionary :
        IIntermediateMemberDictionary,
        IGroupedMemberDictionary
    {
    }
}
