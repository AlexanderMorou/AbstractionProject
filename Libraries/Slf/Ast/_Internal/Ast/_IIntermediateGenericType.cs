using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    interface _IIntermediateGenericType<TTypeIdentifier>
    {
        void ItemAdded(IGenericParameter parameter);
        void ItemRemoved(IGenericParameter parameter);
        void CardinalityChanged(TTypeIdentifier oldIdentifier);
        void Rearranged(int from, int to);
    }
}
