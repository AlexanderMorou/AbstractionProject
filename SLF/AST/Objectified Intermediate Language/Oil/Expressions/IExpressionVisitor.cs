using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an expression
    /// visitor.
    /// </summary>
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
        /// <param name="expression">The <see cref="IMalleableTypeOfExpression"/> to visit.</param>
        void Visit(IMalleableTypeOfExpression expression);
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
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IFieldReferenceExpression"/>
        /// to visit.</param>
        void Visit(IFieldReferenceExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionToCommaTypeReferenceFusionExpression"/>
        /// to visit.</param>
        void Visit(IExpressionToCommaTypeReferenceFusionExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionToCommaFusionExpression"/>
        /// to visit.</param>
        void Visit(IExpressionToCommaFusionExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionFusionExpression"/>
        /// to visit.</param>
        void Visit(IExpressionFusionExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IEventInvokeExpression"/>
        /// to visit.</param>
        void Visit(IEventInvokeExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDirectionExpression"/>
        /// to visit.</param>
        void Visit(IDirectionExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateReferenceExpression"/>
        /// to visit.</param>
        void Visit(IDelegateReferenceExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateMethodPointerReferenceExpression"/>
        /// to visit.</param>
        void Visit(IDelegateMethodPointerReferenceExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateInvokeExpression"/>
        /// to visit.</param>
        void Visit(IDelegateInvokeExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateHolderReferenceExpression"/>
        /// to visit.</param>
        void Visit(IDelegateHolderReferenceExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateInstanceMemberAssignment"/>
        /// to visit.</param>
        void Visit(ICreateInstanceMemberAssignment expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateInstanceExpression"/>
        /// to visit.</param>
        void Visit(ICreateInstanceExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayExpression"/>
        /// to visit.</param>
        void Visit(ICreateArrayExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayNestedDetailExpression"/>
        /// to visit.</param>
        void Visit(ICreateArrayNestedDetailExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayDetailExpression"/>
        /// to visit.</param>
        void Visit(ICreateArrayDetailExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICommaExpression"/>
        /// to visit.</param>
        void Visit(ICommaExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IArrayDimensionDetailExpression"/>
        /// to visit.</param>
        void Visit(IArrayDimensionDetailExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAnonymousMethodWithParametersExpression"/>
        /// to visit.</param>
        void Visit(IAnonymousMethodWithParametersExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAnonymousMethodExpression"/>
        /// to visit.</param>
        void Visit(IAnonymousMethodExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypedStatementExpression"/>
        /// to visit.</param>
        void Visit(ILambdaTypedStatementExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypeInferredStatementExpression"/>
        /// to visit.</param>
        void Visit(ILambdaTypeInferredStatementExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypedSimpleExpression"/>
        /// to visit.</param>
        void Visit(ILambdaTypedSimpleExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypeInferredSimpleExpression"/>
        /// to visit.</param>
        void Visit(ILambdaTypeInferredSimpleExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IParameterReferenceExpression"/>
        /// to visit.</param>
        void Visit(IParameterReferenceExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IConstructorInvokeExpression"/>
        /// to visit.</param>
        void Visit(IConstructorInvokeExpression expression);
        /// <summary>
        /// Visits the <paramref name="ctorPointerReference"/> provided.
        /// </summary>
        /// <param name="ctorPointerReference">The <see cref="IConstructorPointerReferenceExpression"/>
        /// to visit.</param>
        void Visit(IConstructorPointerReferenceExpression ctorPointerReference);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqExpression"/>
        /// to visit.</param>
        void Visit(ILinqExpression expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAssignmentExpression"/>
        /// to visit.</param>
        void Visit(IAssignmentExpression expression);
        void Visit(ILinqRangeVariableReference expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IEventReferenceExpression"/> 
        /// to visit.</param>
        void Visit(IEventReferenceExpression expression);
    }
}
