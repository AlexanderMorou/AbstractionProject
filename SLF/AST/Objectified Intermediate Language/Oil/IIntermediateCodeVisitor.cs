using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Oil.Statements;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// intermediate code visitor.
    /// </summary>
    public interface IIntermediateCodeVisitor
    {
        void Visit(IIndexerReferenceExpression expression);
        /// <summary>
        /// Visits a conditional expression.
        /// </summary>
        /// <param name="expression">The <see cref="IConditionalExpression"/> to visit.</param>
        void Visit(IConditionalExpression expression);
        /// <summary>
        /// Visits a binary operation expression.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left operand of the expression.</typeparam>
        /// <typeparam name="TRight">The type of the right operand of the expression.</typeparam>
        /// <param name="expression">The <see cref="IBinaryOperationExpression{TLeft, TRight}"/> to visit.</param>
        void Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
            where TLeft :
                INaryOperandExpression
            where TRight :
                INaryOperandExpression;
        /// <summary>
        /// Visits a unary operation expression.
        /// </summary>
        /// <param name="expression">The <see cref="IUnaryOperationExpression"/> to visit.</param>
        void Visit(IUnaryOperationExpression expression);
        /// <summary>
        /// Visits a type cast expression.
        /// </summary>
        /// <param name="expression">The <see cref="ITypeCastExpression"/> to visit.</param>
        void Visit(ITypeCastExpression expression);
        /// <summary>
        /// Visits a type of expression
        /// </summary>
        /// <param name="expression">The <see cref="ITypeOfExpression"/> to visit.</param>
        void Visit(ITypeOfExpression expression);
        /// <summary>
        /// Visits a type reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ITypeReferenceExpression"/> to visit.</param>
        void Visit(ITypeReferenceExpression expression);
        /// <summary>
        /// Visits a variadic type cast expression.
        /// </summary>
        /// <param name="expression">The <see cref="IVariadicTypeCastExpression"/> to visit.</param>
        void Visit(IVariadicTypeCastExpression expression);
        /// <summary>
        /// Visits a symbol expression.
        /// </summary>
        /// <param name="expression">The <see cref="ISymbolExpression"/> to visit.</param>
        void Visit(ISymbolExpression expression);
        /// <summary>
        /// Visits an expression which obtains a member handle through a static
        /// reference.
        /// </summary>
        /// <param name="expression">The <see cref="IStaticGetMemberHandleExpression"/> to visit.</param>
        void Visit(IStaticGetMemberHandleExpression expression);
        /// <summary>
        /// Visits a special reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ISpecialReferenceExpression"/> to visit.</param>
        void Visit(ISpecialReferenceExpression expression);
        /// <summary>
        /// Visits a property reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPropertyReferenceExpression"/> to visit.</param>
        void Visit(IPropertyReferenceExpression expression);
        /// <summary>
        /// Visits a boolean primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<bool> expression);
        /// <summary>
        /// Visits a character primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<char> expression);
        /// <summary>
        /// Visits a string primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<string> expression);
        /// <summary>
        /// Visits a byte primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<byte> expression);
        /// <summary>
        /// Visits a sbyte primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<sbyte> expression);
        /// <summary>
        /// Visits an unsigned 16-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<ushort> expression);
        /// <summary>
        /// Visits a 16-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<short> expression);
        /// <summary>
        /// Visits an unsigned 32-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<uint> expression);
        /// <summary>
        /// Visits a 32-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<int> expression);
        /// <summary>
        /// Visits an unsigned 64-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<ulong> expression);
        /// <summary>
        /// Visits a 64-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<long> expression);
        /// <summary>
        /// Visits a single precision floating point primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<float> expression);
        /// <summary>
        /// Visits a double precision floating point primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<double> expression);
        /// <summary>
        /// Visits a decimal primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<decimal> expression);
        /// <summary>
        /// Visits a parenthesized expression.
        /// </summary>
        /// <param name="expression">The <see cref="IParenthesizedExpression"/> to visit.</param>
        void Visit(IParenthesizedExpression expression);
        /// <summary>
        /// Visits a named parameter expression.
        /// </summary>
        /// <param name="expression">The <see cref="INamedParameterExpression"/> which designates
        /// the name and value of a parameter to pass into a method/constructor/indexer.</param>
        void Visit(INamedParameterExpression expression);
        /// <summary>
        /// Visits a method pointer reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IMethodPointerReferenceExpression"/> to visit.</param>
        void Visit(IMethodPointerReferenceExpression expression);
        /// <summary>
        /// Visits a method invoke expression.
        /// </summary>
        /// <param name="expression">The <see cref="IMethodInvokeExpression"/> to visit.</param>
        void Visit(IMethodInvokeExpression expression);
        /// <summary>
        /// Visits a local reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ILocalReferenceExpression"/> to visit.</param>
        void Visit(ILocalReferenceExpression expression);
        /// <summary>
        /// Visits a fusion type collection target expression.
        /// </summary>
        /// <param name="expression">The <see cref="IFusionTypeCollectionTargetExpression"/> to visit.</param>
        void Visit(IFusionTypeCollectionTargetExpression expression);
        void Visit(IFieldReferenceExpression expression);
        void Visit(IExpressionToCommaTypeReferenceFusionExpression expression);
        void Visit(IExpressionToCommaFusionExpression expression);
        void Visit(IExpressionFusionExpression expression);
        void Visit(IEventInvokeExpression expression);
        void Visit(IDirectionExpression expression);
        void Visit(IDelegateReferenceExpression expression);
        void Visit(IDelegateMethodPointerReferenceExpression expression);
        void Visit(IDelegateInvokeExpression expression);
        void Visit(IDelegateHolderReferenceExpression expression);
        void Visit(ICreateInstanceMemberAssignExpression expression);
        void Visit(ICreateInstanceExpression expression);
        void Visit(ICreateArrayExpression expression);
        void Visit(ICreateArrayDetailExpression expression);
        void Visit(ICommaExpression expression);
        void Visit(IArrayDimensionDetailExpression expression);
        void Visit(IAnonymousMethodWithParametersExpression expression);
        void Visit(IAnonymousMethodExpression expression);
        void Visit(ILambdaTypedStatementExpression expression);
        void Visit(ILambdaTypeInferredStatementExpression expression);
        void Visit(ILambdaTypedSimpleExpression expression);
        void Visit(ILambdaTypeInferredSimpleExpression expression);
        void Visit(ILinqExpression expression);
        void Visit(ILinqSelectBody expression);
        void Visit(ILinqGroupBody expression);
        void Visit(ILinqFusionSelectBody expression);
        void Visit(ILinqFusionGroupBody expression);
        void Visit(ILinqDirectedOrderByClause linqClause);
        void Visit(ILinqDirectedOrderByGroupClause linqClause);
        void Visit(ILinqFromClause linqClause);
        void Visit(ILinqJoinClause linqClause);
        void Visit(ILinqLetClause linqClause);
        void Visit(ILinqOrderByClause linqClause);
        void Visit(ILinqOrderByGroupClause linqClause);
        void Visit(ILinqTypedFromClause linqClause);
        void Visit(ILinqTypedJoinClause linqClause);
        void Visit(ILinqWhereClause linqClause);
        void VisitNull();

        void Visit(IBlockStatement statement);
        void Visit(IBreakStatement statement);
        void Visit(ICallMethodStatement statement);
        void Visit(IConditionBlockStatement statement);
        void Visit(ICallFusionStatement statement);
        void Visit(IConditionContinuationStatement statement);
        void Visit(IEnumerationBlockStatement statement);
        void Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement);
        void Visit(IExpressionStatement statement);
        void Visit(IGoToStatement statement);
        void Visit(IIterationBlockStatement statement);
        void Visit(IJumpStatement statement);
        void Visit(ILabelStatement statement);
        void Visit(IReturnStatement statement);
        void Visit(ISimpleIterationBlockStatement statement);
        void Visit(ISwitchCaseBlockStatement statement);
        void Visit(ISwitchStatement statement);
        void Visit(ITryCatchStatement statement);
        void Visit(ITryStatement statement);

    }
}
