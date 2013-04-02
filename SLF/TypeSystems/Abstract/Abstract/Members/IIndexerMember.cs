using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an indexer member.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer in the current implementation.</typeparam>
    /// <typeparam name="TIndexerParentIdentifier">The kind of identifier used to
    /// represent the parent uniquely from its siblings.</typeparam>
    /// <typeparam name="TIndexerParent">The type of <typeparamref name="TIndexer"/> parent
    /// in the current implementation.</typeparam>
    public interface IIndexerMember<TIndexer, TIndexerParent> :
        ISignatureMember<IGeneralSignatureMemberUniqueIdentifier, TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>, TIndexerParent>,
        IPropertyMember,
        IIndexerMember
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an indexer member.
    /// </summary>
    public interface IIndexerMember :
        IIndexerSignatureMember,
        IPropertyMember,
        IExtendedInstanceMember
    {
        /// <summary>
        /// Returns the <see cref="IMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IIndexerMember"/>.
        /// </summary>
        new IPropertyMethodMember GetMethod { get; }
        /// <summary>
        /// Returns the <see cref="IMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IIndexerMember"/>.
        /// </summary>
        new IPropertyMethodMember SetMethod { get; }
    }
}
