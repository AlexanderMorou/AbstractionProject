using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliManager :
        ICliManager
    {
        private IDictionary<Assembly, ICompiledAssembly> assemblyCache;
        private IDictionary<Type, IType> typeCache = new Dictionary<Type, IType>();
        private object cacheLock = new object();
        private object cacheClearLock = new object();
        #region IIdentityManager<Type> Members

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
                    if (typeCache.TryGetValue(t, out result))
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
                    typeParameters = t.GetGenericArguments().ToCollection(this);
                    t = t.GetGenericTypeDefinition();
                }
                #endregion

                #endregion

                #region Initial type instantiation/retrieval.
                bool cacheLockEntered = false;
                Monitor.Enter(cacheLock, ref cacheLockEntered);
                if (typeCache.ContainsKey(t))
                {
                    result = typeCache[t];
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
                        if (lookupAid is _ICompiledMethodSignatureParent)
                        {
                            _ICompiledMethodSignatureParent parent = (_ICompiledMethodSignatureParent) lookupAid;
                            try
                            {
                                var method = parent.GetSignatureFor(t.DeclaringMethod);
                                try
                                {
                                    result = method.GetGenericParameterFor(t);
                                    positiveMatch = true;
                                    goto positiveExit;
                                }
                                catch (InvalidOperationException)
                                {
                                }
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }
                        if (lookupAid is IMethodParent)
                        {
                            IMethodParent lookupAid2 = (IMethodParent) lookupAid;
                            foreach (ICompiledMethodMember icmm in lookupAid2.Methods.Values)
                                if (icmm.MemberInfo == t.DeclaringMethod)
                                    foreach (ICompiledGenericParameter icgp in icmm.TypeParameters.Values)
                                        if (icgp.UnderlyingSystemType == t)
                                        {
                                            result = icgp;
                                            positiveMatch = true;
                                        }
                        }
                        else if (lookupAid is IMethodSignatureParent)
                        {
                            IMethodSignatureParent lookupAid2 = (IMethodSignatureParent) lookupAid;
                            foreach (ICompiledMethodSignatureMember icmm in lookupAid2.Methods.Values)
                                if (icmm.MemberInfo == t.DeclaringMethod)
                                    foreach (ICompiledGenericParameter icgp in icmm.TypeParameters.Values)
                                        if (icgp.UnderlyingSystemType == t)
                                        {
                                            result = icgp;
                                            positiveMatch = true;
                                        }
                        }
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
                    result = new CompiledEnumType(t, this);
                }
                else if (t.IsSubclassOf(typeof(Delegate)) && t != typeof(MulticastDelegate))
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    result = new CompiledDelegateType(t, this);
                }
                else if (t.IsClass)
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    result = new CompiledClassType(t, this);
                }
                else if (t.IsValueType ||
                         t == typeof(Enum))
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    result = new CompiledStructType(t, this);
                }
                else if (t.IsInterface)
                {
                    if (cacheLockEntered)
                        Monitor.Exit(cacheLock);
                    result = new CompiledInterfaceType(t, this);
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

        #endregion

        #region IAssemblyIdentityManager<Assembly,ICompiledAssembly> Members

        public ICompiledAssembly ObtainAssemblyReference(Assembly assemblyIdentity)
        {
            if (assemblyIdentity == null)
                throw new ArgumentNullException("assembly");
            lock (cacheClearLock)
                lock (cacheLock)
                {
                    if (assemblyCache == null)
                        assemblyCache = new Dictionary<Assembly, ICompiledAssembly>();
                    if (assemblyCache.ContainsKey(assemblyIdentity))
                        return assemblyCache[assemblyIdentity];
                    CompiledAssembly result = new CompiledAssembly(assemblyIdentity, this);
                    assemblyCache.Add(assemblyIdentity, result);
                    return result;
                }
        }

        #endregion

        private void CacheAdd(Type t, IType result)
        {
            lock (cacheLock)
                if (!typeCache.ContainsKey(t))
                {
#if DEBUG
                    if (!t.HasElementType)
                        Debug.WriteLine("Cached {0}.", t);
#endif
                    typeCache.Add(t, result);
                }
        }

        #region ITypeIdentityManager Members

        public IType ObtainTypeReference(TypeSystemSpecialIdentity typeIdentity)
        {
            switch (typeIdentity)
            {
                case TypeSystemSpecialIdentity.ArrayType:
                    return this.ObtainTypeReference(typeof(Array));
                case TypeSystemSpecialIdentity.NullableType:
                    return this.ObtainTypeReference(typeof(Nullable<>));
                case TypeSystemSpecialIdentity.NullableBaseType:
                    return this.ObtainTypeReference(typeof(ValueType));
                case TypeSystemSpecialIdentity.AsynchronousTask:
                    return this.ObtainTypeReference(typeof(Task));
                case TypeSystemSpecialIdentity.AsynchronousTaskOfT:
                    return this.ObtainTypeReference(typeof(Task<>));
                case TypeSystemSpecialIdentity.VoidType:
                    return this.ObtainTypeReference(typeof(void));
                case TypeSystemSpecialIdentity.CompilerGeneratedMetadatum:
                    return this.ObtainTypeReference(typeof(CompilerGeneratedAttribute));
                case TypeSystemSpecialIdentity.RootType:
                    return this.ObtainTypeReference(typeof(object));
                default:
                    throw new ArgumentOutOfRangeException("typeIdentity");
            }
        }

        public IType ObtainTypeReference(object typeIdentity)
        {
            if (typeIdentity == null)
                throw new ArgumentNullException("typeIdentity");
            if (typeIdentity is Type)
                return this.ObtainTypeReference((Type) typeIdentity);
            else if (typeIdentity is IType)
                return (IType)typeIdentity;
            throw new InvalidOperationException();
        }

        public bool IsMetadatumInheritable(IType metadatumType)
        {
            return metadatumType.GetAttributeUsage(this).Inherited;
        }

        public IType ObtainTypeReference(PrimitiveType typeIdentity)
        {
            switch (typeIdentity)
            {
                case PrimitiveType.Boolean:
                    return ObtainTypeReference(typeof(bool));
                case PrimitiveType.Byte:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.SByte:
                    return ObtainTypeReference(typeof(sbyte));
                case PrimitiveType.Int16:
                    return ObtainTypeReference(typeof(short));
                case PrimitiveType.UInt16:
                    return ObtainTypeReference(typeof(ushort));
                case PrimitiveType.Int32:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.UInt32:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.Int64:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.UInt64:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.Decimal:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.Float:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.Double:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.Char:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.String:
                    return ObtainTypeReference(typeof(byte));
                case PrimitiveType.Null:
                    return ObtainTypeReference(typeof(void));
                default:
                    throw new ArgumentOutOfRangeException("typeIdentity");
            }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.ClearCache();
        }

        #endregion

        internal void RemoveFromCache(ICompiledAssembly assembly)
        {
            lock (cacheLock)
                if (assemblyCache.Values.Contains(assembly))
                    assemblyCache.Remove(assemblyCache.First(kvp => kvp.Value == assembly).Key);
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private void ClearCache()
        {

            if (assemblyCache != null)
            {
                ICompiledAssembly[] cacheCopy;
                lock (cacheClearLock)
                    lock (cacheLock)
                    {
                        cacheCopy = assemblyCache.Values.ToArray();
                        assemblyCache.Clear();
                    }
                foreach (var assembly in cacheCopy)
                    assembly.Dispose();
            }
            lock (cacheClearLock)
                lock (cacheLock)
                {
                    Parallel.ForEach(typeCache.Values.ToArray(), p => p.Dispose());
                    typeCache.Clear();
                }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
