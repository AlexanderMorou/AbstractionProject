using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliManager
    {
        internal class MetadataService :
            ITypeIdentityMetadataService
        {
            private CliManager manager;

            public MetadataService(CliManager manager) { this.manager = manager; }

            public ITypeIdentityManager IdentityManager
            {
                get { return this.manager; }
            }

            public bool IsMetadatumInheritable(IType metadatum)
            {
                throw new NotImplementedException();
            }

            public bool IsMetadatumRepeatable(IType metadatum)
            {
                throw new NotImplementedException();
            }

            public TypeIsMetadata IsMetadatum(IType metadatum)
            {
                if (metadatum == null)
                    throw new ArgumentNullException("metadatum");
                var metadataIdentifier = this.manager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum);
                IType rootMetadatum;
                if (this.manager.RuntimeEnvironment.UseCoreLibrary)
                    rootMetadatum = this.manager.ObtainTypeReference(this.manager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum));
                else
                {
                    var assembly = metadatum.Assembly;
                    rootMetadatum = this.manager.ObtainTypeReference(this.manager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum), assembly);
                }
                if (!rootMetadatum.IsAssignableFrom(metadatum))
                {
                    if (metadatum.Type == TypeKind.Interface)
                        return TypeIsMetadata.Interface;
                    return TypeIsMetadata.No;
                }
                else
                    return TypeIsMetadata.Yes;
            }

            public MetadatumInfo GetMetadatumInfo(IType metadatum)
            {
                throw new NotImplementedException();
            }
        }
    }
}
