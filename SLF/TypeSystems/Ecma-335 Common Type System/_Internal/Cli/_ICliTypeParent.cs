﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal interface _ICliTypeParent :
        ICliTypeParent
    {
        /// <summary>
        /// Returns the <see cref="_ICliManager"/> which helps 
        /// resolve type identities.
        /// </summary>
        _ICliManager Manager { get; }
        new _ICliAssembly Assembly { get; }
        /// <summary>
        /// Returns a <see cref="IReadOnlyCollection"/> of <see cref="ICliMetadataTypeDefinitionTableRow"/> instances
        /// which denote the types defined within the local scope of the <see cref="_ICliTypeParent"/>.
        /// </summary>
        IReadOnlyCollection<ICliMetadataTypeDefinitionTableRow> _Types { get; }
    }
}