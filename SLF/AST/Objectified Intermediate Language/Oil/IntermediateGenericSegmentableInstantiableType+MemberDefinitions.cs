using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Properties;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            class, 
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TCtor
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            class,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>,
            TEvent
        where TIntermediateEventMethod :
            class,
            TIntermediateMethod,
            IIntermediateEventMethodMember
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>,
            TField,
            IIntermediateInstanceMember
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        where TIntermediateIndexerMethod :
            class, 
            TIntermediateMethod,
            IIntermediatePropertyMethodMember
        where TMethod :
            class,
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            class,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>,
            TMethod
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TProperty
        where TIntermediatePropertyMethod :
            class,
            TIntermediateMethod,
            IIntermediatePropertyMethodMember
        where TType :
            class,
            IGenericType<TType>,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TType, TIntermediateType>,
            IIntermediateSegmentableType<TType, TIntermediateType>,
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TType
        where TInstanceIntermediateType :
            IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>,
            TIntermediateType
    {
        /// <summary>
        /// Provides a base implementation of a binary operator coercion member defined within
        /// an instantiable type.
        /// </summary>
        public class BinaryOperatorMember :
            IntermediateCoercionMemberBase<IBinaryOperatorCoercionMember<TType>, IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>,
            IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType>
        {
            private BinaryOpCoercionContainingSide containingSide;
            private CoercibleBinaryOperators _operator;
            private IType returnType;
            private IType otherSide;

            internal BinaryOperatorMember(IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
                : base(((TIntermediateType)(((object)(parent)))))
            {
            }

            /// <summary>
            /// Obtains the <see cref="DeclarationBase.Name"/> for the <see cref="BinaryOperatorMember"/>.
            /// </summary>
            /// <returns>A <see cref="System.String"/> that contains 
            /// the name of the 
            /// <see cref="BinaryOperatorMember"/>.</returns>
            protected override sealed string OnGetName()
            {
                switch (this.Operator)
                {
                    case CoercibleBinaryOperators.Add:
                        return "+";
                    case CoercibleBinaryOperators.Subtract:
                        return "-";
                    case CoercibleBinaryOperators.Multiply:
                        return "*";
                    case CoercibleBinaryOperators.Divide:
                        return "/";
                    case CoercibleBinaryOperators.Modulus:
                        return "%";
                    case CoercibleBinaryOperators.BitwiseAnd:
                        return "&";
                    case CoercibleBinaryOperators.BitwiseOr:
                        return "|";
                    case CoercibleBinaryOperators.ExclusiveOr:
                        return "^";
                    case CoercibleBinaryOperators.LeftShift:
                        return "<<";
                    case CoercibleBinaryOperators.RightShift:
                        return ">>";
                    case CoercibleBinaryOperators.IsEqualTo:
                        return "==";
                    case CoercibleBinaryOperators.IsNotEqualTo:
                        return "!=";
                    case CoercibleBinaryOperators.LessThan:
                        return "<";
                    case CoercibleBinaryOperators.GreaterThan:
                        return ">";
                    case CoercibleBinaryOperators.LessThanOrEqualTo:
                        return "<=";
                    case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                        return ">=";
                    default:
                        return null;
                }
            }

            #region IIntermediateBinaryOperatorCoercionMember Members

            public CoercibleBinaryOperators Operator
            {
                get
                {
                    return this._operator;
                }
                set
                {
                    switch (value)
                    {
                        case CoercibleBinaryOperators.Add:
                        case CoercibleBinaryOperators.Subtract:
                        case CoercibleBinaryOperators.Multiply:
                        case CoercibleBinaryOperators.Divide:
                        case CoercibleBinaryOperators.Modulus:
                        case CoercibleBinaryOperators.BitwiseAnd:
                        case CoercibleBinaryOperators.BitwiseOr:
                        case CoercibleBinaryOperators.ExclusiveOr:
                        case CoercibleBinaryOperators.LeftShift:
                        case CoercibleBinaryOperators.RightShift:
                        case CoercibleBinaryOperators.IsEqualTo:
                        case CoercibleBinaryOperators.IsNotEqualTo:
                        case CoercibleBinaryOperators.LessThan:
                        case CoercibleBinaryOperators.GreaterThan:
                        case CoercibleBinaryOperators.LessThanOrEqualTo:
                        case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                            this._operator = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("value");
                    }
                }
            }

            public BinaryOpCoercionContainingSide ContainingSide
            {
                get
                {
                    return this.containingSide;
                }
                set
                {
                    switch (value)
                    {
                        case BinaryOpCoercionContainingSide.LeftSide:
                        case BinaryOpCoercionContainingSide.RightSide:
                        case BinaryOpCoercionContainingSide.Both:
                            this.containingSide = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("value");
                    }
                }
            }

            public IType OtherSide
            {
                get
                {
                    if (this.ContainingSide == BinaryOpCoercionContainingSide.Both)
                        return null;
                    return this.otherSide;
                }
                set
                {
                    if (this.ContainingSide == BinaryOpCoercionContainingSide.Both)
                        throw new InvalidOperationException(Resources.Exception_InvalidOperation_ContainingSideIsBoth);
                    this.otherSide = value;
                }
            }

            public IType ReturnType
            {
                get
                {
                    return this.returnType;
                }
                set
                {
                    this.returnType = value;
                }
            }

            #endregion

            public override string UniqueIdentifier
            {
                get
                {
                    switch (this.ContainingSide)
                    {
                        case BinaryOpCoercionContainingSide.LeftSide:
                            return string.Format("{0} operator {1}({2}, {3})", this.ReturnType, this.Name, this.Parent.FullName, this.OtherSide.FullName);
                        case BinaryOpCoercionContainingSide.RightSide:
                            return string.Format("{0} operator {1}({2}, {3})", this.ReturnType, this.Name, this.OtherSide.FullName, this.Parent.FullName);
                        case BinaryOpCoercionContainingSide.Both:
                            return string.Format("{0} operator {1}({2}, {2})", this.ReturnType, this.Name, this.Parent.FullName);
                        default:
                            return string.Format(this.Name);
                    }
                }
            }

            public override string ToString()
            {
                return this.UniqueIdentifier;
            }

            public override IIntermediateAssembly Assembly
            {
                get { return ((IIntermediateTypeParent)Parent).Assembly; }
            }

            public override void Visit(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        /// <summary>
        /// Provides a base implementation of a constructor member defined within an instantiable type.
        /// </summary>
        public abstract partial class ConstructorMember :
            IntermediateConstructorMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            protected ConstructorMember(IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent, bool typeInitializer = false)
                : base(((TIntermediateType)(((object)(parent)))), typeInitializer)
            {
            }
        }

        /// <summary>
        /// Provides a base implementation of an event member defined within an instantiable type.
        /// </summary>
        public abstract partial class EventMember :
            IntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType, TIntermediateEventMethod>,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>
        {
            public EventMember(TInstanceIntermediateType parent)
                : base(parent)
            {
            }
        }

        /// <summary>
        /// Provides a base implementation of a method member defined within an instantiable type.
        /// </summary>
        public abstract partial class MethodMember :
            IntermediateMethodMemberBase<TMethod, TIntermediateMethod, TType, TIntermediateType>
        {
            public MethodMember(TInstanceIntermediateType parent)
                : base(parent)
            {
            }
            public MethodMember(string name, TInstanceIntermediateType parent)
                : base(name, parent)
            {
            }

            public override IIntermediateAssembly Assembly
            {
                get { return ((IIntermediateType)this.Parent).Assembly; }
            }

        }

        /// <summary>
        /// Provides a  base implementation of a type coercion member defined within an instantiable type.
        /// </summary>
        public class TypeCoercionMember :
            IntermediateCoercionMemberBase<ITypeCoercionMember<TType>, IIntermediateTypeCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>,
            IIntermediateTypeCoercionMember<TType, TIntermediateType>
        {
            private TypeConversionRequirement requirement;
            private TypeConversionDirection direction;
            private IType coercionType;
            internal TypeCoercionMember(TInstanceIntermediateType parent)
                : base(parent)
            {
            }

            #region IIntermediateTypeCoercionMember Members

            public TypeConversionRequirement Requirement
            {
                get
                {
                    return this.requirement;
                }
                set
                {
                    switch (value)
                    {
                        case TypeConversionRequirement.Explicit:
                        case TypeConversionRequirement.Implicit:
                            this.requirement = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("value");
                    }
                }
            }

            public TypeConversionDirection Direction
            {
                get
                {
                    return this.direction;
                }
                set
                {
                    switch (value)
                    {
                        case TypeConversionDirection.ToContainingType:
                        case TypeConversionDirection.FromContainingType:
                            this.direction = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("value");
                    }
                }
            }

            /// <summary>
            /// Returns/sets the type which is coerced by the overload.
            /// </summary>
            /// <exception cref="System.ArgumentException">thrown when <paramref name="value"/> is an interface.</exception>
            /// <exception cref="System.ArgumentNullException">thrown when <paramref name="value"/> is null.</exception>
            public IType CoercionType
            {
                get
                {
                    return this.coercionType;
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException("value");
                    if (value is IInterfaceType)
                        throw new ArgumentException(Resources.Exception_Argument_CoercionType_CannotBeInterface, "value");
                    this.coercionType = value;
                }
            }

            #endregion

            public override IIntermediateAssembly Assembly
            {
                get { return ((IIntermediateType)this.Parent).Assembly; }
            }


            public override void Visit(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        /// <summary>
        /// Provides a base implementation of a unary operator coercion member defined 
        /// within an instantiable type.
        /// </summary>
        public sealed class UnaryOperatorMember :
            IntermediateCoercionMemberBase<IUnaryOperatorCoercionMember<TType>, IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>,
            IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType>
        {
            private IType resultedType;
            private CoercibleUnaryOperators _operator;
            internal UnaryOperatorMember(IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
                : base(((TIntermediateType)(((object)(parent)))))
            {
            }

            protected override string OnGetName()
            {
                string p = null;
                switch (this.Operator)
                {
                    case CoercibleUnaryOperators.Plus:
                        p = "+";
                        break;
                    case CoercibleUnaryOperators.Negation:
                        p = "-";
                        break;
                    case CoercibleUnaryOperators.EvaluatesToFalse:
                        p = "false";
                        break;
                    case CoercibleUnaryOperators.EvaluatesToTrue:
                        p = "true";
                        break;
                    case CoercibleUnaryOperators.LogicalInvert:
                        p = "!";
                        break;
                    case CoercibleUnaryOperators.Complement:
                        p = "~";
                        break;
                    case CoercibleUnaryOperators.Increment:
                        p = "++";
                        break;
                    case CoercibleUnaryOperators.Decrement:
                        p = "--";
                        break;
                    default:
                        return null;
                }
                return string.Format("unary operator {0}", p);
            }

            #region IIntermediateUnaryOperatorCoercionMember Members

            /// <summary>
            /// Returns/sets operator coerced.
            /// </summary>
            public CoercibleUnaryOperators Operator
            {
                get
                {
                    return _operator;
                }
                set
                {
                    switch (value)
                    {
                        case CoercibleUnaryOperators.Plus:
                        case CoercibleUnaryOperators.Negation:
                        case CoercibleUnaryOperators.EvaluatesToFalse:
                        case CoercibleUnaryOperators.EvaluatesToTrue:
                        case CoercibleUnaryOperators.LogicalInvert:
                        case CoercibleUnaryOperators.Complement:
                        case CoercibleUnaryOperators.Increment:
                        case CoercibleUnaryOperators.Decrement:
                            this._operator = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("value");
                    }
                }
            }

            #endregion

            #region IIntermediateUnaryOperatorCoercionMember Members

            /// <summary>
            /// Returns/sets the <see cref="IType"/> which results from the
            /// unary operation.
            /// </summary>
            public IType ResultedType
            {
                get
                {
                    if (this.resultedType == null)
                        this.resultedType = base.Parent;
                    return this.resultedType;
                }
                set
                {
                    if (value == CommonTypeRefs.Void)
                        throw new InvalidOperationException("Cannot yield a void type.");
                    this.resultedType = value;
                }
            }

            #endregion

            public override IIntermediateAssembly Assembly
            {
                get { return ((IIntermediateType)this.Parent).Assembly; }
            }

            public override void Visit(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public abstract class PropertyMember :
            IntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType, TIntermediatePropertyMethod>
        {
            protected PropertyMember(string name, TInstanceIntermediateType parent)
                : base(name, parent)
            {

            }
            protected PropertyMember(TInstanceIntermediateType parent)
                : base(parent)
            {

            }
        }

        /// <summary>
        /// Provides a base implementation of an indexer member as seen by
        /// an intermediate generic segmentable instantiable type.
        /// </summary>
        public abstract class IndexerMember :
            IntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType, TIntermediateIndexerMethod>
        {
            /// <summary>
            /// Creates a new <see cref="IndexerMember"/> with the <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TInstanceIntermediateType"/>
            /// which contains the <see cref="IndexerMember"/>.</param>
            protected IndexerMember(TInstanceIntermediateType parent)
                : base(parent)
            {
                
            }

            /// <summary>
            /// Creates a new <see cref="IndexerMember"/> with the
            /// <paramref name="name"/> and <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="name">The <see cref="String"/> value representing the unique
            /// identifier of the <see cref="IndexerMember"/>.</param>
            /// <param name="parent">The <typeparamref name="TInstanceIntermediateType"/>
            /// which contains the <see cref="IndexerMember"/>.</param>
            protected IndexerMember(string name, TInstanceIntermediateType parent)
                : base(name, parent)
            {
                
            }

            protected override void OnParameterAdded(EventArgsR1<IIntermediateIndexerParameterMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>> e)
            {
                base.OnParameterAdded(e);
            }

            protected override void OnParameterRemoved(EventArgsR1<IIntermediateIndexerParameterMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>> e)
            {
                base.OnParameterRemoved(e);
            }
        }

        public abstract class FieldMember :
            IntermediateFieldMemberBase<TField, TIntermediateField, TType, TIntermediateType>,
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>,
            IIntermediateInstanceMember,
            IIntermediateScopedDeclaration
        {
            private InstanceMemberFlags instanceFlags;

            protected FieldMember(string name, TInstanceIntermediateType parent)
                : base(name, parent)
            {

            }

            #region IIntermediateInstanceMember Members

            /// <summary>
            /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/>
            /// hides the original definition completely.
            /// </summary>
            public bool IsHideBySignature
            {
                get
                {
                    return ((this.instanceFlags & InstanceMemberFlags.HideBySignature) == InstanceMemberFlags.HideBySignature);
                }
                set
                {
                    if (this.IsHideBySignature == value)
                        return;
                    if (value)
                        this.instanceFlags |= InstanceMemberFlags.HideBySignature;
                    else
                        this.instanceFlags &= ~InstanceMemberFlags.HideBySignature;
                }
            }

            /// <summary>
            /// Returns/sets whether the <see cref="IntermediateClassMethodMember{TInstanceIntermediateType}"/> is
            /// static.
            /// </summary>
            public bool IsStatic
            {
                get
                {
                    if (Parent is IIntermediateClassType)
                    {
                        var intermediateParent = Parent as IIntermediateClassType;
                        if (intermediateParent.SpecialModifier != SpecialClassModifier.None)
                            return true;
                    }
                    return IsExplicitStatic;
                }
                set
                {
                    if (value)
                    {
                        this.instanceFlags |= InstanceMemberFlags.Static;
                    }
                    else
                        this.instanceFlags &= ~InstanceMemberFlags.Static;
                }
            }

            public bool IsExplicitStatic
            {
                get
                {
                    return ((this.instanceFlags & InstanceMemberFlags.Static) == InstanceMemberFlags.Static);
                }
            }

            #endregion

            #region IInstanceMember Members

            public InstanceMemberFlags InstanceFlags
            {
                get { return this.instanceFlags; }
            }

            #endregion


            #region IIntermediateScopedDeclaration Members

            public AccessLevelModifiers AccessLevel { get; set; }

            #endregion
        }
    }
}
