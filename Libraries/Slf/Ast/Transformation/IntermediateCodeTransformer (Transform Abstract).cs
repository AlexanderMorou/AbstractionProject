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
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    partial class IntermediateTreeTransformer
    {
        public abstract IStatement Transform(IBlockStatement statement);

        public abstract IStatement Transform<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
            where TEvent : IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter : IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter : IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature : IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent : ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;

        public abstract IStatement Transform(IBreakStatement statement);

        public abstract IStatement Transform(ICallFusionStatement statement);

        public abstract IStatement Transform(ICallMethodStatement statement);

        public abstract IStatement Transform(IChangeEventHandlerStatement statement);

        public abstract IStatement Transform(ICommentStatement statement);

        public abstract IStatement Transform(IConditionBlockStatement statement);

        public abstract IStatement Transform(IConditionContinuationStatement statement);

        public abstract IStatement Transform(IEnumerateSetBreakableBlockStatement statement);

        public abstract IStatement Transform(IExplicitlyTypedLocalVariableDeclarationStatement statement);

        public abstract IStatement Transform(IExplicitStringLiteralStatement statement);

        public abstract IStatement Transform(IExpressionStatement statement);

        public abstract IStatement Transform(IGoToCaseStatement statement);

        public abstract IStatement Transform(IGoToStatement statement);

        public abstract IStatement Transform(IIterationBlockStatement statement);

        public abstract IStatement Transform(IIterationDeclarationBlockStatement statement);

        public abstract IStatement Transform(IJumpStatement statement);

        public abstract IStatement Transform(IJumpTarget statement);

        public abstract IStatement Transform(ILabelStatement statement);

        public abstract IStatement Transform(ILocalDeclarationsStatement statement);

        public abstract IStatement Transform(ILockStatement statement);

        public abstract IStatement Transform(IReturnStatement statement);

        public abstract IStatement Transform(ISimpleIterationBlockStatement statement);

        public abstract IStatement Transform(ISwitchCaseBlockStatement statement);

        public abstract IStatement Transform(ISwitchStatement statement);

        public abstract IStatement Transform(IThrowStatement statement);

        public abstract IStatement Transform(ITryStatement statement);

        public abstract IStatement Transform(IUsingBlockStatement statement);

        public abstract IStatement Transform(IUsingExpressionBlockStatement statement);

        public abstract IStatement Transform(IWhileStatement statement);

        public abstract IStatement Transform(IYieldBreakStatement statement);

        public abstract IStatement Transform(IYieldReturnStatement statement);

        public abstract IScopeCoercion Transform(INamedInclusionRenameScopeCoercion inclusion);

        public abstract IScopeCoercion Transform(INamedInclusionScopeCoercion inclusion);

        public abstract IScopeCoercion Transform(INamespaceInclusionRenameScopeCoercion inclusion);

        public abstract IScopeCoercion Transform(INamespaceInclusionScopeCoercion inclusion);

        public abstract IScopeCoercion Transform(IStaticInclusionScopeCoercion inclusion);

        public abstract IScopeCoercion Transform(ITypeInclusionRenameScopeCoercion inclusion);

        public abstract IScopeCoercion Transform(ITypeInclusionScopeCoercion inclusion);

        public abstract IExpression Transform(IAnonymousMethodExpression expression);

        public abstract IExpression Transform(IAnonymousMethodWithParametersExpression expression);

        public abstract IExpression Transform(IAssignmentExpression expression);

        public abstract IExpression Transform(IAwaitExpression expression);

        public abstract IExpression Transform(IAwaitStatementExpression expression);

        public abstract IExpression Transform<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
            where TLeft :
                INaryOperandExpression
            where TRight :
                INaryOperandExpression;

        public abstract IExpression Transform(IBlockExpression expression);

        public abstract IExpression Transform(IBoundLocalReferenceExpression expression);

        public abstract IExpression Transform(ICommaExpression expression);

        public abstract IExpression Transform(ICommentExpression expression);

        public abstract IExpression Transform(IConditionalExpression expression);

        public abstract IExpression Transform(IConstructorInvokeExpression expression);

        public abstract IExpression Transform(IConstructorPointerReferenceExpression expression);

        public abstract IExpression Transform(ICreateArrayDetailExpression expression);

        public abstract IExpression Transform(ICreateArrayExpression expression);

        public abstract IExpression Transform(ICreateArrayNestedDetailExpression expression);

        public abstract IExpression Transform(ICreateInstanceExpression expression);

        public abstract IExpression Transform<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression)
            where TField : IFieldMember<TField, TFieldParent>
            where TFieldParent : IFieldParent<TField, TFieldParent>;

        public abstract IExpression Transform<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression)
            where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertySignatureParent<TProperty, TPropertyParent>;

        public abstract IExpression Transform(ICreateInstanceUnboundMemberAssignment expression);

        public abstract IExpression Transform(IDecoratingExpression expression);

        public abstract IExpression Transform(IDefaultValueExpression expression);

        public abstract IExpression Transform(IDelegateHolderReferenceExpression expression);

        public abstract IExpression Transform(IDelegateInvokeExpression expression);

        public abstract IExpression Transform(IDelegateMethodPointerReferenceExpression expression);

        public abstract IExpression Transform(IDelegateReferenceExpression expression);

        public abstract IExpression Transform(IDirectionExpression expression);

        public abstract IExpression Transform(IEventInvokeExpression expression);

        public abstract IExpression Transform(IEventReferenceExpression expression);

        public abstract IExpression Transform<TEvent, TEventParameter, TEventParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> expression)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>;

        public abstract IExpression Transform(IExplicitStringLiteralDecorationExpression expression);

        public abstract IExpression Transform(IExpressionFusionExpression expression);

        public abstract IExpression Transform(IExpressionToCommaFusionExpression expression);

        public abstract IExpression Transform(IExpressionToCommaTypeReferenceFusionExpression expression);

        public abstract IExpression Transform(IFieldReferenceExpression expression);

        public abstract IExpression Transform<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>;

        public abstract IExpression Transform(IIndexerReferenceExpression expression);

        public abstract IExpression Transform(ILocalReferenceExpression expression);

        public abstract IExpression Transform(IMethodInvokeExpression expression);

        public abstract IExpression Transform(IMethodPointerReferenceExpression expression);

        public abstract IExpression Transform(IMethodReferenceStub expression);

        public abstract IExpression Transform<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> expression)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
            where TParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>;

        public abstract IExpression Transform(INamedParameterExpression expression);

        public abstract IExpression Transform(INewLineExpression expression);

        public abstract IExpression Transform(IParameterReferenceExpression expression);

        public abstract IExpression Transform<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>(IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter> expression)
            where TParameterParent :
                IParameterParent<TParameterParent, TParameter>
            where TIntermediateParameterParent :
                TParameterParent, IIntermediateParameterParent<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>
            where TParameter :
                IParameterMember<TParameterParent>
            where TIntermediateParameter :
                TParameter,
                IIntermediateParameterMember<TParameterParent, TIntermediateParameterParent>;

        public abstract IExpression Transform(IParenthesizedExpression expression);

        public abstract IExpression Transform(IPropertyReferenceExpression expression);

        public abstract IExpression Transform<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression)
            where TProperty : IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertyParent<TProperty, TPropertyParent>;

        public abstract IExpression Transform<TProperty, TPropertyParent>(IPropertySignatureReferenceExpression<TProperty, TPropertyParent> expression)
            where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertySignatureParent<TProperty, TPropertyParent>;

        public abstract IExpression Transform(ISpecialReferenceExpression expression);

        public abstract IExpression Transform(IStaticGetMemberHandleExpression expression);

        public abstract IExpression Transform(ISymbolExpression expression);

        public abstract IExpression Transform(ITypeCastExpression expression);

        public abstract IExpression Transform(ITypeOfExpression expression);

        public abstract IExpression Transform(ITypeReferenceExpression expression);

        public abstract IExpression Transform(IUnaryOperationExpression expression);

        public abstract IExpression Transform(IVariadicTypeCastExpression expression);

        public abstract IExpression Transform(ILambdaTypedSimpleExpression expression);

        public abstract IExpression Transform(ILambdaTypedStatementExpression expression);

        public abstract IExpression Transform(ILambdaTypeInferredSimpleExpression expression);

        public abstract IExpression Transform(ILambdaTypeInferredStatementExpression expression);

        public abstract IExpression Transform(ILinqExpression expression);

        public abstract IExpression Transform(ILinqRangeVariableReference expression);

        public abstract IExpression Transform(IMetadatumDefinitionExpressionParameter expression);

    }
}
