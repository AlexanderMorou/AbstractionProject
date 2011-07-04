using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public abstract class IntermediateCoercionMemberBase<TCoercion, TIntermediateCoercion, TType, TIntermediateType> :
        IntermediateMemberBase<TType, TIntermediateType>,
        IIntermediateCoercionMember<TCoercion, TIntermediateCoercion, TType, TIntermediateType>,
        ITopBlockStatement
        where TCoercion :
            ICoercionMember<TCoercion, TType>
        where TIntermediateCoercion :
            TCoercion,
            IIntermediateCoercionMember<TCoercion, TIntermediateCoercion, TType, TIntermediateType>
        where TType :
            ICoercibleType<TCoercion, TType>
        where TIntermediateType :
            TType,
            IIntermediateCoercibleType<TCoercion, TIntermediateCoercion, TType, TIntermediateType>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/> which contains the 
        /// <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>.</param>
        protected IntermediateCoercionMemberBase(TIntermediateType parent)
            : base(parent)
        {
        }
        #region IIntermediateCoercionMember Members

        IIntermediateCoercibleType IIntermediateCoercionMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IIntermediateScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get
            {
                return AccessLevelModifiers.Public;
            }
            set
            {
                throw new NotSupportedException("Altering the access level of expression coercion members is not supported.");
            }
        }

        #endregion

        #region ICoercionMember Members

        ICoercibleType ICoercionMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion


        #region TopBlock Members

        private BlockStatementParentContainer statementContainer;

        private BlockStatementParentContainer StatementContainer
        {
            get
            {
                this.CheckStatementContainer();
                return this.statementContainer;
            }
        }

        private void CheckStatementContainer()
        {
            if (this.statementContainer == null)
                this.statementContainer = new BlockStatementParentContainer(this);
        }

        #region IBlockStatementParent Members

        public ILocalMemberDictionary Locals
        {
            get
            {
                return this.StatementContainer.Locals;
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
            return this.StatementContainer.Return();
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
            return this.StatementContainer.Return(value);
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
            return this.StatementContainer.If(condition);
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
            return this.StatementContainer.Switch(caseCondition);
        }

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
            return this.StatementContainer.Call(target);
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
            return this.StatementContainer.Call(target);
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
            return this.StatementContainer.Call(ptr, parameters);
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
            return this.StatementContainer.Call(ptr, parameters);
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
            return this.StatementContainer.Call(stub, parameters);
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
            return this.StatementContainer.Call(stub, parameters);
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
            return this.StatementContainer.Call(parent, methodName, parameters);
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
            return this.StatementContainer.Call(parent, methodName, parameters);
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
            return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
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
            return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
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
            return this.StatementContainer.Call(methodName, parameters);
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
            return this.StatementContainer.Call(methodName, parameters);
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
            return this.StatementContainer.Call(methodName, typeParameters, parameters);
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
            return this.StatementContainer.Call(methodName, typeParameters, parameters);
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
            return this.StatementContainer.Call(callType, methodName, parameters);
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
            return this.StatementContainer.Call(callType, methodName, parameters);
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
            return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
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
            return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
        }

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
            return this.StatementContainer.Iterate(initializers, condition, iterations);
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
        /// which represents the operation..</returns>
        public IIterationDeclarationBlockStatement Iterate(ILocalDeclarationStatement localDeclaration, IExpression condition, IEnumerable<IStatementExpression> iterations)
        {
            return this.StatementContainer.Iterate(localDeclaration, condition, iterations);
        }

        public ISimpleIterationBlockStatement Iterate(ILocalDeclarationStatement target, IExpression start, IExpression end, bool endExclusive = true, IExpression incremental = null)
        {
            return this.StatementContainer.Iterate(target, start, end, endExclusive, incremental);
        }

        /// <summary>
        /// Creates, inserts and returns a new 
        /// <see cref="IEnumerateSetBreakableBlockStatement"/> which represents an 
        /// iteration over the <paramref name="target">individual elements</paramref>
        /// of a <paramref name="source">set</paramref>, either fixed
        /// or dynamic in nature.
        /// </summary>
        /// <param name="target">The <see cref="ILocalMember"/> 
        /// which should receive the elements of the
        /// <paramref name="source"/> as a part of the iteration.
        /// </param>
        /// <param name="source">The <see cref="IExpression"/> which designates
        /// where the information comes from.</param>
        /// <returns>A new <see cref="IEnumerateSetBreakableBlockStatement"/> which represents 
        /// the operation.</returns>
        public IEnumerateSetBreakableBlockStatement Enumerate(ILocalMember target, IExpression source)
        {
            return this.StatementContainer.Enumerate(target, source);
        }

        /// <summary>
        /// Creates an inserts a <see cref="ILocalDeclarationStatement"/> with the
        /// <paramref name="local"/> provided.
        /// </summary>
        /// <param name="local">The <see cref="ILocalMember"/> to declare.</param>
        /// <returns>A new <see cref="ILocalDeclarationStatement"/>
        /// which aims to declare the <paramref name="local"/> provided.</returns>
        public ILocalDeclarationStatement DefineLocal(ILocalMember local)
        {
            return this.StatementContainer.DefineLocal(local);
        }

        /// <summary>
        /// Defines a <see cref="ILabelStatement"/> with the 
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// value associated to the label to create.</param>
        /// <returns>A new <see cref="ILabelStatement"/> relative to the 
        /// <paramref name="name"/> provided.</returns>
        public ILabelStatement DefineLabel(string name)
        {
            return this.StatementContainer.DefineLabel(name);
        }

        /// <summary>
        /// Defines the provided <paramref name="label"/>.
        /// </summary>
        /// <param name="label">The <see cref="ILabelStatement"/>
        /// to declare</param>
        public void DefineLabel(ILabelStatement label)
        {
            this.StatementContainer.DefineLabel(label);
        }

        /// <summary>
        /// Assigns a <paramref name="value"/> to the <paramref name="target"/>
        /// with the given assignment <paramref name="operation"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMemberReferenceExpression"/> which is the target
        /// of the assignment.</param>
        /// <param name="operation">The <see cref="AssignmentOperation"/> which denotes how the operation
        /// should be managed.</param>
        /// <param name="value">The <see cref="INaryOperandExpression"/> which denotes the value
        /// to assign to the <paramref name="target"/>.</param>
        /// <returns>A new <see cref="IExpressionStatement"/> which represents the assignment.</returns>
        public IExpressionStatement Assign(IMemberReferenceExpression target, AssignmentOperation operation, INaryOperandExpression value)
        {
            return this.StatementContainer.Assign(target, operation, value);
        }

        /// <summary>
        /// Assigns a <paramref name="value"/> to the <paramref name="target"/>
        /// with the standard <see cref="AssignmentOperation.SimpleAssign"/> operation.
        /// </summary>
        /// <param name="target">The <see cref="IMemberReferenceExpression"/> which is the target of the
        /// assignment.</param>
        /// <param name="value">The <see cref="INaryOperandExpression"/> which denotes the value
        /// to assign to the <paramref name="target"/>.</param>
        /// <returns>A new <see cref="IExpressionStatement"/> which represents the assignment.</returns>
        public IExpressionStatement Assign(IMemberReferenceExpression target, INaryOperandExpression value)
        {
            return this.StatementContainer.Assign(target, value);
        }

        /// <summary>
        /// Increments the <paramref name="target"/> variable by one.
        /// </summary>
        /// <param name="target">The <see cref="IAssignTargetExpression"/>
        /// which can be incremented.</param>
        /// <returns>A <see cref="IExpressionStatement"/> which represents the 
        /// increment operation.</returns>
        public IExpressionStatement Increment(IAssignTargetExpression target)
        {
            return this.StatementContainer.Increment(target);
        }

        /// <summary>
        /// Increments the <paramref name="target"/> by the <paramref name="incrementBy"/> value.
        /// </summary>
        /// <param name="target">The <see cref="IAssignTargetExpression"/> 
        /// which can be incremented.</param>
        /// <param name="incrementBy">The <see cref="INaryOperandExpression"/> which acts 
        /// as the operand in the increment operation.</param>
        /// <returns>A <see cref="IExpressionStatement"/> which represents the
        /// increment operation.</returns>
        public IExpressionStatement Increment(IAssignTargetExpression target, INaryOperandExpression incrementBy)
        {
            return this.StatementContainer.Increment(target, incrementBy);
        }

        /// <summary>
        /// Decrements the <paramref name="target"/> variable by one.
        /// </summary>
        /// <param name="target">The <see cref="IAssignTargetExpression"/>
        /// which can be decremented.</param>
        /// <returns>A <see cref="IExpressionStatement"/> which represents the 
        /// decrement operation.</returns>
        public IExpressionStatement Decrement(IAssignTargetExpression target)
        {
            return this.StatementContainer.Decrement(target);
        }

        /// <summary>
        /// Decrements the <paramref name="target"/> by the <paramref name="decrementBy"/> value.
        /// </summary>
        /// <param name="target">The <see cref="IAssignTargetExpression"/> 
        /// which can be decremented.</param>
        /// <param name="decrementBy">The <see cref="INaryOperandExpression"/> which acts 
        /// as the operand in the decrement operation.</param>
        /// <returns>A <see cref="IExpressionStatement"/> which represents the
        /// decrement operation.</returns>
        public IExpressionStatement Decrement(IAssignTargetExpression target, INaryOperandExpression decrementBy)
        {
            return this.StatementContainer.Decrement(target, decrementBy);
        }

        public IJumpStatement Jump(IJumpTarget target)
        {
            return this.StatementContainer.Jump(target);
        }

        /// <summary>
        /// Creates, inserts and returns a <see cref="IGoToStatement"/>
        /// which encodes the intent to go to the <paramref name="target"/> 
        /// provided.
        /// </summary>
        /// <param name="target">The <see cref="ILabelStatement"/>
        /// which needs jumped to.</param>
        /// <returns>The <see cref="IGoToStatement"/>
        /// which </returns>
        public IGoToStatement GoTo(ILabelStatement target)
        {
            return this.StatementContainer.GoTo(target);
        }

        /// <summary>
        /// Returns the <see cref="IBlockStatementLabelDictionary"/> of the labels associated to the current
        /// <see cref="IBlockStatementParent"/>.
        /// </summary>
        public IBlockStatementLabelDictionary Labels
        {
            get { return this.StatementContainer.Labels; }
        }

        /// <summary>
        /// Returns the <see cref="IBlockStatementLabelDictionary"/> which
        /// is a unioned dictionary of those contained within the current
        /// <see cref="IBlockStatementParent"/> and those above.
        /// </summary>
        public IBlockStatementLabelDictionary ScopeLabels
        {
            get { return this.StatementContainer.ScopeLabels; }
        }

        public IChangeEventHandlerStatement AddHandler(IEventReferenceExpression targetEvent, IMethodPointerReferenceExpression sourceMethod)
        {
            return this.StatementContainer.AddHandler(targetEvent, sourceMethod);
        }

        public IChangeEventHandlerStatement AddHandler(IMemberParentReferenceExpression target, string eventName, IMethodPointerReferenceExpression sourceMethod)
        {
            return this.StatementContainer.AddHandler(target, eventName, sourceMethod);
        }

        public IChangeEventHandlerStatement AddHandler(IMemberParentReferenceExpression target, string eventName, string methodName)
        {
            return this.StatementContainer.AddHandler(target, eventName, methodName);
        }

        public IChangeEventHandlerStatement ChangeHandler(IEventReferenceExpression targetEvent, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression sourceMethod)
        {
            return this.StatementContainer.ChangeHandler(targetEvent, changeKind, sourceMethod);
        }

        public IChangeEventHandlerStatement ChangeHandler(IMemberParentReferenceExpression target, string eventName, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression sourceMethod)
        {
            return this.StatementContainer.ChangeHandler(target, eventName, changeKind, sourceMethod);
        }

        public IChangeEventHandlerStatement ChangeHandler(IMemberParentReferenceExpression target, string eventName, EventHandlerChangeKind changeKind, string methodName)
        {
            return this.StatementContainer.ChangeHandler(target, eventName, changeKind, methodName);
        }

        public IChangeEventHandlerStatement RemoveHandler(IEventReferenceExpression targetEvent, IMethodPointerReferenceExpression sourceMethod)
        {
            return this.StatementContainer.RemoveHandler(targetEvent, sourceMethod);
        }

        public IChangeEventHandlerStatement RemoveHandler(IMemberParentReferenceExpression target, string eventName, IMethodPointerReferenceExpression sourceMethod)
        {
            return this.StatementContainer.RemoveHandler(target, eventName, sourceMethod);
        }

        public IChangeEventHandlerStatement RemoveHandler(IMemberParentReferenceExpression target, string eventName, string methodName)
        {
            return this.StatementContainer.RemoveHandler(target, eventName, methodName);
        }

        /// <summary>
        /// Inserts a <paramref name="comment"/> into the series of statements.
        /// </summary>
        /// <param name="comment">The <see cref="String"/> value denoting the comment.</param>
        /// <returns>A new <see cref="ICommentStatement"/> which denotes the <paramref name="comment"/>
        /// provided.</returns>
        public ICommentStatement Comment(string comment)
        {
            return this.StatementContainer.Comment(comment);
        }

        #endregion


        #region IControlledStateCollection<IStatement> Members

        /// <summary>
        /// Gets the number of elements contained in the <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>.</summary>
        /// <returns>
        /// The number of elements contained in the <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>.</returns>
        public int Count
        {
            get
            {
                /* *
                 * Null containers don't contain any elements.
                 * */
                if (this.statementContainer == null)
                    return 0;
                return this.statementContainer.Count;
            }
        }

        /// <summary>
        /// Determines whether the <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/> contains a specific 
        /// value.</summary>
        /// <param name="item">
        /// The object to locate in the <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>;
        /// otherwise, false.
        /// </returns>
        public bool Contains(IStatement item)
        {
            /* *
             * Can't possibly contain it if the container is null.
             * */
            if (this.statementContainer == null)
                return false;
            return this.statementContainer.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/> to an
        /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
        /// index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
        /// elements copied from <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>. The 
        /// <see cref="System.Array"/> must
        /// have zero-based indexing.</param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="array"/> is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> 
        /// is equal to or greater than the length of <paramref name="array"/>.-or-The 
        /// number of elements in the source <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/> is greater 
        /// than the available space from <paramref name="arrayIndex"/> to the 
        /// end of the destination <paramref name="array"/>.</exception>
        public void CopyTo(IStatement[] array, int arrayIndex = 0)
        {
            /* *
             * If the container is null, there are no elements to copy.
             * */
            if (this.statementContainer == null)
                return;

            this.statementContainer.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns the element at the index provided
        /// </summary>
        /// <param name="index">The index of the element to get.</param>
        /// <returns>The instance of <see cref="IStatement"/> at the index provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is  beyond the range of the 
        /// <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>.
        /// </exception>
        public IStatement this[int index]
        {
            get
            {
                /* *
                 * Self-securing, if there's no container instance, 
                 * the count will be zero; thus, no index is valid.
                 * */
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.statementContainer[index];
            }
        }

        /// <summary>
        /// Translates the <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/> into a flat <see cref="System.Array"/>
        /// of <see cref="IStatement"/> elements.
        /// </summary>
        /// <returns>A new <see cref="System.Array"/> of <see cref="IStatement"/> instances.</returns>
        public IStatement[] ToArray()
        {
            /* *
             * No need to construct the container to return an
             * empty array.
             * */
            if (this.statementContainer == null)
                return new IStatement[0];
            return this.statementContainer.ToArray();
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> ordinal index of the 
        /// <paramref name="element"/> provided.
        /// </summary>
        /// <param name="element">The <see cref="IStatement"/>
        /// instance to find within the <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>.</param>
        /// <returns>-1 if the <paramref name="element"/> was not found within
        /// the <see cref="IntermediateCoercionMemberBase{TCoercion, TIntermediateCoercion, TType, TIntermediateType}"/>; a positive <see cref="Int32"/>
        /// value indicating the ordinal index of <paramref name="element"/>
        /// otherwise.</returns>
        public int IndexOf(IStatement element)
        {
            /* *
             * Can't return the index of a statement that can't 
             * exist within a null statement container.
             * */
            if (this.statementContainer == null)
                return -1;
            return this.statementContainer.IndexOf(element);
        }

        #endregion

        #region IEnumerable<IStatement> Members

        public IEnumerator<IStatement> GetEnumerator()
        {
            this.CheckStatementContainer();
            return this.statementContainer.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #endregion

        #region IIntermediateTypeParent Members

        public IScopeCoercionCollection ScopeCoercions
        {
            get
            {
                return this.StatementContainer.ScopeCoercions;
            }
        }

        public IIntermediateClassTypeDictionary Classes
        {
            get
            {
                return this.StatementContainer.Classes;
            }
        }

        public IIntermediateDelegateTypeDictionary Delegates
        {
            get
            {
                return this.StatementContainer.Delegates;
            }
        }

        public IIntermediateEnumTypeDictionary Enums
        {
            get
            {
                return this.StatementContainer.Enums;
            }
        }

        public IIntermediateInterfaceTypeDictionary Interfaces
        {
            get
            {
                return this.StatementContainer.Interfaces;
            }
        }

        public IIntermediateStructTypeDictionary Structs
        {
            get
            {
                return this.StatementContainer.Structs;
            }
        }

        public IIntermediateFullTypeDictionary Types
        {
            get
            {
                return this.StatementContainer.Types;
            }
        }

        public abstract IIntermediateAssembly Assembly { get; }

        #endregion

        #region ITypeParent Members

        public IEnumerable<string> AggregateIdentifiers
        {
            get { return this.StatementContainer.AggregateIdentifiers; }
        }

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
            get
            {
                return this.Assembly;
            }
        }

        #endregion

    }
}
