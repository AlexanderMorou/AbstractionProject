using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.CodeDom.Compiler;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Translation
{
    
    public abstract class IntermediateCodeTranslatorBase :
        IIntermediateCodeTranslator
    {
        /// <summary>
        /// The <see cref="IndentedTextWriter"/> which is
        /// used to handle indentation.
        /// </summary>
        private IndentedTextWriter target = null;

        private IIntermediateCodeTranslatorOptions options;

        protected abstract IIntermediateCodeTranslatorOptions InitializeOptions();

        #region IIntermediateCodeTranslator Members

        public IIntermediateCodeTranslatorOptions Options
        {
            get {
                if (this.options == null)
                    this.options = this.InitializeOptions();
                return this.options;
            }
        }
        #endregion

        #region IExpressionVisitor Members

        public virtual void Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
            where TLeft : 
                INaryOperandExpression
            where TRight : 
                INaryOperandExpression
        {
            switch (expression.Associativity)
            {
                case BinaryOperationAssociativity.Left:
                    if (expression.LeftSide != null)
                    {
                        expression.LeftSide.Visit(this);
                        Visit(expression.OperationKind);
                    }
                    expression.RightSide.Visit(this);
                    break;
                case BinaryOperationAssociativity.Right:
                    expression.LeftSide.Visit(this);
                    if (expression.RightSide != null)
                    {
                        Visit(expression.OperationKind);
                        expression.RightSide.Visit(this);
                    }
                    break;
                default:
                    break;
            }
        }

        public abstract void Visit(BinaryOperationKind kind);

        public abstract void Visit(IIndexerReferenceExpression expression);

        /// <summary>
        /// Visits a conditional expression.
        /// </summary>
        /// <param name="expression">The <see cref="IConditionalExpression"/> to visit.</param>
        public abstract void Visit(IConditionalExpression expression);

        /// <summary>
        /// Visits a unary operation expression.
        /// </summary>
        /// <param name="expression">The <see cref="IUnaryOperationExpression"/> to visit.</param>
        public abstract void Visit(IUnaryOperationExpression expression);

        /// <summary>
        /// Visits a type cast expression.
        /// </summary>
        /// <param name="expression">The <see cref="ITypeCastExpression"/> to visit.</param>
        public abstract void Visit(ITypeCastExpression expression);

        /// <summary>
        /// Visits a type of expression
        /// </summary>
        /// <param name="expression">The <see cref="ITypeOfExpression"/> to visit.</param>
        public abstract void Visit(ITypeOfExpression expression);

        /// <summary>
        /// Visits a type reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ITypeReferenceExpression"/> to visit.</param>
        public abstract void Visit(ITypeReferenceExpression expression);

        /// <summary>
        /// Visits a variadic type cast expression.
        /// </summary>
        /// <param name="expression">The <see cref="IVariadicTypeCastExpression"/> to visit.</param>
        public abstract void Visit(IVariadicTypeCastExpression expression);

        /// <summary>
        /// Visits a symbol expression.
        /// </summary>
        /// <param name="expression">The <see cref="ISymbolExpression"/> to visit.</param>
        public abstract void Visit(ISymbolExpression expression);

        /// <summary>
        /// Visits an expression which obtains a member handle through a static
        /// reference.
        /// </summary>
        /// <param name="expression">The <see cref="IStaticGetMemberHandleExpression"/> to visit.</param>
        public abstract void Visit(IStaticGetMemberHandleExpression expression);

        /// <summary>
        /// Visits a special reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ISpecialReferenceExpression"/> to visit.</param>
        public abstract void Visit(ISpecialReferenceExpression expression);

        /// <summary>
        /// Visits a property reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPropertyReferenceExpression"/> to visit.</param>
        public abstract void Visit(IPropertyReferenceExpression expression);

        /// <summary>
        /// Visits a parenthesized expression.
        /// </summary>
        /// <param name="expression">The <see cref="IParenthesizedExpression"/> to visit.</param>
        public abstract void Visit(IParenthesizedExpression expression);

        /// <summary>
        /// Visits a named parameter expression.
        /// </summary>
        /// <param name="expression">The <see cref="INamedParameterExpression"/> which designates
        /// the name and value of a parameter to pass into a method/constructor/indexer.</param>
        public abstract void Visit(INamedParameterExpression expression);

        /// <summary>
        /// Visits a method pointer reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IMethodPointerReferenceExpression"/> to visit.</param>
        public abstract void Visit(IMethodPointerReferenceExpression expression);

        /// <summary>
        /// Visits a method invoke expression.
        /// </summary>
        /// <param name="expression">The <see cref="IMethodInvokeExpression"/> to visit.</param>
        public abstract void Visit(IMethodInvokeExpression expression);

        /// <summary>
        /// Visits a local reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ILocalReferenceExpression"/> to visit.</param>
        public abstract void Visit(ILocalReferenceExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IFieldReferenceExpression"/>
        /// to visit.</param>
        public abstract void Visit(IFieldReferenceExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionToCommaTypeReferenceFusionExpression"/>
        /// to visit.</param>
        public abstract void Visit(IExpressionToCommaTypeReferenceFusionExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionToCommaFusionExpression"/>
        /// to visit.</param>
        public abstract void Visit(IExpressionToCommaFusionExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionFusionExpression"/>
        /// to visit.</param>
        public abstract void Visit(IExpressionFusionExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IEventInvokeExpression"/>
        /// to visit.</param>
        public abstract void Visit(IEventInvokeExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDirectionExpression"/>
        /// to visit.</param>
        public abstract void Visit(IDirectionExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateReferenceExpression"/>
        /// to visit.</param>
        public abstract void Visit(IDelegateReferenceExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateMethodPointerReferenceExpression"/>
        /// to visit.</param>
        public abstract void Visit(IDelegateMethodPointerReferenceExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateInvokeExpression"/>
        /// to visit.</param>
        public abstract void Visit(IDelegateInvokeExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateHolderReferenceExpression"/>
        /// to visit.</param>
        public abstract void Visit(IDelegateHolderReferenceExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateInstanceMemberAssignment"/>
        /// to visit.</param>
        public abstract void Visit(ICreateInstanceMemberAssignment expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateInstanceExpression"/>
        /// to visit.</param>
        public abstract void Visit(ICreateInstanceExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayExpression"/>
        /// to visit.</param>
        public abstract void Visit(ICreateArrayExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayNestedDetailExpression"/>
        /// to visit.</param>
        public abstract void Visit(ICreateArrayNestedDetailExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayDetailExpression"/>
        /// to visit.</param>
        public abstract void Visit(ICreateArrayDetailExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICommaExpression"/>
        /// to visit.</param>
        public abstract void Visit(ICommaExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAnonymousMethodWithParametersExpression"/>
        /// to visit.</param>
        public abstract void Visit(IAnonymousMethodWithParametersExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAnonymousMethodExpression"/>
        /// to visit.</param>
        public abstract void Visit(IAnonymousMethodExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypedStatementExpression"/>
        /// to visit.</param>
        public abstract void Visit(ILambdaTypedStatementExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypeInferredStatementExpression"/>
        /// to visit.</param>
        public abstract void Visit(ILambdaTypeInferredStatementExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypedSimpleExpression"/>
        /// to visit.</param>
        public abstract void Visit(ILambdaTypedSimpleExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypeInferredSimpleExpression"/>
        /// to visit.</param>
        public abstract void Visit(ILambdaTypeInferredSimpleExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IParameterReferenceExpression"/>
        /// to visit.</param>
        public abstract void Visit(IParameterReferenceExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IConstructorInvokeExpression"/>
        /// to visit.</param>
        public abstract void Visit(IConstructorInvokeExpression expression);

        /// <summary>
        /// Visits the <paramref name="ctorPointerReference"/> provided.
        /// </summary>
        /// <param name="ctorPointerReference">The <see cref="IConstructorPointerReferenceExpression"/>
        /// to visit.</param>
        public abstract void Visit(IConstructorPointerReferenceExpression ctorPointerReference);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqExpression"/>
        /// to visit.</param>
        public abstract void Visit(ILinqExpression expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAssignmentExpression"/>
        /// to visit.</param>
        public abstract void Visit(IAssignmentExpression expression);

        /// <summary>
        /// Visits the range variable of a language integrated query.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqRangeVariableReference"/>
        /// to visit.</param>
        public abstract void Visit(ILinqRangeVariableReference expression);

        /// <summary>
        /// Visits the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IEventReferenceExpression"/> 
        /// to visit.</param>
        public abstract void Visit(IEventReferenceExpression expression);

        #endregion

        #region ILinqVisitor Members

        public abstract void Visit(ILinqSelectBody expression);

        public abstract void Visit(ILinqGroupBody expression);

        public abstract void Visit(ILinqFusionSelectBody expression);

        public abstract void Visit(ILinqFusionGroupBody expression);

        public abstract void Visit(ILinqFromClause linqClause);

        public abstract void Visit(ILinqJoinClause linqClause);

        public abstract void Visit(ILinqLetClause linqClause);

        public abstract void Visit(ILinqOrderByClause linqClause);

        public abstract void Visit(ILinqTypedFromClause linqClause);

        public abstract void Visit(ILinqTypedJoinClause linqClause);

        public abstract void Visit(ILinqWhereClause linqClause);

        #endregion

        #region IIntermediatePrimitiveVisitor Members

        /// <summary>
        /// Visits a boolean primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<bool> expression);

        /// <summary>
        /// Visits a character primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<char> expression);

        /// <summary>
        /// Visits a string primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<string> expression);

        /// <summary>
        /// Visits a byte primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<byte> expression);

        /// <summary>
        /// Visits a sbyte primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<sbyte> expression);

        /// <summary>
        /// Visits an unsigned 16-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<ushort> expression);

        /// <summary>
        /// Visits a 16-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<short> expression);

        /// <summary>
        /// Visits an unsigned 32-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<uint> expression);

        /// <summary>
        /// Visits a 32-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<int> expression);

        /// <summary>
        /// Visits an unsigned 64-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<ulong> expression);

        /// <summary>
        /// Visits a 64-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<long> expression);

        /// <summary>
        /// Visits a single precision floating point primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<float> expression);

        /// <summary>
        /// Visits a double precision floating point primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<double> expression);

        /// <summary>
        /// Visits a decimal primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Visit(IPrimitiveExpression<decimal> expression);

        /// <summary>
        /// Visits a null primitive expression.
        /// </summary>
        public abstract void VisitNull();

        #endregion

        #region IStatementVisitor Members

        void VisitStatementSet(IEnumerable<IStatement> statementSet)
        {
            foreach (var statement in statementSet)
                statement.Visit(this);
        }

        public abstract void Visit(IBlockStatement statement);

        public abstract void Visit(IBreakStatement statement);

        public abstract void Visit(ICallMethodStatement statement);

        public abstract void Visit(IConditionBlockStatement statement);

        public abstract void Visit(ICallFusionStatement statement);

        public abstract void Visit(IConditionContinuationStatement statement);

        public abstract void Visit(IEnumerateSetBreakableBlockStatement statement);

        public abstract void Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement);

        public abstract void Visit(IExpressionStatement statement);

        public abstract void Visit(IGoToStatement statement);

        public abstract void Visit(IJumpTarget statement);

        /// <summary>
        /// Visits the iteration block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IIterationBlockStatement"/> to visit.</param>
        public abstract void Visit(IIterationBlockStatement statement);

        /// <summary>
        /// Visits the jump <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IJumpStatement"/> to visit.</param>
        public abstract void Visit(IJumpStatement statement);

        /// <summary>
        /// Visits the label <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ILabelStatement"/> to visit.</param>
        public abstract void Visit(ILabelStatement statement);

        /// <summary>
        /// Visits the return <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IReturnStatement"/> to visit.</param>
        public abstract void Visit(IReturnStatement statement);

        /// <summary>
        /// Visits the simple iteration <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISimpleIterationBlockStatement"/> to visit.</param>
        public abstract void Visit(ISimpleIterationBlockStatement statement);

        /// <summary>
        /// Visits the switch case block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISwitchCaseBlockStatement"/> to visit.</param>
        public abstract void Visit(ISwitchCaseBlockStatement statement);

        /// <summary>
        /// Visits the switch <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISwitchStatement"/> to visit.</param>
        public abstract void Visit(ISwitchStatement statement);

        /// <summary>
        /// Visits the try <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ITryStatement"/> to visit.</param>
        public abstract void Visit(ITryStatement statement);

        /// <summary>
        /// Visits the <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ILocalDeclarationStatement"/> to visit.</param>
        public abstract void Visit(ILocalDeclarationStatement statement);

        /// <summary>
        /// Visits the change event handler <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IChangeEventHandlerStatement"/> 
        /// to visit.</param>
        public abstract void Visit(IChangeEventHandlerStatement statement);

        /// <summary>
        /// Visits the bound change event handler <paramref name="statement"/>
        /// provided.
        /// </summary>
        /// <typeparam name="TEvent">The type of event as it exists in the
        /// abstract type system.</typeparam>
        /// <typeparam name="TEventParameter">The type of parameter
        /// contained within the events.</typeparam>
        /// <typeparam name="TEventParent">The type which owns the properties
        /// in the abstract type system.</typeparam>
        /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
        /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
        /// <typeparam name="TSignatureParent">The parent that contains the <typeparamref name="TSignature"/> 
        /// instances.</typeparam>
        /// <param name="statement">The <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/> to visit.</param>
        public abstract void Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;

        /// <summary>
        /// Visits the comment <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ICommentStatement"/>
        /// to visit.</param>
        public abstract void Visit(ICommentStatement statement);
        #endregion

        #region IIntermediateDeclarationVisitor Members

        public abstract void Visit(IIntermediateAssembly assembly);

        public abstract void Visit(IIntermediateNamespaceDeclaration @namespace);

        #endregion

        #region IIntermediateTypeVisitor Members

        public abstract void Visit(IIntermediateClassType @class);

        public abstract void Visit(IIntermediateDelegateType @delegate);

        public abstract void Visit(IIntermediateEnumType @enum);

        public abstract void Visit(IIntermediateInterfaceType @interface);

        public abstract void Visit(IIntermediateStructType @struct);

        public abstract void Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
            where TGenericParameter :
                IGenericParameter<TGenericParameter, TParent>
            where TIntermediateGenericParameter :
                TGenericParameter,
                IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            where TParent :
                IGenericParamParent<TGenericParameter, TParent>
            where TIntermediateParent :
                TParent,
                IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>;

        #endregion

        #region IIntermediateMemberVisitor Members

        public abstract void Visit(ILocalMember local);

        public abstract void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
            where TCtor :
                IConstructorMember<TCtor, TType>
            where TIntermediateCtor :
                TCtor,
                IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType :
                ICreatableParent<TCtor, TType>
            where TIntermediateType :
                TType,
                IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>;

        public abstract void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
            where TCtor :
                IConstructorMember<TCtor, TType>
            where TIntermediateCtor :
                TCtor,
                IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType :
                ICreatableParent<TCtor, TType>
            where TIntermediateType :
                TType,
                IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>;

        public abstract void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
            where TEvent :
                IEventMember<TEvent, TEventParent>
            where TIntermediateEvent :
                TEvent,
                IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent :
                IEventParent<TEvent, TEventParent>
            where TIntermediateEventParent :
                TEventParent,
                IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>;

        public abstract void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParent>
            where TIntermediateEvent :
                TEvent,
                IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParent>
            where TIntermediateEventParent :
                TEventParent,
                IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>;

        public abstract void Visit<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion)
            where TCoercionParent :
                ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>;

        public abstract void Visit<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion)
            where TCoercionParent :
                ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>;

        public abstract void Visit<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion)
            where TCoercionParent :
                ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>;

        public abstract void Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TIntermediateField :
                TField,
                IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>
            where TIntermediateFieldParent :
                TFieldParent,
                IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>;

        public abstract void Visit(IIntermediateEnumFieldMember field);

        public abstract void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
            where TIndexer :
                IIndexerMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer :
                TIndexer,
                IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent :
                IIndexerParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent :
                TIndexerParent,
                IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>;

        public abstract void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
            where TIndexer :
                IIndexerSignatureMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer :
                TIndexer,
                IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent :
                IIndexerSignatureParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent :
                TIndexerParent,
                IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>;

        public abstract void Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
            where TMethod :
                IMethodMember<TMethod, TMethodParent>
            where TIntermediateMethod :
                IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, 
                TMethod
            where TMethodParent :
                IMethodParent<TMethod, TMethodParent>
            where TIntermediateMethodParent :
                IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, 
                TMethodParent;

        public abstract void Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
            where TSignature :
                IMethodSignatureMember<TSignature, TParent>
            where TIntermediateSignature :
                TSignature,
                IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            where TParent :
                IMethodSignatureParent<TSignature, TParent>
            where TIntermediateParent :
                TParent,
                IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>;

        public abstract void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
            where TProperty :
                IPropertySignatureMember<TProperty, TPropertyParent>
            where TIntermediateProperty :
                TProperty,
                IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent :
                IPropertySignatureParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent :
                TPropertyParent,
                IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>;

        public abstract void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
            where TProperty :
                IPropertyMember<TProperty, TPropertyParent>
            where TIntermediateProperty :
                TProperty,
                IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent :
                IPropertyParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent :
                TPropertyParent,
                IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>;

        public abstract void Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
            where TParent :
                IParameterParent
            where TIntermediateParent :
                TParent,
                IIntermediateParameterParent;

        public abstract void Visit(ILinqRangeVariable rangeVariable);

        #endregion

        #region IIntermediateInclusionVisitor Members

        public abstract void Visit(INamedInclusionScopeCoercion namedInclusion);

        public abstract void Visit(INamedInclusionRenameScopeCoercion renamedInclusion);

        public abstract void Visit(INamespaceInclusionScopeCoercion namespaceInclusion);

        public abstract void Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion);

        public abstract void Visit(ITypeInclusionScopeCoercion typeInclusion);

        public abstract void Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion);

        public abstract void Visit(IStaticInclusionScopeCoercion staticInclusion);

        #endregion

        /// <summary>
        /// Increases the <see cref="Target"/> indent level.
        /// </summary>
        protected void IncreaseIndent()
        {
            this.Target.Indent++;
        }

        /// <summary>
        /// Decreases the <see cref="Target"/> indent level.
        /// </summary>
        protected void DecreaseIndent()
        {
            this.Target.Indent--;
        }

        public IndentedTextWriter Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
            }
        }

    }
}
