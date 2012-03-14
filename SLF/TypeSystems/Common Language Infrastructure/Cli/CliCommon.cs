using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public static partial class CliCommon
    {

        /// <summary>
        /// Obtains a <see cref="ITypeCollection"/> for the <paramref name="array"/> of <see cref="System.Type"/>
        /// provided.
        /// </summary>
        /// <param name="array">The array of <see cref="System.Type"/> the resulted
        /// <see cref="ITypeCollection"/> should contain.</param>
        /// <returns>A new <see cref="ITypeCollection"/> instance containing the 
        /// <paramref name="array"/> entries.</returns>
        public static ITypeCollection ToCollection(this Type[] array, ICliManager manager)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            if (array == null)
                throw new ArgumentNullException("array");
            return array.OnAll(u => manager.ObtainTypeReference(u)).ToArray().ToCollection();
        }

        /// <summary>
        /// Obtains a <see cref="ILockedTypeCollection"/> for the <paramref name="array"/> 
        /// of <see cref="System.Type"/> provided.
        /// </summary>
        /// <param name="array">The array of <see cref="System.Type"/> the resulted
        /// <see cref="ITypeCollection"/> should contain.</param>
        /// <returns>A new <see cref="ILockedTypeCollection"/> instance containing the 
        /// <paramref name="array"/> entries.</returns>
        public static ILockedTypeCollection ToLockedCollection(this Type[] array, ICliManager manager)
        {
            return array.OnAll(u => manager.ObtainTypeReference(u)).ToArray().ToLockedCollection();
        }

        internal static IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> Filter<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(this IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> source, Predicate<TSignature> predicate)
            where TSignatureIdentifier :
                ISignatureMemberUniqueIdentifier,
                IGeneralMemberUniqueIdentifier
            where TSignature :
                ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParameter :
                ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return new FilteredSignatureMembers<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(source.Values.Filter(predicate));
        }

        internal static IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> FindCache<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(IEnumerable<TSignature> values, IEnumerable<IType> search, bool strict)
            where TSignatureIdentifier :
                ISignatureMemberUniqueIdentifier,
                IGeneralMemberUniqueIdentifier
            where TSignature :
                ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParameter :
                ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            ITypeIdentityManager manager = GetManager<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(values, search);
            int searchCount = search.Count();
            if (strict)
            {
                //So simple and straightforward.
                return new FilteredSignatureMembers<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(
                    //Filter the signatures...
                    values.Filter(t =>
                    {
                        return ParametersTypeCheck<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(search, searchCount, null, t, (a, b) => a.Equals(b));
                    }));
            }
            else
            {
                IDictionary<TSignature, int> deviations = new Dictionary<TSignature, int>();
                FilteredSignatureMembers<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> result = new FilteredSignatureMembers<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(
                    //Filter the signatures...
                    values.Filter(t =>
                    {
                        return ParametersTypeCheck<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(search, searchCount, deviations, t, (a, b) => TypeToParamCheck<TSignature>(deviations, t, a, b, manager));
                    }));
                result.deviations = deviations;
                result.SortByDeviations();
                return result;
            }
        }

        private static ITypeIdentityManager GetManager<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(IEnumerable<TSignature> values, IEnumerable<IType> search)
            where TSignatureIdentifier : ISignatureMemberUniqueIdentifier, IGeneralMemberUniqueIdentifier
            where TSignature : ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParameter : ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParent : ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            ITypeIdentityManager manager = (from t in search
                                            select t.Manager).FirstOrDefault();
            if (manager == null)
                manager = (from m in values
                           where m != null
                           from p in m.Parameters.Values
                           where p.ParameterType != null
                           select p.ParameterType.Manager).FirstOrDefault();
            return manager;
        }

        private static bool ParametersTypeCheck<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(IEnumerable<IType> search, int searchCount, IDictionary<TSignature, int> deviations, TSignature t, Func<IType, IType, bool> typeChecker)
            where TSignatureIdentifier :
                ISignatureMemberUniqueIdentifier,
                IGeneralMemberUniqueIdentifier
            where TSignature :
                ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParameter :
                ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
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

        internal static IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> FindCache<TSignature, TSignatureParameter, TSignatureParent>(ITypeCollection genericParameters, IControlledCollection<TSignature> values, string name, IEnumerable<IType> search, bool strict)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            ITypeIdentityManager manager = GetManager<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>(values, search);
            int searchCount = search.Count();
            if (strict)
            {
                return new FilteredSignatureMembers<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>(
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
                        return ParametersTypeCheck<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>(search, searchCount, null, t, (a, b) => a.Equals(b));
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
                FilteredSignatureMembers<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> result = new FilteredSignatureMembers<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>(
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
                        if (ParametersTypeCheck<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>(search, searchCount, deviations, t, (a, b) => MethodMemberParameterTypeCheck<TSignature, TSignatureParameter, TSignatureParent>(genericParameters, deviations, t, a, b, methodGenericParameters, manager)))
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

        private static bool MethodMemberParameterTypeCheck<TSignature, TSignatureParameter, TSignatureParent>(ITypeCollection genericParameters, IDictionary<TSignature, int> deviations, TSignature method, IType parameterType, IType sourceType, Tuple<ITypeCollection, ITypeCollection> methodGenericParameters, ITypeIdentityManager manager)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            if (method.IsGenericConstruct && method.IsGenericDefinition && parameterType.ContainsGenericParameters() &&
                !((genericParameters == null) || (genericParameters.Count == 0)))
            {
                /* *
                 * The unthinkable... GenericParameter-Parameter inferrence, 
                 * GREAT FUN.
                 * */
                return CompareAndContrast<TSignature, TSignatureParameter, TSignatureParent>(parameterType, sourceType, methodGenericParameters, method, deviations, manager);
            }
            return TypeToParamCheck<TSignature>(deviations, method, parameterType, sourceType, manager);
        }

        private static bool CompareAndContrast<TSignature, TSignatureParameter, TSignatureParent>(IType parameterType, IType sourceType, Tuple<ITypeCollection, ITypeCollection> methodGenericParameters, TSignature method, IDictionary<TSignature, int> deviations, ITypeIdentityManager manager)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            if (method.Parent is IGenericType)
            {
                var pParent = (IGenericType)method.Parent;
                if (pParent.IsGenericConstruct)
                    return TypeToParamCheck<TSignature>(deviations, method, parameterType.Disambiguify(pParent.GenericParameters, methodGenericParameters.Item2, TypeParameterSources.Both), sourceType, manager);
            }
            return TypeToParamCheck<TSignature>(deviations, method, parameterType.Disambiguify(null, methodGenericParameters.Item2, TypeParameterSources.Method), sourceType, manager);
        }

        private static bool TypeToParamCheck<TSignature>(IDictionary<TSignature, int> deviations, TSignature t, IType a, IType b, ITypeIdentityManager manager)
        {
            if (a.Equals(b))
                return true;
            else if (b.CanConvertTo(a))
            {
                //Increase how far it deviated.
                deviations[t]++;
                return true;
            }
            else if (manager != null && a == manager.ObtainTypeReference(TypeSystemSpecialIdentity.RootType))
            {
                deviations[t]++;
                return true;
            }
            else
                return false;
        }

        public static ISignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> FilterByName<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(this ISignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> criteria, string name)
            where TSignatureIdentifier :
                ISignatureMemberUniqueIdentifier,
                IGeneralMemberUniqueIdentifier
            where TSignature :
                ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParameter :
                ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return new FilteredSignatureMembers<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(criteria.Values.Filter(j => j.Name == name));
        }

        public static IFilteredSignatureMemberDictionary<IGeneralSignatureMemberUniqueIdentifier, IClassEventMember, IEventParameterMember<IClassEventMember, IClassType>, IClassType> FindInFamily(this IEventMemberDictionary<IClassEventMember, IClassType> target, IDelegateType searchCriteria)
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
            return new FilteredSignatureMembers<IGeneralSignatureMemberUniqueIdentifier, IClassEventMember, IEventParameterMember<IClassEventMember, IClassType>, IClassType>(result.ToArray());
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
            var generalIdentifier = AstIdentifier.Signature(eventName, searchCriteria.Parameters.ParameterTypes);
            for (IClassType current = target.Parent; current != null; current = current.BaseType)
                if (current.Events.Keys.Contains(generalIdentifier))
                {
                    var @event = current.Events[generalIdentifier];
                    if (@event.SignatureSource == EventSignatureSource.Delegate)
                    {
                        if (@event.SignatureType == searchCriteria)
                            return @event;
                    }
                    else if (searchCriteria.Parameters.ParameterTypes.SequenceEqual(@event.Parameters.ParameterTypes))
                        return @event;
                }
            return null;
        }
    }
}
