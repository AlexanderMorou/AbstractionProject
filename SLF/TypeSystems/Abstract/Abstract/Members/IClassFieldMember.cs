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
    /// Defines properties and methods for working with the field of a class.
    /// </summary>
    public interface IClassFieldMember :
        IFieldMember<IClassFieldMember, IClassType>,
        IInstanceFieldMember,
        IScopedDeclaration
    {
        /// <summary>
        /// Returns the <see cref="InstanceFieldMemberAttributes"/> that determine how the
        /// <see cref="IClassFieldMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        new InstanceFieldMemberAttributes Attributes { get; }
    }
}
