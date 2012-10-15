using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal interface __ICliTypeParent :
        _ICliTypeParent,
        ICliTypeParent
    {
        /// <summary>
        /// Returns the <see cref="_ICliManager"/> which helps 
        /// resolve type identities.
        /// </summary>
        _ICliManager IdentityManager { get; }
        new _ICliAssembly Assembly { get; }
    }

    internal interface _ICliTopLevelTypeParent :
        __ICliTypeParent
    {
        ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name, string moduleName);
    }

    internal interface _ICliTypeParent
    {
        /// <summary>
        /// Returns a <see cref="IControlledCollection"/> of <see cref="ICliMetadataTypeDefinitionTableRow"/> instances
        /// which denote the types defined within the local scope of the <see cref="_ICliTypeParent"/>.
        /// </summary>
        IControlledCollection<ICliMetadataTypeDefinitionTableRow> _Types { get; }
    }
}
