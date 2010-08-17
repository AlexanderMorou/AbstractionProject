using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// a switch statement.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Switch/Select statements are jump tables underneath the 
    /// syntactic sugar. Each case is a label, wherein the switch
    /// upon the case is shifted as close to zero as possible and
    /// a switch forward to the various label points is created.
    /// </para>
    /// <para>
    /// In string series, a dictionary is used, which uses the 
    /// string as a key with an index to the jump table's cases
    /// as the value half of the pair.
    /// </para>
    /// </remarks>
    public interface ISwitchStatement :
        IControlledStateCollection<ISwitchCaseBlockStatement>,
        IStatement
    {
        /// <summary>
        /// Returns the <see cref="ISwitchCaseBlockStatement"/> which
        /// represents the default target for the switch statement.
        /// </summary>
        ISwitchCaseBlockStatement DefaultBlock { get; }
        /// <summary>
        /// Returns the <see cref="IBreakExit"/> which denotes the 
        /// exit point of the current <see cref="ISwitchStatement"/>.
        /// </summary>
        IBreakExit BreakExit { get; }
    }
}
