using System;
using System.Collections.Generic;
using System.Text;
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
    /// Defines properties and methods for working with a <see cref="IIntermediateMember{TParent, TIntermediateParent}"/>
    /// which belongs to a <typeparamref name="TIntermediateParent"/> instance.
    /// </summary>
    /// <typeparam name="TParent">The type of <see cref="IMemberParent"/> in the abstract
    /// sense.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of <see cref="IIntermediateMemberParent"/> 
    /// in the intermediate sense.</typeparam>
    public interface IIntermediateMember<TParent, TIntermediateParent> :
        IIntermediateMember,
        IMember<TParent>
        where TParent :
            IMemberParent
        where TIntermediateParent :
            TParent,
            IIntermediateMemberParent
    {
        /// <summary>
        /// Returns the parent of the 
        /// <see cref="IIntermediateMember{TParent, TIntermediateParent}"/>.
        /// </summary>
        new TIntermediateParent Parent { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a general case 
    /// intermediate member of a type, namespace or assembly.
    /// </summary>
    public interface IIntermediateMember :
        IIntermediateDeclaration,
        IMember
    {
    }
}