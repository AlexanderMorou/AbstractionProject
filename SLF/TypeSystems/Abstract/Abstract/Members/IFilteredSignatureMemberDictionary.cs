﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of <typeparamref name="TSignature"/> 
    /// instances filtered from another 
    /// <see cref="ISignatureMemberDictionary{TSignature, TSignatureParameter, TSignatureParent}"/> with data relative
    /// to how far each <typeparamref name="TSignature"/> deviated from the filter criteria.
    /// </summary>
    /// <typeparam name="TSignature">The type of <see cref="ISignatureMember{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by the current implementation.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of <see cref="ISignatureParameterMember{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// contained by <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of <see cref="ISignatureParent{TSignature, TSignatureParameter, TSignatureParent}"/>
    /// that contains <typeparamref name="TSignature"/> instances in the current implementation.</typeparam>
    /// <remarks>The deviation data is used to determine the best signature match for a link operation.
    /// When calling methods, certain data types implicitly cast to another type, finding the least number
    /// of implicit conversions is necessary to find the best method to link.</remarks>
    public interface IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> :
        ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>
        where TSignature :
            ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
    {
        /// <summary>
        /// Returns how far a <paramref name="deviant"/> strayed from the original type signature.
        /// </summary>
        /// <param name="deviant">The <typeparamref name="TSignature"/> expected of deviating.</param>
        /// <returns>A <see cref="System.Int32"/> which indicates how far the <paramref name="deviant"/>
        /// strayed.</returns>
        int this[TSignature deviant] { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with a series
    /// of filtered signature members.
    /// </summary>
    public interface IFilteredSignatureMemberDictionary :
        ISignatureMemberDictionary
    {
        /// <summary>
        /// Returns how far a <paramref name="deviant"/> strayed from the original type signature.
        /// </summary>
        /// <param name="deviant">The signature expected of deviating.</param>
        /// <returns>A <see cref="System.Int32"/> which indicates how far the <paramref name="deviant"/>
        /// strayed.</returns>
        int this[IMember deviant] { get; }
    }
}