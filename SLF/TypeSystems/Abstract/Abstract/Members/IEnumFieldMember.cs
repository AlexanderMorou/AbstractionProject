using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with an enumerator's field member.
    /// </summary>
    public interface IEnumFieldMember :
        IFieldMember<IEnumFieldMember, IEnumType>
    {
        /// <summary>
        /// Returns the <see cref="FieldMemberAttributes"/> that determine how the
        /// <see cref="IEnumFieldMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        /// <remarks>Always returns <see cref="FieldMemberAttributes.Constant"/></remarks>
        new FieldMemberAttributes Attributes { get; }
    }
}
