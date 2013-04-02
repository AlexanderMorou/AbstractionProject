using System;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
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

        public override void Translate(BinaryOperationKind kind)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IIndexerReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IConditionalExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IUnaryOperationExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ITypeCastExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ITypeOfExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ITypeReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IVariadicTypeCastExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ISymbolExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IStaticGetMemberHandleExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ISpecialReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPropertyReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IParenthesizedExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(INamedParameterExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IMethodPointerReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IMethodInvokeExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILocalReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IFieldReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IExpressionToCommaTypeReferenceFusionExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IExpressionToCommaFusionExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IExpressionFusionExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IEventInvokeExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IDirectionExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IDelegateReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IDelegateMethodPointerReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IDelegateInvokeExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IDelegateHolderReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICreateInstanceMemberAssignment expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICreateInstanceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICreateArrayExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICreateArrayNestedDetailExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICreateArrayDetailExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICommaExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IAnonymousMethodWithParametersExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IAnonymousMethodExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILambdaTypedStatementExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILambdaTypeInferredStatementExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILambdaTypedSimpleExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILambdaTypeInferredSimpleExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IParameterReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IConstructorInvokeExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IConstructorPointerReferenceExpression ctorPointerReference)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IAssignmentExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqRangeVariableReference expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IEventReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqSelectBody expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqGroupBody expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqFusionSelectBody expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqFusionGroupBody expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqFromClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqJoinClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqLetClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqOrderByClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqTypedFromClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqTypedJoinClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqWhereClause linqClause)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<bool> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<char> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<string> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<byte> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<sbyte> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<ushort> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<short> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<uint> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<int> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<ulong> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<long> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<float> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<double> expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IPrimitiveExpression<decimal> expression)
        {
            throw new NotImplementedException();
        }

        public override void TranslateNull()
        {
        }

        public override void Translate(IBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IBreakStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICallMethodStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IConditionBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICallFusionStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IConditionContinuationStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IEnumerateSetBreakableBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IExplicitlyTypedLocalVariableDeclarationStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IExpressionStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IGoToStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IJumpTarget statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IIterationBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IJumpStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILabelStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IReturnStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ISimpleIterationBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ISwitchCaseBlockStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ISwitchStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ITryStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILocalDeclarationStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IChangeEventHandlerStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICommentStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IIntermediateAssembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            if (Options.AllowPartials || assembly.IsRoot)
            {

            }
        }

        public override void Translate(IIntermediateNamespaceDeclaration @namespace)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IIntermediateClassType @class)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IIntermediateDelegateType @delegate)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IIntermediateEnumType @enum)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IIntermediateInterfaceType @interface)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IIntermediateStructType @struct)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILocalMember local)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IIntermediateEnumFieldMember field)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ILinqRangeVariable rangeVariable)
        {
            throw new NotImplementedException();
        }

        public override void Translate(INamedInclusionScopeCoercion namedInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Translate(INamedInclusionRenameScopeCoercion renamedInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Translate(INamespaceInclusionScopeCoercion namespaceInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Translate(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ITypeInclusionScopeCoercion typeInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ITypeInclusionRenameScopeCoercion renamedTypeInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IStaticInclusionScopeCoercion staticInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TPropertySignature, TPropertySignatureParent>(IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression)
        {
            throw new NotImplementedException();
        }
    }
}
