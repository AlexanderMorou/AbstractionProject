using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
    /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableType{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableType{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateConstructorMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IIntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableType<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with the 
    /// constructors on an intermediate creatable type.
    /// </summary>
    public interface IIntermediateConstructorMemberDictionary :
        IIntermediateConstructorSignatureMemberDictionary
    {
        /// <summary>
        /// Adds a <see cref="IIntermediateConstructorMember"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">The <see cref="TypedNameSeries"/>
        /// which identifies all of the parameters by name and type.</param>
        /// <returns>A <see cref="IIntermediateConstructorMember"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        new IIntermediateConstructorMember Add(TypedNameSeries parameters);
        /// <summary>
        /// Adds a <see cref="IIntermediateConstructorMember"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">An array of <see cref="TypedName"/>
        /// instances which identify all parameters by type and name.</param>
        /// <returns>A <see cref="IIntermediateConstructorMember"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parameters"/> is null</exception>
        new IIntermediateConstructorMember Add(params TypedName[] parameters);
    }
}
