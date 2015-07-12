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
using AllenCopeland.Abstraction.Slf.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    internal class TestLinkerVisitor :
        IIntermediateCodeVisitor<TestLinkerResult, ICompilationContext>
    {
        #region IExpressionVisitor<TestLinkerResult,ICompilationContext> Members

        public TestLinkerResult Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression, ICompilationContext context)
            where TLeft :
                INaryOperandExpression
            where TRight : 
                INaryOperandExpression
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IIndexerReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IConditionalExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IUnaryOperationExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ITypeCastExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ITypeOfExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ITypeReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IVariadicTypeCastExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ISymbolExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IStaticGetMemberHandleExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ISpecialReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPropertyReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression, ICompilationContext context)
            where TProperty : IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertyParent<TProperty, TPropertyParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TPropertySignature, TPropertySignatureParent>(IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> expression, ICompilationContext context)
            where TPropertySignature : IPropertySignatureMember<TPropertySignature, TPropertySignatureParent>
            where TPropertySignatureParent : IPropertySignatureParent<TPropertySignature, TPropertySignatureParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression, ICompilationContext context)
            where TField : IFieldMember<TField, TFieldParent>
            where TFieldParent : IFieldParent<TField, TFieldParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IParenthesizedExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(INamedParameterExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IMethodPointerReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IMethodInvokeExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILocalReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IFieldReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IExpressionToCommaTypeReferenceFusionExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IExpressionToCommaFusionExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IExpressionFusionExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IEventInvokeExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IDirectionExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IDelegateReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IDelegateMethodPointerReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IDelegateInvokeExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IDelegateHolderReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICreateInstanceUnboundMemberAssignment expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression, ICompilationContext context)
            where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent : IPropertySignatureParent<TProperty, TPropertyParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression, ICompilationContext context)
            where TField : IFieldMember<TField, TFieldParent>
            where TFieldParent : IFieldParent<TField, TFieldParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICreateInstanceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICreateArrayExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICreateArrayNestedDetailExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICreateArrayDetailExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICommaExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IAnonymousMethodWithParametersExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IAnonymousMethodExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILambdaTypedStatementExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILambdaTypeInferredStatementExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILambdaTypedSimpleExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILambdaTypeInferredSimpleExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IParameterReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IConstructorInvokeExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IConstructorPointerReferenceExpression ctorPointerReference, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IAssignmentExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqRangeVariableReference expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IEventReferenceExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICommentExpression expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ILinqVisitor<TestLinkerResult,ICompilationContext> Members

        public TestLinkerResult Visit(ILinqSelectBody expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqGroupBody expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqFusionSelectBody expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqFusionGroupBody expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqFromClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqJoinClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqLetClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqOrderByClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqTypedFromClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqTypedJoinClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqWhereClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPrimitiveVisitor<TestLinkerResult,ICompilationContext> Members

        public TestLinkerResult Visit(IPrimitiveExpression<bool> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<char> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<string> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<byte> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<sbyte> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<ushort> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<short> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<uint> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<int> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<ulong> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<long> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<float> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<double> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IPrimitiveExpression<decimal> expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult VisitNull(ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IStatementVisitor<TestLinkerResult,ICompilationContext> Members

        public TestLinkerResult Visit(IBlockStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IBreakStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICallMethodStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IConditionBlockStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICallFusionStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IConditionContinuationStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IEnumerateSetBreakableBlockStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IExpressionStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IGoToStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IJumpTarget statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IIterationBlockStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IJumpStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILabelStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IReturnStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ISimpleIterationBlockStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ISwitchCaseBlockStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ISwitchStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ITryStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILocalDeclarationStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IChangeEventHandlerStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement, ICompilationContext context)
            where TEvent : IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter : IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter : IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature : IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent : ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ICommentStatement statement, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IIntermediateDeclarationVisitor<TestLinkerResult,ICompilationContext> Members

        public TestLinkerResult Visit(IIntermediateAssembly assembly, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IIntermediateNamespaceDeclaration @namespace, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IIntermediateTypeVisitor<TestLinkerResult,ICompilationContext> Members

        public TestLinkerResult Visit(IIntermediateClassType @class, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IIntermediateDelegateType @delegate, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IIntermediateEnumType @enum, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IIntermediateInterfaceType @interface, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IIntermediateStructType @struct, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter, ICompilationContext context)
            where TGenericParameter : IGenericParameter<TGenericParameter, TParent>
            where TIntermediateGenericParameter : TGenericParameter, IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            where TParent : IGenericParamParent<TGenericParameter, TParent>
            where TIntermediateParent : TParent, IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IIntermediateMemberVisitor<TestLinkerResult,ICompilationContext> Members

        public TestLinkerResult Visit(ILocalMember local, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor, ICompilationContext context)
            where TCtor : IConstructorMember<TCtor, TType>
            where TIntermediateCtor : TCtor, IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType : ICreatableParent<TCtor, TType>
            where TIntermediateType : TType, IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor, ICompilationContext context)
            where TCtor : IConstructorMember<TCtor, TType>
            where TIntermediateCtor : TCtor, IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType : ICreatableParent<TCtor, TType>
            where TIntermediateType : TType, IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event, ICompilationContext context)
            where TEvent : IEventMember<TEvent, TEventParent>
            where TIntermediateEvent : TEvent, IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent : IEventParent<TEvent, TEventParent>
            where TIntermediateEventParent : TEventParent, IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event, ICompilationContext context)
            where TEvent : IEventSignatureMember<TEvent, TEventParent>
            where TIntermediateEvent : TEvent, IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent : IEventSignatureParent<TEvent, TEventParent>
            where TIntermediateEventParent : TEventParent, IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion, ICompilationContext context) where TCoercionParent : ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion, ICompilationContext context) where TCoercionParent : ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion, ICompilationContext context) where TCoercionParent : ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field, ICompilationContext context)
            where TField : IFieldMember<TField, TFieldParent>
            where TIntermediateField : TField, IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
            where TFieldParent : IFieldParent<TField, TFieldParent>
            where TIntermediateFieldParent : TFieldParent, IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IIntermediateEnumFieldMember field, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer, ICompilationContext context)
            where TIndexer : IIndexerMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer : TIndexer, IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent : IIndexerParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature, ICompilationContext context)
            where TIndexer : IIndexerSignatureMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer : TIndexer, IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent : IIndexerSignatureParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method, ICompilationContext context)
            where TMethod : IMethodMember<TMethod, TMethodParent>
            where TIntermediateMethod : IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod
            where TMethodParent : IMethodParent<TMethod, TMethodParent>
            where TIntermediateMethodParent : IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethodParent
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature, ICompilationContext context)
            where TSignature : IMethodSignatureMember<TSignature, TParent>
            where TIntermediateSignature : TSignature, IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            where TParent : IMethodSignatureParent<TSignature, TParent>
            where TIntermediateParent : TParent, IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature, ICompilationContext context)
            where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
            where TIntermediateProperty : TProperty, IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent : IPropertySignatureParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property, ICompilationContext context)
            where TProperty : IPropertyMember<TProperty, TPropertyParent>
            where TIntermediateProperty : TProperty, IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent : IPropertyParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter, ICompilationContext context)
            where TParent : IParameterParent
            where TIntermediateParent : TParent, IIntermediateParameterParent
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ILinqRangeVariable rangeVariable, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IIntermediateInclusionVisitor<TestLinkerResult,ICompilationContext> Members

        public TestLinkerResult Visit(INamedInclusionScopeCoercion namedInclusion, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(INamedInclusionRenameScopeCoercion renamedInclusion, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(INamespaceInclusionScopeCoercion namespaceInclusion, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ITypeInclusionScopeCoercion typeInclusion, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public TestLinkerResult Visit(IStaticInclusionScopeCoercion staticInclusion, ICompilationContext context)
        {
            throw new NotImplementedException();
        }


        public TestLinkerResult Visit(IMetadatumDefinitionExpressionParameter expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
