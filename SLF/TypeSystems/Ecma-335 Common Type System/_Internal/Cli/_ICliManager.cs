using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal enum BaseKindCacheType
    {
        ObjectBase,
        ValueTypeBase,
        EnumBase,
        DelegateBase,
    }

    internal interface _ICliManager :
        ICliManager
    {
        _ICliAssembly GetRelativeAssembly(ICliMetadataRoot root);
        IDictionary<ICliMetadataTypeDefinitionTableRow, IType> TypeCache { get; }
        IDictionary<ICliMetadataTypeDefinitionTableRow, TypeKind> TypeKindCache { get; }
        IDictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType> BaseTypeKinds { get; }
        IDictionary<ICliMetadataTypeDefOrRefRow, BaseKindCacheType> RefBaseTypeKinds { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataModuleTableRow"/> from the module
        /// defined within the <paramref name="metadata"/> reference.
        /// </summary>
        /// <param name="metadata">The <see cref="ICliMetadataModuleReferenceTableRow"/>
        /// which represents the module reference.</param>
        /// <returns>The <see cref="ICliMetadataModuleTableRow"/> which represents
        /// the actual module's metadata.</returns>
        ICliMetadataModuleTableRow LoadModule(ICliMetadataModuleReferenceTableRow metadata);
        /// <summary>
        /// Resolves the <paramref name="scope"/> provided.
        /// </summary>
        /// <param name="scope">The <see cref="ICliMetadataTypeDefOrRefRow"/> to resolve.</param>
        /// <returns>A <see cref="ICliMetadataTypeDefinitionTableRow"/>
        /// which denotes the type from which <paramref name="scope"/>
        /// is derived.</returns>
        /// <remarks>Loads the appropriate assemblies should it be a type
        /// reference relative to the current runtime environment.</remarks>
        ICliMetadataTypeDefinitionTableRow ResolveScope(ICliMetadataTypeDefOrRefRow scope);
        /// <summary>
        /// Obtains the <see cref="IType"/> associated to the
        /// <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The <see cref="ICliMetadataTypeSignature"/>
        /// to obtain a type for.</param>
        /// <param name="activeType">The <see cref="IType"/> which is active in
        /// the resolution scope.</param>
        /// <param name="activeMethod">The <see cref="IMethodSignatureMember"/>
        /// which is active in the resolution scope.</param>
        /// <returns>A <see cref="IType"/> relative to the 
        /// <paramref name="signature"/> provided.</returns>
        IType ObtainTypeReference(ICliMetadataTypeSignature signature, IType activeType, IMethodSignatureMember activeMethod);

    }
}
