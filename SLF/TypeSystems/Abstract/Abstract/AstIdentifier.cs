using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public static partial class TypeSystemIdentifiers
    {
        private static IMultikeyedDictionary<int, string, IGenericParameterUniqueIdentifier> TypeGenericParameterCache = new MultikeyedDictionary<int, string, IGenericParameterUniqueIdentifier>();
        private static IMultikeyedDictionary<int, string, IGenericParameterUniqueIdentifier> MemberGenericParameterCache = new MultikeyedDictionary<int, string, IGenericParameterUniqueIdentifier>();

        private static Dictionary<string, IGeneralTypeUniqueIdentifier> GeneralTypeCache = new Dictionary<string, IGeneralTypeUniqueIdentifier>();
        public static IGeneralSignatureMemberUniqueIdentifier GetSignatureIdentifier(string name, IEnumerable<IType> signature)
        {
            return new DefaultSignatureMemberUniqueIdentifier(name, signature);
        }

        /// <summary>
        /// Obtains a unique identifier for a generic parameter at the
        /// <paramref name="index"/> provided; when <paramref name="onType"/>
        /// is true, it indicates that the type-parameter is contained within
        /// a type.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value representing the ordinal
        /// index of the type-parameter relative to its siblings.</param>
        /// <param name="name">The <see cref="String"/> value denoting the display
        /// name of the generic parameter.</param>
        /// <param name="onType">Whether the generic-parameter exists on a type, if true
        /// the indexing of the generic-parameter is handled differently.</param>
        /// <returns>A <see cref="IGenericParameterUniqueIdentifier"/> which represents 
        /// the generic parameter described relative to local context.
        /// </returns>
        public static IGenericParameterUniqueIdentifier GetGenericParameterIdentifier(int index, string name, bool onType = true)
        {
            IMultikeyedDictionary<int, string, IGenericParameterUniqueIdentifier> cache;
            if (onType)
                cache = TypeGenericParameterCache;
            else
                cache = MemberGenericParameterCache;
            IGenericParameterUniqueIdentifier result;
            lock (cache)
                if (!cache.TryGetValue(index, name, out result))
                    cache.Add(index, name, result = new DefaultGenericParameterUniqueIdentifier(index, name, onType));
            return result;
        }

        public static IGenericParameterUniqueIdentifier GetGenericParameterIdentifier(int index, bool onType = true)
        {
            return new DefaultGenericParameterUniqueIdentifier(index, onType);
        }

        public static IGenericParameterUniqueIdentifier GetGenericParameterIdentifier(string name, bool onType = true)
        {
            return new DefaultGenericParameterUniqueIdentifier(name, onType);
        }

        public static IGeneralSignatureMemberUniqueIdentifier GetSignatureIdentifier(string name, params IType[] signature)
        {
            return new DefaultSignatureMemberUniqueIdentifier(name, signature);
        }

        public static IGeneralSignatureMemberUniqueIdentifier GetCtorSignatureIdentifier()
        {
            return GetSignatureIdentifier(".cctor");
        }

        public static IGeneralSignatureMemberUniqueIdentifier GetCtorSignatureIdentifier(params IType[] signature)
        {
            return GetSignatureIdentifier(".ctor", signature);
        }

        public static IGeneralSignatureMemberUniqueIdentifier GetCtorSignatureIdentifier(IEnumerable<IType> signature)
        {
            return GetSignatureIdentifier(".ctor", signature);
        }

        public static IGeneralMemberUniqueIdentifier GetMemberIdentifier(string name)
        {
            return new DefaultMemberUniqueIdentifier(name);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GetGenericSignatureIdentifier(string name, IEnumerable<IType> signature)
        {
            return GetGenericSignatureIdentifier(name, 0, signature);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GetGenericSignatureIdentifier(string name, params IType[] signature)
        {
            return GetGenericSignatureIdentifier(name, 0, signature);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GetGenericSignatureIdentifier(string name, int typeParams, IEnumerable<IType> signature)
        {
            return new DefaultGenericSignatureMemberUniqueIdentifier(name, typeParameters: typeParams, parameters: signature);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GetGenericSignatureIdentifier(string name, int typeParams, params IType[] signature)
        {
            return GetGenericSignatureIdentifier(name, typeParams, (IEnumerable<IType>)signature);
        }

        public static IBinaryOperatorUniqueIdentifier GetBinaryOperatorBothIdentifier(CoercibleBinaryOperators @operator)
        {
            return GetBinaryOperatorBothIdentifier(@operator);
        }

        internal static IBinaryOperatorUniqueIdentifier GetBinaryOperatorIdentifier(CoercibleBinaryOperators @operator, BinaryOpCoercionContainingSide containingSide = BinaryOpCoercionContainingSide.Both, IType otherSide = null)
        {
            return new DefaultBinaryOperatorUniqueIdentifier(@operator, containingSide, otherSide);
        }

        public static IBinaryOperatorUniqueIdentifier GetBinaryOperatorLeftIdentifier(CoercibleBinaryOperators @operator, IType otherSide)
        {
            return GetBinaryOperatorIdentifier(@operator, BinaryOpCoercionContainingSide.LeftSide, otherSide);
        }

        public static IBinaryOperatorUniqueIdentifier GetBinaryOperatorRightIdentifier(CoercibleBinaryOperators @operator, IType otherSide)
        {
            return GetBinaryOperatorIdentifier(@operator, BinaryOpCoercionContainingSide.RightSide, otherSide);
        }

        public static IUnaryOperatorUniqueIdentifier GetUnaryOperatorIdentifier(CoercibleUnaryOperators @operator)
        {
            return new DefaultUnaryOperatorUniqueIdentifier(@operator);
        }

        public static ITypeCoercionUniqueIdentifier GetTypeOperatorIdentifier(TypeConversionRequirement requirement, TypeConversionDirection direction, IType coercionType)
        {
            return new DefaultTypeCoercionUniqueIdentifier(requirement, direction, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier GetTypeOperatorToIdentifier(TypeConversionRequirement requirement, IType coercionType)
        {
            return GetTypeOperatorIdentifier(requirement, TypeConversionDirection.ToContainingType, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier GetTypeOperatorFromIdentifier(TypeConversionRequirement requirement, IType coercionType)
        {
            return GetTypeOperatorIdentifier(requirement, TypeConversionDirection.FromContainingType, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier GetTypeOperatorExplicitIdentifier(TypeConversionDirection direction, IType coercionType)
        {
            return GetTypeOperatorIdentifier(TypeConversionRequirement.Explicit, direction, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier GetTypeOperatorImplicitIdentifier(TypeConversionDirection direction, IType coercionType)
        {
            return GetTypeOperatorIdentifier(TypeConversionRequirement.Implicit, direction, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier GetTypeOperatorExplicitToIdentifier(IType coercionType)
        {
            return GetTypeOperatorIdentifier(TypeConversionRequirement.Explicit, TypeConversionDirection.ToContainingType, coercionType);
        }
        public static ITypeCoercionUniqueIdentifier GetTypeOperatorImplicitToIdentifier(IType coercionType)
        {
            return GetTypeOperatorIdentifier(TypeConversionRequirement.Implicit, TypeConversionDirection.ToContainingType, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier GetTypeOperatorExplicitFromIdentifier(IType coercionType)
        {
            return GetTypeOperatorIdentifier(TypeConversionRequirement.Explicit, TypeConversionDirection.FromContainingType, coercionType);
        }
        public static ITypeCoercionUniqueIdentifier GetTypeOperatorImplicitFromIdentifier(IType coercionType)
        {
            return GetTypeOperatorIdentifier(TypeConversionRequirement.Implicit, TypeConversionDirection.FromContainingType, coercionType);
        }

        public static IAssemblyUniqueIdentifier GetAssemblyIdentifier(string name, IVersion assemblyVersion, ICultureIdentifier culture, byte[] publicKey = null)
        {
            if (publicKey != null && publicKey.Length == 0)
                publicKey = null;
            if (publicKey != null && publicKey.Length != 8)
                throw new ArgumentOutOfRangeException("publicKey");
            return new DefaultAssemblyUniqueIdentifier(name, assemblyVersion, culture, publicKey);
        }

        public static IAssemblyUniqueIdentifier GetAssemblyIdentifier(string name, Version assemblyVersion, ICultureIdentifier culture, byte[] publicKey = null)
        {
            if (publicKey != null && publicKey.Length != 8 && publicKey.Length != 0)
                throw new ArgumentOutOfRangeException("publicKey");
            return new DefaultAssemblyUniqueIdentifier(name, new _Version(assemblyVersion), culture, publicKey == null ? null : publicKey.Length == 0 ? null : publicKey);
        }

        public static IGeneralDeclarationUniqueIdentifier GetDeclarationIdentifier(string name)
        {
            return new DefaultGeneralDeclarationUniqueIdentifier(name);
        }

        public static IVersion GetVersion(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return new _Version(major, minor, build, revision);
        }

        public static IGeneralTypeUniqueIdentifier GetTypeIdentifier(IGeneralDeclarationUniqueIdentifier @namespace, string name)
        {
            return new DefaultTypeUniqueIdentifier(name, assembly: null, @namespace: @namespace);
        }

        public static IGeneralTypeUniqueIdentifier GetTypeIdentifier(string @namespace, string name)
        {
            if (string.IsNullOrEmpty(@namespace))
                return GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier)null, name);
            else
                return GetTypeIdentifier(TypeSystemIdentifiers.GetDeclarationIdentifier(@namespace), name);
        }

        public static IGeneralGenericTypeUniqueIdentifier GetTypeIdentifier(IGeneralDeclarationUniqueIdentifier @namespace, string name, int typeParameters)
        {
            return new DefaultGenericTypeUniqueIdentifier(name, typeParameters, assembly: null, @namespace: @namespace);
        }

        public static IGeneralGenericTypeUniqueIdentifier GetTypeIdentifier(string @namespace, string name, int typeParameters)
        {
            if (string.IsNullOrEmpty(@namespace))
                return GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier)null, name, typeParameters);
            else
                return GetTypeIdentifier(TypeSystemIdentifiers.GetDeclarationIdentifier(@namespace), name, typeParameters);
        }

    }
}
