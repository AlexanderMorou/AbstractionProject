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

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public interface ISubScopeAggregate :
        IScopeAggregate
    {
        /// <summary>
        /// Returns the <see cref="IScopeAggregate"/>
        /// to which the current <see cref="ISubScopeAggregate"/>
        /// creates an identity union with.
        /// </summary>
        IScopeAggregate Parent { get; }
    }
}
