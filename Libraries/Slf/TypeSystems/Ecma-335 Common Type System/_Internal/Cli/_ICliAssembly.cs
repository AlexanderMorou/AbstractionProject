﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal interface _ICliAssembly :
        _ICliTopLevelTypeParent,
        ICliAssembly
    {
        INamespaceDeclaration GetNamespace(string @namespace);
        new _ICliManager IdentityManager { get; }
        ControlledDictionary<ICliMetadataExportedTypeTableRow, ICliMetadataTypeDefinitionTableRow> ExportTableLookup { get; }
    }
}
