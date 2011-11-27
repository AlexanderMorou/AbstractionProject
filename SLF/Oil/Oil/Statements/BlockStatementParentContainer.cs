using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class BlockStatementParentContainer :
        ControlledStateCollection<IStatement>,
        IBlockStatementParent,
        IIntermediateTypeParent
    {
        private IBlockStatementParent owner;
        #region Nested GenericParameter Data Members
        /// <summary>
        /// Data member fro <see cref="Classes"/>.
        /// </summary>
        private IIntermediateClassTypeDictionary classes;
        /// <summary>
        /// Data member for <see cref="Delegates"/>.
        /// </summary>
        private IIntermediateDelegateTypeDictionary delegates;
        /// <summary>
        /// Data member for <see cref="Enums"/>.
        /// </summary>
        private IIntermediateEnumTypeDictionary enums;
        /// <summary>
        /// Data member for <see cref="Interfaces"/>.
        /// </summary>
        private IIntermediateInterfaceTypeDictionary interfaces;
        /// <summary>
        /// Data member for <see cref="Structs"/>.
        /// </summary>
        private IIntermediateStructTypeDictionary structs;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private IntermediateFullTypeDictionary types;
        /// <summary>
        /// Data member for <see cref="Locals"/>.
        /// </summary>
        ILocalMemberDictionary locals;
        private BlockStatementScopeLabelDictionary scopeLabels;
        private BlockStatementLabelDictionary labels;
        private IScopeCoercionCollection scopeCoercions;
        #endregion

        internal BlockStatementParentContainer()
        {

        }

        internal void SetOwner(IBlockStatementParent owner)
        {
            this.owner = owner;
        }

        protected internal BlockStatementParentContainer(IBlockStatementParent owner)
        {
            this.owner = owner;
        }

        #region IBlockStatementParent Members

        public ILocalMemberDictionary Locals
        {
            get
            {
                if (this.locals == null)
                    this.locals = new LocalMemberDictionary(this.Owner);
                return this.locals;
            }
        }

        /// <summary>
        /// Inserts and returns a new <see cref="IReturnStatement"/>
        /// with no value as its result.
        /// </summary>
        /// <returns>A new <see cref="IReturnStatement"/>
        /// with no value as its result.</returns>
        public IReturnStatement Return()
        {
            ReturnStatement r = new ReturnStatement(this.Owner);
            this.baseList.Add(r);
            return r;
        }

        /// <summary>
        /// Inserts and returns a new <see cref="IReturnStatement"/>
        /// with the <paramref name="value"/> provided
        /// for the return.
        /// </summary>
        /// <param name="value">An <see cref="IExpression"/> instance that 
        /// relates to the return of the top-most code block</param>
        /// <returns>A new <see cref="IReturnStatement"/>
        /// with the <paramref name="value"/> provided
        /// for the return.</returns>
        public IReturnStatement Return(IExpression value)
        {
            ReturnStatement r = new ReturnStatement(this.Owner, value);
            this.baseList.Add(r);
            return r;
        }

        /// <summary>
        /// Inserts and returns a new <see cref="IConditionBlockStatement"/> instance
        /// which relates to the <paramref name="condition"/> provided.
        /// </summary>
        /// <param name="condition">The <see cref="IExpression"/> to evaluate
        /// before executing the <see cref="IConditionBlockStatement"/>'s statements.</param>
        /// <returns>A new <see cref="IConditionBlockStatement"/> with the
        /// <see cref="IExpression"/> <paramref name="condition"/> provided.</returns>
        public IConditionBlockStatement If(IExpression condition)
        {
            var ifResult = OnIf(condition);
            this.baseList.Add(ifResult);
            return ifResult;
        }

        /// <summary>
        /// Inserts and returns a new <see cref="ISwitchStatement"/> instance
        /// which relates to the <paramref name="caseCondition"/> provided.
        /// </summary>
        /// <param name="caseCondition">A <see cref="IExpression"/> instance which
        /// represents a value to check on each case of the <see cref="ISwitchStatement"/>
        /// that results.</param>
        /// <returns>A new <see cref="ISwitchStatement"/> with no cases relative to the
        /// <paramref name="caseCondition"/> provided.</returns>
        public ISwitchStatement Switch(IExpression caseCondition)
        {
            var result = new SwitchStatement(this.Owner) { Selection = caseCondition };
            base.baseList.Add(result);
            return result;
        }

        #region Call Method insertion

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallFusionStatement"/> with the
        /// <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IExpressionToCommaFusionExpression"/> which represents
        /// a fusion between a seemingly callable expression and a series of parameters which
        /// were fused.</param>
        /// <returns>A new <see cref="ICallFusionStatement"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/>
        /// is null.</exception>
        public ICallFusionStatement Call(IExpressionToCommaFusionExpression target)
        {
            var result = new CallFusionStatement(this.Owner) { Target = target };
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMethodInvokeExpression"/> to reference
        /// for the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/>
        /// is null.</exception>
        public ICallMethodStatement Call(IMethodInvokeExpression target)
        {
            var result = new CallMethodStatement(this.Owner) { Target = target };
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="ptr"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="ptr">The <see cref="IMethodPointerReferenceExpression"/> that identifies
        /// the pointer to the method to call.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, params IExpression[] parameters)
        {
            return this.Call(new MethodInvokeExpression(ptr, parameters.ToCollection()));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="ptr"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="ptr">The <see cref="IMethodPointerReferenceExpression"/>
        /// in which to reference for the new <see cref="ICallMethodStatement"/>.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, IExpressionCollection parameters)
        {
            return this.Call(new MethodInvokeExpression(ptr, parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="stub"/>, and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="stub">The <see cref="IMethodReferenceStub"/> which
        /// specifies the origin of the call, its name, and calling convention.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(IMethodReferenceStub stub, params IExpression[] parameters)
        {
            return this.Call(new MethodInvokeExpression(stub, parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="stub"/>, and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="stub">The <see cref="IMethodReferenceStub"/> which
        /// specifies the origin of the call, its name, and calling convention.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(IMethodReferenceStub stub, IExpressionCollection parameters)
        {
            return this.Call(stub.Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="parent"/>, <paramref name="methodName"/> and 
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IMemberParentReferenceExpression"/> from which
        /// the call originates.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="methodName"/> is <see cref="String.Empty"/>.</exception>
        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, params IExpression[] parameters)
        {
            return this.Call(parent.Call(methodName, parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="parent"/>, <paramref name="methodName"/> and 
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IMemberParentReferenceExpression"/> from which
        /// the call originates.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="methodName"/> is <see cref="String.Empty"/>.</exception>
        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, IExpressionCollection parameters)
        {
            return this.Call(parent.Call(methodName, parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="parent"/>, <paramref name="methodName"/>, <paramref name="typeParameters"/>
        /// and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IMemberParentReferenceExpression"/> from which
        /// the call originates.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="typeParameters">The <see cref="ITypeCollection"/> representing the types
        /// of the parameters for the call.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parent"/>, <paramref name="methodName"/>, <paramref name="typeParameters"/>, or <paramref name="parameters"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="methodName"/> is <see cref="String.Empty"/>.</exception>
        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
        {
            return this.Call(parent.GetMethod(methodName, typeParameters).Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="parent"/>, <paramref name="methodName"/>, <paramref name="typeParameters"/>
        /// and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IMemberParentReferenceExpression"/> from which
        /// the call originates.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="typeParameters">The <see cref="ITypeCollection"/> representing the types
        /// of the parameters for the call.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parent"/>, <paramref name="methodName"/>, <paramref name="typeParameters"/>, or <paramref name="parameters"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="methodName"/> is <see cref="String.Empty"/>.</exception>
        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
        {
            return this.Call(parent.GetMethod(methodName, typeParameters).Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="methodName"/> and <paramref name="parameters"/>.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(string methodName, params IExpression[] parameters)
        {
            return this.Call(new UnboundMethodReferenceStub(methodName).Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="methodName"/> and <paramref name="parameters"/>.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(string methodName, IExpressionCollection parameters)
        {
            return this.Call(new UnboundMethodReferenceStub(methodName).Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="methodName"/>, <paramref name="typeParameters"/>, and 
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="typeParameters">The <see cref="ITypeCollection"/> representing the types
        /// of the parameters for the call.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
        {
            return this.Call(new UnboundMethodReferenceStub(methodName, typeParameters).Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="methodName"/>, <paramref name="typeParameters"/>, and 
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="typeParameters">The <see cref="ITypeCollection"/> representing the types
        /// of the parameters for the call.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
        {
            return this.Call(new UnboundMethodReferenceStub(methodName, typeParameters).Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="callType"/>, <paramref name="methodName"/> and 
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="callType">
        /// The <see cref="MethodReferenceType"/> which determines
        /// whether to follow the virtual calling convention.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, params IExpression[] parameters)
        {
            return this.Call(new UnboundMethodReferenceStub(methodName, callType).Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="callType"/>, <paramref name="methodName"/> and 
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="callType">
        /// The <see cref="MethodReferenceType"/> which determines
        /// whether to follow the virtual calling convention.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, IExpressionCollection parameters)
        {
            return this.Call(new UnboundMethodReferenceStub(methodName, callType).Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="callType"/>, <paramref name="methodName"/>, <paramref name="typeParameters"/> and 
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="callType">
        /// The <see cref="MethodReferenceType"/> which determines
        /// whether to follow the virtual calling convention.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="typeParameters">The <see cref="ITypeCollection"/> representing the types
        /// of the parameters for the call.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
        {
            return this.Call(new UnboundMethodReferenceStub(methodName, typeParameters, callType).Invoke(parameters));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="callType"/>, <paramref name="methodName"/>, <paramref name="typeParameters"/> and 
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="callType">
        /// The <see cref="MethodReferenceType"/> which determines
        /// whether to follow the virtual calling convention.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="typeParameters">The <see cref="ITypeCollection"/> representing the types
        /// of the parameters for the call.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
        {
            return this.Call(new UnboundMethodReferenceStub(methodName, typeParameters, callType).Invoke(parameters));
        }

        #endregion

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="IIterationBlockStatement"/> which
        /// has a series of <paramref name="initializers"/>, a continuation <paramref name="condition"/>
        /// and a series of <paramref name="iterations"/>.
        /// </summary>
        /// <param name="initializers">The series of <see cref="IStatementExpression"/>
        /// elements which describe what to do during the initialization phase of the iterator.</param>
        /// <param name="condition">The <see cref="Boolean"/> <see cref="IExpression"/>
        /// which denotes the condition to evaluate prior to executing the iteration's block body.</param>
        /// <param name="iterations">The series of <see cref="IStatementExpression"/>
        /// values which denote the logic to execute after the code-block, 
        /// but prior to checking the continuation <paramref name="condition"/>.
        /// </param>
        /// <returns>A new <see cref="IIterationBlockStatement"/> which
        /// has a series of <paramref name="initializers"/>, a continuation <paramref name="condition"/>
        /// and a series of <paramref name="iterations"/> which denote the logic used prior to the 
        /// <paramref name="condition"/> check but before executing the inner block of the iteration.</returns>
        public IIterationBlockStatement Iterate(IEnumerable<IStatementExpression> initializers, IExpression condition, IEnumerable<IStatementExpression> iterations)
        {
            var result = new IterationBlockStatement(this.Owner, initializers, condition, iterations);
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Creates, inserts and returns a new <see cref="IIterationDeclarationBlockStatement"/>
        /// which defines a local through the <paramref name="localDeclaration"/>,
        /// a continuation <paramref name="condition"/> and a series of <paramref name="iterations"/>.
        /// </summary>
        /// <param name="localDeclaration">A <see cref="ILocalDeclarationStatement"/>
        /// which defines the local used within the scope of the iteration block.</param>
        /// <param name="condition">The <see cref="Boolean"/> <see cref="IExpression"/>
        /// which denotes the condition to evaluate prior to executing the iteration's block body.</param>
        /// <param name="iterations">The series of <see cref="IStatementExpression"/>
        /// values which denote the logic to execute after the code-block, 
        /// but prior to checking the continuation <paramref name="condition"/>.
        /// </param>
        /// <returns>A new <see cref="IIterationDeclarationBlockStatement"/>
        /// which defines a local through the <paramref name="localDeclaration"/>,
        /// a continuation <paramref name="condition"/> and a series of <paramref name="iterations"/>.</returns>
        public IIterationDeclarationBlockStatement Iterate(ILocalDeclarationStatement localDeclaration, IExpression condition, IEnumerable<IStatementExpression> iterations)
        {
            var result = new IterationDeclarationBlockStatement(this.owner, localDeclaration, condition, iterations);
            this.baseList.Add(result);
            return result;
        }

        public ISimpleIterationBlockStatement Iterate(ILocalDeclarationStatement target, IExpression start, IExpression end, bool endExclusive = true, IExpression incremental = null)
        {
            var initializer = start;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates, inserts and returns a new 
        /// <see cref="IEnumerateSetBreakableBlockStatement"/> which represents an 
        /// iteration over the <paramref name="targetName">individual elements</paramref>
        /// of a <paramref name="source">set</paramref>, either fixed
        /// or dynamic in nature.
        /// </summary>
        /// <param name="targetName">The <see cref="String"/> 
        /// which represents the name of the local to receive the elements of
        /// <paramref name="source"/> during enumeration.
        /// </param>
        /// <param name="source">The <see cref="IExpression"/> which designates
        /// where the information comes from.</param>
        /// <returns>A new <see cref="IEnumerateSetBreakableBlockStatement"/> which represents 
        /// the operation.</returns>
        public IEnumerateSetBreakableBlockStatement Enumerate(string targetName, IExpression source)
        {
            var result = new EnumerateSetBreakableBlockStatement(this, targetName) { Source = source };
            this.baseList.Add(result);
            return result;
        }

        public ILocalDeclarationStatement DefineLocal(ILocalMember local)
        {
            var result = local.GetDeclarationStatement();
            base.baseList.Add(result);
            return result;
        }

        public ILabelStatement DefineLabel(string name)
        {
            var label = this.Labels.Add(name);
            this.baseList.Add(label);
            return label;
        }

        public void DefineLabel(ILabelStatement label)
        {
            if (!this.Labels.Values.Contains(label))
                this.Labels.Add(label);
            this.baseList.Add(label);
        }

        public BlockStatementLabelDictionary Labels
        {
            get {
                if (this.labels == null)
                    this.labels = new BlockStatementLabelDictionary(this.owner);
                return this.labels;
            }
        }

        IBlockStatementLabelDictionary IBlockStatementParent.Labels
        {
            get {
                return this.Labels;
            }
        }

        public IBlockStatementLabelDictionary ScopeLabels
        {
            get {
                if (this.scopeLabels == null)
                    this.scopeLabels = new BlockStatementScopeLabelDictionary(this);
                return this.scopeLabels;
            }
        }

        public IExpressionStatement Assign(IMemberReferenceExpression target, AssignmentOperation operation, INaryOperandExpression value)
        {
            var result = new ExpressionStatement(this.owner, new AssignmentExpression(target, operation, value));
            base.baseList.Add(result);
            return result;
        }

        public IExpressionStatement Assign(IMemberReferenceExpression target, INaryOperandExpression value)
        {
            return this.Assign(target,  AssignmentOperation.SimpleAssign, value);
        }

        public IExpressionStatement Assign(IExpressionFusionExpression target, AssignmentOperation operation, INaryOperandExpression value)
        {
            var result = new ExpressionStatement(this.owner, new AssignmentExpression(target, operation, value));
            base.baseList.Add(result);
            return result;
        }

        public IExpressionStatement Assign(IExpressionFusionExpression target, INaryOperandExpression value)
        {
            return this.Assign(target, AssignmentOperation.SimpleAssign, value);
        }

        public IExpressionStatement Increment(IAssignTargetExpression target)
        {
            var result = new ExpressionStatement(this.owner, new UnaryOperationExpression(target, UnaryOperation.Increment | UnaryOperation.PostAction));
            this.baseList.Add(result);
            return result;
        }

        public IExpressionStatement Increment(IAssignTargetExpression target, INaryOperandExpression incrementBy)
        {
            var result = new ExpressionStatement(this.owner, new AssignmentExpression(target, AssignmentOperation.AddAssign, incrementBy));
            this.baseList.Add(result);
            return result;
        }

        public IExpressionStatement Decrement(IAssignTargetExpression target)
        {
            var result = new ExpressionStatement(this.owner, new UnaryOperationExpression(target, UnaryOperation.Decrement | UnaryOperation.PostAction));
            this.baseList.Add(result);
            return result;
        }

        public IExpressionStatement Decrement(IAssignTargetExpression target, INaryOperandExpression decrementBy)
        {
            var result = new ExpressionStatement(this.owner, new AssignmentExpression(target, AssignmentOperation.SubtractionAssign, decrementBy));
            this.baseList.Add(result);
            return result;
        }

        public IJumpStatement Jump(IJumpTarget target)
        {
            var jumpStatement = new JumpStatement(this.owner, target);
            this.baseList.Add(jumpStatement);
            return jumpStatement;
        }

        public IGoToStatement GoTo(ILabelStatement target)
        {
            var gotoStatement = target.GetGoTo(this.owner);
            this.baseList.Add(gotoStatement);
            return gotoStatement;
        }
        #region Event Handler Delegation

        /// <summary>
        /// Creates, inserts and returns a <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// using <see cref="EventHandlerChangeKind.Add"/> to denote the kind of action on
        /// the event handler, with the the <paramref name="event"/>, and
        /// <paramref name="method"/> provided.
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
        /// <param name="event">The <typeparamref name="TEvent"/>
        /// which references the event in question.</param>
        /// <param name="method">The <typeparamref name="TSignature"/>
        /// which denotes the method to add to the event.</param>
        /// <returns>A <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="event"/>, or <paramref name="method"/>
        /// is null.</exception>
        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> AddHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> @event, TSignature method)
            where TEvent :
                class,
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignature :
                class,
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return this.ChangeHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(@event, EventHandlerChangeKind.Add, method);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// using <see cref="EventHandlerChangeKind.Add"/> to denote the kind of action on
        /// the event handler, with the the <paramref name="targetEvent"/>, and
        /// <paramref name="methodPtr"/> provided.
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
        /// <param name="targetEvent">The <see cref="IEventReferenceExpression<TEvent, TEventParameter, TEventParent>"/>
        /// which references the event in question.</param>
        /// <param name="methodPtr">The <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which denotes the method to add to the event.</param>
        /// <returns>A <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="event"/>, or <paramref name="method"/>
        /// is null.</exception>
        /// <remarks>Exceedingly long type signature due to being a bound expression, typical use is for the compiler or
        /// code generators that know the exact event and method to apply to.</remarks>
        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> AddHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent, IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> methodPtr)
            where TEvent :
                class,
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignature :
                class,
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return this.ChangeHandler(targetEvent, EventHandlerChangeKind.Add, methodPtr);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// using <see cref="EventHandlerChangeKind.Add"/> to denote the kind of action on
        /// the event handler, with the the <paramref name="target"/>, 
        /// and <paramref name="sourceMethod"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IEventReferenceExpression"/>
        /// which references the event in question.</param>
        /// <param name="sourceMethod">The <see cref="IMethodPointerReferenceExpression"/>
        /// which denotes the method to point to.</param>
        /// <returns>A new <see cref="IChangeEventHandlerStatement"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/> or <paramref name="sourceMethod"/> is
        /// null.</exception>
        public IChangeEventHandlerStatement AddHandler(IEventReferenceExpression target, IMethodPointerReferenceExpression sourceMethod)
        {
            return ChangeHandler(target, EventHandlerChangeKind.Add, sourceMethod);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// using <see cref="EventHandlerChangeKind.Add"/> to denote the kind of action on
        /// the event handler, with the the<paramref name="target"/>, <paramref name="eventName"/>
        /// and <paramref name="sourceMethod"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMemberParentReferenceExpression"/> which contains the event via
        /// <paramref name="eventName"/>.</param>
        /// <param name="eventName">The <see cref="String"/> value representing the unique name of the
        /// event in question.</param>
        /// <param name="sourceMethod">The <see cref="IMethodPointerReferenceExpression"/>
        /// which denotes the method to point to.</param>
        /// <returns>A new <see cref="IChangeEventHandlerStatement"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/>, <paramref name="eventName"/> or
        /// <paramref name="sourceMethod"/> is null.</exception>
        public IChangeEventHandlerStatement AddHandler(IMemberParentReferenceExpression target, string eventName, IMethodPointerReferenceExpression sourceMethod)
        {
            return ChangeHandler(target, eventName, EventHandlerChangeKind.Add, sourceMethod);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// using <see cref="EventHandlerChangeKind.Add"/> to denote the kind of action on
        /// the event handler, with the the <paramref name="target"/>, <paramref name="eventName"/>
        /// and <paramref name="methodName"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMemberParentReferenceExpression"/> which contains the event via
        /// <paramref name="eventName"/>.</param>
        /// <param name="eventName">The <see cref="String"/> value representing the unique name of the
        /// event in question.</param>
        /// <param name="methodName">The <see cref="String"/> value representing the unique name of the
        /// method containing a signature that matches the event of <paramref name="eventName"/>.</param>
        /// <returns>A new <see cref="IChangeEventHandlerStatement"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/>, <paramref name="eventName"/> or
        /// <paramref name="methodName"/> is null.</exception>
        public IChangeEventHandlerStatement AddHandler(IMemberParentReferenceExpression target, string eventName, string methodName)
        {
            return ChangeHandler(target, eventName, EventHandlerChangeKind.Add, methodName);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// with the the <paramref name="changeKind"/>, <paramref name="event"/>, and
        /// <paramref name="method"/> provided.
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
        /// <param name="event">The <typeparamref name="TEvent"/>
        /// which references the event in question.</param>
        /// <param name="changeKind">The <see cref="EventHandlerChangeKind"/> which denotes
        /// the kind of change on the event to make.</param>
        /// <param name="method">The <typeparamref name="TSignature"/>
        /// which denotes the method to add to the event.</param>
        /// <returns>A <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="event"/>, or <paramref name="method"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when 
        /// <paramref name="changeKind"/> is out of the valid range of values 
        /// (<see cref="EventHandlerChangeKind.Add"/> or 
        /// <see cref="EventHandlerChangeKind.Remove"/>).
        /// </exception>
        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> ChangeHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> @event, EventHandlerChangeKind changeKind, TSignature method)
            where TEvent :
                class,
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignature :
                class,
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            var result = new BoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(this.Owner, @event, changeKind, method.GetMethodReference<TSignatureParameter, TSignature, TSignatureParent>().GetPointer());
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// with the the <paramref name="targetEvent"/>, <see cref="changeKind"/>, and
        /// <paramref name="methodPtr"/> provided.
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
        /// <param name="targetEvent">The <see cref="IEventReferenceExpression<TEvent, TEventParameter, TEventParent>"/>
        /// which references the event in question.</param>
        /// <param name="changeKind">The <see cref="EventHandlerChangeKind"/> which denotes
        /// the kind of change on the event to make.</param>
        /// <param name="methodPtr">The <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which denotes the method to remove from the event.</param>
        /// <returns>A <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="event"/>, or <paramref name="method"/>
        /// is null.</exception>
        /// <remarks>Exceedingly long type signature due to being a bound expression, typical use is for the compiler or
        /// code generators that know the exact event and method to apply to.</remarks>
        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> ChangeHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> methodPtr)
            where TEvent :
                class,
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignature :
                class,
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            var result = new BoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(this.owner, targetEvent, changeKind, methodPtr);
            this.baseList.Add(result);
            return result;
        }
        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// with the <paramref name="changeKind"/>, <paramref name="target"/>, 
        /// and <paramref name="sourceMethod"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IEventReferenceExpression"/> which denotes the 
        /// event to target.</param>
        /// <param name="changeKind">The <see cref="EventHandlerChangeKind"/> which denotes
        /// the kind of change on the event to make.</param>
        /// <param name="sourceMethod">The <see cref="IMethodPointerReferenceExpression"/> which
        /// denotes the method to change.</param>
        /// <returns>A new <see cref="IChangeEventHandlerStatement"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/> or <paramref name="sourceMethod"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when 
        /// <paramref name="changeKind"/> is out of the valid range of values 
        /// (<see cref="EventHandlerChangeKind.Add"/> or 
        /// <see cref="EventHandlerChangeKind.Remove"/>).
        /// </exception>
        public IChangeEventHandlerStatement ChangeHandler(IEventReferenceExpression target, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression sourceMethod)
        {
            var result = new ChangeEventHandlerStatement(target, changeKind, sourceMethod, this.Owner);
            this.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// with the <paramref name="changeKind"/>, <paramref name="target"/>, <paramref name="eventName"/>, and
        /// <paramref name="sourceMethod"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMemberParentReferenceExpression"/> which denotes the 
        /// event to target.</param>
        /// <param name="eventName">The <see cref="String"/> value which denotes the unique name of the event.</param>
        /// <param name="changeKind">The <see cref="EventHandlerChangeKind"/>
        /// that denotes the kind of change on the event to make.</param>
        /// <param name="sourceMethod">The <see cref="IMethodPointerReferenceExpression"/>
        /// which denotes the method to change.</param>
        /// <returns>A new <see cref="IChangeEventHandlerStatement"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/>, <paramref name="eventName"/>, or <paramref name="sourceMethod"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when 
        /// <paramref name="changeKind"/> is out of the valid range of values 
        /// (<see cref="EventHandlerChangeKind.Add"/> or 
        /// <see cref="EventHandlerChangeKind.Remove"/>).
        /// </exception>
        public IChangeEventHandlerStatement ChangeHandler(IMemberParentReferenceExpression target, string eventName, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression sourceMethod)
        {
            return ChangeHandler(target.GetEvent(eventName), changeKind, sourceMethod);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// with the <paramref name="changeKind"/>, <paramref name="target"/>, <paramref name="eventName"/>, and
        /// <paramref name="methodName"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMemberParentReferenceExpression"/> which denotes the 
        /// event to target.</param>
        /// <param name="eventName">The <see cref="String"/> value which denotes the unique name of the event.</param>
        /// <param name="changeKind">The <see cref="EventHandlerChangeKind"/>
        /// that denotes the kind of change on the event to make.</param>
        /// <param name="methodName">The <see cref="String"/> value representing the unique name of the
        /// method containing a signature that matches the event of <paramref name="eventName"/>.</param>
        /// <returns>A new <see cref="IChangeEventHandlerStatement"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/>, <paramref name="eventName"/>, or <paramref name="methodName"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when 
        /// <paramref name="changeKind"/> is out of the valid range of values 
        /// (<see cref="EventHandlerChangeKind.Add"/> or 
        /// <see cref="EventHandlerChangeKind.Remove"/>).
        /// </exception>
        public IChangeEventHandlerStatement ChangeHandler(IMemberParentReferenceExpression target, string eventName, EventHandlerChangeKind changeKind, string methodName)
        {
            return ChangeHandler(target.GetEvent(eventName), changeKind, new UnboundMethodReferenceStub(methodName).GetPointer());
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// using <see cref="EventHandlerChangeKind.Remove"/> to denote the kind of action on
        /// the event handler, with the the <paramref name="event"/>, and
        /// <paramref name="method"/> provided.
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
        /// <param name="event">The <typeparamref name="TEvent"/>
        /// which references the event in question.</param>
        /// <param name="method">The <typeparamref name="TSignature"/>
        /// which denotes the method to remove from the event.</param>
        /// <returns>A <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="event"/>, or <paramref name="method"/>
        /// is null.</exception>
        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> RemoveHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> @event, TSignature method)
            where TEvent :
                class,
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignature :
                class,
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return this.ChangeHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(@event, EventHandlerChangeKind.Remove, method);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// using <see cref="EventHandlerChangeKind.Remove"/> to denote the kind of action on
        /// the event handler, with the the <paramref name="targetEvent"/>, and
        /// <paramref name="methodPtr"/> provided.
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
        /// <param name="targetEvent">The <see cref="IEventReferenceExpression<TEvent, TEventParameter, TEventParent>"/>
        /// which references the event in question.</param>
        /// <param name="methodPtr">The <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which denotes the method to remove from the event.</param>
        /// <returns>A <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="event"/>, or <paramref name="method"/>
        /// is null.</exception>
        /// <remarks>Exceedingly long type signature due to being a bound expression, typical use is for the compiler or
        /// code generators that know the exact event and method to apply to.</remarks>
        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> RemoveHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent, IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> methodPtr)
            where TEvent :
                class,
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignature :
                class,
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return ChangeHandler(targetEvent, EventHandlerChangeKind.Remove, methodPtr);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// using <see cref="EventHandlerChangeKind.Remove"/> to denote the kind of action on
        /// the event handler, with the the <paramref name="target"/>, and
        /// <paramref name="sourceMethod"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IEventReferenceExpression"/>
        /// which references the event in question.</param>
        /// <param name="sourceMethod">The <see cref="IMethodPointerReferenceExpression"/>
        /// which denotes the method to point to.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/> or <paramref name="sourceMethod"/> is
        /// null.</exception>
        public IChangeEventHandlerStatement RemoveHandler(IEventReferenceExpression target, IMethodPointerReferenceExpression sourceMethod)
        {
            return ChangeHandler(target, EventHandlerChangeKind.Remove, sourceMethod);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// using <see cref="EventHandlerChangeKind.Remove"/> to denote the kind of action on
        /// the event handler, with the the<paramref name="target"/>, <paramref name="eventName"/>
        /// and <paramref name="sourceMethod"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMemberParentReferenceExpression"/> which contains the event via
        /// <paramref name="eventName"/>.</param>
        /// <param name="eventName">The <see cref="String"/> value representing the unique name of the
        /// event in question.</param>
        /// <param name="sourceMethod">The <see cref="IMethodPointerReferenceExpression"/>
        /// which denotes the method to point to.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/>, <paramref name="eventName"/> or
        /// <paramref name="sourceMethod"/> is null.</exception>
        public IChangeEventHandlerStatement RemoveHandler(IMemberParentReferenceExpression target, string eventName, IMethodPointerReferenceExpression sourceMethod)
        {
            return ChangeHandler(target, eventName, EventHandlerChangeKind.Remove, sourceMethod);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// using <see cref="EventHandlerChangeKind.Remove"/> to denote the kind of action on
        /// the event handler, with the the <paramref name="target"/>, <paramref name="eventName"/>
        /// and <paramref name="methodName"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMemberParentReferenceExpression"/> which contains the event via
        /// <paramref name="eventName"/>.</param>
        /// <param name="eventName">The <see cref="String"/> value representing the unique name of the
        /// event in question.</param>
        /// <param name="methodName">The <see cref="String"/> value representing the unique name of the
        /// method containing a signature that matches the event of <paramref name="eventName"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/>, <paramref name="eventName"/> or
        /// <paramref name="methodName"/> is null.</exception>
        public IChangeEventHandlerStatement RemoveHandler(IMemberParentReferenceExpression target, string eventName, string methodName)
        {
            return ChangeHandler(target, eventName, EventHandlerChangeKind.Remove, methodName);
        }

        #endregion

        public ICommentStatement Comment(string comment)
        {
            var result = new CommentStatement(this.Owner, comment);
            this.baseList.Add(result);
            return result;
        }
        #endregion

        internal virtual IConditionBlockStatement OnIf(IExpression condition)
        {
            return new ConditionBlockStatement(this.Owner) { Condition = condition };
        }

        internal virtual IBlockStatementParent Owner
        {
            get
            {
                return this.owner;
            }
        }

        #region IIntermediateTypeParent Members

        public IIntermediateAssembly Assembly
        {
            get
            {
                var current = this.owner;
                while (current != null)
                {
                    if (current is IBlockStatement)
                    {
                        current = ((IBlockStatement)current).Parent;
                    }
                    else if (current is IIntermediateTypeParent)
                    {
                        var currentSig = current as IIntermediateTypeParent;
                        return currentSig.Assembly;
                    }
                }
                return null;
            }
        }

        public IIntermediateClassTypeDictionary Classes
        {
            get
            {
                this.CheckClasses();
                return this.classes;
            }
        }

        public IIntermediateDelegateTypeDictionary Delegates
        {
            get
            {
                this.CheckDelegates();
                return this.delegates;
            }
        }

        public IIntermediateEnumTypeDictionary Enums
        {
            get
            {
                this.CheckEnums();
                return this.enums;
            }
        }

        public IIntermediateInterfaceTypeDictionary Interfaces
        {
            get
            {
                this.CheckInterfaces();
                return this.interfaces;
            }
        }

        public IIntermediateStructTypeDictionary Structs
        {
            get
            {
                this.CheckStructs();
                return this.structs;
            }
        }

        private IntermediateFullTypeDictionary _Types
        {
            get
            {
                this.Check_Types();
                return this.types;
            }
        }

        public IIntermediateFullTypeDictionary Types
        {
            get
            {
                this.CheckClasses();
                this.CheckDelegates();
                this.CheckEnums();
                this.CheckInterfaces();
                this.CheckStructs();
                return this._Types;
            }
        }

        #endregion

        #region ITypeParent Members

        IClassTypeDictionary ITypeParent.Classes
        {
            get { return this.Classes; }
        }

        IDelegateTypeDictionary ITypeParent.Delegates
        {
            get { return this.Delegates; }
        }

        IEnumTypeDictionary ITypeParent.Enums
        {
            get { return this.Enums; }
        }

        IInterfaceTypeDictionary ITypeParent.Interfaces
        {
            get { return this.Interfaces; }
        }

        IStructTypeDictionary ITypeParent.Structs
        {
            get { return this.Structs; }
        }

        IFullTypeDictionary ITypeParent.Types
        {
            get { return this.Types; }
        }

        IAssembly ITypeParent.Assembly
        {
            get { return this.Assembly; }
        }

        public IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get {
                return this.types.Keys.Cast<IGeneralDeclarationUniqueIdentifier>().Concat(
                    this.Locals.Keys);
            }
        }
        #endregion

        #region Member Check Methods

        private void CheckClasses()
        {
            if (this.classes == null)
                this.classes = this.InitializeClasses();
        }

        private void CheckDelegates()
        {
            if (this.delegates == null)
                this.delegates = this.InitializeDelegates();
        }

        private void CheckEnums()
        {
            if (this.enums == null)
                this.enums = this.InitializeEnums();
        }

        private void CheckInterfaces()
        {
            if (this.interfaces == null)
                this.interfaces = this.InitializeInterfaces();
        }

        private void CheckStructs()
        {
            if (this.structs == null)
                this.structs = this.InitializeStructs();
        }

        private void Check_Types()
        {
            if (this.types == null)
                this.types = this.InitializeTypes();
        }
        #endregion

        #region Initializers

        /// <summary>
        /// Initializes the <see cref="Classes"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateClassTypeDictionary"/> instance.</returns>
        protected virtual IntermediateClassTypeDictionary InitializeClasses()
        {
            return new IntermediateClassTypeDictionary(this, this._Types);
        }

        /// <summary>
        /// Initializes the <see cref="Delegates"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateDelegateTypeDictionary"/> instance</returns>
        protected virtual IntermediateDelegateTypeDictionary InitializeDelegates()
        {
            return new IntermediateDelegateTypeDictionary(this, this._Types);
        }

        /// <summary>
        /// Initializes the <see cref="Enums"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateEnumTypeDictionary"/> instance</returns>
        protected virtual IntermediateEnumTypeDictionary InitializeEnums()
        {
            return new IntermediateEnumTypeDictionary(this, this._Types);
        }

        /// <summary>
        /// Initializes the <see cref="Interfaces"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateInterfaceTypeDictionary"/> instance</returns>
        protected virtual IntermediateInterfaceTypeDictionary InitializeInterfaces()
        {
            return new IntermediateInterfaceTypeDictionary(this, this._Types);
        }

        /// <summary>
        /// Initializes the <see cref="Structs"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateStructTypeDictionary"/> instance</returns>
        protected virtual IntermediateStructTypeDictionary InitializeStructs()
        {
            return new IntermediateStructTypeDictionary(this, this._Types);
        }

        /// <summary>
        /// Initializes the full types container to a default state if the 
        /// current <see cref="IntermediateNamespaceDeclaration"/> is 
        /// the root instance; otherwise, 
        /// </summary>
        /// <returns>A new <see cref="IntermediateFullTypeDictionary"/> instance</returns>
        protected virtual IntermediateFullTypeDictionary InitializeTypes()
        {
            return new IntermediateFullTypeDictionary(this);
        }

        #endregion

        public IScopeCoercionCollection ScopeCoercions
        {
            get
            {
                if (this.scopeCoercions == null)
                    this.scopeCoercions = new ScopeCoercionCollection();
                return this.scopeCoercions;
            }
        }

    }
}
