using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for working with a full series of members.
    /// </summary>
    public interface IIntermediateFullMemberDictionary :
        IIntermediateFullDeclarationDictionary<IGeneralMemberUniqueIdentifier, IMember, IIntermediateMember>,
        IFullMemberDictionary,
        IDisposable
    {
        /// <summary>
        /// Returns the number of members which belong to the 
        /// partial <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">
        /// The <see cref="IIntermediateMemberParent"/> which
        /// </param>
        /// <returns>A <see cref="Int32"/> value representing
        /// the number of members which belong to the partial
        /// <paramref name="parent"/> provided.
        /// </returns>
        int GetCountFor(IIntermediateMemberParent parent);

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, TValue}"/> elements
        /// which are exclusively defined on the owning <paramref name="context"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, TValue}"/> elements
        /// which are exclusively defined on the owning <paramref name="context"/>.</returns>
        /// <param name="context">The <see cref="IIntermediateMemberParent"/> which denotes
        /// the owning context that is relevant to the request.</param>
        IEnumerable<KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IIntermediateMember>>> ExclusivelyOnParent(IIntermediateMemberParent context);
    }
}
