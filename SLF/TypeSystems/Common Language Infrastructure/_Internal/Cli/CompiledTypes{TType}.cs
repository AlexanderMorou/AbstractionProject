using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
/*----------------------------------------\
| Copyright © 2010 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledTypes<TType> :
        LockedGroupedDeclarationsBase<TType, IType, Type>
        where TType :
            class,
            IType<TType>
    {
        public CompiledTypes(LockedFullTypes fullTypes) 
            : base(fullTypes)
        {

        }
        protected override string FetchKey(Type item)
        {
            return item.Name;
        }


    }

}
