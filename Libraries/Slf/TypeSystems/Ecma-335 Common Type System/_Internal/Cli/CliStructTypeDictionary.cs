using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliStructTypeDictionary :
        CliTypeDictionary<IGeneralGenericTypeUniqueIdentifier, IStructType>,
        IStructTypeDictionary
    {
        public CliStructTypeDictionary(__ICliTypeParent parent, CliFullTypeDictionary master)
            : base(parent, master, TypeKind.Struct)
        {
        }
    }
}
