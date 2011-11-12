using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    partial class AstIdentifier
    {
        private class DefaultGeneralDeclarationUniqueIdentifier :
            IGeneralDeclarationUniqueIdentifier
        {

            #region IDeclarationUniqueIdentifier<IGeneralDeclarationUniqueIdentifier> Members

            public string Name { get; private set; }
            #endregion

            public DefaultGeneralDeclarationUniqueIdentifier(string name)
            {
                this.Name = name;
            }

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGeneralMemberUniqueIdentifier || 
                    other is IGeneralTypeUniqueIdentifier ||
                    other is IAssemblyUniqueIdentifier)
                    return other.Equals(this);
                return other.Name == this.Name;
            }
            public override bool Equals(object obj)
            {
                if (obj is IGeneralDeclarationUniqueIdentifier)
                    return this.Equals((IGeneralDeclarationUniqueIdentifier)obj);
                return false;
            }
            #endregion
            public override int GetHashCode()
            {
                if (this.Name == null)
                    return -101;
                return this.Name.GetHashCode();
            }
            public override string ToString()
            {
                return this.Name;
            }
        }

        private class DefaultGenericTypeUniqueIdentifier :
            IGeneralGenericTypeUniqueIdentifier
        {

            public DefaultGenericTypeUniqueIdentifier(string name) :
                this(name, 0)
            {
            }

            public DefaultGenericTypeUniqueIdentifier(string name, int typeParameters)
            {
                this.Name = name;
                this.TypeParameters = typeParameters;
            }

            #region IDeclarationUniqueIdentifier Members

            public string Name { get; private set; }

            #endregion

            #region IEquatable<IGeneralGenericTypeUniqueIdentifier> Members

            public bool Equals(IGeneralGenericTypeUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                return other.TypeParameters == this.TypeParameters && other.Name == this.Name;
            }

            #endregion

            #region IGenericParamParentUniqueIdentifier<IGeneralGenericTypeUniqueIdentifier> Members

            public bool IsGenericConstruct
            {
                get { return this.TypeParameters > 0; }
            }

            public int TypeParameters { get; private set; }
            #endregion

            #region IEquatable<IGeneralTypeUniqueIdentifier> Members

            public bool Equals(IGeneralTypeUniqueIdentifier other)
            {
                if (other is IGeneralGenericTypeUniqueIdentifier)
                    return this.Equals((IGeneralGenericTypeUniqueIdentifier)other);
                return false;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGeneralGenericTypeUniqueIdentifier)
                    return this.Equals((IGeneralGenericTypeUniqueIdentifier)other);
                return false;
            }

            #endregion
            public override bool Equals(object obj)
            {
                if (obj is IGeneralGenericTypeUniqueIdentifier)
                    return this.Equals((IGeneralGenericTypeUniqueIdentifier)obj);
                return false;
            }
            public override int GetHashCode()
            {
                if (this.Name == null)
                    return -33 ^ this.TypeParameters;
                else
                    return this.Name.GetHashCode() ^ this.TypeParameters;
            }

            public override string ToString()
            {
                if (this.Name == null)
                    if (this.IsGenericConstruct)
                        return string.Format("<unknown>`{0}", this.TypeParameters);
                    else
                        return "<unknown>";
                else if (this.IsGenericConstruct)
                    return string.Format("{0}`{1}", this.Name, this.TypeParameters);
                else
                    return this.Name;
            }
        }

        private class DefaultTypeUniqueIdentifier : 
            IGeneralTypeUniqueIdentifier
        {
            #region IDeclarationUniqueIdentifier Members

            public string Name { get; private set; }

            #endregion

            #region IEquatable<IGeneralTypeUniqueIdentifier> Members

            public bool Equals(IGeneralTypeUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                return other.Name == this.Name;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other != null && other is IGeneralTypeUniqueIdentifier)
                    return this.Equals((IGeneralTypeUniqueIdentifier)other);
                return false;
            }

            #endregion

            public override bool Equals(object obj)
            {
                if (obj != null && obj is IGeneralTypeUniqueIdentifier)
                    return this.Equals((IGeneralTypeUniqueIdentifier)obj);
                return false;
            }

            public DefaultTypeUniqueIdentifier(string name)
            {
                this.Name = name;
            }
            public override int GetHashCode()
            {
                if (this.Name == null)
                    return -7;
                return this.Name.GetHashCode();
            }
            public override string ToString()
            {
                return this.Name;
            }
        }

        private class DefaultMemberUniqueIdentifier :
            IGeneralMemberUniqueIdentifier
        {

            #region IDeclarationUniqueIdentifier Members

            public string Name { get; private set; }

            #endregion

            #region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                return other.Name == this.Name;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other != null || other is IGeneralMemberUniqueIdentifier)
                    return this.Equals((IGeneralMemberUniqueIdentifier)other);
                return false;
            }

            #endregion

            public DefaultMemberUniqueIdentifier(string name)
            {
                this.Name = name;
            }
            public override int GetHashCode()
            {
                if (this.Name == null)
                    return -5;
                return this.Name.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj != null || obj is IGeneralMemberUniqueIdentifier)
                    return this.Equals((IGeneralMemberUniqueIdentifier)obj);
                return false;
            }

            public override string ToString()
            {
                return this.Name;
            }
        }

        private class DefaultGenericParameterUniqueIdentifier :
            IGenericParameterUniqueIdentifier
        {

            #region IGenericParameterUniqueIdentifier Members

            public int? Position { get; private set; }

            public bool IsTypeParameter { get; private set; }

            #endregion

            public DefaultGenericParameterUniqueIdentifier(string name, bool onType)
            {
                this.Name = name;
                this.IsTypeParameter = onType;
            }

            public DefaultGenericParameterUniqueIdentifier(int index, string name, bool onType)
            {
                this.Position = index;
                this.Name = name;
                this.IsTypeParameter = onType;
            }

            public DefaultGenericParameterUniqueIdentifier(int index, bool onType)
            {
                this.Position = index;
                this.IsTypeParameter = onType;
            }

            #region IDeclarationUniqueIdentifier Members
            private string name;
            public string Name
            {
                get
                {
                    if (this.name == null)
                        if (this.Position == null)
                            if (this.IsTypeParameter)
                                return "!T<unknown>";
                            else
                                return "!!T<unknown>";
                        else
                            if (this.IsTypeParameter)
                                return string.Format("!T{0}", this.Position);
                            else
                                return string.Format("!!T{0}", this.Position);
                    else
                        return this.name;
                }
                set
                {
                    this.name = value;
                }
            }

            #endregion

            #region IEquatable<IGenericParameterUniqueIdentifier> Members

            public bool Equals(IGenericParameterUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                if (other.Position != null && (other.Position == this.Position))
                    return other.IsTypeParameter == this.IsTypeParameter;
                return other.Name == this.Name && other.IsTypeParameter == this.IsTypeParameter;
            }

            #endregion

            #region IEquatable<IGeneralTypeUniqueIdentifier> Members

            public bool Equals(IGeneralTypeUniqueIdentifier other)
            {
                if (other is IGenericParameterUniqueIdentifier)
                    return this.Equals((IGenericParameterUniqueIdentifier)other);
                return false;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGenericParameterUniqueIdentifier)
                    return this.Equals((IGenericParameterUniqueIdentifier)other);
                return false;
            }

            #endregion

            #region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IGenericParameterUniqueIdentifier)
                    return this.Equals((IGenericParameterUniqueIdentifier)other);
                return false;
            }

            public override bool Equals(object obj)
            {
                if (obj is IGenericParameterUniqueIdentifier)
                    return this.Equals((IGenericParameterUniqueIdentifier)obj);
                return false;
            }
            #endregion
            public override int GetHashCode()
            {
                return this.Name.GetHashCode() ^ this.IsTypeParameter.GetHashCode();
            }
            public override string ToString()
            {
                return this.Name;
            }
        }

        private class DefaultBinaryOperatorUniqueIdentifier :
            IBinaryOperatorUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        {
            /// <summary>
            /// Returns the <see cref="CoercibleBinaryOperators"/> coerced
            /// by the <see cref="IBinaryOperatorUniqueIdentifier"/>.
            /// </summary>
            public CoercibleBinaryOperators Operator { get; private set; }

            /// <summary>
            /// Returns which side the required self reference
            /// the <see cref="IBinaryOperatorUniqueIdentifier"/>'s
            /// parent is on.
            /// </summary>
            public BinaryOpCoercionContainingSide ContainingSide { get; private set; }

            /// <summary>
            /// Returns the type of the other side of the expression
            /// used when performing the coercion.
            /// </summary>
            /// <remarks>If <see cref="ContainingSide"/>
            /// is <see cref="BinaryOpCoercionContainingSide.Both"/>
            /// <see cref="OtherSide"/> returns null.</remarks>
            public IType OtherSide { get; private set; }

            public DefaultBinaryOperatorUniqueIdentifier(CoercibleBinaryOperators @operator, BinaryOpCoercionContainingSide containingSide, IType otherSide)
            {
                this.Operator = @operator;
                this.ContainingSide = containingSide;
                this.OtherSide = otherSide;
            }

            public bool Equals(IBinaryOperatorUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                switch (this.ContainingSide)
                {
                    case BinaryOpCoercionContainingSide.LeftSide:
                    case BinaryOpCoercionContainingSide.RightSide:
                        return other.Operator == this.Operator &&
                               other.ContainingSide == this.ContainingSide;
                    case BinaryOpCoercionContainingSide.Both:
                        return other.Operator == this.Operator &&
                            other.ContainingSide == BinaryOpCoercionContainingSide.Both;
                    default:
                        return false;
                }
            }

            #region IDeclarationUniqueIdentifier<IBinaryOperatorUniqueIdentifier> Members

            public string Name
            {
                get {
                    switch (this.Operator)
                    {
                        case CoercibleBinaryOperators.Add:
                            return CLICommon.BinaryOperatorNames.Addition;
                        case CoercibleBinaryOperators.Subtract:
                            return CLICommon.BinaryOperatorNames.Subtraction;
                        case CoercibleBinaryOperators.Multiply:
                            return CLICommon.BinaryOperatorNames.Multiply;
                        case CoercibleBinaryOperators.Divide:
                            return CLICommon.BinaryOperatorNames.Division;
                        case CoercibleBinaryOperators.Modulus:
                            return CLICommon.BinaryOperatorNames.Modulus;
                        case CoercibleBinaryOperators.BitwiseAnd:
                            return CLICommon.BinaryOperatorNames.BitwiseAnd;
                        case CoercibleBinaryOperators.BitwiseOr:
                            return CLICommon.BinaryOperatorNames.BitwiseOr;
                        case CoercibleBinaryOperators.ExclusiveOr:
                            return CLICommon.BinaryOperatorNames.ExclusiveOr;
                        case CoercibleBinaryOperators.LeftShift:
                            return CLICommon.BinaryOperatorNames.LeftShift;
                        case CoercibleBinaryOperators.RightShift:
                            return CLICommon.BinaryOperatorNames.RightShift;
                        case CoercibleBinaryOperators.IsEqualTo:
                            return CLICommon.BinaryOperatorNames.Equality;
                        case CoercibleBinaryOperators.IsNotEqualTo:
                            return CLICommon.BinaryOperatorNames.Inequality;
                        case CoercibleBinaryOperators.LessThan:
                            return CLICommon.BinaryOperatorNames.LessThan;
                        case CoercibleBinaryOperators.GreaterThan:
                            return CLICommon.BinaryOperatorNames.GreaterThan;
                        case CoercibleBinaryOperators.LessThanOrEqualTo:
                            return CLICommon.BinaryOperatorNames.LessThanOrEqual;
                        case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                            return CLICommon.BinaryOperatorNames.GreaterThanOrEqual;
                        default:
                            return null;
                    }
                }
            }

            #endregion

            #region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IBinaryOperatorUniqueIdentifier)
                    return this.Equals(((IBinaryOperatorUniqueIdentifier)other));
                return false;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IBinaryOperatorUniqueIdentifier)
                    return this.Equals(((IBinaryOperatorUniqueIdentifier)other));
                return false;
            }

            #endregion

            public override bool Equals(object obj)
            {
                if (obj is IBinaryOperatorUniqueIdentifier)
                    return this.Equals(((IBinaryOperatorUniqueIdentifier)obj));
                return false;
            }

            public override int GetHashCode()
            {
                switch (this.ContainingSide)
                {
                    case BinaryOpCoercionContainingSide.LeftSide:
                        if (this.OtherSide != null)
                            return this.OtherSide.GetHashCode() ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                        else
                            return -3 ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                    case BinaryOpCoercionContainingSide.RightSide:
                        if (this.OtherSide != null)
                            return this.OtherSide.GetHashCode() ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                        else
                            return -2 ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                    case BinaryOpCoercionContainingSide.Both:
                        return -4 ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                    default:
                        return -1 ^ this.ContainingSide.GetHashCode();
                }
            }

            public override string ToString()
            {
                switch (this.ContainingSide)
                {
                    case BinaryOpCoercionContainingSide.LeftSide:
                        return string.Format("{0}(<originType>, {1})", this.Name, this.OtherSide);
                    case BinaryOpCoercionContainingSide.RightSide:
                        return string.Format("{0}({1}, <originType>)", this.Name, this.OtherSide);
                    case BinaryOpCoercionContainingSide.Both:
                        return string.Format("{0}(<originType>, <originType>)", this.Name);                        
                    default:
                        return string.Format("{0}(<unknown>, <unknown>)", this.Name);
                }
            }
        }

        private class DefaultUnaryOperatorUniqueIdentifier :
            IUnaryOperatorUniqueIdentifier
        {

            public DefaultUnaryOperatorUniqueIdentifier(CoercibleUnaryOperators @operator)
            {
                this.Operator = @operator;
            }
            #region IUnaryOperatorUniqueIdentifier Members

            public CoercibleUnaryOperators Operator { get; private set; }

            #endregion

            #region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get
                {
                    switch (this.Operator)
                    {
                        case CoercibleUnaryOperators.Plus:
                            return CLICommon.UnaryOperatorNames.Plus;
                        case CoercibleUnaryOperators.Negation:
                            return CLICommon.UnaryOperatorNames.Negation;
                        case CoercibleUnaryOperators.EvaluatesToFalse:
                            return CLICommon.UnaryOperatorNames.False;
                        case CoercibleUnaryOperators.EvaluatesToTrue:
                            return CLICommon.UnaryOperatorNames.True;
                        case CoercibleUnaryOperators.LogicalInvert:
                            return CLICommon.UnaryOperatorNames.LogicalNot;
                        case CoercibleUnaryOperators.Complement:
                            return CLICommon.UnaryOperatorNames.OnesComplement;
                        case CoercibleUnaryOperators.Increment:
                            return CLICommon.UnaryOperatorNames.Increment;
                        case CoercibleUnaryOperators.Decrement:
                            return CLICommon.UnaryOperatorNames.Decrement;
                        default:
                            return string.Empty;
                    }
                }
            }

            #endregion

            #region IEquatable<IUnaryOperatorUniqueIdentifier> Members

            public bool Equals(IUnaryOperatorUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                return other.Operator == this.Operator;
            }

            #endregion

            #region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IUnaryOperatorUniqueIdentifier)
                    return this.Equals((IUnaryOperatorUniqueIdentifier)other);
                return false;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IUnaryOperatorUniqueIdentifier)
                    return this.Equals((IUnaryOperatorUniqueIdentifier)other);
                return false;
            }

            #endregion

            public override int GetHashCode()
            {
                return this.Operator.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is IUnaryOperatorUniqueIdentifier)
                    return this.Equals((IUnaryOperatorUniqueIdentifier)obj);
                return false;
            }

            public override string ToString()
            {
                return string.Format("{0}(<originType>)", this.Name);
            }
        }

        private class DefaultSignatureMemberUniqueIdentifier :
            IGeneralSignatureMemberUniqueIdentifier
        {

            public DefaultSignatureMemberUniqueIdentifier(string name, IEnumerable<IType> parameters)
            {
                this.Name = name;
                this.Parameters = parameters;
            }

            #region ISignatureMemberUniqueIdentifier<IGeneralSignatureMemberUniqueIdentifier> Members

            public IEnumerable<IType> Parameters { get; private set; }
            private int? parameterCount;
            public int ParameterCount
            {
                get
                {
                    if (this.parameterCount == null)
                        this.parameterCount = this.Parameters.Count();
                    return this.parameterCount.Value;
                }
            }

            #endregion

            #region IDeclarationUniqueIdentifier Members

            public string Name { get; private set; }

            #endregion

            #region IEquatable<IGeneralSignatureMemberUniqueIdentifier> Members

            public bool Equals(IGeneralSignatureMemberUniqueIdentifier other)
            {
                if (other.ParameterCount != this.ParameterCount)
                    return false;
                if (other.Name != this.Name)
                    return false;
                return other.Parameters.SequenceEqual(this.Parameters);
            }

            #endregion

            #region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IGeneralSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralSignatureMemberUniqueIdentifier)other);
                return false;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGeneralSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralSignatureMemberUniqueIdentifier)other);
                return false;
            }

            #endregion

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(this.Name);
                builder.Append('(');
                bool first = true;
                foreach (var element in this.Parameters)
                {
                    if (first)
                        first = false;
                    else
                        builder.Append(", ");
                    builder.Append(element.FullName);
                }
                builder.Append(')');
                return builder.ToString();
            }

            public override int GetHashCode()
            {
                return this.Name.GetHashCode() ^ this.ParameterCount;
            }

            public override bool Equals(object obj)
            {
                if (obj is IGeneralSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralSignatureMemberUniqueIdentifier)obj);
                return false;
            }
        }
        private class DefaultTypeCoercionUniqueIdentifier :
            ITypeCoercionUniqueIdentifier
        {
            #region ITypeCoercionUniqueIdentifier Members

            public TypeConversionRequirement Requirement { get; private set; }

            public TypeConversionDirection Direction { get; private set; }

            public IType CoercionType { get; private set; }

            #endregion

            #region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get {
                    switch (Requirement)
                    {
                        case TypeConversionRequirement.Explicit:
                            switch (Direction)
	                        {
                                case TypeConversionDirection.ToContainingType:
                                    return string.Format("explicit operator <originType>({0})", this.CoercionType);
                                case TypeConversionDirection.FromContainingType:
                                    return string.Format("explicit operator {0}(<originType>)", this.CoercionType);
	                        }
                            return "explicit operator <unknown>(<unknown>)";
                        case TypeConversionRequirement.Implicit:
                            switch (Direction)
	                        {
                                case TypeConversionDirection.ToContainingType:
                                    return string.Format("implicit operator <originType>({0})", this.CoercionType);
                                case TypeConversionDirection.FromContainingType:
                                    return string.Format("implicit operator {0}(<originType>)", this.CoercionType);
	                        }
                            return "implicit operator <unknown>(<unknown>)";
                    }
                    switch (Direction)
                    {
                        case TypeConversionDirection.ToContainingType:
                            return string.Format("<unknown> operator <originType>({0})", this.CoercionType);
                        case TypeConversionDirection.FromContainingType:
                            return string.Format("<unknown> operator {0}(<originType>)", this.CoercionType);
                        default:
                            return "<unknown> operator <unknown>(<unknown>)";
                    }
                }
            }

            #endregion

            #region IEquatable<ITypeCoercionUniqueIdentifier> Members

            public bool Equals(ITypeCoercionUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                return other.Direction == this.Direction && other.CoercionType == this.CoercionType && other.Requirement == this.Requirement;
            }

            #endregion

            #region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is ITypeCoercionUniqueIdentifier)
                    return this.Equals((ITypeCoercionUniqueIdentifier)other);
                return false;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is ITypeCoercionUniqueIdentifier)
                    return this.Equals((ITypeCoercionUniqueIdentifier)other);
                return false;
            }

            #endregion

            public override string ToString()
            {
                return this.Name;
            }

            public override int GetHashCode()
            {
                if (this.Name != null)
                    if (this.CoercionType != null)
                        return this.Name.GetHashCode() ^ this.Requirement.GetHashCode() ^ this.Direction.GetHashCode() ^ this.CoercionType.GetHashCode();
                    else
                        return this.Name.GetHashCode() ^ this.Requirement.GetHashCode() ^ this.Direction.GetHashCode() ^ -1;
                else
                    if (this.CoercionType != null)
                        return -3 ^ this.Requirement.GetHashCode() ^ this.Direction.GetHashCode() ^ this.CoercionType.GetHashCode();
                    else
                        return -3 ^ this.Requirement.GetHashCode() ^ this.Direction.GetHashCode() ^ -5;
            }

            public DefaultTypeCoercionUniqueIdentifier(TypeConversionRequirement requirement, TypeConversionDirection direction, IType coercionType)
            {
                this.Requirement = requirement;
                this.Direction = direction;
                this.CoercionType = coercionType;
            }
        }

        private class DefaultGenericSignatureMemberUniqueIdentifier :
            IGeneralGenericSignatureMemberUniqueIdentifier
        {

            #region IEquatable<IGeneralGenericSignatureMemberUniqueIdentifier> Members

            public bool Equals(IGeneralGenericSignatureMemberUniqueIdentifier other)
            {
                return other.TypeParameters == this.TypeParameters && other.Name == this.Name && other.ParameterCount == this.ParameterCount && other.Parameters.SequenceEqual(this.Parameters);
            }

            #endregion

            #region IGenericParamParentUniqueIdentifier<IGeneralGenericSignatureMemberUniqueIdentifier> Members

            public bool IsGenericConstruct
            {
                get { return this.TypeParameters > 0; }
            }

            public int TypeParameters { get; private set; }

            #endregion


            #region ISignatureMemberUniqueIdentifier<IGeneralSignatureMemberUniqueIdentifier> Members

            public IEnumerable<IType> Parameters { get; private set; }
            private int? parameterCount;
            public int ParameterCount
            {
                get
                {
                    if (this.parameterCount == null)
                        this.parameterCount = this.Parameters.Count();
                    return this.parameterCount.Value;
                }
            }

            #endregion

            #region IDeclarationUniqueIdentifier Members

            public string Name { get; private set; }

            #endregion

            #region IEquatable<IGeneralSignatureMemberUniqueIdentifier> Members

            public bool Equals(IGeneralSignatureMemberUniqueIdentifier other)
            {
                if (other is IGeneralGenericSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralGenericSignatureMemberUniqueIdentifier)other);
                return false;
            }

            #endregion

            #region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IGeneralGenericSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralGenericSignatureMemberUniqueIdentifier)other);
                return false;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGeneralGenericSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralGenericSignatureMemberUniqueIdentifier)other);
                return false;
            }

            #endregion


            public DefaultGenericSignatureMemberUniqueIdentifier(string name, int typeParameters, IEnumerable<IType> parameters)
            {
                this.Name = name;
                this.TypeParameters = typeParameters;
                this.Parameters = parameters;
            }
        }
    }
}
