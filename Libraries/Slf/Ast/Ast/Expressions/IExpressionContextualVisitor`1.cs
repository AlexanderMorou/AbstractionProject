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
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
  /// <summary>
  /// Represents a basic visitor for expressions which has a <typeparamref name="TContext"/>
  /// relevant to the visit.
  /// </summary>
  /// <typeparam name="TContext">
  /// Denotes the type of context the members of the <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IExpressionContextualVisitor{TContext}"/>
  /// should receive along with the types that accept the visitor.
  /// </typeparam>
  public interface IExpressionContextualVisitor<TContext>
  {
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IAnonymousMethodExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IAnonymousMethodExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IAnonymousMethodWithParametersExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IAnonymousMethodWithParametersExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IAssignmentExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IAssignmentExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IAwaitExpression"/> relevant
    /// to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IAwaitExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IAwaitStatementExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IAwaitStatementExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IBinaryOperationExpression{TLeft,TRight}"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression, TContext context)
      where TLeft:
        INaryOperandExpression
      where TRight:
        INaryOperandExpression;
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IBlockExpression"/> relevant
    /// to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IBlockExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IBoundLocalReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IBoundLocalReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ICommaExpression"/> relevant
    /// to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ICommaExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ICommentExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ICommentExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IConditionalExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IConditionalExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IConstructorInvokeExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IConstructorInvokeExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IConstructorPointerReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IConstructorPointerReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ICreateArrayDetailExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ICreateArrayDetailExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ICreateArrayExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ICreateArrayExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ICreateArrayNestedDetailExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ICreateArrayNestedDetailExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ICreateInstanceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ICreateInstanceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ICreateInstanceFieldAssignment{TField,TFieldParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression, TContext context)
      where TField:
        IFieldMember<TField, TFieldParent>
      where TFieldParent:
        IFieldParent<TField, TFieldParent>;
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ICreateInstancePropertyAssignment{TProperty,TPropertyParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression, TContext context)
      where TProperty:
        IPropertySignatureMember<TProperty, TPropertyParent>
      where TPropertyParent:
        IPropertySignatureParent<TProperty, TPropertyParent>;
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ICreateInstanceUnboundMemberAssignment"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ICreateInstanceUnboundMemberAssignment expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IDecoratingExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IDecoratingExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IDefaultValueExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IDefaultValueExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IDelegateHolderReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IDelegateHolderReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IDelegateInvokeExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IDelegateInvokeExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IDelegateMethodPointerReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IDelegateMethodPointerReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IDelegateReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IDelegateReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IDirectionExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IDirectionExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IEventInvokeExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IEventInvokeExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IEventReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IEventReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IEventReferenceExpression{TEvent,TEventParameter,TEventParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit<TEvent, TEventParameter, TEventParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> expression, TContext context)
      where TEvent:
        IEventSignatureMember<TEvent, TEventParameter, TEventParent>
      where TEventParameter:
        IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
      where TEventParent:
        IEventSignatureParent<TEvent, TEventParameter, TEventParent>;
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IExplicitStringLiteralDecorationExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IExplicitStringLiteralDecorationExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IExpressionFusionExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IExpressionFusionExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IExpressionToCommaFusionExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IExpressionToCommaFusionExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IExpressionToCommaTypeReferenceFusionExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IExpressionToCommaTypeReferenceFusionExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IFieldReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IFieldReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IFieldReferenceExpression{TField,TFieldParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression, TContext context)
      where TField:
        IFieldMember<TField, TFieldParent>
      where TFieldParent:
        IFieldParent<TField, TFieldParent>;
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IIndexerReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IIndexerReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ILocalReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ILocalReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IMethodInvokeExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IMethodInvokeExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IMethodPointerReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IMethodPointerReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IMethodReferenceStub"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IMethodReferenceStub expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IMethodReferenceStub{TSignatureParameter,TSignature,TParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> expression, TContext context)
      where TSignatureParameter:
        IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
      where TSignature:
        IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
      where TParent:
        ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>;
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.INamedParameterExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(INamedParameterExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.INewLineExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(INewLineExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IParameterReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IParameterReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IParameterReferenceExpression{TParameterParent,TIntermediateParameterParent,TParameter,TIntermediateParameter}"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>(IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter> expression, TContext context)
      where TParameterParent:
        IParameterParent<TParameterParent, TParameter>
      where TIntermediateParameterParent:
        TParameterParent,
        IIntermediateParameterParent<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>
      where TParameter:
        IParameterMember<TParameterParent>
      where TIntermediateParameter:
        TParameter,
        IIntermediateParameterMember<TParameterParent, TIntermediateParameterParent>;
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IParenthesizedExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IParenthesizedExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPropertyReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IPropertyReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPropertyReferenceExpression{TProperty,TPropertyParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression, TContext context)
      where TProperty:
        IPropertyMember<TProperty, TPropertyParent>
      where TPropertyParent:
        IPropertyParent<TProperty, TPropertyParent>;
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPropertySignatureReferenceExpression{TProperty,TPropertyParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit<TProperty, TPropertyParent>(IPropertySignatureReferenceExpression<TProperty, TPropertyParent> expression, TContext context)
      where TProperty:
        IPropertySignatureMember<TProperty, TPropertyParent>
      where TPropertyParent:
        IPropertySignatureParent<TProperty, TPropertyParent>;
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ISpecialReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ISpecialReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IStaticGetMemberHandleExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IStaticGetMemberHandleExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ISymbolExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ISymbolExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ITypeCastExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ITypeCastExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ITypeOfExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ITypeOfExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.ITypeReferenceExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ITypeReferenceExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IUnaryOperationExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IUnaryOperationExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IVariadicTypeCastExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IVariadicTypeCastExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda.ILambdaTypedSimpleExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ILambdaTypedSimpleExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda.ILambdaTypedStatementExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ILambdaTypedStatementExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda.ILambdaTypeInferredSimpleExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ILambdaTypeInferredSimpleExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda.ILambdaTypeInferredStatementExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ILambdaTypeInferredStatementExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqExpression"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ILinqExpression expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqRangeVariableReference"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ILinqRangeVariableReference expression, TContext context);
    /// <summary>
    /// Visits the <paramref name="expression"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="expression">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.IMetadatumDefinitionExpressionParameter"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IMetadatumDefinitionExpressionParameter expression, TContext context);
  };
};