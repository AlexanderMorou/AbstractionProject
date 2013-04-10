using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class TypeSystemIdentifiers
    {
        private class DefaultGeneralDeclarationUniqueIdentifier :
            IGeneralDeclarationUniqueIdentifier
        {

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get;
                private set;
            }
            //#endregion

            public DefaultGeneralDeclarationUniqueIdentifier(string name)
            {
                this.Name = name;
            }

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

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
                    return this.Equals((IGeneralDeclarationUniqueIdentifier) obj);
                return false;
            }
            //#endregion
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
            private IAssemblyUniqueIdentifier assembly;
            public DefaultGenericTypeUniqueIdentifier(string name, IGeneralTypeUniqueIdentifier parent, IGeneralDeclarationUniqueIdentifier @namespace = null)
                : this(name, 0, parent, @namespace)
            {
                this.ParentIdentifier = parent;
            }

            public DefaultGenericTypeUniqueIdentifier(string name, int typeParameters, IGeneralTypeUniqueIdentifier parent, IGeneralDeclarationUniqueIdentifier @namespace = null)
                : this(name, typeParameters, assembly:null, @namespace:@namespace)
            {
                this.ParentIdentifier = parent;
            }

            public DefaultGenericTypeUniqueIdentifier(string name, IAssemblyUniqueIdentifier assembly, IGeneralDeclarationUniqueIdentifier @namespace) :
                this(name, 0, assembly, @namespace)
            {
            }

            public DefaultGenericTypeUniqueIdentifier(string name, int typeParameters, IAssemblyUniqueIdentifier assembly, IGeneralDeclarationUniqueIdentifier @namespace)
            {
                int graveIndex = name.IndexOf('`');
                if (graveIndex != -1)
                {
                    var gravePart = name.Substring(graveIndex + 1);
                    int graveTypeParameters;
                    if (int.TryParse(gravePart, out graveTypeParameters))
                        if (graveTypeParameters == typeParameters)
                            name = name.Substring(0, graveIndex);
                        else
                            this.UsesNonstandardGraveAccentElement = true;
                    else
                        this.UsesNonstandardGraveAccentElement = true;
                }
                this.Name = name;
                this.TypeParameters = typeParameters;
                this.Assembly = assembly;
                this.Namespace = @namespace;
            }

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get;
                private set;
            }

            //#endregion

            //#region IEquatable<IGeneralGenericTypeUniqueIdentifier> Members

            public bool Equals(IGeneralGenericTypeUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                /* *
                 * Assembly ID resolution can be nested, thus
                 * obtain it only once.
                 * */
                IAssemblyUniqueIdentifier tAssem = this.Assembly,
                                          oAssem = other.Assembly;
            if (oAssem == null && tAssem != null ||
                    oAssem != null && tAssem == null)
                    return false;
                if (tAssem == null)
                    return other.TypeParameters == this.TypeParameters && other.Name == this.Name;
                else
                    return other.TypeParameters == this.TypeParameters && other.Name == this.Name && tAssem.Equals(oAssem);
            }

            //#endregion

            //#region IGenericParamParentUniqueIdentifier Members

            public bool IsGenericConstruct
            {
                get
                {
                    return this.TypeParameters > 0;
                }
            }

            public int TypeParameters
            {
                get;
                private set;
            }
            //#endregion

            //#region IEquatable<IGeneralTypeUniqueIdentifier> Members

            public bool Equals(IGeneralTypeUniqueIdentifier other)
            {
                if (other is IGeneralGenericTypeUniqueIdentifier)
                    return this.Equals((IGeneralGenericTypeUniqueIdentifier) other);
                return false;
            }

            //#endregion

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGeneralGenericTypeUniqueIdentifier)
                    return this.Equals((IGeneralGenericTypeUniqueIdentifier) other);
                return false;
            }
            //#endregion

            public override bool Equals(object obj)
            {
                if (obj is IGeneralGenericTypeUniqueIdentifier)
                    return this.Equals((IGeneralGenericTypeUniqueIdentifier) obj);
                return false;
            }

            public override int GetHashCode()
            {
                if (this.Name == null)
                    return -33 ^ this.TypeParameters ^ (this.UsesNonstandardGraveAccentElement ? 0x40 : 0x00);
                else
                    return this.Name.GetHashCode() ^ this.TypeParameters ^ (this.UsesNonstandardGraveAccentElement ? 0x40 : 0x00);
            }

            public override string ToString()
            {
                if (this.Assembly == null)
                    return this.FullName;
                else
                    return string.Format("{0}, {1}", this.FullName, this.Assembly);
            }

            public string FullName
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    if (this.ParentIdentifier != null)
                    {
                        sb.Append(this.ParentIdentifier.FullName);
                        sb.Append('+');
                    }
                    if (this.Namespace != null && !string.IsNullOrEmpty(this.Namespace.Name))
                    {
                        sb.Append(this.Namespace);
                        sb.Append('.');
                    }
                    sb.Append(this.Name ?? "<unknown>");
                    if (this.TypeParameters > 0 && !this.UsesNonstandardGraveAccentElement)
                    {
                        sb.Append('`');
                        sb.Append(this.TypeParameters);
                    }
                    return sb.ToString();
                }
            }

            //#region ITypeUniqueIdentifier Members

            public IAssemblyUniqueIdentifier Assembly
            {
                get {
                    if (this.assembly == null && this.ParentIdentifier != null)
                        return this.ParentIdentifier.Assembly;
                    return this.assembly; }
                private set { this.assembly = value; }
            }

            public IGeneralDeclarationUniqueIdentifier Namespace
            {
                get;
                private set;
            }

            //#endregion


            //#region IGeneralGenericTypeUniqueIdentifier Members

            public bool UsesNonstandardGraveAccentElement
            {
                get;
                private set;
            }

            //#endregion

            public IGeneralTypeUniqueIdentifier GetNestedIdentifier(string name)
            {
                if (name.Contains('`'))
                {
                    var lastGrave = name.LastIndexOf('`');
                    var typeParams = name.Substring(lastGrave + 1);
                    int typeParamCount;
                    if (int.TryParse(typeParams, out typeParamCount))
                        return GetNestedIdentifier(name, typeParamCount);
                }
                return new DefaultTypeUniqueIdentifier(name, this);
            }

            public IGeneralGenericTypeUniqueIdentifier GetNestedIdentifier(string name, int typeParameterCount)
            {
                return new DefaultGenericTypeUniqueIdentifier(name, typeParameterCount, this);
            }

            public IGeneralTypeUniqueIdentifier GetNestedIdentifier(string name, IGeneralDeclarationUniqueIdentifier @namespace)
            {
                if (name.Contains('`'))
                {
                    var lastGrave = name.LastIndexOf('`');
                    var typeParams = name.Substring(lastGrave + 1);
                    int typeParamCount;
                    if (int.TryParse(typeParams, out typeParamCount))
                        return GetNestedIdentifier(name, typeParamCount, @namespace);
                }
                return new DefaultTypeUniqueIdentifier(name, this, @namespace);
            }

            public IGeneralGenericTypeUniqueIdentifier GetNestedIdentifier(string name, int typeParameterCount, IGeneralDeclarationUniqueIdentifier @namespace)
            {
                return new DefaultGenericTypeUniqueIdentifier(name, typeParameterCount, this, @namespace);
            }

            public IGeneralTypeUniqueIdentifier ParentIdentifier { get; private set; }
        }

        private class DefaultTypeUniqueIdentifier :
            IGeneralTypeUniqueIdentifier
        {
            private IAssemblyUniqueIdentifier assembly;

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get;
                private set;
            }

            //#endregion

            //#region IEquatable<IGeneralTypeUniqueIdentifier> Members

            public bool Equals(IGeneralTypeUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                return other.Name == this.Name;
            }

            //#endregion

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other != null && other is IGeneralTypeUniqueIdentifier)
                    return this.Equals((IGeneralTypeUniqueIdentifier) other);
                return false;
            }

            //#endregion

            public override bool Equals(object obj)
            {
                if (obj != null && obj is IGeneralTypeUniqueIdentifier)
                    return this.Equals((IGeneralTypeUniqueIdentifier) obj);
                return false;
            }

            public DefaultTypeUniqueIdentifier(string name, IAssemblyUniqueIdentifier assembly, IGeneralDeclarationUniqueIdentifier @namespace)
            {
                this.Name = name;
                this.Assembly = assembly;
                this.Namespace = @namespace;
            }

            public DefaultTypeUniqueIdentifier(string name, IGeneralTypeUniqueIdentifier parentIdentifier)
            {
                this.Name = name;
                this.ParentIdentifier = parentIdentifier;
            }

            public DefaultTypeUniqueIdentifier(string name, IGeneralTypeUniqueIdentifier parentIdentifier, IGeneralDeclarationUniqueIdentifier @namespace)
            {
                this.Name = name;
                this.ParentIdentifier = parentIdentifier;
                this.Namespace = @namespace;
            }

            public override int GetHashCode()
            {
                if (this.Name == null)
                    return -7;
                return this.Name.GetHashCode();
            }
            public override string ToString()
            {
                if (this.Assembly == null)
                    return this.FullName;
                return string.Format("{0}, {1}", this.FullName, this.Assembly);
            }

            public string FullName
            {
                get
                {
                    if (this.Namespace == null || this.Namespace.Name == string.Empty)
                        if (this.ParentIdentifier == null)
                            return this.Name;
                        else
                            return string.Format("{0}+{1}", this.ParentIdentifier.FullName, this.Name);
                    if (this.ParentIdentifier == null)
                        return string.Format("{0}.{1}", this.Namespace, this.Name);
                    else
                        return string.Format("{0}+{1}.{2}", this.ParentIdentifier.FullName, this.Namespace, this.Name);
                }
            }

            //#region ITypeUniqueIdentifier Members

            public IAssemblyUniqueIdentifier Assembly
            {
                get
                {
                    if (this.assembly == null && this.ParentIdentifier != null)
                        return this.ParentIdentifier.Assembly;
                    return this.assembly;
                }
                private set { this.assembly = value; }
            }

            public IGeneralDeclarationUniqueIdentifier Namespace
            {
                get;
                private set;
            }

            public IGeneralTypeUniqueIdentifier GetNestedIdentifier(string name)
            {
                if (name.Contains('`'))
                {
                    var lastGrave = name.LastIndexOf('`');
                    var typeParams = name.Substring(lastGrave + 1);
                    int typeParamCount;
                    if (int.TryParse(typeParams, out typeParamCount))
                        return GetNestedIdentifier(name, typeParamCount);
                }
                return new DefaultTypeUniqueIdentifier(name, this);
            }

            public IGeneralGenericTypeUniqueIdentifier GetNestedIdentifier(string name, int typeParameterCount)
            {
                return new DefaultGenericTypeUniqueIdentifier(name, typeParameterCount, this);
            }

            public IGeneralTypeUniqueIdentifier GetNestedIdentifier(string name, IGeneralDeclarationUniqueIdentifier @namespace)
            {
                if (name.Contains('`'))
                {
                    var lastGrave = name.LastIndexOf('`');
                    var typeParams = name.Substring(lastGrave + 1);
                    int typeParamCount;
                    if (int.TryParse(typeParams, out typeParamCount))
                        return GetNestedIdentifier(name, typeParamCount, @namespace);
                }
                return new DefaultTypeUniqueIdentifier(name, this, @namespace);
            }

            public IGeneralGenericTypeUniqueIdentifier GetNestedIdentifier(string name, int typeParameterCount, IGeneralDeclarationUniqueIdentifier @namespace)
            {
                return new DefaultGenericTypeUniqueIdentifier(name, typeParameterCount, this, @namespace);
            }

            public IGeneralTypeUniqueIdentifier ParentIdentifier { get; private set; }

            //#endregion
        }

        private class DefaultMemberUniqueIdentifier :
            IGeneralMemberUniqueIdentifier
        {

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get;
                private set;
            }

            //#endregion

            //#region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                return other.Name == this.Name;
            }

            //#endregion

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other != null || other is IGeneralMemberUniqueIdentifier)
                    return this.Equals((IGeneralMemberUniqueIdentifier) other);
                return false;
            }

            //#endregion

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
                    return this.Equals((IGeneralMemberUniqueIdentifier) obj);
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

            //#region IGenericParameterUniqueIdentifier Members

            public int? Position
            {
                get;
                private set;
            }

            public bool IsTypeParameter
            {
                get;
                private set;
            }

            //#endregion

            public DefaultGenericParameterUniqueIdentifier(string name, bool onType, IAssembly assembly = null)
            {
                this.Name = name;
                this.IsTypeParameter = onType;
            }

            public DefaultGenericParameterUniqueIdentifier(int index, string name, bool onType, IAssembly assembly = null)
            {
                this.Position = index;
                this.Name = name;
                this.IsTypeParameter = onType;
            }

            public DefaultGenericParameterUniqueIdentifier(int index, bool onType, IAssembly assembly = null)
            {
                this.Position = index;
                this.IsTypeParameter = onType;
            }

            //#region IDeclarationUniqueIdentifier Members
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

            //#endregion

            //#region IEquatable<IGenericParameterUniqueIdentifier> Members

            public bool Equals(IGenericParameterUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                if (other.Position != null && (other.Position == this.Position))
                    return other.IsTypeParameter == this.IsTypeParameter;
                return other.Name == this.Name && other.IsTypeParameter == this.IsTypeParameter;
            }

            //#endregion

            //#region IEquatable<IGeneralTypeUniqueIdentifier> Members

            public bool Equals(IGeneralTypeUniqueIdentifier other)
            {
                if (other is IGenericParameterUniqueIdentifier)
                    return this.Equals((IGenericParameterUniqueIdentifier) other);
                return false;
            }

            //#endregion

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGenericParameterUniqueIdentifier)
                    return this.Equals((IGenericParameterUniqueIdentifier) other);
                return false;
            }

            //#endregion

            //#region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IGenericParameterUniqueIdentifier)
                    return this.Equals((IGenericParameterUniqueIdentifier) other);
                return false;
            }

            public override bool Equals(object obj)
            {
                if (obj is IGenericParameterUniqueIdentifier)
                    return this.Equals((IGenericParameterUniqueIdentifier) obj);
                return false;
            }
            //#endregion
            public override int GetHashCode()
            {
                return this.Name.GetHashCode() ^ this.IsTypeParameter.GetHashCode();
            }
            public override string ToString()
            {
                return this.Name;
            }

            public IAssemblyUniqueIdentifier Assembly
            {
                get;
                private set;
            }

            public IGeneralDeclarationUniqueIdentifier Namespace
            {
                get
                {
                    return null;
                }
            }


            public IGeneralTypeUniqueIdentifier GetNestedIdentifier(string name)
            {
                throw new NotSupportedException();
            }

            public IGeneralGenericTypeUniqueIdentifier GetNestedIdentifier(string name, int typeParameterCount)
            {
                throw new NotSupportedException();
            }

            public IGeneralTypeUniqueIdentifier GetNestedIdentifier(string name, IGeneralDeclarationUniqueIdentifier @namespace)
            {
                throw new NotSupportedException();
            }

            public IGeneralGenericTypeUniqueIdentifier GetNestedIdentifier(string name, int typeParameterCount, IGeneralDeclarationUniqueIdentifier @namespace)
            {
                throw new NotSupportedException();
            }

            public IGeneralTypeUniqueIdentifier ParentIdentifier
            {
                get { return null; }
            }

            public string FullName
            {
                get { return null; }
            }
        };

        private class DefaultBinaryOperatorUniqueIdentifier :
            IBinaryOperatorUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        {
            /// <summary>
            /// Returns the <see cref="CoercibleBinaryOperators"/> coerced
            /// by the <see cref="IBinaryOperatorUniqueIdentifier"/>.
            /// </summary>
            public CoercibleBinaryOperators Operator
            {
                get;
                private set;
            }

            /// <summary>
            /// Returns which side the required self reference
            /// the <see cref="IBinaryOperatorUniqueIdentifier"/>'s
            /// parent is on.
            /// </summary>
            public BinaryOpCoercionContainingSide ContainingSide
            {
                get;
                private set;
            }

            /// <summary>
            /// Returns the type of the other side of the expression
            /// used when performing the coercion.
            /// </summary>
            /// <remarks>If <see cref="ContainingSide"/>
            /// is <see cref="BinaryOpCoercionContainingSide.Both"/>
            /// <see cref="OtherSide"/> returns null.</remarks>
            public IType OtherSide
            {
                get;
                private set;
            }

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

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get
                {
                    switch (this.Operator)
                    {
                        case CoercibleBinaryOperators.Add:
                            return AbstractGateway.BinaryOperatorNames.Addition;
                        case CoercibleBinaryOperators.Subtract:
                            return AbstractGateway.BinaryOperatorNames.Subtraction;
                        case CoercibleBinaryOperators.Multiply:
                            return AbstractGateway.BinaryOperatorNames.Multiply;
                        case CoercibleBinaryOperators.Divide:
                            return AbstractGateway.BinaryOperatorNames.Division;
                        case CoercibleBinaryOperators.Modulus:
                            return AbstractGateway.BinaryOperatorNames.Modulus;
                        case CoercibleBinaryOperators.BitwiseAnd:
                            return AbstractGateway.BinaryOperatorNames.BitwiseAnd;
                        case CoercibleBinaryOperators.BitwiseOr:
                            return AbstractGateway.BinaryOperatorNames.BitwiseOr;
                        case CoercibleBinaryOperators.ExclusiveOr:
                            return AbstractGateway.BinaryOperatorNames.ExclusiveOr;
                        case CoercibleBinaryOperators.LeftShift:
                            return AbstractGateway.BinaryOperatorNames.LeftShift;
                        case CoercibleBinaryOperators.RightShift:
                            return AbstractGateway.BinaryOperatorNames.RightShift;
                        case CoercibleBinaryOperators.IsEqualTo:
                            return AbstractGateway.BinaryOperatorNames.Equality;
                        case CoercibleBinaryOperators.IsNotEqualTo:
                            return AbstractGateway.BinaryOperatorNames.Inequality;
                        case CoercibleBinaryOperators.LessThan:
                            return AbstractGateway.BinaryOperatorNames.LessThan;
                        case CoercibleBinaryOperators.GreaterThan:
                            return AbstractGateway.BinaryOperatorNames.GreaterThan;
                        case CoercibleBinaryOperators.LessThanOrEqualTo:
                            return AbstractGateway.BinaryOperatorNames.LessThanOrEqual;
                        case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                            return AbstractGateway.BinaryOperatorNames.GreaterThanOrEqual;
                        default:
                            return null;
                    }
                }
            }

            //#endregion

            //#region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IBinaryOperatorUniqueIdentifier)
                    return this.Equals(((IBinaryOperatorUniqueIdentifier) other));
                return false;
            }

            //#endregion

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IBinaryOperatorUniqueIdentifier)
                    return this.Equals(((IBinaryOperatorUniqueIdentifier) other));
                return false;
            }

            //#endregion

            public override bool Equals(object obj)
            {
                if (obj is IBinaryOperatorUniqueIdentifier)
                    return this.Equals(((IBinaryOperatorUniqueIdentifier) obj));
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
            //#region IUnaryOperatorUniqueIdentifier Members

            public CoercibleUnaryOperators Operator
            {
                get;
                private set;
            }

            //#endregion

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get
                {
                    switch (this.Operator)
                    {
                        case CoercibleUnaryOperators.Plus:
                            return AbstractGateway.UnaryOperatorNames.Plus;
                        case CoercibleUnaryOperators.Negation:
                            return AbstractGateway.UnaryOperatorNames.Negation;
                        case CoercibleUnaryOperators.EvaluatesToFalse:
                            return AbstractGateway.UnaryOperatorNames.False;
                        case CoercibleUnaryOperators.EvaluatesToTrue:
                            return AbstractGateway.UnaryOperatorNames.True;
                        case CoercibleUnaryOperators.LogicalInvert:
                            return AbstractGateway.UnaryOperatorNames.LogicalNot;
                        case CoercibleUnaryOperators.Complement:
                            return AbstractGateway.UnaryOperatorNames.OnesComplement;
                        case CoercibleUnaryOperators.Increment:
                            return AbstractGateway.UnaryOperatorNames.Increment;
                        case CoercibleUnaryOperators.Decrement:
                            return AbstractGateway.UnaryOperatorNames.Decrement;
                        default:
                            return string.Empty;
                    }
                }
            }

            //#endregion

            //#region IEquatable<IUnaryOperatorUniqueIdentifier> Members

            public bool Equals(IUnaryOperatorUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                return other.Operator == this.Operator;
            }

            //#endregion

            //#region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IUnaryOperatorUniqueIdentifier)
                    return this.Equals((IUnaryOperatorUniqueIdentifier) other);
                return false;
            }

            //#endregion

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IUnaryOperatorUniqueIdentifier)
                    return this.Equals((IUnaryOperatorUniqueIdentifier) other);
                return false;
            }

            //#endregion

            public override int GetHashCode()
            {
                return this.Operator.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is IUnaryOperatorUniqueIdentifier)
                    return this.Equals((IUnaryOperatorUniqueIdentifier) obj);
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

            //#region ISignatureMemberUniqueIdentifier Members

            public IEnumerable<IType> Parameters
            {
                get;
                private set;
            }
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

            //#endregion

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get;
                private set;
            }

            //#endregion

            //#region IEquatable<IGeneralSignatureMemberUniqueIdentifier> Members

            public bool Equals(IGeneralSignatureMemberUniqueIdentifier other)
            {
                if (other.ParameterCount != this.ParameterCount)
                    return false;
                if (other.Name != this.Name)
                    return false;
                return other.Parameters.SequenceEqual(this.Parameters);
            }

            //#endregion

            //#region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IGeneralSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralSignatureMemberUniqueIdentifier) other);
                return false;
            }

            //#endregion

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGeneralSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralSignatureMemberUniqueIdentifier) other);
                return false;
            }

            //#endregion

            public override string ToString()
            {
                return this.ToString(null);
            }

            public string ToString(string parentName)
            {
                StringBuilder builder = new StringBuilder();
                if (!StringHandling.IsEmptyOrNull(parentName))
                {
                    builder.Append(parentName);
                    builder.Append("::");
                }
                builder.Append(this.Name);
                builder.Append('(');
                bool first = true;
                foreach (var element in this.Parameters)
                {
                    if (first)
                        first = false;
                    else
                        builder.Append(", ");
                    if (string.IsNullOrEmpty(element.FullName))
                        builder.Append(element.Name);
                    else
                        builder.Append(element.FullName);
                }
                builder.Append(')');
                return builder.ToString();
            }

            public override int GetHashCode()
            {
                return ((this.Name == null) ? 0 : this.Name.GetHashCode()) ^ this.ParameterCount;
            }

            public override bool Equals(object obj)
            {
                if (obj is IGeneralSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralSignatureMemberUniqueIdentifier) obj);
                return false;
            }
        }
        private class DefaultTypeCoercionUniqueIdentifier :
            ITypeCoercionUniqueIdentifier
        {
            //#region ITypeCoercionUniqueIdentifier Members

            public TypeConversionRequirement Requirement
            {
                get;
                private set;
            }

            public TypeConversionDirection Direction
            {
                get;
                private set;
            }

            public IType CoercionType
            {
                get;
                private set;
            }

            //#endregion

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get
                {
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

            //#endregion

            //#region IEquatable<ITypeCoercionUniqueIdentifier> Members

            public bool Equals(ITypeCoercionUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                return other.Direction == this.Direction && other.CoercionType == this.CoercionType && other.Requirement == this.Requirement;
            }

            //#endregion

            //#region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is ITypeCoercionUniqueIdentifier)
                    return this.Equals((ITypeCoercionUniqueIdentifier) other);
                return false;
            }

            //#endregion

            public override bool Equals(object obj)
            {
                if (obj is ITypeCoercionUniqueIdentifier)
                    return this.Equals((ITypeCoercionUniqueIdentifier) obj);
                return false;
            }

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is ITypeCoercionUniqueIdentifier)
                    return this.Equals((ITypeCoercionUniqueIdentifier) other);
                return false;
            }

            //#endregion

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

            //#region IEquatable<IGeneralGenericSignatureMemberUniqueIdentifier> Members

            public bool Equals(IGeneralGenericSignatureMemberUniqueIdentifier other)
            {
                return other.TypeParameters == this.TypeParameters && other.Name == this.Name && other.ParameterCount == this.ParameterCount && other.Parameters.SequenceEqual(this.Parameters);
            }

            //#endregion

            //#region IGenericParamParentUniqueIdentifier Members

            public bool IsGenericConstruct
            {
                get
                {
                    return this.TypeParameters > 0;
                }
            }

            public int TypeParameters
            {
                get;
                private set;
            }

            //#endregion


            //#region ISignatureMemberUniqueIdentifier Members

            public IEnumerable<IType> Parameters
            {
                get;
                private set;
            }
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

            //#endregion

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get;
                private set;
            }

            //#endregion

            //#region IEquatable<IGeneralSignatureMemberUniqueIdentifier> Members

            public bool Equals(IGeneralSignatureMemberUniqueIdentifier other)
            {
                if (other is IGeneralGenericSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralGenericSignatureMemberUniqueIdentifier) other);
                return false;
            }

            //#endregion

            public override bool Equals(object obj)
            {
                if (obj is IGeneralGenericSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralGenericSignatureMemberUniqueIdentifier) obj);
                return false;
            }

            //#region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IGeneralGenericSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralGenericSignatureMemberUniqueIdentifier) other);
                return false;
            }

            //#endregion

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGeneralGenericSignatureMemberUniqueIdentifier)
                    return this.Equals((IGeneralGenericSignatureMemberUniqueIdentifier) other);
                return false;
            }

            //#endregion


            public DefaultGenericSignatureMemberUniqueIdentifier(string name, int typeParameters, IEnumerable<IType> parameters)
            {
                this.Name = name;
                this.TypeParameters = typeParameters;
                this.Parameters = parameters;
            }

            public override int GetHashCode()
            {
                if (this.Name == null)
                    return -2 ^ this.IsGenericConstruct.GetHashCode() ^ this.ParameterCount.GetHashCode() ^ this.TypeParameters.GetHashCode() ^ -3;
                else
                    return this.Name.GetHashCode() ^ this.IsGenericConstruct.GetHashCode() ^ this.ParameterCount.GetHashCode() ^ this.TypeParameters.GetHashCode() ^ -3;
            }

            public override string ToString()
            {
                return this.ToString(null);
            }

            public string ToString(string parentName)
            {
                StringBuilder builder = new StringBuilder();
                if (!StringHandling.IsEmptyOrNull(parentName))
                {
                    builder.Append(parentName);
                    builder.Append("::");
                }
                builder.Append(this.Name);
                builder.Append('(');
                bool first = true;
                foreach (var element in this.Parameters)
                {
                    if (first)
                        first = false;
                    else
                        builder.Append(", ");
                    if (string.IsNullOrEmpty(element.FullName))
                        builder.Append(element.Name);
                    else
                        builder.Append(element.FullName);
                }
                builder.Append(')');
                return builder.ToString();
            }
        }

        private class DefaultAssemblyUniqueIdentifier :
            IAssemblyUniqueIdentifier
        {
            private IMultikeyedDictionary<string, int, IGeneralGenericTypeUniqueIdentifier> GenericTypeCache = new MultikeyedDictionary<string, int, IGeneralGenericTypeUniqueIdentifier>();

            public DefaultAssemblyUniqueIdentifier(string name, IVersion version, ICultureIdentifier cultureIdentifier, byte[] publicKeyToken = null)
            {
                this.Name = name;
                this.Version = version;
                this.Culture = cultureIdentifier;
                this.PublicKeyToken = publicKeyToken;
            }
            //#region IAssemblyUniqueIdentifier Members

            public IVersion Version
            {
                get;
                private set;
            }

            public ICultureIdentifier Culture
            {
                get;
                private set;
            }

            public byte[] PublicKeyToken
            {
                get;
                private set;
            }

            //#endregion

            //#region IDeclarationUniqueIdentifier Members

            public string Name
            {
                get;
                private set;
            }

            //#endregion

            //#region IEquatable<IAssemblyUniqueIdentifier> Members

            public bool Equals(IAssemblyUniqueIdentifier other)
            {
                if (this.PublicKeyToken == null && other.PublicKeyToken != null ||
                    this.PublicKeyToken != null && other.PublicKeyToken == null)
                    return false;
                else if (this.PublicKeyToken == null && other.PublicKeyToken == null)
                    return other.Name == this.Name && other.Version.Equals(this.Version) && other.Culture == this.Culture;
                else
                    return other.Name == this.Name && other.Version.Equals(this.Version) && other.Culture == this.Culture && other.PublicKeyToken.SequenceEqual(this.PublicKeyToken);
            }

            //#endregion

            //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IAssemblyUniqueIdentifier)
                    return this.Equals((IAssemblyUniqueIdentifier) other);
                return false;
            }

            //#endregion

            public override string ToString()
            {
                return string.Format("{0}, Version={1}, Culture={2}, PublicKeyToken={3}", this.Name, this.Version, string.IsNullOrEmpty(this.Culture.Name) ? "neutral" : this.Culture.Name, this.PublicKeyToken != null ? this.PublicKeyToken.FormatHexadecimal() : "null");
            }

            public override bool Equals(object other)
            {
                if (other is IAssemblyUniqueIdentifier)
                    return this.Equals((IAssemblyUniqueIdentifier) other);
                return false;
            }

            public override int GetHashCode()
            {
                if (this.PublicKeyToken != null)
                {
                    int hashResult = 0;
                    for (int i = 0; i < this.PublicKeyToken.Length; i++)
                        hashResult ^= PublicKeyToken[i];
                    return hashResult ^ this.Name.GetHashCode() ^ this.Culture.GetHashCode() ^ this.Version.GetHashCode();
                }
                return this.Name.GetHashCode() ^ this.Culture.GetHashCode() ^ this.Version.GetHashCode();
            }

            #region IAssemblyUniqueIdentifier Members


            public IGeneralTypeUniqueIdentifier GetTypeIdentifier(IGeneralDeclarationUniqueIdentifier @namespace, string name)
            {
                if (name.Contains('`'))
                {
                    var lastGrave = name.LastIndexOf('`');
                    var typeParams = name.Substring(lastGrave + 1);
                    int typeParamCount;
                    if (int.TryParse(typeParams, out typeParamCount))
                        return GetTypeIdentifier(@namespace, name, typeParamCount);
                }
                return new DefaultTypeUniqueIdentifier(name, this, @namespace);
            }

            public IGeneralTypeUniqueIdentifier GetTypeIdentifier(string @namespace, string name)
            {
                if (string.IsNullOrEmpty(@namespace))
                    return GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier) null, name);
                else
                    return GetTypeIdentifier(TypeSystemIdentifiers.GetDeclarationIdentifier(@namespace), name);
            }

            public IGeneralGenericTypeUniqueIdentifier GetTypeIdentifier(IGeneralDeclarationUniqueIdentifier @namespace, string name, int typeParameters)
            {
                return new DefaultGenericTypeUniqueIdentifier(name, typeParameters, this, @namespace);
            }

            public IGeneralGenericTypeUniqueIdentifier GetTypeIdentifier(string @namespace, string name, int typeParameters)
            {
                if (string.IsNullOrEmpty(@namespace))
                    return GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier) null, name, typeParameters);
                else
                    return GetTypeIdentifier(TypeSystemIdentifiers.GetDeclarationIdentifier(@namespace), name, typeParameters);
            }

            #endregion
        }
    }
}
