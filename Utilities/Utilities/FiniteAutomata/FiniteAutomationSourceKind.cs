using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// The point within the automation from which
    /// the source is derived.
    /// </summary>
    [Flags]
    public enum FiniteAutomationSourceKind
    {
        /// <summary>
        /// No part of the automation, not used.
        /// </summary>
        None = 0,
        /// <summary>
        /// The point within the automation is 
        /// the initial state of the source.
        /// </summary>
        Initial = 1,
        /// <summary>
        /// The point within the automation is an 
        /// intermediate state of the source.
        /// </summary>
        Intermediate = 2,
        /// <summary>
        /// The point within the automation is a point of
        /// repetition state of the source.
        /// </summary>
        RepeatPoint = 4,
        /// <summary>
        /// The point within the automation is the final
        /// state of the source.
        /// </summary>
        Final = 8,
    }
}
