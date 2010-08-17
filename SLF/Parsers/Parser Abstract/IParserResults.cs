using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    /// <summary>
    /// General case label interface for custom parser implementations to expand upon
    /// and use with <see cref="IParser{TResults, TTokenizer}"/>.
    /// </summary>
    public interface IParserResults
    {
        /// <summary>
        /// Returns whether the parse resulted in success.
        /// </summary>
        bool Successful { get; }
    }
}
