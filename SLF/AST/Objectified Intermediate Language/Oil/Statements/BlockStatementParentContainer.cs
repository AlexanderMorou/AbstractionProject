using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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

        public IReturnStatement Return()
        {
            ReturnStatement r = new ReturnStatement(this.Owner);
            this.baseCollection.Add(r);
            return r;
        }

        public IReturnStatement Return(IExpression value)
        {
            ReturnStatement r = new ReturnStatement(this.Owner, value);
            this.baseCollection.Add(r);
            return r;
        }

        public IConditionBlockStatement If(IExpression condition)
        {
            return OnIf(condition);
        }

        internal virtual IConditionBlockStatement OnIf(IExpression condition)
        {
            var result = new ConditionBlockStatement(this.Owner)
            {
                Condition = condition
            };
            this.baseCollection.Add(result);
            return result;
        }

        #region Call Method insertion

        public ICallFusionStatement Call(IExpressionToCommaFusionExpression target)
        {
            var result = new CallFusionStatement(this.Owner) { Target = target };
            this.baseCollection.Add(result);
            return result;
        }

        public ICallMethodStatement Call(IMethodInvokeExpression target)
        {
            var result = new CallMethodStatement(this.Owner) { Target = target };
            this.baseCollection.Add(result);
            return result;
        }

        public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, params IExpression[] parameters)
        {
            return this.Call(new MethodInvokeExpression(ptr, parameters.ToCollection()));
        }

        public ICallMethodStatement Call(IMethodPointerReferenceExpression ptr, IExpressionCollection parameters)
        {
            return this.Call(new MethodInvokeExpression(ptr, parameters));
        }

        public ICallMethodStatement Call(IMethodReferenceStub stub, params IExpression[] parameters)
        {
            return this.Call(new MethodInvokeExpression(stub, parameters));
        }

        public ICallMethodStatement Call(IMethodReferenceStub stub, IExpressionCollection parameters)
        {
            return this.Call(stub.Invoke(parameters));
        }

        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, params IExpression[] parameters)
        {
            return this.Call(parent.Call(methodName, parameters));
        }

        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, IExpressionCollection parameters)
        {
            return this.Call(parent.Call(methodName, parameters));
        }

        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
        {
            return this.Call(parent.GetMethod(methodName, typeParameters).Invoke(parameters));
        }

        public ICallMethodStatement Call(IMemberParentReferenceExpression parent, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
        {
            return this.Call(parent.GetMethod(methodName, typeParameters).Invoke(parameters));
        }

        public ICallMethodStatement Call(string methodName, params IExpression[] parameters)
        {
            return this.Call(new MethodReferenceStub(methodName).Invoke(parameters));
        }

        public ICallMethodStatement Call(string methodName, IExpressionCollection parameters)
        {
            return this.Call(new MethodReferenceStub(methodName).Invoke(parameters));
        }

        public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
        {
            return this.Call(new MethodReferenceStub(methodName, typeParameters).Invoke(parameters));
        }

        public ICallMethodStatement Call(string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
        {
            return this.Call(new MethodReferenceStub(methodName, typeParameters).Invoke(parameters));
        }

        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, params IExpression[] parameters)
        {
            return this.Call(new MethodReferenceStub(methodName, callType).Invoke(parameters));
        }

        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, IExpressionCollection parameters)
        {
            return this.Call(new MethodReferenceStub(methodName, callType).Invoke(parameters));
        }

        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, params IExpression[] parameters)
        {
            return this.Call(new MethodReferenceStub(methodName, typeParameters, callType).Invoke(parameters));
        }

        public ICallMethodStatement Call(MethodReferenceType callType, string methodName, ITypeCollection typeParameters, IExpressionCollection parameters)
        {
            return this.Call(new MethodReferenceStub(methodName, typeParameters, callType).Invoke(parameters));
        }

        public ISwitchStatement Switch(IExpression caseCondition)
        {
            var result = new SwitchStatement(this.Owner);
            base.baseCollection.Add(result);
            return result;
        }
        #endregion

        #endregion

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
                    else if (current is IIntermediateMethodMember)
                    {
                        var currentSig = current as IIntermediateMethodMember;
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

    }
}
