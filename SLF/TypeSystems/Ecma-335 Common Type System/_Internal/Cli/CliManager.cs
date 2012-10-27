using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Documentation;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using System.Threading;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliManager :
        _ICliManager
    {
        private TypeCache typeCache;
        private Dictionary<string, IAssemblyUniqueIdentifier> fileIdentifiers = new Dictionary<string, IAssemblyUniqueIdentifier>();
        private Dictionary<IAssemblyUniqueIdentifier, CliAssembly> loadedAssemblies = new Dictionary<IAssemblyUniqueIdentifier, CliAssembly>();
        private Dictionary<string, Tuple<PEImage, CliMetadataRoot, string>> loadedModules = new Dictionary<string, Tuple<PEImage, CliMetadataRoot, string>>();
        private MultikeyedDictionary<CliAssembly, string, CliModule> moduleAssemblyIdentities = new MultikeyedDictionary<CliAssembly, string, CliModule>();
        private Dictionary<Type, IType> systemTypeCache = new Dictionary<Type, IType>();
        private IDictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType> baseTypeKinds = new Dictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType>();
        private IDictionary<ICliMetadataTypeDefOrRefRow, BaseKindCacheType> refBaseTypeKinds = new Dictionary<ICliMetadataTypeDefOrRefRow, BaseKindCacheType>();
        private object cacheLock = new object();
        private object cacheClearLock = new object();
        private MetadataService metadataService;
        //"System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"

        private ICliRuntimeEnvironmentInfo runtimeEnvironment;
        /// <summary>
        /// Creates a new <see cref="CliManager"/> with the 
        /// <paramref name="runtimeEnvironment"/> specified.
        /// </summary>
        /// <param name="runtimeEnvironment">The <see cref="ICliRuntimeEnvironmentInfo"/>
        /// which the <see cref="CliManager"/> targets.</param>
        public CliManager(ICliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            this.typeCache = new TypeCache(this);
            this.runtimeEnvironment = runtimeEnvironment;
        }

        //#region ITypeIdentityManager<Type> Members

        public IType ObtainTypeReference(Type typeIdentity)
        {
            lock (cacheClearLock)
            {
                /* *
                 * Notes: First, remove byref
                 *        Second, remove pointers 
                 *        Third, remove arrays
                 *        Fourth, remove 'nullable' status
                 *        Finally, Breakdown generic-type
                 * */
                /*-------------------------------------------------------*\
                |  Cache note: The breakdown stage cache points are to    |
                |              re-establish the System.Type with the      |
                |              staged built IType that results from       |
                |              this call.  So each permutation            |
                |              is available in cache for later use.       |
                |---------------------------------------------------------|
                |              This reduces further call overhead with    |
                |              a light memory cost and single reference   |
                |              per stage.                                 |
                \*-------------------------------------------------------*/
                Type t = typeIdentity;
                if (t == null)
                    throw new ArgumentNullException("type");
                IType result;
                lock (cacheLock)
                    if (systemTypeCache.TryGetValue(t, out result))
                        return result;
                Type byRefType = null;
                #region Type breakdown

                #region ByReference
                bool byRef = t.IsByRef;
                if (byRef)
                {
                    byRefType = t;
                    t = t.GetElementType();
                }
                #endregion

                #region Array Breakdown

                int arrayDepth = 0;
                for (Type j = t; j.IsArray; j = j.GetElementType())
                    arrayDepth++;
                int[] ranks = new int[arrayDepth];
                int rankCount = arrayDepth;
                arrayDepth = 0;
                /* *
                 * Fill in the array ranks.
                 * Ignore that it's reversed order since 
                 * the type system by default is in reverse rank
                 * order.
                 * */
                Stack<Type> arrayTypes = new Stack<Type>();
                for (; t.IsArray; t = t.GetElementType())
                {
                    ranks[rankCount - ++arrayDepth] = t.GetArrayRank();
                    arrayTypes.Push(t);
                }
                if (arrayTypes.Count == 0)
                    arrayTypes = null;
                #endregion

                #region Pointers
                int ptrThreshold = 0;
                Stack<Type> pointerTypes = new Stack<Type>();
                while (t.IsPointer)
                {
                    pointerTypes.Push(t);
                    t = t.GetElementType();
                    ptrThreshold++;
                }
                if (pointerTypes.Count == 0)
                    pointerTypes = null;
                #endregion

                #region Nullable breakdown
                bool nullable = false;
                Type nullableType = null;
                if (t.IsGenericType && (!t.IsGenericTypeDefinition && t.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    nullableType = t;
                    nullable = true;
                    t = t.GetGenericArguments()[0];
                }
                #endregion

                #region Closed GenericType breakdown
                ITypeCollection typeParameters = null;
                Type closedGenericType = null;
                if (t.IsGenericType && !t.IsGenericTypeDefinition)
                {
                    closedGenericType = t;
                    typeParameters = new LockedTypeCollection(from gArg in t.GetGenericArguments()
                                                              select this.ObtainTypeReference(gArg));
                    //typeParameters = t.GetGenericArguments().ToCollection(this);
                    t = t.GetGenericTypeDefinition();
                }
                #endregion

                #endregion

                #region Initial type instantiation/retrieval.
                bool cacheLockEntered = false;
                Monitor.Enter(cacheLock, ref cacheLockEntered);
                if (systemTypeCache.ContainsKey(t))
                {
                    result = systemTypeCache[t];
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                }
                else if (t.IsGenericParameter)
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    #region GenericParameter-Parameter resolution.
                    bool positiveMatch = true;
                    if (t.DeclaringMethod == null && t.DeclaringType != null)
                    {
                        IGenericType lookupPoint = (IGenericType) this.ObtainTypeReference(t.DeclaringType);
                        if (!(t.GenericParameterPosition < 0 || t.GenericParameterPosition >= lookupPoint.GenericParameters.Count))
                        {
                            result = lookupPoint.GenericParameters[t.GenericParameterPosition];
                            positiveMatch = true;
                        }
                    }
                    else if (t.DeclaringMethod != null)
                    {
                        IType lookupAid = this.ObtainTypeReference(t.DeclaringMethod.DeclaringType);

                    }
                    #endregion
                positiveExit:
                    if (!positiveMatch)
                        return null;
                }
                else if (t.IsEnum)
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    result = ObtainLocalizedTypeReference(t, TypeKind.Enumeration);
                }
                else if (t.IsSubclassOf(typeof(Delegate)) && t != typeof(MulticastDelegate))
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    result = ObtainLocalizedTypeReference(t, TypeKind.Delegate);
                    //result = new CompiledDelegateType(t, this);
                }
                else if (t.IsClass)
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    result = ObtainLocalizedTypeReference(t, TypeKind.Class);
                }
                else if (t.IsValueType ||
                         t == typeof(Enum))
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    result = ObtainLocalizedTypeReference(t, TypeKind.Struct);
                }
                else if (t.IsInterface)
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    result = ObtainLocalizedTypeReference(t, TypeKind.Interface);
                }
                #endregion

                CacheAdd(t, result);

                #region GenericParameter production
                IGenericType gType;
                if (typeParameters != null && result.IsGenericConstruct && result is IGenericType && (gType = ((IGenericType) (result))).IsGenericDefinition)
                {
                    result = gType.MakeGenericClosure(typeParameters);
                    CacheAdd(closedGenericType, result);
                }
                if (nullable)
                    CacheAdd(nullableType, result = result.MakeNullable());
                if (ptrThreshold > 0)
                    while (ptrThreshold-- > 0)
                        CacheAdd(pointerTypes.Pop(), result = result.MakePointer());
                if (arrayTypes != null)
                {
                    for (int i = 0; i < arrayDepth; i++)
                    {
                        var arrayType = arrayTypes.Pop();
                        if (ranks[i] == 1)
                        {
                            //Make sure it's a vector array.
                            if (arrayType.GetElementType().MakeArrayType() == arrayType)
                                CacheAdd(arrayType, result = result.MakeArray(ranks[i]));
                            else
                                /* *
                                 * Single-dimensional arrays which are not equivalent to the 
                                 * above 'make vector array' result are not supported
                                 * as actual types of elements.
                                 * */
                                CacheAdd(arrayType, result = result.MakeArray(new int[] { 0 }));
                        }
                        else
                            CacheAdd(arrayType, result = result.MakeArray(ranks[i]));
                    }
                }
                if (byRef)
                    CacheAdd(byRefType, result = result.MakeByReference());
                #endregion
                return result;
            }
        }

        private IType ObtainLocalizedTypeReference(Type baseElementType, TypeKind typeKind)
        {
            var typeHandle = (uint)baseElementType.MetadataToken;
            var typeIndex = typeHandle & 0x00FFFFFF;
            CliMetadataTableKinds typeTable = (CliMetadataTableKinds) ((ulong)1 << (int)((typeHandle & 0xFF000000U) >> 24));
            var assembly = (ICliAssembly)this.ObtainAssemblyReference(baseElementType.Assembly);
            var tableStream = assembly.MetadataRoot.TableStream;
            if (typeTable == CliMetadataTableKinds.GenericParameter)
            {
                var entry = tableStream.GenericParameterTable[(int)typeIndex];
                switch (entry.OwnerSource)
                {
                    case CliMetadataTypeOrMethodDef.TypeDefinition:
                        var parentEntry = tableStream.TypeDefinitionTable[(int)entry.OwnerIndex];
                        var owner = this.ObtainTypeReference(parentEntry);
                        if (owner is IGenericType)
                            return (IGenericParameter)((IGenericType)owner).TypeParameters.Values[entry.Number];
                        break;
                    case CliMetadataTypeOrMethodDef.MethodDefinition:
                        var methodEntry = tableStream.MethodDefinitionTable[(int)entry.OwnerIndex];
                        bool found = false;
                        foreach (var type in tableStream.TypeDefinitionTable)
                        {
                            int index = type.Methods.IndexOf(methodEntry);
                            if (index > -1)
                            {
                                var typeRef = this.ObtainTypeReference(type);
                                if (typeRef is IMethodParent)
                                {
                                    var mTypeRef = (IMethodParent)typeRef;
                                    var method = (IMethodSignatureMember)mTypeRef.Methods[index];
                                    return (IGenericParameter)method.TypeParameters.Values[methodEntry.TypeParameters.IndexOf(entry)];
                                }
                                break;
                            }
                        }
                        break;
                }
            }
            else if (typeTable == CliMetadataTableKinds.TypeDefinition)
            {
                var def = tableStream.TypeDefinitionTable[(int)typeIndex];
                return this.ObtainTypeReference(def);
            }
            throw new NotSupportedException();
        }

        //#endregion

        private void CacheAdd(Type t, IType result)
        {
            lock (cacheLock)
                if (!systemTypeCache.ContainsKey(t))
                {
#if DEBUG
                    if (!t.HasElementType)
                        Debug.WriteLine("Cached {0}.", t);
#endif
                    systemTypeCache.Add(t, result);
                }
        }
        //#region ITypeIdentityManager Members

        public IType ObtainTypeReference(object typeIdentity)
        {
            if (typeIdentity is string)
                return ObtainTypeReference((string)typeIdentity);
            else if (typeIdentity is Type)
                return ObtainTypeReference((Type)typeIdentity);
            else if (typeIdentity is PrimitiveType)
                return ObtainTypeReference((PrimitiveType)typeIdentity);
            else if (typeIdentity is ICliMetadataTypeDefOrRefRow)
                return this.ObtainTypeReference((ICliMetadataTypeDefOrRefRow)typeIdentity);
            else if (typeIdentity is IGeneralTypeUniqueIdentifier)
                return this.ObtainTypeReference((IGeneralTypeUniqueIdentifier)typeIdentity);
            else if (typeIdentity is RuntimeCoreType)
                return this.ObtainTypeReference((RuntimeCoreType)(typeIdentity));
            else if (typeIdentity is CliRuntimeCoreType)
                return this.ObtainTypeReference((CliRuntimeCoreType)(typeIdentity));
            throw new ArgumentOutOfRangeException("typeIdentity");
        }

        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            lock (this.loadedAssemblies)
            {
                foreach (var assembly in this.loadedAssemblies.Values)
                    assembly.Dispose();
                this.loadedAssemblies.Clear();
                this.fileIdentifiers.Clear();
            }
        }

        //#endregion
        //#region IAssemblyIdentityManager<Assembly,ICompiledAssembly> Members

        public IAssembly ObtainAssemblyReference(Assembly assemblyIdentity)
        {
            /* *
             * Return the assembly relative to the current runtime environment,
             * using its location might load the wrong one.
             * */

            return this.ObtainAssemblyReference(assemblyIdentity.Location);
        }

        //#endregion

        //#region ICliManager Members
        public ICliRuntimeEnvironmentInfo RuntimeEnvironment
        {
            get { return this.runtimeEnvironment; }
        }


        public ICliMetadataModuleTableRow LoadModule(ICliMetadataModuleReferenceTableRow metadata)
        {
            if (metadata.MetadataRoot == null)
                throw new ArgumentException("metadata must contain proper root.");
            ICliMetadataFileTable fileTable;
            var relativeAssembly = GetRelativeAssembly(metadata.MetadataRoot);
            if (relativeAssembly == null)
                throw new ArgumentException("No assembly loaded for metadata.", "metadata");
            if ((fileTable = metadata.MetadataRoot.TableStream.FileTable) != null)
            {
                fileTable.Read();
                ICliMetadataFileTableRow toCheck = null;
                foreach (var file in fileTable)
                {
                    /* *
                     * Valid metadata.
                     * */
                    if (file.NameIndex == metadata.NameIndex &&
                        file.Flags == CliMetadataFileAttributes.ContainsMetadata)
                    {
                        toCheck = file;
                        break;
                    }
                }
                if (toCheck == null)
                    throw new BadImageFormatException("There is no file entry in the metadata for the module.");
                Tuple<PEImage, CliMetadataRoot, string> valid = null;

                foreach (var path in (from dirInfo in this.RuntimeEnvironment.ResolutionPaths
                                      select dirInfo.FullName).Concat<string>(new string[] { Path.GetDirectoryName(relativeAssembly.Location) }).Distinct().ToArray())
                {
                    var currentFilename = string.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, toCheck.Name);
                    if (!File.Exists(currentFilename))
                        continue;
                    if (this.loadedModules.ContainsKey(path))
                    {
                        CliModule result;
                        if (this.moduleAssemblyIdentities.TryGetValue(relativeAssembly, currentFilename, out result))
                            return result.Metadata;
                    }
                    var current = CliCommon.CheckFilename(currentFilename, false);
                    if (current == null)
                        continue;
                    else
                    {
                        valid = Tuple.Create(current.Item3, current.Item4, current.Item6);
                        break;
                    }
                }

                if (valid == null)
                    throw new FileNotFoundException("The file associated to the module to load cannot be found.", toCheck.Name);
                if (valid.Item2.TableStream.ModuleTable == null ||
                    valid.Item2.TableStream.ModuleTable.Count == 0)
                    throw new BadImageFormatException("No module module table present in the metadata.");
                this.loadedModules.Add(valid.Item3, valid);
                return valid.Item2.TableStream.ModuleTable[1];

            }
            throw new NotImplementedException();
        }


        internal CliAssembly GetRelativeAssembly(ICliMetadataRoot root)
        {
            if (root != null)
                foreach (var assembly in this.loadedAssemblies.Values)
                    if (assembly.MetadataRoot == root)
                        return assembly;
                    else
                        foreach (var module in assembly.Modules.Values.Skip(1))
                        {
                            var cliModule = module as ICliModule;
                            if (cliModule == null)
                                continue;
                            if (cliModule.Metadata.MetadataRoot == root)
                                return assembly;
                        }
            return null;
        }

        //#endregion

        //#region ITypeIdentityManager<string> Members

        public IType ObtainTypeReference(string typeIdentity)
        {
            throw new NotImplementedException();
        }

        //#endregion

        //#region ITypeIdentityManager<CliMetadataTypeDefinitionTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            return this.typeCache.ObtainTypeReference(typeIdentity);
        }

        private ICliMetadataTypeDefinitionTableRow ResolveScope(ICliMetadataTypeDefOrRefRow typeIdentity, Func<_ICliManager, ICliMetadataTypeDefinitionTableRow, bool> selectionPredicate = null, bool typeSpec = false)
        {
            return CliCommon.ResolveScope(typeIdentity, this, selectionPredicate, typeSpec);
        }

        private ICliMetadataTypeDefinitionTableRow ResolveScope(ICliMetadataTypeSignature typeIdentity, Func<ICliMetadataTypeDefinitionTableRow, bool> selectionPredicate, bool typeSpec)
        {
            throw new NotImplementedException();
        }
        //#endregion

        //#region ITypeIdentityManager<CliMetadataTypeRefTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeDefOrRefRow typeIdentity)
        {
            switch (typeIdentity.TypeDefOrRefEncoding)
            {
                case CliMetadataTypeDefOrRefTag.TypeDefinition:
                    if (typeIdentity is ICliMetadataTypeDefinitionTableRow)
                        return this.ObtainTypeReference((ICliMetadataTypeDefinitionTableRow) typeIdentity);
                    throw new ArgumentException(string.Format("Type identity not of required type '{0}'.", typeof(ICliMetadataTypeDefinitionTableRow)), "typeIdentity");
                case CliMetadataTypeDefOrRefTag.TypeReference:
                    if (typeIdentity is ICliMetadataTypeRefTableRow)
                        return this.ObtainTypeReference((ICliMetadataTypeRefTableRow) typeIdentity);
                    throw new ArgumentException(string.Format("Type identity not of required type '{0}'.", typeof(ICliMetadataTypeRefTableRow)), "typeIdentity");
                    throw new NotSupportedException();
                case CliMetadataTypeDefOrRefTag.TypeSpecification:
                    if (typeIdentity is ICliMetadataTypeSpecificationTableRow)
                        return this.ObtainTypeReference((ICliMetadataTypeSpecificationTableRow) typeIdentity);
                    throw new ArgumentException(string.Format("Type identity not of required type '{0}'.", typeof(ICliMetadataTypeSpecificationTableRow)), "typeIdentity");
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException("typeIdentity");
            }
        }

        public IType ObtainTypeReference(ICliMetadataTypeRefTableRow typeIdentity)
        {
            switch (typeIdentity.ResolutionScope)
            {
                case CliMetadataResolutionScopeTag.Module:
                    {
                        /* *
                         * Shouldn't appear in compliant compilers which
                         * compress the metadata into the smallest form.
                         * *
                         * This should support compilers that are not CLS
                         * compliant.
                         * */
                        var assembly = this.GetRelativeAssembly(typeIdentity.MetadataRoot);
                        if (assembly == null)
                        {
                            var identifier = CliCommon.GetAssemblyUniqueIdentifier(new Tuple<PEImage, ICliMetadataRoot>(typeIdentity.MetadataRoot.SourceImage, typeIdentity.MetadataRoot));
                            if (identifier == null)
                            {
                                /* *
                                 * Must be from a module not loaded by this manager.
                                 * */
                                var typeTable = typeIdentity.MetadataRoot.TableStream.TypeDefinitionTable;
                                if (typeTable == null)
                                    throw new TypeLoadException("Type reference not resolveable.");
                                typeTable.Read();
                                foreach (var typeDef in typeTable)
                                    if (typeDef.NameIndex == typeIdentity.NameIndex &&
                                        typeDef.NamespaceIndex == typeIdentity.NamespaceIndex)
                                        return this.ObtainTypeReference(typeDef);
                                /* *
                                 * First pass failed, since it's already not a compressed metadata
                                 * stream, it's likely they didn't condense the names either.
                                 * *
                                 * Start off by caching the name/namespace, to reduce the number of
                                 * string heap fetches.
                                 * */
                                string tidN = typeIdentity.Name,
                                    tidNS = typeIdentity.Namespace;
                                foreach (var typeDef in typeTable)
                                    if (typeDef.Name == tidN &&
                                        typeDef.Namespace == tidNS)
                                        return this.ObtainTypeReference(typeDef);
                                throw new TypeLoadException("Type reference not resolveable.");
                            }
                            else
                            {
                                CliAssembly identitySource;
                                this.loadedAssemblies.Add(identifier.Item2, identitySource = new CliAssembly(this, identifier.Item3, identifier.Item2, identifier.Item1));
                                var localizedType = identitySource.FindType(typeIdentity.Namespace, typeIdentity.Name);
                                if (localizedType == null)
                                    throw new TypeLoadException(string.Format("Cannot find type \"{0}.{1}, {2}\"", typeIdentity.Namespace, typeIdentity.Name, identitySource.UniqueIdentifier));
                                return this.ObtainTypeReference(localizedType);
                            }
                        }
                        break;
                    }
                case CliMetadataResolutionScopeTag.ModuleReference:
                    {
                        var assembly = this.GetRelativeAssembly(typeIdentity.MetadataRoot);
                        if (assembly == null)
                        {
                            var identifier = CliCommon.GetAssemblyUniqueIdentifier(new Tuple<PEImage, ICliMetadataRoot>(typeIdentity.MetadataRoot.SourceImage, typeIdentity.MetadataRoot));
                            if (identifier == null)
                            {
                                var loadedModule = this.LoadModule((ICliMetadataModuleReferenceTableRow) typeIdentity.Source);
                                if (loadedModule == null)
                                    throw new TypeLoadException(string.Format("Module {0} containing type {1} could not be loaded.", typeIdentity.Source.Name, typeIdentity.Name));

                                var typeTable = typeIdentity.MetadataRoot.TableStream.TypeDefinitionTable;
                                if (typeTable == null)
                                    throw new TypeLoadException("Type reference not resolveable.");
                                typeTable.Read();
                                /* *
                                 * Name index quick check doesn't work here, they're from
                                 * different string heaps.
                                 * */
                                string tidN = typeIdentity.Name,
                                       tidNS = typeIdentity.Namespace;
                                foreach (var typeDef in typeTable)
                                    if (typeDef.Name == tidN &&
                                        typeDef.Namespace == tidNS)
                                        return this.ObtainTypeReference(typeDef);
                                throw new TypeLoadException("Type reference not resolveable.");
                            }
                        }
                        {
                            var typeDef = assembly.FindType(typeIdentity.Namespace, typeIdentity.Name, typeIdentity.Source.Name);
                            return this.ObtainTypeReference(typeDef);
                        }
                    }
                case CliMetadataResolutionScopeTag.AssemblyReference:
                    {
                        var assemblyRef = typeIdentity.Source as ICliMetadataAssemblyRefTableRow;
                        if (assemblyRef == null)
                            throw new TypeLoadException("Assembly reference for type identity could not be resolved.");
                        var assembly = this.ObtainAssemblyReference(assemblyRef);
                        if (assembly is ICliAssembly)
                        {
                            var cliAssembly = (ICliAssembly)assembly;
                            var typeDef = cliAssembly.FindType(typeIdentity.Namespace, typeIdentity.Name);
                            return this.ObtainTypeReference(typeDef);
                        }
                        break;
                    }
                case CliMetadataResolutionScopeTag.TypeReference:
                    {
                        var declaringTypeRef = this.ObtainTypeReference((ICliMetadataTypeRefTableRow) typeIdentity.Source);
                        if (declaringTypeRef is ICliTypeParent)
                        {
                            var dtp = declaringTypeRef as ICliTypeParent;
                            var typeDef = dtp.FindType(typeIdentity.Namespace, typeIdentity.Name);
                            return this.ObtainTypeReference(typeDef);
                        }
                        else
                            throw new TypeLoadException("Resulted type exists as a child of a type which should not contain nested types.");
                    }
            }
            throw new TypeLoadException("Invalid resolution scope.");
        }

        //#endregion

        //#region IAssemblyIdentityManager<IAssemblyUniqueIdentifier,ICompiledAssembly> Members

        public virtual IAssembly ObtainAssemblyReference(IAssemblyUniqueIdentifier assemblyIdentity)
        {
            return ObtainAssemblyReference(assemblyIdentity, null);
        }

        private IAssembly ObtainAssemblyReference(IAssemblyUniqueIdentifier assemblyIdentity, IAssembly relativeSource)
        {
            if (assemblyIdentity == null)
                throw new ArgumentNullException("assemblyIdentity");
            CliAssembly result;
            string[] extensions = new string[] { "exe", "dll" };
            bool gacAssembly = false;
            ICliRuntimeEnvironmentInfo runtimeEnvironment = this.runtimeEnvironment;
            if (relativeSource != null)
            {
                if (relativeSource is ICliAssembly)
                {
                    var cliAssem = (ICliAssembly)relativeSource;
                    if (cliAssem.RuntimeEnvironment.Version != runtimeEnvironment.Version ||
                        cliAssem.RuntimeEnvironment.Platform != runtimeEnvironment.Platform)
                    {
                        runtimeEnvironment = cliAssem.RuntimeEnvironment;
                    }
                }
            }
            lock (this.loadedAssemblies)
            {
                if (!this.loadedAssemblies.TryGetValue(assemblyIdentity, out result))
                {
                    var baseName = assemblyIdentity.Name;
                    Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string> validResult = null;
                    /* *
                     * Resolution method:
                     * Runtime folder, GAC Cache
                     * */
                    if (runtimeEnvironment.UseGlobalAccessCache)
                        foreach (var path in runtimeEnvironment.GetGacLocationsFor(assemblyIdentity))
                        {
                            string extension = "exe";
                        checkAgain:
                            var check = CliCommon.CheckFilename(path.FullName, baseName, extension);
                            if (check != null && check.Item1.Equals(assemblyIdentity))
                            {
                                validResult = check;
                                gacAssembly = true;
                                break;
                            }
                            if (validResult == null && extension == "exe")
                            {
                                extension = "dll";
                                goto checkAgain;
                            }
                        }
                    if (validResult == null)
                    {
                        foreach (var path in runtimeEnvironment.ResolutionPaths)
                        {
                            string extension = "exe";
                        checkAgain:
                            var check = CliCommon.CheckFilename(path.FullName, baseName, extension);
                            if (check != null && check.Item1.Equals(assemblyIdentity))
                            {
                                validResult = check;
                                break;
                            }
                            if (validResult == null && extension == "exe")
                            {
                                extension = "dll";
                                goto checkAgain;
                            }
                        }
                    }
                    if (validResult != null)
                    {
                        this.loadedAssemblies.Add(validResult.Item1, result = new CliAssembly(this, validResult.Item5, validResult.Item1, validResult.Item2));
                        if (gacAssembly)
                            result.IsFromGlobalAssemblyCache = true;
                        //result.InitializeCommon();
                        this.fileIdentifiers.Add(validResult.Item6, validResult.Item1);
                        return result;
                    }
                    else
                        throw new FileNotFoundException(string.Format("Assembly {0} not found.", assemblyIdentity));
                }
                else
                    return result;
            }
        }

        //#endregion


        //#region IAssemblyIdentityManager<string,ICompiledAssembly> Members


        /// <summary>
        /// Obtains a <see cref="IAssembly"/> reference by
        /// the filename.
        /// </summary>
        /// <param name="filename">The <see cref="String"/> value
        /// which denotes the location of the assembly image.</param>
        /// <returns>A <see cref="IAssembly"/>
        /// which denotes the assembly in question.</returns>
        /// <exception cref="System.IO.FileNotFoundException">thrown when 
        /// <paramref name="filename"/> was not found.</exception>
        public IAssembly ObtainAssemblyReference(string filename)
        {
            filename = CliCommon.MinimizeFilename(filename);
            IAssemblyUniqueIdentifier uniqueIdentifier;
            if (!this.fileIdentifiers.TryGetValue(filename, out uniqueIdentifier))
            {
                if (File.Exists(filename))
                {
                    var peAndMetadata = CliCommon.LoadAssemblyMetadata(filename);
                    var peImage = peAndMetadata.Item1;
                    var metadataRoot = peAndMetadata.Item2;
                    using (peImage)
                    {
                        var imageKind = peImage.ExtendedHeader.ImageKind;
                        bool supportedOnPlatform = false;
                        if (this.runtimeEnvironment.Platform == CliFrameworkPlatform.x64Platform)
                        {
                            /* *
                             * x64 is backwards compatible with x86, therefore
                             * if it doesn't require a 32 bit mode, it should be valid, 
                             * since it targets the Any runtimeEnvironment, but the PE Header
                             * is PE32.
                             * */
                            CliHeader header;
                            if (imageKind == PEImageKind.x86Image &&
                                ((((header = metadataRoot.Header).Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly) &&
                                  ((header.Flags & CliRuntimeFlags.Requires32BitProcess) != CliRuntimeFlags.Requires32BitProcess)))
                                supportedOnPlatform = true;
                            /* *
                             * In this case regardless of whether they have IL only 
                             * */
                            else if (imageKind == PEImageKind.x64Image)
                                supportedOnPlatform = true;
                        }
                        else if (this.runtimeEnvironment.Platform == CliFrameworkPlatform.x86Platform)
                        {
                            /* *
                             * ... however, the reverse is not true.
                             * */
                            if (imageKind == PEImageKind.x64Image)
                                supportedOnPlatform = false;
                            else if (imageKind == PEImageKind.x86Image)
                                supportedOnPlatform = true;
                        }
                        else if (this.runtimeEnvironment.Platform == CliFrameworkPlatform.AnyPlatform)
                        {
                            CliHeader header = metadataRoot.Header;
                            if (imageKind == PEImageKind.x64Image &&
                                ((header.Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly))
                                supportedOnPlatform = true;
                            else if (imageKind == PEImageKind.x86Image &&
                                ((header.Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly) &&
                                (header.Flags & CliRuntimeFlags.Requires32BitProcess) != CliRuntimeFlags.Requires32BitProcess)
                                supportedOnPlatform = true;
                        }
                        if (supportedOnPlatform)
                        {
                            if (metadataRoot.TableStream.ContainsKey(CliMetadataTableKinds.Assembly))
                            {
                                var pubKeyId = CliCommon.GetAssemblyUniqueIdentifier(metadataRoot.TableStream.AssemblyTable[1]);
                                var firstAssemblyRow = pubKeyId.Item3;
                                IAssemblyUniqueIdentifier assemblyUniqueIdentifier = pubKeyId.Item2;
                                if (assemblyUniqueIdentifier == null)
                                    this.ThrowNoMetadataFound();
                                IStrongNamePublicKeyInfo publicKeyInfo = pubKeyId.Item1;
                                CliAssembly result;
                                if (this.loadedAssemblies.ContainsKey(assemblyUniqueIdentifier))
                                {
                                    peImage.Dispose();
                                    metadataRoot.Dispose();
                                    return this.loadedAssemblies[assemblyUniqueIdentifier];
                                }
                                loadedAssemblies.Add(assemblyUniqueIdentifier, result = new CliAssembly(this, firstAssemblyRow, assemblyUniqueIdentifier, publicKeyInfo));
                                //result.InitializeCommon();
                                this.fileIdentifiers.Add(filename, assemblyUniqueIdentifier);
                                return result;
                            }
                        }
                        else
                            throw new BadImageFormatException(string.Format("Expecting {0} image, but got {1}.", this.runtimeEnvironment.Platform, imageKind));
                    }
                }
                else
                    throw new FileNotFoundException("AssemblyIdentity not found.", filename);
            }
            return this.loadedAssemblies[this.fileIdentifiers[filename]];
        }

        private void ThrowNoMetadataFound()
        {
            throw new BadImageFormatException("No Assembly metadata entry found.");
        }

        //#endregion

        //#region IAssemblyIdentityManager<CliMetadataAssemblyTableRow,ICompiledAssembly> Members

        public IAssembly ObtainAssemblyReference(ICliMetadataAssemblyTableRow assemblyIdentity)
        {
            var identity = CliCommon.GetAssemblyUniqueIdentifier(assemblyIdentity);
            CliAssembly result;
            if (!this.loadedAssemblies.TryGetValue(identity.Item2, out result))
            {
                result = new CliAssembly(this, assemblyIdentity, identity.Item2, identity.Item1);
                this.loadedAssemblies.Add(identity.Item2, result);
            }
            return this.loadedAssemblies[identity.Item2];
        }

        //#endregion

        //#region IAssemblyIdentityManager<CliMetadataAssemblyRefTableRow,ICompiledAssembly> Members

        public IAssembly ObtainAssemblyReference(ICliMetadataAssemblyRefTableRow assemblyIdentity)
        {
            CliAssembly result;
            var identity = CliCommon.GetAssemblyUniqueIdentifier(assemblyIdentity);
            if (!this.loadedAssemblies.TryGetValue(identity.Item2, out result))
            {
                string relativePath = Path.GetDirectoryName(assemblyIdentity.MetadataRoot.SourceImage.Filename);
                var baseName = identity.Item2.Name;
                Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string> validResult = null;
                var check = CliCommon.CheckFilename(relativePath, baseName, "exe");
                if (check != null && check.Item1.Equals(assemblyIdentity))
                    validResult = check;
                else
                {
                    check = CliCommon.CheckFilename(relativePath, baseName, "dll");
                    if (check != null && check.Item1.Equals(assemblyIdentity))
                        validResult = check;
                }
                if (validResult != null)
                {
                    this.loadedAssemblies.Add(validResult.Item1, result = new CliAssembly(this, validResult.Item5, validResult.Item1, validResult.Item2));
                    this.fileIdentifiers.Add(validResult.Item6, validResult.Item1);
                }
                else
                    return this.ObtainAssemblyReference(identity.Item2, this.GetRelativeAssembly(assemblyIdentity.MetadataRoot));
                return this.loadedAssemblies[identity.Item2];
            }
            return result;
        }

        //#endregion

        #region ITypeIdentityManager<ICliMetadataTypeSignature> Members

        public IType ObtainTypeReference(ICliMetadataTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            switch (typeIdentity.TypeSignatureKind)
            {
                case CliMetadataTypeSignatureKind.ArrayType:
                    return ObtainTypeReference((ICliMetadataArrayTypeSignature) typeIdentity, activeType, activeMethod);
                case CliMetadataTypeSignatureKind.ElementType:
                    return ObtainTypeReference((ICliMetadataElementTypeSignature) typeIdentity, activeType, activeMethod);
                case CliMetadataTypeSignatureKind.ElementTypeAndModifiers:
                    return ObtainTypeReference((ICliMetadataElementTypeAndModifiersSignature) typeIdentity, activeType, activeMethod);
                case CliMetadataTypeSignatureKind.GenericParameter:
                    return ObtainTypeReference((ICliMetadataGenericParameterTypeSignature) typeIdentity, activeType, activeMethod);
                case CliMetadataTypeSignatureKind.FunctionPointerType:
                    return ObtainTypeReference((ICliMetadataFunctionPointerTypeSignature) typeIdentity, activeType, activeMethod);
                case CliMetadataTypeSignatureKind.NativeType:
                    return ObtainTypeReference((ICliMetadataNativeTypeSignature) typeIdentity, activeType, activeMethod);
                case CliMetadataTypeSignatureKind.ReturnType:
                    return ObtainTypeReference((ICliMetadataReturnTypeSignature) typeIdentity, activeType, activeMethod);
                case CliMetadataTypeSignatureKind.ValueOrClassType:
                    return ObtainTypeReference((ICliMetadataValueOrClassTypeSignature) typeIdentity, activeType, activeMethod);
                case CliMetadataTypeSignatureKind.GenericInstType:
                    return ObtainTypeReference((ICliMetadataGenericInstanceTypeSignature) typeIdentity, activeType, activeMethod);
                case CliMetadataTypeSignatureKind.VectorArrayType:
                    return ObtainTypeReference((ICliMetadataVectorArrayTypeSignature) typeIdentity, activeType, activeMethod);
                default:
                    throw new NotSupportedException();
            }
        }

        private IType ObtainTypeReference(ICliMetadataArrayTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            var elementType = this.ObtainTypeReference(typeIdentity.ElementType);
            var shape = typeIdentity.Shape;
            int[] lowerBounds;
            if (shape.LowerBounds.Count < shape.Rank)
            {
                lowerBounds = new int[shape.Rank];
                shape.LowerBounds.CopyTo(lowerBounds);
            }
            else
                lowerBounds = shape.LowerBounds.ToArray();
            //ToDo: Add Sizes to type infrastructure.
            return elementType.MakeArray(lowerBounds);
        }

        private IType ObtainTypeReference(ICliMetadataElementTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            switch (typeIdentity.Classification)
            {
                case TypeElementClassification.Nullable:
                    return ObtainTypeReference(typeIdentity.ElementType).MakeNullable();
                case TypeElementClassification.Pointer:
                    return ObtainTypeReference(typeIdentity.ElementType).MakePointer();
                case TypeElementClassification.Reference:
                    return ObtainTypeReference(typeIdentity.ElementType).MakeByReference();
                default:
                    throw new NotSupportedException();
            }
        }

        private IType ObtainTypeReference(ICliMetadataElementTypeAndModifiersSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            return ((_ICliType) this.ObtainTypeReference((ICliMetadataElementTypeSignature) (typeIdentity))).MakeModified(typeIdentity.CustomModifiers);
        }

        private IType ObtainTypeReference(ICliMetadataGenericParameterTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            switch (typeIdentity.Parent)
            {
                case CliMetadataGenericParameterParent.Type:
                    if (activeType is IGenericType)
                        return ((IGenericType)(activeType)).GenericParameters[(int)typeIdentity.Position];
                    throw new ArgumentException("activeType");
                case CliMetadataGenericParameterParent.Method:
                    return activeMethod.GenericParameters[(int)typeIdentity.Position];
                default:
                    throw new ArgumentException("typeIdentity references a generic type of an unknown kind.", "typeIdentity");
            }
        }

        private IType ObtainTypeReference(ICliMetadataFunctionPointerTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            throw new NotImplementedException();
        }

        private IType ObtainTypeReference(ICliMetadataNativeTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            throw new NotImplementedException();
        }

        private IType ObtainTypeReference(ICliMetadataReturnTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            throw new NotImplementedException();
        }

        private IType ObtainTypeReference(ICliMetadataValueOrClassTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            throw new NotImplementedException();
        }

        private IType ObtainTypeReference(ICliMetadataGenericInstanceTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            throw new NotImplementedException();
        }

        private IType ObtainTypeReference(ICliMetadataVectorArrayTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod)
        {
            throw new NotImplementedException();
        }

        #endregion

        //#region ITypeIdentityManager Members

        IStandardRuntimeEnvironmentInfo ITypeIdentityManager.RuntimeEnvironment
        {
            get { return this.runtimeEnvironment; }
        }

        //#endregion
        private class M_T<T, D> :
            CliGenericTypeBase<T, D>,
            _ICliType
            where T :
        IGenericTypeUniqueIdentifier
            where D :
        IGenericType<T, D>
        {
            private _ICliManager creatingManager;

            internal M_T(TypeKind kind, ICliMetadataTypeDefinitionTableRow metadata, _ICliManager creatingManager, CliAssembly assembly)
                : base(assembly, metadata)
            {
                this.TypeKind = kind;
                this.MetadataEntry = metadata;
                this.creatingManager = creatingManager;
            }
            protected override IType OnGetDeclaringType()
            {
                if (this.MetadataEntry.DeclaringType == null)
                    return null;
                return this.creatingManager.ObtainTypeReference(this.MetadataEntry.DeclaringType);
            }

            private TypeKind TypeKind { get; set; }

            protected override TypeKind TypeImpl
            {
                get { return TypeKind; }
            }

            #region ICliType Members

            public new ICliAssembly Assembly
            {
                get
                {
                    return this.creatingManager.GetRelativeAssembly(this.MetadataEntry.MetadataRoot);
                }
            }

            public ICliMetadataTypeDefinitionTableRow MetadataEntry { get; private set; }

            #endregion


            #region ICliDeclaration Members

            ICliMetadataTableRow ICliDeclaration.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }

            #endregion

            #region _ICliType Members

            public IModifiedType MakeModified(IControlledCollection<ICliMetadataCustomModifierSignature> modifiers)
            {
                throw new NotSupportedException();
            }

            #endregion

            protected override IFullMemberDictionary OnGetMembers()
            {
                throw new NotImplementedException();
            }

            public override D MakeGenericClosure(ITypeCollectionBase typeParameters)
            {
                throw new NotImplementedException();
            }
        }

        private class M_T<T> :
            TypeBase<T>,
            _ICliType
        where T :
            ITypeUniqueIdentifier
        {
            private _ICliManager creatingManager;
            internal M_T(TypeKind kind, ICliMetadataTypeDefinitionTableRow metadata, _ICliManager creatingManager)
            {
                this.TypeKind = kind;
                this.MetadataEntry = metadata;
                this.creatingManager = creatingManager;
            }
            protected override IType OnGetDeclaringType()
            {
                if (this.MetadataEntry.DeclaringType == null)
                    return null;
                return this.creatingManager.ObtainTypeReference(this.MetadataEntry.DeclaringType);
            }

            private TypeKind TypeKind { get; set; }

            protected override TypeKind TypeImpl
            {
                get { return TypeKind; }
            }

            protected internal override bool CanCacheImplementsList
            {
                get { throw new NotImplementedException(); }
            }

            protected override ILockedTypeCollection OnGetImplementedInterfaces()
            {
                throw new NotImplementedException();
            }

            protected override IFullMemberDictionary OnGetMembers()
            {
                throw new NotImplementedException();
            }

            protected override INamespaceDeclaration OnGetNamespace()
            {
                throw new NotImplementedException();
            }

            protected override AccessLevelModifiers OnGetAccessLevel()
            {
                throw new NotImplementedException();
            }

            protected override IAssembly OnGetAssembly()
            {
                return this.Assembly;
            }

            public override bool IsGenericConstruct
            {
                get { throw new NotImplementedException(); }
            }

            protected override bool IsSubclassOfImpl(IType other)
            {
                throw new NotImplementedException();
            }

            protected override string OnGetNamespaceName()
            {
                return this.MetadataEntry.Namespace;
            }

            protected override IType BaseTypeImpl
            {
                get { throw new NotImplementedException(); }
            }

            protected override T OnGetUniqueIdentifier()
            {
                throw new NotImplementedException();
            }

            protected override IMetadataCollection InitializeCustomAttributes()
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
            {
                get { throw new NotImplementedException(); }
            }

            protected override ITypeIdentityManager OnGetManager()
            {
                return this.creatingManager;
            }

            protected override string OnGetName()
            {
                return this.MetadataEntry.Name;
            }

            public override string FullName
            {
                get
                {
                    if (this.MetadataEntry.NamespaceIndex == 0)
                        return string.Format("{0}", this.MetadataEntry.Name);
                    else
                        return string.Format("{0}.{1}", this.MetadataEntry.Namespace, this.MetadataEntry.Name);
                }
            }

            #region ICliType Members

            public new ICliAssembly Assembly
            {
                get
                {
                    return this.creatingManager.GetRelativeAssembly(this.MetadataEntry.MetadataRoot);
                }
            }

            public ICliMetadataTypeDefinitionTableRow MetadataEntry { get; private set; }

            #endregion


            #region ICliDeclaration Members

            ICliMetadataTableRow ICliDeclaration.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }

            #endregion

            #region _ICliType Members

            public IModifiedType MakeModified(IControlledCollection<ICliMetadataCustomModifierSignature> modifiers)
            {
                throw new NotSupportedException();
            }

            #endregion
        }



        public ICliMetadataTypeDefinitionTableRow ResolveScope(ICliMetadataTypeDefOrRefRow scope)
        {
            return ResolveScope(scope, null, true);
        }
        //#region _ICliManager Members


        IDictionary<ICliMetadataTypeDefinitionTableRow, IType> _ICliManager.TypeCache
        {
            get { return this.typeCache.metadataTypeCache; }
        }

        IDictionary<ICliMetadataTypeDefinitionTableRow, TypeKind> _ICliManager.TypeKindCache
        {
            get { return this.typeCache.typeKindCache; }
        }

        IDictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType> _ICliManager.BaseTypeKinds
        {
            get { return this.baseTypeKinds; }
        }

        IDictionary<ICliMetadataTypeDefOrRefRow, BaseKindCacheType> _ICliManager.RefBaseTypeKinds
        {
            get { return this.refBaseTypeKinds; }
        }

        _ICliAssembly _ICliManager.GetRelativeAssembly(ICliMetadataRoot root)
        {
            return GetRelativeAssembly(root);
        }
        //#endregion


        public IType MakeClassificationType(IType elementType, TypeElementClassification classification)
        {
            return this.typeCache.MakeClassificationType(elementType, classification);
        }

        public IArrayType MakeArray(IType elementType)
        {
            return this.MakeArray(elementType, 1);
        }

        public IArrayType MakeArray(IType elementType, int rank)
        {
            var cache = this.typeCache.GetArrayCache(elementType);
            IArrayType result;
            lock (cache)
                result = cache.CreateArray(rank);
            return result;
        }

        public IArrayType MakeArray(IType elementType, int[] lowerBounds = null, uint[] lengths = null)
        {
            var cache = this.typeCache.GetArrayCache(elementType);
            IArrayType result;
            lock (cache)
                result = cache.CreateArray(lowerBounds, lengths);
            return result;
        }

        public IClassType MakeGenericClosure(IClassType source, ITypeCollectionBase closureArguments)
        {
            throw new NotImplementedException();
        }

        public IDelegateType MakeGenericClosure(IDelegateType source, ITypeCollectionBase closureArguments)
        {
            throw new NotImplementedException();
        }

        public IInterfaceType MakeGenericClosure(IInterfaceType source, ITypeCollectionBase closureArguments)
        {
            throw new NotImplementedException();
        }

        public IStructType MakeGenericClosure(IStructType source, ITypeCollectionBase closureArguments)
        {
            throw new NotImplementedException();
        }


        public IModifiedType MakeModifiedType(IType elementType, params TypeModification[] modifications)
        {
            var modifiedCache = this.typeCache.GetModifiedTypeCache(elementType);
            IModifiedType result;
            lock (modifiedCache)
            {
                TypeModifierSetEntry entry = new TypeModifierSetEntry(modifications);
                if (!modifiedCache.TryObtainConstruct(entry, out result))
                    modifiedCache.RegisterConstruct(entry, result = new ModifiedType(elementType, modifications));
            }
            return result;
        }

        public RuntimeCoreType ObtainCoreType(IType type)
        {
            if (!(type is ICliType))
                return RuntimeCoreType.None;
            var cliType = (ICliType) type;
            var metadata = cliType.MetadataEntry;
            if (this.RuntimeEnvironment.UseCoreLibrary)
            {
                var assembly = cliType.Assembly;
                if (assembly.UniqueIdentifier == this.RuntimeEnvironment.CoreLibraryIdentifier)
                    return DiscernCoreTypeKind(metadata.Namespace, metadata.Name);
                return RuntimeCoreType.None;
            }
            else
                return DiscernCoreTypeKind(metadata.Namespace, metadata.Name);
        }

        private static RuntimeCoreType DiscernCoreTypeKind(string @namespace, string name)
        {
            if (@namespace == "System")
            {
                switch (name)
                {
                    case "SByte":
                        return RuntimeCoreType.SByte;
                    case "Int16":
                        return RuntimeCoreType.Int16;
                    case "Int32":
                        return RuntimeCoreType.Int32;
                    case "Int64":
                        return RuntimeCoreType.Int64;
                    case "Byte":
                        return RuntimeCoreType.Byte;
                    case "UInt16":
                        return RuntimeCoreType.UInt16;
                    case "UInt32":
                        return RuntimeCoreType.UInt32;
                    case "UInt64":
                        return RuntimeCoreType.UInt64;
                    case "Single":
                        return RuntimeCoreType.Single;
                    case "Double":
                        return RuntimeCoreType.Double;
                    case "Type":
                        return RuntimeCoreType.Type;
                    case "Decimal":
                        return RuntimeCoreType.Decimal;
                    case "Boolean":
                        return RuntimeCoreType.Boolean;
                    case "Array":
                        return RuntimeCoreType.Array;
                    case "Enum":
                        return RuntimeCoreType.RootEnum;
                    case "ValueType":
                        return RuntimeCoreType.RootStruct;
                    case "Object":
                        return RuntimeCoreType.RootType;
                    case "String":
                        return RuntimeCoreType.String;
                    case "Void":
                        return RuntimeCoreType.VoidType;
                    case "Char":
                        return RuntimeCoreType.Char;
                }
            }
            return RuntimeCoreType.None;
        }

        public IType GetMetadatum(MetadatumKind kind)
        {
            switch (kind)
            {
                case MetadatumKind.ParameterArray:
                    return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "ParamArrayAttribute", 0));
                case MetadatumKind.CompilerGenerated:
                    return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System.Runtime.CompilerServices", "CompilerGeneratedAttribute", 0));
                case MetadatumKind.Obsolete:
                    return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "ObsoleteAttribute", 0));
                default:
                    throw new ArgumentOutOfRangeException("kind");
            }
        }
        public ITypeIdentityMetadataService MetadatumHandler
        {
            get
            {
                return this.metadataService ?? (this.metadataService = new MetadataService(this));
            }
        }
    }
}
