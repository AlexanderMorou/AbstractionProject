﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with the 
    /// parent of an <see cref="IBlockStatement"/>.
    /// </summary>
    public interface IBlockStatementParent :
        IControlledCollection<IStatement>,
        IStatementParent,
        IIntermediateTypeParent,
        IIntermediateMemberParent
    {
        void Add(IStatement statement);
        bool AddAfter(IStatement locator, IStatement toAdd);
        bool AddBefore(IStatement locator, IStatement toAdd);
        bool Remove(IStatement target);

        /// <summary>
        /// Inserts and returns a new <see cref="IReturnStatement"/>
        /// with no value as its result.
        /// </summary>
        /// <returns>A new <see cref="IReturnStatement"/>
        /// with no value as its result.</returns>
        IReturnStatement Return();

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
        IReturnStatement Return(IExpression value);

        /// <summary>
        /// Inserts and returns a new <see cref="IConditionBlockStatement"/> instance
        /// which relates to the <paramref name="condition"/> provided.
        /// </summary>
        /// <param name="condition">The <see cref="IExpression"/> to evaluate
        /// before executing the <see cref="IConditionBlockStatement"/>'s statements.</param>
        /// <returns>A new <see cref="IConditionBlockStatement"/> with the
        /// <see cref="IExpression"/> <paramref name="condition"/> provided.</returns>
        IConditionBlockStatement If(IExpression condition);

        /// <summary>
        /// Inserts and returns a new <see cref="IWhileStatement"/> instance which relates
        /// to the <paramref name="condition"/> provided.
        /// </summary>
        /// <param name="condition">
        /// The <see cref="IExpression"/> which must evaluate to true for the block of the
        /// <see cref="IWhileStatement"/> to be evaluated.
        /// </param>
        /// <returns>A new <see cref="IWhileStatement"/> instance which relates
        /// to the <paramref name="condition"/> provided.</returns>
        IWhileStatement While(IExpression condition);

        /// <summary>
        /// Inserts and returns a new <see cref="ISwitchStatement"/> instance
        /// which relates to the <paramref name="caseCondition"/> provided.
        /// </summary>
        /// <param name="caseCondition">A <see cref="IExpression"/> instance which
        /// represents a value to check on each case of the <see cref="ISwitchStatement"/>
        /// that results.</param>
        /// <returns>A new <see cref="ISwitchStatement"/> with no cases relative to the
        /// <paramref name="caseCondition"/> provided.</returns>
        ISwitchStatement Switch(IExpression caseCondition);

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
        ICallFusionStatement Call(IExpressionToCommaFusionExpression target);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMethodInvokeExpression"/> to reference
        /// for the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/>
        /// is null.</exception>
        ICallMethodStatement Call(IMethodInvokeExpression target);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="ptr"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="ptr">The <see cref="IMethodPointerReferenceExpression"/> that identifies
        /// the pointer to the method to call.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, params IExpression[] parameters);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="ptr"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="ptr">The <see cref="IMethodPointerReferenceExpression"/>
        /// in which to reference for the new <see cref="ICallMethodStatement"/>.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, IExpressionCollection parameters);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="stub"/>, and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="stub">The <see cref="IMethodReferenceStub"/> which
        /// specifies the origin of the call, its name, and calling convention.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        ICallMethodStatement Call(IMethodReferenceStub stub, params IExpression[] parameters);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="stub"/>, and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="stub">The <see cref="IMethodReferenceStub"/> which
        /// specifies the origin of the call, its name, and calling convention.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        ICallMethodStatement Call(IMethodReferenceStub stub, IExpressionCollection parameters);
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
        ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, params IExpression[] parameters);
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
        ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, IExpressionCollection parameters);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="parent"/>, <paramref name="methodName"/>, <oaranref name="typeParameters"/>
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
        ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, params IExpression[] parameters);
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
        ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="methodName"/> and <paramref name="parameters"/>.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The array of <see cref="IExpression"/> values
        /// which represents the parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        ICallMethodStatement Call(string methodName, params IExpression[] parameters);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ICallMethodStatement"/> with the
        /// <paramref name="methodName"/> and <paramref name="parameters"/>.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/> to use for the
        /// parameters of the call.</param>
        /// <returns>A new <see cref="ICallMethodStatement"/>.</returns>
        ICallMethodStatement Call(string methodName, IExpressionCollection parameters);
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
        ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, params IExpression[] parameters);
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
        ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, IExpressionCollection parameters);
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
        ICallMethodStatement Call(MethodReferenceType callType, string methodName, params IExpression[] parameters);
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
        ICallMethodStatement Call(MethodReferenceType callType, string methodName, IExpressionCollection parameters);
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
        ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, params IExpression[] parameters);
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
        ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters);
        #endregion

        /// <summary>Returns whether or not the <see cref="Locals"/> contains any information.</summary>
        bool HasLocals { get; }

        /// <summary>
        /// Returns the <see cref="ILocalMemberDictionary"/> associated to the current
        /// <see cref="IBlockStatementParent"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasLocals")]
