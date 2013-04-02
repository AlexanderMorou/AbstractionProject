using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods associated to a message
    /// from a compiler.
    /// </summary>
    public interface ICompilerMessage
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value which is language specific
        /// that denotes the unique identifier of the message.
        /// </summary>
        int MessageIdentifier { get; }
    }
}
