using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Properties;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Events;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
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
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>,
            IGenericType<IGeneralGenericTypeUniqueIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
            IIntermediateGenericType<IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
            IIntermediateSegmentableType<IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
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
            IntermediateCoercionMemberBase<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TType>, IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>,
            IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType>
        {
            
            private BinaryOpCoercionContainingSide containingSide;
            private CoercibleBinaryOperators _operator;
            private IType returnType;
            private IType otherSide;
            private IBinaryOperatorUniqueIdentifier uniqueIdentifier;
            private ILocalMember leftSide;
            private ILocalMember rightSide;

            internal BinaryOperatorMember(IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
                : base(((TIntermediateType)(((object)(parent)))))
            {
            }

            /// <summary>
            /// Obtains the <see cref="DeclarationBase{TIdentifier}.Name"/> for the <see cref="BinaryOperatorMember"/>.
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

            TType IBinaryOperatorCoercionMember<TType>.Parent
            {
                get
                {
                    return this.Parent;
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
                            if (value == this._operator)
                                return;
                            var oldId = this.UniqueIdentifier;
                            this._operator = value;
                            this.OnIdentifierChanged(oldId, DeclarationChangeCause.Signature);
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
                            if (value == this.containingSide)
                                return;
                            var oldId = this.UniqueIdentifier;
                            this.containingSide = value;
                            this.OnIdentifierChanged(oldId, DeclarationChangeCause.Signature);
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
                    if (value == otherSide)
                        return;
                    if (this.ContainingSide == BinaryOpCoercionContainingSide.Both)
                        throw new InvalidOperationException(Resources.Exception_InvalidOperation_ContainingSideIsBoth);
                    var oldId = this.UniqueIdentifier;
                    this.otherSide = value;
                    this.OnIdentifierChanged(oldId, DeclarationChangeCause.Signature);
                }
            }

            protected override void OnIdentifierChanged(IBinaryOperatorUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
            {
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
                base.OnIdentifierChanged(oldIdentifier, cause);
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

            public override IBinaryOperatorUniqueIdentifier UniqueIdentifier
            {
                get
                {
                    lock (this.SyncObject)
                    {
                        if (uniqueIdentifier == null)
                        {
                            var service = this.Assembly.GetUniqueIdentifierService();
                            this.uniqueIdentifier = service.HandlesBinaryOperatorCoercionIdentifier
                                                    ? service.GetIdentifier(this)
                                                    : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(this);
                        }
                        return this.uniqueIdentifier;
                    }
                }
            }

            public override string ToString()
            {
                return this.UniqueIdentifier.ToString();
            }

            public override IIntermediateAssembly Assembly
            {
                get { return ((IIntermediateTypeParent)Parent).Assembly; }
            }

            public override void Accept(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }

            public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
            {
                return visitor.Visit(this, context);
            }

            protected override void ClearIdentifier()
            {
                lock (this.SyncObject)
                    if (this.uniqueIdentifier != null)
                        this.uniqueIdentifier = null;
            }


            public ILocalMember LeftSide
            {
                get
                {
                    if (this.leftSide == null)
                    {
                        IType targetType = (this.ContainingSide == BinaryOpCoercionContainingSide.LeftSide || this.ContainingSide == BinaryOpCoercionContainingSide.Both) ?
                            ((IType)this.Parent) : this.OtherSide;
                        this.leftSide = this.Locals.Add(new TypedName("__leftSide", targetType));
                        this.leftSide.AutoDeclare = false;
                    }
                    return this.leftSide;
                }
            }

            public ILocalMember RightSide
            {
                get
                {
                    if (this.rightSide == null)
                    {
                        IType targetType = (this.ContainingSide == BinaryOpCoercionContainingSide.RightSide || this.ContainingSide == BinaryOpCoercionContainingSide.Both) ?
                            ((IType)this.Parent) : this.OtherSide;
                        this.rightSide = this.Locals.Add(new TypedName("__rightSide", targetType));
                        rightSide.AutoDeclare = false;
                    }
                    return this.rightSide;
                }
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
                : base(parent, parent.Assembly)
            {
            }
            public MethodMember(string name, TInstanceIntermediateType parent)
                : base(name, parent, parent.Assembly)
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
            IntermediateCoercionMemberBase<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TType>, IIntermediateTypeCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>,
            IIntermediateTypeCoercionMember<TType, TIntermediateType>
        {
            private TypeConversionRequirement requirement;
            private TypeConversionDirection direction;
            private IType coercionType;

            private ITypeCoercionUniqueIdentifier uniqueIdentifier;
            private ILocalMember _incoming;
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
                    if (value == this.requirement)
                        return;
                    switch (value)
                    {
                        case TypeConversionRequirement.Explicit:
                        case TypeConversionRequirement.Implicit:
                            var oldIdentifier = this.UniqueIdentifier;
                            this.requirement = value;
                            this.OnIdentifierChanged(oldIdentifier, DeclarationChangeCause.Signature);
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
                    if (value == this.direction)
                        return;
                    switch (value)
                    {
                        case TypeConversionDirection.ToContainingType:
                        case TypeConversionDirection.FromContainingType:
                            var oldIdentifier = this.UniqueIdentifier;
                            this.direction = value;
                            this.OnIdentifierChanged(oldIdentifier, DeclarationChangeCause.Signature);
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
                    if (value == this.coercionType)
                        return;
                    var oldIdentifier = this.UniqueIdentifier;
                    this.coercionType = value;
                    this.OnIdentifierChanged(oldIdentifier, DeclarationChangeCause.Signature);
                }
            }

            #endregion

            public override IIntermediateAssembly Assembly
            {
                get { return ((IIntermediateType)this.Parent).Assembly; }
            }


            public override void Accept(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }

            public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
            {
                return visitor.Visit(this, context);
            }

            protected override void ClearIdentifier()
            {
                lock (this.SyncObject)
                    if (this.uniqueIdentifier != null)
                        this.uniqueIdentifier = null;
            }

            TType ITypeCoercionMember<TType>.Parent
            {
                get
                {
                    return this.Parent;
                }
            }

            public override ITypeCoercionUniqueIdentifier UniqueIdentifier
            {
                get
                {
                    lock (this.SyncObject)
                    {
                        if (uniqueIdentifier == null)
                        {
                            var service = this.Assembly.GetUniqueIdentifierService();
                            this.uniqueIdentifier = service.HandlesTypeCoercionIdentifier
                                                    ? service.GetIdentifier(this)
                                                    : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(this);
                        }
                        return this.uniqueIdentifier;
                    }
                }
            }

            public ILocalMember Incoming
            {
                get
                {
                    if (this._incoming == null)
                    {
                        this._incoming = this.Locals.Add(new TypedName("___incoming", CoercionType));
                        _incoming.AutoDeclare = false;
                    }
                    return this._incoming;
                }
            }

        }

        /// <summary>
        /// Provides a base implementation of a unary operator coercion member defined 
        /// within an instantiable type.
        /// </summary>
        public sealed class UnaryOperatorMember :
            IntermediateCoercionMemberBase<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TType>, IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>,
            IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType>
        {
            private IType resultedType;
            private CoercibleUnaryOperators _operator;
            private IUnaryOperatorUniqueIdentifier uniqueIdentifier;

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

            TType IUnaryOperatorCoercionMember<TType>.Parent
            {
                get
                {
                    return this.Parent;
                }
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
                    if (value == this._operator)
                        return;
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
                            var oldIdentifier = this.UniqueIdentifier;
                            this._operator = value;
                            this.OnIdentifierChanged(oldIdentifier, DeclarationChangeCause.Signature);
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
                    if (value == this.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType))
                        throw new InvalidOperationException("Cannot yield a void type.");
                    this.resultedType = value;
                }
            }

            #endregion

            public override IUnaryOperatorUniqueIdentifier UniqueIdentifier
            {
                get
                {
                    lock (this.SyncObject)
                    {
                        if (uniqueIdentifier == null)
                        {
                            var service = this.Assembly.GetUniqueIdentifierService();
                            this.uniqueIdentifier = service.HandlesUnaryOperatorCoercionIdentifier
                                                    ? service.GetIdentifier(this)
                                                    : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(this);
                        }
                        return this.uniqueIdentifier;
                    }
                }
            }

            public override IIntermediateAssembly Assembly
            {
                get { return ((IIntermediateType)this.Parent).Assembly; }
            }

            public override void Accept(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }


            public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
            {
                return visitor.Visit(this, context);
            }

            protected override void ClearIdentifier()
            {
                lock (this.SyncObject)
                    if (this.uniqueIdentifier != null)
                        this.uniqueIdentifier = null;
            }

        }

        public abstract class PropertyMember :
            IntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType, TIntermediatePropertyMethod>
        {
            protected PropertyMember(string name, TInstanceIntermediateType parent)
                : base(name, parent, parent.Assembly)
            {

            }
            protected PropertyMember(TInstanceIntermediateType parent)
                : base(parent, parent.Assembly)
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
                : base(parent, parent.Assembly)
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
                : base(name, parent, parent.Assembly)
            {

            }

        }

        public abstract class FieldMember :
            IntermediateFieldMemberBase<TField, TIntermediateField, TType, TIntermediateType>,
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>,
            IIntermediateInstanceMember,
            IIntermediateScopedDeclaration
        {
            private InstanceMemberAttributes instanceFlags;

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
                    return ((this.instanceFlags & InstanceMemberAttributes.HideBySignature) == InstanceMemberAttributes.HideBySignature);
                }
                set
                {
                    if (this.IsHideBySignature == value)
                        return;
                    if (value)
                        this.instanceFlags |= InstanceMemberAttributes.HideBySignature;
                    else
                        this.instanceFlags &= ~InstanceMemberAttributes.HideBySignature;
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
                        var specialModifier = intermediateParent.SpecialModifier;
                        if ((specialModifier & SpecialClassModifier.Static) == SpecialClassModifier.Static ||
                            (specialModifier & SpecialClassModifier.Module) == SpecialClassModifier.Module)
                            return true;
                    }
                    return IsExplicitStatic;
                }
                set
                {
                    if (value)
                    {
                        this.instanceFlags |= InstanceMemberAttributes.Static;
                    }
                    else
                        this.instanceFlags &= ~InstanceMemberAttributes.Static;
                }
            }

            public bool IsExplicitStatic
            {
                get
                {
                    return ((this.instanceFlags & InstanceMemberAttributes.Static) == InstanceMemberAttributes.Static);
                }
            }

            #endregion

            #region IInstanceMember Members

            public new InstanceMemberAttributes Attributes
            {
                get { return this.instanceFlags; }
            }

            #endregion


            #region IIntermediateScopedDeclaration Members

            public AccessLevelModifiers AccessLevel { get; set; }

            #endregion

            protected override IIntermediateIdentityManager IdentityManager
            {
                get { return this.Parent.IdentityManager; }
            }

            protected override IIntermediateAssembly Assembly
            {
                get
                {
                    return this.Parent.Assembly;
                }
            }
        }
    }
}
