using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using System.Reflection;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Modules;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs;
using System.IO;
using AllenCopeland.Abstraction.Slf.Cli.Modules;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliAssembly :
        AssemblyBase,
        ICliAssembly
    {
        private string location;
        private CliManager identityManager;
        private CliMetadataRoot metadataRoot;
        private _AssemblyInformation assemblyInformation;
        private IStrongNamePublicKeyInfo strongNameInfo;
        private IAssemblyUniqueIdentifier uniqueIdentifier;
        private CliNamespaceKeyedTree namespaceInformation;
        private CliNamespaceDictionary namespaces;
        private CliModuleDictionary Modules { get { return (CliModuleDictionary) base.Modules; } }
        private CliAssemblyReferences references;
        private IDictionary<string, INamespaceDeclaration> namespaceCache = new Dictionary<string, INamespaceDeclaration>();
        public CliAssembly(string location, CliManager identityManager, ICliMetadataAssemblyTableRow metadata, IAssemblyUniqueIdentifier uniqueIdentifier, IStrongNamePublicKeyInfo strongNameInfo)
        {
            this.location = location;
            this.metadataRoot = metadata.MetadataRoot;
            this.Metadata = metadata;
            this.uniqueIdentifier = uniqueIdentifier;
            this.strongNameInfo = strongNameInfo;
            this.identityManager = identityManager;
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { throw new NotImplementedException(); }
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
            throw new NotImplementedException();
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

        public IReadOnlyDictionary<ICliMetadataAssemblyRefTableRow, ICliAssembly> References
        {
            get
            {
                if (this.references == null)
                    this.references = new CliAssemblyReferences(this);
                return this.references;
            }
        }

        public ICliRuntimeEnvironmentInfo RuntimeEnvironment
        {
            get { return this.identityManager.RuntimeEnvironment; }
        }

        ICliManager ICliAssembly.IdentityManager
        {
            get { return this.identityManager; }
        }

        public CliMetadataRoot MetadataRoot
        {
            get { return this.metadataRoot; }
        }

        //#endregion

        internal static int _CompareTo_(CliMetadataTypeDefinitionTableRow leftRow, CliMetadataTypeDefinitionTableRow rightRow)
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

            ICliMetadataTypeDefinitionTableRow[] types = new ICliMetadataTypeDefinitionTableRow[typeTables.Sum(p => p.Count)];
            for (int moduleIndex = 0, cOff = 0; moduleIndex < typeTables.Length; cOff += typeTables[moduleIndex++].Count)
                typeTables[moduleIndex].CopyTo(types, cOff);
            for (int i = 0, moduleIndex = 0, moduleOffset = 0; i < types.Length; i++, moduleOffset++)
            {
                if (moduleOffset >= typeTables[moduleIndex].Count)
                {
                    moduleIndex++;
                    moduleOffset = 0;
                }
                var typeRow = types[i];
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
                if (topLevelRows.Length != topLevelCount)
                {
                    CliMetadataTypeDefinitionTableRow[] topLevelRowsActual = new CliMetadataTypeDefinitionTableRow[topLevelCount];
                    Array.ConstrainedCopy(topLevelRows, 0, topLevelRowsActual, 0, topLevelCount);
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
            for (int i = 0; i < namespaceIndicesArray.Length; i++)
            {
                uint currentIndex = namespaceIndicesArray[i].Item2;
                if (currentIndex == 0)
                    continue;
                var currentRows = nonNestedTypes[currentIndex];
                var currentCount = nonNestedCounts[currentIndex];
                string currentString = modules[namespaceIndicesArray[i].Item1].MetadataRoot.StringsHeap[currentIndex];

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
                     * Position tracking, the first element doesn't 
                     * start with a '.', if it does, the split will
                     * show such, and the part will still not
                     * be '.'.
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
                        partIndex.Add(part, partId = (uint) part.GetHashCode());
                    int startIndex = currentLength;
                    currentLength += part.Length;
                    bool fullName = currentLength == currentString.Length;
                    CliNamespaceKeyedTreeNode next;
                    if (!current.TryGetValue(partId, out next))
                        if (fullName)
                            current = current.Add(partId, currentIndex, startIndex, currentLength - startIndex, currentRows);
                        else
                            current = current.Add(partId, currentIndex, startIndex, currentLength - startIndex);
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
            this.metadataRoot = null;
            this.namespaceInformation = null;
        }

        public ICliMetadataAssemblyTableRow Metadata { get; private set; }

        //#region ICliDeclaration Members

        ICliMetadataTableRow ICliDeclaration.Metadata
        {
            get { return this.Metadata; }
        }

        //#endregion

        internal string Location { get { return this.location; } }

        internal bool IsFromGlobalAssemblyCache { get; set; }

        public override string ToString()
        {
            return this.UniqueIdentifier.ToString();
        }

        public ICliType GetTypeByMetadata(ICliMetadataTypeDefinitionTableRow metadata)
        {
            return (ICliType) this.IdentityManager.ObtainTypeReference(metadata);
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name, string moduleName)
        {
            string ns = @namespace;

            int lastIndex = 0;
            IModule module;
            if (!this.Modules.TryGetValue(AstIdentifier.GetDeclarationIdentifier(moduleName), out module))
                return null;
            ICliModule cliModule = module as ICliModule;
            if (cliModule == null)
                return null;
            CliNamespaceKeyedTree topLevel = this.NamespaceInformation;
        nextPart:
            int next = ns.IndexOf('.', lastIndex);
            if (next != -1)
            {
                string current = ns.Substring(lastIndex, next - lastIndex);
                uint currentHash = (uint) current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    return null;
                lastIndex = next + 1;
                goto nextPart;
            }
            else
            {
                string current = ns.Substring(lastIndex);
                uint currentHash = (uint) current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    return null;
            }
            if (topLevel.NamespaceTypes != null)
            {
                foreach (var nsType in topLevel.NamespaceTypes)
                    if (nsType.MetadataRoot == cliModule.Metadata.MetadataRoot && nsType.Name == name)
                        return nsType;
            }
            return null;
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name)
        {
            string ns = @namespace;

            int lastIndex = 0;
            CliNamespaceKeyedTree topLevel = this.NamespaceInformation;
        nextPart:
            int next = ns.IndexOf('.', lastIndex);
            if (next != -1)
            {
                string current = ns.Substring(lastIndex, next - lastIndex);
                uint currentHash = (uint) current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    return null;
                lastIndex = next + 1;
                goto nextPart;
            }
            else
            {
                string current = ns.Substring(lastIndex);
                uint currentHash = (uint) current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    return null;
            }
            if (topLevel.NamespaceTypes != null)
            {
                foreach (var nsType in topLevel.NamespaceTypes)
                    if (nsType.Name == name)
                        return nsType;
            }
            return null;
        }

        internal INamespaceDeclaration GetNamespace(string namespaceName)
        {
            return this.Namespaces[namespaceName];
        }
    }
}
