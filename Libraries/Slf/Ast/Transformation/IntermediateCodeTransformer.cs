using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    public partial class IntermediateTreeTransformer :
        IExpressionVisitor<IExpression>,
        IInclusionVisitor <IScopeCoercion>,
        IStatementVisitor <IStatement>,
        IExpressionVisitor<TransformationImpact>,
        IInclusionVisitor <TransformationImpact>,
        IStatementVisitor <TransformationImpact>,
        ILinqBodyVisitor<TransformationImpact>,
        ILinqClauseVisitor<TransformationImpact>,
        IPrimitiveVisitor<TransformationImpact>
    {
        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IAnonymousMethodExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IAnonymousMethodWithParametersExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IAssignmentExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IAwaitExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IAwaitStatementExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IBlockExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IBoundLocalReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ICommaExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ICommentExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IConditionalExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IConstructorInvokeExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IConstructorPointerReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ICreateArrayDetailExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ICreateArrayExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ICreateArrayNestedDetailExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ICreateInstanceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ICreateInstanceUnboundMemberAssignment expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IDecoratingExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IDefaultValueExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IDelegateHolderReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IDelegateInvokeExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IDelegateMethodPointerReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IDelegateReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IDirectionExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IEventInvokeExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IEventReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit<TEvent, TEventParameter, TEventParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IExplicitStringLiteralDecorationExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IExpressionFusionExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IExpressionToCommaFusionExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IExpressionToCommaTypeReferenceFusionExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IFieldReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IIndexerReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ILocalReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IMethodInvokeExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IMethodPointerReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IMethodReferenceStub expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(INamedParameterExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(INewLineExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IParameterReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>(IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter> expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IParenthesizedExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IPropertyReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit<TProperty, TPropertyParent>(IPropertySignatureReferenceExpression<TProperty, TPropertyParent> expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ISpecialReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IStaticGetMemberHandleExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ISymbolExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ITypeCastExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ITypeOfExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ITypeReferenceExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IUnaryOperationExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IVariadicTypeCastExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ILambdaTypedSimpleExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ILambdaTypedStatementExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ILambdaTypeInferredSimpleExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ILambdaTypeInferredStatementExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ILinqExpression expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(ILinqRangeVariableReference expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IExpressionVisitor<TransformationImpact>.Visit(IMetadatumDefinitionExpressionParameter expression)
        {
            return CalculateRefactorImpact(expression);
        }

        TransformationImpact IInclusionVisitor<TransformationImpact>.Visit(INamedInclusionRenameScopeCoercion inclusion)
        {
            return CalculateRefactorImpact(inclusion);
        }

        TransformationImpact IInclusionVisitor<TransformationImpact>.Visit(INamedInclusionScopeCoercion inclusion)
        {
            return CalculateRefactorImpact(inclusion);
        }

        TransformationImpact IInclusionVisitor<TransformationImpact>.Visit(INamespaceInclusionRenameScopeCoercion inclusion)
        {
            return CalculateRefactorImpact(inclusion);
        }

        TransformationImpact IInclusionVisitor<TransformationImpact>.Visit(INamespaceInclusionScopeCoercion inclusion)
        {
            return CalculateRefactorImpact(inclusion);
        }

        TransformationImpact IInclusionVisitor<TransformationImpact>.Visit(IStaticInclusionScopeCoercion inclusion)
        {
            return CalculateRefactorImpact(inclusion);
        }

        TransformationImpact IInclusionVisitor<TransformationImpact>.Visit(ITypeInclusionRenameScopeCoercion inclusion)
        {
            return CalculateRefactorImpact(inclusion);
        }

        TransformationImpact IInclusionVisitor<TransformationImpact>.Visit(ITypeInclusionScopeCoercion inclusion)
        {
            return CalculateRefactorImpact(inclusion);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IBlockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IBreakStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ICallFusionStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ICallMethodStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IChangeEventHandlerStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ICommentStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IConditionBlockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IConditionContinuationStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IEnumerateSetBreakableBlockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IExplicitStringLiteralStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IExpressionStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IGoToCaseStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IGoToStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IIterationBlockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IIterationDeclarationBlockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IJumpStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IJumpTarget statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ILabelStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ILocalDeclarationsStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ILockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IReturnStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ISimpleIterationBlockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ISwitchCaseBlockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ISwitchStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IThrowStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(ITryStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IUsingBlockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IUsingExpressionBlockStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IWhileStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IYieldBreakStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact IStatementVisitor<TransformationImpact>.Visit(IYieldReturnStatement statement)
        {
            return CalculateRefactorImpact(statement);
        }

        TransformationImpact ILinqBodyBuilder.Visit(ILinqSelectBody linqBody)
        {
            return CalculateRefactorImpact(linqBody);
        }

        TransformationImpact ILinqBodyBuilder.Visit(ILinqGroupBody linqBody)
        {
            return CalculateRefactorImpact(linqBody);
        }

        TransformationImpact ILinqBodyBuilder.Visit(ILinqFusionGroupBody linqBody)
        {
            return CalculateRefactorImpact(linqBody);
        }

        TransformationImpact ILinqBodyBuilder.Visit(ILinqFusionSelectBody linqBody)
        {
            return CalculateRefactorImpact(linqBody);
        }

        TransformationImpact ILinqClauseVisitor<TransformationImpact>.Visit(ILinqFromClause linqClause)
        {
            throw new NotImplementedException();
        }

        TransformationImpact ILinqClauseVisitor<TransformationImpact>.Visit(ILinqJoinClause linqClause)
        {
            throw new NotImplementedException();
        }

        TransformationImpact ILinqClauseVisitor<TransformationImpact>.Visit(ILinqLetClause linqClause)
        {
            throw new NotImplementedException();
        }

        TransformationImpact ILinqClauseVisitor<TransformationImpact>.Visit(ILinqOrderByClause linqClause)
        {
            throw new NotImplementedException();
        }

        TransformationImpact ILinqClauseVisitor<TransformationImpact>.Visit(ILinqTypedFromClause linqClause)
        {
            throw new NotImplementedException();
        }

        TransformationImpact ILinqClauseVisitor<TransformationImpact>.Visit(ILinqTypedJoinClause linqClause)
        {
            throw new NotImplementedException();
        }

        TransformationImpact ILinqClauseVisitor<TransformationImpact>.Visit(ILinqWhereClause linqClause)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<Abstract.IType> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<bool> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<byte> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<char> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<decimal> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<double> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<short> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<int> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<long> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<sbyte> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<float> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<string> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<ushort> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<uint> primitive)
        {
            throw new NotImplementedException();
        }

        TransformationImpact IPrimitiveVisitor<TransformationImpact>.Visit(IPrimitiveExpression<ulong> primitive)
        {
            throw new NotImplementedException();
        }
    }
}
