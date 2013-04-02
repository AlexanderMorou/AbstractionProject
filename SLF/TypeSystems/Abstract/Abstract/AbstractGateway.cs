/*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
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
        public static int CompareTo(this AccessLevelModifiers modifiers, AccessLevelModifiers compare)
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

        public static string GetFullName(this INamespaceDeclaration declaration)
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
                    result.Append(".");
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


    }
}
