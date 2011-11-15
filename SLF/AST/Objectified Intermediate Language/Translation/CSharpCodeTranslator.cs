using System;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Translation
{
    public class CSharpCodeTranslator :
        IntermediateCodeTranslatorBase
    {

        protected override IIntermediateCodeTranslatorOptions InitializeOptions()
        {
            throw new NotImplementedException();
        }

        public override void Visit(BinaryOperationKind kind)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIndexerReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IConditionalExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IUnaryOperationExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ITypeCastExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ITypeOfExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ITypeReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IVariadicTypeCastExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ISymbolExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IStaticGetMemberHandleExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ISpecialReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPropertyReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IParenthesizedExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(INamedParameterExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IMethodPointerReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IMethodInvokeExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILocalReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IFieldReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IExpressionToCommaTypeReferenceFusionExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IExpressionToCommaFusionExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IExpressionFusionExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IEventInvokeExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IDirectionExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IDelegateReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IDelegateMethodPointerReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IDelegateInvokeExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IDelegateHolderReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ICreateInstanceMemberAssignment expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ICreateInstanceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ICreateArrayExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ICreateArrayNestedDetailExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ICreateArrayDetailExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ICommaExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IAnonymousMethodWithParametersExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IAnonymousMethodExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILambdaTypedStatementExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILambdaTypeInferredStatementExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILambdaTypedSimpleExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILambdaTypeInferredSimpleExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IParameterReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IConstructorInvokeExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IConstructorPointerReferenceExpression ctorPointerReference)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IAssignmentExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqRangeVariableReference expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IEventReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqSelectBody expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqGroupBody expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqFusionSelectBody expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqFusionGroupBody expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqFromClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqJoinClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqLetClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqOrderByClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqTypedFromClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqTypedJoinClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqWhereClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<bool> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<char> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<string> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<byte> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<sbyte> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<ushort> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<short> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<uint> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<int> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<ulong> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<long> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<float> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<double> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IPrimitiveExpression<decimal> expression)
        {
            throw new NotImplementedException();
        }

        public override void VisitNull()
        {
            //base.SetTextClass(IntermediateSpanTranslationClasses.Keyword);
        }

        public override void Visit(IBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IBreakStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ICallMethodStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IConditionBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ICallFusionStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IConditionContinuationStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IEnumerateSetBreakableBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IExpressionStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IGoToStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IJumpTarget statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIterationBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IJumpStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILabelStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IReturnStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ISimpleIterationBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ISwitchCaseBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ISwitchStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ITryStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILocalDeclarationStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IChangeEventHandlerStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ICommentStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIntermediateAssembly assembly)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIntermediateNamespaceDeclaration @namespace)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIntermediateClassType @class)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIntermediateDelegateType @delegate)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIntermediateEnumType @enum)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIntermediateInterfaceType @interface)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIntermediateStructType @struct)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILocalMember local)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IIntermediateEnumFieldMember field)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ILinqRangeVariable rangeVariable)
        {
            throw new NotImplementedException();
        }

        public override void Visit(INamedInclusionScopeCoercion namedInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Visit(INamedInclusionRenameScopeCoercion renamedInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Visit(INamespaceInclusionScopeCoercion namespaceInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ITypeInclusionScopeCoercion typeInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IStaticInclusionScopeCoercion staticInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
        {
            throw new NotImplementedException();
        }

    }
}
