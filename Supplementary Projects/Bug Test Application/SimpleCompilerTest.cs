using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using Microsoft.VisualBasic;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Compilers.Oilexer;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    public static class SimpleCompilerTest 
    {
        private sealed class TypeAggregator :
            IIntermediateCodeVisitor
        {
            private HashSet<IType> resultedTypeList;
            private IReadOnlyCollection<IType> _resultedTypeList;
            private TypeAggregator() 
            {
                this.resultedTypeList = new HashSet<IType>();
            }

            private IReadOnlyCollection<IType> ResultedTypeList
            {
                get {
                    if (this._resultedTypeList == null)
                        this._resultedTypeList = new ReadOnlyCollection<IType>(this.resultedTypeList.ToList());
                    return this._resultedTypeList;
                }
            }

            internal static IReadOnlyCollection<IType> AggregateTypes(IIntermediateAssembly assembly)
            {
                TypeAggregator aggregator = new TypeAggregator();
                aggregator.Visit(assembly);
                return aggregator.ResultedTypeList;
            }
            #region IExpressionVisitor Members

            public void Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
                where TLeft : INaryOperandExpression
                where TRight : INaryOperandExpression
            {
                if (expression.LeftSide != null)
                    expression.LeftSide.Visit(this);
                if (expression.RightSide != null)
                    expression.RightSide.Visit(this);
            }

            public void Visit(IIndexerReferenceExpression expression)
            {
                this.Visit(expression.Parameters);
            }

            private void Visit<T>(IExpressionCollection<T> series)
                where T :
                    IExpression
            {
                foreach (var expression in series)
                    if (expression != null)
                        expression.Visit(this);
            }

            public void Visit(IConditionalExpression expression)
            {
                if (expression.CheckPart != null)
                    expression.CheckPart.Visit(this);
                if (expression.Type != ExpressionKinds.ConditionalForwardTerm)
                {
                    if (expression.TruePart != null)
                        expression.TruePart.Visit(this);
                    if (expression.FalsePart != null)
                        expression.FalsePart.Visit(this);
                }

            }

            public void Visit(IUnaryOperationExpression expression)
            {
                expression.Term.Visit(this);
            }

            public void Visit(ITypeCastExpression expression)
            {
                this.CheckType(expression.CastType);
                if (expression.Target != null)
                    expression.Target.Visit(this);
            }

            private void CheckType(IType target)
            {
                if (target == null)
                    return;
                switch (target.ElementClassification)
                {
                    case TypeElementClassification.Array:
                    case TypeElementClassification.Nullable:
                    case TypeElementClassification.Pointer:
                    case TypeElementClassification.Reference:
                        this.CheckType(target.ElementType);
                        break;
                    case TypeElementClassification.GenericTypeDefinition:
                        this.CheckType(target.ElementType);
                        this.CheckTypes((target as IGenericType).GenericParameters);
                        break;
                }
                this.resultedTypeList.Add(target);
            }

            public void Visit(ITypeOfExpression expression)
            {
                this.CheckType(expression.ReferenceType);
            }

            public void Visit(ITypeReferenceExpression expression)
            {
                this.CheckType(expression.ReferenceType);

            }


            private void CheckTypes(IEnumerable<IType> series)
            {
                if (series == null)
                    return;
                foreach (var type in series)
                    this.CheckType(type);
            }

            public void Visit(IVariadicTypeCastExpression expression)
            {
                this.CheckTypes(expression.CastTypes);
                expression.Target.Visit(this);
            }

            public void Visit(ISymbolExpression expression)
            {
                if (expression.Source != null)
                    expression.Source.Visit(this);
            }

            public void Visit(IStaticGetMemberHandleExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ISpecialReferenceExpression expression)
            {
                //Noting to do.
            }

            public void Visit(IPropertyReferenceExpression expression)
            {
                if (expression.Source != null)
                    expression.Source.Visit(this);
            }

            public void Visit(IParenthesizedExpression expression)
            {
                if (expression.Parenthesized != null)
                    expression.Parenthesized.Visit(this);
            }

            public void Visit(INamedParameterExpression expression)
            {
                if (expression.Expression != null)
                    expression.Expression.Visit(this);
            }

            public void Visit(IMethodPointerReferenceExpression expression)
            {
                if (expression.Reference != null)
                {
                    if (expression.Reference.Source != null)
                        expression.Reference.Source.Visit(this);
                    if (expression.Reference.GenericParameters != null &&
                        expression.Reference.GenericParameters.Count > 0)
                        this.CheckTypes(expression.Reference.GenericParameters);
                }   
                this.CheckTypes(expression.Signature);
            }

            public void Visit(IMethodInvokeExpression expression)
            {
                if (expression.Reference != null)
                    expression.Reference.Visit(this);
                this.Visit(expression.Parameters);
            }

            public void Visit(ILocalReferenceExpression expression)
            {
            }

            public void Visit(IFieldReferenceExpression expression)
            {
                if (expression.Source != null)
                    expression.Source.Visit(this);
            }

            public void Visit(IExpressionToCommaTypeReferenceFusionExpression expression)
            {
                if (expression.Left != null)
                    expression.Left.Visit(this);
                if (expression.Right != null)
                    this.CheckTypes(expression.Right);
            }

            public void Visit(IExpressionToCommaFusionExpression expression)
            {
                if (expression.Left != null)
                    expression.Left.Visit(this);
                if (expression.Right != null)
                    this.Visit(expression.Right);
            }

            public void Visit(IExpressionFusionExpression expression)
            {
                if (expression.Left != null)
                    expression.Left.Visit(this);
                if (expression.Right != null)
                    expression.Right.Visit(this);

            }

            public void Visit(IEventInvokeExpression expression)
            {
                if (expression.Reference != null)
                {
                    if (expression.Reference.SignatureType != null)
                        this.CheckType(expression.Reference.SignatureType);
                }
            }

            public void Visit(IDirectionExpression expression)
            {
                if (expression.Directed != null)
                    expression.Directed.Visit(this);
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
                throw new NotImplementedException();
            }

            public void Visit(ICreateInstanceMemberAssignment expression)
            {
                if (expression.AssignValue != null)
                    expression.AssignValue.Visit(this);
            }

            public void Visit(ICreateInstanceExpression expression)
            {
                
                if (expression.PropertyAssignments != null)
                    foreach (var assignment in expression.PropertyAssignments.Values)
                        this.Visit(assignment);
                if (expression.Reference != null)
                {
                    if (expression.Reference.Signature != null &&
                        expression.Reference.Signature.Count > 0)
                        this.CheckTypes(expression.Reference.Signature);
                    if (expression.Reference.Reference != null && expression.Reference.Reference.InstanceType != null)
                        this.CheckType(expression.Reference.Reference.InstanceType);
                }
            }

            public void Visit(ICreateArrayExpression expression)
            {
                this.CheckType(expression.ArrayType);
                if (expression.Sizes != null)
                    this.Visit(expression.Sizes);
            }

            public void Visit(ICreateArrayDetailExpression expression)
            {
                this.CheckType(expression.ArrayType);
                if (expression.Sizes != null)
                    this.Visit(expression.Sizes);
                this.Visit(expression.Details);
            }

            public void Visit(ICommaExpression expression)
            {
                this.Visit<IExpression>(expression);
            }

            public void Visit(IArrayDimensionDetailExpression expression)
            {
                
            }

            public void Visit(IAnonymousMethodWithParametersExpression expression)
            {
                foreach (var param in expression.Parameters.Values)
                    param.Visit(this);
                this.Visit((IIntermediateTypeParent)(expression));
                this.Visit(expression.Locals);
                foreach (var item in expression)
                    item.Visit(this);
            }

            private void Visit(ILocalMemberDictionary locals)
            {
                throw new NotImplementedException();
            }

            private void Visit(IIntermediateTypeParent typeParent)
            {
                foreach (var type in typeParent.Types.Values)
                    type.Entry.Visit(this);
            }

            private void Visit(IIntermediateNamespaceParent namespaceParent)
            {
                this.Visit((IIntermediateTypeParent)namespaceParent);
                foreach (var @namespace in namespaceParent.Namespaces.Values)
                    @namespace.Visit(this);
            }

            public void Visit(IAnonymousMethodExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILambdaTypedStatementExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILambdaTypeInferredStatementExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(Slf.Oil.Expressions.Lambda.ILambdaTypedSimpleExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(Slf.Oil.Expressions.Lambda.ILambdaTypeInferredSimpleExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IParameterReferenceExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IConstructorInvokeExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IConstructorPointerReferenceExpression ctorPointerReference)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IAssignmentExpression expression)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region ILinqVisitor Members

            public void Visit(ILinqSelectBody expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqGroupBody expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqFusionSelectBody expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqFusionGroupBody expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqDirectedOrderByClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqDirectedOrderByGroupClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqFromClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqJoinClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqLetClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqOrderByClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqOrderByGroupClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqTypedFromClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqTypedJoinClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqWhereClause linqClause)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IIntermediatePrimitiveVisitor Members

            public void Visit(IPrimitiveExpression<bool> expression)
            {
                this.CheckType(typeof(bool).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<char> expression)
            {
                this.CheckType(typeof(char).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<string> expression)
            {
                this.CheckType(typeof(string).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<byte> expression)
            {
                this.CheckType(typeof(byte).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<sbyte> expression)
            {
                this.CheckType(typeof(sbyte).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<ushort> expression)
            {
                this.CheckType(typeof(ushort).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<short> expression)
            {
                this.CheckType(typeof(short).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<uint> expression)
            {
                this.CheckType(typeof(uint).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<int> expression)
            {
                this.CheckType(typeof(int).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<ulong> expression)
            {
                this.CheckType(typeof(ulong).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<long> expression)
            {
                this.CheckType(typeof(long).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<float> expression)
            {
                this.CheckType(typeof(float).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<double> expression)
            {
                this.CheckType(typeof(double).GetTypeReference());
            }

            public void Visit(IPrimitiveExpression<decimal> expression)
            {
                this.CheckType(typeof(decimal).GetTypeReference());
            }

            public void VisitNull()
            {
            }

            #endregion

            #region IStatementVisitor Members

            public void Visit(IBlockStatement statement)
            {
                foreach (var subStatement in statement)
                    subStatement.Visit(this);
            }

            public void Visit(IBreakStatement statement)
            {
            }

            public void Visit(ICallMethodStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IConditionBlockStatement statement)
            {
                if (statement.Condition != null)
                    statement.Condition.Visit(this);
                this.Visit((IBlockStatement)statement);

            }

            public void Visit(ICallFusionStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IConditionContinuationStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IEnumerationBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IExpressionStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IGoToStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IJumpTarget statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIterationBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IJumpStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILabelStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IReturnStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ISimpleIterationBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ISwitchCaseBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ISwitchStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITryCatchStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITryStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILocalDeclarationStatement statement)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IIntermediateDeclarationVisitor Members

            public void Visit(IIntermediateAssembly assembly)
            {
                this.Visit((IIntermediateNamespaceParent)assembly);
            }

            public void Visit(IIntermediateNamespaceDeclaration @namespace)
            {
                this.Visit((IIntermediateNamespaceParent)(@namespace));
            }

            #endregion

            #region IIntermediateTypeVisitor Members

            public void Visit(IIntermediateClassType @class)
            {
                this.Visit((IIntermediateTypeParent)@class);
                this.CheckTypes(@class.ImplementedInterfaces);
                if (@class.BaseType != null)
                    this.CheckType(@class.BaseType);
                foreach (var member in @class.Members.Values)
                    member.Entry.Visit(this);
            }

            public void Visit(IIntermediateDelegateType @delegate)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIntermediateEnumType @enum)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIntermediateInterfaceType @interface)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIntermediateStructType @struct)
            {
                throw new NotImplementedException();
            }

            public void Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
                where TGenericParameter : IGenericParameter<TGenericParameter, TParent>
                where TIntermediateGenericParameter : TGenericParameter, IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
                where TParent : IGenericParamParent<TGenericParameter, TParent>
                where TIntermediateParent : TParent, IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IIntermediateMemberVisitor Members

            public void Visit(ILocalMember local)
            {
                throw new NotImplementedException();
            }

            public void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
                where TCtor : IConstructorMember<TCtor, TType>
                where TIntermediateCtor : TCtor, IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
                where TType : ICreatableType<TCtor, TType>
                where TIntermediateType : TType, IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType>
            {
                throw new NotImplementedException();
            }

            public void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
                where TCtor : IConstructorMember<TCtor, TType>
                where TIntermediateCtor : TCtor, IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
                where TType : ICreatableType<TCtor, TType>
                where TIntermediateType : TType, IIntermediateCreatableType<TCtor, TIntermediateCtor, TType, TIntermediateType>
            {
                throw new NotImplementedException();
            }

            public void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
                where TEvent : IEventMember<TEvent, TEventParent>
                where TIntermediateEvent : TEvent, IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
                where TEventParent : IEventParent<TEvent, TEventParent>
                where TIntermediateEventParent : TEventParent, IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
                where TEvent : IEventSignatureMember<TEvent, TEventParent>
                where TIntermediateEvent : TEvent, IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
                where TEventParent : IEventSignatureParent<TEvent, TEventParent>
                where TIntermediateEventParent : TEventParent, IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion) where TCoercionParent : ICoercibleType<IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion) where TCoercionParent : ICoercibleType<ITypeCoercionMember<TCoercionParent>, TCoercionParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion) where TCoercionParent : ICoercibleType<IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
                where TField : IFieldMember<TField, TFieldParent>
                where TIntermediateField : TField, IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
                where TFieldParent : IFieldParent<TField, TFieldParent>
                where TIntermediateFieldParent : TFieldParent, IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
            {
                this.CheckType(field.FieldType);
            }

            public void Visit(IIntermediateEnumFieldMember field)
            {
                throw new NotImplementedException();
            }

            public void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
                where TIndexer : IIndexerMember<TIndexer, TIndexerParent>
                where TIntermediateIndexer : TIndexer, IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
                where TIndexerParent : IIndexerParent<TIndexer, TIndexerParent>
                where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
                where TIndexer : IIndexerSignatureMember<TIndexer, TIndexerParent>
                where TIntermediateIndexer : TIndexer, IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
                where TIndexerParent : IIndexerSignatureParent<TIndexer, TIndexerParent>
                where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
                where TMethod : IMethodMember<TMethod, TMethodParent>
                where TIntermediateMethod : IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod
                where TMethodParent : IMethodParent<TMethod, TMethodParent>
                where TIntermediateMethodParent : IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethodParent
            {
                throw new NotImplementedException();
            }

            public void Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
                where TSignature : IMethodSignatureMember<TSignature, TParent>
                where TIntermediateSignature : TSignature, IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
                where TParent : IMethodSignatureParent<TSignature, TParent>
                where TIntermediateParent : TParent, IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
                where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
                where TIntermediateProperty : TProperty, IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
                where TPropertyParent : IPropertySignatureParentType<TProperty, TPropertyParent>
                where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertySignatureParentType<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
                where TProperty : IPropertyMember<TProperty, TPropertyParent>
                where TIntermediateProperty : TProperty, IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
                where TPropertyParent : IPropertyParentType<TProperty, TPropertyParent>
                where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertyParentType<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
                where TParent : IParameterParent
                where TIntermediateParent : TParent, IIntermediateParameterParent
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IIntermediateInclusionVisitor Members

            public void Visit(INamedInclusionScopeCoercion namedInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(INamedInclusionRenameScopeCoercion renamedInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(INamespaceInclusionScopeCoercion namespaceInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITypeInclusionScopeCoercion typeInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(IStaticInclusionScopeCoercion staticInclusion)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
        internal static void Main(string[] args)
        {
            var results = OILexerProgram.CallMainMethod("-q", "-nl", @"C:\Projects\Code\C#\Abstraction\SLF\Languages\OILexer\bin\x86\Debug\Samples\CSharp\Root.oilexer");
            if (results != null && results.Project != null)
            {
                var typeAggregate = TypeAggregator.AggregateTypes(results.Project);

            }
        }

    }
}
