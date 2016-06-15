using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Documentation;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    public class IntermediateCodeLinker :
        IIntermediateCodeLinker
    {
        #region IPrimitiveVisitor<TransformationKind,ITransformationContext> Members

        public TransformationKind Visit(IPrimitiveExpression<bool> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<char> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<string> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<byte> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<sbyte> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<ushort> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<short> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<uint> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<int> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<ulong> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<long> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<float> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<double> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<decimal> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind VisitNull(ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        #endregion

        #region IIntermediateTransformer Members

        public TransformerKind Kind
        {
            get { return TransformerKind.Linker; }
        }

        #endregion

        #region ILinqVisitor<TransformationKind,ITransformationContext> Members

        public TransformationKind Visit(ILinqSelectBody expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqGroupBody expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqFusionSelectBody expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqFusionGroupBody expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqFromClause linqClause, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqJoinClause linqClause, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqLetClause linqClause, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqOrderByClause linqClause, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqTypedFromClause linqClause, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqTypedJoinClause linqClause, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILinqWhereClause linqClause, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        #endregion

        #region IExpressionVisitor<TransformationKind,ITransformationContext> Members

        public TransformationKind Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression, ITransformationContext context)
            where TLeft : INaryOperandExpression
            where TRight : INaryOperandExpression
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IIndexerReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IConditionalExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IUnaryOperationExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ITypeCastExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ITypeOfExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ITypeReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IVariadicTypeCastExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ISymbolExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IStaticGetMemberHandleExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ISpecialReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPropertyReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Replace;
        }

        public TransformationKind Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression, ITransformationContext context)
            where TProperty : IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertyParent<TProperty, TPropertyParent>
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit<TPropertySignature, TPropertySignatureParent>(IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> expression, ITransformationContext context)
            where TPropertySignature : IPropertySignatureMember<TPropertySignature, TPropertySignatureParent>
            where TPropertySignatureParent : IPropertySignatureParent<TPropertySignature, TPropertySignatureParent>
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression, ITransformationContext context)
            where TField : IFieldMember<TField, TFieldParent>
            where TFieldParent : IFieldParent<TField, TFieldParent>
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IParenthesizedExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(INamedParameterExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IMethodPointerReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IMethodInvokeExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ILocalReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Replace;
        }

        public TransformationKind Visit(IFieldReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Replace;
        }

        public TransformationKind Visit(IExpressionToCommaTypeReferenceFusionExpression expression, ITransformationContext context)
        {
            return TransformationKind.Replace;
        }

        public TransformationKind Visit(IExpressionToCommaFusionExpression expression, ITransformationContext context)
        {
            return TransformationKind.Replace;
        }

        public TransformationKind Visit(IExpressionFusionExpression expression, ITransformationContext context)
        {
            return TransformationKind.Replace;
        }

        public TransformationKind Visit(IEventInvokeExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IDirectionExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IDelegateReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IDelegateMethodPointerReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IDelegateInvokeExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(IDelegateHolderReferenceExpression expression, ITransformationContext context)
        {
            return TransformationKind.Investigate;
        }

        public TransformationKind Visit(ICreateInstanceUnboundMemberAssignment expression, ITransformationContext context)
        {
            return TransformationKind.Delete;
        }


        public TransformationKind Visit<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression, ITransformationContext context)
            where TProperty : 
                IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent : 
                IPropertySignatureParent<TProperty, TPropertyParent>
        {
            return TransformationKind.Delete;
        }

        public TransformationKind Visit<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression, ITransformationContext context)
            where TField : 
                IFieldMember<TField, TFieldParent>
            where TFieldParent : 
                IFieldParent<TField, TFieldParent>
        {
            return TransformationKind.Delete;
        }

        public TransformationKind Visit(ICreateInstanceExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ICreateArrayExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ICreateArrayNestedDetailExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ICreateArrayDetailExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ICommaExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IAnonymousMethodWithParametersExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IAnonymousMethodExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILambdaTypedStatementExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILambdaTypeInferredStatementExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILambdaTypedSimpleExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILambdaTypeInferredSimpleExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IParameterReferenceExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IConstructorInvokeExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IConstructorPointerReferenceExpression ctorPointerReference, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILinqExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IAssignmentExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILinqRangeVariableReference expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IEventReferenceExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IStatementVisitor<TransformationKind,ITransformationContext> Members

        public TransformationKind Visit(IBlockStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IBreakStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ICallMethodStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IConditionBlockStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ICallFusionStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IConditionContinuationStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IEnumerateSetBreakableBlockStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IExpressionStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IGoToStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IJumpTarget statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IIterationBlockStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IJumpStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILabelStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IReturnStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ISimpleIterationBlockStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ISwitchCaseBlockStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ISwitchStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ITryStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILocalDeclarationsStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IChangeEventHandlerStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement, ITransformationContext context)
            where TEvent : IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter : IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter : IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature : IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent : ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ICommentStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IIntermediateMemberVisitor<TransformationKind,ITransformationContext> Members

        public TransformationKind Visit(ILocalMember local, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> member, ITransformationContext context)
            where TSignatureParameter : IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
            where TSignature : IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
            where TParent : ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor, ITransformationContext context)
            where TCtor : IConstructorMember<TCtor, TType>
            where TIntermediateCtor : TCtor, IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType : ICreatableParent<TCtor, TType>
            where TIntermediateType : TType, IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor, ITransformationContext context)
            where TCtor : IConstructorMember<TCtor, TType>
            where TIntermediateCtor : TCtor, IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType : ICreatableParent<TCtor, TType>
            where TIntermediateType : TType, IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event, ITransformationContext context)
            where TEvent : IEventMember<TEvent, TEventParent>
            where TIntermediateEvent : TEvent, IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent : IEventParent<TEvent, TEventParent>
            where TIntermediateEventParent : TEventParent, IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event, ITransformationContext context)
            where TEvent : IEventSignatureMember<TEvent, TEventParent>
            where TIntermediateEvent : TEvent, IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParent>
            where TIntermediateEventParent : TEventParent, IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion, ITransformationContext context) where TCoercionParent : ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion, ITransformationContext context) where TCoercionParent : ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion, ITransformationContext context) where TCoercionParent : ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field, ITransformationContext context)
            where TField : IFieldMember<TField, TFieldParent>
            where TIntermediateField : TField, IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
            where TFieldParent : IFieldParent<TField, TFieldParent>
            where TIntermediateFieldParent : TFieldParent, IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IIntermediateEnumFieldMember field, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer, ITransformationContext context)
            where TIndexer : IIndexerMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer : TIndexer, IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent : IIndexerParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature, ITransformationContext context)
            where TIndexer : IIndexerSignatureMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer : TIndexer, IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent : IIndexerSignatureParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method, ITransformationContext context)
            where TMethod : IMethodMember<TMethod, TMethodParent>
            where TIntermediateMethod : IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod
            where TMethodParent : IMethodParent<TMethod, TMethodParent>
            where TIntermediateMethodParent : IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethodParent
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature, ITransformationContext context)
            where TSignature : IMethodSignatureMember<TSignature, TParent>
            where TIntermediateSignature : TSignature, IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            where TParent : IMethodSignatureParent<TSignature, TParent>
            where TIntermediateParent : TParent, IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature, ITransformationContext context)
            where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
            where TIntermediateProperty : TProperty, IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent : IPropertySignatureParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property, ITransformationContext context)
            where TProperty : IPropertyMember<TProperty, TPropertyParent>
            where TIntermediateProperty : TProperty, IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent : IPropertyParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter, ITransformationContext context)
            where TParent : IParameterParent
            where TIntermediateParent : TParent, IIntermediateParameterParent
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILinqRangeVariable rangeVariable, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IIntermediateDeclarationVisitor<TransformationKind,ITransformationContext> Members

        public TransformationKind Visit(IIntermediateAssembly assembly, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IIntermediateNamespaceDeclaration @namespace, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IIntermediateTypeVisitor<TransformationKind,ITransformationContext> Members

        public TransformationKind Visit(IIntermediateClassType @class, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IIntermediateDelegateType @delegate, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IIntermediateEnumType @enum, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IIntermediateInterfaceType @interface, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IIntermediateStructType @struct, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter, ITransformationContext context)
            where TGenericParameter : IGenericParameter<TGenericParameter, TParent>
            where TIntermediateGenericParameter : TGenericParameter, IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            where TParent : IGenericParamParent<TGenericParameter, TParent>
            where TIntermediateParent : TParent, IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IIntermediateInclusionVisitor<TransformationKind,ITransformationContext> Members

        public TransformationKind Visit(IMethodReferenceStub member, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IDefaultValueExpression member, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(INamedInclusionScopeCoercion namedInclusion, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(INamedInclusionRenameScopeCoercion renamedInclusion, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(INamespaceInclusionScopeCoercion namespaceInclusion, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ITypeInclusionScopeCoercion typeInclusion, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IStaticInclusionScopeCoercion staticInclusion, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ICommentExpression expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }
        public TransformationKind Visit(IDecoratingExpression expression, ITransformationContext context)
        {
            /* *
             * The decorating expression will be replaced
             * with the decorating expression's contained
             * expression.
             * */
            return TransformationKind.Replace;
        }
        public TransformationKind Visit(INewLineExpression expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IMetadatumDefinitionExpressionParameter expression, ITransformationContext context)
        {
            return expression.Value.Visit(this, context);
        }

        #endregion

        public TransformationKind Visit(IBoundLocalReferenceExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }


        public TransformationKind Visit(IUsingBlockStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IUsingExpressionBlockStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IThrowStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(ILockStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }


        public TransformationKind Visit(IYieldReturnStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IYieldBreakStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }


        public TransformationKind Visit(IWhileStatement whileStatement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }


        public TransformationKind Visit(IGoToCaseStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }


        public TransformationKind Visit(IAwaitExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }


        public TransformationKind Visit(IExplicitStringLiteralDecorationExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }

        public TransformationKind Visit(IBlockExpression expression, ITransformationContext context)
        {
            throw new NotImplementedException();
        }


        public TransformationKind Visit(IExplicitStringLiteralStatement statement, ITransformationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
