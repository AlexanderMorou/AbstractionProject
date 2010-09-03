using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf._Internal;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a partial implementation for an intermediate method member.
    /// </summary>
    /// <typeparam name="TMethod">The type of method in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMethod">The type of method in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TMethodParent">The type which owns the <typeparamref name="TMethod"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMethodParent">The type which owns the <typeparamref name="TIntermediateMethod"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract partial class IntermediateMethodMemberBase<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> :
        IntermediateMethodSignatureMemberBase<IMethodParameterMember<TMethod, TMethodParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
        IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
        where TMethod : 
            class,
            IMethodMember<TMethod, TMethodParent>
        where TIntermediateMethod :
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethod
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
        where TIntermediateMethodParent :
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethodParent
    {

        /// <summary>
        /// Creates a new <see cref="IntermediateMethodMemberBase{TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent}"/> with the
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateMethodParent"/> which owns the 
        /// <see cref="IntermediateMethodMemberBase{TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent}"/>.</param>
        public IntermediateMethodMemberBase(TIntermediateMethodParent parent)
            : base(parent)
        {

        }

        /// <summary>
        /// Creates a new <see cref="IntermediateMethodMemberBase{TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent}"/> with the
        /// <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique name of the 
        /// <see cref="IntermediateMethodMemberBase{TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateMethodParent"/> which owns the 
        /// <see cref="IntermediateMethodMemberBase{TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent}"/>.</param>
        public IntermediateMethodMemberBase(string name, TIntermediateMethodParent parent)
            : base(name, parent)
        {

        }

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

        public IReturnStatement Return()
        {
            return this.StatementContainer.Return();
        }

        public IReturnStatement Return(IExpression value)
        {
            return this.StatementContainer.Return(value);
        }

        public ISwitchStatement Switch(IExpression caseCondition)
        {
            return this.StatementContainer.Switch(caseCondition);
        }

        public IConditionBlockStatement If(IExpression condition)
        {
            return this.StatementContainer.If(condition);
        }

        public ICallFusionStatement Call(IExpressionToCommaFusionExpression target)
        {
            return this.StatementContainer.Call(target);
        }

        public ICallMethodStatement Call(IMethodInvokeExpression target)
        {
            return this.StatementContainer.Call(target);
        }

        public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, params IExpression[] parameters)
        {
            return this.StatementContainer.Call(ptr, parameters);
        }

        public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, IExpressionCollection parameters)
        {
            return this.StatementContainer.Call(ptr, parameters);
        }

        public ICallMethodStatement Call(IMethodReferenceStub stub, params IExpression[] parameters)
        {
            return this.StatementContainer.Call(stub, parameters);
        }

        public ICallMethodStatement Call(IMethodReferenceStub stub, IExpressionCollection parameters)
        {
            return this.StatementContainer.Call(stub, parameters);
        }

        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, params IExpression[] parameters)
        {
            return this.StatementContainer.Call(parent, methodName, parameters);
        }

        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, IExpressionCollection parameters)
        {
            return this.StatementContainer.Call(parent, methodName, parameters);
        }

        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
        {
            return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
        }

        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
        {
            return this.StatementContainer.Call(parent, methodName, typeParameters, parameters);
        }

        public ICallMethodStatement Call(string methodName, params IExpression[] parameters)
        {
            return this.StatementContainer.Call(methodName, parameters);
        }

        public ICallMethodStatement Call(string methodName, IExpressionCollection parameters)
        {
            return this.StatementContainer.Call(methodName, parameters);
        }

        public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
        {
            return this.StatementContainer.Call(methodName, typeParameters, parameters);
        }

        public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
        {
            return this.StatementContainer.Call(methodName, typeParameters, parameters);
        }

        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, params IExpression[] parameters)
        {
            return this.StatementContainer.Call(callType, methodName, parameters);
        }

        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, IExpressionCollection parameters)
        {
            return this.StatementContainer.Call(callType, methodName, parameters);
        }

        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
        {
            return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
        }

        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
        {
            return this.StatementContainer.Call(callType, methodName, typeParameters, parameters);
        }

        #endregion

        #region IControlledStateCollection<IStatement> Members

        public int Count
        {
            get
            {
                this.CheckStatementContainer();
                return this.statementContainer.Count;
            }
        }

        public bool Contains(IStatement item)
        {
            this.CheckStatementContainer();
            return this.statementContainer.Contains(item);
        }

        public void CopyTo(IStatement[] array, int arrayIndex)
        {
            this.CheckStatementContainer();
            this.statementContainer.CopyTo(array, arrayIndex);
        }

        public IStatement this[int index]
        {
            get
            {
                this.CheckStatementContainer();
                return this.statementContainer[index];
            }
        }

        public IStatement[] ToArray()
        {
            this.CheckStatementContainer();
            return this.statementContainer.ToArray();
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

        #region IIntermediateScopedDeclaration Members

        public virtual AccessLevelModifiers AccessLevel { get; set; }

        #endregion

        public override string UniqueIdentifier
        {
            get
            {
                return this.GetUniqueIdentifier();
            }
        }

        #region IIntermediateMethodMember Members
        IIntermediateMethodParent IIntermediateMethodMember.Parent
        {
            get
            {
                return base.Parent;
            }
        }
        #endregion

        #region IIntermediateTypeParent Members

        public IIntermediateClassTypeDictionary Classes
        {
            get
            {
                return this.statementContainer.Classes;
            }
        }

        public IIntermediateDelegateTypeDictionary Delegates
        {
            get
            {
                return this.statementContainer.Delegates;
            }
        }

        public IIntermediateEnumTypeDictionary Enums
        {
            get
            {
                return this.statementContainer.Enums;
            }
        }

        public IIntermediateInterfaceTypeDictionary Interfaces
        {
            get
            {
                return this.statementContainer.Interfaces;
            }
        }

        public IIntermediateStructTypeDictionary Structs
        {
            get
            {
                return this.statementContainer.Structs;
            }
        }

        public IIntermediateFullTypeDictionary Types
        {
            get
            {
                return this.statementContainer.Types;
            }
        }

        public abstract IIntermediateAssembly Assembly { get; }

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
            get
            {
                return this.Assembly;
            }
        }

        #endregion

    }
}
