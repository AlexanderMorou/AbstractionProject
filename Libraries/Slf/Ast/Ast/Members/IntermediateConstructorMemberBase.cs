﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides an implementation of a constructor member.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate type system.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableParent{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract partial class IntermediateConstructorMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableParent<TCtor, TType>
        where TIntermediateType :
            IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TType
    {
        private ICallParameterSet cascadeMembers;
        /// <summary>
        /// Creates a new <see cref="IntermediateConstructorMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/> provdied.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/>
        /// which the <see cref="IntermediateConstructorMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// belongs to.</param>
        /// <paramnparam name="identityManager">The <see cref="ITypeIdentityManager"/> which is responsible for marshalling
        /// type identities in the current type model.</paramnparam>
        public IntermediateConstructorMemberBase(TIntermediateType parent)
            : base(parent, parent.Assembly)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateConstructorMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/> and <paramref name="typeInitializer"/> provdied.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/>
        /// which the <see cref="IntermediateConstructorMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// belongs to.</param>
        /// <param name="typeInitializer">Whether the <see cref="IntermediateConstructorMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/> 
        /// is a type initializer</param>
        internal IntermediateConstructorMemberBase(TIntermediateType parent, bool typeInitializer)
            : base(parent, parent.Assembly, typeInitializer)
        {
        }

        #region IIntermediateConstructorMember Members

        public ConstructorCascadeTarget CascadeTarget { get; set; }

        public ICallParameterSet CascadeMembers
        {
            get
            {
                if (this.cascadeMembers == null)
                    this.cascadeMembers = new CallParameterSet();
                return this.cascadeMembers;
            }
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

        public IEnumerable<IType> GetTypes()
        {
            if (this.statementContainer == null)
                return new IType[0];
            return this.statementContainer.GetTypes();
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
        /// <param name="localDeclaration">A <see cref="ILocalDeclarationsStatement"/>
        /// which defines the local used within the scope of the iteration block.</param>
        /// <param name="condition">The <see cref="Boolean"/> <see cref="IExpression"/>
        /// which denotes the condition to evaluate prior to executing the iteration's block body.</param>
        /// <param name="iterations">The series of <see cref="IStatementExpression"/>
        /// values which denote the logic to execute after the code-block, 
        /// but prior to checking the continuation <paramref name="condition"/>.
        /// </param>
        /// <returns>A new <see cref="IIterationDeclarationBlockStatement"/>
        /// which represents the operation..</returns>
        public IIterationDeclarationBlockStatement Iterate(ILocalDeclarationsStatement localDeclaration, IExpression condition, IEnumerable<IStatementExpression> iterations)
        {
            return this.StatementContainer.Iterate(localDeclaration, condition, iterations);
        }

        public ISimpleIterationBlockStatement Iterate(ILocalDeclarationsStatement target, IExpression start, IExpression end, bool endExclusive = true, IExpression incremental = null)
        {
            return this.StatementContainer.Iterate(target, start, end, endExclusive, incremental);
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
            return this.StatementContainer.Enumerate(targetName, source);
        }

        /// <summary>
        /// Creates an inserts a <see cref="ILocalDeclarationsStatement"/> with the
        /// <paramref name="local"/> provided.
        /// </summary>
        /// <param name="local">The <see cref="ILocalMember"/> to declare.</param>
        /// <returns>A new <see cref="ILocalDeclarationsStatement"/>
        /// which aims to declare the <paramref name="local"/> provided.</returns>
        public ILocalDeclarationsStatement DefineLocal(ILocalMember local)
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
        /// with the given assignment <paramref name="operation"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IExpressionFusionExpression"/> which denotes
        /// a fusion between two expressions that potentially references
        /// a assignable member.</param>
        /// <param name="operation">The <see cref="AssignmentOperation"/> which denotes how the operation
        /// should be managed.</param>
        /// <param name="value">The <see cref="INaryOperandExpression"/> which denotes the value
        /// to assign to the <paramref name="target"/>.</param>
        /// <returns>A new <see cref="IExpressionStatement"/>
        /// which denotes the operation</returns>
        public IExpressionStatement Assign(IExpressionFusionExpression target, AssignmentOperation operation, INaryOperandExpression value)
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
        /// Assigns a <paramref name="value"/> to the <paramref name="target"/>
        /// with the standard <see cref="AssignmentOperation.SimpleAssign"/> operation.
        /// </summary>
        /// <param name="target">The <see cref="IExpressionFusionExpression"/> which is the target of the
        /// assignment.</param>
        /// <param name="value">The <see cref="INaryOperandExpression"/> which denotes the value
        /// to assign to the <paramref name="target"/>.</param>
        /// <returns>A new <see cref="IExpressionStatement"/> which represents the assignment.</returns>
        public IExpressionStatement Assign(IExpressionFusionExpression target, INaryOperandExpression value)
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

        #region Event Handling
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
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                class, 
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return this.StatementContainer.AddHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(@event, method);
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
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                class, 
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return this.StatementContainer.AddHandler(targetEvent, methodPtr);
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
            return this.StatementContainer.AddHandler(target, sourceMethod);
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
            return this.StatementContainer.AddHandler(target, eventName, sourceMethod);
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
            return this.StatementContainer.AddHandler(target, eventName, methodName);
        }

        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> ChangeHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> @event, EventHandlerChangeKind changeKind, TSignature method)
            where TEvent :
                class, 
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                class, 
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return this.StatementContainer.ChangeHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(@event, changeKind, method);
        }

        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> ChangeHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> methodPtr)
            where TEvent :
                class, 
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                class, 
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return this.StatementContainer.ChangeHandler(targetEvent, changeKind, methodPtr);
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
            return this.StatementContainer.ChangeHandler(target, changeKind, sourceMethod);
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
            return this.StatementContainer.ChangeHandler(target, eventName, changeKind, sourceMethod);
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
            return this.StatementContainer.ChangeHandler(target, eventName, changeKind, methodName);
        }

        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> RemoveHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> @event, TSignature method)
            where TEvent :
                class, 
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                class, 
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return this.StatementContainer.RemoveHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(@event, method);
        }

        public IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> RemoveHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent, IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> methodPtr)
            where TEvent :
                class, 
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                class, 
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return this.StatementContainer.RemoveHandler(targetEvent, methodPtr);
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
            return this.StatementContainer.RemoveHandler(target, sourceMethod);
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
            return this.StatementContainer.RemoveHandler(target, eventName, sourceMethod);
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
            return this.StatementContainer.RemoveHandler(target, eventName, methodName);
        }

        #endregion

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


        #region IControlledCollection<IStatement> Members

        /// <summary>
        /// Gets the number of elements contained in the <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/>.</summary>
        /// <returns>
        /// The number of elements contained in the <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/>.</returns>
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
        /// Determines whether the <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/> contains a specific 
        /// value.</summary>
        /// <param name="item">
        /// The object to locate in the <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/>;
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
        /// Copies the elements of the <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/> to an
        /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
        /// index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
        /// elements copied from <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/>. The 
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
        /// number of elements in the source <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/> is greater 
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
        /// <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/>.
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
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                return this.statementContainer[index];
            }
        }

        /// <summary>
        /// Translates the <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/> into a flat <see cref="System.Array"/>
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
        /// instance to find within the <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/>.</param>
        /// <returns>-1 if the <paramref name="element"/> was not found within
        /// the <see cref="IntermediateCoercionMemberBase{TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent}"/>; a positive <see cref="Int32"/>
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

        public new abstract IIntermediateAssembly Assembly { get; }

        #endregion

        #region ITypeParent Members

        public IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
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


        public override void Accept(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public void Add(IStatement statement)
        {
            this.StatementContainer.Add(statement);
        }

        public bool AddAfter(IStatement locator, IStatement toAdd)
        {
            return this.StatementContainer.AddAfter(locator, toAdd);
        }

        public bool AddBefore(IStatement locator, IStatement toAdd)
        {
            return this.StatementContainer.AddBefore(locator, toAdd);
        }

        public IWhileStatement While(IExpression condition)
        {
            return this.StatementContainer.While(condition);
        }

        public bool Remove(IStatement target)
        {
            return this.StatementContainer.Remove(target);
        }

        public bool HasScopeCoercions
        {
            get
            {
                return this.statementContainer != null && this.StatementContainer.HasScopeCoercions;
            }
        }

        public bool HasClasses
        {
            get
            {
                return this.statementContainer != null && this.StatementContainer.HasClasses;
            }
        }

        public bool HasDelegates
        {
            get
            {
                return this.statementContainer != null && this.StatementContainer.HasDelegates;
            }
        }

        public bool HasEnums
        {
            get
            {
                return this.statementContainer != null && this.StatementContainer.HasEnums;
            }
        }

        public bool HasInterfaces
        {
            get
            {
                return this.statementContainer != null && this.StatementContainer.HasInterfaces;
            }
        }

        public bool HasStructs
        {
            get
            {
                return this.statementContainer != null && this.StatementContainer.HasStructs;
            }
        }

        public bool HasTypes
        {
            get
            {
                return this.statementContainer != null && this.StatementContainer.HasTypes;
            }
        }

        public bool HasLabels
        {
            get { return this.statementContainer != null && this.StatementContainer.HasLabels; }
        }

        public bool HasLocals
        {
            get { return this.statementContainer != null && this.StatementContainer.HasLocals; }
        }

    }
}
