using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class CreateInstanceMemberAssignmentDictionary :
        ControlledDictionary<string, ICreateInstanceMemberAssignment>,
        ICreateInstanceMemberAssignmentDictionary
    {
        #region ICreateInstanceMemberAssignmentDictionary Members

        /// <summary>
        /// Adds a <see cref="ICreateInstanceMemberAssignment"/> to
        /// the <see cref="CreateInstanceMemberAssignmentDictionary"/>
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
        public ICreateInstanceMemberAssignment Add(string name, IExpression value)
        {
            var result = new CreateInstanceUnboundMemberAssignment(name, value);
            this._Add(name, result);
            return result;
        }

        /// <summary>
        /// Adds a <see cref="ICreateInstanceMemberAssignment"/> to the
        /// <see cref="ICreateInstanceMemberAssignmentDictionary"/>.
        /// </summary>
        /// <param name="assignment">The <see cref="ICreateInstanceMemberAssignmentDictionary"/>
        /// to insert.</param>
        public void Add(ICreateInstanceUnboundMemberAssignment assignment)
        {

            this._Add(assignment.Name, assignment);
        }

        #endregion
    }
}
