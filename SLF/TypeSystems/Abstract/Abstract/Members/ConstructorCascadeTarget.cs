﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// The target of a series of constructor cascade arguments.
    /// </summary>
    [Serializable]
    public enum ConstructorCascadeTarget
    {
        /// <summary>
        /// The constructor cascade isn't used.
        /// </summary>
        Undefined,
        /// <summary>
        /// The constructor cascade should occur on the base-type.
        /// </summary>
        Base,
        /// <summary>
        /// The constructor cascade should occur on the current type.
        /// </summary>
        This
    }
}