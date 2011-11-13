using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Globalization;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public static partial class AstIdentifier
    {
        public static IGeneralGenericTypeUniqueIdentifier Type(string name, int typeParameters)
        {
            return new DefaultGenericTypeUniqueIdentifier(name, typeParameters);
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
        public static IGenericParameterUniqueIdentifier GenericParameter(int index, string name, bool onType = true)
        {
            return new DefaultGenericParameterUniqueIdentifier(index, name, onType);
        }

        public static IGenericParameterUniqueIdentifier GenericParameter(int index, bool onType = true)
        {
            return new DefaultGenericParameterUniqueIdentifier(index, onType);
        }

        public static IGenericParameterUniqueIdentifier GenericParameter(string name, bool onType = true)
        {
            return new DefaultGenericParameterUniqueIdentifier(name, onType);
        }

        /// <summary>
        /// Obtains a unique identifier for a type with the <paramref name="name"/>
        /// provided.
        /// </summary>
        /// <param name="name">The display name of the type which differentiates
        /// it from its siblings.</param>
        /// <returns>A <see cref="IGeneralTypeUniqueIdentifier"/>
        /// which represents the type.</returns>
        public static IGeneralTypeUniqueIdentifier Type(string name)
        {
            return new DefaultTypeUniqueIdentifier(name);
        }

        public static IGeneralSignatureMemberUniqueIdentifier Signature(string name, IEnumerable<IType> signature)
        {
            return new DefaultSignatureMemberUniqueIdentifier(name, signature);
        }

        public static IGeneralSignatureMemberUniqueIdentifier Signature(string name, params IType[] signature)
        {
            return Signature(name, (IEnumerable<IType>)signature);
        }

        public static IGeneralSignatureMemberUniqueIdentifier CtorSignature()
        {
            return Signature(".cctor");
        }

        public static IGeneralSignatureMemberUniqueIdentifier CtorSignature(params IType[] signature)
        {
            return Signature(".ctor", (IEnumerable<IType>)signature);
        }

        public static IGeneralSignatureMemberUniqueIdentifier CtorSignature(IEnumerable<IType> signature)
        {
            return Signature(".ctor", signature);
        }

        public static IGeneralMemberUniqueIdentifier Member(string name)
        {
            return new DefaultMemberUniqueIdentifier(name);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GenericSignature(string name, IEnumerable<IType> signature)
        {
            return GenericSignature(name, 0, signature);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GenericSignature(string name, params IType[] signature)
        {
            return GenericSignature(name, 0, (IEnumerable<IType>)signature);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GenericSignature(string name, int typeParams, IEnumerable<IType> signature)
        {
            return new DefaultGenericSignatureMemberUniqueIdentifier(name, typeParameters:typeParams, parameters: signature);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GenericSignature(string name, int typeParams, params IType[] signature)
        {
            return GenericSignature(name, typeParams, (IEnumerable<IType>)signature);
        }

        public static IBinaryOperatorUniqueIdentifier BinaryOperatorBoth(CoercibleBinaryOperators @operator)
        {
            return BinaryOperator(@operator);
        }

        internal static IBinaryOperatorUniqueIdentifier BinaryOperator(CoercibleBinaryOperators @operator, BinaryOpCoercionContainingSide containingSide = BinaryOpCoercionContainingSide.Both, IType otherSide = null)
        {
            return new DefaultBinaryOperatorUniqueIdentifier(@operator, containingSide, otherSide);
        }

        public static IBinaryOperatorUniqueIdentifier BinaryOperatorLeft(CoercibleBinaryOperators @operator, IType otherSide)
        {
            return BinaryOperator(@operator, BinaryOpCoercionContainingSide.LeftSide, otherSide);
        }

        public static IBinaryOperatorUniqueIdentifier BinaryOperatorRight(CoercibleBinaryOperators @operator, IType otherSide)
        {
            return BinaryOperator(@operator, BinaryOpCoercionContainingSide.RightSide, otherSide);
        }

        public static IUnaryOperatorUniqueIdentifier UnaryOperator(CoercibleUnaryOperators @operator)
        {
            return new DefaultUnaryOperatorUniqueIdentifier(@operator);
        }

        public static ITypeCoercionUniqueIdentifier TypeOperator(TypeConversionRequirement requirement, TypeConversionDirection direction, IType coercionType)
        {
            return new DefaultTypeCoercionUniqueIdentifier(requirement, direction, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier TypeOperatorTo(TypeConversionRequirement requirement, IType coercionType)
        {
            return TypeOperator(requirement, TypeConversionDirection.ToContainingType, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier TypeOperatorFrom(TypeConversionRequirement requirement, IType coercionType)
        {
            return TypeOperator(requirement, TypeConversionDirection.FromContainingType, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier TypeOperatorExplicit(TypeConversionDirection direction, IType coercionType)
        {
            return TypeOperator(TypeConversionRequirement.Explicit, direction, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier TypeOperatorImplicit(TypeConversionDirection direction, IType coercionType)
        {
            return TypeOperator(TypeConversionRequirement.Implicit, direction, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier TypeOperatorExplicitTo(IType coercionType)
        {
            return TypeOperator(TypeConversionRequirement.Explicit, TypeConversionDirection.ToContainingType, coercionType);
        }
        public static ITypeCoercionUniqueIdentifier TypeOperatorImplicitTo(IType coercionType)
        {
            return TypeOperator(TypeConversionRequirement.Implicit, TypeConversionDirection.ToContainingType, coercionType);
        }

        public static ITypeCoercionUniqueIdentifier TypeOperatorExplicitFrom(IType coercionType)
        {
            return TypeOperator(TypeConversionRequirement.Explicit, TypeConversionDirection.FromContainingType, coercionType);
        }
        public static ITypeCoercionUniqueIdentifier TypeOperatorImplicitFrom(IType coercionType)
        {
            return TypeOperator(TypeConversionRequirement.Implicit, TypeConversionDirection.FromContainingType, coercionType);
        }

        public static IAssemblyUniqueIdentifier Assembly(string name, Version assemblyVersion, ICultureIdentifier culture, byte[] publicKey = null)
        {
            if (publicKey != null && publicKey.Length != 8)
                throw new ArgumentOutOfRangeException("publicKey");
            return new DefaultAssemblyUniqueIdentifier(name, assemblyVersion, culture, publicKey);
        }

        public static IGeneralDeclarationUniqueIdentifier Declaration(string name)
        {
            return new DefaultGeneralDeclarationUniqueIdentifier(name);
        }

        public static IDelegateUniqueIdentifier Delegate(string name, int typeParameterCount, IEnumerable<IType> signature)
        {
            return new DefaultDelegateUniqueIdentifier(name, signature, typeParameterCount);
        }

        public static IDelegateUniqueIdentifier Delegate(string name, int typeParameterCount, params IType[] signature)
        {
            return Delegate(name, typeParameterCount, (IEnumerable<IType>)signature);
        }
    }
}
