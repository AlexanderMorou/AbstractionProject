using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
        #region Nested Type Data Members
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
        #endregion
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

        public IReturnStatement Return()
        {
            return this.StatementContainer.Return();
        }

        public IReturnStatement Return(IExpression value)
        {
            return this.StatementContainer.Return(value);
        }

        public IConditionBlockStatement If(IExpression condition)
        {
            return this.StatementContainer.If(condition);
        }

        public ISwitchStatement Switch(IExpression caseCondition)
        {
            return this.StatementContainer.Switch(caseCondition);
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

        #region IIntermediateTypeParent Members

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

        public abstract IIntermediateAssembly Assembly { get; }

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
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone class type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// class type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
        protected virtual IntermediateClassTypeDictionary InitializeClasses()
        {
            return new IntermediateClassTypeDictionary(this, this._Types);
        }

        /// <summary>
        /// Initializes the <see cref="Delegates"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateDelegateTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone delegate type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// delegate type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
        protected virtual IntermediateDelegateTypeDictionary InitializeDelegates()
        {
            return new IntermediateDelegateTypeDictionary(this, this._Types);
        }

        /// <summary>
        /// Initializes the <see cref="Enums"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateEnumTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone enum type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// enum type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
        protected virtual IntermediateEnumTypeDictionary InitializeEnums()
        {
            return new IntermediateEnumTypeDictionary(this, this._Types);
        }

        /// <summary>
        /// Initializes the <see cref="Interfaces"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateInterfaceTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone interface type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// interface type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
        protected virtual IntermediateInterfaceTypeDictionary InitializeInterfaces()
        {
            return new IntermediateInterfaceTypeDictionary(this, this._Types);
        }

        /// <summary>
        /// Initializes the <see cref="Structs"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateStructTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone struct type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// struct type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
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
