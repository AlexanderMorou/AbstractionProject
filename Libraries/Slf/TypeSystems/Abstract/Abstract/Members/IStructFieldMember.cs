﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a field defined on a 
    /// <see cref="IStructType"/>.
    /// </summary>
    public interface IStructFieldMember :
        IFieldMember<IStructFieldMember, IStructType>,
        IInstanceMember,
        IScopedDeclaration
    {
        /// <summary>
        /// Returns the <see cref="InstanceFieldMemberAttributes"/> that determine how the
        /// <see cref="IStructFieldMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        new InstanceFieldMemberAttributes Attributes { get; }
    }
}