#endif
        ILocalMemberDictionary Locals { get; }

        /// <summary>Returns whether or not the <see cref="Labels"/> contains any information.</summary>
        bool HasLabels { get; }
        /// <summary>
        /// Returns the <see cref="IBlockStatementLabelDictionary"/> of the labels associated to the current
        /// <see cref="IBlockStatementParent"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasLabels")]
#endif
        IBlockStatementLabelDictionary Labels { get; }
        /// <summary>
        /// Returns the <see cref="IBlockStatementLabelDictionary"/> which
        /// is a unioned dictionary of those contained within the current
        /// <see cref="IBlockStatementParent"/> and those above.
        /// </summary>
#if DEBUG
        [VisitorImplementationIgnoreProperty]
#endif
        IBlockStatementLabelDictionary ScopeLabels { get; }

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
        IIterationBlockStatement Iterate(IEnumerable<IStatementExpression> initializers, IExpression condition, IEnumerable<IStatementExpression> iterations);

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
        /// which represents the operation.</returns>
        IIterationDeclarationBlockStatement Iterate(ILocalDeclarationsStatement localDeclaration, IExpression condition, IEnumerable<IStatementExpression> iterations);

        /// <summary>
        /// Creates, inserts and returns a new <see cref="ISimpleIterationBlockStatement"/>
        /// which defines a local through the <paramref name="target"/>,
        /// and moves from <paramref name="start"/> to <paramref name="end"/>,
        /// where perhaps the <paramref name="endExclusive"/>, using the optional
        /// <paramref name="incremental"/>.
        /// </summary>
        /// <param name="target">The <see cref="ILocalDeclarationsStatement"/> which 
        /// defines the local to use for the duration of the block.</param>
        /// <param name="start">The <see cref="IExpression"/> that denotes the starting
        /// position of the loop.</param>
        /// <param name="end">The <see cref="IExpression"/> that denotes the ending position
        /// of the loop.</param>
        /// <param name="endExclusive">Whether the <paramref name="end"/>
        /// is exclusive (not included) within the iteration of the loop.</param>
        /// <param name="incremental">The optional <see cref="IExpression"/>
        /// which denotes the value to increment/decrement the <paramref name="target"/> per iteration
        /// between <paramref name="start"/> and <paramref name="end"/>.</param>
        /// <returns>A new <see cref="ISimpleIterationBlockStatement"/> which
        /// represents the operation.</returns>
        ISimpleIterationBlockStatement Iterate(ILocalDeclarationsStatement target, IExpression start, IExpression end, bool endExclusive = true, IExpression incremental = null);

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
        IEnumerateSetBreakableBlockStatement Enumerate(string targetName, IExpression source);

        /// <summary>
        /// Creates an inserts a <see cref="ILocalDeclarationsStatement"/> with the
        /// <paramref name="local"/> provided.
        /// </summary>
        /// <param name="local">The <see cref="ILocalMember"/> to declare.</param>
        /// <returns>A new <see cref="ILocalDeclarationsStatement"/>
        /// which aims to declare the <paramref name="local"/> provided.</returns>
        ILocalDeclarationsStatement DefineLocal(ILocalMember local);
        /// <summary>
        /// Defines a <see cref="ILabelStatement"/> with the 
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// value associated to the label to create.</param>
        /// <returns>A new <see cref="ILabelStatement"/> relative to the 
        /// <paramref name="name"/> provided.</returns>
        ILabelStatement DefineLabel(string name);
        /// <summary>
        /// Defines the provided <paramref name="label"/>.
        /// </summary>
        /// <param name="label">The <see cref="ILabelStatement"/>
        /// to declare</param>
        void DefineLabel(ILabelStatement label);

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
        IExpressionStatement Assign(IMemberReferenceExpression target, AssignmentOperation operation, INaryOperandExpression value);

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
        IExpressionStatement Assign(IExpressionFusionExpression target, AssignmentOperation operation, INaryOperandExpression value);
        /// <summary>
        /// Assigns a <paramref name="value"/> to the <paramref name="target"/>
        /// with the standard <see cref="AssignmentOperation.SimpleAssign"/> operation.
        /// </summary>
        /// <param name="target">The <see cref="IMemberReferenceExpression"/> which is the target of the
        /// assignment.</param>
        /// <param name="value">The <see cref="INaryOperandExpression"/> which denotes the value
        /// to assign to the <paramref name="target"/>.</param>
        /// <returns>A new <see cref="IExpressionStatement"/> which represents the assignment.</returns>
        IExpressionStatement Assign(IMemberReferenceExpression target, INaryOperandExpression value);
        /// <summary>
        /// Assigns a <paramref name="value"/> to the <paramref name="target"/>
        /// with the standard <see cref="AssignmentOperation.SimpleAssign"/> operation.
        /// </summary>
        /// <param name="target">The <see cref="IExpressionFusionExpression"/> which is the target of the
        /// assignment.</param>
        /// <param name="value">The <see cref="INaryOperandExpression"/> which denotes the value
        /// to assign to the <paramref name="target"/>.</param>
        /// <returns>A new <see cref="IExpressionStatement"/> which represents the assignment.</returns>
        IExpressionStatement Assign(IExpressionFusionExpression target, INaryOperandExpression value);

        /// <summary>
        /// Increments the <paramref name="target"/> variable by one.
        /// </summary>
        /// <param name="target">The <see cref="IAssignTargetExpression"/>
        /// which can be incremented.</param>
        /// <returns>A <see cref="IExpressionStatement"/> which represents the 
        /// increment operation.</returns>
        IExpressionStatement Increment(IAssignTargetExpression target);
        /// <summary>
        /// Increments the <paramref name="target"/> by the <paramref name="incrementBy"/> value.
        /// </summary>
        /// <param name="target">The <see cref="IAssignTargetExpression"/> 
        /// which can be incremented.</param>
        /// <param name="incrementBy">The <see cref="INaryOperandExpression"/> which acts 
        /// as the operand in the increment operation.</param>
        /// <returns>A <see cref="IExpressionStatement"/> which represents the
        /// increment operation.</returns>
        IExpressionStatement Increment(IAssignTargetExpression target, INaryOperandExpression incrementBy);
        /// <summary>
        /// Decrements the <paramref name="target"/> variable by one.
        /// </summary>
        /// <param name="target">The <see cref="IAssignTargetExpression"/>
        /// which can be decremented.</param>
        /// <returns>A <see cref="IExpressionStatement"/> which represents the 
        /// decrement operation.</returns>
        IExpressionStatement Decrement(IAssignTargetExpression target);
        /// <summary>
        /// Decrements the <paramref name="target"/> by the <paramref name="decrementBy"/> value.
        /// </summary>
        /// <param name="target">The <see cref="IAssignTargetExpression"/> 
        /// which can be decremented.</param>
        /// <param name="decrementBy">The <see cref="INaryOperandExpression"/> which acts 
        /// as the operand in the decrement operation.</param>
        /// <returns>A <see cref="IExpressionStatement"/> which represents the
        /// decrement operation.</returns>
        IExpressionStatement Decrement(IAssignTargetExpression target, INaryOperandExpression decrementBy);

        IJumpStatement Jump(IJumpTarget target);
        /// <summary>
        /// Creates, inserts and returns a <see cref="IGoToStatement"/>
        /// which encodes the intent to go to the <paramref name="target"/> 
        /// provided.
        /// </summary>
        /// <param name="target">The <see cref="ILabelStatement"/>
        /// which needs jumped to.</param>
        /// <returns>The <see cref="IGoToStatement"/>
        /// which </returns>
        IGoToStatement GoTo(ILabelStatement target);

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
        /// <param name="event">The <see cref="IEventReferenceExpression<TEvent, TEventParameter, TEventParent>"/>
        /// which references the event in question.</param>
        /// <param name="method">The <typeparamref name="TSignature"/>
        /// which denotes the method to add to the event.</param>
        /// <returns>A <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="event"/>, or <paramref name="method"/>
        /// is null.</exception>
        IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> AddHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> @event, TSignature method)
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
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;

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
        IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> AddHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent, IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> methodPtr)
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
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;

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
        IChangeEventHandlerStatement AddHandler(IEventReferenceExpression target, IMethodPointerReferenceExpression sourceMethod);
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
        IChangeEventHandlerStatement AddHandler(IMemberParentReferenceExpression target, string eventName, IMethodPointerReferenceExpression sourceMethod);
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
        IChangeEventHandlerStatement AddHandler(IMemberParentReferenceExpression target, string eventName, string methodName);

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
        /// <param name="event">The <see cref="IEventReferenceExpression{TEvent, TEventParameter, TEventParent}"/>
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
        IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> ChangeHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> @event, EventHandlerChangeKind changeKind, TSignature method)
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
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;

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
        IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> ChangeHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> methodPtr)
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
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;

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
        IChangeEventHandlerStatement ChangeHandler(IEventReferenceExpression target, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression sourceMethod);
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
        IChangeEventHandlerStatement ChangeHandler(IMemberParentReferenceExpression target, string eventName, EventHandlerChangeKind changeKind, IMethodPointerReferenceExpression sourceMethod);
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
        IChangeEventHandlerStatement ChangeHandler(IMemberParentReferenceExpression target, string eventName, EventHandlerChangeKind changeKind, string methodName);
        
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
        /// <param name="event">The <see cref="IEventReferenceExpression{TEvent, TEventParameter, TEventParent}"/>
        /// which references the event in question.</param>
        /// <param name="method">The <typeparamref name="TSignature"/>
        /// which denotes the method to remove from the event.</param>
        /// <returns>A <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/>
        /// which represents the operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="event"/>, or <paramref name="method"/>
        /// is null.</exception>
        IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> RemoveHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> @event, TSignature method)
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
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;

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
        IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> RemoveHandler<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> targetEvent, IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent> methodPtr)
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
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;

        /// <summary>
        /// Creates, inserts and returns a <see cref="IChangeEventHandlerStatement"/>
        /// using <see cref="EventHandlerChangeKind.Remove"/> to denote the kind of action on
        /// the event handler, with the the <paramref name="target"/>, and
        /// <paramref name="sourceMethod"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IEventReferenceExpression"/>
        /// which references the event in question.</param>
        /// <param name="sourceMethod">The <see cref="IMethodPointerReferenceExpression"/>
        /// which denotes the method to remove from the event.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="target"/> or <paramref name="sourceMethod"/> is
        /// null.</exception>
        IChangeEventHandlerStatement RemoveHandler(IEventReferenceExpression target, IMethodPointerReferenceExpression sourceMethod);
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
        IChangeEventHandlerStatement RemoveHandler(IMemberParentReferenceExpression target, string eventName, IMethodPointerReferenceExpression sourceMethod);
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
        IChangeEventHandlerStatement RemoveHandler(IMemberParentReferenceExpression target, string eventName, string methodName);

        /// <summary>
        /// Inserts a <paramref name="comment"/> into the series of statements.
        /// </summary>
        /// <param name="comment">The <see cref="String"/> value denoting the comment.</param>
        /// <returns>A new <see cref="ICommentStatement"/> which denotes the <paramref name="comment"/>
        /// provided.</returns>
        ICommentStatement Comment(string comment);
    }
}
