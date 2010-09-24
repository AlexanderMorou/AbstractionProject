using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    /// <summary>
    /// Provides the master locked members dictionary
    /// with access to the creator delegate from
    /// the subordinates it owns.
    /// </summary>
    /// <remarks>Necessary
    /// because of the level of abstraction difference
    /// between the 'general' main dictionary and the 
    /// subordinates.  The subordinates are, a factor of 
    /// one, higher in abstraction through generic
    /// parameters.</remarks>
    internal interface _LockedGroupDeclarationsMasterPass
    {
        IDeclaration Fetch(object source);
        string FetchKey(object source);
    }
}
