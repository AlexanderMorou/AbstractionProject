using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Oil.Statements;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IExpressionVisitor :
        ILinqVisitor,
        IIntermediatePrimitiveVisitor
    {
        void Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
            where TLeft :
                INaryOperandExpression
            where TRight :
                INaryOperandExpression;
        void Visit(IIndexerReferenceExpression expression);
        /// <summary>
        /// Visits a conditional expression.
        /// </summary>
        /// <param name="expression">The <see cref="IConditionalExpression"/> to visit.</param>
        void Visit(IConditionalExpression expression);
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
        void Visit(ICreateInstanceMemberAssignment expression);
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
        void Visit(IParameterReferenceExpression expression);
        void Visit(IConstructorInvokeExpression expression);
        void Visit(IConstructorPointerReferenceExpression constructorPointerReferenceExpression);
        void Visit(ILinqExpression expression);
        void Visit(IAssignmentExpression expression);
    }
}
