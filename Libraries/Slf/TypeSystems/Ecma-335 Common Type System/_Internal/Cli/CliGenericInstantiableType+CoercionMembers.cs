using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliGenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TIndexer :
            class,
            IIndexerMember<TIndexer, TType>
        where TMethod :
            class,
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TProperty :
            class,
            IPropertyMember<TProperty, TType>
        where TField :
            class,
            IFieldMember<TField, TType>,
            IInstanceMember
        where TCtor :
            class,
            IConstructorMember<TCtor, TType>
        where TEvent :
            class,
            IEventMember<TEvent, TType>
        where TType :
            class,
            IGenericType<IGeneralGenericTypeUniqueIdentifier, TType>,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>
    {
        private class BinaryOperatorMember :
            CliMemberBase<IBinaryOperatorUniqueIdentifier, TType, ICliMetadataMethodDefinitionTableRow>,
            IBinaryOperatorCoercionMember<TType>
        {
            private IBinaryOperatorUniqueIdentifier uniqueIdentifier;
            private CliMetadataCollection metadata;

            internal BinaryOperatorMember(IBinaryOperatorUniqueIdentifier uniqueIdentifier, ICliMetadataMethodDefinitionTableRow metadataEntry, TType parent)
                : base(parent, metadataEntry)
            {
                this.uniqueIdentifier = uniqueIdentifier;
            }

            protected override string OnGetName()
            {
                IType left = null, right = null;
                switch (this.ContainingSide)
                {
                    case BinaryOpCoercionContainingSide.LeftSide:
                        left = this.Parent;
                        right = this.OtherSide;
                        break;
                    case BinaryOpCoercionContainingSide.RightSide:
                        left = this.OtherSide;
                        right = this.Parent;
                        break;
                    case BinaryOpCoercionContainingSide.Both:
                        left = right = this.Parent;
                        break;
                }
                string operatorText = null;
                switch (this.Operator)
                {
                    case CoercibleBinaryOperators.Add:
                        operatorText = CliCommon.BinaryOperatorNames.Addition;
                        break;
                    case CoercibleBinaryOperators.Subtract:
                        operatorText = CliCommon.BinaryOperatorNames.Subtraction;
                        break;
                    case CoercibleBinaryOperators.Multiply:
                        operatorText = CliCommon.BinaryOperatorNames.Multiply;
                        break;
                    case CoercibleBinaryOperators.Divide:
                        operatorText = CliCommon.BinaryOperatorNames.Division;
                        break;
                    case CoercibleBinaryOperators.Modulus:
                        operatorText = CliCommon.BinaryOperatorNames.Modulus;
                        break;
                    case CoercibleBinaryOperators.BitwiseAnd:
                        operatorText = CliCommon.BinaryOperatorNames.BitwiseAnd;
                        break;
                    case CoercibleBinaryOperators.BitwiseOr:
                        operatorText = CliCommon.BinaryOperatorNames.BitwiseOr;
                        break;
                    case CoercibleBinaryOperators.ExclusiveOr:
                        operatorText = CliCommon.BinaryOperatorNames.ExclusiveOr;
                        break;
                    case CoercibleBinaryOperators.LeftShift:
                        operatorText = CliCommon.BinaryOperatorNames.LeftShift;
                        break;
                    case CoercibleBinaryOperators.RightShift:
                        operatorText = CliCommon.BinaryOperatorNames.RightShift;
                        break;
                    case CoercibleBinaryOperators.IsEqualTo:
                        operatorText = CliCommon.BinaryOperatorNames.Equality;
                        break;
                    case CoercibleBinaryOperators.IsNotEqualTo:
                        operatorText = CliCommon.BinaryOperatorNames.Inequality;
                        break;
                    case CoercibleBinaryOperators.LessThan:
                        operatorText = CliCommon.BinaryOperatorNames.LessThan;
                        break;
                    case CoercibleBinaryOperators.GreaterThan:
                        operatorText = CliCommon.BinaryOperatorNames.GreaterThan;
                        break;
                    case CoercibleBinaryOperators.LessThanOrEqualTo:
                        operatorText = CliCommon.BinaryOperatorNames.LessThanOrEqual;
                        break;
                    case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                        operatorText = CliCommon.BinaryOperatorNames.GreaterThanOrEqual;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                return string.Format("{0} {1}({2}, {3})", this.ReturnType, operatorText, left, right);
            }

            public override string ToString()
            {
                return string.Format("binaryop {0}::{1}",this.Parent,  this.UniqueIdentifier);
            }

            public override IBinaryOperatorUniqueIdentifier UniqueIdentifier
            {
                get { return this.uniqueIdentifier; }
            }

            ICoercibleType ICoercionMember.Parent
            {
                get { return this.Parent; }
            }

            public AccessLevelModifiers AccessLevel
            {
                get { return AccessLevelModifiers.Public; }
            }

            public CoercibleBinaryOperators Operator
            {
                get { return this.UniqueIdentifier.Operator; }
            }

            public BinaryOpCoercionContainingSide ContainingSide
            {
                get { return this.UniqueIdentifier.ContainingSide; }
            }

            public IType OtherSide
            {
                get {
                    switch (this.ContainingSide)
                    {
                        case BinaryOpCoercionContainingSide.LeftSide:
                        case BinaryOpCoercionContainingSide.RightSide:
                            return this.UniqueIdentifier.OtherSide;
                        case BinaryOpCoercionContainingSide.Both:
                            return this.Parent;
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }

            private _ICliManager IdentityManager { get { return (_ICliManager)this.Parent.IdentityManager; } }

            public IType ReturnType
            {
                get { return this.IdentityManager.ObtainTypeReference(this.MetadataEntry.Signature.ReturnType, this.Parent, null, this.Parent.Assembly); }
            }

            #region IMetadataEntity Members

            public IMetadataCollection Metadata
            {
                get
                {
                    if (this.metadata == null)
                        this.metadata = new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this.IdentityManager);
                    return this.metadata;
                }
            }

            public bool IsDefined(IType metadatumType)
            {
                return this.Metadata.Contains(metadatumType);
            }

            #endregion
        }

        private class TypeCoercionMember :
            CliMemberBase<ITypeCoercionUniqueIdentifier, TType, ICliMetadataMethodDefinitionTableRow>,
            ITypeCoercionMember<TType>
        {
            private ITypeCoercionUniqueIdentifier uniqueIdentifier;
            private CliMetadataCollection metadata;

            internal TypeCoercionMember(ITypeCoercionUniqueIdentifier uniqueIdentifier, ICliMetadataMethodDefinitionTableRow metadataEntry, TType parent)
                : base(parent, metadataEntry)
            {
                this.uniqueIdentifier = uniqueIdentifier;
            }

            protected override string OnGetName()
            {
                switch (this.UniqueIdentifier.Requirement)
                {
                    case TypeConversionRequirement.Explicit:
                        return CliCommon.TypeCoercionNames.Explicit;
                    case TypeConversionRequirement.Implicit:
                        return CliCommon.TypeCoercionNames.Implicit;
                    default:
                    case TypeConversionRequirement.Unknown:
                        throw new InvalidOperationException();
                }
            }

            public override ITypeCoercionUniqueIdentifier UniqueIdentifier
            {
                get { return this.uniqueIdentifier; }
            }

            ICoercibleType ICoercionMember.Parent
            {
                get { return this.Parent; }
            }

            public AccessLevelModifiers AccessLevel
            {
                get { return AccessLevelModifiers.Public; }
            }

            public TypeConversionRequirement Requirement
            {
                get { return this.UniqueIdentifier.Requirement; }
            }

            public TypeConversionDirection Direction
            {
                get { return this.UniqueIdentifier.Direction; }
            }

            public IType CoercionType
            {
                get { return this.UniqueIdentifier.CoercionType; }
            }

            public override string ToString()
            {
                return string.Format("typeCoercion {0}::{1}", this.Parent, this.UniqueIdentifier);
            }

            private _ICliManager IdentityManager { get { return (_ICliManager)this.Parent.IdentityManager; } }

            #region IMetadataEntity Members

            public IMetadataCollection Metadata
            {
                get
                {
                    if (this.metadata == null)
                        this.metadata = new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this.IdentityManager);
                    return this.metadata;
                }
            }

            public bool IsDefined(IType metadatumType)
            {
                return this.Metadata.Contains(metadatumType);
            }

            #endregion

        }

        private class UnaryOperatorMember :
            CliMemberBase<IUnaryOperatorUniqueIdentifier, TType, ICliMetadataMethodDefinitionTableRow>,
            IUnaryOperatorCoercionMember<TType>
        {
            private IUnaryOperatorUniqueIdentifier uniqueIdentifier;
            private CliMetadataCollection metadata;

            internal UnaryOperatorMember(IUnaryOperatorUniqueIdentifier uniqueIdentifier, ICliMetadataMethodDefinitionTableRow metadataEntry, TType parent)
                : base(parent, metadataEntry)
            {
                this.uniqueIdentifier = uniqueIdentifier;
            }

            protected override string OnGetName()
            {
                switch (this.Operator)
                {
                    case CoercibleUnaryOperators.Plus:
                        return CliCommon.UnaryOperatorNames.Plus;
                    case CoercibleUnaryOperators.Negation:
                        return CliCommon.UnaryOperatorNames.Negation;
                    case CoercibleUnaryOperators.EvaluatesToFalse:
                        return CliCommon.UnaryOperatorNames.False;
                    case CoercibleUnaryOperators.EvaluatesToTrue:
                        return CliCommon.UnaryOperatorNames.True;
                    case CoercibleUnaryOperators.LogicalInvert:
                        return CliCommon.UnaryOperatorNames.LogicalNot;
                    case CoercibleUnaryOperators.Complement:
                        return CliCommon.UnaryOperatorNames.OnesComplement;
                    case CoercibleUnaryOperators.Increment:
                        return CliCommon.UnaryOperatorNames.Increment;
                    case CoercibleUnaryOperators.Decrement:
                        return CliCommon.UnaryOperatorNames.Decrement;
                    default:
                        throw new InvalidOperationException();
                }
            }

            public override IUnaryOperatorUniqueIdentifier UniqueIdentifier
            {
                get { return this.uniqueIdentifier; }
            }


            ICoercibleType ICoercionMember.Parent
            {
                get { return this.Parent; }
            }

            public AccessLevelModifiers AccessLevel
            {
                get { return AccessLevelModifiers.Public; }
            }

            public CoercibleUnaryOperators Operator
            {
                get { return this.uniqueIdentifier.Operator; }
            }

            private _ICliManager _IdentityManager
            {
                get
                {
                    return (_ICliManager)this.Parent.IdentityManager;
                }
            }

            public override string ToString()
            {
                return string.Format("unaryop {0}::{1}", this.Parent, this.UniqueIdentifier);
            }

            public IType ResultedType
            {
                get { return this._IdentityManager.ObtainTypeReference(this.MetadataEntry.Signature.ReturnType, this.Parent, null, this.Parent.Assembly); }
            }


            #region IMetadataEntity Members

            public IMetadataCollection Metadata
            {
                get
                {
                    if (this.metadata == null)
                        this.metadata = new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this._IdentityManager);
                    return this.metadata;
                }
            }

            public bool IsDefined(IType metadatumType)
            {
                return this.Metadata.Contains(metadatumType);
            }

            #endregion

        }
    }
}
