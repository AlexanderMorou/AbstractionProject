using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with an indexer on an
    /// intermediate data structure.
    /// </summary>
    public interface IIntermediateStructIndexerMember :
        IIntermediateIndexerMember<IStructIndexerMember, IIntermediateStructIndexerMember, IStructType, IIntermediateStructType>,
        IStructIndexerMember
    {
    }
}
