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
    /// Defines properties and methods for working with a field
    /// defined on an intermediate enumeration.
    /// </summary>
    public interface IIntermediateEnumFieldMember :
        IIntermediateMember<IEnumType, IIntermediateEnumType>,
        IIntermediateFieldMember,
        IEnumFieldMember
    {

        /// <summary>
        /// Returns/sets the <see cref="IIntermediateEnumFieldValue"/> the 
        /// <see cref="IIntermediateEnumFieldMember"/> is.
        /// </summary>
        IIntermediateEnumFieldValue Value { get; set; }
    }
}