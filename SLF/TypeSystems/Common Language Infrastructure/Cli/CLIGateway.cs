using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using System.ComponentModel;
using AllenCopeland.Abstraction.Utilities.Collections;
#if DEBUG
using System.Diagnostics;
#endif
/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Bridges the gap between the common language infrastructure and the
    /// common typing model used by the Abstraction Static Language
    /// Framework.
    /// </summary>
    public static class CLIGateway
    {

        private static readonly HashSet<TypeCode> intrinsicSet = new HashSet<TypeCode> { TypeCode.Boolean, TypeCode.Byte, TypeCode.SByte, TypeCode.Char, TypeCode.UInt16, TypeCode.Int16, TypeCode.UInt32, TypeCode.Int32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal };
        private static readonly HashSet<TypeCode> extrinsicSet = new HashSet<TypeCode> { TypeCode.Empty, TypeCode.Object, TypeCode.DBNull, TypeCode.DateTime, TypeCode.String };
        internal static MultikeyedDictionary<TypeCode, TypeCode, bool> conversionInfo2 = GetConversionInfo2();

        private static MultikeyedDictionary<TypeCode, TypeCode, bool> GetConversionInfo2()
        {
            MultikeyedDictionary<TypeCode, TypeCode, bool> result = new MultikeyedDictionary<TypeCode, TypeCode, bool>();
            TypeCode[] intrinsicSet = CLIGateway.intrinsicSet.ToArray();
            TypeCode[] extrinsicSet = CLIGateway.extrinsicSet.ToArray();
            result[TypeCode.Char, TypeCode.UInt16] = true;
            result[TypeCode.Char, TypeCode.UInt32] = true;
            result[TypeCode.Char, TypeCode.UInt64] = true;
            result[TypeCode.Char, TypeCode.Int32] = true;
            result[TypeCode.Char, TypeCode.Int64] = true;
            result[TypeCode.Char, TypeCode.Single] = true;
            result[TypeCode.Char, TypeCode.Double] = true;
            result[TypeCode.Char, TypeCode.Decimal] = true;

            result[TypeCode.Byte, TypeCode.Char] = true;
            result[TypeCode.Byte, TypeCode.UInt16] = true;
            result[TypeCode.Byte, TypeCode.UInt32] = true;
            result[TypeCode.Byte, TypeCode.UInt64] = true;
            result[TypeCode.Byte, TypeCode.Int16] = true;
            result[TypeCode.Byte, TypeCode.Int32] = true;
            result[TypeCode.Byte, TypeCode.Int64] = true;
            result[TypeCode.Byte, TypeCode.Single] = true;
            result[TypeCode.Byte, TypeCode.Double] = true;
            result[TypeCode.Byte, TypeCode.Decimal] = true;

            result[TypeCode.SByte, TypeCode.Int16] = true;
            result[TypeCode.SByte, TypeCode.Int32] = true;
            result[TypeCode.SByte, TypeCode.Int64] = true;
            result[TypeCode.SByte, TypeCode.Single] = true;
            result[TypeCode.SByte, TypeCode.Double] = true;
            result[TypeCode.SByte, TypeCode.Decimal] = true;

            result[TypeCode.UInt16, TypeCode.UInt32] = true;
            result[TypeCode.UInt16, TypeCode.UInt64] = true;
            result[TypeCode.UInt16, TypeCode.Int32] = true;
            result[TypeCode.UInt16, TypeCode.Int64] = true;
            result[TypeCode.UInt16, TypeCode.Single] = true;
            result[TypeCode.UInt16, TypeCode.Double] = true;
            result[TypeCode.UInt16, TypeCode.Decimal] = true;

            result[TypeCode.Int16, TypeCode.Int32] = true;
            result[TypeCode.Int16, TypeCode.Int64] = true;
            result[TypeCode.Int16, TypeCode.Single] = true;
            result[TypeCode.Int16, TypeCode.Double] = true;
            result[TypeCode.Int16, TypeCode.Decimal] = true;

            result[TypeCode.UInt32, TypeCode.UInt64] = true;
            result[TypeCode.UInt32, TypeCode.Int64] = true;
            result[TypeCode.UInt32, TypeCode.Single] = true;
            result[TypeCode.UInt32, TypeCode.Double] = true;
            result[TypeCode.UInt32, TypeCode.Decimal] = true;

            result[TypeCode.Int32, TypeCode.Int64] = true;
            result[TypeCode.Int32, TypeCode.Single] = true;
            result[TypeCode.Int32, TypeCode.Double] = true;
            result[TypeCode.Int32, TypeCode.Decimal] = true;

            result[TypeCode.UInt64, TypeCode.Single] = true;
            result[TypeCode.UInt64, TypeCode.Double] = true;
            result[TypeCode.UInt64, TypeCode.Decimal] = true;

            result[TypeCode.Int64, TypeCode.Single] = true;
            result[TypeCode.Int64, TypeCode.Double] = true;
            result[TypeCode.Int64, TypeCode.Decimal] = true;

            result[TypeCode.Single, TypeCode.Double] = true;

            for (int i = 0; i < intrinsicSet.Length; i++)
            {
                for (int j = 0; j < intrinsicSet.Length; j++)
                {
                    bool dummy;
                    if (!result.TryGetValue(intrinsicSet[i], intrinsicSet[j], out dummy))
                        result[intrinsicSet[i], intrinsicSet[j]] = false;
                }
            }
            return result;
        }

        static CLIGateway()
        {
//            Console.WriteLine();
        }
        private static IDictionary<Assembly, ICompiledAssembly> assemblyCache;
        /// <summary>
        /// Stores the CLI type->SLF type relationships.
        /// </summary>
        internal static IDictionary<Type, IType> CompiledTypeCache = new Dictionary<Type, IType>();
        private static object cacheLock = new object();
        private static bool cacheFreeze = false;
        private static object cacheClearClock = new object();
        /// <summary>
        /// Clears the cache.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static void ClearCache()
        {

            if (assemblyCache != null)
            {
                ICompiledAssembly[] cacheCopy;
                lock (cacheClearClock)
                    lock (cacheLock)
                    {
                        cacheCopy = assemblyCache.Values.ToArray();
                        assemblyCache.Clear();
                    }
                foreach (var assembly in cacheCopy)
                    assembly.Dispose();
            }
            lock (cacheClearClock)
                lock (cacheLock)
                {
                    Parallel.ForEach(CompiledTypeCache.Values.ToArray(), p => p.Dispose());
                    CompiledTypeCache.Clear();
                }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        /// <summary>
        /// Returns the size of the CLI type cache.
        /// </summary>
        public static int CacheSize
        {
            get
            {
                return CompiledTypeCache.Count;
            }
        }

        /// <summary>
        /// Obtains an enumerator for iterating the elements within the CLI
        /// type cache.
        /// </summary>
        public static IEnumerable<IType> CacheEnumerator
        {
            get
            {
                foreach (IType i in CompiledTypeCache.Values)
                    yield return i;
                yield break;
            }
        }

        internal static bool CacheContains(this Type target)
        {
            return CompiledTypeCache.ContainsKey(target);
        }

        /// <summary>
        /// Obtains a <typeparamref name="TType"/> for a compiled type.
        /// </summary>
        /// <typeparam name="TType">The type of <see cref="IType{T}"/>
        /// to return.</typeparam>
        /// <param name="type">The <see cref="System.Type"/> which needs wrapped within a
        /// <typeparamref name="TType"/>.</param>
        /// <returns>A new <typeparamref name="TType"/> instance which points to the <paramref name="type"/> provided</returns>
        /// <exception cref="System.ArgumentException">thrown when the <paramref name="type"/>
        /// provided did not yield a <see cref="IType"/> instance of the
        /// <typeparamref name="TType"/> type provided.</exception>
        public static TType GetTypeReference<TTypeIdentifier, TType>(this Type type)
            where TTypeIdentifier :
                ITypeUniqueIdentifier
            where TType :
                IType<TTypeIdentifier, TType>
        {
            IType ict = (type.GetTypeReference());
            if (ict == null)
                return default(TType);
            if (!(ict is TType))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.type, ExceptionMessageId.TypeNotGivenKind, type.GetType().ToString(), typeof(TType).ToString());
            return ((TType)ict);
        }

        public static IType GetTypeReference(this Type type, ITypeCollection typeParameters)
        {
            if (!type.IsGenericType)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.type, ExceptionMessageId.TypeNotGeneric);
            else if (!type.IsGenericTypeDefinition)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.type, ExceptionMessageId.TypeAlreadyGenericClosure, type.ToString());
            else
                return ((IGenericType)type.GetTypeReference()).MakeGenericClosure(typeParameters);
        }

        public static IType GetTypeReference(this Type type, params IType[] typeParameters)
        {
            if (!type.IsGenericType)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.type, ExceptionMessageId.TypeNotGeneric);
            else if (!type.IsGenericTypeDefinition)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.type, ExceptionMessageId.TypeAlreadyGenericClosure, type.ToString());
            else
                return ((IGenericType)type.GetTypeReference()).MakeGenericClosure(typeParameters);
        }

        public static TType GetTypeReference<TTypeIdentifier, TType>(this Type type, ITypeCollection typeParameters)
            where TTypeIdentifier :
                IGenericTypeUniqueIdentifier
            where TType :
                IGenericType<TTypeIdentifier, TType>
        {
            if (!type.IsGenericType)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.type, ExceptionMessageId.TypeNotGeneric);
            else if (!type.IsGenericTypeDefinition)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.type, ExceptionMessageId.TypeAlreadyGenericClosure, type.ToString());
            else
                return type.GetTypeReference<TTypeIdentifier, TType>().MakeGenericClosure(typeParameters);
        }

        public static TType GetTypeReference<TTypeIdentifier, TType>(this Type type, params IType[] typeParameters)
            where TTypeIdentifier :
                IGenericTypeUniqueIdentifier
            where TType :
                IGenericType<TTypeIdentifier, TType>
        {
            if (!type.IsGenericType)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.type, ExceptionMessageId.TypeNotGeneric);
            else if (!type.IsGenericTypeDefinition)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.type, ExceptionMessageId.TypeAlreadyGenericClosure, type.ToString());
            else
                return type.GetTypeReference<TTypeIdentifier, TType>().MakeGenericClosure(typeParameters);
        }

        /// <summary>
        /// Obtains a <see cref="IType"/> reference for the associated
        /// <see cref="GenericParameter"/> provided.
        /// </summary>
        /// <param name="type">The <see cref="GenericParameter"/> to obtain a <see cref="IType"/>
        /// for.</param>
        /// <returns>An <see cref="IType"/> instance that wraps the <paramref name="type"/>
        /// provided.</returns>
        public static IType GetTypeReference(this Type type)
        {
            lock (cacheClearClock)
            {
                /* *
                 * Notes: First, remove byref
                 *        Second, remove pointers 
                 *        Third, remove arrays
                 *        Fourth, remove 'nullable' status
                 *        Finally, Breakdown generic-type
                 * */
                /* *
                 * Cache note: The breakdown stage cache points are to
                 *             re-establish the System.Type with the
                 *             staged built IType that results from
                 *             this call.  So each permutation
                 *             is available in cache for later use.
                 * *
                 *             This reduces further call overhead with
                 *             a light memory cost and single reference
                 *             per stage.
                 * */
                Type t = type;
                if (t == null)
                    throw new ArgumentNullException("type");
                //lock (CLIGateway.CompiledTypeCache)
                IType result;
                lock (cacheLock)
                    if (CLIGateway.CompiledTypeCache.TryGetValue(t, out result))
                        return result;
                Type byRefType = null;
                #region GenericParameter breakdown

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
                    typeParameters = t.GetGenericArguments().ToCollection();
                    t = t.GetGenericTypeDefinition();
                }
                #endregion

                #endregion

                #region Initial type instantiation/retrieval.

                if (CompiledTypeCache.ContainsKey(t))
                    result = CompiledTypeCache[t];
                else if (t.IsGenericParameter)
                {
                    #region GenericParameter-Parameter resolution.
                    bool positiveMatch = true;
                    if (t.DeclaringMethod == null && t.DeclaringType != null)
                    {
                        IGenericType lookupPoint = (IGenericType)t.DeclaringType.GetTypeReference();
                        if (!(t.GenericParameterPosition < 0 || t.GenericParameterPosition >= lookupPoint.GenericParameters.Count))
                        {
                            result = lookupPoint.GenericParameters[t.GenericParameterPosition];
                            positiveMatch = true;
                        }
                    }
                    else if (t.DeclaringMethod != null)
                    {
                        IType lookupAid = t.DeclaringMethod.DeclaringType.GetTypeReference();
                        if (lookupAid is IMethodParent)
                        {
                            IMethodParent lookupAid2 = (IMethodParent)lookupAid;
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
                            IMethodSignatureParent lookupAid2 = (IMethodSignatureParent)lookupAid;
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
                    if (!positiveMatch)
                        return null;
                }
                else if (t.IsEnum)
                    result = new CompiledEnumType(t);
                else if (t.IsSubclassOf(typeof(Delegate)) && t != typeof(MulticastDelegate))
                    result = new CompiledDelegateType(t);
                else if (t.IsClass)
                    result = new CompiledClassType(t);
                else if (t.IsValueType ||
                         t == typeof(Enum))
                    result = new CompiledStructType(t);
                else if (t.IsInterface)
                    result = new CompiledInterfaceType(t);
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

        private static void CacheAdd(Type t, IType result)
        {
            lock (cacheLock)
                if (!CompiledTypeCache.ContainsKey(t))
                {
#if DEBUG
                    if (!t.HasElementType)
                        Debug.WriteLine("Cached {0}.", t);
#endif
                    CompiledTypeCache.Add(t, result);
                }
        }

        internal static void RemoveFromCache(this IType type)
        {
            if (cacheFreeze)
                return;
            lock(CompiledTypeCache)
                if (CompiledTypeCache.Values.Contains(type))
                    CompiledTypeCache.Remove(CompiledTypeCache.First(kvp => kvp.Value == type).Key);
        }

        internal static void RemoveFromCache(this ICompiledAssembly assembly)
        {
            lock (assemblyCache)
                if (assemblyCache.Values.Contains(assembly))
                    assemblyCache.Remove(assemblyCache.First(kvp => kvp.Value == assembly).Key);
        }

        public static TypedName GetTypedName(this Type type, string name)
        {
            return new TypedName(name, type.GetTypeReference());
        }

        public static TType MakeGenericClosure<TTypeIdentifier, TType>(this IGenericType<TTypeIdentifier, TType> target, params Type[] typeParameters)
            where TTypeIdentifier :
                IGenericTypeUniqueIdentifier
            where TType :
                IGenericType<TTypeIdentifier, TType>
        {
            if (!target.IsGenericConstruct)
                throw new InvalidOperationException(Resources.MustBeGenericType);
            else if (!target.IsGenericDefinition)
                throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
            return target.MakeGenericClosure(typeParameters.ToCollection());
        }

        /// <summary>
        /// Obtains an assembly reference from the common language infrastructure 
        /// for a given <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to obtain a 
        /// <see cref="ICompiledAssembly"/> reference for.</param>
        /// <returns>A <see cref="ICompiledAssembly"/> for the
        /// <paramref name="assembly"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="assembly"/>
        /// is null.</exception>
        public static ICompiledAssembly GetAssemblyReference(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            if (assemblyCache == null)
                assemblyCache = new Dictionary<Assembly, ICompiledAssembly>();
            lock (cacheClearClock)
                lock (cacheLock)
                {
                    if (assemblyCache.ContainsKey(assembly))
                        return assemblyCache[assembly];
                    CompiledAssembly result = new CompiledAssembly(assembly);
                    assemblyCache.Add(assembly, result);
                    return result;
                }
        }

        public static ICompiledModule GetModuleReference(this Module module)
        {
            var assem = (_ICompiledAssembly)module.Assembly.GetAssemblyReference();
            return assem.GetModule(module);
        }

        /// <summary>
        /// Determines whether the <see cref="IType"/> is a primitive or not.
        /// </summary>
        /// <param name="type">The <see cref="IType"/> to determine the primitive status of.</param>
        /// <returns>true if the <paramref name="type"/> provided is a primitive type; false, otherwise.</returns>
        public static bool IsPrimitive(this IType type)
        {
            if (type is IGenericParameter)
                return false;
            if (type is ICompiledType)
                return ((ICompiledType)type).UnderlyingSystemType.IsPrimitive;
            return false;
        }

        /// <summary>
        /// Obtains the <see cref="TypeCode"/> of a given type.
        /// </summary>
        /// <param name="type">The <see cref="IType"/> to retrieve the <see cref="TypeCode"/> of.</param>
        /// <returns>A <see cref="TypeCode"/> relative to the <paramref name="type"/> provided.</returns>
        public static TypeCode GetTypeCode(this IType type)
        {
            //If it's a standard compiled type, without any element, obtain the type code from 
            //System.Type.GetTypeCode(System.Type).
            if (type is ICompiledType && type.ElementClassification == TypeElementClassification.None)
                return Type.GetTypeCode(((ICompiledType) (type)).UnderlyingSystemType);
            return TypeCode.Object;
        }

        public static bool CanConvertTo(this Type from, Type to)
        {
            TypeCode fromTC = Type.GetTypeCode(from);
            TypeCode toTC = Type.GetTypeCode(to);
            if (fromTC != toTC)
                if (!(extrinsicSet.Contains(fromTC) || extrinsicSet.Contains(toTC)))
                    return conversionInfo2[fromTC, toTC];
            return to.IsAssignableFrom(from) ||
                TypesAreImplicitlyConvertible(from.GetTypeReference(), to.GetTypeReference());
        }

        public static bool CanConvertTo(this IType from, IType to)
        {
            TypeCode fromTC = from.GetTypeCode();
            TypeCode toTC = to.GetTypeCode();
            if (fromTC != toTC)
                if (!(extrinsicSet.Contains(fromTC) || extrinsicSet.Contains(toTC)))
                    return conversionInfo2[fromTC, toTC];
            return to.IsAssignableFrom(from) ||
                TypesAreImplicitlyConvertible(from, to);
        }

        private static bool TypesAreImplicitlyConvertible(IType from, IType to)
        {
            if (from is ICoercibleType)
            {
                ICoercibleType ctFrom = (ICoercibleType) from;

                if (to is ICoercibleType)
                {
                    ICoercibleType ctTo = (ICoercibleType) to;
                    return ctTo.TypeCoercions.HasImplicitCoercionFrom(from) || ctFrom.TypeCoercions.HasImplicitCoercionTo(to);
                }
                return ctFrom.TypeCoercions.HasImplicitCoercionTo(to);
            }
            else if (to is ICoercibleType)
            {
                ICoercibleType ctTo = (ICoercibleType) to;
                return ctTo.TypeCoercions.HasImplicitCoercionFrom(from);
            }
            return false;
        }

    }
}
