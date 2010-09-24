using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with the parent
    /// of a series of events as an <see cref="IType"/>.
    /// </summary>
    public interface IEventSignatureParent :
        IType
    {
        /// <summary>
        /// Returns the <see cref="IEventSignatureMemberDictionary"/>
        /// associated to the current <see cref="IEventSignatureParent"/>
        /// implementation.
        /// </summary>
        IEventSignatureMemberDictionary Events { get; }
    }
}
