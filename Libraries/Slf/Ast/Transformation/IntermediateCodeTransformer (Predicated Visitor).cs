using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    partial class IntermediateTreeTransformer
    {
        public abstract TransformationImpact CalculateRefactorImpact(IBlockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
            where TEvent : IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter : IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter : IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature : IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent : ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;

        public abstract TransformationImpact CalculateRefactorImpact(IBreakStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(ICallFusionStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(ICallMethodStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IChangeEventHandlerStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(ICommentStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IConditionBlockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IConditionContinuationStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IEnumerateSetBreakableBlockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IExplicitlyTypedLocalVariableDeclarationStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IExplicitStringLiteralStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IExpressionStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IGoToCaseStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IGoToStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IIterationBlockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IIterationDeclarationBlockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IJumpStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IJumpTarget statement);

        public abstract TransformationImpact CalculateRefactorImpact(ILabelStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(ILocalDeclarationsStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(ILockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IReturnStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(ISimpleIterationBlockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(ISwitchCaseBlockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(ISwitchStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IThrowStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(ITryStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IUsingBlockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IUsingExpressionBlockStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IWhileStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IYieldBreakStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(IYieldReturnStatement statement);

        public abstract TransformationImpact CalculateRefactorImpact(INamedInclusionRenameScopeCoercion inclusion);

        public abstract TransformationImpact CalculateRefactorImpact(INamedInclusionScopeCoercion inclusion);

        public abstract TransformationImpact CalculateRefactorImpact(INamespaceInclusionRenameScopeCoercion inclusion);

        public abstract TransformationImpact CalculateRefactorImpact(INamespaceInclusionScopeCoercion inclusion);

        public abstract TransformationImpact CalculateRefactorImpact(IStaticInclusionScopeCoercion inclusion);

        public abstract TransformationImpact CalculateRefactorImpact(ITypeInclusionRenameScopeCoercion inclusion);

        public abstract TransformationImpact CalculateRefactorImpact(ITypeInclusionScopeCoercion inclusion);

        public abstract TransformationImpact CalculateRefactorImpact(IAnonymousMethodExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IAnonymousMethodWithParametersExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IAssignmentExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IAwaitExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IAwaitStatementExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
            where TLeft :
                INaryOperandExpression
            where TRight :
                INaryOperandExpression;

        public abstract TransformationImpact CalculateRefactorImpact(IBlockExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IBoundLocalReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ICommaExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ICommentExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IConditionalExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IConstructorInvokeExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IConstructorPointerReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ICreateArrayDetailExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ICreateArrayExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ICreateArrayNestedDetailExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ICreateInstanceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>;

        public abstract TransformationImpact CalculateRefactorImpact<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression)
            where TProperty :
                IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent :
                IPropertySignatureParent<TProperty, TPropertyParent>;

        public abstract TransformationImpact CalculateRefactorImpact(ICreateInstanceUnboundMemberAssignment expression);

        public abstract TransformationImpact CalculateRefactorImpact(IDecoratingExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IDefaultValueExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IDelegateHolderReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IDelegateInvokeExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IDelegateMethodPointerReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IDelegateReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IDirectionExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IEventInvokeExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IEventReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact<TEvent, TEventParameter, TEventParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> expression)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>;

        public abstract TransformationImpact CalculateRefactorImpact(IExplicitStringLiteralDecorationExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IExpressionFusionExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IExpressionToCommaFusionExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IExpressionToCommaTypeReferenceFusionExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IFieldReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>;

        public abstract TransformationImpact CalculateRefactorImpact(IIndexerReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ILocalReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IMethodInvokeExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IMethodPointerReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IMethodReferenceStub expression);

        public abstract TransformationImpact CalculateRefactorImpact<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> expression)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
            where TParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>;

        public abstract TransformationImpact CalculateRefactorImpact(INamedParameterExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(INewLineExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IParameterReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>(IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter> expression)
            where TParameterParent : IParameterParent<TParameterParent, TParameter>
            where TIntermediateParameterParent : TParameterParent, IIntermediateParameterParent<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>
            where TParameter : IParameterMember<TParameterParent>
            where TIntermediateParameter : TParameter, IIntermediateParameterMember<TParameterParent, TIntermediateParameterParent>;

        public abstract TransformationImpact CalculateRefactorImpact(IParenthesizedExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IPropertyReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression)
            where TProperty : IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertyParent<TProperty, TPropertyParent>;

        public abstract TransformationImpact CalculateRefactorImpact<TProperty, TPropertyParent>(IPropertySignatureReferenceExpression<TProperty, TPropertyParent> expression)
            where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertySignatureParent<TProperty, TPropertyParent>;

        public abstract TransformationImpact CalculateRefactorImpact(ISpecialReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IStaticGetMemberHandleExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ISymbolExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ITypeCastExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ITypeOfExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ITypeReferenceExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IUnaryOperationExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(IVariadicTypeCastExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ILambdaTypedSimpleExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ILambdaTypedStatementExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ILambdaTypeInferredSimpleExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ILambdaTypeInferredStatementExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ILinqExpression expression);

        public abstract TransformationImpact CalculateRefactorImpact(ILinqRangeVariableReference expression);

        public abstract TransformationImpact CalculateRefactorImpact(IMetadatumDefinitionExpressionParameter expression);

        public abstract TransformationImpact CalculateRefactorImpact(ILinqSelectBody linqBody);

        public abstract TransformationImpact CalculateRefactorImpact(ILinqGroupBody linqBody);

        public abstract TransformationImpact CalculateRefactorImpact(ILinqFusionGroupBody linqBody);

        public abstract TransformationImpact CalculateRefactorImpact(ILinqFusionSelectBody linqBody);

    }
}
