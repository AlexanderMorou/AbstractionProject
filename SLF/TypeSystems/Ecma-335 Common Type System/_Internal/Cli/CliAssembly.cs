using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliAssembly :
        AssemblyBase,
        _ICliAssembly
    {
        public class test3 { public abstract class test4 : TypeBase<IGeneralGenericTypeUniqueIdentifier> { } }

        private CliManager identityManager;
        private ICliMetadataRoot metadataRoot;
        private _AssemblyInformation assemblyInformation;
        private IStrongNamePublicKeyInfo strongNameInfo;
        private IAssemblyUniqueIdentifier uniqueIdentifier;
        private IControlledCollection<ICliMetadataTypeDefinitionTableRow> _types;
        private CliNamespaceKeyedTree namespaceInformation;
        private new CliModuleDictionary Modules { get { return (CliModuleDictionary)base.Modules; } }
        private CliAssemblyReferences cliReferences;
        private ICliRuntimeEnvironmentInfo runtimeEnvironment;
        private CliFrameworkPlatform? platform;

        public CliAssembly(CliManager identityManager, ICliMetadataAssemblyTableRow metadata, IAssemblyUniqueIdentifier uniqueIdentifier, IStrongNamePublicKeyInfo strongNameInfo)
        {
            this.metadataRoot = metadata.MetadataRoot;
            this.MetadataEntry = metadata;
            this.uniqueIdentifier = uniqueIdentifier;
            this.strongNameInfo = strongNameInfo;
            this.identityManager = identityManager;
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return this.Members.Keys.Cast<IGeneralDeclarationUniqueIdentifier>().Concat(this.Types.Keys.Cast<IGeneralDeclarationUniqueIdentifier>()); }
        }

        protected override bool CanCacheManifestModule
        {
            get { return true; }
        }

        protected override bool CanCachePublicKeyInfo
        {
            get { return true; }
        }

        protected override IClassTypeDictionary InitializeClasses()
        {
            throw new NotImplementedException();
        }

        protected override IMetadataCollection InitializeCustomAttributes()
        {
            throw new NotImplementedException();
        }

        protected override IDelegateTypeDictionary InitializeDelegates()
        {
            throw new NotImplementedException();
        }

        protected override IEnumTypeDictionary InitializeEnums()
        {
            throw new NotImplementedException();
        }

        protected override IFieldMemberDictionary<ITopLevelFieldMember, INamespaceParent> InitializeFields()
        {
            throw new NotImplementedException();
        }

        protected override IInterfaceTypeDictionary InitializeInterfaces()
        {
            throw new NotImplementedException();
        }

        protected override IFullMemberDictionary InitializeMembers()
        {
            throw new NotImplementedException();
        }

        protected override IMethodMemberDictionary<ITopLevelMethodMember, INamespaceParent> InitializeMethods()
        {
            throw new NotImplementedException();
        }

        protected override IModuleDictionary InitializeModules()
        {
            return new CliModuleDictionary(this);
        }

        protected override INamespaceDictionary InitializeNamespaces()
        {
            return new CliNamespaceDictionary(this, this, this.NamespaceInformation);
        }

        protected override IStructTypeDictionary InitializeStructs()
        {
            throw new NotImplementedException();
        }

        protected override IFullTypeDictionary InitializeTypes()
        {
            return new CliFullTypeDictionary(this._Types, this);
        }

        protected override IAssemblyInformation OnGetAssemblyInformation()
        {
            return assemblyInformation ?? (assemblyInformation = new _AssemblyInformation(this));
        }

        protected override IModule OnGetManifestModule()
        {
            return this.Modules.Values[0];
        }

        protected override IStrongNamePublicKeyInfo OnGetPublicKeyInfo()
        {
            return this.strongNameInfo;
        }

        public override IAssemblyUniqueIdentifier UniqueIdentifier
        {
            get { return this.uniqueIdentifier; }
        }

        public CliManager IdentityManager
        {
            get
            {
                return this.identityManager;
            }
        }

        //#region ICliAssembly Members

        public new CliFrameworkVersion FrameworkVersion
        {
            get
            {
                return CliCommon.GetFrameworkVersionFromString(this.metadataRoot.Version);
            }
        }

        public IControlledDictionary<ICliMetadataAssemblyRefTableRow, ICliAssembly> CliReferences
        {
            get
            {
                if (this.cliReferences == null)
                    this.cliReferences = new CliAssemblyReferences(this);
                return this.cliReferences;
            }
        }

        public CliFrameworkPlatform Platform
        {
            get
            {
                if (this.platform == null)
                {
                    var header = this.MetadataRoot.Header;
                    var peImage = this.MetadataRoot.SourceImage;
                    var imageKind = peImage.ExtendedHeader.ImageKind;
                    if (imageKind == PEImageKind.x86Image &&
                                ((((header = metadataRoot.Header).Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly) &&
                                  ((header.Flags & CliRuntimeFlags.Requires32BitProcess) != CliRuntimeFlags.Requires32BitProcess)))
                        this.platform = CliFrameworkPlatform.AnyPlatform;
                    else if (imageKind == PEImageKind.x64Image)
                        this.platform = CliFrameworkPlatform.x64Platform;
                    else if (imageKind == PEImageKind.x86Image &&
                        ((header.Flags & CliRuntimeFlags.Requires32BitProcess) == CliRuntimeFlags.Requires32BitProcess))
                        this.platform = CliFrameworkPlatform.x86Platform;
                    else
                        this.platform = CliFrameworkPlatform.AnyPlatform;
                }
                return this.platform.Value;
            }
        }

        public ICliRuntimeEnvironmentInfo RuntimeEnvironment
        {
            get {
                if (this.runtimeEnvironment == null)
                {
                    var imRE = this.identityManager.RuntimeEnvironment;
                    if (imRE.Version != this.FrameworkVersion ||
                        imRE.Platform != this.Platform)
                        this.runtimeEnvironment = CliGateway.GetRuntimeEnvironmentInfo(this.Platform, this.FrameworkVersion, imRE.ResolveCurrent, imRE.UseCoreLibrary, imRE.UseGlobalAccessCache, imRE.AdditionalPaths.ToArray());
                    else
                        this.runtimeEnvironment = imRE;
                }
                return this.runtimeEnvironment;
            }
        }

        public ICliMetadataRoot MetadataRoot
        {
            get { return this.metadataRoot; }
        }

        //#endregion

        internal static int _CompareTo_(ICliMetadataTypeDefinitionTableRow leftRow, ICliMetadataTypeDefinitionTableRow rightRow)
        {
            return leftRow.Name.CompareTo(rightRow.Name);
        }

        public CliNamespaceKeyedTree NamespaceInformation
        {
            get
            {
                if (this.namespaceInformation == null)
                    this.InitializeCommon();
                return this.namespaceInformation;
            }
        }

        internal void InitializeCommon()
        {
            const int CLIMETADATA_TYPEDEFINITION_MODULE_INDEX = 0x00000001;
            ICliMetadataModuleTableRow[] modules = this.Modules.GetMetadata();

            var typeTables = (from moduleRow in modules
                              let ts = moduleRow.MetadataRoot.TableStream
                              where ts.TypeDefinitionTable != null
                              select ts.TypeDefinitionTable).ToArray();
            if (typeTables.Length == 0)
                return;
            HashSet<Tuple<int, uint>> namespaceIndices = new HashSet<Tuple<int, uint>>();

            Dictionary<uint, CliMetadataTypeDefinitionTableRow[]> nonNestedTypes = new Dictionary<uint, CliMetadataTypeDefinitionTableRow[]>();
            Dictionary<uint, int> nonNestedCounts = new Dictionary<uint, int>();
            /* *
             * Breakdown the types by their namespace index, and
             * create a unique list of namespaceIndices via a HashSet<T>.
             * *
             * Copy the table to a local set to avoid redundant load
             * checks, and to instruct it to read the full table sequentially.
             * */
            int totalTypeCount = 0;
            for (int typeTable = 0; typeTable < typeTables.Length; typeTable++)
                totalTypeCount += typeTables[typeTable].Count;
            ICliMetadataTypeDefinitionTableRow[] types = new ICliMetadataTypeDefinitionTableRow[totalTypeCount];
            for (int moduleIndex = 0, cOff = 0; moduleIndex < typeTables.Length; cOff += typeTables[moduleIndex++].Count)
                typeTables[moduleIndex].CopyTo(types, cOff);
            ICliMetadataTypeDefinitionTable moduleTypes = typeTables[0];
            for (int typeIndex = 0, moduleIndex = 0, moduleOffset = 0; typeIndex < types.Length; typeIndex++, moduleOffset++)
            {
                if (moduleOffset >= moduleTypes.Count)
                {
                    moduleIndex++;
                    if (moduleIndex < typeTables.Length)
                        moduleTypes = typeTables[moduleIndex];
                    moduleOffset = 0;
                }
                var typeRow = types[typeIndex];
                if ((typeRow.TypeAttributes & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic ||
                    (typeRow.TypeAttributes & TypeAttributes.VisibilityMask) == TypeAttributes.Public)
                {
                    uint nsIndex = typeRow.NamespaceIndex;
                    namespaceIndices.Add(Tuple.Create(moduleIndex, nsIndex));
                    CliMetadataTypeDefinitionTableRow[] currentRows;
                    if (!nonNestedTypes.TryGetValue(nsIndex, out currentRows))
                    {
                        nonNestedTypes.Add(nsIndex, currentRows = new CliMetadataTypeDefinitionTableRow[4]);
                        nonNestedCounts.Add(nsIndex, 0);
                    }
                    CliMetadataTypeDefinitionTableRow[] spaceAvailableRows = currentRows.EnsureSpaceExists(nonNestedCounts[nsIndex], 1);
                    if (spaceAvailableRows != currentRows)
                        nonNestedTypes[nsIndex] = currentRows = spaceAvailableRows;
                    currentRows[nonNestedCounts[nsIndex]++] = typeRow as CliMetadataTypeDefinitionTableRow;
                }
            }

            var namespaceIndicesArray = namespaceIndices.ToArray();

            CliMetadataTypeDefinitionTableRow[] topLevelRows;
            if (nonNestedTypes.TryGetValue(0, out topLevelRows))
            {
                int topLevelCount = nonNestedCounts[0];
                int noModuleCount = 0;
                for (int typeIndex = 0; typeIndex < topLevelCount; typeIndex++)
                    if (topLevelRows[typeIndex].Index != CLIMETADATA_TYPEDEFINITION_MODULE_INDEX)
                        noModuleCount++;

                if (topLevelRows.Length != noModuleCount)
                {
                    CliMetadataTypeDefinitionTableRow[] topLevelRowsActual = new CliMetadataTypeDefinitionTableRow[noModuleCount];
                    /* *
                     * Filter the entries based off of their index.  The special <Module> type
                     * is the global fields and methods.  There's no need to include them and 
                     * have to inject checks elsewhere to filter it out.
                     * */
                    for (int typeIndex = 0, cOffset = 0; typeIndex < topLevelCount; typeIndex++)
                        if (topLevelRows[typeIndex].Index != CLIMETADATA_TYPEDEFINITION_MODULE_INDEX)
                            topLevelRowsActual[cOffset++] = topLevelRows[typeIndex];

                    Array.Sort(topLevelRowsActual, _CompareTo_);
                    topLevelRows = topLevelRowsActual;
                }
                else
                    Array.Sort(topLevelRows, _CompareTo_);
            }
            else
                topLevelRows = new CliMetadataTypeDefinitionTableRow[0];

            CliNamespaceKeyedTree partialOrFullNamespaces = new CliNamespaceKeyedTree(topLevelRows);
            Dictionary<string, uint> partIndex = new Dictionary<string, uint>();
            for (int namespaceIndex = 0; namespaceIndex < namespaceIndicesArray.Length; namespaceIndex++)
            {
                uint currentIndex = namespaceIndicesArray[namespaceIndex].Item2;
                if (currentIndex == 0)
                    continue;
                var currentRows = nonNestedTypes[currentIndex];
                var currentCount = nonNestedCounts[currentIndex];
                var module = modules[namespaceIndicesArray[namespaceIndex].Item1];
                string currentString = module.MetadataRoot.StringsHeap[currentIndex];

                /* *
                 * Eliminate the null row entries by reducing the sizes
                 * of the arrays.
                 * */
                if (currentRows.Length != currentCount)
                {
                    var currentRowsCopy = new CliMetadataTypeDefinitionTableRow[currentCount];
                    Array.ConstrainedCopy(currentRows, 0, currentRowsCopy, 0, currentCount);
                    Array.Sort(currentRowsCopy, _CompareTo_);
                    currentRows = currentRowsCopy;
                }
                else
                    Array.Sort(currentRows, _CompareTo_);

                CliNamespaceKeyedTree current = partialOrFullNamespaces;
                string[] breakdown = currentString.Split(new[] { "." }, StringSplitOptions.None);
                int currentLength = 0;
                bool first = true;
                foreach (var part in breakdown)
                {
                    /* *
                     * Position tracking, there is no period preceeding 
                     * the first namespace identifier.
                     * */
                    if (first)
                        first = false;
                    else
                        currentLength++;
                    uint partId;
                    /* *
                     * Don't use the namespace index as a key for the
                     * partial or full dictionary.
                     * *
                     * Use the substring of the current namespace as a guide,
                     * since the 'System' of 'System'.Collections.Generic would 
                     * be a different string heap index than 'System' alone,
                     * create our own reference lookup for the individual
                     * segments of the namespace's name.
                     * *
                     * Use the segment's hash code to determine the part index;
                     * while there's a chance for collision, the chances are slim.
                     * Main reason is: while the space of possible character combinations
                     * is small, the chance of a user-written name overlapping, in the
                     * realm of its sibling namespaces, is unlikely.
                     * */
                    if (!partIndex.TryGetValue(part, out partId))
                        partIndex.Add(part, partId = (uint)part.GetHashCode());
                    int startIndex = currentLength;
                    currentLength += part.Length;
                    bool fullName = currentLength == currentString.Length;
                    CliNamespaceKeyedTreeNode next;
                    if (!current.TryGetValue(partId, out next))
                        if (fullName)
                            current = current.Add(partId, currentIndex, startIndex, currentLength - startIndex, currentRows, module.MetadataRoot.StringsHeap);
                        else
                            current = current.Add(partId, currentIndex, startIndex, currentLength - startIndex, module.MetadataRoot.StringsHeap);
                    else
                    {
                        current = next;
                        if (fullName)
                            current.PushModuleTypes(currentRows);
                    }
                }
            }
            this.namespaceInformation = partialOrFullNamespaces;
        }

        protected override void Dispose(bool disposing)
        {
            this.metadataRoot.Dispose();
            this.metadataRoot = null;
            this.namespaceInformation = null;
        }

        public ICliMetadataAssemblyTableRow MetadataEntry { get; private set; }

        //#region ICliDeclaration Members

        ICliMetadataTableRow ICliDeclaration.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }

        //#endregion

        internal string Location { get { return this.MetadataRoot.SourceImage.Filename; } }

        internal bool IsFromGlobalAssemblyCache { get; set; }

        public override string ToString()
        {
            return this.UniqueIdentifier.ToString();
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name, string moduleName)
        {
            return CliCommon.FindTypeImplementation(@namespace, name, moduleName, this.NamespaceInformation, this.Modules);
        }

        public ICliMetadataTypeDefinitionTableRow FindType(IGeneralTypeUniqueIdentifier uniqueIdentifier)
        {
            if (uniqueIdentifier.Name.Contains('`'))
                return this.FindType(uniqueIdentifier.Namespace.Name, uniqueIdentifier.Name);
            else if (uniqueIdentifier is IGenericTypeUniqueIdentifier)
            {
                var genericUniqueId = uniqueIdentifier as IGenericTypeUniqueIdentifier;
                if (genericUniqueId.TypeParameters > 0)
                    return this.FindType(uniqueIdentifier.Namespace.Name, string.Format("{0}`{1}", uniqueIdentifier.Name, ((IGenericTypeUniqueIdentifier)(uniqueIdentifier)).TypeParameters));
            }
            if (uniqueIdentifier.Namespace == null)
                return this.FindType(null, uniqueIdentifier.Name);
            else
                return this.FindType(uniqueIdentifier.Namespace.Name, uniqueIdentifier.Name);
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name)
        {
            return CliCommon.FindTypeImplementation(@namespace, name, this.NamespaceInformation);
        }

        public INamespaceDeclaration GetNamespace(string @namespace)
        {
            return this.Namespaces[@namespace];
        }

        #region __ICliTypeParent Members

        _ICliManager __ICliTypeParent.IdentityManager
        {
            get { return this.IdentityManager; }
        }

        _ICliAssembly __ICliTypeParent.Assembly
        {
            get { return this; }
        }

        public IControlledCollection<ICliMetadataTypeDefinitionTableRow> _Types
        {
            get
            {
                return this.NamespaceInformation._Types;
            }
        }

        #endregion

        protected override IControlledDictionary<IAssemblyUniqueIdentifier, IAssembly> InitializeReferences()
        {
            return new CliAbstractAssemblyReferences(this);
        }

        public override IType GetType(IGeneralTypeUniqueIdentifier identifier)
        {
            var typeDefinition = GetTypeDefinition(identifier);
            if (typeDefinition != null)
                return this.IdentityManager.ObtainTypeReference(typeDefinition);
            return null;
        }

        private ICliMetadataTypeDefinitionTableRow GetTypeDefinition(IGeneralTypeUniqueIdentifier identifier)
        {
            var nestingHierarchy = identifier.GetNestingHierarchy();
            ICliMetadataTypeDefinitionTableRow typeDefinition = null;
            bool first = true;
            while (nestingHierarchy.Count > 0)
            {
                var current = nestingHierarchy.Pop();
                if (first)
                {
                    /* *
                     * Going off of a true namespace setup.
                     * */
                    var topLevelType = this.FindType(current);
                    if (topLevelType == null)
                        return null;
                    typeDefinition = topLevelType;
                    first = false;
                }
                else
                {
                    bool found = false;
                    foreach (var nestedType in typeDefinition.NestedClasses)
                        if (nestedType.Name == current.Name &&
                            current.Namespace == null && string.IsNullOrEmpty(nestedType.Namespace) ||
                            (current.Namespace != null && nestedType.Namespace == current.Namespace.Name))
                        {
                            typeDefinition = nestedType;
                            found = true;
                            break;
                        }
                    if (!found)
                        return null;
                }
            }
            return typeDefinition;
        }

        public override IEnumerable<IType> GetTypes()
        {
            if (this.MetadataRoot == null ||
                this.MetadataRoot.TableStream == null||
                this.MetadataRoot.TableStream.TypeDefinitionTable == null)
                return new IType[0];
            return from typeDef in this.MetadataRoot.TableStream.TypeDefinitionTable
                   select this.IdentityManager.ObtainTypeReference(typeDef);
        }

        ICliManager ICliAssembly.IdentityManager
        {
            get
            {
                return this.IdentityManager;
            }
        }
        _ICliManager _ICliAssembly.IdentityManager
        {
            get
            {
                return this.IdentityManager;
            }
        }
    }
}
