using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract partial class _MethodMemberBase<TMethod, TMethodParent> :
        _MethodSignatureMemberBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodMember<TMethod, TMethodParent>,
        _IGenericMethodRegistrar,
        _IGenericParamParent
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        /// <summary>
        /// Data member for the generic parameters cache.
        /// </summary>
        //private Dictionary<IControlledTypeCollection, IMethodMember> genericCache = null;
        private GenericMethodCache<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent> genericCache;
        private object syncObject = new object();
        internal _MethodMemberBase(TMethodParent parent, TMethod original)
            : base(parent, original)
        {

        }
        internal _MethodMemberBase(TMethod original, IControlledTypeCollection genericParameters)
            : base(original, genericParameters)
        {

        }

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return this.Original.AccessLevel; }
        }

        #endregion

        protected override IParameterMemberDictionary<TMethod, IMethodParameterMember<TMethod, TMethodParent>> InitializeParameters()
        {
            return new _Parameters(this, this.Original.Parameters);
        }

        #region _IGenericMethodRegistrar Members

        public void RegisterGenericMethod(IMethodMember targetSignature, IControlledTypeCollection typeParameters)
        {
            if (!this.IsGenericDefinition)
                throw new InvalidOperationException();
            this.CheckGenericCache();
            this.genericCache.RegisterGenericMethod(targetSignature, typeParameters);
        }

        public void UnregisterGenericMethod(IControlledTypeCollection typeParameters)
        {
            this.CheckGenericCache();
            this.genericCache.UnregisterGenericMethod(typeParameters);
        }

        #endregion

        public override sealed TMethod MakeGenericClosure(IControlledTypeCollection genericReplacements)
        {
            if (!this.IsGenericDefinition)
                throw new InvalidOperationException();
            TMethod result = default(TMethod);
            this.CheckGenericCache();
            if (this.genericCache.ContainsGenericMethod(genericReplacements, ref result))
                return result;
            /* *
             * _IGenericMethodRegistrar handles cache.
             * */
            return this.OnMakeGenericMethod(genericReplacements);
        }

        protected abstract TMethod OnMakeGenericMethod(IControlledTypeCollection genericReplacements);

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

        #region _IGenericMethodRegistrar Members

        public void RegisterGenericChild(IMethodParent parent, IMethodMember genericChild)
        {
            throw new NotSupportedException();
        }

        public void UnregisterGenericChild(IMethodParent parent)
        {
            throw new NotSupportedException();
        }

        #endregion

        private void CheckGenericCache()
        {
            lock (this.syncObject)
                if (this.genericCache == null)
                    this.genericCache = new GenericMethodCache<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>();
        }
    }
}
