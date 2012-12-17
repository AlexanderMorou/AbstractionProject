using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliMetadatum :
        IMetadatum
    {
        private ICliMetadataCustomAttributeTableRow metadataEntry;
        private IMetadataEntity declarationPoint;
        private _ICliManager identityManager;
        public CliMetadatum(ICliMetadataCustomAttributeTableRow metadataEntry, _ICliManager identityManager)
        {
            this.metadataEntry = metadataEntry;
            this.identityManager = identityManager;
        }
        public IType Type
        {
            get
            {
                if (this.metadataEntry == null)
                    throw new InvalidOperationException();
                var metadataEntryCtor = metadataEntry.Ctor;
                if (metadataEntryCtor == null)
                    throw new InvalidOperationException();
                ICliMetadataTypeDefinitionTableRow typeDef = null;
                switch (metadataEntryCtor.CustomAttributeTypeEncoding)
                {
                    case CliMetadataCustomAttributeTypeTag.MethodDefinition:
                        ICliMetadataMethodDefinitionTableRow methodDefinition = (ICliMetadataMethodDefinitionTableRow)metadataEntryCtor;
                        break;
                    case CliMetadataCustomAttributeTypeTag.MemberReference:
                        ICliMetadataMemberReferenceTableRow reference = (ICliMetadataMemberReferenceTableRow)metadataEntryCtor;
                        switch (reference.ClassSource)
                        {
                            case CliMetadataMemberRefParentTag.TypeDefinition:
                                typeDef = (ICliMetadataTypeDefinitionTableRow)reference.Class;
                                break;
                            case CliMetadataMemberRefParentTag.TypeReference:
                                typeDef = identityManager.ResolveScope((ICliMetadataTypeRefTableRow)reference.Class);
                                break;
                            case CliMetadataMemberRefParentTag.TypeSpecification:
                            case CliMetadataMemberRefParentTag.ModuleReference:
                            case CliMetadataMemberRefParentTag.MethodDefinition:
                            default:
                                throw new BadImageFormatException();
                        }
                        break;
                }

                if (typeDef == null)
                    throw new BadImageFormatException();
                return identityManager.ObtainTypeReference(typeDef);
            }
        }

        public IMetadataEntity DeclarationPoint
        {
            get
            {
                return this.declarationPoint;
            }
        }

        public IEnumerable<Tuple<IType, object>> Parameters
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Tuple<IType, string, object>> NamedParameters
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            this.metadataEntry = null;
            this.declarationPoint = null;
        }
    }
}
