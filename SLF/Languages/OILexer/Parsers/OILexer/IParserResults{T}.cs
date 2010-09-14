using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Parsers.Oilexer
{
    /// <summary>
    /// Defines properties and methods for working with the generic results of a parse operation.
    /// </summary>
    public interface IParserResults<T> :
        IParserResults
    {
        /// <summary>
        /// Returns the resulted <typeparamref name="T"/> from the parse operation.
        /// </summary>
        new T Result { get; }
    }
}
