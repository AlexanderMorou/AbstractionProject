using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliClassTypeDictionary :
        CliTypeDictionary<IGeneralGenericTypeUniqueIdentifier, IClassType>,
        IClassTypeDictionary
    {
        public CliClassTypeDictionary(_ICliTypeParent parent, CliFullTypeDictionary master)
            : base(parent, master, TypeKind.Class)
        {
        }
    }
}
