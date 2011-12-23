using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.CodeDom.Compiler;
using AllenCopeland.Abstraction.Utilities.Collections;
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
        private IReadOnlyCollection<IIntermediateDeclaration> buildTrailReadOnly;
        private List<IIntermediateDeclaration> buildTrail = new List<IIntermediateDeclaration>();
        #region IIntermediateCodeTranslator Members

        public IReadOnlyCollection<IIntermediateDeclaration> BuildTrail
        {
            get
            {
                if (buildTrailReadOnly == null)
                    buildTrailReadOnly = this.InitializeReadOnlyBuildTrail();
                return this.buildTrailReadOnly;
            }
        }

        private IReadOnlyCollection<IIntermediateDeclaration> InitializeReadOnlyBuildTrail()
        {
            return new ReadOnlyCollection<IIntermediateDeclaration>(buildTrail);
        }

        protected void BuildTrailPush(IIntermediateDeclaration declaration)
        {
            this.buildTrail.Add(declaration);
        }

        protected void BuildTrailPop()
        {
            this.buildTrail.RemoveAt(buildTrail.Count - 1);
        }

        protected void BuildTrailPop(IIntermediateDeclaration declaration)
        {
            if (buildTrail.Contains(declaration))
            {
                for (int i = buildTrail.Count - 1; i >= 0; i--)
                    if (buildTrail[i] == declaration)
                    {
                        buildTrail.RemoveAt(i);
                        break;
                    }
            }
        }

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

        public virtual void Translate<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
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
                        Translate(expression.OperationKind);
                    }
                    expression.RightSide.Visit(this);
                    break;
                case BinaryOperationAssociativity.Right:
                    expression.LeftSide.Visit(this);
                    if (expression.RightSide != null)
                    {
                        Translate(expression.OperationKind);
                        expression.RightSide.Visit(this);
                    }
                    break;
                default:
                    break;
            }
        }

        public abstract void Translate(BinaryOperationKind kind);

        public abstract void Translate(IIndexerReferenceExpression expression);

        /// <summary>
        /// Visits a conditional expression.
        /// </summary>
        /// <param name="expression">The <see cref="IConditionalExpression"/> to visit.</param>
        public abstract void Translate(IConditionalExpression expression);

        /// <summary>
        /// Visits a unary operation expression.
        /// </summary>
        /// <param name="expression">The <see cref="IUnaryOperationExpression"/> to visit.</param>
        public abstract void Translate(IUnaryOperationExpression expression);

        /// <summary>
        /// Translates a type cast expression.
        /// </summary>
        /// <param name="expression">The <see cref="ITypeCastExpression"/> to visit.</param>
        public abstract void Translate(ITypeCastExpression expression);

        /// <summary>
        /// Translates a type of expression
        /// </summary>
        /// <param name="expression">The <see cref="ITypeOfExpression"/> to visit.</param>
        public abstract void Translate(ITypeOfExpression expression);

        /// <summary>
        /// Translates a type reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ITypeReferenceExpression"/> to visit.</param>
        public abstract void Translate(ITypeReferenceExpression expression);

        /// <summary>
        /// Translates a variadic type cast expression.
        /// </summary>
        /// <param name="expression">The <see cref="IVariadicTypeCastExpression"/> to visit.</param>
        public abstract void Translate(IVariadicTypeCastExpression expression);

        /// <summary>
        /// Translates a symbol expression.
        /// </summary>
        /// <param name="expression">The <see cref="ISymbolExpression"/> to visit.</param>
        public abstract void Translate(ISymbolExpression expression);

        /// <summary>
        /// Visits an expression which obtains a member handle through a static
        /// reference.
        /// </summary>
        /// <param name="expression">The <see cref="IStaticGetMemberHandleExpression"/> to visit.</param>
        public abstract void Translate(IStaticGetMemberHandleExpression expression);

        /// <summary>
        /// Translates a special reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ISpecialReferenceExpression"/> to visit.</param>
        public abstract void Translate(ISpecialReferenceExpression expression);

        /// <summary>
        /// Translates a property reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPropertyReferenceExpression"/> to visit.</param>
        public abstract void Translate(IPropertyReferenceExpression expression);

        /// <summary>
        /// Translates a parenthesized expression.
        /// </summary>
        /// <param name="expression">The <see cref="IParenthesizedExpression"/> to visit.</param>
        public abstract void Translate(IParenthesizedExpression expression);

        /// <summary>
        /// Translates a named parameter expression.
        /// </summary>
        /// <param name="expression">The <see cref="INamedParameterExpression"/> which designates
        /// the name and value of a parameter to pass into a method/constructor/indexer.</param>
        public abstract void Translate(INamedParameterExpression expression);

        /// <summary>
        /// Translates a method pointer reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="IMethodPointerReferenceExpression"/> to visit.</param>
        public abstract void Translate(IMethodPointerReferenceExpression expression);

        /// <summary>
        /// Translates a method invoke expression.
        /// </summary>
        /// <param name="expression">The <see cref="IMethodInvokeExpression"/> to visit.</param>
        public abstract void Translate(IMethodInvokeExpression expression);

        /// <summary>
        /// Translates a local reference expression.
        /// </summary>
        /// <param name="expression">The <see cref="ILocalReferenceExpression"/> to visit.</param>
        public abstract void Translate(ILocalReferenceExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IFieldReferenceExpression"/>
        /// to visit.</param>
        public abstract void Translate(IFieldReferenceExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionToCommaTypeReferenceFusionExpression"/>
        /// to visit.</param>
        public abstract void Translate(IExpressionToCommaTypeReferenceFusionExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionToCommaFusionExpression"/>
        /// to visit.</param>
        public abstract void Translate(IExpressionToCommaFusionExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IExpressionFusionExpression"/>
        /// to visit.</param>
        public abstract void Translate(IExpressionFusionExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IEventInvokeExpression"/>
        /// to visit.</param>
        public abstract void Translate(IEventInvokeExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDirectionExpression"/>
        /// to visit.</param>
        public abstract void Translate(IDirectionExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateReferenceExpression"/>
        /// to visit.</param>
        public abstract void Translate(IDelegateReferenceExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateMethodPointerReferenceExpression"/>
        /// to visit.</param>
        public abstract void Translate(IDelegateMethodPointerReferenceExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateInvokeExpression"/>
        /// to visit.</param>
        public abstract void Translate(IDelegateInvokeExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IDelegateHolderReferenceExpression"/>
        /// to visit.</param>
        public abstract void Translate(IDelegateHolderReferenceExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateInstanceMemberAssignment"/>
        /// to visit.</param>
        public abstract void Translate(ICreateInstanceMemberAssignment expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateInstanceExpression"/>
        /// to visit.</param>
        public abstract void Translate(ICreateInstanceExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayExpression"/>
        /// to visit.</param>
        public abstract void Translate(ICreateArrayExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayNestedDetailExpression"/>
        /// to visit.</param>
        public abstract void Translate(ICreateArrayNestedDetailExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICreateArrayDetailExpression"/>
        /// to visit.</param>
        public abstract void Translate(ICreateArrayDetailExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ICommaExpression"/>
        /// to visit.</param>
        public abstract void Translate(ICommaExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAnonymousMethodWithParametersExpression"/>
        /// to visit.</param>
        public abstract void Translate(IAnonymousMethodWithParametersExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAnonymousMethodExpression"/>
        /// to visit.</param>
        public abstract void Translate(IAnonymousMethodExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypedStatementExpression"/>
        /// to visit.</param>
        public abstract void Translate(ILambdaTypedStatementExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypeInferredStatementExpression"/>
        /// to visit.</param>
        public abstract void Translate(ILambdaTypeInferredStatementExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypedSimpleExpression"/>
        /// to visit.</param>
        public abstract void Translate(ILambdaTypedSimpleExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILambdaTypeInferredSimpleExpression"/>
        /// to visit.</param>
        public abstract void Translate(ILambdaTypeInferredSimpleExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IParameterReferenceExpression"/>
        /// to visit.</param>
        public abstract void Translate(IParameterReferenceExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IConstructorInvokeExpression"/>
        /// to visit.</param>
        public abstract void Translate(IConstructorInvokeExpression expression);

        /// <summary>
        /// Translates the <paramref name="ctorPointerReference"/> provided.
        /// </summary>
        /// <param name="ctorPointerReference">The <see cref="IConstructorPointerReferenceExpression"/>
        /// to visit.</param>
        public abstract void Translate(IConstructorPointerReferenceExpression ctorPointerReference);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqExpression"/>
        /// to visit.</param>
        public abstract void Translate(ILinqExpression expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IAssignmentExpression"/>
        /// to visit.</param>
        public abstract void Translate(IAssignmentExpression expression);

        /// <summary>
        /// Translates the range variable of a language integrated query.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqRangeVariableReference"/>
        /// to visit.</param>
        public abstract void Translate(ILinqRangeVariableReference expression);

        /// <summary>
        /// Translates the <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="expression">The <see cref="IEventReferenceExpression"/> 
        /// to visit.</param>
        public abstract void Translate(IEventReferenceExpression expression);

        #endregion

        #region ILinqVisitor Members

        public abstract void Translate(ILinqSelectBody expression);

        public abstract void Translate(ILinqGroupBody expression);

        public abstract void Translate(ILinqFusionSelectBody expression);

        public abstract void Translate(ILinqFusionGroupBody expression);

        public abstract void Translate(ILinqFromClause linqClause);

        public abstract void Translate(ILinqJoinClause linqClause);

        public abstract void Translate(ILinqLetClause linqClause);

        public abstract void Translate(ILinqOrderByClause linqClause);

        public abstract void Translate(ILinqTypedFromClause linqClause);

        public abstract void Translate(ILinqTypedJoinClause linqClause);

        public abstract void Translate(ILinqWhereClause linqClause);

        #endregion

        #region IPrimitiveVisitor Members

        /// <summary>
        /// Translates a boolean primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<bool> expression);

        /// <summary>
        /// Translates a character primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<char> expression);

        /// <summary>
        /// Translates a string primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<string> expression);

        /// <summary>
        /// Translates a byte primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<byte> expression);

        /// <summary>
        /// Translates a sbyte primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<sbyte> expression);

        /// <summary>
        /// Visits an unsigned 16-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<ushort> expression);

        /// <summary>
        /// Translates a 16-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<short> expression);

        /// <summary>
        /// Visits an unsigned 32-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<uint> expression);

        /// <summary>
        /// Translates a 32-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<int> expression);

        /// <summary>
        /// Visits an unsigned 64-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<ulong> expression);

        /// <summary>
        /// Translates a 64-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<long> expression);

        /// <summary>
        /// Translates a single precision floating point primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<float> expression);

        /// <summary>
        /// Translates a double precision floating point primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<double> expression);

        /// <summary>
        /// Translates a decimal primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        public abstract void Translate(IPrimitiveExpression<decimal> expression);

        /// <summary>
        /// Translates a null primitive expression.
        /// </summary>
        public abstract void TranslateNull();

        #endregion

        #region IStatementVisitor Members

        void TranslateStatementSet(IEnumerable<IStatement> statementSet)
        {
            foreach (var statement in statementSet)
                statement.Visit(this);
        }

        public abstract void Translate(IBlockStatement statement);

        public abstract void Translate(IBreakStatement statement);

        public abstract void Translate(ICallMethodStatement statement);

        public abstract void Translate(IConditionBlockStatement statement);

        public abstract void Translate(ICallFusionStatement statement);

        public abstract void Translate(IConditionContinuationStatement statement);

        public abstract void Translate(IEnumerateSetBreakableBlockStatement statement);

        public abstract void Translate(IExplicitlyTypedLocalVariableDeclarationStatement statement);

        public abstract void Translate(IExpressionStatement statement);

        public abstract void Translate(IGoToStatement statement);

        public abstract void Translate(IJumpTarget statement);

        /// <summary>
        /// Visits the iteration block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IIterationBlockStatement"/> to visit.</param>
        public abstract void Translate(IIterationBlockStatement statement);

        /// <summary>
        /// Visits the jump <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IJumpStatement"/> to visit.</param>
        public abstract void Translate(IJumpStatement statement);

        /// <summary>
        /// Visits the label <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ILabelStatement"/> to visit.</param>
        public abstract void Translate(ILabelStatement statement);

        /// <summary>
        /// Visits the return <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IReturnStatement"/> to visit.</param>
        public abstract void Translate(IReturnStatement statement);

        /// <summary>
        /// Visits the simple iteration <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISimpleIterationBlockStatement"/> to visit.</param>
        public abstract void Translate(ISimpleIterationBlockStatement statement);

        /// <summary>
        /// Visits the switch case block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISwitchCaseBlockStatement"/> to visit.</param>
        public abstract void Translate(ISwitchCaseBlockStatement statement);

        /// <summary>
        /// Visits the switch <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISwitchStatement"/> to visit.</param>
        public abstract void Translate(ISwitchStatement statement);

        /// <summary>
        /// Visits the try <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ITryStatement"/> to visit.</param>
        public abstract void Translate(ITryStatement statement);

        /// <summary>
        /// Visits the <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ILocalDeclarationStatement"/> to visit.</param>
        public abstract void Translate(ILocalDeclarationStatement statement);

        /// <summary>
        /// Visits the change event handler <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IChangeEventHandlerStatement"/> 
        /// to visit.</param>
        public abstract void Translate(IChangeEventHandlerStatement statement);

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
        public abstract void Translate<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
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
        public abstract void Translate(ICommentStatement statement);
        #endregion

        #region IIntermediateDeclarationVisitor Members

        public abstract void Translate(IIntermediateAssembly assembly);

        public abstract void Translate(IIntermediateNamespaceDeclaration @namespace);

        #endregion

        #region IIntermediateTypeVisitor Members

        public abstract void Translate(IIntermediateClassType @class);

        public abstract void Translate(IIntermediateDelegateType @delegate);

        public abstract void Translate(IIntermediateEnumType @enum);

        public abstract void Translate(IIntermediateInterfaceType @interface);

        public abstract void Translate(IIntermediateStructType @struct);

        public abstract void Translate<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
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

        public abstract void Translate(ILocalMember local);

        public abstract void Translate<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
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

        public abstract void Translate<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
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

        public abstract void Translate<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
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

        public abstract void Translate<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
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

        public abstract void Translate<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion)
            where TCoercionParent :
                ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>;

        public abstract void Translate<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion)
            where TCoercionParent :
                ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>;

        public abstract void Translate<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion)
            where TCoercionParent :
                ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>;

        public abstract void Translate<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
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

        public abstract void Translate(IIntermediateEnumFieldMember field);

        public abstract void Translate<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
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

        public abstract void Translate<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
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

        public abstract void Translate<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
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

        public abstract void Translate<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
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

        public abstract void Translate<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
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

        public abstract void Translate<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
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

        public abstract void Translate<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
            where TParent :
                IParameterParent
            where TIntermediateParent :
                TParent,
                IIntermediateParameterParent;

        public abstract void Translate(ILinqRangeVariable rangeVariable);

        #endregion

        #region IIntermediateInclusionVisitor Members

        public abstract void Translate(INamedInclusionScopeCoercion namedInclusion);

        public abstract void Translate(INamedInclusionRenameScopeCoercion renamedInclusion);

        public abstract void Translate(INamespaceInclusionScopeCoercion namespaceInclusion);

        public abstract void Translate(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion);

        public abstract void Translate(ITypeInclusionScopeCoercion typeInclusion);

        public abstract void Translate(ITypeInclusionRenameScopeCoercion renamedTypeInclusion);

        public abstract void Translate(IStaticInclusionScopeCoercion staticInclusion);

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

        protected void TranslateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(TIntermediateFieldParent parent)
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
            foreach (var item in parent.Fields.Values)
                item.Visit(this);
        }

        protected void TranslateMethodSignatures<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> signatures)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
            where TIntermediateSignatureParameter :
                IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature,TParent, TIntermediateParent>,
                TSignatureParameter
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
            where TIntermediateSignature :
                IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
                TSignature
            where TParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter,  TParent>
            where TIntermediateParent :
                IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
                TParent
        {
            foreach (var method in signatures.Values)
                method.Visit(this);
        }

        protected void TranslateTypes<TTypeIdentifier, TType, TIntermediateType>(IIntermediateTypeDictionary<TTypeIdentifier, TType, TIntermediateType> target)
            where TTypeIdentifier :
                ITypeUniqueIdentifier
            where TType :
                IType<TTypeIdentifier, TType>
            where TIntermediateType :
                IIntermediateType,
                TType
        {
            foreach (var type in target.Values)
                type.Visit(this);
        }

        protected void TranslateTypeParent(IIntermediateTypeParent parent)
        {
            this.TranslateTypes(parent.Classes);
            this.TranslateTypes(parent.Delegates);
            this.TranslateTypes(parent.Enums);
            this.TranslateTypes(parent.Interfaces);
            this.TranslateTypes(parent.Structs);
        }

        protected void TranslateNamespaceParent(IIntermediateNamespaceParent parent)
        {
            this.TranslateFieldParent<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent>(parent);
            this.TranslateMethodSignatures(parent.Methods);
            this.TranslateTypeParent(parent);
        }

        #region IExpressionVisitor Members

        void IExpressionVisitor.Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IIndexerReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IConditionalExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IUnaryOperationExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ITypeCastExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ITypeOfExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ITypeReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IVariadicTypeCastExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ISymbolExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IStaticGetMemberHandleExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ISpecialReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IPropertyReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IParenthesizedExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(INamedParameterExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IMethodPointerReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IMethodInvokeExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ILocalReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IFieldReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IExpressionToCommaTypeReferenceFusionExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IExpressionToCommaFusionExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IExpressionFusionExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IEventInvokeExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IDirectionExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IDelegateReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IDelegateMethodPointerReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IDelegateInvokeExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IDelegateHolderReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ICreateInstanceMemberAssignment expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ICreateInstanceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ICreateArrayExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ICreateArrayNestedDetailExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ICreateArrayDetailExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ICommaExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IAnonymousMethodWithParametersExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IAnonymousMethodExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ILambdaTypedStatementExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ILambdaTypeInferredStatementExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ILambdaTypedSimpleExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ILambdaTypeInferredSimpleExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IParameterReferenceExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IConstructorInvokeExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IConstructorPointerReferenceExpression ctorPointerReference)
        {
            this.Translate(ctorPointerReference);
        }

        void IExpressionVisitor.Visit(ILinqExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IAssignmentExpression expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(ILinqRangeVariableReference expression)
        {
            this.Translate(expression);
        }

        void IExpressionVisitor.Visit(IEventReferenceExpression expression)
        {
            this.Translate(expression);
        }

        #endregion

        #region ILinqVisitor Members

        void ILinqVisitor.Visit(ILinqSelectBody expression)
        {
            this.Translate(expression);
        }

        void ILinqVisitor.Visit(ILinqGroupBody expression)
        {
            this.Translate(expression);
        }

        void ILinqVisitor.Visit(ILinqFusionSelectBody expression)
        {
            this.Translate(expression);
        }

        void ILinqVisitor.Visit(ILinqFusionGroupBody expression)
        {
            this.Translate(expression);
        }

        void ILinqVisitor.Visit(ILinqFromClause linqClause)
        {
            this.Translate(linqClause);
        }

        void ILinqVisitor.Visit(ILinqJoinClause linqClause)
        {
            this.Translate(linqClause);
        }

        void ILinqVisitor.Visit(ILinqLetClause linqClause)
        {
            this.Translate(linqClause);
        }

        void ILinqVisitor.Visit(ILinqOrderByClause linqClause)
        {
            this.Translate(linqClause);
        }

        void ILinqVisitor.Visit(ILinqTypedFromClause linqClause)
        {
            this.Translate(linqClause);
        }

        void ILinqVisitor.Visit(ILinqTypedJoinClause linqClause)
        {
            this.Translate(linqClause);
        }

        void ILinqVisitor.Visit(ILinqWhereClause linqClause)
        {
            this.Translate(linqClause);
        }

        #endregion

        #region IPrimitiveVisitor Members

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<bool> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<char> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<string> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<byte> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<sbyte> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<ushort> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<short> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<uint> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<int> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<ulong> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<long> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<float> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<double> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.Visit(IPrimitiveExpression<decimal> expression)
        {
            this.Translate(expression);
        }

        void IPrimitiveVisitor.VisitNull()
        {
            this.TranslateNull();
        }

        #endregion

        #region IStatementVisitor Members

        void IStatementVisitor.Visit(IBlockStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IBreakStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(ICallMethodStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IConditionBlockStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(ICallFusionStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IConditionContinuationStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IEnumerateSetBreakableBlockStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IExpressionStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IGoToStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IJumpTarget statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IIterationBlockStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IJumpStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(ILabelStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IReturnStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(ISimpleIterationBlockStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(ISwitchCaseBlockStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(ISwitchStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(ITryStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(ILocalDeclarationStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(IChangeEventHandlerStatement statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
        {
            this.Translate(statement);
        }

        void IStatementVisitor.Visit(ICommentStatement statement)
        {
            this.Translate(statement);
        }

        #endregion

        #region IIntermediateDeclarationVisitor Members

        void IIntermediateDeclarationVisitor.Visit(IIntermediateAssembly assembly)
        {
            this.Translate(assembly);
        }

        void IIntermediateDeclarationVisitor.Visit(IIntermediateNamespaceDeclaration @namespace)
        {
            this.Translate(@namespace);
        }

        #endregion

        #region IIntermediateTypeVisitor Members

        void IIntermediateTypeVisitor.Visit(IIntermediateClassType @class)
        {
            this.Translate(@class);
        }

        void IIntermediateTypeVisitor.Visit(IIntermediateDelegateType @delegate)
        {
            this.Translate(@delegate);
        }

        void IIntermediateTypeVisitor.Visit(IIntermediateEnumType @enum)
        {
            this.Translate(@enum);
        }

        void IIntermediateTypeVisitor.Visit(IIntermediateInterfaceType @interface)
        {
            this.Translate(@interface);
        }

        void IIntermediateTypeVisitor.Visit(IIntermediateStructType @struct)
        {
            this.Translate(@struct);
        }

        void IIntermediateTypeVisitor.Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
        {
            this.Translate(parameter);
        }

        #endregion

        #region IIntermediateMemberVisitor Members

        void IIntermediateMemberVisitor.Visit(ILocalMember local)
        {
            this.Translate(local);
        }

        void IIntermediateMemberVisitor.Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
        {
            this.Translate(ctor);
        }

        void IIntermediateMemberVisitor.Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
        {
            this.Translate(ctor);
        }

        void IIntermediateMemberVisitor.Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
        {
            this.Translate(@event);
        }

        void IIntermediateMemberVisitor.Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
        {
            this.Translate(@event);
        }

        void IIntermediateMemberVisitor.Visit<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion)
        {
            this.Translate(binaryCoercion);
        }

        void IIntermediateMemberVisitor.Visit<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion)
        {
            this.Translate(typeCoercion);
        }

        void IIntermediateMemberVisitor.Visit<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion)
        {
            this.Translate(unaryCoercion);
        }

        void IIntermediateMemberVisitor.Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
        {
            this.Translate(field);
        }

        void IIntermediateMemberVisitor.Visit(IIntermediateEnumFieldMember field)
        {
            this.Translate(field);
        }

        void IIntermediateMemberVisitor.Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
        {
            this.Translate(indexer);
        }

        void IIntermediateMemberVisitor.Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
        {
            this.Translate(indexerSignature);
        }

        void IIntermediateMemberVisitor.Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
        {
            this.Translate(method);
        }

        void IIntermediateMemberVisitor.Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
        {
            this.Translate(methodSignature);
        }

        void IIntermediateMemberVisitor.Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
        {
            this.Translate(propertySignature);
        }

        void IIntermediateMemberVisitor.Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
        {
            this.Translate(property);
        }

        void IIntermediateMemberVisitor.Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
        {
            this.Translate(parameter);
        }

        void IIntermediateMemberVisitor.Visit(ILinqRangeVariable rangeVariable)
        {
            this.Translate(rangeVariable);
        }

        #endregion

        #region IIntermediateInclusionVisitor Members

        void IIntermediateInclusionVisitor.Visit(INamedInclusionScopeCoercion namedInclusion)
        {
            this.Translate(namedInclusion);
        }

        void IIntermediateInclusionVisitor.Visit(INamedInclusionRenameScopeCoercion renamedInclusion)
        {
            this.Translate(renamedInclusion);
        }

        void IIntermediateInclusionVisitor.Visit(INamespaceInclusionScopeCoercion namespaceInclusion)
        {
            this.Translate(namespaceInclusion);
        }

        void IIntermediateInclusionVisitor.Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion)
        {
            this.Translate(renamedNamespaceInclusion);
        }

        void IIntermediateInclusionVisitor.Visit(ITypeInclusionScopeCoercion typeInclusion)
        {
            this.Translate(typeInclusion);
        }

        void IIntermediateInclusionVisitor.Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion)
        {
            this.Translate(renamedTypeInclusion);
        }

        void IIntermediateInclusionVisitor.Visit(IStaticInclusionScopeCoercion staticInclusion)
        {
            this.Translate(staticInclusion);
        }

        #endregion
    }
}
