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
        private KeyedTree<uint, CliNamespaceInfo> namespaceInformation;

        public CliAssembly(string location, CliManager identityManager, CliMetadataRoot metadataRoot, IAssemblyUniqueIdentifier uniqueIdentifier, IStrongNamePublicKeyInfo strongNameInfo)
        {
            this.location = location;
            this.metadataRoot = metadataRoot;
            this.uniqueIdentifier = uniqueIdentifier;
            this.strongNameInfo = strongNameInfo;
            this.identityManager = identityManager;
            Stopwatch sw = Stopwatch.StartNew();
            this.InitializeCommon();
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
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
            throw new NotImplementedException();
        }

        protected override INamespaceDictionary InitializeNamespaces()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        protected override IModule OnGetManifestModule()
        {
            throw new NotImplementedException();
        }

        protected override IStrongNamePublicKeyInfo OnGetPublicKeyInfo()
        {
            return this.strongNameInfo;
        }

        public override IAssemblyUniqueIdentifier UniqueIdentifier
        {
            get { return this.uniqueIdentifier; }
        }

        //#region ICompiledAssembly Members

        public ICliRuntimeEnvironmentInfo RuntimeEnvironment
        {
            get { return this.identityManager.RuntimeEnvironment; }
        }

        public ICliManager IdentityManager
        {
            get { return this.identityManager; }
        }

        public CliMetadataRoot MetadataRoot
        {
            get { return this.metadataRoot; }
        }

        //#endregion

        private void InitializeCommon()
        {
            var typeTable = (CliMetadataTypeDefinitionTable) metadataRoot.TableStream.TypeDefinitionTable;
            if (typeTable == null)
                return;
            /* *
             * We're doing a scan of all namespace names,
             * thus reading them in, in one shot, would 
             * save on frequent seeks and other checks.
             * */
            HashSet<uint> namespaceIndices = new HashSet<uint>();

            Dictionary<uint, CliMetadataTypeDefinitionTableRow[]> nonNestedTypes = new Dictionary<uint, CliMetadataTypeDefinitionTableRow[]>();
            Dictionary<uint, int> nonNestedCounts = new Dictionary<uint, int>();
            /* *
             * Breakdown the types by their namespace index, and
             * create a unique list of namespaceIndices via a HashSet<T>.
             * */
            ICliMetadataTypeDefinitionTableRow[] types = new ICliMetadataTypeDefinitionTableRow[typeTable.Count];
            typeTable.CopyTo(types, 0);
            for (int i = 0; i < types.Length; i++)
            {
                var typeRow = types[i];
                if ((typeRow.TypeAttributes & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic ||
                    (typeRow.TypeAttributes & TypeAttributes.VisibilityMask) == TypeAttributes.Public)
                {
                    uint nsIndex = typeRow.NamespaceIndex;
                    namespaceIndices.Add(nsIndex);
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

            KeyedTree<uint, CliNamespaceInfo> partialOrFullNamespaces = new KeyedTree<uint, CliNamespaceInfo>();
            Dictionary<string, uint> partIndex = new Dictionary<string, uint>();
            for (int i = 0; i < namespaceIndicesArray.Length; i++)
            {
                uint currentIndex = namespaceIndicesArray[i];
                var currentRows = nonNestedTypes[currentIndex];
                var currentCount = nonNestedCounts[currentIndex];
                string currentString = metadataRoot.StringsHeap[currentIndex];

                /* *
                 * Eliminate the null row entries by reducing the sizes
                 * of the arrays.
                 * */
                if (currentRows.Length != currentCount)
                {
                    var currentRowsCopy = new CliMetadataTypeDefinitionTableRow[currentCount];
                    Array.ConstrainedCopy(currentRows, 0, currentRowsCopy, 0, currentCount);
                    currentRows = currentRowsCopy;
                }


                KeyedTree<uint, CliNamespaceInfo> current = partialOrFullNamespaces;
                string[] breakdown = currentString.Split(new[] { "." }, StringSplitOptions.None);
                int currentLength = 0;
                bool first = true;
                foreach (var part in breakdown)
                {
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
                     * */
                    if (!partIndex.TryGetValue(part, out partId))
                        partIndex.Add(part, partId = (uint) partIndex.Count);
                    int startIndex = currentLength;
                    currentLength += part.Length;
                    bool fullName = currentLength == currentString.Length;
                    KeyedTreeNode<uint, CliNamespaceInfo> next;
                    if (!current.TryGetValue(partId, out next))
                        if (fullName)
                            current = current.Add(partId, new CliNamespaceInfo(currentIndex, startIndex, currentLength - startIndex, currentRows));
                        else
                            current = current.Add(partId, new CliNamespaceInfo(currentIndex, startIndex, currentLength - startIndex));
                    else
                        current = next;
                }
            }
            this.namespaceInformation = partialOrFullNamespaces;
        }

    }
}
