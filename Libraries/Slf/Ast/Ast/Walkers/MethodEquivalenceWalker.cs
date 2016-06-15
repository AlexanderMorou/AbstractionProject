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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Walkers
{
    public class MethodEquivalenceWalker :
        IStatementVisitor<bool, IStatement>,
        ICommonExpressionVisitor<bool, object>
    {
        private object StripDecoration(object context)
        {
            if (context is IDecoratingExpression)
            {
                var decoratingContext = (IDecoratingExpression)context;
                return StripDecoration(decoratingContext.ContainedExpression);
            }
            return context;
        }

        public virtual bool Visit(IBlockExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IBlockExpression)
            {
                var blockContext = (IBlockExpression)context;
                if (expression.Type != blockContext.Type)
                    return false;
                return this.Visit((IEnumerable<IStatement>)expression.Block, blockContext.Block);
            }
            return false;
        }

        public virtual bool Visit(IExplicitStringLiteralDecorationExpression expression, object context)
        {
            /* Decoration is ignored. */
            return true;
        }

        public virtual bool Visit(IAwaitExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IAwaitExpression)
            {
                var awaitContext = (IAwaitExpression)context;
                return this.VisitCommon(expression.Proffer, awaitContext.Proffer);
            }
            return false;
        }

        public virtual bool Visit(IDefaultValueExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IDefaultValueExpression)
            {
                var defContext = (IDefaultValueExpression)context;
                return this.Visit(expression.TypeToDefault, defContext.TypeToDefault);
            }
            return false;
        }

        public virtual bool Visit(IMethodReferenceStub member, object context)
        {
            context = StripDecoration(context);
            if (context is IMethodReferenceStub)
            {
                var mrContext = (IMethodReferenceStub)context;
                if (member.Name != mrContext.Name)
                    return false;
                if (!this.Visit(member.GenericParameters, mrContext.GenericParameters))
                    return false;
                if (member.ReferenceType != mrContext.ReferenceType)
                    return false;
                return member.Source.Accept(this, mrContext.Source);
            }
            return false;
        }

        public virtual bool Visit<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> expressionSegment, object context)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
            where TParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
        {
            return Visit((IMethodReferenceStub)(expressionSegment), context);
        }

        public virtual bool Visit(IMetadatumDefinitionExpressionParameter expression, object context)
        {
            context = StripDecoration(context);
            if (context is IMetadatumDefinitionExpressionParameter)
            {
                var metadatumContext = (IMetadatumDefinitionExpressionParameter)context;
                return expression.Value.Accept(this, metadatumContext.Value);
            }
            return false;
        }

        public virtual bool Visit(INewLineExpression expression, object context)
        {
            return true;
        }

        public virtual bool Visit(IDecoratingExpression expression, object context)
        {
            var newExpression = (IExpression)StripDecoration(expression);
            var newContext = StripDecoration(context);
            return newExpression.Accept(this, newContext);
        }

        public virtual bool Visit(ICommentExpression expression, object context)
        {
            return true;
        }

        public virtual bool Visit(IEventReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IEventReferenceExpression)
            {
                var evRefContext = (IEventReferenceExpression)context;
                if (expression.Name != evRefContext.Name)
                    return false;
                return expression.Source.Accept(this, evRefContext.Source);
            }
            return false;
        }

        public virtual bool Visit(ILinqRangeVariableReference expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqRangeVariableReference)
            {
                var rangeVarContext = (ILinqRangeVariableReference)context;
                if (expression.Name != rangeVarContext.Name)
                    return false;
                return true;
            }
            return false;
        }

        public virtual bool Visit(IAssignmentExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IAssignmentExpression)
            {
                var assignContext = (IAssignmentExpression)context;
                if (expression.Operation != assignContext.Operation)
                    return false;
                if (expression.Operation == AssignmentOperation.Term)
                    if (expression.Associativity == BinaryOperationAssociativity.Left)
                        return expression.LeftSide.Accept(this, assignContext.LeftSide);
                    else
                        return expression.RightSide.Accept(this, assignContext.RightSide);
                return expression.LeftSide.Accept(this, assignContext.LeftSide) &&
                       expression.RightSide.Accept(this, assignContext.RightSide);
            }
            return false;
        }

        public virtual bool Visit(ILinqExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqExpression)
            {
                var linqContext = (ILinqExpression)(context);
                return this.Visit(expression.Body, linqContext.Body) && this.Visit(expression.From, linqContext.From);
            }
            return false;
        }

        public virtual bool Visit(IConstructorPointerReferenceExpression ctorPointerReference, object context)
        {
            context = StripDecoration(context);
            if (context is IConstructorPointerReferenceExpression)
            {
                var ctorContext = (IConstructorPointerReferenceExpression)context;
                if (!ctorPointerReference.Reference.InstanceType.Equals(ctorContext.Reference.InstanceType))
                    return false;
                return this.Visit(ctorPointerReference.Signature, ctorContext.Signature);
            }
            return false;
        }

        public virtual bool Visit(IConstructorInvokeExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IConstructorInvokeExpression)
            {
                var ctorContext = (IConstructorInvokeExpression)context;

                if (expression.Reference != null && ctorContext.Reference != null)
                    expression.Reference.Accept(this, ctorContext.Reference);
                return this.Visit(expression.Arguments, ctorContext.Arguments);
            }
            return false;
        }

        public virtual bool Visit(IParameterReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IParameterReferenceExpression)
            {
                var paramContext = (IParameterReferenceExpression)context;
                return expression.Name == paramContext.Name;
            }
            return false;
        }

        public virtual bool Visit(ILambdaTypeInferredSimpleExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILambdaTypeInferredSimpleExpression)
            {
                var lambdaContext = (ILambdaTypeInferredSimpleExpression)context;
                if (!expression.Block.Expression.Accept(this, lambdaContext.Block.Expression))
                    return false;
                try
                {
                    foreach (var paramPair in expression.Parameters.Values.EnumerateWith(lambdaContext.Parameters.Values))
                    {
                        if (paramPair.Item1 == null && paramPair.Item2 == null)
                            continue;
                        if (paramPair.Item1 == null || paramPair.Item2 == null)
                            return false;
                        if (paramPair.Item1.Name != paramPair.Item2.Name)
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public virtual bool Visit(ILambdaTypedSimpleExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILambdaTypedSimpleExpression)
            {
                var lambdaContext = (ILambdaTypedSimpleExpression)context;
                if (!expression.Block.Expression.Accept(this, lambdaContext.Block.Expression))
                    return false;
                try
                {
                    foreach (var paramPair in expression.Parameters.Values.EnumerateWith(lambdaContext.Parameters.Values))
                    {
                        if (paramPair.Item1 == null && paramPair.Item2 == null)
                            continue;
                        if (paramPair.Item1 == null || paramPair.Item2 == null)
                            return false;
                        if (paramPair.Item1.Name != paramPair.Item2.Name || !paramPair.Item1.ParameterType.Equals(paramPair.Item2.ParameterType))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public virtual bool Visit(ILambdaTypeInferredStatementExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILambdaTypeInferredStatementExpression)
            {
                var lambdaContext = (ILambdaTypeInferredStatementExpression)context;
                if (!expression.Block.Accept(this, lambdaContext.Block))
                    return false;
                try
                {
                    foreach (var paramPair in expression.Parameters.Values.EnumerateWith(lambdaContext.Parameters.Values))
                    {
                        if (paramPair.Item1 == null && paramPair.Item2 == null)
                            continue;
                        if (paramPair.Item1 == null || paramPair.Item2 == null)
                            return false;
                        if (paramPair.Item1.Name != paramPair.Item2.Name)
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public virtual bool Visit(ILambdaTypedStatementExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILambdaTypedStatementExpression)
            {
                var lambdaContext = (ILambdaTypedStatementExpression)context;
                if (!expression.Block.Accept(this, lambdaContext.Block))
                    return false;
                try
                {
                    foreach (var paramPair in expression.Parameters.Values.EnumerateWith(lambdaContext.Parameters.Values))
                    {
                        if (paramPair.Item1 == null && paramPair.Item2 == null)
                            continue;
                        if (paramPair.Item1 == null || paramPair.Item2 == null)
                            return false;
                        if (paramPair.Item1.Name != paramPair.Item2.Name || !paramPair.Item1.ParameterType.Equals(paramPair.Item2.ParameterType))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public virtual bool Visit(IAnonymousMethodExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IAnonymousMethodExpression)
            {
                var anonContext = (IAnonymousMethodExpression)context;
                if (!this.Visit((IEnumerable<IStatement>)expression, anonContext))
                    return false;
                /* ToDo: Include type breakdown when available. */
                return true;
            }
            return false;
        }

        public virtual bool Visit(IAnonymousMethodWithParametersExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IAnonymousMethodWithParametersExpression)
            {
                var anonContext = (IAnonymousMethodWithParametersExpression)context;
                try
                {
                    foreach (var statementPair in expression.EnumerateWith(anonContext))
                    {
                        if (statementPair.Item1 == null && statementPair.Item2 == null)
                            continue;
                        if (statementPair.Item1 == null || statementPair.Item1 == null)
                            return false;
                        if (!statementPair.Item1.Accept(this, statementPair.Item2))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                try
                {
                    foreach (var paramPair in expression.Parameters.Values.EnumerateWith(anonContext.Parameters.Values))
                    {
                        if (paramPair.Item1 == null && paramPair.Item2 == null)
                            continue;
                        if (paramPair.Item1 == null || paramPair.Item1 == null)
                            return false;
                        if (paramPair.Item1.Name != paramPair.Item2.Name)
                            return false;
                        if (!paramPair.Item1.ParameterType.Equals(paramPair.Item2.ParameterType))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                /* ToDo: Include type breakdown when available. */
                return true;
            }
            return false;
        }

        public virtual bool Visit(ICommaExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ICommaExpression)
            {
                var commaContext = (ICommaExpression)context;
                try
                {
                    foreach (var commaExpressionPair in expression.EnumerateWith(commaContext))
                    {
                        if (commaExpressionPair.Item1 == null && commaExpressionPair.Item2 == null)
                            continue;
                        if (commaExpressionPair.Item1 == null || commaExpressionPair.Item1 == null)
                            return false;
                        if (!commaExpressionPair.Item1.Accept(this, commaExpressionPair.Item2))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public virtual bool Visit(ICreateArrayDetailExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ICreateArrayDetailExpression)
            {
                var arrayContext = (ICreateArrayDetailExpression)context;
                if (expression.Rank != arrayContext.Rank)
                    return false;
                if (!expression.ArrayType.Equals(arrayContext.ArrayType))
                    return false;
                if (!this.Visit(expression.Details, arrayContext.Details))
                    return false;
                if (!this.Visit(expression.Sizes, arrayContext.Sizes))
                    return false;
                return true;
            }
            return false;
        }

        public virtual bool Visit(ICreateArrayNestedDetailExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ICreateArrayNestedDetailExpression)
            {
                var arrayContext = (ICreateArrayNestedDetailExpression)context;
                if (!this.Visit(expression.Details, arrayContext.Details))
                    return false;
                return true;
            }
            return false;
        }

        public virtual bool Visit(ICreateArrayExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ICreateArrayExpression)
            {
                var arrayContext = (ICreateArrayExpression)context;
                if (expression.Rank != arrayContext.Rank)
                    return false;
                if (!this.Visit(expression.Sizes, arrayContext.Sizes))
                    return false;
            }
            return false;
        }

        public virtual bool Visit(ICreateInstanceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ICreateInstanceExpression)
            {
                var ceContext = (ICreateInstanceExpression)context;
                if (expression.Reference != null && ceContext.Reference != null)
                    expression.Reference.Accept(this, ceContext.Reference);
                if (!this.Visit(expression.Arguments, ceContext.Arguments))
                    return false;
                if (expression.MemberAssignments != null)
                    if (ceContext.MemberAssignments == null && expression.MemberAssignments.Count > 0)
                        return false;
                    else
                        try
                        {
                            foreach (var memberAssignmentPair in expression.MemberAssignments.Values.EnumerateWith(ceContext.MemberAssignments.Values))
                            {
                                if (memberAssignmentPair.Item1 == null && memberAssignmentPair.Item2 == null)
                                    continue;
                                if (memberAssignmentPair.Item1 == null || memberAssignmentPair.Item2 == null)
                                    return false;
                                if (!memberAssignmentPair.Item1.Accept(this, memberAssignmentPair.Item2))
                                    return false;
                            }
                        }
                        catch (InequalSeriesException)
                        {
                            return false;
                        }
                else if (ceContext.MemberAssignments != null && ceContext.MemberAssignments.Count > 0)
                    return false;
            }
            return false;
        }

        public virtual bool Visit<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression, object context)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>
        {
            context = StripDecoration(context);
            if (context is ICreateInstanceFieldAssignment<TField, TFieldParent>)
            {
                var cifaContext = (ICreateInstanceFieldAssignment<TField, TFieldParent>)context;
                if (expression.Name != cifaContext.Name)
                    return false;
                if (expression.Member != null && cifaContext.Member != null)
                {
                    if (!object.ReferenceEquals(expression.Member, cifaContext.Member))
                        return false;
                }
                else if (expression.Member != null || cifaContext.Member != null)
                    return false;
                return this.Visit(expression.MemberType, cifaContext.MemberType) &&
                       this.VisitCommon(expression.Source, cifaContext.Source) &&
                       this.VisitCommon(expression.AssignValue, cifaContext.AssignValue);
            }
            return false;
        }

        public virtual bool Visit<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression, object context)
            where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertySignatureParent<TProperty, TPropertyParent>
        {
            context = StripDecoration(context);
            if (context is ICreateInstancePropertyAssignment<TProperty, TPropertyParent>)
            {
                var cifaContext = (ICreateInstancePropertyAssignment<TProperty, TPropertyParent>)context;
                if (expression.Name != cifaContext.Name)
                    return false;
                if (expression.Member != null && cifaContext.Member != null)
                {
                    if (!object.ReferenceEquals(expression.Member, cifaContext.Member))
                        return false;
                }
                else if (expression.Member != null || cifaContext.Member != null)
                    return false;
                if (expression.MemberType != null && cifaContext.MemberType != null)
                {
                    if (!expression.MemberType.Equals(cifaContext.MemberType))
                        return false;
                }
                else if (expression.MemberType != null || cifaContext.MemberType != null)
                    return false;
                if (expression.Source != null && cifaContext.Source != null)
                {
                    if (!expression.Source.Accept(this, cifaContext.Source))
                        return false;
                }
                else if (expression.Source != null || cifaContext.Source != null)
                    return false;
                if (expression.AssignValue != null && cifaContext.AssignValue != null)
                {
                    if (!expression.AssignValue.Accept(this, cifaContext.AssignValue))
                        return false;
                }
                else if (expression.AssignValue != null || cifaContext.AssignValue != null)
                    return false;
                return true;
            }
            return false;
        }

        public virtual bool Visit(ICreateInstanceUnboundMemberAssignment expression, object context)
        {
            context = StripDecoration(context);
            if (context is ICreateInstanceUnboundMemberAssignment)
            {
                var cifaContext = (ICreateInstanceUnboundMemberAssignment)context;
                return expression.Name == cifaContext.Name && 
                       this.VisitCommon(expression.AssignValue, cifaContext.AssignValue);
            }
            return false;
        }

        public virtual bool Visit(IDelegateHolderReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IDelegateHolderReferenceExpression)
            {
                var delegateContext = (IDelegateHolderReferenceExpression)context;
                return this.Visit(expression.DelegateType, delegateContext.DelegateType) &&
                       expression.ReferenceType == delegateContext.ReferenceType;
            }
            return false;
        }

        public virtual bool Visit(IDelegateInvokeExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IDelegateInvokeExpression)
            {
                var delegateContext = (IDelegateInvokeExpression)context;
                return this.VisitCommon(expression.Reference, delegateContext.Reference) &&
                       this.Visit(expression.Arguments, delegateContext.Arguments);
            }
            return false;
        }

        public virtual bool Visit(IDelegateMethodPointerReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IDelegateMethodPointerReferenceExpression)
            {
                var delegateContext = (IDelegateMethodPointerReferenceExpression)context;
                return this.Visit(expression.DelegateType, delegateContext.DelegateType) &&
                       this.VisitCommon(expression.Reference, delegateContext.Reference) &&
                       expression.ReferenceType == delegateContext.ReferenceType;
            }
            return false;
        }

        public virtual bool Visit(IDelegateReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IDelegateReferenceExpression)
            {
                var delegateContext = (IDelegateReferenceExpression)context;
                return expression.ReferenceType == delegateContext.ReferenceType;
            }
            return false;
        }

        public virtual bool Visit(IDirectionExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IDirectionExpression)
            {
                var directionContext = (IDirectionExpression)context;
                return expression.Direction == directionContext.Direction && this.VisitCommon(expression.Directed, directionContext.Directed);
            }
            return false;
        }

        public virtual bool Visit(IEventInvokeExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IEventInvokeExpression)
            {
                var eventContext = (IEventInvokeExpression)context;
                return this.VisitCommon(expression.Reference, eventContext.Reference) &&
                       this.Visit(expression.Parameters, eventContext.Parameters);
            }
            return false;
        }

        public virtual bool Visit(IExpressionFusionExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IExpressionFusionExpression)
            {
                var fuseContext = (IExpressionFusionExpression)context;
                return this.VisitCommon(expression.Left, fuseContext.Left) &&
                       this.VisitCommon(expression.Right, fuseContext.Right);
            }
            return false;
        }

        public virtual bool Visit(IExpressionToCommaFusionExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IExpressionToCommaFusionExpression)
            {
                var fuseContext = (IExpressionToCommaFusionExpression)context;
                return this.VisitCommon(expression.Left, fuseContext.Left) && 
                       this.Visit(expression.Right, fuseContext.Right);
            }
            return false;
        }

        public virtual bool Visit(IExpressionToCommaTypeReferenceFusionExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IExpressionToCommaTypeReferenceFusionExpression)
            {
                var fuseContext = (IExpressionToCommaTypeReferenceFusionExpression)context;
                return this.VisitCommon(expression.Left, fuseContext.Left) && this.Visit(expression.Right, fuseContext.Right);
            }
            return false;
        }

        public virtual bool Visit(IFieldReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IFieldReferenceExpression)
            {
                var fieldContext = (IFieldReferenceExpression)context;
                return this.VisitCommon(expression.Source, fieldContext.Source) && expression.Name == fieldContext.Name;
            }
            return false;
        }

        public virtual bool Visit(ILocalReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILocalReferenceExpression)
            {
                var localContext = (ILocalReferenceExpression)context;
                return expression.Name == localContext.Name;
            }
            return false;
        }

        public virtual bool Visit(IMethodInvokeExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IMethodInvokeExpression)
            {
                var miContext = (IMethodInvokeExpression)context;
                return this.Visit(expression.Arguments, miContext.Arguments) && 
                       this.VisitCommon(expression.Reference, miContext.Reference);
            }
            return false;
        }

        public virtual bool Visit(IMethodPointerReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IMethodPointerReferenceExpression)
            {
                var methodContext = (IMethodPointerReferenceExpression)context;
                if (expression.Reference != null && methodContext.Reference != null)
                {
                    if (!expression.Reference.Accept(this, methodContext.Reference))
                        return false;
                }
                else if (expression.Reference != null || methodContext.Reference != null)
                    return false;
                return this.Visit(expression.Signature, methodContext.Signature);
            }
            return false;
        }

        public virtual bool Visit(INamedParameterExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is INamedParameterExpression)
            {
                var namedContext = (INamedParameterExpression)context;
                return expression.Name == namedContext.Name &&
                       this.VisitCommon(expression.Expression, namedContext.Expression);
            }
            return false;
        }

        public virtual bool Visit(IParenthesizedExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IParenthesizedExpression)
            {
                var parContext = (IParenthesizedExpression)context;
                return this.VisitCommon(expression.Parenthesized, parContext.Parenthesized);
            }
            return false;
        }

        public virtual bool Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression, object context)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>
        {
            context = StripDecoration(context);
            if (context is IFieldReferenceExpression<TField, TFieldParent>)
            {
                var fieldContext = (IFieldReferenceExpression<TField, TFieldParent>)context;
                if (!object.ReferenceEquals(expression.Member, fieldContext.Member))
                    return false;
                return this.VisitCommon(expression.Source, fieldContext.Source) && expression.Name == fieldContext.Name;
            }
            return false;
        }

        public virtual bool Visit<TPropertySignature, TPropertySignatureParent>(IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> expression, object context)
            where TPropertySignature :
                IPropertySignatureMember<TPropertySignature, TPropertySignatureParent>
            where TPropertySignatureParent :
                IPropertySignatureParent<TPropertySignature, TPropertySignatureParent>
        {
            context = StripDecoration(context);
            if (context is IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent>)
            {
                var propertyContext = (IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent>)context;
                if (!object.ReferenceEquals(expression.Member, propertyContext.Member))
                    return false;
                return this.VisitCommon(expression.Source, propertyContext.Source) && expression.Name == propertyContext.Name;
            }
            return false;
        }

        public bool Visit<TEvent, TEventParameter, TEventParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> expression, object context)
            where TEvent : IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter : IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        {
            context = StripDecoration(context);
            if (context is IEventReferenceExpression<TEvent, TEventParameter, TEventParent>)
            {
                var eventContext = (IEventReferenceExpression<TEvent, TEventParameter, TEventParent>)context;
                if (!object.ReferenceEquals(expression.Member, eventContext.Member))
                    return false;
                return this.VisitCommon(expression.Source, eventContext.Source) && expression.Name == eventContext.Name;
            }
            return false;
        }

        public bool Visit<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>(IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter> expression, object context)
            where TParameterParent : IParameterParent<TParameterParent, TParameter>
            where TIntermediateParameterParent : TParameterParent, Members.IIntermediateParameterParent<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>
            where TParameter : IParameterMember<TParameterParent>
            where TIntermediateParameter : TParameter, Members.IIntermediateParameterMember<TParameterParent, TIntermediateParameterParent>
        {
            context = StripDecoration(context);
            if (context is IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>)
            {
                var eventContext = (IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>)context;
                return expression.ReferenceTarget.Name      == eventContext.ReferenceTarget.Name &&
                       expression.ReferenceTarget.Direction == eventContext.ReferenceTarget.Direction;
            }
            return false;
        }

        public virtual bool Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression, object context)
            where TProperty : IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertyParent<TProperty, TPropertyParent>
        {
            context = StripDecoration(context);
            if (context is IPropertyReferenceExpression<TProperty, TPropertyParent>)
            {
                var propertyContext = (IPropertyReferenceExpression<TProperty, TPropertyParent>)context;
                if (!object.ReferenceEquals(expression.Member, propertyContext.Member))
                    return false;
                return this.VisitCommon(expression.Source, propertyContext.Source) && expression.Name == propertyContext.Name;
            }
            return false;
        }

        public virtual bool Visit(IPropertyReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPropertyReferenceExpression)
            {
                var propertyContext = (IPropertyReferenceExpression)context;
                return this.VisitCommon(expression.Source, propertyContext.Source) && expression.Name == propertyContext.Name;
            }
            return false;
        }

        public virtual bool Visit(ISpecialReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ISpecialReferenceExpression)
            {
                var specialContext = (ISpecialReferenceExpression)context;
                return expression.Kind == specialContext.Kind;
            }
            return false;
        }

        public virtual bool Visit(IStaticGetMemberHandleExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IStaticGetMemberHandleExpression)
            {
                var getMemberHandleContext = (IStaticGetMemberHandleExpression)context;
                throw new NotImplementedException();
            }
            return false;
        }

        public virtual bool Visit(ISymbolExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ISymbolExpression)
            {
                var symbolContext = (ISymbolExpression)context;
                return this.VisitCommon(expression.Source, symbolContext.Source) && expression.Symbol == symbolContext.Symbol;
            }
            return false;
        }

        public virtual bool Visit(IVariadicTypeCastExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IVariadicTypeCastExpression)
            {
                var relevantContext = (IVariadicTypeCastExpression)context;
                throw new NotImplementedException();
            }
            return false;
        }

        public virtual bool Visit(ITypeReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ITypeReferenceExpression)
            {
                var typeContext = (ITypeReferenceExpression)context;
                return this.Visit(expression.ReferenceType, typeContext.ReferenceType);
            }
            return false;
        }

        public virtual bool Visit(ITypeOfExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ITypeOfExpression)
            {
                var typeOfContext = (ITypeOfExpression)context;
                return this.Visit(expression.ReferenceType, typeOfContext.ReferenceType);
            }
            return false;
        }

        public virtual bool Visit(ITypeCastExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is ITypeCastExpression)
            {
                var typeCastContext = (ITypeCastExpression)context;
                return this.Visit(expression.CastType, typeCastContext.CastType) &&
                       this.VisitCommon(expression.Target, typeCastContext.Target) &&
                       this.Visit(expression.OptionalModifiers, typeCastContext.OptionalModifiers) &&
                       this.Visit(expression.RequiredModifiers, typeCastContext.RequiredModifiers);
            }
            return false;
        }

        public virtual bool Visit(IUnaryOperationExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IUnaryOperationExpression)
            {
                var unaryContext = (IUnaryOperationExpression)context;
                return this.VisitCommon(expression.Term, unaryContext.Term) &&
                       expression.Operation == unaryContext.Operation;
            }
            return false;
        }

        public virtual bool Visit(IConditionalExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IConditionalExpression)
            {
                var conditionalContext = (IConditionalExpression)context;
                return this.VisitCommon(expression.CheckPart, conditionalContext.CheckPart) &&
                       this.VisitCommon(expression.TruePart, conditionalContext.TruePart) &&
                       this.VisitCommon(expression.FalsePart, conditionalContext.FalsePart);
            }
            return false;
        }

        public virtual bool Visit(IIndexerReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IIndexerReferenceExpression)
            {
                var indexerContext = (IIndexerReferenceExpression)context;
                return (expression.IndexerType == indexerContext.IndexerType) &&
                       this.Visit(expression.Parameters, indexerContext.Parameters) &&
                       expression.Name == indexerContext.Name &&
                       this.VisitCommon(expression.Source, indexerContext.Source) &&
                       expression.ReferenceType == indexerContext.ReferenceType;
            }
            return false;
        }

        public virtual bool Visit(IBoundLocalReferenceExpression expression, object context)
        {
            context = StripDecoration(context);
            if (context is IBoundLocalReferenceExpression)
            {
                var localRefContext = (IBoundLocalReferenceExpression)context;
                return expression.Name == localRefContext.Name;
            }
            return false;
        }

        public virtual bool Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression, object context)
            where TLeft : 
                INaryOperandExpression
            where TRight : 
                INaryOperandExpression
        {
            context = StripDecoration(context);
            if (context is IBinaryOperationExpression<TLeft, TRight>)
            {
                var binOpContext = (IBinaryOperationExpression<TLeft, TRight>)context;
                return (expression.Associativity == binOpContext.Associativity) &&
                       this.VisitCommon((IExpression)expression.LeftSide, binOpContext.LeftSide) &&
                       this.VisitCommon((IExpression)expression.RightSide, binOpContext.RightSide) &&
                       expression.OperationKind == binOpContext.OperationKind;
            }
            return false;
        }

        public virtual bool Visit(ILinqWhereClause linqClause, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqWhereClause)
            {
                var whereContext = (ILinqWhereClause)context;
                return linqClause.Condition.Accept(this, whereContext.Condition);
            }
            return false;
        }

        public virtual bool Visit(ILinqTypedJoinClause linqClause, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqTypedJoinClause)
            {
                var joinContext = (ILinqTypedJoinClause)context;
                return (linqClause.IntoRangeVariable.Name == joinContext.IntoRangeVariable.Name) &&
                        linqClause.LeftSelector.Accept(this, joinContext.LeftSelector) &&
                        linqClause.RangeSource.Accept(this, joinContext.RangeSource) &&
                        linqClause.RangeVariable.Name == joinContext.RangeVariable.Name &&
                        linqClause.RangeVariable.RangeType.Equals(joinContext.RangeVariable.RangeType) &&
                        linqClause.RightSelector.Accept(this, joinContext.RightSelector);
            }
            return false;
        }

        public virtual bool Visit(ILinqTypedFromClause linqClause, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqTypedFromClause)
            {
                var fromContext = (ILinqTypedFromClause)context;
                return linqClause.RangeSource.Accept(this, fromContext.RangeSource) &&
                       linqClause.RangeVariable.Name == fromContext.RangeVariable.Name &&
                       linqClause.RangeVariable.RangeType.Equals(fromContext.RangeVariable.RangeType);
            }
            return false;
        }

        public virtual bool Visit(ILinqOrderByClause linqClause, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqOrderByClause)
            {
                var orderContext = (ILinqOrderByClause)context;
                try
                {
                    foreach (var orderingPair in linqClause.Orderings.EnumerateWith(orderContext.Orderings))
                    {
                        if (orderingPair.Item1.OrderingKey == null || orderingPair.Item2.OrderingKey == null)
                            return false;
                        if (orderingPair.Item1.Direction != orderingPair.Item2.Direction)
                            return false;
                        if (!orderingPair.Item1.OrderingKey.Accept(this, orderingPair.Item2.OrderingKey))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public virtual bool Visit(ILinqLetClause linqClause, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqLetClause)
            {
                var letContext = (ILinqLetClause)context;
                if (linqClause.RangeSource == null && letContext.RangeSource != null ||
                    linqClause.RangeSource != null && letContext.RangeSource == null)
                    return false;
                return linqClause.RangeSource.Accept(this, letContext.RangeSource) &&
                       linqClause.RangeVariable.Name == letContext.RangeVariable.Name;
            }
            return false;
        }

        public virtual bool Visit(ILinqJoinClause linqClause, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqJoinClause)
            {
                var joinContext = (ILinqJoinClause)context;
                return (linqClause.IntoRangeVariable.Name == joinContext.IntoRangeVariable.Name) &&
                        linqClause.LeftSelector.Accept(this, joinContext.LeftSelector) &&
                        linqClause.RangeSource.Accept(this, joinContext.RangeSource) &&
                        linqClause.RangeVariable.Name == joinContext.RangeVariable.Name &&
                        linqClause.RightSelector.Accept(this, joinContext.RightSelector);
            }
            return false;
        }

        public virtual bool Visit(ILinqFromClause linqClause, object context)
        {
            if (context is ILinqFromClause)
            {
                var fromContext = (ILinqFromClause)context;
                return linqClause.RangeSource.Accept(this, fromContext.RangeSource) &&
                       linqClause.RangeVariable.Name == fromContext.RangeVariable.Name;
            }
            return false;
        }

        public virtual bool Visit(ILinqBody body, ILinqBody context)
        {
            if (body != null && context != null)
            {
                try
                {
                    foreach (var clausePair in body.Clauses.EnumerateWith(context.Clauses))
                    {
                        if (clausePair.Item1 == null && clausePair.Item2 == null)
                            continue;
                        if (clausePair.Item1 == null || clausePair.Item2 == null)
                            return false;
                        if (clausePair.Item1 is ILinqTypedFromClause)
                        {
                            if (!this.Visit((ILinqTypedFromClause)clausePair.Item1, clausePair.Item2))
                                return false;
                        }
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
            }
            return false;
        }

        public virtual bool Visit(ILinqFusionGroupBody expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqFusionGroupBody)
            {
                var groupContext = (ILinqFusionGroupBody)context;
                return this.Visit((ILinqGroupBody)expression, context) &&
                    expression.Next.Accept(this, groupContext.Next) &&
                    expression.Target.Name == groupContext.Target.Name;
            }
            return false;
        }

        public virtual bool Visit(ILinqFusionSelectBody expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqFusionSelectBody)
            {
                var selectContext = (ILinqFusionSelectBody)context;
                return this.Visit((ILinqSelectBody)expression, context) &&
                       expression.Next.Accept(this, selectContext.Next) &&
                       expression.Target.Name == selectContext.Target.Name;
            } 
            return false;
        }

        public virtual bool Visit(ILinqGroupBody expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqGroupBody)
            {
                var groupContext = (ILinqGroupBody)context;
                return this.Visit((ILinqSelectBody)expression, context) &&
                       this.VisitCommon(expression.Key, groupContext.Key);
            }
            return false;
        }

        public virtual bool Visit(ILinqSelectBody expression, object context)
        {
            context = StripDecoration(context);
            if (context is ILinqSelectBody)
            {
                var selectContext = (ILinqSelectBody)context;
                return this.Visit((ILinqBody)expression, selectContext) &&
                       this.VisitCommon(expression.Selection, selectContext.Selection);
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<IType> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<IType>)
            {
                var primitiveContext = (IPrimitiveExpression<IType>)context;
                return this.Visit(expression.Value, primitiveContext.Value);
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<decimal> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<decimal>)
            {
                var primitiveContext = (IPrimitiveExpression<decimal>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<double> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<double>)
            {
                var primitiveContext = (IPrimitiveExpression<double>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<float> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<float>)
            {
                var primitiveContext = (IPrimitiveExpression<float>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<long> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<long>)
            {
                var primitiveContext = (IPrimitiveExpression<long>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<ulong> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<ulong>)
            {
                var primitiveContext = (IPrimitiveExpression<ulong>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<int> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<int>)
            {
                var primitiveContext = (IPrimitiveExpression<int>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<uint> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<uint>)
            {
                var primitiveContext = (IPrimitiveExpression<uint>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<short> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<short>)
            {
                var primitiveContext = (IPrimitiveExpression<short>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<ushort> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<ushort>)
            {
                var primitiveContext = (IPrimitiveExpression<ushort>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<sbyte> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<sbyte>)
            {
                var primitiveContext = (IPrimitiveExpression<sbyte>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<byte> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<byte>)
            {
                var primitiveContext = (IPrimitiveExpression<byte>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<string> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<string>)
            {
                var primitiveContext = (IPrimitiveExpression<string>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<char> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<char>)
            {
                var primitiveContext = (IPrimitiveExpression<char>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public virtual bool Visit(IPrimitiveExpression<bool> expression, object context)
        {
            context = StripDecoration(context);
            if (context is IPrimitiveExpression<bool>)
            {
                var primitiveContext = (IPrimitiveExpression<bool>)context;
                return expression.Value == primitiveContext.Value;
            }
            return false;
        }

        public bool VisitNull(object context)
        {
            if (context == null)
                return false;
            return context.Equals(IntermediateGateway.NullValue);
        }

        public virtual bool Visit(IExplicitStringLiteralStatement statement, IStatement context)
        {
            return true;
        }

        public virtual bool Visit(IWhileStatement whileStatement, IStatement context)
        {
            if (context is IWhileStatement)
            {
                var whileContext = (IWhileStatement)context;
                return this.VisitCommon(whileStatement.Condition, whileContext.Condition) &&
                       this.Visit((IEnumerable<IStatement>)whileStatement, whileContext);
            }
            return false;
        }

        public virtual bool Visit(IYieldBreakStatement statement, IStatement context)
        {
            if (context is IYieldBreakStatement)
                return true;
            return false;
        }

        public virtual bool Visit(IYieldReturnStatement statement, IStatement context)
        {
            if (context is IYieldReturnStatement)
            {
                var yieldContext = (IYieldReturnStatement)context;
                return this.VisitCommon(statement.YieldedResult, yieldContext.YieldedResult);
            }
            return false;
        }

        public virtual bool Visit(ILockStatement statement, IStatement context)
        {
            if (context is ILockStatement)
            {
                var lockContext = (ILockStatement)context;
                return this.VisitCommon(statement.MonitorLock, lockContext.MonitorLock);
            }
            return false;
        }

        public virtual bool Visit(IThrowStatement statement, IStatement context)
        {
            if (context is IThrowStatement)
            {
                var relevantContext = (IThrowStatement)context;
                return this.VisitCommon(statement.ThrowTarget, relevantContext.ThrowTarget);
            }
            return false;
        }

        public virtual bool Visit(IUsingExpressionBlockStatement statement, IStatement context)
        {
            if (context is IUsingExpressionBlockStatement)
            {
                var usingContext = (IUsingExpressionBlockStatement)context;
                return this.VisitCommon(statement.ResourceAcquisition, usingContext.ResourceAcquisition) &&
                       this.Visit((IEnumerable<IStatement>)statement, usingContext);
            }
            return false;
        }

        public virtual bool Visit(IUsingBlockStatement statement, IStatement context)
        {
            if (context is IUsingBlockStatement)
            {
                var usingContext = (IUsingBlockStatement)context;
                return statement.ResourceAcquisition.Accept(this, usingContext.ResourceAcquisition) &&
                       this.Visit((IEnumerable<IStatement>)statement, usingContext);
            }
            return false;
        }

        public virtual bool Visit(ICommentStatement statement, IStatement context)
        {
            if (context is ICommentStatement)
                return true;
            return false;
        }

        public virtual bool Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement, IStatement context)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            if (context is IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>)
            {
                var eventContext = (IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>)context;
                throw new NotImplementedException();
            }
            return false;
        }

        public virtual bool Visit(IChangeEventHandlerStatement statement, IStatement context)
        {
            if (context is IChangeEventHandlerStatement)
            {
                var relevantContext = (IChangeEventHandlerStatement)context;
                throw new NotImplementedException();
            }
            return false;
        }

        public virtual bool Visit(ILocalDeclarationsStatement statement, IStatement context)
        {
            if (context is ILocalDeclarationsStatement)
            {
                var localContext = (ILocalDeclarationsStatement)context;
                if (statement.DeclaredLocals.Count != localContext.DeclaredLocals.Count)
                    return false;
                try
                {
                    foreach (var localDeclPair in statement.DeclaredLocals.EnumerateWith(localContext.DeclaredLocals))
                    {
                        if (localDeclPair.Item1 == null && localDeclPair.Item2 == null)
                            continue;
                        if (localDeclPair.Item1 == null || localDeclPair.Item2 == null)
                            return false;
                        if (localDeclPair.Item1.Name != localDeclPair.Item2.Name ||
                            this.VisitCommon(localDeclPair.Item1.InitializationExpression, localDeclPair.Item2.InitializationExpression))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
            }
            return false;
        }

        public virtual bool Visit(ITryStatement statement, IStatement context)
        {
            if (context is ITryStatement)
            {
                var tryContext = (ITryStatement)context;
                if (!((statement.HasFinally && tryContext.HasFinally && this.Visit((IEnumerable<IStatement>)statement.Finally, tryContext.Finally)) || !(statement.HasFinally || tryContext.HasFinally)) &&
                     ((statement.HasCatchAll && tryContext.HasCatchAll && this.Visit((IEnumerable<IStatement>)statement.CatchAll, tryContext.CatchAll)) || !(statement.HasCatchAll || tryContext.HasCatchAll)) &&
                     this.Visit(statement.Keys, tryContext.Keys))
                    return false;
                try
                {
                    foreach (var statementBlockPair in statement.Values.EnumerateWith(tryContext.Values))
                    {
                        if (statementBlockPair.Item1 == null && statementBlockPair.Item2 == null)
                            continue;
                        if (statementBlockPair.Item1 == null || statementBlockPair.Item2 == null)
                            return false;
                        if (!this.Visit((IEnumerable<IStatement>)statementBlockPair.Item1, statementBlockPair.Item2))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public virtual bool Visit(ISwitchStatement statement, IStatement context)
        {
            if (context is ISwitchStatement)
            {
                var switchContext = (ISwitchStatement)context;
                if (!this.VisitCommon(statement.Selection, switchContext.Selection))
                    return false;
                try
                {
                    foreach (var casePair in statement.EnumerateWith(switchContext))
                    {
                        if (casePair.Item1 == null && casePair.Item2 == null)
                            continue;
                        if (casePair.Item1 == null || casePair.Item2 == null)
                            return false;
                        if (!casePair.Item1.Accept(this, casePair.Item2))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public virtual bool Visit(ISwitchCaseBlockStatement statement, IStatement context)
        {
            if (context is ISwitchCaseBlockStatement)
            {
                var caseContext = (ISwitchCaseBlockStatement)context;
                return this.Visit(statement.Cases, caseContext.Cases) &&
                       this.Visit(((IEnumerable<IStatement>)(statement)), caseContext);
            }
            return false;
        }

        public virtual bool Visit(ISimpleIterationBlockStatement statement, IStatement context)
        {
            if (context is ISimpleIterationBlockStatement)
            {
                var iterContext = (ISimpleIterationBlockStatement)context;
                return statement.Target.Accept(this, iterContext.Target) &&
                       this.VisitCommon(statement.Start, iterContext.Start) &&
                       this.VisitCommon(statement.End, iterContext.End) && statement.EndExclusive && iterContext.EndExclusive &&
                       this.VisitCommon(statement.Incremental, iterContext.Incremental) &&
                       this.Visit((IEnumerable<IStatement>)statement, iterContext);
            }
            return false;
        }

        public virtual bool Visit(IReturnStatement statement, IStatement context)
        {
            if (context is IReturnStatement)
            {
                var returnContext = (IReturnStatement)context;
                return this.VisitCommon(statement.ReturnValue, returnContext.ReturnValue);
            }
            return false;
        }

        public virtual bool Visit(ILabelStatement statement, IStatement context)
        {
            if (context is ILabelStatement)
            {
                var labelContext = (ILabelStatement)context;
                return statement.Name == labelContext.Name;
            }
            return false;
        }

        public virtual bool Visit(IJumpStatement statement, IStatement context)
        {
            if (context is IJumpStatement)
            {
                var jumpContext = (IJumpStatement)context;
                return statement.Target is IStatement && jumpContext.Target is IStatement &&
                       ((IStatement)statement.Target).Accept(this, (IStatement)jumpContext.Target) || 
                       (!(statement.Target is IStatement || jumpContext.Target is IStatement));
            }
            return false;
        }

        public virtual bool Visit(IIterationBlockStatement statement, IStatement context)
        {
            if (context is IIterationBlockStatement)
            {
                var iterationContext = (IIterationBlockStatement)context;
                return this.Visit(statement.Iterations, iterationContext.Iterations) &&
                       this.VisitCommon(statement.Condition, iterationContext.Condition) &&
                       this.Visit(statement.Initializers, iterationContext.Initializers) &&
                       this.Visit((IEnumerable<IStatement>)statement, iterationContext);
            }
            return false;
        }

        public virtual bool Visit(IJumpTarget statement, IStatement context)
        {
            if (context is IJumpTarget)
            {
                var relevantContext = (IJumpTarget)context;
                throw new NotImplementedException();
            }
            return false;
        }

        public virtual bool Visit(IGoToCaseStatement statement, IStatement context)
        {
            if (context is IGoToCaseStatement)
            {
                var caseContext = (IGoToCaseStatement)context;
                return statement.Target.Accept(this, caseContext.Target);
            }
            return false;
        }

        public virtual bool Visit(IGoToStatement statement, IStatement context)
        {
            if (context is IGoToStatement)
            {
                var gotoContext = (IGoToStatement)context;
                return statement.Target.Accept(this, gotoContext.Target);
            }
            return false;
        }

        public virtual bool Visit(IExpressionStatement statement, IStatement context)
        {
            if (context is IExpressionStatement)
            {
                var expressionContext = (IExpressionStatement)context;
                return this.VisitCommon(statement.Expression, expressionContext.Expression);
            }
            return false;
        }

        public virtual bool Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement, IStatement context)
        {
            if (context is IExplicitlyTypedLocalVariableDeclarationStatement)
            {
                var localContext = (IExplicitlyTypedLocalVariableDeclarationStatement)context;
                
                if (statement.DeclaredLocals.Count != localContext.DeclaredLocals.Count ||
                    !this.Visit(statement.LocalType, localContext.LocalType))
                    return false;
                try
                {
                    foreach (var localDeclPair in statement.DeclaredLocals.EnumerateWith(localContext.DeclaredLocals))
                    {
                        if (localDeclPair.Item1 == null && localDeclPair.Item2 == null)
                            continue;
                        if (localDeclPair.Item1 == null || localDeclPair.Item2 == null)
                            return false;
                        if (localDeclPair.Item1.Name != localDeclPair.Item2.Name ||
                            this.VisitCommon(localDeclPair.Item1.InitializationExpression, localDeclPair.Item2.InitializationExpression))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
            }
            return false;
        }

        public virtual bool Visit(IEnumerateSetBreakableBlockStatement statement, IStatement context)
        {
            if (context is IEnumerateSetBreakableBlockStatement)
            {
                var relevantContext = (IEnumerateSetBreakableBlockStatement)context;
                return statement.Local.TypingMethod == relevantContext.Local.TypingMethod &&
                       statement.Local.Name == relevantContext.Local.Name &&
                       this.VisitCommon(statement.Source, relevantContext.Source) &&
                       this.Visit((IEnumerable<IStatement>)statement, relevantContext);
            }
            return false;
        }

        public virtual bool Visit(IConditionContinuationStatement statement, IStatement context)
        {
            if (context is IConditionContinuationStatement)
            {
                var continuationContext = (IConditionContinuationStatement)context;
                return this.Visit((IEnumerable<IStatement>)statement, continuationContext);
            }
            return false;
        }

        public virtual bool Visit(ICallFusionStatement statement, IStatement context)
        {
            if (context is ICallFusionStatement)
            {
                var relevantContext = (ICallFusionStatement)context;
                return this.VisitCommon(statement.Target, relevantContext.Target);
            }
            return false;
        }

        public virtual bool Visit(IConditionBlockStatement statement, IStatement context)
        {
            if (context is IConditionBlockStatement)
            {
                var conditionContext = (IConditionBlockStatement)context;
                return ((statement.HasNext && conditionContext.HasNext &&
                        statement.Next.Accept(this, conditionContext.Next)) ||
                        !(statement.HasNext || conditionContext.HasNext)) &&
                       this.VisitCommon(statement.Condition, conditionContext.Condition);
            }
            return false;
        }

        public virtual bool Visit(ICallMethodStatement statement, IStatement context)
        {
            if (context is ICallMethodStatement)
            {
                var relevantContext = (ICallMethodStatement)context;
                return this.VisitCommon(statement.Target, relevantContext.Target);
            }
            return false;
        }

        public virtual bool Visit(IBreakStatement statement, IStatement context)
        {
            if (context is IBreakStatement)
            {
                var relevantContext = (IBreakStatement)context;
                return statement.Target.Accept(this, relevantContext.Target);
            }
            return false;
        }

        public virtual bool Visit(IBlockStatement statement, IStatement context)
        {
            if (context is IBlockStatement)
            {
                var blockContext = (IBlockStatement)context;
                return this.Visit((IEnumerable<IStatement>)statement, blockContext);
            }
            return false;
        }

        public bool Visit(ICallParameterSet left, ICallParameterSet right)
        {
            if (left.IndexedParameters != null)
                if (right.IndexedParameters == null && left.IndexedParameters.Count > 0)
                    return false;
                else
                {
                    if (left.IndexedParameters.Count != right.IndexedParameters.Count)
                        return false;
                    try
                    {
                        foreach (var argPair in left.IndexedParameters.EnumerateWith(right.IndexedParameters))
                        {
                            if (argPair.Item1 == null && argPair.Item2 == null)
                                continue;
                            if (argPair.Item1 == null || argPair.Item2 == null)
                                return false;
                            if (!argPair.Item1.Accept(this, argPair.Item2))
                                return false;
                        }
                    }
                    catch (InequalSeriesException)
                    {
                        return false;
                    }
                }
            else if (right.IndexedParameters != null && right.IndexedParameters.Count > 0)
                return false;
            if (left.NamedParameters != null)
                if (right.NamedParameters == null && left.NamedParameters.Count > 0)
                    return false;
                else
                {
                    if (left.NamedParameters.Count != right.NamedParameters.Count)
                        return false;
                    try
                    {
                        foreach (var argPair in left.NamedParameters.Values.EnumerateWith(right.NamedParameters.Values))
                        {
                            if (argPair.Item1 == null && argPair.Item2 == null)
                                continue;
                            if (argPair.Item1 == null || argPair.Item2 == null)
                                return false;
                            if (!argPair.Item1.Accept(this, argPair.Item2))
                                return false;
                        }
                    }
                    catch (InequalSeriesException)
                    {
                        return false;
                    }
                }
            else if (right.NamedParameters != null && right.NamedParameters.Count > 0)
                return false;
            return left.TrackingMethod == right.TrackingMethod;
        }

        public bool Visit<T>(IExpressionCollection<T> left, IExpressionCollection<T> right)
            where T :
                class,
                IExpression
        {
            if (left != null && right != null)
            {
                if (left.Count != right.Count)
                    return false;
                try
                {
                    foreach (var expressionPair in left.EnumerateWith(right))
                    {
                        if (expressionPair.Item1 == null && expressionPair.Item2 == null)
                            continue;
                        if (expressionPair.Item1 == null || expressionPair.Item2 == null)
                            return false;
                        if (!expressionPair.Item1.Accept(this, expressionPair.Item2))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
            }
            else if (left != null && left.Count > 0 || right != null && right.Count > 0)
                return false;
            return true;
        }
        public bool Visit(IEnumerable<IStatement> left, IEnumerable<IStatement> right)
        {
            /* Remove components which don't impact the behavior */
            left  = left.Where(k => !(k is ICommentStatement || k is IExplicitStringLiteralStatement));
            right = right.Where(k => !(k is ICommentStatement || k is IExplicitStringLiteralStatement));
            if (left != null && right != null)
            {
                if (left.Count() != right.Count())
                    return false;
                try
                {
                    foreach (var expressionPair in left.EnumerateWith(right))
                    {
                        if (expressionPair.Item1 == null && expressionPair.Item2 == null)
                            continue;
                        if (expressionPair.Item1 == null || expressionPair.Item2 == null)
                            return false;
                        if (!expressionPair.Item1.Accept(this, expressionPair.Item2))
                            return false;
                    }
                }
                catch (InequalSeriesException)
                {
                    return false;
                }
            }
            else if (left != null && left.Count() > 0 || right != null && right.Count() > 0)
                return false;
            return true;
        }

        public bool Visit(IControlledCollection<IType> left, IControlledCollection<IType> right)
        {
            if (left != null && right != null)
            {
                if (left.Count != right.Count)
                    return false;
                foreach (var element in left.OrderBy(k => k.FullName).EnumerateWith(right.OrderBy(k => k.FullName)))
                {
                    if (left == null && right == null)
                        continue;
                    if (left == null || right == null)
                        return false;
                    if (!left.Equals(right))
                        return false;
                }
            }
            else if (left != null && left.Count > 0 || right != null && right.Count > 0)
                return false;
            return true;
        }

        public bool VisitCommon<T>(T left, T right)
            where T :
                class,
                IExpression
        {
            if (left != null && right != null)
                return left.Accept(this, right);
            else if (left != null || right != null)
                return false;
            return true;
        }

        public bool Visit(IType left, IType right)
        {
            if (left != null && right != null)
                return left.Equals(right);
            else if (left != null || right != null)
                return false;
            return true;
        }


        public virtual bool Visit(IIterationDeclarationBlockStatement statement, IStatement context)
        {
            if (statement is IIterationDeclarationBlockStatement)
            {
                var relevantContext = (IIterationDeclarationBlockStatement)context;
                return statement.LocalDeclaration.Accept(this, relevantContext.LocalDeclaration);
            }
            return false;
        }


        public bool Visit(IAwaitStatementExpression expression, object context)
        {
            if (context is IAwaitStatementExpression)
            {
                var relevantContext = (IAwaitStatementExpression)context;
                return expression.Proffer.Accept(this, relevantContext.Proffer);
            }
            return false;
        }

    }
}
