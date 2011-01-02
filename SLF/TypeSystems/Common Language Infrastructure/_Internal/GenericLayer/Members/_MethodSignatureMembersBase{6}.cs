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
    internal abstract class _MethodSignatureMembersBase<TSignatureParameter, TSignature, TSignatureParent, TSignatures> :
        _SignatureMembersBase<TSignature, TSignatureParameter, TSignatureParent, TSignatures>,
        IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>,
        IMethodSignatureMemberDictionary
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatures :
            class,
            IGroupedMemberDictionary<TSignatureParent, TSignature>,
            IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>
    {
        protected _MethodSignatureMembersBase(_FullMembersBase master, TSignatures originalSet, TSignatureParent parent)
            : base(master, originalSet, parent)
        {
        }

        #region IMethodSignatureMemberDictionary<TSignatureParameter,TSignature,TSignatureParent> Members

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="ISignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, ITypeCollection search)
        {
            return this.Find(name, null, strict, search);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="ISignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, ITypeCollection)"/> where strict is true.</remarks>
        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection search)
        {
            return this.Find(name, null, true, search);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="ISignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, params IType[] search)
        {
            return this.Find(name, null, strict, search);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="name">The root name of the items to include in the result.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="ISignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(string, IType[])"/> where strict is true.</remarks>
        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, params IType[] search)
        {
            return this.Find(name, null, true, search);
        }


        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, ITypeCollection search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(genericParameters, this.Values, name, search, strict);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, ITypeCollection search)
        {
            return this.Find(name, genericParameters, true, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, params IType[] search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(genericParameters, this.Values, name, search, strict);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, params IType[] search)
        {
            return this.Find(name, genericParameters, true, search);
        }

        #endregion

        #region IMethodSignatureMemberDictionary Members

        int IMethodSignatureMemberDictionary.IndexOf(IMethodSignatureMember method)
        {
            if (!(method is TSignature))
                return -1;
            return this.IndexOf((TSignature)(method));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, bool strict, ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(name, genericParameters, strict, search);
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(name, genericParameters, search);
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, bool strict, params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(name, genericParameters, strict, search);
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(name, genericParameters, search);
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, bool strict, ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(name, null, strict, search);
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(name, null, search);
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, bool strict, params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(name, null, strict, search);
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(name, null, search);
        }

        #endregion

        #region IMemberDictionary Members

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

    }
}
