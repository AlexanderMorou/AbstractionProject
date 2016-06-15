using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines propertise and methods for working with a create 
    /// anonymous type expression's assignment expression series.
    /// </summary>
    public interface ICreateAnonymousTypeMemberAssignmentDictionary :
        IControlledDictionary<string, ICreateAnonymousTypeMemberAssignment>
    {
        /// <summary>
        /// Adds a <see cref="ICreateAnonymousTypeMemberAssignment"/> to
        /// the <see cref="ICreateAnonymousTypeMemberAssignmentDictionary"/>
        /// with the <paramref name="name"/>, <paramref name="value"/> 
        /// and <paramref name="immutable"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>that designates 
        /// the name of the property to assign to <paramref name="value"/>.
        /// </param>
        /// <param name="value">The <see cref="IExpression"/> that represents
        /// the value to be assigned.</param>
        /// <param name="immutable">Whether the member associated to the 
        /// <see cref="ICreateAnonymousTypeMemberAssignment">assignment</see>
        /// is immutable (true) or mutable (false).</param>
        /// <returns>A new <see cref="ICreateAnonymousTypeMemberAssignment"/>
        /// relative to the <paramref name="name"/>, <paramref name="value"/> 
        /// and <paramref name="immutable"/> provided.</returns>
        ICreateAnonymousTypeMemberAssignment Add(string name, IExpression value, bool immutable = true);
        /// <summary>
        /// Adds a <see cref="ICreateAnonymousTypeMemberAssignment"/> to the
        /// <see cref="ICreateAnonymousTypeMemberAssignmentDictionary"/>.
        /// </summary>
        /// <param name="assignment">The <see cref="ICreateAnonymousTypeMemberAssignment"/>
        /// to insert.</param>
        void Add(ICreateAnonymousTypeMemberAssignment assignment);
    }
}
