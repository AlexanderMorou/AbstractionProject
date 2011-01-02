using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines propertise and methods for working with a create 
    /// instance expression's assignment expression series.
    /// </summary>
    public interface ICreateInstanceMemberAssignmentDictionary :
        IControlledStateDictionary<string, ICreateInstanceMemberAssignment>
    {
        /// <summary>
        /// Adds a <see cref="ICreateInstanceMemberAssignment"/> to
        /// the <see cref="ICreateInstanceMemberAssignmentDictionary"/>
        /// with the <paramref name="name"/> and <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>that designates 
        /// the name of the property to assign to <paramref name="value"/>.</param>
        /// <param name="value">The <see cref="IExpression"/> that represents
        /// the value to be assigned.</param>
        /// <returns>A new <see cref="ICreateInstanceMemberAssignment"/>
        /// relative to the <paramref name="name"/> and <paramref name="value"/>
        /// provided</returns>
        ICreateInstanceMemberAssignment Add(string name, IExpression value);
        /// <summary>
        /// Adds a <see cref="ICreateInstanceMemberAssignment"/> to the
        /// <see cref="ICreateInstanceMemberAssignmentDictionary"/>.
        /// </summary>
        /// <param name="assignment">The <see cref="ICreateInstanceMemberAssignmentDictionary"/>
        /// to insert.</param>
        void Add(ICreateInstanceMemberAssignment assignment);
    }
}
