﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public class IntermediateGroupedSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateGroupedMemberDictionary<TSignatureParent, TIntermediateSignatureParent, TSignature, TIntermediateSignature>,
        IIntermediateSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateSignatureMemberDictionary
        where TSignature :
            ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateSignatureMember<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            TSignatureParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
    {

        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> with the 
        /// <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateSignatureParent"/>
        /// which contains the <see cref="IntermediateGroupedSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>.</param>
        public IntermediateGroupedSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent)
            : base(master, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/> and <paramref name="items"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateSignatureParent"/>
        /// which contains the <see cref="IntermediateGroupedSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>.</param>
        /// <param name="root">The <see cref="IntermediateGroupedSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>
        /// which the current is based upon.</param>
        public IntermediateGroupedSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent, IntermediateGroupedSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> root)
            : base(master, parent, root)
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
        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, ITypeCollection search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(((ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>)this).Values, search, strict);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(bool, ITypeCollection)"/> where strict is true.</remarks>
        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(ITypeCollection search)
        {
            return this.Find(true, search);
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
        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, params IType[] search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(((ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>)this).Values, search, strict);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(bool, IType[])"/> where strict is true.</remarks>
        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(params IType[] search)
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