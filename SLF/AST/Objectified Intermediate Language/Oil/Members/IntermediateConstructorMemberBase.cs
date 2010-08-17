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
    /// <summary>
    /// Provides an implementation of a constructor member.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate type system.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableType{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableType{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public partial class IntermediateConstructorMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableType<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
        bool naming = false;
        /// <summary>
        /// Creates a new <see cref="IntermediateConstructorMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/> provdied.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/>
        /// which the <see cref="IntermediateConstructorMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// belongs to.</param>
        public IntermediateConstructorMemberBase(TIntermediateType parent)
            : base(parent)
        {
            naming = true;
            this.Name = ".ctor";
            naming = false;
        }

        internal IntermediateConstructorMemberBase(TIntermediateType parent, bool typeInitializer)
            : this(parent)
        {
            if (typeInitializer)
            {
                naming = true;
                this.Name = ".cctor";
                naming = false;
            }
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
    }
}
