using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate parameter
    /// with the <paramref name="TIntermediateParent"/> as its owner.
    /// </summary>
    /// <typeparam name="TParent">The <see cref="IParameterParent"/>
    /// type which owns the abstract definition of the current 
    /// <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/>.</typeparam>
    /// <typeparam name="TIntermediateParent">The <see cref="IIntermediateParameterParent"/>
    /// which owns the current <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/>.</typeparam>
    public interface IIntermediateParameterMember<TParent, TIntermediateParent> :
        IIntermediateMember<TParent, TIntermediateParent>,
        IIntermediateParameterMember,
        IParameterMember<TParent>
        where TParent :
            IParameterParent
        where TIntermediateParent :
            TParent, 
            IIntermediateParameterParent
    {

    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate parameter
    /// member.
    /// </summary>
    public interface IIntermediateParameterMember :
        IIntermediateMember,
        IIntermediateCustomAttributedDeclaration,
        IParameterMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="IIntermediateParameterMember"/>.
        /// </summary>
        new IIntermediateParameterParent Parent { get; }
        /// <summary>
        /// Returns/sets the type that the <see cref="IIntermediateParameterMember"/>
        /// is defined as.
        /// </summary>
        new IType ParameterType { get; set; }
        /// <summary>
        /// Returns/sets the direction the parameter is coerced.
        /// </summary>
        new ParameterDirection Direction { get; set; }
    }
}
