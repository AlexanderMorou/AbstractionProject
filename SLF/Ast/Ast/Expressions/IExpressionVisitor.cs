using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an expression
    /// visitor.
    /// </summary>
    public interface IExpressionVisitor :
        ILinqVisitor,
        IPrimitiveVisitor
    {
        /// <summary>
        /// Visits a property reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPropertyReferenceExpression{TProperty, TPropertyParent}"/> to visit.</param>
        void Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression)
            where TProperty :
                IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent :
                IPropertyParent<TProperty, TPropertyParent>;
        /// <summary>
        /// Visits a property reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPropertySignatureReferenceExpression{TPropertySignature, TPropertySignatureParent}"/> to visit.</param>
        void Visit<TPropertySignature, TPropertySignatureParent>(IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> expression)
            where TPropertySignature :
                IPropertySignatureMember<TPropertySignature, TPropertySignatureParent>
            where TPropertySignatureParent :
                IPropertySignatureParent<TPropertySignature, TPropertySignatureParent>;
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IFieldReferenceExpression{TField, TFieldParent}"/>
        /// to visit.</param>
        void Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>;

        /// <summary>
        /// Visits a binary operation expression.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left operand of the 
        /// binary expression.</typeparam>
        /// <typeparam name="TRight">The type of the right operand of the
        /// binary expression.</typeparam>
        /// <param name="expression">The <see cref="IBinaryOperationExpression{TLeft, TRight}"/>
        /// to visit.</param>
        void Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
            where TLeft :
                INaryOperandExpression
            where TRight :
                INaryOperandExpression;
        /// <summary>
        /// Visits the indexer reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IIndexerReferenceExpression"/>
        /// to visit.</param>
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
        /// <summary>
        /// Visits the range variable of a language integrated query.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqRangeVariableReference"/>
        /// to visit.</param>
        void Visit(ILinqRangeVariableReference expression);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IEventReferenceExpression"/> 
        /// to visit.</param>
        void Visit(IEventReferenceExpression expression);
    }
    /// <summary>
    /// Defines properties and methods for working with an expression
    /// visitor.
    /// </summary>
    /// <typeparam name="TResult">The value returned upon visiting the expression.</typeparam>
    /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
    public interface IExpressionVisitor<TResult, TContext> :
        ILinqVisitor<TResult, TContext>,
        IPrimitiveVisitor<TResult, TContext>
    {
        /// <summary>
        /// Visits a binary operation expression.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left operand of the 
        /// binary expression.</typeparam>
        /// <typeparam name="TRight">The type of the right operand of the
        /// binary expression.</typeparam>
        /// <param name="expression">The <see cref="IBinaryOperationExpression{TLeft, TRight}"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression, TContext context)
            where TLeft :
                INaryOperandExpression
            where TRight :
                INaryOperandExpression;
        /// <summary>
        /// Visits the indexer reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IIndexerReferenceExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IIndexerReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits a conditional expression.
        /// </summary>
        /// <param name="expression">The <see cref="IConditionalExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IConditionalExpression expression, TContext context);
        /// <summary>
        /// Visits a unary operation expression.
        /// </summary>
        /// <param name="expression">The <see cref="IUnaryOperationExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IUnaryOperationExpression expression, TContext context);
        /// <summary>
        /// Visits a type cast expression.
        /// </summary>
        /// <param name="expression">The <see cref="ITypeCastExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ITypeCastExpression expression, TContext context);
        /// <summary>
        /// Visits a type of expression
        /// </summary>
        /// <param name="expression">The <see cref="ITypeOfExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ITypeOfExpression expression, TContext context);
        /// <summary>
        /// Visits a type reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ITypeReferenceExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ITypeReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits a variadic type cast expression.
        /// </summary>
        /// <param name="expression">The <see cref="IVariadicTypeCastExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IVariadicTypeCastExpression expression, TContext context);
        /// <summary>
        /// Visits a symbol expression.
        /// </summary>
        /// <param name="expression">The <see cref="ISymbolExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ISymbolExpression expression, TContext context);
        /// <summary>
        /// Visits an expression which obtains a member handle through a static
        /// reference.
        /// </summary>
        /// <param name="expression">The <see cref="IStaticGetMemberHandleExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IStaticGetMemberHandleExpression expression, TContext context);
        /// <summary>
        /// Visits a special reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ISpecialReferenceExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ISpecialReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits a property reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPropertyReferenceExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IPropertyReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits a property reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPropertyReferenceExpression{TProperty, TPropertyParent}"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression, TContext context)
            where TProperty :
                IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent :
                IPropertyParent<TProperty, TPropertyParent>;
        /// <summary>
        /// Visits a property reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPropertySignatureReferenceExpression{TPropertySignature, TPropertySignatureParent}"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit<TPropertySignature, TPropertySignatureParent>(IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> expression, TContext context)
            where TPropertySignature :
                IPropertySignatureMember<TPropertySignature, TPropertySignatureParent>
            where TPropertySignatureParent :
                IPropertySignatureParent<TPropertySignature, TPropertySignatureParent>;
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IFieldReferenceExpression{TField, TFieldParent}"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression, TContext context)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>;
        /// <summary>
        /// Visits a parenthesized expression.
        /// </summary>
        /// <param name="expression">The <see cref="IParenthesizedExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IParenthesizedExpression expression, TContext context);
        /// <summary>
        /// Visits a named parameter expression.
        /// </summary>
        /// <param name="expression">The <see cref="INamedParameterExpression"/> which designates
        /// the name and value of a parameter to pass into a method/constructor/indexer.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(INamedParameterExpression expression, TContext context);
        /// <summary>
        /// Visits a method pointer reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IMethodPointerReferenceExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IMethodPointerReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits a method invoke expression.
        /// </summary>
        /// <param name="expression">The <see cref="IMethodInvokeExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IMethodInvokeExpression expression, TContext context);
        /// <summary>
        /// Visits a local reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ILocalReferenceExpression"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILocalReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IFieldReferenceExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IFieldReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionToCommaTypeReferenceFusionExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IExpressionToCommaTypeReferenceFusionExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionToCommaFusionExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IExpressionToCommaFusionExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionFusionExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IExpressionFusionExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IEventInvokeExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IEventInvokeExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDirectionExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IDirectionExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateReferenceExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IDelegateReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateMethodPointerReferenceExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IDelegateMethodPointerReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateInvokeExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IDelegateInvokeExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateHolderReferenceExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IDelegateHolderReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateInstanceMemberAssignment"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ICreateInstanceMemberAssignment expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateInstanceExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ICreateInstanceExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ICreateArrayExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayNestedDetailExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ICreateArrayNestedDetailExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayDetailExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ICreateArrayDetailExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICommaExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ICommaExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAnonymousMethodWithParametersExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IAnonymousMethodWithParametersExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAnonymousMethodExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IAnonymousMethodExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypedStatementExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILambdaTypedStatementExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypeInferredStatementExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILambdaTypeInferredStatementExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypedSimpleExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILambdaTypedSimpleExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypeInferredSimpleExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILambdaTypeInferredSimpleExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IParameterReferenceExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IParameterReferenceExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IConstructorInvokeExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IConstructorInvokeExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="ctorPointerReference"/> provided.
        /// </summary>
        /// <param name="ctorPointerReference">The <see cref="IConstructorPointerReferenceExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IConstructorPointerReferenceExpression ctorPointerReference, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqExpression expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAssignmentExpression"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IAssignmentExpression expression, TContext context);
        /// <summary>
        /// Visits the range variable of a language integrated query.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqRangeVariableReference"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqRangeVariableReference expression, TContext context);
        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IEventReferenceExpression"/> 
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IEventReferenceExpression expression, TContext context);
    }
}
