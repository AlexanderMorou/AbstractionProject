using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    /// <summary>
    /// Defines properties and methods for working with a parameter
    /// parent defined within the common language infrastructure.
    /// </summary>
    internal interface _ICliParameterParent :
        ICliDeclaration,
        IParameterParent
    {
        /// <summary>
        /// Returns the <see cref="_ICliManager"/> responsible for
        /// identity resolution.
        /// </summary>
        _ICliManager IdentityManager { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataMethodSignature"/>
        /// associated to the <see cref="_ICliParameterParent"/>.
        /// </summary>
        ICliMetadataMethodSignature Signature { get; }
    }
}
