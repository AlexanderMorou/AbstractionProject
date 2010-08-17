using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
/*----------------------------------------\
| Copyright © 2009 Allen Copeland Jr.     |
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
        _IGenericMethodSignatureRegistrar
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {
        /// <summary>
        /// Data member for the generic parameters cache.
        /// </summary>
        private Dictionary<ITypeCollectionBase, IMethodSignatureMember> genericCache = null;

        internal _MethodSignatureMemberBase(TSignatureParent parent, TSignature original)
            : base(parent, original)
        {

        }

        internal _MethodSignatureMemberBase(TSignature original, ITypeCollectionBase genericReplacements)
            : base(original, genericReplacements)
        {
        }

        protected override IParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>> InitializeParameters()
        {
            return new _Parameters(this, this.Original.Parameters);
        }

        #region _IGenericMethodSignatureRegistrar Members

        public void RegisterGenericMethodSignature(IMethodSignatureMember targetSignature, ITypeCollectionBase typeParameters)
        {
            if (this.genericCache == null)
                this.genericCache = new Dictionary<ITypeCollectionBase, IMethodSignatureMember>();
            IMethodSignatureMember required = null;
            if (this.ContainsGenericMethodSignature(typeParameters, ref required))
                return;
            genericCache.Add(typeParameters, targetSignature);
        }

        public void UnregisterGenericMethodSignature(ITypeCollectionBase typeParameters)
        {
            if (this.genericCache == null)
                return;
            ITypeCollectionBase match = null;
            foreach (var itc in this.genericCache.Keys)
                if (itc.SequenceEqual(typeParameters))
                {
                    match = itc;
                    break;
                }
            //Nothing matched.
            if (match == null)
                return;
            genericCache.Remove(match);
            if (match is ILockedTypeCollection)
                ((ILockedTypeCollection)(match)).Dispose();
            else if (match is ITypeCollection)
                try
                {
                    ((ITypeCollection)(match)).Clear();
                }
                /* *
                 * Even being a type collection, it doesn't
                 * support modification, the proper response
                 * is not supported...?
                 * */
                catch (NotSupportedException)
                {
                }
        }

        #endregion

        private bool ContainsGenericMethodSignature(ITypeCollectionBase typeParameters, ref IMethodSignatureMember r)
        {
            if (this.genericCache == null)
                return false;
            var fd = this.genericCache.Keys.FirstOrDefault(itc => itc.SequenceEqual(typeParameters));
            if (fd == null)
                return false;
            r = this.genericCache[fd];
            return true;
        }
        public override sealed TSignature MakeGenericMethod(ITypeCollection genericReplacements)
        {
            if (!this.IsGenericMethodDefinition)
                throw new InvalidOperationException();
            IMethodSignatureMember k = null;
            if (this.ContainsGenericMethodSignature(genericReplacements, ref k))
                return ((TSignature)(k));
            /* *
             * _IGenericMethodRegistrar handles cache.
             * */
            var q = this.OnMakeGenericMethod(genericReplacements);
            CLIGateway.VerifyTypeParameters<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>(this, genericReplacements);
            return q;
        }

        protected abstract TSignature OnMakeGenericMethod(ITypeCollectionBase genericReplacements);

        public override void Dispose()
        {
            try
            {
                if (this.genericCache != null)
                {
                    for (int i = 0; i < this.genericCache.Count; i++)
                    {
                        var first = this.genericCache.Keys.ElementAt(0);
                        this.genericCache[first].Dispose();
                        this.genericCache.Remove(first);
                    }
                }
            }
            finally
            {
                base.Dispose();
            }
        }
    }
}
