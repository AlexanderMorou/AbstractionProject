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
            throw new NotImplementedException();
        }

        public static IGenericParameterUniqueIdentifier Type(int index)
        {
            throw new NotImplementedException();
        }

        public static IGeneralTypeUniqueIdentifier Type(string name)
        {
            throw new NotImplementedException();
        }

        public static IGeneralSignatureMemberUniqueIdentifier Signature(string name, IEnumerable<IType> signature)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GenericSignature(string name, IEnumerable<IType> signature)
        {
            return GenericSignature(name, 0, signature);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GenericSignature(string name, IType[] signature)
        {
            return GenericSignature(name, 0, (IEnumerable<IType>)signature);
        }

        public static IGeneralGenericSignatureMemberUniqueIdentifier GenericSignature(string name, int typeParams, IEnumerable<IType> signature)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static ITypeCoercionUniqueIdentifier TypeOperator(TypeConversionRequirement requirement, TypeConversionDirection direction, IType coercionType)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static IGeneralDeclarationUniqueIdentifier Declaration(string name)
        {
            return new DefaultGeneralDeclarationUniqueIdentifier(name);
        }

        public static IDelegateUniqueIdentifier Delegate(string name, int typeParameterCount, IEnumerable<IType> signature)
        {
            throw new NotImplementedException();
        }

        public static IDelegateUniqueIdentifier Delegate(string name, int typeParameterCount, params IType[] signature)
        {
            return Delegate(name, typeParameterCount, (IEnumerable<IType>)signature);
        }
    }
}
