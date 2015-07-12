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
        private object assemblyIdSyncLock = new object();
        private object loadedAssemblyLock = new object();
        //private MultikeyedDictionary<string, CliFrameworkVersion, IAssemblyUniqueIdentifier> fileIdentifiers = new MultikeyedDictionary<string, CliFrameworkVersion, IAssemblyUniqueIdentifier>();
        private Dictionary<string, IAssemblyUniqueIdentifier> fileIdentifiers = new Dictionary<string, IAssemblyUniqueIdentifier>();
        private Dictionary<IAssemblyUniqueIdentifier, CliAssembly> loadedAssemblies = new Dictionary<IAssemblyUniqueIdentifier, CliAssembly>();
        private Dictionary<IAssemblyUniqueIdentifier, object> assemblyIdSyncs = new Dictionary<IAssemblyUniqueIdentifier, object>();
        private Dictionary<string, Tuple<PEImage, CliMetadataFixedRoot, string>> loadedModules = new Dictionary<string, Tuple<PEImage, CliMetadataFixedRoot, string>>();
        private MultikeyedDictionary<CliAssembly, string, CliModule> moduleAssemblyIdentities = new MultikeyedDictionary<CliAssembly, string, CliModule>();
        private Dictionary<Type, IType> systemTypeCache = new Dictionary<Type, IType>();
        private IDictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType> baseTypeKinds = new Dictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType>();
        private IDictionary<ICliMetadataTypeDefOrRefRow, BaseKindCacheType> refBaseTypeKinds = new Dictionary<ICliMetadataTypeDefOrRefRow, BaseKindCacheType>();
        private object cacheLock = new object();
        private object cacheClearLock = new object();
        //"System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        /// <summary>
        /// Data member for supported services beyond the default services.
        /// </summary>
        private Dictionary<Guid, IIdentityService> supportedServices;
        /// <summary>
        /// Data member used to cache services of various types.
        /// </summary>
        private MultikeyedDictionary<Guid, Type, IIdentityService> cachedServices;
        /// <summary>
        /// Data member for the runtime environment.
        /// </summary>
        private ICliRuntimeEnvironmentInfo runtimeEnvironment;
        /// <summary>
        /// Creates a new <see cref="CliManager"/> with the 
        /// <paramref name="runtimeEnvironment"/> specified.
        /// </summary>
        /// <param name="runtimeEnvironment">The <see cref="ICliRuntimeEnvironmentInfo"/>
        /// which the <see cref="CliManager"/> targets.</param>
        public CliManager(ICliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            this.cachedServices = new MultikeyedDictionary<Guid, Type, IIdentityService>();
            this.supportedServices = new Dictionary<Guid, IIdentityService>();
            this.typeCache = new TypeCache(this);
            this.runtimeEnvironment = runtimeEnvironment;
            this.RegisterService<IIdentityMetadataService>(IdentityServiceGuids.MetadatumService, new MetadataService(this));
            this.RegisterService<ITypeNameBuilderService>(IdentityServiceGuids.TypeNameBuilderService, new TypeNameService(this));
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
                        IGenericType lookupPoint = (IGenericType)this.ObtainTypeReference(t.DeclaringType);
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
                if (typeParameters != null && result.IsGenericConstruct && result is IGenericType && (gType = ((IGenericType)(result))).IsGenericDefinition)
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
            CliMetadataTableKinds typeTable = (CliMetadataTableKinds)((ulong)1 << (int)((typeHandle & 0xFF000000U) >> 24));
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
#if DEBUG && TRACE_CACHE
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
                switch ((PrimitiveType)typeIdentity)
                {
                    case PrimitiveType.Boolean:
                        return ObtainTypeReference(RuntimeCoreType.Boolean);
                    case PrimitiveType.Byte:
                        return ObtainTypeReference(RuntimeCoreType.Byte);
                    case PrimitiveType.SByte:
                        return ObtainTypeReference(RuntimeCoreType.SByte);
                    case PrimitiveType.Int16:
                        return ObtainTypeReference(RuntimeCoreType.Int16);
                    case PrimitiveType.UInt16:
                        return ObtainTypeReference(RuntimeCoreType.UInt16);
                    case PrimitiveType.Int32:
                        return ObtainTypeReference(RuntimeCoreType.Int32);
                    case PrimitiveType.UInt32:
                        return ObtainTypeReference(RuntimeCoreType.UInt32);
                    case PrimitiveType.Int64:
                        return ObtainTypeReference(RuntimeCoreType.Int64);
                    case PrimitiveType.UInt64:
                        return ObtainTypeReference(RuntimeCoreType.UInt64);
                    case PrimitiveType.Decimal:
                        return ObtainTypeReference(RuntimeCoreType.Decimal);
                    case PrimitiveType.Float:
                        return ObtainTypeReference(RuntimeCoreType.Single);
                    case PrimitiveType.Double:
                        return ObtainTypeReference(RuntimeCoreType.Double);
                    case PrimitiveType.Char:
                        return ObtainTypeReference(RuntimeCoreType.Char);
                    case PrimitiveType.String:
                        return ObtainTypeReference(RuntimeCoreType.String);
                }
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
            lock (this.assemblyIdSyncLock)
            {
                foreach (var assembly in this.loadedAssemblies.Values)
                    lock (this.assemblyIdSyncs[assembly.UniqueIdentifier])
                        assembly.Dispose();
                lock (loadedAssemblyLock)
                    this.loadedAssemblies.Clear();
                this.assemblyIdSyncs.Clear();
                this.fileIdentifiers.Clear();
            }
            this.OnDisposed();
        }

        protected virtual void OnDisposed()
        {
            lock (this.assemblyIdSyncLock)
            {
                var disposed = this.Disposed;
                if (disposed != null)
                    disposed(this, EventArgs.Empty);
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

        public event EventHandler Disposed;


        public IType ObtainTypeReference(CliRuntimeCoreType coreType, IAssembly relativeSource)
        {
            return this.ObtainTypeReference(this.RuntimeEnvironment.GetCoreIdentifier(coreType, relativeSource), relativeSource);
        }

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
                Tuple<PEImage, CliMetadataFixedRoot, string> valid = null;

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
                lock (this.loadedModules)
                    this.loadedModules.Add(valid.Item3, valid);
                return valid.Item2.TableStream.ModuleTable[1];

            }
            throw new NotImplementedException();
        }



        internal CliAssembly GetRelativeAssembly(ICliMetadataRoot root)
        {
            if (root != null)
            {
                CliAssembly[] loadedAssemblies;
                lock (loadedAssemblyLock)
                    loadedAssemblies = this.loadedAssemblies.Values.ToArray();
                foreach (var assembly in loadedAssemblies)
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
            return this.ObtainTypeReference(typeIdentity, null, null);
        }

        public IType ObtainTypeReference(ICliMetadataTypeDefOrRefRow typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            switch (typeIdentity.TypeDefOrRefEncoding)
            {
                case CliMetadataTypeDefOrRefTag.TypeDefinition:
                    if (typeIdentity is ICliMetadataTypeDefinitionTableRow)
                        return this.ObtainTypeReference((ICliMetadataTypeDefinitionTableRow)typeIdentity);
                    throw new ArgumentException(string.Format("Type identity not of required type '{0}'.", typeof(ICliMetadataTypeDefinitionTableRow)), "typeIdentity");
                case CliMetadataTypeDefOrRefTag.TypeReference:
                    if (typeIdentity is ICliMetadataTypeRefTableRow)
                        return this.ObtainTypeReference((ICliMetadataTypeRefTableRow)typeIdentity);
                    throw new ArgumentException(string.Format("Type identity not of required type '{0}'.", typeof(ICliMetadataTypeRefTableRow)), "typeIdentity");
                    throw new NotSupportedException();
                case CliMetadataTypeDefOrRefTag.TypeSpecification:
                    if (typeIdentity is ICliMetadataTypeSpecificationTableRow)
                        return this.ObtainTypeReference((ICliMetadataTypeSpecificationTableRow)typeIdentity, activeType, activeMethod, activeAssembly);
                    throw new ArgumentException(string.Format("Type identity not of required type '{0}'.", typeof(ICliMetadataTypeSpecificationTableRow)), "typeIdentity");
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
                                CliAssembly identitySource = new CliAssembly(this, identifier.Item3, identifier.Item2, identifier.Item1);
                                this.loadedAssemblies.Add(identitySource.UniqueIdentifier, identitySource);
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
                                var loadedModule = this.LoadModule((ICliMetadataModuleReferenceTableRow)typeIdentity.Source);
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
                        var declaringTypeRef = this.ObtainTypeReference((ICliMetadataTypeRefTableRow)typeIdentity.Source);
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
                if (relativeSource is ICliAssembly)
                {
                    var cliAssem = (ICliAssembly)relativeSource;
                    if (cliAssem.RuntimeEnvironment.Version != runtimeEnvironment.Version ||
                        cliAssem.RuntimeEnvironment.Platform != runtimeEnvironment.Platform)
                        runtimeEnvironment = cliAssem.RuntimeEnvironment;
                }
            object syncLock;
            lock (assemblyIdSyncLock)
            {
                if (!assemblyIdSyncs.TryGetValue(assemblyIdentity, out syncLock))
                    assemblyIdSyncs.Add(assemblyIdentity, syncLock = new { LockFor = assemblyIdentity.ToString() });
            }

            lock (syncLock)
            {
                bool contains;
                lock (loadedAssemblyLock)
                    contains = this.loadedAssemblies.TryGetValue(assemblyIdentity, out result);
                if (!contains)
                {
                    var baseName = assemblyIdentity.Name;
                    Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataFixedRoot, ICliMetadataAssemblyTableRow, string> validResult = null;
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
                        lock (loadedAssemblyLock)
                        {
                            result = new CliAssembly(this, validResult.Item5, validResult.Item1, validResult.Item2);
                            this.loadedAssemblies.Add(result.UniqueIdentifier, result);
                            //result.InitializeCommon();
                            this.fileIdentifiers.Add(validResult.Item6, result.UniqueIdentifier);
                            this.OnAssemblyLoaded(new CliAssemblyLoadedEventArgs() { AssemblyIdentifier = result.UniqueIdentifier, AssemblyLocation = validResult.Item6, LoadedAssembly = result });
                        }
                        if (gacAssembly)
                            result.IsFromGlobalAssemblyCache = true;
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
                        supportedOnPlatform = true;
                        /*
                        CliHeader header = metadataRoot.Header;
                        if (imageKind == PEImageKind.x64Image &&
                            ((header.Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly))
                            supportedOnPlatform = true;
                        else if (imageKind == PEImageKind.x86Image &&
                         // ((header.Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly) &&
                            (header.Flags & CliRuntimeFlags.Requires32BitProcess) != CliRuntimeFlags.Requires32BitProcess)
                            supportedOnPlatform = true;
                        //*/
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
                            object syncLock;
                            lock (assemblyIdSyncLock)
                            {
                                if (!assemblyIdSyncs.TryGetValue(assemblyUniqueIdentifier, out syncLock))
                                    assemblyIdSyncs.Add(assemblyUniqueIdentifier, syncLock = new { LockFor = assemblyUniqueIdentifier.ToString() });
                            }
                            lock (syncLock)
                            {
                                if (this.loadedAssemblies.TryGetValue(assemblyUniqueIdentifier, out result))
                                {
                                    this.fileIdentifiers.Add(filename, result.UniqueIdentifier);
                                    peImage.Dispose();
                                    metadataRoot.Dispose();
                                    return this.loadedAssemblies[assemblyUniqueIdentifier];
                                }
                            }
                            result = new CliAssembly(this, firstAssemblyRow, assemblyUniqueIdentifier, publicKeyInfo);
                            loadedAssemblies.Add(result.UniqueIdentifier, result);
                            //result.InitializeCommon();
                            this.fileIdentifiers.Add(filename, result.UniqueIdentifier);
                            this.OnAssemblyLoaded(new CliAssemblyLoadedEventArgs() { AssemblyIdentifier = assemblyUniqueIdentifier, AssemblyLocation = filename, LoadedAssembly = result });
                            return result;
                        }
                    }
                    else
                        throw new BadImageFormatException(string.Format("Expecting {0} image, but got {1}.", this.runtimeEnvironment.Platform, imageKind));
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
                this.loadedAssemblies.Add(result.UniqueIdentifier, result);
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
                Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataFixedRoot, ICliMetadataAssemblyTableRow, string> validResult = null;
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
                    result = new CliAssembly(this, validResult.Item5, validResult.Item1, validResult.Item2);
                    this.loadedAssemblies.Add(result.UniqueIdentifier, result);
                    this.fileIdentifiers.Add(validResult.Item6, result.UniqueIdentifier);
                    this.OnAssemblyLoaded(new CliAssemblyLoadedEventArgs() { AssemblyIdentifier = result.UniqueIdentifier, AssemblyLocation = validResult.Item6, LoadedAssembly = result });
                }
                else
                    return this.ObtainAssemblyReference(identity.Item2, this.GetRelativeAssembly(assemblyIdentity.MetadataRoot));
                return this.loadedAssemblies[identity.Item2];
            }
            return result;
        }

        //#endregion

        #region ITypeIdentityManager<ICliMetadataTypeSignature> Members

        public IType ObtainTypeReference(ICliMetadataParamSignature signature, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            return this.ObtainTypeReference(signature, signature.ParameterType, activeType, activeMethod, activeAssembly: activeAssembly);
        }

        public IType ObtainTypeReference(ICliMetadataTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            switch (typeIdentity.TypeSignatureKind)
            {
                case CliMetadataTypeSignatureKind.ArrayType:
                    return ObtainTypeReference((ICliMetadataArrayTypeSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                case CliMetadataTypeSignatureKind.ElementType:
                    return ObtainTypeReference((ICliMetadataElementTypeSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                case CliMetadataTypeSignatureKind.ElementTypeAndModifiers:
                    return ObtainTypeReference((ICliMetadataElementTypeAndModifiersSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                case CliMetadataTypeSignatureKind.GenericParameter:
                    return ObtainTypeReference((ICliMetadataGenericParameterTypeSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                case CliMetadataTypeSignatureKind.FunctionPointerType:
                    return ObtainTypeReference((ICliMetadataFunctionPointerTypeSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                case CliMetadataTypeSignatureKind.NativeType:
                    return ObtainTypeReference((ICliMetadataNativeTypeSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                case CliMetadataTypeSignatureKind.ReturnType:
                    return ObtainTypeReference((ICliMetadataReturnTypeSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                case CliMetadataTypeSignatureKind.ValueOrClassType:
                    return ObtainTypeReference((ICliMetadataValueOrClassTypeSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                case CliMetadataTypeSignatureKind.GenericInstType:
                    return ObtainTypeReference((ICliMetadataGenericInstanceTypeSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                case CliMetadataTypeSignatureKind.VectorArrayType:
                    return ObtainTypeReference((ICliMetadataVectorArrayTypeSignature)typeIdentity, activeType, activeMethod, activeAssembly);
                default:
                    throw new NotSupportedException();
            }
        }

        private IType ObtainTypeReference(ICliMetadataArrayTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            var elementType = this.ObtainTypeReference(typeIdentity.ElementType, activeType, activeMethod, activeAssembly);
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

        private IType ObtainTypeReference(ICliMetadataElementTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            switch (typeIdentity.Classification)
            {
                case TypeElementClassification.Nullable:
                    return ObtainTypeReference(typeIdentity.ElementType, activeType, activeMethod, activeAssembly).MakeNullable();
                case TypeElementClassification.Pointer:
                    return ObtainTypeReference(typeIdentity.ElementType, activeType, activeMethod, activeAssembly).MakePointer();
                case TypeElementClassification.Reference:
                    return ObtainTypeReference(typeIdentity.ElementType, activeType, activeMethod, activeAssembly).MakeByReference();
                default:
                    throw new NotSupportedException();
            }
        }

        private IType ObtainTypeReference(ICliMetadataElementTypeAndModifiersSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            return this.ObtainTypeReference(typeIdentity, typeIdentity.ElementType, activeType, activeMethod,
                target =>
                {
                    switch (typeIdentity.Classification)
                    {
                        case TypeElementClassification.Nullable:
                            return target.MakeNullable();
                        case TypeElementClassification.Pointer:
                            return target.MakePointer();
                        case TypeElementClassification.Reference:
                            return target.MakeByReference();
                        default:
                            throw new NotSupportedException();
                    }
                }, activeAssembly);
        }

        private IType ObtainTypeReference(ICliMetadataGenericParameterTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {

            switch (typeIdentity.Parent)
            {
                case CliMetadataGenericParameterParent.Type:
                    if (activeType != null && activeType is IGenericType)
                        return ((IGenericType)(activeType)).GenericParameters[(int)typeIdentity.Position];
                    throw new ArgumentException("activeType");
                case CliMetadataGenericParameterParent.Method:
                    if (activeMethod == null)
                        throw new ArgumentNullException("activeMethod");
                    return activeMethod.GenericParameters[(int)typeIdentity.Position];
                default:
                    throw new ArgumentException("typeIdentity references a generic type of an unknown kind.", "typeIdentity");
            }
        }

        private IType ObtainTypeReference(ICliMetadataFunctionPointerTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            throw new NotImplementedException();
        }

        private IType ObtainTypeReference(ICliMetadataNativeTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            RuntimeCoreType coreType = RuntimeCoreType.None;
            var assembly = activeAssembly as ICliAssembly;
            ICliRuntimeEnvironmentInfo runtimeEnvironment = assembly == null ? this.RuntimeEnvironment : assembly.RuntimeEnvironment;
            switch (typeIdentity.TypeKind)
            {
                case CliMetadataNativeTypes.Void:
                    coreType = RuntimeCoreType.VoidType;
                    break;
                case CliMetadataNativeTypes.Boolean:
                    coreType = RuntimeCoreType.Boolean;
                    break;
                case CliMetadataNativeTypes.Char:
                    coreType = RuntimeCoreType.Char;
                    break;
                case CliMetadataNativeTypes.SByte:
                    coreType = RuntimeCoreType.SByte;
                    break;
                case CliMetadataNativeTypes.Byte:
                    coreType = RuntimeCoreType.Byte;
                    break;
                case CliMetadataNativeTypes.Int16:
                    coreType = RuntimeCoreType.Int16;
                    break;
                case CliMetadataNativeTypes.UInt16:
                    coreType = RuntimeCoreType.UInt16;
                    break;
                case CliMetadataNativeTypes.Int32:
                    coreType = RuntimeCoreType.Int32;
                    break;
                case CliMetadataNativeTypes.UInt32:
                    coreType = RuntimeCoreType.UInt32;
                    break;
                case CliMetadataNativeTypes.Int64:
                    coreType = RuntimeCoreType.Int64;
                    break;
                case CliMetadataNativeTypes.UInt64:
                    coreType = RuntimeCoreType.UInt64;
                    break;
                case CliMetadataNativeTypes.Single:
                    coreType = RuntimeCoreType.Single;
                    break;
                case CliMetadataNativeTypes.Double:
                    coreType = RuntimeCoreType.Double;
                    break;
                case CliMetadataNativeTypes.String:
                    coreType = RuntimeCoreType.String;
                    break;
                case CliMetadataNativeTypes.TypedByReference:
                    if (runtimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(runtimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "TypedReference"), assembly);
                    else
                        return this.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "TypedReference"), assembly);
                case CliMetadataNativeTypes.NativeInteger:
                    if (runtimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(runtimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "IntPtr"), assembly);
                    else
                        return this.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IntPtr"), assembly);
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                    if (runtimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(runtimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "UIntPtr"), assembly);
                    else
                        return this.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "UIntPtr"), assembly);
                case CliMetadataNativeTypes.Object:
                    coreType = RuntimeCoreType.RootType;
                    break;
                case CliMetadataNativeTypes.Type:
                    coreType = RuntimeCoreType.Type;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return this.ObtainTypeReference(coreType, assembly);
        }

        private IType ObtainTypeReference(ICliMetadataReturnTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            return ObtainTypeReference(typeIdentity, typeIdentity.ReturnType, activeType, activeMethod, activeAssembly: activeAssembly);
        }

        internal IType ObtainTypeReference(ICliMetadataCustomModifierTypeSignature customModifierHolder, ICliMetadataTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, Func<IType, IType> preModifier = null, IAssembly activeAssembly = null)
        {
            if (preModifier == null)
                if (customModifierHolder.CustomModifiers.Count == 0)
                    return this.ObtainTypeReference(typeIdentity, activeType, activeMethod, activeAssembly);
                else
                    return this.ObtainTypeReference(typeIdentity, activeType, activeMethod, activeAssembly).MakeModified((from m in customModifierHolder.CustomModifiers
                                                                                                                          select new TypeModification(() => this.ObtainTypeReference(m.ModifierType, activeType, activeMethod, activeAssembly), m.Required)).ToArray());
            else
                if (customModifierHolder.CustomModifiers.Count == 0)
                    return preModifier(this.ObtainTypeReference(typeIdentity, activeType, activeMethod, activeAssembly));
                else
                    return preModifier(this.ObtainTypeReference(typeIdentity, activeType, activeMethod, activeAssembly)).MakeModified((from m in customModifierHolder.CustomModifiers
                                                                                                                                       select new TypeModification(() => this.ObtainTypeReference(m.ModifierType, activeType, activeMethod, activeAssembly), m.Required)).ToArray());
        }

        private IType ObtainTypeReference(ICliMetadataValueOrClassTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            return this.ObtainTypeReference(typeIdentity.Target, activeType, activeMethod, activeAssembly);
        }

        private IType ObtainTypeReference(ICliMetadataGenericInstanceTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            var type = this.ObtainTypeReference(typeIdentity.Target, activeType, activeMethod, activeAssembly);

            if (type is IGenericType)
            {
                return ((IGenericType)type).MakeGenericClosure((from t in typeIdentity.GenericParameters
                                                                select this.ObtainTypeReference(t, activeType, activeMethod, activeAssembly)).ToArray());
            }
            else if (type is IEnumType && type.Parent is IGenericType &&
                ((IGenericType)type.Parent).GenericParameters.Count == typeIdentity.GenericParameters.Count)
            {
                var parentType = (IGenericType)type.Parent;
                var parent = parentType.MakeGenericClosure((from t in typeIdentity.GenericParameters
                                                            select this.ObtainTypeReference(t, activeType, activeMethod, activeAssembly)).ToArray());
                if (parent is ITypeParent)
                {
                    var typeParent = (ITypeParent)parent;
                    return typeParent.Types[type.UniqueIdentifier].Entry;
                }
            }
            throw new InvalidOperationException();
        }

        private IType ObtainTypeReference(ICliMetadataVectorArrayTypeSignature typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            return this.ObtainTypeReference(typeIdentity.ElementType, activeType, activeMethod, activeAssembly).MakeArray();
        }

        #endregion

        //#region ITypeIdentityManager Members

        IStandardRuntimeEnvironmentInfo ITypeIdentityManager.RuntimeEnvironment
        {
            get { return this.runtimeEnvironment; }
        }

        //#endregion

        public virtual ICliMetadataTypeDefinitionTableRow ResolveScope(ICliMetadataTypeDefOrRefRow scope)
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
            var cliType = (ICliType)type;
            var metadata = cliType.MetadataEntry;
            if (this.RuntimeEnvironment.UseCoreLibrary)
            {
                var assembly = cliType.Assembly;
                if (assembly.UniqueIdentifier.Equals(this.RuntimeEnvironment.CoreLibraryIdentifier))
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
        public IIdentityMetadataService MetadatumHandler
        {
            get
            {
                return this.GetService<IIdentityMetadataService>(IdentityServiceGuids.MetadatumService);
            }
        }


        public IEnumerable<IType> GetCoreTypeInterfaces(RuntimeCoreType coreType, IAssembly relativeAssembly = null)
        {

            ICliRuntimeEnvironmentInfo runtimeInfo = this.RuntimeEnvironment;
            if (relativeAssembly is ICliAssembly)
            {
                var cliAssembly = (ICliAssembly)relativeAssembly;
                runtimeInfo = cliAssembly.RuntimeEnvironment;
            }
            switch (coreType)
            {
                case RuntimeCoreType.Array:
                    if (runtimeInfo.UseCoreLibrary)
                    {
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "ICloneable", 0), relativeAssembly);
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Collections", "IList", 0), relativeAssembly);
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Collections", "ICollection", 0), relativeAssembly);
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Collections", "IEnumerable", 0), relativeAssembly);
                        switch (runtimeInfo.Version & CliFrameworkVersion.VersionMask)
                        {
                            case CliFrameworkVersion.v4_0_30319:
                            case CliFrameworkVersion.v4_5:
                                yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Collections", "IStructuralComparable", 0), relativeAssembly);
                                yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Collections", "IStructuralEquatable", 0), relativeAssembly);
                                break;
                        }
                    }
                    else
                    {
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "ICloneable", 0), relativeAssembly);
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections", "IList", 0), relativeAssembly);
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections", "ICollection", 0), relativeAssembly);
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections", "IEnumerable", 0), relativeAssembly);
                        switch (runtimeInfo.Version & CliFrameworkVersion.VersionMask)
                        {
                            case CliFrameworkVersion.v4_0_30319:
                            case CliFrameworkVersion.v4_5:
                                yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections", "IStructuralComparable", 0), relativeAssembly);
                                yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections", "IStructuralEquatable", 0), relativeAssembly);
                                break;
                        }
                    }
                    break;
                case RuntimeCoreType.Boolean:
                case RuntimeCoreType.Char:
                    if (runtimeInfo.UseCoreLibrary)
                    {
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "IComparable", 0), relativeAssembly);
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "IConvertible", 0), relativeAssembly);
                        switch (runtimeInfo.Version & CliFrameworkVersion.VersionMask)
                        {
                            case CliFrameworkVersion.v2_0_50727:
                            case CliFrameworkVersion.v3_0:
                            case CliFrameworkVersion.v3_5:
                            case CliFrameworkVersion.v4_0_30319:
                            case CliFrameworkVersion.v4_5:
                                yield return ((IGenericType)ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "IComparable", 1), relativeAssembly)).MakeGenericClosure(ObtainTypeReference(coreType, relativeAssembly));
                                yield return ((IGenericType)ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "IEquatable", 1), relativeAssembly)).MakeGenericClosure(ObtainTypeReference(coreType, relativeAssembly));
                                break;
                        }
                    }
                    else
                    {
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IComparable", 0), relativeAssembly);
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IConvertible", 0), relativeAssembly);
                        switch (runtimeInfo.Version & CliFrameworkVersion.VersionMask)
                        {
                            case CliFrameworkVersion.v2_0_50727:
                            case CliFrameworkVersion.v3_0:
                            case CliFrameworkVersion.v3_5:
                            case CliFrameworkVersion.v4_0_30319:
                            case CliFrameworkVersion.v4_5:
                                yield return ((IGenericType)ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IComparable", 1), relativeAssembly)).MakeGenericClosure(ObtainTypeReference(coreType, relativeAssembly));
                                yield return ((IGenericType)ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IEquatable", 1), relativeAssembly)).MakeGenericClosure(ObtainTypeReference(coreType, relativeAssembly));
                                break;
                        }
                    }
                    break;
                case RuntimeCoreType.Decimal:
                    if ((runtimeInfo.Version & (CliFrameworkVersion.v4_0_30319 | CliFrameworkVersion.v4_5)) != (CliFrameworkVersion)0)
                        if (runtimeInfo.UseCoreLibrary)
                            yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Runtime.Serialization", "IDeserializationCallback", 0), relativeAssembly);
                        else
                            yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Runtime.Serialization", "IDeserializationCallback", 0), relativeAssembly);

                    goto case RuntimeCoreType.Single;
                case RuntimeCoreType.Double:
                case RuntimeCoreType.Single:
                case RuntimeCoreType.SByte:
                case RuntimeCoreType.Byte:
                case RuntimeCoreType.Int16:
                case RuntimeCoreType.UInt16:
                case RuntimeCoreType.Int32:
                case RuntimeCoreType.UInt32:
                case RuntimeCoreType.Int64:
                case RuntimeCoreType.UInt64:
                    if (runtimeInfo.UseCoreLibrary)
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "IFormattable", 0), relativeAssembly);
                    else
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IFormattable", 0), relativeAssembly);
                    goto case RuntimeCoreType.Boolean;
                case RuntimeCoreType.RootEnum:
                    if (runtimeInfo.UseCoreLibrary)
                    {
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "IFormattable", 0), relativeAssembly);
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "IComparable", 0), relativeAssembly);
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "IConvertible", 0), relativeAssembly);
                    }
                    else
                    {
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IFormattable", 0), relativeAssembly);
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IComparable", 0), relativeAssembly);
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IConvertible", 0), relativeAssembly);
                    }
                    break;
                case RuntimeCoreType.String:

                    if (runtimeInfo.UseCoreLibrary)
                    {
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Collections", "IEnumerable", 0), relativeAssembly);
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System", "ICloneable", 0), relativeAssembly);
                        switch (runtimeInfo.Version & CliFrameworkVersion.VersionMask)
                        {
                            case CliFrameworkVersion.v2_0_50727:
                            case CliFrameworkVersion.v3_0:
                            case CliFrameworkVersion.v3_5:
                            case CliFrameworkVersion.v4_0_30319:
                            case CliFrameworkVersion.v4_5:
                                yield return ((IGenericType)ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Collections.Generic", "IEnumerable", 1), relativeAssembly)).MakeGenericClosure(ObtainTypeReference(RuntimeCoreType.Char, relativeAssembly));
                                break;
                        }
                    }
                    else
                    {
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections", "IEnumerable", 0), relativeAssembly);
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "ICloneable", 0), relativeAssembly);
                        switch (runtimeInfo.Version & CliFrameworkVersion.VersionMask)
                        {
                            case CliFrameworkVersion.v2_0_50727:
                            case CliFrameworkVersion.v3_0:
                            case CliFrameworkVersion.v3_5:
                            case CliFrameworkVersion.v4_0_30319:
                            case CliFrameworkVersion.v4_5:
                                yield return ((IGenericType)ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections.Generic", "IEnumerable", 1), relativeAssembly)).MakeGenericClosure(ObtainTypeReference(RuntimeCoreType.Char, relativeAssembly));
                                break;
                        }
                    }
                    goto case RuntimeCoreType.Char;
                case RuntimeCoreType.Type:
                    if (runtimeInfo.UseCoreLibrary)
                    {
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Runtime.InteropServices", "_Type", 0), relativeAssembly);
                        yield return ObtainTypeReference(runtimeInfo.CoreLibraryIdentifier.GetTypeIdentifier("System.Reflection", "IReflect", 0), relativeAssembly);
                    }
                    else
                    {
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Runtime.InteropServices", "_Type", 0), relativeAssembly);
                        yield return ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Reflection", "IReflect", 0), relativeAssembly);
                    }
                    break;
            }
        }

        private bool UserServiceIs<TService>(Guid service)
            where TService :
                IIdentityService
        {
            return supportedServices[service] is TService;
        }

        private bool SupportsUserService(Guid service)
        {
            IIdentityService serviceTemp;
            if (supportedServices.TryGetValue(service, out serviceTemp))
                return serviceTemp != null;
            return false;
        }

        #region IServiceProvider<ITypeIdentityService> Members

        public bool SupportsService(Guid service)
        {
            IIdentityService serviceTemp;
            if (supportedServices.TryGetValue(service, out serviceTemp))
                return serviceTemp != null;
            return false;
        }

        /// <summary>
        /// Registers a <paramref name="service"/> by the 
        /// <paramref name="serviceGuid"/> provided.
        /// </summary>
        /// <typeparam name="TService">The type of <see cref="IIdentityService"/>
        /// to register.</typeparam>
        /// <param name="serviceGuid">The <see cref="Guid"/> of the service to register
        /// to aid in looking it up.</param>
        /// <param name="service">The <typeparamref name="TService"/> to actually 
        /// register.</param>
        protected void RegisterService<TService>(Guid serviceGuid, TService service)
            where TService :
                class,                
                IIdentityService
        {
            if (service == null)
                throw new ArgumentNullException(ThrowHelper.GetArgumentName(ArgumentWithException.service));
            IIdentityService previousService = null;
            this.supportedServices.TryGetValue(serviceGuid, out previousService);

            this.supportedServices[serviceGuid] = service;
            if (previousService != null)
            {
                lock (cachedServices)
                {
                    var deadCacheEntries = (from entry in this.cachedServices
                                            where entry.Keys.Key1 == serviceGuid &&
                                                  !entry.Keys.Key2.IsAssignableFrom(service.GetType())
                                            select entry.Keys).ToArray();
                    var replaceableCacheEntries = (from entry in this.cachedServices
                                                   where entry.Keys.Key1 == serviceGuid &&
                                                         entry.Keys.Key2 != typeof(TService) &&
                                                         entry.Keys.Key2.IsAssignableFrom(service.GetType())
                                                   select entry.Keys).ToArray();
                    foreach (var deadEntry in deadCacheEntries)
                        this.cachedServices.Remove(deadEntry.Key1, deadEntry.Key2);
                    foreach (var replaceEntry in replaceableCacheEntries)
                        this.cachedServices[replaceEntry.Key1, replaceEntry.Key2] = service;
                    this.cachedServices[serviceGuid, typeof(TService)] = service;
                }
            }
        }


        public bool ServiceIs<TService>(Guid service)
            where TService :
                IIdentityService
        {
            if (this.SupportsUserService(service))
                return UserServiceIs<TService>(service);
            throw new InvalidOperationException("Service not supported.");
        }

        public TService GetService<TService>(Guid service)
            where TService :
                IIdentityService
        {
            IIdentityService result;
            lock (cachedServices)
            {
                if (this.cachedServices.TryGetValue(service, typeof(TService), out result))
                    return (TService)result;
                else
                    if (this.SupportsUserService(service))
                        if (!UserServiceIs<TService>(service))
                            throw new InvalidOperationException(string.Format("Service of type '{0}' does not support expected type '{1}'.", supportedServices[service].GetType(), typeof(TService)));
                        else
                            result = this.supportedServices[service];
                    else
                        throw new InvalidOperationException("Service not supported.");
                this.cachedServices.TryAdd(service, typeof(TService), result);
            }
            return (TService)result;
        }

        public bool TryGetService<TService>(Guid serviceGuid, out TService service)
            where TService :
                IIdentityService
        {
            IIdentityService result;
            lock (cachedServices)
            {
                if (this.cachedServices.TryGetValue(serviceGuid, typeof(TService), out result))
                {
                    service = (TService)result;
                    return true;
                }
                else if (this.SupportsUserService(serviceGuid))
                    if (!UserServiceIs<TService>(serviceGuid))
                    {
                        service = default(TService);
                        return false;
                    }
                    else
                    {
                        service = (TService)this.supportedServices[serviceGuid];
                        this.cachedServices.TryAdd(serviceGuid, typeof(TService), result = service);
                        return true;
                    }
                else
                {
                    service = default(TService);
                    return false;
                }
            }
        }

        #endregion

        public event EventHandler<CliAssemblyLoadedEventArgs> AssemblyLoaded;

        protected virtual void OnAssemblyLoaded(CliAssemblyLoadedEventArgs args)
        {
            var assemblyLoaded = AssemblyLoaded;
            if (assemblyLoaded != null)
                assemblyLoaded(this, args);
        }
    }
}
