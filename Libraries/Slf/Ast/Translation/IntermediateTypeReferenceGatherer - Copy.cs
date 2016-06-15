using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Translation
{
    public partial class IntermediateTypeReferenceGatherer :
        IIntermediateTreeVisitor
    {
        private List<IType> types;
        private object locker = new object();
        private bool allowPartials;
        private readonly List<IIntermediateNamespaceDeclaration> observedNamespaces;
        public IntermediateTypeReferenceGatherer(bool allowPartials)
        {
            this.observedNamespaces = new List<IIntermediateNamespaceDeclaration>();
            this.allowPartials = allowPartials;
        }

        public IEnumerable<IIntermediateNamespaceDeclaration> ObservedNamespaces
        {
            get
            {
                foreach (var @namespace in this.observedNamespaces)
                    yield return @namespace;
            }
        }

        public void Gather(IScopeCoercionCollection collection)
        {
            foreach (var element in collection)
                element.Accept(this);
        }

        public void Gather(IEnumerable<IStatement> statements)
        {
            foreach (var statement in statements)
                statement.Accept(this);
        }

        public void Gather(IEnumerable<IExpression> expressions)
        {
            foreach (var expression in expressions)
                expression.Accept(this);
        }

        public void Gather(IEnumerable<ICreateInstanceMemberAssignment> expressions)
        {
            foreach (var expression in expressions)
                expression.Accept(this);
        }
        public void Gather(IEnumerable<IIntermediateNamespaceDeclaration> namespaces)
        {
            foreach (var @namespace in namespaces)
                @namespace.Accept(this);
        }

        public void Gather(IEnumerable<ILinqClause> clauses)
        {
            foreach (var clause in clauses)
                clause.Accept(this);
        }

        public void Gather(IEnumerable<IIntermediateMember> members)
        {
            foreach (var member in members)
                member.Accept(this);
        }

        public void Gather(IEnumerable<IIntermediateType> series)
        {
            foreach (var element in series)
                element.Accept(this);
        }

        public IEnumerable<IType> GatherTypes(IIntermediateAssembly targetAssembly)
        {
            this.types = new List<IType>();
            targetAssembly.Accept(this);
            return (from t in types
                    orderby t.NamespaceName,
                            t.FullName
                    select t).ToArray().SinglePass();
        }

        private void CheckTypes(IEnumerable<IType> typeCollection)
        {
            if (typeCollection == null)
                return;
            foreach (var type in typeCollection)
                this.CheckType(type);
        }

        private void CheckType(IType type)
        {
            if (type == null)
                return;
            if (type is IGenericType)
            {
                var genericType = (IGenericType)type;
                if (genericType.IsGenericConstruct && !genericType.IsGenericDefinition)
                    CheckType(genericType.ElementType);
                foreach (var gp in genericType.GenericParameters)
                    this.CheckType(gp);
            }
            lock (locker)
                if (!this.types.Contains(type))
                    this.types.Add(type);
        }

        public void VisitNull() { }
#if false
        #region IExpressionVisitor Members

        public void Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression)
            where TProperty : IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertyParent<TProperty, TPropertyParent>
        {
            if (expression == null)
                return;
            if (expression.Source != null)
                expression.Source.Accept(this);
        }

        public void Visit<TPropertySignature, TPropertySignatureParent>(IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> expression)
            where TPropertySignature : IPropertySignatureMember<TPropertySignature, TPropertySignatureParent>
            where TPropertySignatureParent : IPropertySignatureParent<TPropertySignature, TPropertySignatureParent>
        {
            if (expression == null)
                return;
            if (expression.Source != null)
                expression.Source.Accept(this);
        }

        public void Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression)
            where TField : IFieldMember<TField, TFieldParent>
            where TFieldParent : IFieldParent<TField, TFieldParent>
        {
            if (expression == null)
                return;
            if (expression.Source != null)
                expression.Source.Accept(this);
        }

        public void Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
            where TLeft : INaryOperandExpression
            where TRight : INaryOperandExpression
        {
            if (expression == null)
                return;
            if (expression.Type == ExpressionKind.BinaryForwardTerm)
            {
                if (expression.Associativity == BinaryOperationAssociativity.Left)
                    expression.RightSide.Accept(this);
                else if (expression.Associativity == BinaryOperationAssociativity.Right)
                    expression.LeftSide.Accept(this);
            }
            else
            {
                expression.LeftSide.Accept(this);
                expression.RightSide.Accept(this);
            }
        }

        public void Visit(IIndexerReferenceExpression expression)
        {
            if (expression == null)
                return;
            if (expression.Source != null)
                expression.Source.Accept(this);
        }

        public void Visit(IConditionalExpression expression)
        {
            if (expression == null)
                return;
            if (expression.Type == ExpressionKind.ConditionalForwardTerm && expression.CheckPart != null)
                expression.CheckPart.Accept(this);
            else
            {
                expression.CheckPart.Accept(this);
                expression.TruePart.Accept(this);
                expression.FalsePart.Accept(this);
            }
        }

        public void Visit(IUnaryOperationExpression expression)
        {
            if (expression == null)
                return;
            expression.Term.Accept(this);
        }

        public void Visit(ITypeCastExpression expression)
        {
            if (expression == null)
                return;
            CheckType(expression.CastType);
            expression.Target.Accept(this);
        }

        public void Visit(ITypeOfExpression expression)
        {
            if (expression == null)
                return;
            CheckType(expression.ReferenceType);
        }

        public void Visit(ITypeReferenceExpression expression)
        {
            if (expression == null)
                return;

            this.CheckType(expression.ReferenceType);
        }

        public void Visit(IVariadicTypeCastExpression expression)
        {
            if (expression == null || expression.CastTypes == null || expression.CastTypes.Count == 0)
                return;
            expression.CastTypes.OnAll(k => CheckType(k));
        }

        public void Visit(ISymbolExpression expression)
        {
            if (expression == null)
                return;
            if (expression.Source != null)
                expression.Source.Accept(this);
        }

        public void Visit(IStaticGetMemberHandleExpression expression)
        {
        }

        public void Visit(ISpecialReferenceExpression expression)
        {
        }

        public void Visit(IPropertyReferenceExpression expression)
        {
            if (expression == null)
                return;
            if (expression.Source != null)
                expression.Source.Accept(this);
        }

        public void Visit(IParenthesizedExpression expression)
        {
            if (expression == null)
                return;
            expression.Parenthesized.Accept(this);
        }

        public void Visit(INamedParameterExpression expression)
        {
            if (expression == null)
                return;
            expression.Expression.Accept(this);
        }

        public void Visit(IMethodPointerReferenceExpression expression)
        {
            if (expression == null || expression.Reference == null || expression.Reference.GenericParameters == null)
                return;
            this.CheckTypes(expression.Reference.GenericParameters);
            if (expression.Signature == null)
                return;
            this.CheckTypes(expression.Signature);
        }

        public void Visit(IMethodInvokeExpression expression)
        {
            if (expression == null)
                return;
            foreach (var arg in expression.Arguments)
                arg.Accept(this);
            expression.Reference.Accept(this);
        }

        public void Visit(ILocalReferenceExpression expression)
        {
        }

        public void Visit(IFieldReferenceExpression expression)
        {
            if (expression == null || expression.Source == null)
                return;
            expression.Source.Accept(this);
        }

        public void Visit(IExpressionToCommaTypeReferenceFusionExpression expression)
        {
            expression.Left.Accept(this);
            this.CheckTypes(expression.Right);
        }

        public void Visit(IExpressionToCommaFusionExpression expression)
        {
            expression.Left.Accept(this);
            this.Visit(expression.Right);
        }

        public void Visit(IExpressionFusionExpression expression)
        {
            expression.Left.Accept(this);
            expression.Right.Accept(this);
        }

        public void Visit(IEventInvokeExpression expression)
        {
            if (expression == null ||
                expression.Reference == null)
                return;
            expression.Reference.Accept(this);
        }

        public void Visit(IDirectionExpression expression)
        {
            expression.Directed.Accept(this);
        }

        public void Visit(IDelegateReferenceExpression expression)
        {
        }

        public void Visit(IDelegateMethodPointerReferenceExpression expression)
        {
        }

        public void Visit(IDelegateInvokeExpression expression)
        {
        }

        public void Visit(IDelegateHolderReferenceExpression expression)
        {
        }

        public void Visit(ICreateInstanceUnboundMemberAssignment expression)
        {
            expression.AssignValue.Accept(this);
        }

        public void Visit<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression)
            where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertySignatureParent<TProperty, TPropertyParent>
        {
            if (expression.Source != null)
                expression.Source.Accept(this);
            if (expression.MemberType != null)
                this.CheckType(expression.MemberType);
            if (expression.AssignValue != null)
                expression.AssignValue.Accept(this);
        }

        public void Visit<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression)
            where TField : IFieldMember<TField, TFieldParent>
            where TFieldParent : IFieldParent<TField, TFieldParent>
        {
            if (expression.Source != null)
                expression.Source.Accept(this);
            if (expression.MemberType != null)
                this.CheckType(expression.MemberType);
            if (expression.AssignValue != null)
                expression.AssignValue.Accept(this);
        }

        public void Visit(ICreateInstanceExpression expression)
        {
            if (expression == null )
                return;
            this.Visit((IConstructorInvokeExpression)expression);
            if (expression.Reference != null)
                expression.Reference.Accept(this);
            if (expression.MemberAssignments != null)
                foreach (var memberAssignment in expression.MemberAssignments.Values)
                    memberAssignment.AssignValue.Accept(this);
        }

        public void Visit(ICreateArrayExpression expression)
        {
            if (expression == null)
                return;
            this.CheckType(expression.ArrayType);
            if (expression.Sizes != null && expression.Sizes.Count > 0)
                this.Visit(expression.Sizes);
        }

        public void Visit(ICreateArrayNestedDetailExpression expression)
        {
            this.Visit(expression.Details);
        }

        public void Visit(ICreateArrayDetailExpression expression)
        {
            if (expression == null)
                return;
            this.CheckType(expression.ArrayType);
            if (expression.Sizes != null && expression.Sizes.Count > 0)
                this.Visit(expression.Sizes);
            this.Visit(expression.Details);
        }

        public void Visit(ICommaExpression expression)
        {
            this.Visit((IExpressionCollection)expression);
        }

        public void Visit(IAnonymousMethodWithParametersExpression expression)
        {
            foreach (var param in expression.Parameters.Values)
                param.Accept(this);
            expression.Block.Accept(this);
        }

        public void Visit(IAnonymousMethodExpression expression)
        {
            this.Visit((IControlledCollection<IStatement>)expression);
        }

        public void Visit(ILambdaTypedStatementExpression expression)
        {
            if (expression == null)
                return;
            foreach (var param in expression.Parameters.Values)
                param.Accept(this);
            expression.Block.Accept(this);
        }

        public void Visit(ILambdaTypeInferredStatementExpression expression)
        {
            if (expression == null)
                return;
            foreach (var param in expression.Parameters.Values)
                param.Accept(this);
            expression.Block.Accept(this);
        }

        public void Visit(ILambdaTypedSimpleExpression expression)
        {
            if (expression == null)
                return;
            foreach (var param in expression.Parameters.Values)
                param.Accept(this);
            expression.Block.Expression.Accept(this);
        }

        public void Visit(ILambdaTypeInferredSimpleExpression expression)
        {
            if (expression == null)
                return;
            foreach (var param in expression.Parameters.Values)
                param.Accept(this);
            expression.Block.Expression.Accept(this);
        }

        public void Visit(IParameterReferenceExpression expression)
        {
        }

        public void Visit<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>(IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter> expression)
            where TParameterParent : IParameterParent<TParameterParent, TParameter>
            where TIntermediateParameterParent : TParameterParent, IIntermediateParameterParent<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>
            where TParameter : IParameterMember<TParameterParent>
            where TIntermediateParameter : TParameter, IIntermediateParameterMember<TParameterParent, TIntermediateParameterParent>
        {
            if (expression == null)
                return;
            expression.ReferenceTarget.Accept(this);
        }

        public void Visit(IConstructorInvokeExpression expression)
        {
            if (expression == null)
                return;
            expression.Reference.Accept(this);
            foreach (var param in expression.Arguments)
                param.Accept(this);
        }

        public void Visit(IConstructorPointerReferenceExpression ctorPointerReference)
        {
            if (ctorPointerReference == null)
                return;
            if (ctorPointerReference.Reference != null)
                this.CheckType(ctorPointerReference.Reference.InstanceType);
        }

        public void Visit(ILinqExpression expression)
        {
            expression.From.Accept(this);
            expression.Body.Accept(this);
        }

        public void Visit(IAssignmentExpression expression)
        {
            if (expression.Operation == AssignmentOperation.Term)
            {
                if (expression.Associativity == BinaryOperationAssociativity.Left)
                    expression.RightSide.Accept(this);
                else if (expression.Associativity == BinaryOperationAssociativity.Right)
                    expression.LeftSide.Accept(this);
            }
            else
            {
                expression.LeftSide.Accept(this);
                expression.RightSide.Accept(this);
            }
        }

        public void Visit(ILinqRangeVariableReference expression)
        {
            expression.Target.Accept(this);
        }

        public void Visit(IEventReferenceExpression expression)
        {
            if (expression == null)
                return;
            if (expression.Source != null)
                expression.Source.Accept(this);
        }

        public void Visit<TEvent, TEventParameter, TEventParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> expression)
            where TEvent : IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter : IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        {
            if (expression == null)
                return;
            if (expression.Source != null)
                expression.Source.Accept(this);
            if (expression.MemberType == null)
                return;
            this.CheckType(expression.MemberType);
        }

        public void Visit(ICommentExpression expression)
        {
        }
        public void Visit(IDecoratingExpression expression)
        {
            expression.ContainedExpression.Accept(this);
        }

        public void Visit(INewLineExpression expression)
        {
        }
        #endregion

        #region ILinqVisitor Members

        public void Visit(ILinqSelectBody expression)
        {
            foreach (var clause in expression.Clauses)
                clause.Accept(this);
            expression.Selection.Accept(this);
        }

        public void Visit(ILinqGroupBody expression)
        {
            foreach (var clause in expression.Clauses)
                clause.Accept(this);
            expression.Selection.Accept(this);
            expression.Key.Accept(this);
        }

        public void Visit(ILinqFusionSelectBody expression)
        {
            foreach (var clause in expression.Clauses)
                clause.Accept(this);
            expression.Selection.Accept(this);
            expression.Next.Accept(this);
        }

        public void Visit(ILinqFusionGroupBody expression)
        {
            foreach (var clause in expression.Clauses)
                clause.Accept(this);
            expression.Selection.Accept(this);
            expression.Key.Accept(this);
            expression.Next.Accept(this);
        }

        public void Visit(ILinqFromClause linqClause)
        {
            linqClause.RangeVariable.Accept(this);
            linqClause.RangeSource.Accept(this);
        }

        public void Visit(ILinqJoinClause linqClause)
        {
            linqClause.RangeVariable.Accept(this);
            linqClause.RangeSource.Accept(this);
            linqClause.LeftSelector.Accept(this);
            linqClause.RightSelector.Accept(this);
            if (linqClause.IntoRangeVariable != null)
                linqClause.IntoRangeVariable.Accept(this);
        }

        public void Visit(ILinqLetClause linqClause)
        {
            linqClause.RangeVariable.Accept(this);
            linqClause.RangeSource.Accept(this);
        }

        public void Visit(ILinqOrderByClause linqClause)
        {
            if (linqClause != null &&
                linqClause.Orderings != null)
                foreach (var ordering in linqClause.Orderings)
                if (ordering.OrderingKey != null)
                    ordering.OrderingKey.Accept(this);
        }

        public void Visit(ILinqTypedFromClause linqClause)
        {
            linqClause.RangeVariable.Accept(this);
            linqClause.RangeSource.Accept(this);
        }

        public void Visit(ILinqTypedJoinClause linqClause)
        {
            linqClause.RangeVariable.Accept(this);
            linqClause.RangeSource.Accept(this);
            linqClause.LeftSelector.Accept(this);
            linqClause.RightSelector.Accept(this);
            if (linqClause.IntoRangeVariable != null)
                linqClause.IntoRangeVariable.Accept(this);
        }

        public void Visit(ILinqWhereClause linqClause)
        {
            linqClause.Condition.Accept(this);
        }

        #endregion

        #region IPrimitiveVisitor Members

        public void Visit(IPrimitiveExpression<bool> expression)
        {
        }

        public void Visit(IPrimitiveExpression<char> expression)
        {
        }

        public void Visit(IPrimitiveExpression<string> expression)
        {
        }

        public void Visit(IPrimitiveExpression<byte> expression)
        {
        }

        public void Visit(IPrimitiveExpression<sbyte> expression)
        {
        }

        public void Visit(IPrimitiveExpression<ushort> expression)
        {
        }

        public void Visit(IPrimitiveExpression<short> expression)
        {
        }

        public void Visit(IPrimitiveExpression<uint> expression)
        {
        }

        public void Visit(IPrimitiveExpression<int> expression)
        {
        }

        public void Visit(IPrimitiveExpression<ulong> expression)
        {
        }

        public void Visit(IPrimitiveExpression<long> expression)
        {
        }

        public void Visit(IPrimitiveExpression<float> expression)
        {
        }

        public void Visit(IPrimitiveExpression<double> expression)
        {
        }

        public void Visit(IPrimitiveExpression<decimal> expression)
        {
        }

        public void Visit(IPrimitiveExpression<IType> expression)
        {
            if (expression != null && expression.Value != null)
                this.CheckType(expression.Value);
        }

        public void VisitNull()
        {
        }

        #endregion

        #region IStatementVisitor Members

        public void Visit(IBlockStatement statement)
        {
            this.Visit((IControlledCollection<IStatement>)statement);
            this.Visit((IIntermediateTypeParent)statement);
        }

        public void Visit(IBreakStatement statement)
        {
        }

        public void Visit(ICallMethodStatement statement)
        {
            if (statement != null &&
                statement.Target != null)
                statement.Target.Accept(this);
        }

        public void Visit(IConditionBlockStatement statement)
        {
            statement.Condition.Accept(this);
            this.Visit((IBlockStatement)statement);
            if (statement.HasNext)
                statement.Next.Accept(this);
        }

        public void Visit(ICallFusionStatement statement)
        {
            statement.Target.Accept(this);
        }

        public void Visit(IConditionContinuationStatement statement)
        {
            this.Visit((IBlockStatement)statement);
        }

        public void Visit(IEnumerateSetBreakableBlockStatement statement)
        {
            if (statement.Local != null)
                statement.Local.Accept(this);
            if (statement.Source != null)
                statement.Source.Accept(this);
        }

        public void Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement)
        {
            this.CheckType(statement.LocalType);
        }

        public void Visit(IExpressionStatement statement)
        {
            statement.Expression.Accept(this);
        }

        public void Visit(IGoToStatement statement)
        {
        }

        public void Visit(IJumpTarget statement)
        {
        }

        public void Visit(IIterationDeclarationBlockStatement statement)
        {
            if (statement == null)
                return;
            if (statement.Iterations != null)
                foreach (var expression in statement.Iterations)
                    if (expression != null)
                        expression.Accept(this);
            if (statement.Condition != null)
                statement.Condition.Accept(this);
            if (statement.LocalDeclaration != null)
                statement.LocalDeclaration.Accept(this);
        }

        public void Visit(IIterationBlockStatement statement)
        {
            if (statement == null)
                return;
            if (statement.Iterations != null)
                foreach (var expression in statement.Iterations)
                    if (expression != null)
                        expression.Accept(this);
            if (statement.Condition != null)
                statement.Condition.Accept(this);
            if (statement.Initializers != null)
                foreach (var expression in statement.Initializers)
                    if (expression != null)
                        expression.Accept(this);
        }

        public void Visit(IJumpStatement statement)
        {
        }

        public void Visit(ILabelStatement statement)
        {
        }

        public void Visit(IReturnStatement statement)
        {
            if (statement != null && statement.ReturnValue != null)
                statement.ReturnValue.Accept(this);
        }

        public void Visit(ISimpleIterationBlockStatement statement)
        {
            if (statement == null)
                return;
            if (statement.Incremental != null)
                statement.Incremental.Accept(this);
            if (statement.Start != null)
                statement.Start.Accept(this);
            if (statement.End != null)
                statement.End.Accept(this);
            if (statement.Target != null)
                statement.Target.Accept(this);
        }

        public void Visit(ISwitchCaseBlockStatement statement)
        {
            foreach (var @case in statement.Cases)
                if (@case != null)
                    @case.Accept(this);
            this.Visit((IBlockStatement)statement);
        }

        public void Visit(ISwitchStatement statement)
        {
            if (statement == null)
                return;
            if (statement.Selection != null)
                statement.Selection.Accept(this);

            foreach (var set in statement)
                set.Accept(this);
        }

        public void Visit(ITryStatement statement)
        {
            this.Visit((IBlockStatement)statement);
            foreach (var typedBlock in statement.Values)
                if (typedBlock != null)
                {
                    if (typedBlock.CaughtException != null)
                        this.CheckType(typedBlock.CaughtException);
                    typedBlock.Accept(this);
                }
            if (statement.HasCatchAll)
                statement.CatchAll.Accept(this);
            if (statement.HasFinally)
                statement.Finally.Accept(this);
        }

        public void Visit(ILocalDeclarationsStatement statement)
        {
            foreach (var local in statement.DeclaredLocals)
                local.Accept(this);
        }

        public void Visit(ILocalMember local)
        {
            if (local == null)
                return;
            if (local.TypingMethod == LocalTypingKind.Explicit)
            {
                var typedLocal = (ITypedLocalMember)local;
                if (typedLocal.LocalType != null)
                    this.CheckType(typedLocal.LocalType);
            }
            if (local.InitializationExpression != null)
                local.InitializationExpression.Accept(this);
        }

        public void Visit(IChangeEventHandlerStatement statement)
        {
            statement.SourceMethod.Accept(this);
        }

        public void Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
            where TEvent : IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter : IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter : IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature : IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent : ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            statement.SourceMethod.Accept(this);
        }

        public void Visit(ICommentStatement statement)
        {
        }

        #endregion

        #region IIntermediateDeclarationVisitor Members

        public void Visit(IIntermediateAssembly assembly)
        {
            if (!(allowPartials || assembly.IsRoot))
                return;
            this.Visit((IIntermediateTypeParent)assembly);
            if (allowPartials)
            {
                foreach (var method in assembly.Methods.ExclusivelyOnParent())
                    method.Value.Accept(this);
                foreach (var field in assembly.Fields.ExclusivelyOnParent())
                    field.Value.Accept(this);
                foreach (var @namespace in assembly.Namespaces.ExclusivelyOnParent())
                    @namespace.Value.Accept(this);
            }
            else
            {
                foreach (var method in assembly.Methods)
                    method.Value.Accept(this);
                foreach (var field in assembly.Fields)
                    field.Value.Accept(this);
                foreach (var @namespace in assembly.Namespaces)
                    @namespace.Value.Accept(this);
            }
        }

        public void Visit(IIntermediateNamespaceDeclaration @namespace)
        {
            bool observed = false;
            if (!(allowPartials || @namespace.IsRoot))
                return;
            if (this.Visit((IIntermediateTypeParent)@namespace))
                observed = true;
            if (allowPartials)
            {
                foreach (var method in @namespace.Methods.ExclusivelyOnParent())
                {
                    if (!observed)
                        observed = true;
                    method.Value.Accept(this);
                }
                foreach (var field in @namespace.Fields.ExclusivelyOnParent())
                {
                    if (!observed)
                        observed = true;
                    field.Value.Accept(this);
                }
                foreach (var childspace in @namespace.Namespaces.ExclusivelyOnParent())
                {
                    childspace.Value.Accept(this);
                    if (!observed)
                        observed = true;
                }
            }
            else
            {
                foreach (var method in @namespace.Methods)
                {
                    method.Value.Accept(this);
                    if (!observed)
                        observed = true;
                }
                foreach (var field in @namespace.Fields)
                {
                    field.Value.Accept(this);
                    if (!observed)
                        observed = true;
                }
                foreach (var childspace in @namespace.Namespaces)
                {
                    childspace.Value.Accept(this);
                    if (!observed)
                        observed = true;
                }
            }
            if (observed)
                this.observedNamespaces.Add(@namespace);
        }

        #endregion

        #region IIntermediateTypeVisitor Members

        public void Visit(IIntermediateClassType @class)
        {
            if (!(allowPartials || @class.IsRoot))
                return;
            //this.CheckType(@class);
            this.Visit((IIntermediateTypeParent)@class);
            foreach (var type in ((IType)@class).GetDirectlyImplementedInterfaces())
                this.CheckType(type);
            if (@class.BaseType != null)
                this.CheckType(@class.BaseType);
            if (@class.IsGenericConstruct && @class.TypeParameters != null &&
                @class.TypeParameters.Values != null && @class.TypeParameters.Values.Count > 0)
                foreach (var genericParam in @class.TypeParameters.Values)
                    if (genericParam != null)
                        genericParam.Accept(this);
            if (allowPartials)
            {
                foreach (var member in @class.Members.ExclusivelyOnParent(@class))
                    if (member.Value.Entry != null)
                        member.Value.Entry.Accept(this);
            }
            else
            {
                foreach (var member in @class.Members.Values)
                    if (member.Entry != null)
                        member.Entry.Accept(this);
            }
            if (@class.IsRoot)
                this.CheckTypes(@class.GetDirectlyImplementedInterfaces());
        }

        public void Visit(IIntermediateDelegateType @delegate)
        {
            //this.CheckType(@delegate);
            if (@delegate.IsGenericConstruct && @delegate.TypeParameters != null &&
                @delegate.TypeParameters.Values != null && @delegate.TypeParameters.Values.Count > 0)
                foreach (var genericParam in @delegate.TypeParameters.Values)
                        if (genericParam != null)
                        genericParam.Accept(this);
            if (@delegate.Parameters != null && @delegate.Parameters.Values != null && @delegate.Parameters.Values.Count > 0)
                foreach (var param in @delegate.Parameters.Values)
                    param.Accept(this);
        }

        public void Visit(IIntermediateEnumType @enum)
        {
            foreach (var member in @enum.Members.Values)
                if (member.Entry != null)
                    member.Entry.Accept(this);
        }

        public void Visit(IIntermediateInterfaceType @interface)
        {
            //this.CheckType(@interface);
            if (!(allowPartials || @interface.IsRoot))
                return;
            this.Visit((IIntermediateTypeParent)@interface);
            if (@interface.IsGenericConstruct && @interface.TypeParameters != null &&
                @interface.TypeParameters.Values != null && @interface.TypeParameters.Values.Count > 0)
                foreach (var genericParam in @interface.TypeParameters.Values)
                    if (genericParam != null)
                        genericParam.Accept(this);
            if (allowPartials)
            {
                foreach (var member in @interface.Members.ExclusivelyOnParent(@interface))
                    if (member.Value.Entry != null)
                        member.Value.Entry.Accept(this);

            }
            else
            {
                foreach (var member in @interface.Members.Values)
                    if (member.Entry != null)
                        member.Entry.Accept(this);
            }
            if (@interface.IsRoot)
                this.CheckTypes(@interface.ImplementedInterfaces);
        }

        public void Visit(IIntermediateStructType @struct)
        {
            if (!(allowPartials || @struct.IsRoot))
                return;
            //this.CheckType(@struct);
            this.Visit((IIntermediateTypeParent)@struct);
            foreach (var type in ((IType)@struct).GetDirectlyImplementedInterfaces())
                this.CheckType(type);
            if (@struct.IsGenericConstruct && @struct.TypeParameters != null &&
                @struct.TypeParameters.Values != null && @struct.TypeParameters.Values.Count > 0)
                foreach (var genericParam in @struct.TypeParameters.Values)
                    if (genericParam != null)
                        genericParam.Accept(this);
            if (allowPartials)
            {
                foreach (var member in @struct.Members.ExclusivelyOnParent(@struct))
                    if (member.Value.Entry != null)
                        member.Value.Entry.Accept(this);
            }
            else
            {
                foreach (var member in @struct.Members.Values)
                    if (member.Entry != null)
                        member.Entry.Accept(this);
            }
            if (@struct.IsRoot)
                this.CheckTypes(@struct.GetDirectlyImplementedInterfaces());
        }

        public void Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
            where TGenericParameter : IGenericParameter<TGenericParameter, TParent>
            where TIntermediateGenericParameter : TGenericParameter, IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            where TParent : IGenericParamParent<TGenericParameter, TParent>
            where TIntermediateParent : TParent, IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        {
            if (parameter == null)
                return;

            if (parameter.Members != null && parameter.Members.Values != null && parameter.Members.Count > 0)
                foreach (var member in parameter.Members.Values)
                    if (member.Entry != null)
                        member.Entry.Accept(this);
            if (parameter.Constraints != null && parameter.Constraints.Count > 0)
                this.CheckTypes(parameter.Constraints);
        }

        #endregion

        #region IIntermediateMemberVisitor Members

        public void Visit(IBlockStatementParent blockParent)
        {
            foreach (var local in blockParent.Locals.Values)
            {
                if (local.AutoDeclare)
                    this.Visit(local);
            }
        }

        public void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
            where TCtor : IConstructorMember<TCtor, TType>
            where TIntermediateCtor : TCtor, IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType : ICreatableParent<TCtor, TType>
            where TIntermediateType : TType, IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            if (ctor == null)
                return;
            if (ctor.Parameters != null && ctor.Parameters.Values != null && ctor.Parameters.Values.Count > 0)
                foreach (var param in ctor.Parameters.Values)
                    param.Accept(this);
        }

        public void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
            where TCtor : IConstructorMember<TCtor, TType>
            where TIntermediateCtor : TCtor, IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType : ICreatableParent<TCtor, TType>
            where TIntermediateType : TType, IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            if (ctor == null)
                return;
            if (ctor.Parameters != null && ctor.Parameters.Values != null && ctor.Parameters.Values.Count > 0)
                foreach (var param in ctor.Parameters.Values)
                    param.Accept(this);
            this.Visit((IIntermediateTypeParent)ctor);
            this.Visit(((IControlledCollection<IStatement>)ctor));
            this.Visit((IBlockStatementParent)ctor);
        }

        public void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
            where TEvent : IEventMember<TEvent, TEventParent>
            where TIntermediateEvent : TEvent, IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent : IEventParent<TEvent, TEventParent>
            where TIntermediateEventParent : TEventParent, IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        {
            if (@event == null)
                return;
            if (@event.SignatureSource == EventSignatureSource.Declared && @event.Parameters != null && @event.Parameters.Values != null && @event.Parameters.Values.Count > 0)
                foreach (var param in @event.Parameters.Values)
                    param.Accept(this);
            else if (@event.SignatureSource == EventSignatureSource.Delegate && @event.SignatureType != null)
                this.CheckType(@event.SignatureType);
            this.Visit((IIntermediateTypeParent)@event);
            this.Visit(((IControlledCollection<IStatement>)@event));
            this.Visit((IBlockStatementParent)@event);
        }

        public void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
            where TEvent : IEventSignatureMember<TEvent, TEventParent>
            where TIntermediateEvent : TEvent, IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParent>
            where TIntermediateEventParent : TEventParent, IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        {
            if (@event == null)
                return;
            if (@event.SignatureSource == EventSignatureSource.Declared && @event.Parameters != null && @event.Parameters.Values != null && @event.Parameters.Values.Count > 0)
                foreach (var param in @event.Parameters.Values)
                    param.Accept(this);
            else if (@event.SignatureSource == EventSignatureSource.Delegate && @event.SignatureType != null)
                this.CheckType(@event.SignatureType);
        }


        public void Visit<TCoercionParent, TIntermediateCoercionParent>(IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> binaryCoercion)
            where TCoercionParent :
                ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
            where TIntermediateCoercionParent : 
                IIntermediateCoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>,
                IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>, TCoercionParent
        {
            if (binaryCoercion.ContainingSide != BinaryOpCoercionContainingSide.Both)
                this.CheckType(binaryCoercion.OtherSide);

            this.Visit((IControlledCollection<IStatement>)(binaryCoercion));
            this.Visit((IIntermediateTypeParent)binaryCoercion);
            this.Visit((IBlockStatementParent)binaryCoercion);
        }

        public void Visit<TCoercionParent, TIntermediateCoercionParent>(IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent> typeCoercion)
            where TCoercionParent : 
                ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
            where TIntermediateCoercionParent : 
                IIntermediateCoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, 
                IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>, TCoercionParent
        {
            this.CheckType(typeCoercion.CoercionType);
            this.Visit((IControlledCollection<IStatement>)(typeCoercion));
            this.Visit((IIntermediateTypeParent)typeCoercion);
            this.Visit((IBlockStatementParent)typeCoercion);
        }

        public void Visit<TCoercionParent, TIntermediateCoercionParent>(IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> unaryCoercion)
            where TCoercionParent : 
                ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
            where TIntermediateCoercionParent : 
                IIntermediateCoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, 
                IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>, TCoercionParent
        {
            this.CheckType(unaryCoercion.ResultedType);
            this.Visit((IControlledCollection<IStatement>)(unaryCoercion));
            this.Visit((IIntermediateTypeParent)unaryCoercion);
            this.Visit((IBlockStatementParent)unaryCoercion);
        }
        
        public void Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TIntermediateField :
                TField, 
                IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>
            where TIntermediateFieldParent :    
                TFieldParent,
                IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
        {
            if (field == null)
                return;
            if (field.InitializationExpression != null)
                field.InitializationExpression.Accept(this);
            this.CheckType(field.FieldType);
        }

        public void Visit(IIntermediateEnumFieldMember field)
        {
            if (field.Value != null && field.Value.ValueType == EnumValueType.Mixed &&
                field.Value is IIntermediateEnumFieldExpressionValue)
                ((IIntermediateEnumFieldExpressionValue)field.Value).Value.Accept(this);

        }

        public void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
            where TIndexer : IIndexerMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer : TIndexer, IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent : IIndexerParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        {
            if (indexer == null)
                return;
            this.CheckType(indexer.PropertyType);
            if (indexer.CanRead && indexer.GetMethod != null)
                indexer.GetMethod.Accept(this);
            if (indexer.CanWrite && indexer.SetMethod != null)
                indexer.SetMethod.Accept(this);
        }

        public void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
            where TIndexer : IIndexerSignatureMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer : TIndexer, IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent : IIndexerSignatureParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        {
            this.CheckType(indexerSignature.PropertyType);
        }

        public void Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
            where TMethod : IMethodMember<TMethod, TMethodParent>
            where TIntermediateMethod : IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod
            where TMethodParent : IMethodParent<TMethod, TMethodParent>
            where TIntermediateMethodParent : IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethodParent
        {
            foreach (var parameter in method.Parameters.Values)
                parameter.Accept(this);
            if (method.IsGenericConstruct && method.IsGenericConstruct)
                foreach (var genericParameter in method.TypeParameters.Values)
                    genericParameter.Accept(this);
            this.Visit((IControlledCollection<IStatement>)(method));
            this.Visit((IIntermediateTypeParent)method);
            this.CheckType(method.ReturnType);
            this.Visit((IBlockStatementParent)method);
        }

        public void Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
            where TSignature : IMethodSignatureMember<TSignature, TParent>
            where TIntermediateSignature : TSignature, IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            where TParent : IMethodSignatureParent<TSignature, TParent>
            where TIntermediateParent : TParent, IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        {
            if (methodSignature.IsGenericConstruct && methodSignature.IsGenericConstruct)
                foreach (var genericParameter in methodSignature.TypeParameters.Values)
                    genericParameter.Accept(this);
            foreach (var parameter in methodSignature.Parameters.Values)
                parameter.Accept(this);
            this.CheckType(methodSignature.ReturnType);
        }

        public void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
            where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
            where TIntermediateProperty : TProperty, IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent : IPropertySignatureParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        {
            if (propertySignature == null)
                return;
            this.CheckType(propertySignature.PropertyType);
            if (propertySignature.CanRead && propertySignature.GetMethod != null)
                propertySignature.GetMethod.Accept(this);
            if (propertySignature.CanWrite && propertySignature.SetMethod != null)
                propertySignature.SetMethod.Accept(this);
        }

        public void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
            where TProperty : IPropertyMember<TProperty, TPropertyParent>
            where TIntermediateProperty : TProperty, IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent : IPropertyParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        {
            if (property == null)
                return;
            this.CheckType(property.PropertyType);
            if (property.CanRead && property.GetMethod != null)
                property.GetMethod.Accept(this);
            if (property.CanWrite && property.SetMethod != null)
                property.SetMethod.Accept(this);
        }

        public void Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
            where TParent : IParameterParent
            where TIntermediateParent : TParent, IIntermediateParameterParent
        {
            this.CheckType(parameter.ParameterType);
        }

        public void Visit(ILinqRangeVariable rangeVariable)
        {
            
        }

        public void Visit(ILinqTypedRangeVariable rangeVariable)
        {
            if (rangeVariable == null)
                return;
            this.CheckType(rangeVariable.RangeType);
        }

        #endregion

        #region IInclusionVisitor Members

        public void Visit(INamedInclusionScopeCoercion namedInclusion)
        {
        }

        public void Visit(INamedInclusionRenameScopeCoercion renamedInclusion)
        {
        }

        public void Visit(INamespaceInclusionScopeCoercion namespaceInclusion)
        {
        }

        public void Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion)
        {
        }

        public void Visit(ITypeInclusionScopeCoercion typeInclusion)
        {
        }

        public void Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion)
        {
        }

        public void Visit(IStaticInclusionScopeCoercion staticInclusion)
        {
        }

        public void Visit(IMetadatumDefinitionExpressionParameter expression)
        {
            if (expression.Value != null)
                expression.Value.Accept(this);
        }
        #endregion

        private bool Visit(IIntermediateTypeParent parent)
        {
            bool result = false;
            if (allowPartials)
            {
                foreach (var type in parent.Types.ExclusivelyOnParent())
                {
                    if (type.Value.Entry != null)
                    {
                        type.Value.Entry.Accept(this);
                        result = true;
                    }
                }
            }
            else
            {
                foreach (var type in parent.Types.Values)
                {
                    if (type.Entry != null)
                        type.Entry.Accept(this);
                    result = true;
                }
            }
            return result;
        }

        public void Visit(IExpressionCollection target)
        {
            if (target != null)
                foreach (var expression in target)
                    if (expression != null)
                        expression.Accept(this);
        }

        private void CheckType(IType type)
        {
            if (type == null)
                return;
            if (type is IGenericType)
            {
                var genericType = (IGenericType)type;
                if (genericType.IsGenericConstruct && !genericType.IsGenericDefinition)
                    CheckType(genericType.ElementType);
                foreach (var gp in genericType.GenericParameters)
                    this.CheckType(gp);
            }
            lock (locker)
                if (!this.types.Contains(type))
                    this.types.Add(type);
        }
        private void CheckTypes(IControlledTypeCollection typeCollection)
        {
            foreach (var type in typeCollection)
                this.CheckType(type);
        }

        private void Visit(IControlledCollection<IStatement> statements)
        {
            if (statements == null)
                return;
            foreach (var statement in statements)
                if (statements != null)
                    statement.Accept(this);
        }

        public void Visit<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> member)
            where TSignatureParameter : IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
            where TSignature : IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
            where TParent : ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
        {
        }

        public void Visit(IMethodReferenceStub expressionSegment)
        {
            expressionSegment.Source.Accept(this);
        }

        public IEnumerable<IIntermediateNamespaceDeclaration> ObservedNamespaces
        {
            get
            {
                foreach (var @namespace in this.observedNamespaces)
                    yield return @namespace;
            }
        }


        public void Visit(IBoundLocalReferenceExpression expression)
        {
            
        }


        public void Visit(IUsingBlockStatement statement)
        {
            statement.ResourceAcquisition.Accept(this);
            this.Visit((IControlledCollection<IStatement>)statement);
        }

        public void Visit(IUsingExpressionBlockStatement statement)
        {
            statement.ResourceAcquisition.Accept(this);
            this.Visit((IControlledCollection<IStatement>)statement);
        }

        public void Visit(IThrowStatement statement)
        {
            if (statement.ThrowTarget != null)
                statement.ThrowTarget.Accept(this);
        }

        public void Visit(ILockStatement statement)
        {
            this.Visit((IControlledCollection<IStatement>)statement);
        }

        public void Visit(IDefaultValueExpression defaultValueExpression)
        {
            if (defaultValueExpression != null)
                CheckType(defaultValueExpression.TypeToDefault);
        }


        public void Visit(IYieldReturnStatement statement)
        {
            if (statement != null && statement.YieldedResult != null)
                statement.YieldedResult.Accept(this);
        }

        public void Visit(IYieldBreakStatement statement)
        {
        }


        public void Visit(IWhileStatement whileStatement)
        {
            this.Visit((IControlledCollection<IStatement>)whileStatement);
        }


        public void Visit(IGoToCaseStatement statement)
        {

        }


        public void Visit(IAwaitExpression expression)
        {
            if (expression != null && expression.Proffer != null)
                expression.Proffer.Accept(this);
        }

        public void Visit(IAwaitStatementExpression expression)
        {
            if (expression != null && expression.Proffer != null)
                expression.Proffer.Accept(this);
        }

        public void Visit(IExplicitStringLiteralDecorationExpression expression) { }

        public void Visit(IBlockExpression expression)
        {
            if (expression != null && expression.Block != null)
                expression.Block.Accept(this);
        }

        public void Visit(IExplicitStringLiteralStatement statement) { }
#endif
    }
}
