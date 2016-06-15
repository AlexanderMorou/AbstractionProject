using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Walkers
{
    public class StackedMethodEquivalenceWalker :
        MethodEquivalenceWalker
    {
        private Stack<object> _stack = new Stack<object>();

        public override bool Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }

        public override bool Visit<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }

        public override bool Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }

        public override bool Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit<TPropertySignature, TPropertySignatureParent>(IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> expressionSegment, object context)
        {
            _stack.Push(expressionSegment);
            try
            {
                return base.Visit(expressionSegment, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(Slf.Ast.Expressions.IAnonymousMethodExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }

        public override bool Visit(IAnonymousMethodWithParametersExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IAssignmentExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IAwaitExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IBlockExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IBlockStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IBoundLocalReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICommaExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICommentExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IConditionalExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IConstructorInvokeExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IBreakStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICallFusionStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICallMethodStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IChangeEventHandlerStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICommentStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IConditionBlockStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IConditionContinuationStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IConstructorPointerReferenceExpression ctorPointerReference, object context)
        {
            _stack.Push(ctorPointerReference);
            try
            {
                return base.Visit(ctorPointerReference, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICreateArrayDetailExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICreateArrayExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICreateArrayNestedDetailExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICreateInstanceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ICreateInstanceUnboundMemberAssignment expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IDecoratingExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IDefaultValueExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IDelegateHolderReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IDelegateInvokeExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IDelegateMethodPointerReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IDelegateReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IDirectionExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IEnumerateSetBreakableBlockStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IEventInvokeExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IEventReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IExplicitStringLiteralDecorationExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IExplicitStringLiteralStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IExpressionFusionExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IExpressionStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IExpressionToCommaFusionExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IExpressionToCommaTypeReferenceFusionExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IFieldReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IGoToCaseStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IGoToStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IIndexerReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IIterationBlockStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IJumpStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IJumpTarget statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILabelStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILambdaTypedSimpleExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILambdaTypedStatementExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILambdaTypeInferredSimpleExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILambdaTypeInferredStatementExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqBody body, ILinqBody context)
        {
            _stack.Push(body);
            try
            {
                return base.Visit(body, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqFromClause linqClause, object context)
        {
            _stack.Push(linqClause);
            try
            {
                return base.Visit(linqClause, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqFusionGroupBody expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqFusionSelectBody expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqGroupBody expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqJoinClause linqClause, object context)
        {
            _stack.Push(linqClause);
            try
            {
                return base.Visit(linqClause, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqLetClause linqClause, object context)
        {
            _stack.Push(linqClause);
            try
            {
                return base.Visit(linqClause, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqOrderByClause linqClause, object context)
        {
            _stack.Push(linqClause);
            try
            {
                return base.Visit(linqClause, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqRangeVariableReference expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqSelectBody expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqTypedFromClause linqClause, object context)
        {
            _stack.Push(linqClause);
            try
            {
                return base.Visit(linqClause, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqTypedJoinClause linqClause, object context)
        {
            _stack.Push(linqClause);
            try
            {
                return base.Visit(linqClause, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILinqWhereClause linqClause, object context)
        {
            _stack.Push(linqClause);
            try
            {
                return base.Visit(linqClause, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILocalDeclarationsStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILocalReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ILockStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IMetadatumDefinitionExpressionParameter expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IMethodInvokeExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IMethodPointerReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IMethodReferenceStub member, object context)
        {
            _stack.Push(member);
            try
            {
                return base.Visit(member, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(INamedParameterExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(INewLineExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IParameterReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IParenthesizedExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<bool> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<byte> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<char> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<decimal> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<double> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<float> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<int> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<IType> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<long> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<sbyte> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<short> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<string> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<uint> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<ulong> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPrimitiveExpression<ushort> expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IPropertyReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IReturnStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ISimpleIterationBlockStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ISpecialReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IStaticGetMemberHandleExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ISwitchCaseBlockStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ISwitchStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ISymbolExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IThrowStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ITryStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ITypeCastExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ITypeOfExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(ITypeReferenceExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IUnaryOperationExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IUsingBlockStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IUsingExpressionBlockStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IVariadicTypeCastExpression expression, object context)
        {
            _stack.Push(expression);
            try
            {
                return base.Visit(expression, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IWhileStatement whileStatement, IStatement context)
        {
            _stack.Push(whileStatement);
            try
            {
                return base.Visit(whileStatement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IYieldBreakStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public override bool Visit(IYieldReturnStatement statement, IStatement context)
        {
            _stack.Push(statement);
            try
            {
                return base.Visit(statement, context);
            }
            finally
            {
                _stack.Pop();
            }
        }
        public IEnumerable ActiveContext
        {
            get
            {
                foreach (var element in this._stack)
                    yield return element;
            }
        }
    }
}