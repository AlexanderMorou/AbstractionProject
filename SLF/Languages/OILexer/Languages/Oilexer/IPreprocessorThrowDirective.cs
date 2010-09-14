using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface IPreprocessorThrowDirective :
        IPreprocessorDirective
    {
        /// <summary>
        /// Returns the reference to the error referenced by the throw directive.
        /// </summary>
        IErrorEntry Reference { get; }

        /// <summary>
        /// Returns the arguments associated with the throw directive, all tokens.
        /// </summary>
        IGDToken[] Arguments { get; }
    }
}
