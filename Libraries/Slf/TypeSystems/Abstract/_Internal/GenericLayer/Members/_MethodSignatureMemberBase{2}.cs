using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract partial class _MethodSignatureMemberBase<TSignature, TSignatureParent> :
        _MethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>,
        IMethodSignatureMember<TSignature, TSignatureParent>,
        _IGenericMethodSignatureRegistrar,
        _IGenericParamParent
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {
        private object syncObject = new object();
        /// <summary>
        /// Data member for the generic parameters cache.
        /// </summary>
        private GenericMethodSignatureCache<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent> genericCache;

        internal _MethodSignatureMemberBase(TSignatureParent parent, TSignature original)
            : base(parent, original)
        {

        }

        internal _MethodSignatureMemberBase(TSignature original, IControlledTypeCollection genericReplacements)
            : base(original, genericReplacements)
        {
        }

        protected override IParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>> InitializeParameters()
        {
            return new _Parameters(this, this.Original.Parameters);
        }

        #region _IGenericMethodSignatureRegistrar Members

        public void RegisterGenericMethod(IMethodSignatureMember targetSignature, IControlledTypeCollection typeParameters)
        {
            this.CheckGenericCache();
            this.genericCache.RegisterGenericMethod(targetSignature, typeParameters);
        }

        public void UnregisterGenericMethod(IControlledTypeCollection typeParameters)
        {
            this.CheckGenericCache();
            this.genericCache.UnregisterGenericMethod(typeParameters);
        }

        private void CheckGenericCache()
        {
            lock (this.syncObject)
                if (this.genericCache == null)
                    this.genericCache = new GenericMethodSignatureCache<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>();
        }

        #endregion

        public override sealed TSignature MakeGenericClosure(IControlledTypeCollection genericReplacements)
        {
            if (!this.IsGenericDefinition)
                throw new InvalidOperationException();
            TSignature result = default(TSignature);
            if (this.genericCache.ContainsGenericMethod(genericReplacements, ref result))
                return result;
            /* *
             * _IGenericMethodSignatureRegistrar handles cache.
             * */
            return this.OnMakeGenericMethod(genericReplacements);
        }

        protected abstract TSignature OnMakeGenericMethod(IControlledTypeCollection genericReplacements);

        public override void Dispose()
        {
            try
            {
                lock (this.syncObject)
                    if (this.genericCache != null)
                    {
                        this.genericCache.Dispose();
                        this.genericCache = null;
                    }
            }
            finally
            {
                base.Dispose();
            }
        }

        #region _IGenericParamParent Members

        public void PositionalShift(int from, int to)
        {
            if (this.IsGenericConstruct && !this.IsGenericDefinition)
            {
                if (from < 0 || from >= this.GenericReplacementsImpl.Count)
                    throw new ArgumentOutOfRangeException("from");
                if (to < 0 || to >= this.GenericReplacementsImpl.Count)
                    throw new ArgumentOutOfRangeException("to");
                lock (this.syncObject)
                {
                    var items = this.GenericReplacementsImpl.ToArray();
                    bool backwards = from > to;
                    var item = items[from];
                    if (backwards)
                        for (int i = from; i > to; i--)
                            items[i] = items[i - 1];
                    else
                        for (int i = from; i < to; i++)
                            items[i] = items[i + 1];

                    items[to] = item;
                    this.GenericReplacementsImpl = new LockedTypeCollection(items);
                }
            }
            this.CheckGenericCache();
            this.genericCache.PositionalShift(from, to);
        }
        #endregion

        #region _IGenericMethodSignatureRegistrar Members

        public void RegisterGenericChild(IMethodSignatureParent parent, IMethodSignatureMember genericChild)
        {
            throw new NotSupportedException();
        }

        public void UnregisterGenericChild(IMethodSignatureParent parent)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
