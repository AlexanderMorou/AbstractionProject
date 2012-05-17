using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal interface _ICliAssembly :
        ICliAssembly
    {
        ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name);
        INamespaceDeclaration GetNamespace(string @namespace);
    }
}
