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
    /// <summary>
    /// Defines generic properties and methods for working with a semantic
    /// parser.
    /// </summary>
    /// <typeparam name="TResults">The type of <see cref="IParserResults"/> the
    /// <see cref="IParser{TResults, TTokenizer}"/> yields upon parse.</typeparam>
    /// <typeparam name="TTokenizer">The type of <see cref="ITokenizer"/> the 
    /// <see cref="IParser{TResults, TTokenizer}"/> uses to stream tokens.</typeparam>
    public interface IParser<TResults, TTokenizer> :
        IParser
        where TResults :
            IParserResults
        where TTokenizer :
            ITokenizer
    {
        /// <summary>
        /// Returns the <typeparamref name="TResults"/> of a parse operation.
        /// </summary>
        /// <param name="filename">The name of the file to parse.</param>
        /// <returns>A <typeparamref name="TResults"/> of the parse operation.</returns>
        new TResults Parse(string fileName);
        /// <summary>
        /// Returns the <typeparamref name="TResults"/> of a parse operation on the
        /// <paramref name="source"/> <see cref="Stream"/>.
        /// </summary>
        /// <param name="source">The <see cref="Stream"/> from which to read bytes from.</param>
        /// <returns>A <typeparamref name="TResults"/> of the parse operation.</returns>
        new TResults Parse(Stream source);
        /// <summary>
        /// Returns the <typeparamref name="TTokenizer"/> instance
        /// that the current <see cref="IParser{TResults, TTokenizer}"/> 
        /// uses to stream tokens.
        /// </summary>
        /// <remarks>The tokenizer is not stateless and a new instance will 
        /// be used per stream.  Caching instances of the tokenizer is therefore
        /// not recommended due to their volatile instance nature.</remarks>
        new TTokenizer Tokenizer { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with a lexical parser.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Returns the <see cref="IParserResults"/> of a parse operation 
        /// on the <see cref="String"/> <paramref name="filename"/> provided.
        /// </summary>
        /// <param name="filename">The name of the file to parse.</param>
        /// <returns>A <see cref="IParserResults"/> of the parse operation.</returns>
        IParserResults Parse(string fileName);
        /// <summary>
        /// Returns the <see cref="IParserResults"/> of a parse operation on the
        /// <paramref name="source"/> <see cref="Stream"/>.
        /// </summary>
        /// <param name="source">The <see cref="Stream"/> from which
        /// to read bytes from.</param>
        /// <returns>A <see cref="IParserResults"/> of the parse
        /// operation.</returns>
        IParserResults Parse(Stream source);
        /// <summary>
        /// Returns the <see cref="ITokenizer"/> instance
        /// that the current <see cref="IParser"/> 
        /// uses to understand tokens.
        /// </summary>
        /// <remarks>The tokenizer is not stateless and a new instance will 
        /// be used per stream.  Caching instances of the tokenizer is therefore
        /// not recommended due to their volatile instance nature.</remarks>
        ITokenizer Tokenizer { get; }
    }
}
