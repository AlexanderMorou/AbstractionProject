using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with the parent of indexer 
    /// signatures or declarations.
    /// </summary>
    public interface IIndexerSignatureParent :
        ISignatureParent
    {
        /// <summary>
        /// Returns the <see cref="IIndexerSignatureMemberDictionary"/>
        /// contained within the <see cref="IIndexerSignatureParent"/>.
        /// </summary>
        IIndexerSignatureMemberDictionary Indexers { get; }
    }
}
