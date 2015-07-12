using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate event's 
    /// raise, add or remove method members.
    /// </summary>
    public interface IIntermediateEventMethodMember :
        IIntermediateMethodMember
    {
        /// <summary>
        /// Informs an intermediate event's method member
        /// that the internal code generation method has changed.
        /// </summary>
        /// <param name="newManagementType">The <see cref="IntermediateEventManagementType"/>
        /// which indicates whether the code should be generated automatically or
        /// manually.</param>
        void GenerationTypeChanged(IntermediateEventManagementType newManagementType);
        /// <summary>
        /// Returns which method the <see cref="IIntermediateEventMethodMember"/> is.
        /// </summary>
        EventMethodType MethodType { get; }
    }
}
