﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    interface _ICliMemberParent :
        IMemberParent
    {
        IReadOnlyCollection<ICliMetadataPropertyTableRow> _Properties { get; }
        IReadOnlyCollection<ICliMetadataEventTableRow> _Events { get; }
        IReadOnlyCollection<ICliMetadataMethodDefinitionTableRow> _Methods { get; }
        IReadOnlyCollection<ICliMetadataFieldTableRow> _Fields { get; }
        ISubordinateDictionary BinaryOperators { get; }
        ISubordinateDictionary Constructors { get; }
        ISubordinateDictionary Events { get; }
        ISubordinateDictionary Fields { get; }
        ISubordinateDictionary Methods { get; }
        ISubordinateDictionary Properties { get; }
        ISubordinateDictionary TypeCoercions { get; }
        ISubordinateDictionary UnaryOperators { get; }
        ICliMetadataRoot MetadataRoot { get; }
        ICliMetadataTypeDefinitionTableRow MetadataEntry { get; }
        IMember CreateItem(CliMemberType member, ICliMetadataTableRow metadataEntry);
    }
}