using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Provides a series of constants and methods used throughout the common
    /// language infrastructure type bridge.
    /// </summary>
    public static partial class CLICommon
    {

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
        /// <param name="target">The <see cref="ITypeCollection"/>
        /// on which to add the <paramref name="type"/>.</param>
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

        internal static IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Filter<TSignature, TSignatureParameter, TSignatureParent>(this IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> source, Predicate<TSignature> predicate)
            where TSignature :
                ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParameter :
                ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            return new FilteredSignatureMembers<TSignature, TSignatureParameter, TSignatureParent>(source.Values.Filter(predicate));
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
            else if (a == CommonTypeRefs.Object)
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
        /// Verifies a set of <paramref name="typeReplacements"/> <see cref="IType"/> instances
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
            Parallel.For(0, testCases.Length, i =>
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
        public static IFilteredSignatureMemberDictionary<IClassEventMember, IEventParameterMember<IClassEventMember, IClassType>, IClassType> FindInFamily(this IEventMemberDictionary<IClassEventMember, IClassType> target, IDelegateType searchCriteria)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (searchCriteria == null)
                throw new ArgumentNullException("searchCriteria");
            List<IClassEventMember> result = new List<IClassEventMember>();
            for (IClassType current = target.Parent; current != null; current = current.BaseType)
                foreach (var matchedItem in current.Events.Find(searchCriteria).Values)
                    if (result.FirstOrDefault(m => m.Name == matchedItem.Name) == null)
                        result.Add(matchedItem);
            return new FilteredSignatureMembers<IClassEventMember, IEventParameterMember<IClassEventMember, IClassType>, IClassType>(result.ToArray());
        }

        public static IClassMethodMember FindInFamily(this IMethodMemberDictionary<IClassMethodMember, IClassType> target, string methodName, params IType[] signature)
        {
            for (IClassType current = target.Parent; current != null; current = current.BaseType)
                foreach (var method in current.Methods.Values)
                    if (method.Name == methodName && signature.SequenceEqual(method.Parameters.ParameterTypes))
                        return method;
            return null;
        }

        public static IClassEventMember FindInFamily(this IEventMemberDictionary<IClassEventMember, IClassType> target, string eventName, IDelegateType searchCriteria)
        {
            for (IClassType current = target.Parent; current != null; current = current.BaseType)
                if (current.Events.Keys.Contains(eventName))
                    foreach (var @event in current.Events.Values)
                        if (@event.Name == eventName)
                            if (@event.SignatureSource == EventSignatureSource.Delegate)
                            {
                                if (@event.SignatureType == searchCriteria)
                                    return @event;
                            }
                            else if (searchCriteria.Parameters.ParameterTypes.SequenceEqual(@event.Parameters.ParameterTypes))
                                return @event;
            return null;
        }
    }
}
