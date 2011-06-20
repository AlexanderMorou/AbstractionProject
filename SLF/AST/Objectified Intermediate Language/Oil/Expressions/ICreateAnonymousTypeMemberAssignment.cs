using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
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
