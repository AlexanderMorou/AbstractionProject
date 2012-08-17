/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides common helper methods for interacting with
    /// the abstract type system.
    /// </summary>
    public static partial class AbstractGateway
    {
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
                    if (compare == AccessLevelModifiers.Private)
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
                    if (compare == AccessLevelModifiers.Private)
                        return 0;
                    else
                        return -1;
                case AccessLevelModifiers.Protected:
                    switch (compare)
                    {
                        case AccessLevelModifiers.ProtectedAndInternal:
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
            return (IStrongNamePrivateKeyInfo) StrongNameKeyPairHelper.LoadStrongNameKeyData(filename);
        }
        public static IStrongNamePublicKeyInfo GetStrongNamePublicKey(string filename)
        {
            return (IStrongNamePublicKeyInfo) StrongNameKeyPairHelper.LoadStrongNameKeyData(filename, false);
        }
        public static IStrongNamePrivateKeyInfo GetStrongNameKeyPair(byte[] data)
        {
            return (IStrongNamePrivateKeyInfo) StrongNameKeyPairHelper.LoadStrongNameKeyData(data);
        }

        public static IStrongNamePublicKeyInfo GetStrongNamePublicKey(byte[] data)
        {
            return (IStrongNamePublicKeyInfo) StrongNameKeyPairHelper.LoadStrongNameKeyData(data, false);
        }
    }
}
