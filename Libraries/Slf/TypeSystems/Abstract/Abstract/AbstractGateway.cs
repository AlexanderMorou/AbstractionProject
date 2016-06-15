/*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities;
using System.Text;
using System.IO;
using AllenCopeland.Abstraction.Utilities.Security;
using AllenCopeland.Abstraction._Internal.Utilities.Security;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides common helper methods for interacting with
    /// the abstract type system.
    /// </summary>
    public static partial class AbstractGateway
    {
        internal static Guid MetadatumMarshalServiceGuid = new Guid(0xe701b831, 0x5a52, 0x47c1, 0xa0, 0xf5, 0xa9, 0xb8, 0xb2, 0x7e, 0x55, 0xd3);
        /// <summary>
        /// Determines how the <paramref name="modifiers"/> <paramref name="compare"/> to the
        /// second set of <see cref="AccessLevelModifiers"/> provided.
        /// </summary>
        /// <param name="modifiers">The <see cref="AccessLevelModifiers"/> which denote
        /// the left side of the comparison.</param>
        /// <param name="compare">The <see cref="AccessLevelModifiers"/> which denote the right
        /// side of the comparison.</param>
        /// <returns>-1 if <paramref name="modifiers"/> is less than
        /// the second <see cref="AccessLevelModifiers"/> 
        /// <paramref name="compare"/>d; 1 if the <see cref="AccessLevelModifiers"/> 
        /// to <paramref name="compare"/> to is less than the <paramref name="modifiers"/>
        /// provided; and '0' if they are equal.</returns>
        public static int CompareToEx(this AccessLevelModifiers modifiers, AccessLevelModifiers compare)
        {
            switch (modifiers)
            {
                case AccessLevelModifiers.ProtectedAndInternal:
                    if (compare == AccessLevelModifiers.Private || compare == AccessLevelModifiers.PrivateScope)
                        return 1;
                    else if (compare == AccessLevelModifiers.ProtectedAndInternal)
                        return 0;
                    else
                        return -1;
                case AccessLevelModifiers.Internal:
                    switch (compare)
                    {
                        case AccessLevelModifiers.ProtectedAndInternal:
                        case AccessLevelModifiers.Private:
                        case AccessLevelModifiers.PrivateScope:
                            return 1;
                        case AccessLevelModifiers.Protected:
                        case AccessLevelModifiers.ProtectedOrInternal:
                        case AccessLevelModifiers.Public:
                            return -1;
                        case AccessLevelModifiers.Internal:
                        default:
                            return 0;
                    }
                case AccessLevelModifiers.Private:
                    if (compare == AccessLevelModifiers.PrivateScope)
                        return 1;
                    else if (compare == AccessLevelModifiers.Private)
                        return 0;
                    else
                        return -1;
                case AccessLevelModifiers.Protected:
                    switch (compare)
                    {
                        case AccessLevelModifiers.ProtectedAndInternal:
                        case AccessLevelModifiers.PrivateScope:
                        case AccessLevelModifiers.Internal:
                        case AccessLevelModifiers.Private:
                            return 1;
                        case AccessLevelModifiers.ProtectedOrInternal:
                        case AccessLevelModifiers.Public:
                            return -1;
                        case AccessLevelModifiers.Protected:
                        default:
                            return 0;
                    }
                case AccessLevelModifiers.ProtectedOrInternal:
                    if (compare == AccessLevelModifiers.Public)
                        return -1;
                    else if (compare == AccessLevelModifiers.ProtectedOrInternal)
                        return 0;
                    else
                        return 1;
                case AccessLevelModifiers.Public:
                    if (compare == AccessLevelModifiers.Public)
                        return 0;
                    else
                        return 1;
                default:
                    return 0;
            }
        }

        public static bool StandardIsDefined(this IMetadataEntity target, IType metadatumType)
        {
            foreach (IMetadatum inst in target.Metadata)
                if (metadatumType.IsAssignableFrom(inst.Type))
                    return true;
            return false;
        }

        public static TypedName WithName(this IType target, string name)
        {
            return new TypedName(name, target);
        }

        public static IMethodParameterMember<TMethod, TMethodParent> ParamOf<TMethod, TMethodParent>(this string name, IMethodMember<TMethod, TMethodParent> method)
            where TMethod :
                IMethodMember<TMethod, TMethodParent>
            where TMethodParent :
                IMethodParent<TMethod, TMethodParent>
        {
            IMethodParameterMember<TMethod, TMethodParent> result;
            method.Parameters.TryGetValue(TypeSystemIdentifiers.GetMemberIdentifier(name), out result);
            return result;
        }

        public static IConstructorParameterMember<TCtor, TType> ParamOf<TCtor, TType>(this string name, IConstructorMember<TCtor, TType> ctor)
            where TCtor :
                IConstructorMember<TCtor, TType>
            where TType :
                ICreatableParent<TCtor, TType>
        {
            IConstructorParameterMember<TCtor, TType> result;
            ctor.Parameters.TryGetValue(TypeSystemIdentifiers.GetMemberIdentifier(name), out result);
            return result;
        }

        public static IIndexerParameterMember<TIndexer, TIndexerParent> ParamOf<TIndexer, TIndexerParent>(this string name, IIndexerMember<TIndexer, TIndexerParent> indexer)
            where TIndexer :
                IIndexerMember<TIndexer, TIndexerParent>
            where TIndexerParent :
                IIndexerParent<TIndexer, TIndexerParent>
        {
            IIndexerParameterMember<TIndexer, TIndexerParent> result;
            indexer.Parameters.TryGetValue(TypeSystemIdentifiers.GetMemberIdentifier(name), out result);
            return result;
        }

        public static IIndexerSignatureParameterMember<TIndexer, TIndexerParent> ParamOf<TIndexer, TIndexerParent>(this string name, IIndexerSignatureMember<TIndexer, TIndexerParent> indexer)
            where TIndexer :
                IIndexerSignatureMember<TIndexer, TIndexerParent>
            where TIndexerParent :
                IIndexerSignatureParent<TIndexer, TIndexerParent>
        {
            IIndexerSignatureParameterMember<TIndexer, TIndexerParent> result;
            indexer.Parameters.TryGetValue(TypeSystemIdentifiers.GetMemberIdentifier(name), out result);
            return result;
        }

        public static TField FieldOf<TField, TFieldParent>(this string name, IFieldParent<TField, TFieldParent> fieldParent)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>
        {
            TField result;
            fieldParent.Fields.TryGetValue(TypeSystemIdentifiers.GetMemberIdentifier(name), out result);
            return result;
        }

        public static IStrongNamePrivateKeyInfo GetStrongNameKeyPair(string filename)
        {
            return (IStrongNamePrivateKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(filename);
        }
        public static IStrongNamePublicKeyInfo GetStrongNamePublicKey(string filename)
        {
            return (IStrongNamePublicKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(filename, false);
        }
        public static IStrongNamePrivateKeyInfo GetStrongNameKeyPair(byte[] data)
        {
            return (IStrongNamePrivateKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(data);
        }

        public static IStrongNamePublicKeyInfo GetStrongNamePublicKey(byte[] data)
        {
            return (IStrongNamePublicKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(data, false);
        }

        public static IStrongNamePublicKeyInfo GetStrongNamePublicKey(Stream data)
        {
            return (IStrongNamePublicKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(data, false);
        }

        public static IStrongNamePrivateKeyInfo GetStrongNameKeyPair(Stream data)
        {
            return (IStrongNamePrivateKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(data);
        }
        
        public static string GetFullName(this INamespaceDeclaration declaration, string termSeparator = ".")
        {
            Stack<INamespaceDeclaration> hierarchy = new Stack<INamespaceDeclaration>();
            INamespaceDeclaration current = declaration;
            while (current != null)
            {
                if (hierarchy.Contains(current))
                    throw new InvalidOperationException("Circular namespace hierarchy.");
                hierarchy.Push(current);
                current = current.Parent as INamespaceDeclaration;
            }
            StringBuilder result = new StringBuilder();
            bool first = true;
            while (hierarchy.Count > 0)
            {
                if (first)
                    first = false;
                else
                    result.Append(termSeparator);
                result.Append(hierarchy.Pop().Name);
            }
            return result.ToString();
        }

        public static bool IsAtLeast(AccessLevelModifiers source, AccessLevelModifiers target)
        {
            switch (source)
            {
                case AccessLevelModifiers.ProtectedAndInternal:
                    switch (target)
                    {
                        case AccessLevelModifiers.ProtectedAndInternal:
                        case AccessLevelModifiers.Private:
                        case AccessLevelModifiers.PrivateScope:
                            return true;
                        default:
                            return false;
                    }
                case AccessLevelModifiers.Internal:
                    switch (target)
                    {
                        case AccessLevelModifiers.ProtectedAndInternal:
                        case AccessLevelModifiers.Internal:
                        case AccessLevelModifiers.Private:
                        case AccessLevelModifiers.PrivateScope:
                        case AccessLevelModifiers.ProtectedOrInternal:
                            return true;
                        default:
                            return false;
                    }
                case AccessLevelModifiers.Private:
                    switch (target)
                    {
                        case AccessLevelModifiers.Private:
                        case AccessLevelModifiers.PrivateScope:
                            return true;
                        default:
                            return false;
                    }
                case AccessLevelModifiers.PrivateScope:
                    switch (target)
                    {
                        case AccessLevelModifiers.PrivateScope:
                            return true;
                        default:
                            return false;
                    }
                case AccessLevelModifiers.Public:
                    return true;
                case AccessLevelModifiers.Protected:
                    switch (target)
                    {
                        case AccessLevelModifiers.Public:
                        case AccessLevelModifiers.Internal:
                            return false;
                        default:
                            return true;
                    }
                case AccessLevelModifiers.ProtectedOrInternal:
                    switch (target)
                    {
                        case AccessLevelModifiers.Public:
                            return false;
                        default:
                            return true;
                    }
            }
            return false;
        }

        public static IEnumerable<TSignature> Filter<TSignatureParameter, TSignature, TSignatureParent>(this IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent> target, string name)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return from m in target.Values
                   where m.Name == name
                   select m;
        }

        public static IEnumerable<IClassMethodMember> Filter(this IMethodSignatureMemberDictionary<IMethodParameterMember<IClassMethodMember, IClassType>, IClassMethodMember, IClassType> target, AccessLevelModifiers lowestAccessLevel)
        {
            return from m in target.Values
                   where IsAtLeast(m.AccessLevel, lowestAccessLevel)
                   select m;
        }

        public static IEnumerable<IClassMethodMember> Filter(this IMethodSignatureMemberDictionary<IMethodParameterMember<IClassMethodMember, IClassType>, IClassMethodMember, IClassType> target, string name, AccessLevelModifiers lowestAccessLevel = AccessLevelModifiers.Public)
        {
            return from m in target.Values
                   where m.Name == name
                   where IsAtLeast(m.AccessLevel, lowestAccessLevel)
                   select m;
        }

        public static IEnumerable<IClassMethodMember> GetAvailableMethodsFor(this IClassType targetClass, string name, AccessLevelModifiers lowestAccessLevel = AccessLevelModifiers.Public)
        {
            return targetClass.GetAvailableMethodsFor(m => m.Name == name && IsAtLeast(m.AccessLevel, lowestAccessLevel));
        }

        public static IEnumerable<IClassMethodMember> GetAvailableMethodsFor(this IClassType targetClass, AccessLevelModifiers lowestAccessLevel, Predicate<IClassMethodMember> predicate = null)
        {
            return targetClass.GetAvailableMethodsFor(predicate.And(k => IsAtLeast(k.AccessLevel, lowestAccessLevel)));
        }

        public static IEnumerable<IClassMethodMember> GetAvailableMethodsFor(this IClassType targetClass, Predicate<IClassMethodMember> predicate)
        {
            IClassType current = targetClass;
            var currentYield = new Dictionary<IGenericSignatureMemberUniqueIdentifier, IMethodMember>();
        CheckAgain:
            foreach (var method in current.Methods.Values)
                if (predicate(method))
                    if (!currentYield.ContainsKey(method.UniqueIdentifier))
                    {
                        currentYield.Add(method.UniqueIdentifier, method);
                        yield return method;
                    }
            if (current.BaseType == null)
                yield break;
            else
            {
                current = current.BaseType;
                goto CheckAgain;
            }
        }

        public static IEnumerable<IMember> GetAvailableMembersFor(this IClassType targetClass, AccessLevelModifiers lowestAccessLevel, Predicate<IMember> predicate = null)
        {
            return targetClass.GetAvailableMembersFor(predicate.And(k => 
            {
                if (k is IScopedDeclaration)
                {
                    var scopedMember = (IScopedDeclaration)k;
                    return IsAtLeast(scopedMember.AccessLevel, lowestAccessLevel);
                }
                return false;
            }));
        }

        public static IEnumerable<IMember> GetAvailableMembersFor(this IClassType targetClass, string name, AccessLevelModifiers lowestAccessLevel)
        {
            return targetClass.GetAvailableMembersFor(k =>
            {
                if (k.Name != name)
                    return false;
                if (k is IScopedDeclaration)
                {
                    var scopedMember = (IScopedDeclaration)k;
                    return IsAtLeast(scopedMember.AccessLevel, lowestAccessLevel);
                }
                return false;
            });
        }
        public static IEnumerable<IMember> GetAvailableMembersFor(this IClassType targetClass, Predicate<IMember> predicate)
        {
            IClassType current = targetClass;
            var currentYield = new Dictionary<IMemberUniqueIdentifier, IMember>();
        CheckAgain:
            foreach (var memberEntry in current.Members.Values)
            {
                var member = memberEntry.Entry;
                if (predicate(member))
                    if (!currentYield.ContainsKey(member.UniqueIdentifier))
                    {
                        currentYield.Add(member.UniqueIdentifier, member);
                        yield return member;
                    }
            }
            if (current.BaseType == null)
                yield break;
            else
            {
                current = current.BaseType;
                goto CheckAgain;
            }
        }


        public static IClassMethodMember ObtainBaseDefinition(this IClassMethodMember source)
        {
            return ObtainPreviousDefinition(source, true);
        }

        public static IClassMethodMember ObtainPreviousDefinition(IClassMethodMember source, bool recurse = false)
        {

            if (!source.IsOverride)
                throw new InvalidOperationException();
            for (IClassType p = source.Parent.BaseType; p != null; p = p.BaseType)
                foreach (var methodMember in p.Methods.Values)
                    if (methodMember.Name != source.Name ||
                        methodMember.Parameters.Count != source.Parameters.Count ||
                        (methodMember.IsGenericConstruct && source.IsGenericConstruct &&
                         methodMember.TypeParameters.Count != source.TypeParameters.Count))
                        continue;
                    else
                    {
                        bool match = true;
                        /* *
                         * For the sake of source find operation,
                         * the type-parameters will be mostly ignored to 
                         * ensure that the base declaration is found.
                         * *
                         * If the signature matches, it's a valid override;
                         * however, if the constraints upon the type-parameter
                         * don't match, then that's the compiler's domain to
                         * notify.
                         * */
                        for (int i = 0; i < source.Parameters.Count; i++)
                        {
                            /* *
                             * Var variable declaration is helpful for 
                             * cases like source.
                             * */
                            var targetParam = methodMember.Parameters.Values[i];
                            var sourceParam = source.Parameters.Values[i];
                            if (targetParam.Direction != sourceParam.Direction)
                            {
                                match = false;
                                break;
                            }
                            else if (targetParam.ParameterType.IsGenericTypeParameter)
                            {
                                /* *
                                 * Rewrite source code so that when the source parameter
                                 * is a generic parameter, and its parent is the enclosing
                                 * type, that the newly declared version is equal to the
                                 * generic parameter defined in the inheritance chain.
                                 * */
                                if (!sourceParam.ParameterType.IsGenericTypeParameter)
                                {
                                    match = false;
                                    break;
                                }

                                IGenericParameter sourceTParam = (IGenericParameter)sourceParam.ParameterType,
                                                  targetTParam = (IGenericParameter)targetParam.ParameterType;
                                if (targetTParam.Parent == methodMember)
                                {
                                    if (sourceTParam.Parent != source)
                                    {
                                        match = false;
                                        break;
                                    }
                                    match = sourceTParam.Position == targetTParam.Position;
                                }
                                else if (targetTParam.Parent == source.Parent)
                                {
                                    if (sourceTParam.Parent != source.Parent)
                                    {
                                        match = false;
                                        break;
                                    }
                                    match = sourceTParam.Position == targetTParam.Position;
                                }
                            }
                            else
                                match = targetParam.ParameterType.Equals(sourceParam.ParameterType);
                            if (!match)
                                break;
                        }
                        if (match)
                        {
                            if (methodMember.ReturnType == source.ReturnType)
                            {
                                if (recurse && methodMember.IsOverride)
                                    return methodMember.BaseDefinition;
                                else
                                    return methodMember;
                            }
                        }
                    }
            throw new InvalidOperationException("match not found");
        }
        public static MetadatumTypedParameter<T> GetIndexedParameter<T>(this IEnumerable<MetadatumTypedParameter> series, int index)
        {
            if (series.Count() <= index)
                return MetadatumTypedParameter<T>.Empty;
            var element = series.Skip(index).FirstOrDefault();
            return MetadatumTypedParameter.ToKnownTypedParameter<T>(element);
        }
        public static MetadatumNamedParameter<T> GetNamedParameter<T>(this IEnumerable<MetadatumNamedParameter> series, string name)
        {
            var element = series.Where(k => k.MemberName == name && k.Value is T).FirstOrDefault();
            if (element.IsEmpty)
                return MetadatumNamedParameter<T>.Empty;
            return MetadatumNamedParameter.ToKnownNamedParameter<T>(element);
        }

    }
}
