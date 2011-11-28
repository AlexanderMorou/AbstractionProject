using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an assignment
    /// associated to the instantiation of an anonymous type.
    /// </summary>
    public interface ICreateAnonymousTypeMemberAssignment :
        ICreateInstanceMemberAssignment
    {
        /// <summary>
        /// Returns/sets whether the member associated to the 
        /// assignment is immutable (true) or mutable (false).
        /// </summary>
        bool Immutable { get; set; }
        /// <summary>
        /// Returns the <see cref="AnonymousTypeMember"/> associated
        /// to the assignment represented by the
        /// <see cref="ICreateAnonymousTypeMemberAssignment"/>.
        /// </summary>
        /// <returns>A <see cref="AnonymousTypeMember"/>
        /// with information relative to the current 
        /// <see cref="ICreateAnonymousTypeMemberAssignment"/>.</returns>
        AnonymousTypeMember GetMember();
    }
}
