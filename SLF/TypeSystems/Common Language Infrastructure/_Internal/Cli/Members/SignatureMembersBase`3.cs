using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides a base implementation of <see cref="ISignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// for working with a series of <typeparamref name="TSignature"/> instances contained by a 
    /// <typeparamref name="TSignatureParent"/>. 
    /// </summary>
    /// <typeparam name="TSignature">The type of <see cref="ISignatureMember{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by the current implementation.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of <see cref="ISignatureParameterMember{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of <see cref="ISignatureParent{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// that contains <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    internal abstract class SignatureMembersBase<TSignature, TSignatureParameter, TSignatureParent> :
        GroupedMembersBase<TSignatureParent, TSignature>,
        ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>,
        ISignatureMemberDictionary
        where TSignature :
            ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
    {
        /// <summary>
        /// Creates a new <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>
        /// with the <paramref name="parent"/> provided
        /// </summary>
        /// <param name="master">The <see cref="FullMembersBase"/> 
        /// which moderates the 
        /// <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TSignatureParent"/> which contains the 
        /// <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>.</param>
        protected SignatureMembersBase(FullMembersBase master, TSignatureParent parent)
            : base(master, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>
        /// initialized to a default state.
        /// </summary>
        /// <param name="master">The <see cref="FullMembersBase"/> 
        /// which moderates the 
        /// <see cref="SignatureMembersBase{TSignature, TSignatureParameter, TSignatureParent}"/>.</param>
        internal SignatureMembersBase(FullMembersBase master) :
            base(master)
        {

        }
        #region ISignatureMemberDictionary<TSignature,TSignatureParameter,TSignatureParent> Members

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        public virtual IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, ITypeCollection search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(this.Values, search, strict);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        public virtual IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, params IType[] search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(this.Values, search, strict);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(bool, ITypeCollection)"/> where strict is true.</remarks>
        public virtual IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(ITypeCollection search)
        {
            return this.Find(true, search);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(bool, IType[])"/> where strict is true.</remarks>
        public virtual IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(params IType[] search)
        {
            return this.Find(true, search);
        }

        #endregion

        #region ISignatureMemberDictionary Members

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(bool strict, ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(strict, search);
        }

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(search);
        }

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(bool strict, params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(strict, search);
        }

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(search);
        }

        #endregion
    }
}
