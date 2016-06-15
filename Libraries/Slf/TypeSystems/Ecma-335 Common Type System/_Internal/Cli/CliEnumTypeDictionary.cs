using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliEnumTypeDictionary :
       CliTypeDictionary<IGeneralTypeUniqueIdentifier, IEnumType>,
       IEnumTypeDictionary
    {
        public CliEnumTypeDictionary(__ICliTypeParent parent, CliFullTypeDictionary master)
            : base(parent, master, TypeKind.Enumeration)
        {
        }
    }
}
