using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a default intermediate signature member dictionary.
    /// </summary>
    /// <typeparam name="TSignature">The type of 
    /// <see cref="ISignatureMember{TSignature, TSignatureParameter, TSignatureParent}"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The type of 
    /// <see cref="IIntermediateSignatureMember{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> 
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of 
    /// <see cref="ISignatureParameterMember{TSignature, TSignatureParameter, TSignatureParent}"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParameter">The type of 
    /// <see cref="IIntermediateSignatureParameterMember{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> 
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParent">The type of 
    /// <see cref="ISignatureParent{TSignature, TSignatureParameter, TSignatureParent}"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParent">The type of 
    /// <see cref="IIntermediateSignatureParent{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> 
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateMemberDictionary<TSignatureParent, TIntermediateSignatureParent, TSignature, TIntermediateSignature>,
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
        /// Creates a new <see cref="IntermediateSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>
        /// initialized to its default state.
        /// </summary>
        public IntermediateSignatureMemberDictionary(TIntermediateSignatureParent parent) :
            base(parent)
        {
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>
        /// with the <see cref="Dictionary{TKey, TValue}"/> <paramref name="toWrap"/>.
        /// </summary>
        /// <param name="toWrap">The <see cref="IntermediateSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> the current is based upon.</param>
        public IntermediateSignatureMemberDictionary(TIntermediateSignatureParent parent, IntermediateSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> toWrap) :
            base(parent, toWrap)
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
            return CLICommon.FindCache<TSignature, TSignatureParameter, TSignatureParent>(((ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>)this).Values, search, strict);
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
            return CLICommon.FindCache<TSignature, TSignatureParameter, TSignatureParent>(((ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>)this).Values, search, strict);
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
