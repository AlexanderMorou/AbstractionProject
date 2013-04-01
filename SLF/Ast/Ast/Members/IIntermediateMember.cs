using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate member
    /// which belongs to a <typeparamref name="TIntermediateParent"/> instance.
    /// </summary>
    /// <typeparam name="TIdentifier">The kind of identifier used to differentiate
    /// the member from its siblings.</typeparam>
    /// <typeparam name="TParent">The type of <see cref="IMemberParent"/> in the abstract
    /// sense.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of <see cref="IIntermediateMemberParent"/> 
    /// in the intermediate sense.</typeparam>
    public interface IIntermediateMember<TIdentifier, TParent, TIntermediateParent> :
        IIntermediateDeclaration<TIdentifier>,
        IIntermediateMember,
        IMember<TIdentifier, TParent>
        where TIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TParent :
            IMemberParent
        where TIntermediateParent :
            TParent,
            IIntermediateMemberParent
    {
        /// <summary>
        /// Returns the parent of the 
        /// <see cref="IIntermediateMember{TIdentifier, TParent, TIntermediateParent}"/>.
        /// </summary>
        new TIntermediateParent Parent { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a 
    /// general case intermediate member of a type, namespace
    /// or assembly.
    /// </summary>
    public interface IIntermediateMember :
        IIntermediateDeclaration,
        IMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="IIntermediateMember"/>.
        /// </summary>
        new IIntermediateMemberParent Parent { get; }
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateMemberVisitor"/> to
        /// receive the <see cref="IIntermediateMember"/> as a visitor.</param>
        void Visit(IIntermediateMemberVisitor visitor);
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return for the visitor.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <param name="visitor">
        /// The <see cref="IIntermediateMemberVisitor{TResult, TContext}"/> to
        /// receive the <see cref="IIntermediateMember"/> as a visitor.
        /// </param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context);
    }
}
