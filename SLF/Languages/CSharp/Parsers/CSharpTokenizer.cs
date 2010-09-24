using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public class CSharpTokenizer :
        TokenizerBase,
        ICSharpTokenizer
    {
        /// <summary>
        /// Creates a new <see cref="CSharpTokenizer"/> with the
        /// <paramref name="stream"/> provided.
        /// </summary>
        /// <param name="stream">The <see cref="System.IO.Stream"/>
        /// from which to read the C&#9839; tokens from.</param>
        public CSharpTokenizer(Stream stream)
            : base(stream)
        {
        }
    }
}
