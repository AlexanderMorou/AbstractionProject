using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal interface _LockedRelativeHelper<TMItem>
        where TMItem :
            class,
            IDeclaration,
            IDisposable
    {
        void RelateSubordinateMembers<TSItem, TRItem>(TRItem[] sourceData, ISubordinateDictionary<string, TSItem, TMItem> subordinate)
            where TSItem :
                class,
                TMItem;
    }
}
