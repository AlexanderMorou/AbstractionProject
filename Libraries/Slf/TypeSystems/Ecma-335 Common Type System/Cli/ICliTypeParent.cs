using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICliTypeParent :
        ITypeParent
    {
        ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name);
        ICliMetadataTypeDefinitionTableRow FindType(IGeneralTypeUniqueIdentifier uniqueIdentifier);
    }
}
