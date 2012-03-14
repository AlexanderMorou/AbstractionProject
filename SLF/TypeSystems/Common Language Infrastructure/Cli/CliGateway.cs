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
    public static class CliGateway
    {

        private static readonly HashSet<TypeCode> intrinsicSet = new HashSet<TypeCode> { TypeCode.Boolean, TypeCode.Byte, TypeCode.SByte, TypeCode.Char, TypeCode.UInt16, TypeCode.Int16, TypeCode.UInt32, TypeCode.Int32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal };
        private static readonly HashSet<TypeCode> extrinsicSet = new HashSet<TypeCode> { TypeCode.Empty, TypeCode.Object, TypeCode.DBNull, TypeCode.DateTime, TypeCode.String };
        internal static MultikeyedDictionary<TypeCode, TypeCode, bool> conversionInfo2 = GetConversionInfo2();

        private static MultikeyedDictionary<TypeCode, TypeCode, bool> GetConversionInfo2()
        {
            MultikeyedDictionary<TypeCode, TypeCode, bool> result = new MultikeyedDictionary<TypeCode, TypeCode, bool>();
            TypeCode[] intrinsicSet = CliGateway.intrinsicSet.ToArray();
            TypeCode[] extrinsicSet = CliGateway.extrinsicSet.ToArray();
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

        static CliGateway()
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

        public static TType MakeGenericClosure<TTypeIdentifier, TType>(this IGenericType<TTypeIdentifier, TType> target, ICliManager manager, params Type[] typeParameters)
            where TTypeIdentifier :
                IGenericTypeUniqueIdentifier
            where TType :
                IGenericType<TTypeIdentifier, TType>
        {
            if (!target.IsGenericConstruct)
                throw new InvalidOperationException(Resources.MustBeGenericType);
            else if (!target.IsGenericDefinition)
                throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
            return target.MakeGenericClosure(typeParameters.ToCollection(manager));
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
        public static ICompiledAssembly GetAssemblyReference(this Assembly assembly, ICliManager manager)
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
                    CompiledAssembly result = new CompiledAssembly(assembly, manager);
                    assemblyCache.Add(assembly, result);
                    return result;
                }
        }

        public static ICompiledModule GetModuleReference(this Module module, ICliManager manager)
        {
            var assem = (_ICompiledAssembly)module.Assembly.GetAssemblyReference(manager);
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

        public static bool CanConvertTo(this Type from, Type to, ICliManager manager)
        {
            TypeCode fromTC = Type.GetTypeCode(from);
            TypeCode toTC = Type.GetTypeCode(to);
            if (fromTC != toTC)
                if (!(extrinsicSet.Contains(fromTC) || extrinsicSet.Contains(toTC)))
                    return conversionInfo2[fromTC, toTC];
            return to.IsAssignableFrom(from) ||
                TypesAreImplicitlyConvertible(manager.ObtainTypeReference(from), manager.ObtainTypeReference(to));
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

        public static ICliManager CreateIdentityManager()
        {
            return new CliManager();
        }
    }
}
