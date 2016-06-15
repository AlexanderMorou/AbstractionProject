using System;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal
{
    internal interface _LockedRelativeHelper<TMItemIdentifier, TMItem>
        where TMItem :
            class,
            IDeclaration,
            IDisposable
    {
        void RelateSubordinateMembers<TSItemIdentifier, TSItem, TRItem>(TRItem[] sourceData, ISubordinateDictionary<TSItemIdentifier, TMItemIdentifier, TSItem, TMItem> subordinate)
            where TSItemIdentifier :
                TMItemIdentifier
            where TSItem :
                class,
                TMItem;
    }
}
