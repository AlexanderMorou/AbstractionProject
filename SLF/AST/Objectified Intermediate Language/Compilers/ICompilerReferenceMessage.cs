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

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with a reference message
    /// for a compiler used to generate either errors or warnings.
    /// </summary>
    public interface ICompilerReferenceMessage
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value unique to the reference message.
        /// </summary>
        int MessageIdentifier { get; }
        /// <summary>
        /// Returns the <see cref="String"/> value relative to the 
        /// text associated to a given message from a compiler.
        /// </summary>
        string MessageBase { get; }
    }
}
