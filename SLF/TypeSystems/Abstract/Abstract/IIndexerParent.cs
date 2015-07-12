using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with the
    /// parent of a series of indexers.
    /// </summary>
    public interface IIndexerParent :
        ISignatureParent
    {
        /// <summary>
        /// Returns the <see cref="IIndexerMemberDictionary"/>
        /// contained within the <see cref="IIndexerParent"/>.
        /// </summary>
        IIndexerMemberDictionary Indexers { get; }
    }
}
