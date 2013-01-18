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
using System.Reflection;
using System.Security;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal static class CliMetadataValidator
    {
        internal static ICompilerErrorCollection ValidateMetadata(IAssembly hostAssembly, ICliMetadataRoot metadataRoot, _ICliManager identityManager)
        {
            CompilerErrorCollection resultErrorCollection = new CompilerErrorCollection();
            Parallel.ForEach(metadataRoot.TableStream.Values, table => table.Read());
            ValidateAssemblyTable(hostAssembly, metadataRoot, resultErrorCollection);
            ValidateModuleTable(hostAssembly, metadataRoot, resultErrorCollection);
            ValidateTypeReferenceTable(hostAssembly, metadataRoot, resultErrorCollection);
            ValidateTypeDefinitionTable(hostAssembly, metadataRoot, resultErrorCollection, identityManager);
            return resultErrorCollection;
        }

        private static void ValidateAssemblyTable(IAssembly hostAssembly, ICliMetadataRoot metadataRoot, CompilerErrorCollection resultErrorCollection)
        {
            if (metadataRoot == null)
                throw new ArgumentNullException("metadataRoot");
            if (metadataRoot.TableStream == null)
                throw new ArgumentException("Table stream does not exist in the metadata provided.", "metadataRoot");
            var assemblyTable = metadataRoot.TableStream.AssemblyTable;
            if (assemblyTable != null)
            {
                if (assemblyTable.Count > 1)
                    /* *
                     * The assembly table shall contain zero or one row only.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata2001, hostAssembly, assemblyTable);
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
                        /* *
                         * HashAlgId shall be one of the specified values.
                         * */
                        resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata2002, hostAssembly, firstAssembly);
                        break;
                }
                var remainingFlags = (firstAssembly.Flags & ~CliMetadataAssemblyFlags.ValidMask);
                if ((int)remainingFlags != 0)
                    /* *
                     * Flags shall have only those values set that are specified.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata2004, hostAssembly, firstAssembly);
                if (firstAssembly.NameIndex == 0)
                    /* *
                     * Name shall index a non-empty string in the String Heap.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata2006, hostAssembly, firstAssembly);
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
                        resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata2009, hostAssembly, firstAssembly, new string[] { firstAssembly.Culture });
                    }
                }
            }
        }

        private static void ValidateModuleTable(IAssembly hostAssembly, ICliMetadataRoot metadataRoot, CompilerErrorCollection resultErrorCollection)
        {
            var moduleTable = metadataRoot.TableStream.ModuleTable;
            if (moduleTable == null || moduleTable.Count > 1)
            {
                if (moduleTable.Count > 1)
                    // The module table shall contain one and only one row.  More than one was found.
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0001, hostAssembly, moduleTable);
                else
                    // The module table shall contain one and only one row.  None were found.
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0001, hostAssembly);
            }
            if (moduleTable != null && moduleTable.Count > 0)
            {
                var firstModule = moduleTable[1];
                if (firstModule.NameIndex == 0)
                    /* *
                     * Name shall index a non-empty string. This string should match exactly
                     * any corresponding ModuleRef.Name string that resolves to this module.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0002, hostAssembly, firstModule);
                if (firstModule.ModuleVersionIndex == 0)
                    // Mvid shall index a non-null Guid in the Guid heap.
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0003, hostAssembly, firstModule);
            }
        }



        private static void ValidateTypeReferenceTable(IAssembly hostAssembly, ICliMetadataRoot metadataRoot, CompilerErrorCollection resultErrorCollection)
        {
            var typeRefTable = metadataRoot.TableStream.TypeRefTable;
            if (typeRefTable != null)
            {
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
                                    /* *
                                     * When resolution scope is null, there shall be an
                                     * ExportedType table row for this type (fullName).
                                     * */
                                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0101a, hostAssembly, typeRef, new string[] { fullName });
                                var exportedType =
                                    (from eType in exportedTypeTable
                                     where eType.NamespaceIndex == typeRef.NamespaceIndex &&
                                         eType.NameIndex == typeRef.NameIndex
                                     select eType).FirstOrDefault();
                                if (exportedType == null)
                                    /* *
                                     * When resolution scope is null, there shall be an
                                     * ExportedType table row for this type (fullName).
                                     * */
                                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0101a, hostAssembly, metadataRoot, new string[] { fullName });
                            }
                            else
                                /* *
                                 * When resolution scope is a module token, the type referenced (fullName) 
                                 * should be defined within the current module; though, this should not
                                 * occur in a CLI ("Compressed Metadata") module.
                                 * */
                                resultErrorCollection.ModelWarning(CliWarningsAndErrors.CliMetadata0101d, hostAssembly, typeRef, new string[] { fullName });
                            break;
                        case CliMetadataResolutionScopeTag.ModuleReference:
                            var moduleRef = (ICliMetadataModuleReferenceTableRow)typeRef.Source;
                            if (!hostAssembly.Modules.ContainsKey(AstIdentifier.GetDeclarationIdentifier(moduleRef.Name)))
                                /* *
                                 * When resolution scope is a moduleref token, the target type (fullName)
                                 * is defined in another module within the same assembly.
                                 * */
                                resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0101c, hostAssembly, typeRef, new string[] { fullName });
                            break;
                        case CliMetadataResolutionScopeTag.AssemblyReference:
                            var assemblyReference = (ICliMetadataAssemblyRefTableRow)typeRef.Source;
                            var assemblyUniqueId = CliCommon.GetAssemblyUniqueIdentifier(assemblyReference).Item2;
                            if (assemblyUniqueId == hostAssembly.UniqueIdentifier)
                                /* *
                                 * When resolution scope is an assemblyref, the type referenced (fullName)
                                 * should be defined within another assembly other than the current
                                 * module's assembly.
                                 * */
                                resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0101e, hostAssembly, typeRef, new string[] { fullName });
                            else if (!hostAssembly.References.ContainsKey(assemblyUniqueId))
                            {
                                hostAssembly.References.ContainsKey(assemblyUniqueId);
                                /* *
                                 * When resolution scope is an assemblyref, the type referenced (fullName)
                                 * should be defined within another assembly (assemblyUniqueId) which
                                 * cannot be found.
                                 * */
                                resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0101f, hostAssembly, typeRef, new string[] { fullName, assemblyUniqueId.ToString() });
                            }
                            break;
                        case CliMetadataResolutionScopeTag.TypeReference:

                            break;
                        default:
                            break;
                    }
                    if (typeRef.NameIndex == 0)
                        /* *
                         * Name shall index a non-empty string within the StringHeap.
                         * */
                        resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0102, hostAssembly, typeRef);
                    if (typeRef.NamespaceIndex > 0 && typeRef.Namespace == string.Empty)
                        /* *
                         * Namespace shall index a non-empty string if not null.
                         * */
                        resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0104, hostAssembly, typeRef);
                });

            }
        }

        private static void ValidateTypeDefinitionTable(IAssembly hostAssembly, ICliMetadataRoot metadataRoot, CompilerErrorCollection resultErrorCollection, _ICliManager identityManager)
        {
            var typeDefTable = metadataRoot.TableStream.TypeDefinitionTable;
            if (typeDefTable == null)
                return;
            int maxTypeDef = typeDefTable.Count;
            int maxTypeRef = metadataRoot.TableStream.TypeRefTable == null ? 0 : metadataRoot.TableStream.TypeRefTable.Count;
            int maxTypeSpec = metadataRoot.TableStream.TypeSpecificationTable == null ? 0 : metadataRoot.TableStream.TypeSpecificationTable.Count;
            Parallel.ForEach(typeDefTable.ToArray(), typeDefinition =>
            {
                var typeAttributes = typeDefinition.TypeAttributes;
                if (((int)(typeAttributes ^ (typeAttributes & (TypeAttributes)0xD77DBF))) != 0)
                    /* *
                     * Flags must contain only those values specified by System.Reflection.TypeAttributes
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0202a, hostAssembly, typeDefinition);
                if (((typeAttributes & TypeAttributes.SequentialLayout) == TypeAttributes.SequentialLayout) &&
                    ((typeAttributes & TypeAttributes.ExplicitLayout) == TypeAttributes.SequentialLayout))
                    /* *
                     * Sequential Layout and Explicit Layout cannot be set together.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0202b, hostAssembly, typeDefinition);
                if (((typeAttributes & TypeAttributes.AutoClass) == TypeAttributes.AutoClass) &&
                    ((typeAttributes & TypeAttributes.UnicodeClass) == TypeAttributes.UnicodeClass))
                    /* *
                     * UnicodeClass and AutoClass flags cannot be set together.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0202c, hostAssembly, typeDefinition);
                if ((typeAttributes & TypeAttributes.HasSecurity) == TypeAttributes.HasSecurity)
                {
                    if (!(CheckForDeclSecurityRow(metadataRoot, typeDefinition) || CheckForSecuritySuppressionAttribute(metadataRoot, typeDefTable, typeDefinition)))
                        /* *
                         * If HasSecurity is set, then the type must contain at least one
                         * DeclSecurity row, or a custom attribute called 
                         * SuppressUnmanagedCodeSecurityAttribute.
                         * */
                        resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0202d, hostAssembly, typeDefinition);
                    if ((typeAttributes & TypeAttributes.Interface) == TypeAttributes.Interface)
                        /* *
                         * Interfaces are allowed to have the HasSecurity flag set; however,
                         * the security system ignores any permission requests attached to
                         * that interface.
                         * */
                        resultErrorCollection.ModelWarning(CliWarningsAndErrors.CliMetadata0202g, hostAssembly, typeDefinition);
                }
                else if (CheckForDeclSecurityRow(metadataRoot, typeDefinition))
                    /* *
                     * If this type owns one (or more) DeclSecurity rows, then the HasSecurity
                     * flag must be set.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0202e, hostAssembly, typeDefinition);
                else if (CheckForSecuritySuppressionAttribute(metadataRoot, typeDefTable, typeDefinition))
                    /* *
                     * If this type has a custom attribute called SuppressUnmanagedCodeSecurityAttribute,
                     * then the HasSecurity flag must be set.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0202f, hostAssembly, typeDefinition);
                if (string.IsNullOrEmpty(typeDefinition.Name))
                    /* *
                     * Name shall index a non-empty string in the String Heap.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0203, hostAssembly, typeDefinition);
                string name = typeDefinition.Name,
                    @namespace = typeDefinition.Namespace;
                int maxIndex = 0;
                switch (typeDefinition.ExtendsSource)
                {
                    case CliMetadataTypeDefOrRefTag.TypeDefinition:
                        maxIndex = maxTypeDef;
                        break;
                    case CliMetadataTypeDefOrRefTag.TypeReference:
                        maxIndex = maxTypeRef;
                        break;
                    case CliMetadataTypeDefOrRefTag.TypeSpecification:
                        maxIndex = maxTypeSpec;
                        break;
                }
                /* *
                 * If the index is beyond the allowed ranges, don't even check the
                 * 'Extends' part as it'll yield an exception.
                 * *
                 * The exception being if there's no actual reference in the first place, 
                 * when the source is a type def and the index is zero.
                 * */
                if (!(typeDefinition.ExtendsSource == CliMetadataTypeDefOrRefTag.TypeDefinition && typeDefinition.ExtendsIndex == 0) &&
                    (1 > typeDefinition.ExtendsIndex || typeDefinition.ExtendsIndex > maxIndex))
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0211a, hostAssembly, typeDefinition);
                else if (typeDefinition.Extends == null && (typeAttributes & TypeAttributes.Interface) != TypeAttributes.Interface)
                {
                    if (!(CliCommon.IsSpecialModule(typeDefinition) ||
                          CliCommon.IsBaseObject(identityManager, typeDefinition)))
                        /* *
                         * Every class (with exception to System.Object and the special class
                         * <Module>) shall extend one, and only one, other Class - so Extends
                         * is for a Class shall be non-null.
                         * */
                        resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0208, hostAssembly, typeDefinition);
                }
                else if (typeDefinition.Extends != null &&
                    name == "Object" &&
                    @namespace == "System")
                    /* *
                     * System.Object must have an extends value of null.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0209, hostAssembly, typeDefinition);
                else if (name == "ValueType" &&
                    @namespace == "System" &&
                    typeDefinition.DeclaringType == null)
                {
                    if (!CliCommon.IsBaseObject(identityManager, typeDefinition.Extends))
                        /* *
                         * System.ValueType must have an extends value of System.Object
                         * */
                        resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0210, hostAssembly, typeDefinition);
                }
                /* *
                 * ToDo: Add code here to check the name identifier against CLS rules.
                 * Warning CliMetadata0204
                 * */
                if (typeDefinition.NamespaceIndex != 0 && typeDefinition.Namespace == string.Empty)
                    /* *
                     * If non-null, Namespace shall index a non-empty string in the
                     * String Heap.
                     * */
                    resultErrorCollection.ModelError(CliWarningsAndErrors.CliMetadata0206, hostAssembly, typeDefinition);
                /* *
                 * ToDo: Add code here to check the namespace identifier against CLS rules.
                 * Warning CliMetadata0207
                 * */

            });
        }

        private static bool ExtendsTarget(ICliMetadataTypeDefinitionTableRow typeDefinition, string nameSpace, string name)
        {

            var extends = typeDefinition.Extends;
            if (typeDefinition.ExtendsSource == CliMetadataTypeDefOrRefTag.TypeDefinition)
            {
                var extendsDef = (ICliMetadataTypeDefinitionTableRow)extends;
                if (!(extendsDef.Name == name &&
                    extendsDef.Namespace == nameSpace &&
                    extendsDef.DeclaringType == null))
                    return false;
            }
            else if (typeDefinition.ExtendsSource == CliMetadataTypeDefOrRefTag.TypeReference)
            {
                var extendsRef = (ICliMetadataTypeRefTableRow)extends;
                if (!(extendsRef.Name == name &&
                    extendsRef.Namespace == nameSpace &&
                    extendsRef.ResolutionScope == CliMetadataResolutionScopeTag.AssemblyReference ||
                    extendsRef.ResolutionScope == CliMetadataResolutionScopeTag.ModuleReference ||
                    extendsRef.ResolutionScope == CliMetadataResolutionScopeTag.Module))
                    return false;
            }
            else
                return false;
            return true;
        }

        private static bool CheckForDeclSecurityRow(ICliMetadataRoot metadataRoot, ICliMetadataTypeDefinitionTableRow typeDefinition)
        {
            bool hasSecurityRow = false;
            var declSecurityTable = metadataRoot.TableStream.DeclSecurityTable;
            if (declSecurityTable == null)
                return hasSecurityRow;
            foreach (var declSecurity in declSecurityTable)
                if (declSecurity.ParentSource == CliMetadataHasDeclSecurityTag.TypeDefinition && declSecurity.ParentIndex == typeDefinition.Index)
                    hasSecurityRow = true;
            return hasSecurityRow;
        }

        private static bool CheckForSecuritySuppressionAttribute(ICliMetadataRoot metadataRoot, ICliMetadataTypeDefinitionTable typeDefTable, ICliMetadataTypeDefinitionTableRow typeDefinition)
        {
            bool hasSuppressionAttribute = false;
            foreach (var customAttr in typeDefinition.CustomAttributes)
                switch (customAttr.CtorSource)
                {
                    case CliMetadataCustomAttributeTypeTag.MethodDefinition:
                        {
                            var methodDef = (ICliMetadataMethodDefinitionTableRow)customAttr.Ctor;
                            for (int i = 1; i <= metadataRoot.TableStream.TypeDefinitionTable.Count; i++)
                            {
                                var current = typeDefTable[i];
                                if (current.MethodStartIndex <= methodDef.Index)
                                {
                                    if (i == metadataRoot.TableStream.TypeDefinitionTable.Count)
                                    {
                                        if (current.Name == "SuppressUnmanagedCodeSecurityAttribute" &&
                                            current.Namespace == "System.Security")
                                        {
                                            hasSuppressionAttribute = true;
                                            goto breakLoop;
                                        }
                                    }
                                    else
                                    {
                                        var next = typeDefTable[i + 1];
                                        if (next.MethodStartIndex <= methodDef.Index)
                                            continue;
                                        if (current.Name == "SuppressUnmanagedCodeSecurityAttribute" &&
                                            current.Namespace == "System.Security")
                                        {
                                            hasSuppressionAttribute = true;
                                            goto breakLoop;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case CliMetadataCustomAttributeTypeTag.MemberReference:
                        var methodRef = (ICliMetadataMemberReferenceTableRow)customAttr.Ctor;
                        if (methodRef.ClassSource == CliMetadataMemberRefParentTag.TypeReference)
                        {
                            var typeRef = (ICliMetadataTypeRefTableRow)methodRef.Class;
                            if (typeRef.Name == "SuppressUnmanagedCodeSecurityAttribute" &&
                                typeRef.Namespace == "System.Security")
                            {
                                hasSuppressionAttribute = true;
                                goto breakLoop;
                            }
                        }
                        else if (methodRef.ClassSource == CliMetadataMemberRefParentTag.TypeDefinition)
                        {
                            var typeDef = (ICliMetadataTypeDefinitionTableRow)methodRef.Class;
                            if (typeDef.Name == "SuppressUnmanagedCodeSecurityAttribute" &&
                                typeDef.Namespace == "System.Security")
                            {
                                hasSuppressionAttribute = true;
                                goto breakLoop;
                            }
                        }
                        break;
                }
        breakLoop:
            return hasSuppressionAttribute;
        }
    }
}
