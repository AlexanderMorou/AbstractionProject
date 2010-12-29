using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Threading.Tasks;
using System.Reflection.Emit;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Provides helper functions for the Common Language 
    /// Infrastructure and the abstract syntax tree defined 
    /// by the Scripting Language Foundation.
    /// </summary>
    public static class CLIGateway
    {
        static CLIGateway()
        {
//            Console.WriteLine();
        }
        private static IDictionary<Assembly, ICompiledAssembly> assemblyCache;
        /// <summary>
        /// Stores the CLI type->SLF type relationships.
        /// </summary>
        internal static IDictionary<Type, IType> CompiledTypeCache = new Dictionary<Type, IType>();
        private static bool cacheFreeze = false;
        /// <summary>
        /// Clears the cache.
        /// </summary>
        public static void ClearCache()
        {
            if (assemblyCache != null)
            {
                var copy = assemblyCache.Values.ToArray();
                assemblyCache.Clear();
                foreach (var assembly in copy)
                    assembly.Dispose();
            }
            cacheFreeze = true;
            Parallel.ForEach(CompiledTypeCache.Values.ToArray(), p => p.Dispose());
            cacheFreeze = false;
            CompiledTypeCache.Clear();
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

        /// <summary>
        /// Obtains a <typeparamref name="TCompiledType"/> for a compiled type.
        /// </summary>
        /// <typeparam name="TCompiledType">The type of <see cref="ICompiledType"/>
        /// to return.</typeparam>
        /// <param name="type">The <see cref="System.Type"/> which needs a 
        /// <typeparamref name="TCompiledType"/>.</param>
        /// <returns>A new <see cref="IType"/> instance which points to the <paramref name="type"/> provided</returns>
        /// <exception cref="System.ArgumentException">thrown when the <paramref name="type"/>
        /// provided did not yield a <see cref="ICompiledType"/> instance of the
        /// <typeparamref name="TCompiledType"/> type provided.</exception>
        public static TType GetTypeReference<TType>(this Type type)
            where TType :
                IType<TType>
        {
            IType ict = (type.GetTypeReference());
            if (!(ict is TType))
                throw new ArgumentException("type");
            return ((TType)ict);
        }

        public static IType GetTypeReference(this Type type, ITypeCollection typeParameters)
        {
            if (type.IsGenericType && type.IsGenericTypeDefinition)
                return ((IGenericType)type.GetTypeReference()).MakeGenericClosure(typeParameters);
            throw new ArgumentException("type is not a generic type or is already an instance of a generic type.", "type");
        }

        public static IType GetTypeReference(this Type type, params IType[] typeParameters)
        {
            if (type.IsGenericType && type.IsGenericTypeDefinition)
                return ((IGenericType)type.GetTypeReference()).MakeGenericClosure(typeParameters);
            throw new ArgumentException("type is not a generic type or is already an instance of a generic type.", "type");
        }
        public static TType GetTypeReference<TType>(this Type type, ITypeCollection typeParameters)
            where TType :
                IGenericType<TType>
        {
            if (type.IsGenericType && type.IsGenericTypeDefinition)
                return type.GetTypeReference<TType>().MakeGenericClosure(typeParameters);
            throw new ArgumentException("type is not a generic type or is already an instance of a generic type.", "type");
        }

        public static TType GetTypeReference<TType>(this Type type, params IType[] typeParameters)
            where TType :
                IGenericType<TType>
        {
            if (type.IsGenericType && type.IsGenericTypeDefinition)
                return type.GetTypeReference<TType>().MakeGenericClosure(typeParameters);
            throw new ArgumentException("type is not a generic type or is already an instance of a generic type.", "type");
        }

        /// <summary>
        /// Obtains a <see cref="IType"/> reference for the associated
        /// <see cref="Type"/> provided.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to obtain a <see cref="IType"/>
        /// for.</param>
        /// <returns>An <see cref="IType"/> instance that wraps the <paramref name="type"/>
        /// provided.</returns>
        public static IType GetTypeReference(this Type type)
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
            //lock (CLIGateway.CompiledTypeCache)
            IType result;
            lock (CLIGateway.CompiledTypeCache)
                if (CLIGateway.CompiledTypeCache.TryGetValue(t, out result))
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
                #region Type-Parameter resolution.
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
            else if (t.IsSubclassOf(typeof(Delegate)))
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

            #region Type production
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

        private static void CacheAdd(Type t, IType result)
        {
            lock (CompiledTypeCache)
                if (!CompiledTypeCache.ContainsKey(t))
                    CompiledTypeCache.Add(t, result);
        }

        /// <summary>
        /// Obtains a <see cref="ITypeCollection"/> for the <paramref name="array"/> of <see cref="System.Type"/>
        /// provided.
        /// </summary>
        /// <param name="array">The array of <see cref="System.Type"/> the resulted
        /// <see cref="ITypeCollection"/> should contain.</param>
        /// <returns>A new <see cref="ITypeCollection"/> instance containing the 
        /// <paramref name="array"/> entries.</returns>
        public static ITypeCollection ToCollection(this Type[] array)
        {
            return array.OnAll(u => u.GetTypeReference()).ToArray().ToCollection();
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

        public static TType MakeGenericClosure<TType>(this IGenericType<TType> target, params Type[] typeParameters)
            where TType :
                IGenericType<TType>
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
            if (assemblyCache.ContainsKey(assembly))
                return assemblyCache[assembly];
            CompiledAssembly result = new CompiledAssembly(assembly);
            assemblyCache.Add(assembly, result);
            return result;
        }

        /// <summary>
        /// Adds a series of <see cref="Type"/> references to the <see cref="ITypeCollection"/>
        /// <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The target <see cref="ITypeCollection"/> to receive the series of
        /// <see cref="IType"/> reference wrappers.</param>
        /// <param name="types">The zero-based index <see cref="Type"/> array 
        /// to add to the <paramref name="target"/>.</param>
        /// <returns></returns>
        public static IType[] AddRange(this ITypeCollection target, params Type[] types)
        {
            IType[] result = new IType[types.Length];
            for (int i = 0; i < types.Length; i++)
                result[i] = types[i].GetTypeReference();
            target.AddRange(result);
            return result;
        }

        /// <summary>
        /// Inserts and returns the <see cref="IType"/> translated from the <paramref name="type"/>
        /// provided.
        /// </summary>
        /// <param name="type">A <see cref="System.Type"/> to insert and return as a <see cref="IType"/>.</param>
        /// <returns>A <see cref="IType"/> instance that was inserted in place of the <paramref name="type"/>.</returns>
        public static IType Add(this ITypeCollection target, Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            IType result = type.GetTypeReference();
            target.Add(result);
            return result;
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
        internal static TypeCode GetTypeCode(this IType type)
        {
            //If it's a standard compiled type, without any element, obtain the type code from 
            //System.Type.GetTypeCode(System.Type).
            if (type is ICompiledType && type.ElementClassification == TypeElementClassification.None)
                return Type.GetTypeCode(((ICompiledType)(type)).UnderlyingSystemType);
            return TypeCode.Object;
        }
        /// <summary>
        /// Checks to see if you can go <paramref name="from"/> one type <paramref name="to"/> another.
        /// </summary>
        /// <param name="from">The type to check conversion of.</param>
        /// <param name="to">The type to see if <paramref name="from"/> can go to.</param>
        /// <returns>True if <paramref name="from"/> can be cast/converted <paramref name="to"/>; otherwise false.</returns>
        public static bool CanConvertFrom(this IType from, IType to)
        {
            TypeCode fromTC = GetTypeCode(from);
            TypeCode toTC = GetTypeCode(to);
            try
            {
                if (fromTC != toTC)
                    return TypeExtensions.conversionInfo[fromTC][toTC];
                else if (fromTC == TypeCode.Object)
                    //ToDo: Add code here to use the Expression coersion members to find an
                    //implicit coercion from 'from' to 'to'.
                    return (to.IsAssignableFrom(from));
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        /*//
        public static bool CanConvertFrom(this IType from, IType to, ref IImplicitChain translation)
        {
            TypeCode fromTC = GetTypeCode(from);
            TypeCode toTC = GetTypeCode(to);
            try
            {
                if (fromTC != toTC)
                    return TypeExtensions.conversionInfo[fromTC][toTC];
                else if (fromTC == TypeCode.Object)
                    //ToDo: Add code here to use the Expression coersion members to find an
                    //implicit coercion from 'from' to 'to'.
                    return (to.IsAssignableFrom(from));
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
        //*/

        /// <summary>
        /// Verifies the type-parameters of a generic method 
        /// <paramref name="signature"/> with the <paramref name="typeReplacements"/>
        /// provided.
        /// </summary>
        /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
        /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/>
        /// and a child of <typeparamref name="TSignatureParent"/>.</typeparam>
        /// <typeparam name="TSignatureParent">The parent that contains the <typeparamref name="TSignature"/> 
        /// instances.</typeparam>
        /// <param name="signature">The <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// to verify the <paramref name="typeReplacements"/> against.</param>
        /// <param name="typeReplacements">The <see cref="ITypeCollection"/> that defines
        /// the replacement types to verify.</param>
        /// <remarks>TGeneric* type-parameters are primarily used to complete the hierarchy
        /// chain to allow for type strict reverse-traversal.</remarks>
        #if !DEBUG
        [DebuggerHidden]
        #endif
        public static void VerifyTypeParameters<TSignatureParameter, TSignature, TSignatureParent>(this IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent> signature, ITypeCollectionBase typeReplacements)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            if (signature == null)
                throw new ArgumentNullException("signature");
            if (typeReplacements == null)
                throw new ArgumentNullException("typeReplacements");
            /* *
             * Setup the test case parameter logic.
             * *
             * If the method belongs to a generic type, mine the 
             * generic type-parameters from it to help in 
             * properly disambiguating the method's type-parameter
             * constraints.
             * */
            var parentTypeReplacements = LockedTypeCollection.Empty;
            TypeParameterSources source = TypeParameterSources.Method;
            if (signature.Parent is IGenericType && ((IGenericType)(signature.Parent)).IsGenericConstruct)
            {
                IGenericType parent = ((IGenericType)(signature.Parent));
                /* *
                 * Special case on when the parent is a generic definition.
                 * It's still a valid call if the type-replacements passed
                 * contain enough replacements to fill the parent's
                 * and the method's type-parameters.  Least likely case.
                 * *
                 * Obtain the replacements 
                 * */
                if (parent.IsGenericDefinition)
                    if (typeReplacements.Count == parent.GenericParameters.Count + signature.GenericParameters.Count)
                    {
                        parentTypeReplacements = typeReplacements.Take(parent.GenericParameters.Count).ToLockedCollection();
                        typeReplacements = typeReplacements.Skip(parentTypeReplacements.Count).ToLockedCollection();
                        source = TypeParameterSources.Both;
                    }
                    else
                        throw new ArgumentException("typeReplacements");
                else
                {
                    parentTypeReplacements = parent.GenericParameters;
                    source = TypeParameterSources.Both;
                }
            }
            else if (signature.TypeParameters.Count != typeReplacements.Count)
                throw new ArgumentException("typeReplacements");
            VerifyTypeParameters_VerifyReplacements(typeReplacements);
            /* *
             * Generate test-case generic parameters
             * to do the verification with.
             * *
             * Logic to handle the parentTypeReplacements is 
             * defined above.
             * */
            IGenericTestCaseParameter[] testCases =
                (from IGenericParameter t in signature.GenericParameters
                 select new GenericVerificationParameter(
                     (from k in t.Constraints
                      select k.Disambiguify(
                             parentTypeReplacements,
                             typeReplacements,
                             source)).ToCollection(), t)).ToArray();
            VerifyTypeParametersInternal(typeReplacements, testCases);
        }

        /// <summary>
        /// Verifies a set of <paramref name="typeReplacement"/> <see cref="IType"/> instances
        /// against the type-parameters defined on the <paramref name="genericType"/>.
        /// </summary>
        /// <param name="genericType">The <see cref="IGenericType"/>
        /// which contains the parameters to verify against.</param>
        /// <param name="typeReplacements">The <see cref="ITypeCollection"/> that defines
        /// the replacement types to verify.</param>
        public static void VerifyTypeParameters(this IGenericType genericType, ITypeCollectionBase typeReplacements)
        {
            if (genericType == null)
                throw new ArgumentNullException("genericType");
            if (typeReplacements == null)
                throw new ArgumentNullException("typeReplacements");
            if (genericType.GenericParameters.Count != typeReplacements.Count)
                throw new ArgumentException("typeReplacements");
            VerifyTypeParameters_VerifyReplacements(typeReplacements);

            /* *
             * Obtain a series of generic verifiers that act as dummy
             * generic parameters for the checks to be performed upon.
             * */
            IGenericTestCaseParameter[] testCases =
                (from IGenericTypeParameter t in genericType.GenericParameters
                 select new GenericVerificationParameter(
                     (from k in t.Constraints
                      select k.Disambiguify(
                             typeReplacements,
                             TypeCollection.Empty,
                             TypeParameterSources.Type)).ToCollection(), t)).ToArray();
            VerifyTypeParametersInternal(typeReplacements, testCases);
        }

        private static void VerifyTypeParameters_VerifyReplacements(ITypeCollectionBase typeReplacements)
        {
            /* *
             * Verify the type-replacements given are valid and don't
             * contain pointers, by-reference types or the 
             * System.Void ValueType/struct.
             * */
            for (int i = 0; i < typeReplacements.Count; i++)
                if (typeReplacements[i] == null)
                    throw new ArgumentNullException(string.Format("typeReplacements[{0}]", i));
                //Pointer types, by-reference types, and the void type cannot be generic type parameters.
                else if (typeReplacements[i].ElementClassification == TypeElementClassification.Pointer ||
                         typeReplacements[i].ElementClassification == TypeElementClassification.Reference ||
                         typeReplacements[i] is ICompiledType && ((ICompiledType)typeReplacements[i]).UnderlyingSystemType == typeof(void))
                    throw new ArgumentException(string.Format(Resources.TypeConstraintFailure_InvalidType, typeReplacements[i].ToString()));
                else if (typeReplacements[i] is IClassType && ((IClassType)(typeReplacements[i])).SpecialModifier != SpecialClassModifier.None)
                    throw new ArgumentException(string.Format(Resources.TypeConstraintFailure_InvalidType_AbstractSealed, typeReplacements[i].ToString()));

        }

        private static void VerifyTypeParametersInternal(ITypeCollectionBase typeReplacements, IGenericTestCaseParameter[] testCases)
        {
            /* *
             * Iterate through the replacements and compare them against 
             * the test cases.
             * */
            Parallel.For(0, testCases.Length, i=>
            //for (int i = 0; i < testCases.Length; i++)
            {
                IGenericTestCaseParameter param = testCases[i];
                IType replacement = typeReplacements[i];
                /* *
                 * Generic parameters require special processing.
                 * */
                if (replacement.IsGenericTypeParameter && replacement is IGenericParameter)
                {
                    IGenericParameter replacementParam = ((IGenericParameter)(replacement));
                    if (param.RequiresNewConstructor && !replacementParam.RequiresNewConstructor)
                        throw new ArgumentException(string.Format(Resources.TypeConstraintFailure,
                            /*{0}*/ replacementParam.Name,
                            /*{1}*/ param.Name,
                            /*{2}*/ Resources.TypeConstraintFailure_NewConstraint));
                    if (param.SpecialConstraint == GenericTypeParameterSpecialConstraint.Class && !
                          ((replacementParam.SpecialConstraint == GenericTypeParameterSpecialConstraint.Class ||
                           (replacementParam.Constraints.Count > 0 &&
                            replacementParam.Constraints[0] is IReferenceType))))
                        throw new ArgumentException(string.Format(Resources.TypeConstraintFailure,
                            /*{0}*/ replacementParam.Name,
                            /*{1}*/ param.Name,
                            /*{2}*/ Resources.TypeConstraintFailure_ReferenceType));
                    else if (param.SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct &&
                             replacementParam.SpecialConstraint != GenericTypeParameterSpecialConstraint.Struct)
                        throw new ArgumentException(string.Format(Resources.TypeConstraintFailure,
                            /*{0}*/ replacementParam.Name,
                            /*{1}*/ param.Name,
                            /*{2}*/ Resources.TypeConstraintFailure_ValueType));
                    /* *
                     * Verify each constraint, they were translated from their
                     * earlier form into a type-parameter resolved form.
                     * */
                    foreach (IType constraint in param.Constraints)
                        if (!replacementParam.Constraints.Any(replacementConstraint => constraint.IsAssignableFrom(replacementConstraint)))
                            throw new ArgumentException(string.Format(Resources.TypeConstraintFailure,
                                /*{0}*/ replacementParam.Name,
                                /*{1}*/ param.Name,
                                /*{2}*/ string.Format(Resources.TypeConstraintFailure_ParamConstraint, constraint.IsGenericTypeParameter ? constraint.Name : constraint.FullName)));

                }
                else
                {
                    /* *
                     * Structs and enumerations are both value types and
                     * automatically contain a default constructor 
                     * (See CIL keyword 'initobj'; requires
                     * variable address to be loaded onto the stack).
                     * */
                    if (param.RequiresNewConstructor && !(replacement.Type == TypeKind.Struct || replacement.Type == TypeKind.Enumerator))
                    {
                        if (replacement.Type == TypeKind.Interface || (!(replacement is ICreatableType)))
                            throw new ArgumentException(string.Format(Resources.TypeConstraintFailure,
                                /*{0}*/ replacement.Name,
                                /*{1}*/ param.Name,
                                /*{2}*/ Resources.TypeConstraintFailure_NewInterfaceDelegateOther));
                        ICreatableType creatableReplacement = (ICreatableType)(replacement);
                        if (creatableReplacement.Constructors.Find().Count == 0)
                            throw new ArgumentException(string.Format(Resources.TypeConstraintFailure,
                                /*{0}*/ replacement.Name,
                                /*{1}*/ param.Name,
                                /*{2}*/ Resources.TypeConstraintFailure_NewStandard));
                    }

                    /* *
                     * SpecialConstraint check.
                     * */
                    switch (param.SpecialConstraint)
                    {
                        case GenericTypeParameterSpecialConstraint.Struct:
                            if (!(replacement.Type == TypeKind.Enumerator ||
                                replacement.Type == TypeKind.Struct))
                                throw new ArgumentException(string.Format(Resources.TypeConstraintFailure,
                                    /*{0}*/ replacement.Name,
                                    /*{1}*/ param.Name,
                                    /*{2}*/ Resources.TypeConstraintFailure_ValueType));
                            break;
                        case GenericTypeParameterSpecialConstraint.Class:
                            /* *
                             * Since I can't predict the future, I used an
                             * interface to replace specific type checking.
                             * If someone wants to expand the framework
                             * for whatever reason, this makes things easier.
                             * */
                            if (!(replacement is IReferenceType))
                                throw new ArgumentException(string.Format(Resources.TypeConstraintFailure,
                                    /*{0}*/ replacement.Name,
                                    /*{1}*/ param.Name,
                                    /*{2}*/ Resources.TypeConstraintFailure_ReferenceType));
                            break;
                    }
                    /* *
                     * Every constraint needs to be assignable 
                     * by the replacement.
                     * */
                    for (int j = 0; j < param.Constraints.Count; j++)
                    {
                        IType constraint = param.Constraints[j];
                        if (!constraint.IsAssignableFrom(replacement))
                        {
                            IType originalConstraint = param.Original.Constraints[j];
                            throw new ArgumentException(string.Format(Resources.TypeConstraintFailure,
                                /*{0}*/ replacement.Name,
                                /*{1}*/ param.Original.Name,
                                /*{2}*/ string.Format(Resources.TypeConstraintFailure_Constraint, originalConstraint.IsGenericTypeParameter ? originalConstraint.Name : originalConstraint.BuildTypeName(true), constraint.BuildTypeName(true))));
                        }
                    }

                }
                /* *
                 * Dispose the type-parameter test case, it's no longer needed
                 * */
                param.Dispose();
            });
        }
        /// <summary>
        /// Obtains a <see cref="ILockedTypeCollection"/> for the <paramref name="array"/> 
        /// of <see cref="System.Type"/> provided.
        /// </summary>
        /// <param name="array">The array of <see cref="System.Type"/> the resulted
        /// <see cref="ITypeCollection"/> should contain.</param>
        /// <returns>A new <see cref="ILockedTypeCollection"/> instance containing the 
        /// <paramref name="array"/> entries.</returns>
        public static ILockedTypeCollection ToLockedCollection(this Type[] array)
        {
            return array.OnAll(u => u.GetTypeReference()).ToArray().ToLockedCollection();
        }

        internal static IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> FindCache<TSignature, TSignatureParameter, TSignatureParent>(IEnumerable<TSignature> values, IEnumerable<IType> search, bool strict)
            where TSignature :
                ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParameter :
                ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            int searchCount = search.Count();
            if (strict)
            {
                //So simple and straightforward.
                return new FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent>(
                    //Filter the signatures...
                    values.Filter(t =>
                    {
                        return ParametersTypeCheck<TSignature, TSignatureParameter, TSignatureParent>(search, searchCount, null, t, (a, b) => a.Equals(b));
                    }));
            }
            else
            {
                IDictionary<TSignature, int> deviations = new Dictionary<TSignature, int>();
                FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent> result = new FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent>(
                    //Filter the signatures...
                    values.Filter(t =>
                    {
                        return ParametersTypeCheck<TSignature, TSignatureParameter, TSignatureParent>(search, searchCount, deviations, t, (a, b) => TypeToParamCheck<TSignature>(deviations, t, a, b));
                    }));
                result.deviations = deviations;
                result.SortByDeviations();
                return result;
            }
        }

        private static bool ParametersTypeCheck<TSignature, TSignatureParameter, TSignatureParent>(IEnumerable<IType> search, int searchCount, IDictionary<TSignature, int> deviations, TSignature t, Func<IType, IType, bool> typeChecker)
            where TSignature :
                ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParameter :
                ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            bool strict = (deviations == null);
            if (!strict)
                deviations.Add(t, 0);
            //Annoying params parameter check...
            if (t.LastIsParams)
            {
                if (searchCount < t.Parameters.Count)
                    return false;
                bool paramsDeviate = false;
                if (!(strict || 
                    (searchCount == t.Parameters.Count &&
                     search.ElementAt(search.Count() - 1)
                        .Equals(t.Parameters[t.Parameters.Count - 1]))))
                {
                    paramsDeviate = true;
                    if (deviations != null)
                        deviations[t]++;
                }
                int tCount = t.Parameters.Count;
                //Strict searches require exact matches.
                if (strict && tCount != searchCount)
                    return false;
                if (t.Parameters.Values.Take(tCount - 1).CompareSeriesTo(search.Take(tCount - 1), (a, b) => typeChecker(a.ParameterType, b)))
                {
                    /* *
                     * There was no derivative marked on the params parameter.
                     * Assume that is because the last element and all elements
                     * before are equivalent enough to use.  Element checking here
                     * would invalidate the search.
                     * */
                    if (!paramsDeviate)
                        return true;
                    /* *
                     * So far, so good, the first n-1 elements checked out.
                     * Next step is to check the n->z elements to see if they
                     * match the element type of the params member.
                     * */
                    TSignatureParameter paramsParam = t.Parameters.Values.ElementAt(tCount - 1);
                    /* *
                     * Just in case someone applied the attribute illegally.
                     * Is it possible?
                     * */
                    if (paramsParam.ParameterType.ElementClassification == TypeElementClassification.Array)
                    {
                        if (search.Take(tCount - 1, searchCount - (tCount - 1)).All(p =>
                            typeChecker(paramsParam.ParameterType.ElementType, p)))
                            return true;
                    }
                }
                //No match found.
                if (paramsDeviate && deviations != null)
                    deviations.Remove(t);
                return false;
            }
            //When their parameters match.
            bool bResult = t.Parameters.Values.CompareSeriesTo(search, (a, b) =>
            {
                return typeChecker(a.ParameterType, b);
            });
            //If a parameter didn't match at all.
            if (deviations != null && (!bResult && deviations.ContainsKey(t)))
                deviations.Remove(t);
            return bResult;
        }

        internal static IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> FindCacheTest<TSignature, TGenericParameter, TGenericParameterConstructor, TGenericParameterConstructorParameter, TSignatureParameter, TSignatureParent>(ITypeCollectionBase genericParameters, IControlledStateCollection<TSignature> values, string name, IEnumerable<IType> search, bool strict)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            //As it implies, simpler.
            if (strict)
            {
                var resultSys = values.Where(method =>
                {
                    if (method.Name != name)
                        return false;
                    if (genericParameters != null && genericParameters.Count > 0)
                    {
                        //Obvious mismatch.
                        if (!method.IsGenericConstruct)
                            return false;
                        //...same.
                        else if (method.TypeParameters.Count != genericParameters.Count)
                            return false;
                        //Compare the type-parameters.
                        try { method.VerifyTypeParameters(genericParameters); }
                        catch (ArgumentException) { return false; }
                    }
                    else if (method.IsGenericConstruct)
                        //Strict mode requires type-parameters to be present.
                        return false;

                    return method.Parameters.Values.CompareSeriesTo(search, (methodParam, searchType) => methodParam.ParameterType.Equals(searchType));
                });
                return new FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent>(resultSys.ToArray());
            }
            else
            {
                var resultSys = values.Where(method =>
                {
                    if (method.Name != name)
                        return false;
                    /* *
                     * Notes   : There are a few implied things known.
                     *           When inferring the types, you know that 
                     *           the generic parameters are not given.
                     *           Therefore, only check in this condition.
                     *           Multiple resultant generic parameter 
                     *           sets may result; however, the best choice 
                     *           is always sort by the least deviations.
                     * *
                     * Cautions: High levels of inference may yield MULTIPLE
                     *           result sets.  As a result, the CIL translator
                     *           will have to fill in the gaps through
                     *           analyzing the associated set result 
                     *           interactions.  If a member on one possible
                     *           match is used, then instances without
                     *           said member(s) can be eliminated.
                     *           Similar inferences can be made through remaining
                     *           sets and the usage of chained expressions that 
                     *           use the result of the call.
                     * */
                    if (genericParameters != null)
                    {
                        /* *
                         * Generic parameters being present automatically
                         * eliminate non-generic methods.
                         * */
                        if (!method.IsGenericConstruct)
                            return false;

                        //if (!method.TypeParameters.Values.All(gP => gP.VerifyTypeParameter(genericParameters[gP.Position])))
                        //    return false;
                        try { method.VerifyTypeParameters(genericParameters); }
                        catch (ArgumentException) { return false; }
                    }
                    else
                    {
                    }
                    throw new NotImplementedException();
                });
                throw new NotImplementedException();
            }
        }

        internal static IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> FindCache<TSignature, TSignatureParameter, TSignatureParent>(ITypeCollection genericParameters, IControlledStateCollection<TSignature> values, string name, IEnumerable<IType> search, bool strict)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            int searchCount = search.Count();
            if (strict)
            {
                return new FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent>(
                    values.Filter(t =>
                    {
                        if (genericParameters != null && genericParameters.Count > 0)
                        {
                            if (t.IsGenericConstruct && t.GenericParameters.Count != genericParameters.Count)
                                return false;
                            //Generic variant test...
                            try
                            {
                                TSignature gVar = t.MakeGenericClosure(genericParameters);
                            }
                            catch (ArgumentException)
                            {
                                return false;
                            }
                            return true;
                        }
                        else
                            if (t.IsGenericConstruct)
                                return false;

                        if (t.Name != name)
                            return false;
                        return ParametersTypeCheck<TSignature, TSignatureParameter, TSignatureParent>(search, searchCount, null, t, (a, b) => a.Equals(b));
                    }).OnAll(e =>
                    {
                        //Transform the methods if they're generics.
                        if (e.IsGenericConstruct)
                        {
                            return e.MakeGenericClosure(genericParameters);
                        }
                        return e;
                    }).ToArray());
            }
            else
            {
                Dictionary<TSignature, Tuple<ITypeCollection, ITypeCollection>> successfulPermutations = new Dictionary<TSignature, Tuple<ITypeCollection, ITypeCollection>>();
                IDictionary<TSignature, int> deviations = new Dictionary<TSignature, int>();
                FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent> result = new FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent>(
                    //Filter the signatures...
                    values.Filter(t =>
                    {
                        Tuple<ITypeCollection, ITypeCollection> methodGenericParameters = null;
                        if (t.IsGenericConstruct && t.IsGenericDefinition)
                        {
                            methodGenericParameters = new Tuple<ITypeCollection, ITypeCollection>(new TypeCollection(), new TypeCollection());
                            t.TypeParameters.Values.ToArray().OnAll(tgp =>
                            {
                                methodGenericParameters.Item1.Add(tgp);
                                methodGenericParameters.Item2.Add(genericParameters[tgp.Position]);
                            });
                        }
                        if (t.Name != name)
                            return false;
                        if (ParametersTypeCheck<TSignature, TSignatureParameter, TSignatureParent>(search, searchCount, deviations, t, (a, b) => MethodMemberParameterTypeCheck<TSignature, TSignatureParameter, TSignatureParent>(genericParameters, deviations, t, a, b, methodGenericParameters)))
                        {
                            if (methodGenericParameters != null)
                                successfulPermutations.Add(t, methodGenericParameters);
                            return true;
                        }
                        return false;
                    }));
                result.deviations = deviations;
                result.SortByDeviations();
                return result;
            }


        }

        private static bool MethodMemberParameterTypeCheck<TSignature, TSignatureParameter, TSignatureParent>(ITypeCollection genericParameters, IDictionary<TSignature, int> deviations, TSignature method, IType parameterType, IType sourceType, Tuple<ITypeCollection, ITypeCollection> methodGenericParameters)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            if (method.IsGenericConstruct && method.IsGenericDefinition && parameterType.ContainsGenericParameters() &&
                !((genericParameters == null) || (genericParameters.Count == 0)))
            {
                /* *
                 * The unthinkable... Type-Parameter inferrence, 
                 * GREAT FUN.
                 * */
                return CompareAndContrast<TSignature, TSignatureParameter, TSignatureParent>(parameterType, sourceType, methodGenericParameters, method, deviations);
            }
            return TypeToParamCheck<TSignature>(deviations, method, parameterType, sourceType);
        }

        private static bool CompareAndContrast<TSignature, TSignatureParameter, TSignatureParent>(IType parameterType, IType sourceType, Tuple<ITypeCollection, ITypeCollection> methodGenericParameters, TSignature method, IDictionary<TSignature, int> deviations)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            if (method.Parent is IGenericType)
            {
                var pParent = (IGenericType)method.Parent;
                if (pParent.IsGenericConstruct)
                    return TypeToParamCheck<TSignature>(deviations, method, parameterType.Disambiguify(pParent.GenericParameters, methodGenericParameters.Item2, TypeParameterSources.Both), sourceType);
            }
            return TypeToParamCheck<TSignature>(deviations, method, parameterType.Disambiguify(null, methodGenericParameters.Item2, TypeParameterSources.Method), sourceType);
        }

        private static bool TypeToParamCheck<TSignature>(IDictionary<TSignature, int> deviations, TSignature t, IType a, IType b)
        {
            if (a.Equals(b))
                return true;
            else if (b.CanConvertFrom(a))
            {
                //Increase how far it deviated.
                deviations[t]++;
                return true;
            }
            else if (a == typeof(object).GetTypeReference())
            {
                deviations[t]++;
                return true;
            }
            else
                return false;
        }

        public static ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> FilterByName<TSignature, TSignatureParameter, TSignatureParent>(this ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> criteria, string name)
            where TSignature :
                ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParameter :
                ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            return new FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent>(criteria.Values.Filter(j => j.Name == name));
        }

        /// <summary>
        /// Adds a <see cref="TypedName"/> to the <paramref name="target"/> <see cref="TypedNameSeries"/>
        /// with the <paramref name="name"/> and <paramref name="type"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="TypedNameSeries"/> to add the typed name to.</param>
        /// <param name="name">The name of the type name pair.</param>
        /// <param name="type">The <see cref="Type"/> of the type name pair.</param>
        public static void Add(this TypedNameSeries target, string name, Type type)
        {
            target.Add(name, type.GetTypeReference());
        }

        /// <summary>
        /// Adds a <see cref="TypedName"/> to the <paramref name="target"/> <see cref="TypedNameSeries"/>
        /// with the <paramref name="name"/> and <paramref name="type"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="TypedNameSeries"/> to add the typed name to.</param>
        /// <param name="name">The name of the type name pair.</param>
        /// <param name="type">The <see cref="Type"/> of the type name pair.</param>
        /// <param name="direction">The <see cref="ParameterDirection"/> which determines
        /// how the type should be coerced when using the type in an input position.</param>
        public static void Add(this TypedNameSeries target, string name, Type type, ParameterDirection direction)
        {
            target.Add(name, type.GetTypeReference());
        }

    }
}
