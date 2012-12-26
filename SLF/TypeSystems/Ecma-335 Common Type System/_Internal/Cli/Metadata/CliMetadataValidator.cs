using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Globalization;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal static class CliMetadataValidator
    {
        public static ICompilerErrorCollection ValidateMetadata(IAssembly hostAssembly, ICliMetadataRoot metadataRoot)
        {
            CompilerErrorCollection cec = new CompilerErrorCollection();
            ValidateAssemblyTable(hostAssembly, metadataRoot, cec);
            ValidateModuleTable(hostAssembly, metadataRoot, cec);
            ValidateTypeReferenceTable(hostAssembly, metadataRoot, cec);
            return cec;
        }

        private static void ValidateAssemblyTable(IAssembly hostAssembly, ICliMetadataRoot metadataRoot, CompilerErrorCollection cec)
        {
            if (metadataRoot == null)
                throw new ArgumentNullException("metadataRoot");
            if (metadataRoot.TableStream == null)
                throw new ArgumentException("Table stream does not exist in the metadata provided.", "metadataRoot");
            var assemblyTable = metadataRoot.TableStream.AssemblyTable;
            if (assemblyTable != null)
            {
                if (assemblyTable.Count > 1)
                    //Error: Table 0x20, metadata validation rule 1.
                    cec.ModelError(CliWarningsAndErrors.CliMetadata201, hostAssembly, assemblyTable);
                var firstAssembly = assemblyTable[1];
                switch (firstAssembly.HashAlgorithmId)
                {
                    case AssemblyHashAlgorithm.None:
                    case AssemblyHashAlgorithm.MD5_Reserved:
                    case AssemblyHashAlgorithm.SHA1:
                    case AssemblyHashAlgorithm.SHA256:
                    case AssemblyHashAlgorithm.SHA384:
                    case AssemblyHashAlgorithm.SHA512:
                        break;
                    default:
                        cec.ModelError(CliWarningsAndErrors.CliMetadata202, hostAssembly, firstAssembly);
                        break;
                }
                var remainingFlags = (firstAssembly.Flags & ~CliMetadataAssemblyFlags.ValidMask);
                if ((int)remainingFlags != 0)
                    cec.ModelError(CliWarningsAndErrors.CliMetadata204, hostAssembly, firstAssembly);
                if (firstAssembly.NameIndex == 0)
                    cec.ModelError(CliWarningsAndErrors.CliMetadata206, hostAssembly, firstAssembly);
                if (firstAssembly.CultureIndex != 0)
                {
                    /* *
                     * The sets of identifiers used vary slightly, transcode it from the metadata set
                     * to the standard culture identifier set.
                     * */
                    var transcodedCulture = ValidCultureIdentifiers.TranscodeToCultureIdentifier(firstAssembly.Culture);
                    try
                    {
                        CultureIdentifiers.GetIdentifierByName(transcodedCulture);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        /* *
                         * The specified culture was invalid.
                         * */
                        cec.ModelError(CliWarningsAndErrors.CliMetadata209, hostAssembly, firstAssembly, new string[] { firstAssembly.Culture });
                    }
                }
            }
        }

        private static void ValidateModuleTable(IAssembly hostAssembly, ICliMetadataRoot metadataRoot, CompilerErrorCollection cec)
        {
            var moduleTable = metadataRoot.TableStream.ModuleTable;
            if (moduleTable == null || moduleTable.Count > 1)
            {
                if (moduleTable.Count > 1)
                    cec.ModelError(CliWarningsAndErrors.CliMetadata001, hostAssembly, moduleTable);
                else
                    cec.ModelError(CliWarningsAndErrors.CliMetadata001, hostAssembly);
            }
            if (moduleTable != null && moduleTable.Count > 0)
            {
                var firstModule = moduleTable[1];
                /* *
                 * Table 0x00, second metadata rule.
                 * */
                if (firstModule.NameIndex == 0)
                    cec.ModelError(CliWarningsAndErrors.CliMetadata002, hostAssembly, firstModule);
                if (firstModule.ModuleVersionIndex == 0)
                    cec.ModelError(CliWarningsAndErrors.CliMetadata003, hostAssembly, firstModule);
            }
        }



        private static void ValidateTypeReferenceTable(IAssembly hostAssembly, ICliMetadataRoot metadataRoot, CompilerErrorCollection cec)
        {
            var typeRefTable = metadataRoot.TableStream.TypeRefTable;
            if (typeRefTable != null)
            {
                typeRefTable.Read();
                Parallel.ForEach(typeRefTable.ToArray(), typeRef =>
                {
                    var @namespace = typeRef.Namespace;
                    string fullName = null;
                    if (@namespace == null)
                        fullName = typeRef.Name;
                    else
                        fullName = string.Format("{0}.{1}", @namespace, typeRef.Name);
                    switch (typeRef.ResolutionScope)
                    {
                        case CliMetadataResolutionScopeTag.Module:
                            if (typeRef.Source == null)
                            {
                                var exportedTypeTable = metadataRoot.TableStream.ExportedTypeTable;
                                if (exportedTypeTable == null)
                                    cec.ModelError(CliWarningsAndErrors.CliMetadata011a, hostAssembly, typeRef);
                                var exportedType =
                                    (from eType in exportedTypeTable
                                     where eType.NamespaceIndex == typeRef.NamespaceIndex &&
                                         eType.NameIndex == typeRef.NameIndex
                                     select eType).FirstOrDefault();
                                if (exportedType == null)
                                    cec.ModelError(CliWarningsAndErrors.CliMetadata011a, hostAssembly, metadataRoot, new string[] { fullName });
                            }
                            else
                                cec.ModelWarning(CliWarningsAndErrors.CliMetadata011d, hostAssembly, typeRef, new string[] { fullName });
                            break;
                        case CliMetadataResolutionScopeTag.ModuleReference:
                            var moduleRef = (ICliMetadataModuleReferenceTableRow)typeRef.Source;
                            if (!hostAssembly.Modules.ContainsKey(AstIdentifier.GetDeclarationIdentifier(moduleRef.Name)))
                                cec.ModelError(CliWarningsAndErrors.CliMetadata011c, hostAssembly, typeRef, new string[] { fullName });
                            break;
                        case CliMetadataResolutionScopeTag.AssemblyReference:
                            var assemblyReference = (ICliMetadataAssemblyRefTableRow)typeRef.Source;
                            var assemblyUniqueId = CliCommon.GetAssemblyUniqueIdentifier(assemblyReference).Item2;
                            if (assemblyUniqueId == hostAssembly.UniqueIdentifier)
                                cec.ModelError(CliWarningsAndErrors.CliMetadata011e, hostAssembly, typeRef, new string[] { fullName });
                            else if (!hostAssembly.References.ContainsKey(assemblyUniqueId))
                                cec.ModelError(CliWarningsAndErrors.CliMetadata011f, hostAssembly, typeRef, new string[] { fullName, assemblyUniqueId.ToString() });
                            break;
                        case CliMetadataResolutionScopeTag.TypeReference:
                            
                            break;
                        default:
                            break;
                    }
                    if (typeRef.NameIndex == 0)
                        cec.ModelError(CliWarningsAndErrors.CliMetadata012, hostAssembly, typeRef);
                    if (typeRef.NamespaceIndex > 0 && typeRef.Namespace == string.Empty)
                        cec.ModelError(CliWarningsAndErrors.CliMetadata014, hostAssembly, typeRef);

                });
            }
        }
    }
}
