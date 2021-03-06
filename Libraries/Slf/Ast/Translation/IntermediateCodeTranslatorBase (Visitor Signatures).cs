 /* ----------------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.        |
 |  Version: 0.5.0.0                                                |
 |------------------------------------------------------------------|
 |  To ensure the code works properly,                              |
 |  please do not make any changes to the file.                     |
 |------------------------------------------------------------------|
 |  The specific language is C♯ (Runtime Version: 4.0.30319.42000)  |
 |  Sub-tool Name: C♯ Code Translator                               |
 |  Sub-tool Version: 1.0.0.0                                       |
 \---------------------------------------------------------------- */
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using System;
namespace AllenCopeland.Abstraction.Slf.Translation
{
  partial class IntermediateCodeTranslatorBase :
    IIntermediateTreeVisitor
  {
    void IExpressionVisitor.Visit(IAnonymousMethodExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IAnonymousMethodWithParametersExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IAssignmentExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IAwaitExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IAwaitStatementExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IBlockExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IBoundLocalReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ICommaExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ICommentExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IConditionalExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IConstructorInvokeExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IConstructorPointerReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ICreateArrayDetailExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ICreateArrayExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ICreateArrayNestedDetailExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ICreateInstanceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ICreateInstanceUnboundMemberAssignment expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IDecoratingExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IDefaultValueExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IDelegateHolderReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IDelegateInvokeExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IDelegateMethodPointerReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IDelegateReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IDirectionExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IEventInvokeExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IEventReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit<TEvent, TEventParameter, TEventParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IExplicitStringLiteralDecorationExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IExpressionFusionExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IExpressionToCommaFusionExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IExpressionToCommaTypeReferenceFusionExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IFieldReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IIndexerReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ILambdaTypedSimpleExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ILambdaTypedStatementExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ILambdaTypeInferredSimpleExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ILambdaTypeInferredStatementExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ILinqExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ILinqRangeVariableReference expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ILocalReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IMetadatumDefinitionExpressionParameter expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IMethodInvokeExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IMethodPointerReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IMethodReferenceStub expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(INamedParameterExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(INewLineExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IParameterReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>(IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter> expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IParenthesizedExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IPropertyReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit<TProperty, TPropertyParent>(IPropertySignatureReferenceExpression<TProperty, TPropertyParent> expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ISpecialReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IStaticGetMemberHandleExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ISymbolExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ITypeCastExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ITypeOfExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(ITypeReferenceExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IUnaryOperationExpression expression)
    {
      this.Translate(expression);
    }
    void IExpressionVisitor.Visit(IVariadicTypeCastExpression expression)
    {
      this.Translate(expression);
    }
    void ILinqBodyVisitor.Visit(ILinqFusionGroupBody linqBody)
    {
      this.Translate(linqBody);
    }
    void ILinqBodyVisitor.Visit(ILinqFusionSelectBody linqBody)
    {
      this.Translate(linqBody);
    }
    void ILinqBodyVisitor.Visit(ILinqGroupBody linqBody)
    {
      this.Translate(linqBody);
    }
    void ILinqBodyVisitor.Visit(ILinqSelectBody linqBody)
    {
      this.Translate(linqBody);
    }
    void ILinqClauseVisitor.Visit(ILinqFromClause linqClause)
    {
      this.Translate(linqClause);
    }
    void ILinqClauseVisitor.Visit(ILinqJoinClause linqClause)
    {
      this.Translate(linqClause);
    }
    void ILinqClauseVisitor.Visit(ILinqLetClause linqClause)
    {
      this.Translate(linqClause);
    }
    void ILinqClauseVisitor.Visit(ILinqOrderByClause linqClause)
    {
      this.Translate(linqClause);
    }
    void ILinqClauseVisitor.Visit(ILinqTypedFromClause linqClause)
    {
      this.Translate(linqClause);
    }
    void ILinqClauseVisitor.Visit(ILinqTypedJoinClause linqClause)
    {
      this.Translate(linqClause);
    }
    void ILinqClauseVisitor.Visit(ILinqWhereClause linqClause)
    {
      this.Translate(linqClause);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<IType> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<bool> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<byte> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<char> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<decimal> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<double> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<short> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<int> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<long> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<sbyte> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<float> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<string> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<ushort> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<uint> primitive)
    {
      this.Translate(primitive);
    }
    void IPrimitiveVisitor.Visit(IPrimitiveExpression<ulong> primitive)
    {
      this.Translate(primitive);
    }
    void IScopeCoercionVisitor.Visit(INamedInclusionRenameScopeCoercion scopeCoercion)
    {
      this.Translate(scopeCoercion);
    }
    void IScopeCoercionVisitor.Visit(INamedInclusionScopeCoercion scopeCoercion)
    {
      this.Translate(scopeCoercion);
    }
    void IScopeCoercionVisitor.Visit(INamespaceInclusionRenameScopeCoercion scopeCoercion)
    {
      this.Translate(scopeCoercion);
    }
    void IScopeCoercionVisitor.Visit(INamespaceInclusionScopeCoercion scopeCoercion)
    {
      this.Translate(scopeCoercion);
    }
    void IScopeCoercionVisitor.Visit(IStaticInclusionScopeCoercion scopeCoercion)
    {
      this.Translate(scopeCoercion);
    }
    void IScopeCoercionVisitor.Visit(ITypeInclusionRenameScopeCoercion scopeCoercion)
    {
      this.Translate(scopeCoercion);
    }
    void IScopeCoercionVisitor.Visit(ITypeInclusionScopeCoercion scopeCoercion)
    {
      this.Translate(scopeCoercion);
    }
    void IStatementVisitor.Visit(IBlockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IBreakStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ICallFusionStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ICallMethodStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IChangeEventHandlerStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ICommentStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IConditionBlockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IConditionContinuationStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IEnumerateSetBreakableBlockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IExplicitStringLiteralStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IExpressionStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IGoToCaseStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IGoToStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IIterationBlockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IIterationDeclarationBlockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IJumpStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IJumpTarget statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ILabelStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ILocalDeclarationsStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ILockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IReturnStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ISimpleIterationBlockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ISwitchCaseBlockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ISwitchStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IThrowStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(ITryStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IUsingBlockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IUsingExpressionBlockStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IWhileStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IYieldBreakStatement statement)
    {
      this.Translate(statement);
    }
    void IStatementVisitor.Visit(IYieldReturnStatement statement)
    {
      this.Translate(statement);
    }
    void IIntermediateDeclarationVisitor.Visit(IIntermediateAssembly intermediateDeclaration)
    {
      this.Translate(intermediateDeclaration);
    }
    void IIntermediateDeclarationVisitor.Visit(IIntermediateNamespaceDeclaration intermediateDeclaration)
    {
      this.Translate(intermediateDeclaration);
    }
    void IIntermediateMemberVisitor.Visit<TCoercionParent, TIntermediateCoercionParent>(IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit(IIntermediateEnumFieldMember intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TCoercionParent, TIntermediateCoercionParent>(IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit<TCoercionParent, TInterCoercionParent>(IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TInterCoercionParent> intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit(ILinqRangeVariable intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit(ILinqTypedRangeVariable intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateMemberVisitor.Visit(ILocalMember intermediateMember)
    {
      this.Translate(intermediateMember);
    }
    void IIntermediateTypeVisitor.Visit(IIntermediateClassType intermediateType)
    {
      this.Translate(intermediateType);
    }
    void IIntermediateTypeVisitor.Visit(IIntermediateDelegateType intermediateType)
    {
      this.Translate(intermediateType);
    }
    void IIntermediateTypeVisitor.Visit(IIntermediateEnumType intermediateType)
    {
      this.Translate(intermediateType);
    }
    void IIntermediateTypeVisitor.Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> intermediateType)
    {
      this.Translate(intermediateType);
    }
    void IIntermediateTypeVisitor.Visit(IIntermediateInterfaceType intermediateType)
    {
      this.Translate(intermediateType);
    }
    void IIntermediateTypeVisitor.Visit(IIntermediateStructType intermediateType)
    {
      this.Translate(intermediateType);
    }
  };
};
