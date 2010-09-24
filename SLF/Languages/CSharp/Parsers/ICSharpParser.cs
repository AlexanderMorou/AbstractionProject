using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using System.IO;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    /// <summary>
    /// Defines properties and methods for working with a C&#9839; parser
    /// which parses a C&#9839; source file.
    /// </summary>
    public interface ICSharpParser :
        IParser<ICSharpAssembly, ICSharpTokenizer>
    {
        /// <summary>
        /// Returns the <see cref="ICSharpAssembly"/> of a parse operation as a partial
        /// to the <paramref name="root"/> provided.
        /// </summary>
        /// <param name="filename">The name of the file to parse.</param>
        ICSharpAssembly Parse(string filename, ICSharpAssembly root);
        /// <summary>
        /// Returns the <see cref="ICSharpAssembly"/> of a parse operation on the
        /// <paramref name="source"/> <see cref="Stream"/> as a partial
        /// to the <paramref name="root"/> provided.
        /// </summary>
        /// <param name="source">The <see cref="Stream"/> from which to read bytes from.</param>
        ICSharpAssembly Parse(Stream source, ICSharpAssembly root);
    }
}
