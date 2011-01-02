using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract partial class _MethodMemberBase<TMethod, TMethodParent> :
        _MethodSignatureMemberBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodMember<TMethod, TMethodParent>,
        _IGenericMethodRegistrar
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        /// <summary>
        /// Data member for the generic parameters cache.
        /// </summary>
        private Dictionary<ITypeCollectionBase, IMethodMember> genericCache = null;

        internal _MethodMemberBase(TMethodParent parent, TMethod original)
            : base(parent, original)
        {

        }
        internal _MethodMemberBase(TMethod original, ITypeCollectionBase genericParameters)
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

        public void RegisterGenericMethod(IMethodMember targetSignature, ITypeCollectionBase typeParameters)
        {
            if (this.genericCache == null)
                this.genericCache = new Dictionary<ITypeCollectionBase, IMethodMember>();
            IMethodMember required = null;
            if (this.ContainsGenericMethod(typeParameters, ref required))
                return;
            genericCache.Add(typeParameters, targetSignature);
        }

        public void UnregisterGenericMethod(ITypeCollectionBase typeParameters)
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

        private bool ContainsGenericMethod(ITypeCollectionBase typeParameters, ref IMethodMember r)
        {
            if (this.genericCache == null)
                return false;
            var fd = this.genericCache.Keys.FirstOrDefault(itc => itc.SequenceEqual(typeParameters));
            if (fd == null)
                return false;
            r = this.genericCache[fd];
            return true;
        }

        public override sealed TMethod MakeGenericClosure(ITypeCollectionBase genericReplacements)
        {
            if (!this.IsGenericDefinition)
                throw new InvalidOperationException();
            IMethodMember k = null;
            if (this.ContainsGenericMethod(genericReplacements, ref k))
                return ((TMethod)(k));
            /* *
             * _IGenericMethodRegistrar handles cache.
             * */
            TMethod tK = default(TMethod);
            tK = this.OnMakeGenericMethod(genericReplacements);
            CLIGateway.VerifyTypeParameters<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>(this, genericReplacements);
            return tK;
        }

        protected abstract TMethod OnMakeGenericMethod(ITypeCollectionBase genericReplacements);
    }
}
