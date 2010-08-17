using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    /// <summary>
    /// Defines properties and methods for a lexical analysis state machine.
    /// </summary>
    /// <remarks>
    /// Utilizes a <see cref="ITokenizer.Stream"/> to verify that 
    /// the next bytes in the lexical analysis are valid.
    /// </remarks>
    public interface ITokenizer :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="System.IO.Stream"/> the <see cref="ITokenizer"/> is using.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        TextReader Reader { get; }
        /// <summary>
        /// A <see cref="Stream"/> which represents the current input source.
        /// </summary>
        Stream Stream { get; }
        /// <summary>
        /// Looks <paramref name="ahead"/> in the <see cref="Stream"/> and returns the 
        /// character at that point.
        /// </summary>
        /// <param name="ahead">A <see cref="System.Int64"/> which represents the number of 
        /// characters to look ahead.</param>
        /// <returns>A <see cref="Char"/> which represents the look 
        /// <paramref name="ahead"/>.</returns>
        char LookAhead(long ahead);
        /// <summary>
        /// Flushes the full look-ahead based upon the overall length of the buffer
        /// size.
        /// </summary>
        /// <returns>A series of <see cref="Char"/> values relative to the current 
        /// full buffer.</returns>
        char[] Flush();
        /// <summary>
        /// Flushes the look-ahead buffer and returns a character
        /// array of the <paramref name="length"/> provided.
        /// </summary>
        /// <param name="length">An <see cref="Int64"/> which represents the number of
        /// characters to return from the buffer.</param>
        /// <returns>A series of <see cref="Char"/> values relative to the current buffer.</returns>
        char[] Flush(long length);
    }
}
