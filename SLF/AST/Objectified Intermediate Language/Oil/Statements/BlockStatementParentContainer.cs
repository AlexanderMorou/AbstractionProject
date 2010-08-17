using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Abstract;
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
        IBlockStatementParent
    {
        private IBlockStatementParent owner;

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


    }
}
