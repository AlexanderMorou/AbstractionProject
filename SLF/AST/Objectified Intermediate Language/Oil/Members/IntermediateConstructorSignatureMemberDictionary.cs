﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base intermediate constructor member dictionary.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableType{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableSignatureType{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IntermediateGroupedSignatureMemberDictionary<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        IIntermediateConstructorSignatureMemberDictionary
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TCtor
        where TType :
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TType
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateConstructorSignatureMemberDictionary{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// with the <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateSignatureParent"/>
        /// which contains the <see cref="IntermediateConstructorSignatureMemberDictionary{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>.</param>
        protected IntermediateConstructorSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateType parent) :
            base(master, parent)
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
        /// which contains the <see cref="IntermediateConstructorSignatureMemberDictionary{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>.</param>
        /// <param name="root">The <see cref="IntermediateConstructorSignatureMemberDictionary{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// which the current is based upon.</param>
        protected IntermediateConstructorSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateType parent, IntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> root) :
            base(master, parent, root)
        {
        }

        #region IIntermediateConstructorSignatureMemberDictionary<TCtor,TIntermediateCtor,TType,TIntermediateType> Members

        /// <summary>
        /// Adds a <typeparamref name="TIntermediateCtor"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">The <see cref="TypedNameSeries"/>
        /// which identifies all of the parameters by name and type.</param>
        /// <returns>A <typeparamref name="TIntermediateCtor"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        /// <exception cref="System.ArgumentException">The provided set of <paramref name="parameters"/>
        /// exists already, or a name of one of the parameters is null.</exception>
        public TIntermediateCtor Add(TypedNameSeries parameters)
        {
            TIntermediateCtor item = this.GetConstructor(parameters);
            if (this.ContainsKey(item.UniqueIdentifier))
                throw new ArgumentException("parameters");
            base.Add(item.UniqueIdentifier, item);
            return item;
        }

        /// <summary>
        /// Adds a <typeparamref name="TIntermediateCtor"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">An array of <see cref="TypedName"/>
        /// instances which identify all parameters by type and name.</param>
        /// <returns>A <typeparamref name="TIntermediateCtor"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parameters"/> is null</exception>
        public TIntermediateCtor Add(params TypedName[] parameters)
        {
            return this.Add(new TypedNameSeries(parameters));
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected abstract TIntermediateCtor GetConstructor(TypedNameSeries parameters);

        #region IIntermediateConstructorSignatureMemberDictionary Members

        IIntermediateConstructorSignatureMember IIntermediateConstructorSignatureMemberDictionary.Add(TypedNameSeries parameters)
        {
            return this.Add(parameters);
        }

        IIntermediateConstructorSignatureMember IIntermediateConstructorSignatureMemberDictionary.Add(params TypedName[] parameters)
        {
            return this.Add(parameters);
        }

        #endregion

    }
}