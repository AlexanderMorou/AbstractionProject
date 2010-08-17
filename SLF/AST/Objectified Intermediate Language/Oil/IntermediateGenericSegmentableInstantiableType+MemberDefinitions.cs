using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Properties;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
            TCtor,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>
        where TIntermediateEventMethod :
            class,
            TIntermediateMethod,
            IIntermediateEventMethodMember
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            TField,
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        where TIntermediateIndexerMethod :
            class,
            TMethod,
            IIntermediatePropertyMethodMember
        where TMethod :
            class,
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            class,
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>
        where TIntermediatePropertyMethod :
            class,
            TMethod,
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
            /// Obtains the <see cref="DeclarationBase.Name"/> for the <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
            /// </summary>
            /// <returns>A <see cref="System.String"/> that contains 
            /// the name of the 
            /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.</returns>
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

            #region TopBlock Members

            private BlockStatementParentContainer statementContainer;

            private BlockStatementParentContainer StatementContainer
            {
                get
                {
                    this.CheckStatementContainer();
                    return this.statementContainer;
                }
            }

            private void CheckStatementContainer()
            {
                if (this.statementContainer == null)
                    this.statementContainer = new BlockStatementParentContainer(this);
            }

            #region IBlockStatementParent Members

            public IReturnStatement Return()
            {
                return this.StatementContainer.Return();
            }

            public IReturnStatement Return(IExpression value)
            {
                return this.StatementContainer.Return(value);
            }

            public IConditionBlockStatement If(IExpression condition)
            {
                return this.StatementContainer.If(condition);
            }

            public ISwitchStatement Switch(IExpression caseCondition)
            {
                return this.StatementContainer.Switch(caseCondition);
            }

            public ICallFusionStatement Call(IExpressionToCommaFusionExpression target)
            {
                return this.StatementContainer.Call(target);
            }

            public ICallMethodStatement Call(IMethodInvokeExpression target)
            {
                return this.StatementContainer.Call(target);
            }

            public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(ptr, parameters);
            }

            public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(ptr, parameters);
            }

            public ICallMethodStatement Call(IMethodReferenceStub stub, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(stub, parameters);
            }

            public ICallMethodStatement Call(IMethodReferenceStub stub, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(stub, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(parent, methodName, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(parent, methodName, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(string methodName, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(methodName, parameters);
            }

            public ICallMethodStatement Call(string methodName, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(methodName, parameters);
            }

            public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(callType, methodName, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(callType, methodName, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
            }

            #endregion

            #region IControlledStateCollection<IStatement> Members

            public int Count
            {
                get
                {
                    this.CheckStatementContainer();
                    return this.statementContainer.Count;
                }
            }

            public bool Contains(IStatement item)
            {
                this.CheckStatementContainer();
                return this.statementContainer.Contains(item);
            }

            public void CopyTo(IStatement[] array, int arrayIndex)
            {
                this.CheckStatementContainer();
                this.statementContainer.CopyTo(array, arrayIndex);
            }

            public IStatement this[int index]
            {
                get
                {
                    this.CheckStatementContainer();
                    return this.statementContainer[index];
                }
            }

            public IStatement[] ToArray()
            {
                this.CheckStatementContainer();
                return this.statementContainer.ToArray();
            }

            #endregion

            #region IEnumerable<IStatement> Members

            public IEnumerator<IStatement> GetEnumerator()
            {
                this.CheckStatementContainer();
                return this.statementContainer.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #endregion
        }

        /// <summary>
        /// Provides a base implementation of a constructor member defined within an instantiable type.
        /// </summary>
        public abstract partial class ConstructorMember :
            IntermediateConstructorMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            protected ConstructorMember(IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
                : base(((TIntermediateType)(((object)(parent)))))
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

            #region TopBlock Members

            private BlockStatementParentContainer statementContainer;

            private BlockStatementParentContainer StatementContainer
            {
                get
                {
                    this.CheckStatementContainer();
                    return this.statementContainer;
                }
            }

            private void CheckStatementContainer()
            {
                if (this.statementContainer == null)
                    this.statementContainer = new BlockStatementParentContainer(this);
            }

            #region IBlockStatementParent Members

            public IReturnStatement Return()
            {
                return this.StatementContainer.Return();
            }

            public IReturnStatement Return(IExpression value)
            {
                return this.StatementContainer.Return(value);
            }

            public IConditionBlockStatement If(IExpression condition)
            {
                return this.StatementContainer.If(condition);
            }

            public ISwitchStatement Switch(IExpression caseCondition)
            {
                return this.StatementContainer.Switch(caseCondition);
            }

            public ICallFusionStatement Call(IExpressionToCommaFusionExpression target)
            {
                return this.StatementContainer.Call(target);
            }

            public ICallMethodStatement Call(IMethodInvokeExpression target)
            {
                return this.StatementContainer.Call(target);
            }

            public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(ptr, parameters);
            }

            public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(ptr, parameters);
            }

            public ICallMethodStatement Call(IMethodReferenceStub stub, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(stub, parameters);
            }

            public ICallMethodStatement Call(IMethodReferenceStub stub, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(stub, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(parent, methodName, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(parent, methodName, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(string methodName, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(methodName, parameters);
            }

            public ICallMethodStatement Call(string methodName, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(methodName, parameters);
            }

            public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(callType, methodName, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(callType, methodName, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
            }

            #endregion

            #region IControlledStateCollection<IStatement> Members

            public int Count
            {
                get
                {
                    this.CheckStatementContainer();
                    return this.statementContainer.Count;
                }
            }

            public bool Contains(IStatement item)
            {
                this.CheckStatementContainer();
                return this.statementContainer.Contains(item);
            }

            public void CopyTo(IStatement[] array, int arrayIndex)
            {
                this.CheckStatementContainer();
                this.statementContainer.CopyTo(array, arrayIndex);
            }

            public IStatement this[int index]
            {
                get
                {
                    this.CheckStatementContainer();
                    return this.statementContainer[index];
                }
            }

            public IStatement[] ToArray()
            {
                this.CheckStatementContainer();
                return this.statementContainer.ToArray();
            }

            #endregion

            #region IEnumerable<IStatement> Members

            public IEnumerator<IStatement> GetEnumerator()
            {
                this.CheckStatementContainer();
                return this.statementContainer.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #endregion

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
                    case CoercibleUnaryOperators.Negative:
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
                        case CoercibleUnaryOperators.Negative:
                        case CoercibleUnaryOperators.EvaluatesToFalse:
                        case CoercibleUnaryOperators.EvaluatesToTrue:
                        case CoercibleUnaryOperators.LogicalInvert:
                        case CoercibleUnaryOperators.Complement:
                            this._operator = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("value");
                    }
                }
            }

            #endregion

            #region TopBlock Members

            private BlockStatementParentContainer statementContainer;

            private BlockStatementParentContainer StatementContainer
            {
                get
                {
                    this.CheckStatementContainer();
                    return this.statementContainer;
                }
            }

            private void CheckStatementContainer()
            {
                if (this.statementContainer == null)
                    this.statementContainer = new BlockStatementParentContainer(this);
            }

            #region IBlockStatementParent Members

            public IReturnStatement Return()
            {
                return this.StatementContainer.Return();
            }

            public IReturnStatement Return(IExpression value)
            {
                return this.StatementContainer.Return(value);
            }

            public IConditionBlockStatement If(IExpression condition)
            {
                return this.StatementContainer.If(condition);
            }

            public ISwitchStatement Switch(IExpression caseCondition)
            {
                return this.StatementContainer.Switch(caseCondition);
            }

            public ICallFusionStatement Call(IExpressionToCommaFusionExpression target)
            {
                return this.StatementContainer.Call(target);
            }

            public ICallMethodStatement Call(IMethodInvokeExpression target)
            {
                return this.StatementContainer.Call(target);
            }

            public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(ptr, parameters);
            }

            public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(ptr, parameters);
            }

            public ICallMethodStatement Call(IMethodReferenceStub stub, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(stub, parameters);
            }

            public ICallMethodStatement Call(IMethodReferenceStub stub, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(stub, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(parent, methodName, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(parent, methodName, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(string methodName, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(methodName, parameters);
            }

            public ICallMethodStatement Call(string methodName, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(methodName, parameters);
            }

            public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(callType, methodName, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(callType, methodName, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
            {
                return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
            }

            public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
            {
                return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
            }

            #endregion

            #region IControlledStateCollection<IStatement> Members

            public int Count
            {
                get
                {
                    this.CheckStatementContainer();
                    return this.statementContainer.Count;
                }
            }

            public bool Contains(IStatement item)
            {
                this.CheckStatementContainer();
                return this.statementContainer.Contains(item);
            }

            public void CopyTo(IStatement[] array, int arrayIndex)
            {
                this.CheckStatementContainer();
                this.statementContainer.CopyTo(array, arrayIndex);
            }

            public IStatement this[int index]
            {
                get
                {
                    this.CheckStatementContainer();
                    return this.statementContainer[index];
                }
            }

            public IStatement[] ToArray()
            {
                this.CheckStatementContainer();
                return this.statementContainer.ToArray();
            }

            #endregion

            #region IEnumerable<IStatement> Members

            public IEnumerator<IStatement> GetEnumerator()
            {
                this.CheckStatementContainer();
                return this.statementContainer.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

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
                    if (value == IntermediateGateway.CommonlyUsedTypeReferences.Void)
                        throw new InvalidOperationException("Cannot yield a void type.");
                    this.resultedType = value;
                }
            }

            #endregion

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
        }
    }
}
