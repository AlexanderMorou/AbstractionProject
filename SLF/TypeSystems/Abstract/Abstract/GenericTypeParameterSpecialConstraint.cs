using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// The special constraint on the <see cref="IGenericParameter"/>.
    /// </summary>
    public enum GenericTypeParameterSpecialConstraint
    {
        /// <summary>
        /// Contains no special constraint.
        /// </summary>
        None,
        /// <summary>
        /// Contains the 'struct' constraint.
        /// </summary>
        Struct,
        /// <summary>
        /// Contains the 'class' constraint.
        /// </summary>
        Class,
    }
}
