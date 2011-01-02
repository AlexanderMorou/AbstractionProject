using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the
    /// constructors of an intermediate creatable type.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor signature in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableType{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableSignatureType{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IIntermediateSignatureMemberDictionary<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateGroupedMemberDictionary<TType, TIntermediateType, TCtor, TIntermediateCtor>,
        IConstructorMemberDictionary<TCtor, TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
        /// <summary>
        /// Adds a <typeparamref name="TIntermediateCtor"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">The <see cref="TypedNameSeries"/>
        /// which identifies all of the parameters by name and type.</param>
        /// <returns>A <typeparamref name="TIntermediateCtor"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        TIntermediateCtor Add(TypedNameSeries parameters);
        /// <summary>
        /// Adds a <typeparamref name="TIntermediateCtor"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">An array of <see cref="TypedName"/>
        /// instances which identify all parameters by type and name.</param>
        /// <returns>A <typeparamref name="TIntermediateCtor"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parameters"/> is null</exception>
        TIntermediateCtor Add(params TypedName[] parameters);
    }
    /// <summary>
    /// Defines properties and methods for working with the 
    /// constructors on an intermediate creatable type.
    /// </summary>
    public interface IIntermediateConstructorSignatureMemberDictionary :
        IConstructorMemberDictionary
    {
        /// <summary>
        /// Adds a <see cref="IIntermediateConstructorSignatureMember"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">The <see cref="TypedNameSeries"/>
        /// which identifies all of the parameters by name and type.</param>
        /// <returns>A <see cref="IIntermediateConstructorSignatureMember"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        IIntermediateConstructorSignatureMember Add(TypedNameSeries parameters);
        /// <summary>
        /// Adds a <see cref="IIntermediateConstructorSignatureMember"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">An array of <see cref="TypedName"/>
        /// instances which identify all parameters by type and name.</param>
        /// <returns>A <see cref="IIntermediateConstructorSignatureMember"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parameters"/> is null</exception>
        IIntermediateConstructorSignatureMember Add(params TypedName[] parameters);
    }
}
