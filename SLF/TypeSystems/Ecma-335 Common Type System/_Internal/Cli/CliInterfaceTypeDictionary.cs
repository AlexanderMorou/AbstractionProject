using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliInterfaceTypeDictionary :
       CliTypeDictionary<IGeneralGenericTypeUniqueIdentifier, IInterfaceType>,
       IInterfaceTypeDictionary
    {
        public CliInterfaceTypeDictionary(__ICliTypeParent parent, CliFullTypeDictionary master)
            : base(parent, master, TypeKind.Interface)
        {
        }
    }
}
