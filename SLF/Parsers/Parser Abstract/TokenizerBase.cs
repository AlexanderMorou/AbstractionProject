using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public class TokenizerBase :
        ITokenizer
    {
        /// <summary>
        /// Buffer used to look ahead, but only so far.
        /// </summary>
        private char[] buffer;
        /// <summary>
        /// The actual size of the data inside <see cref="buffer"/>.
        /// </summary>
        private long bufferSize = 0;
        /// <summary>
        /// The start location of the buffer.
        /// </summary>
        private long bufferStartLocale = 0;
        private List<long> lineStarts = new List<long>();
        private Stream stream;

        /// <summary>
        /// Creates a new <see cref="TokenizerBase"/> instance with the
        /// <paramref name="stream"/> provided.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> which 
        /// reads the characters from the target source.</param>
        protected TokenizerBase(Stream stream)
        {
            this.Stream = stream;
        }

        #region ITokenizer Members

        public TextReader Reader { get; private set; }
        public Stream Stream
        {
            get { return this.stream; }
            private set
            {
                this.stream = value;
                this.Reader = new StreamReader(value);
            }
        }

        /// <summary>
        /// Performs a lookahead in the current stream.
        /// </summary>
        /// <param name="howFar">The number of characters to look ahead.</param>
        /// <returns>The character of the index in the stream, if <paramref name="howFar"/> 
        /// is in range.  <see cref="Char.MinValue"/> otherwise.</returns>
        public char LookAhead(long howFar)
        {
            if (bufferStartLocale + (howFar + 1) > Stream.Length)
                return char.MinValue;
            if (buffer.LongLength < howFar)
            {
                long mSize = buffer.LongLength * 2;
                if (mSize < howFar)
                    mSize = howFar * 2;
                char[] buffer2 = new char[mSize];
                buffer.CopyTo(buffer2, 0);
                buffer = buffer2;
            }
            if (bufferSize < (howFar + 1))
                bufferSize += Reader.Read(buffer, (int)bufferSize, (int)((howFar + 1) - bufferSize));
            return (char)buffer[howFar];
        }

        /// <summary>
        /// Flushes the full look-ahead based upon the overall length of the buffer
        /// size.
        /// </summary>
        /// <returns>A series of <see cref="Char"/> values relative to the current 
        /// full buffer.</returns>
        public char[] Flush()
        {
            if (this.bufferSize == 0)
                return new char[0];
            return this.Flush(this.bufferSize);
        }

        /// <summary>
        /// Flushes the look-ahead buffer and returns a character
        /// array of the <paramref name="length"/> provided.
        /// </summary>
        /// <param name="length">An <see cref="Int64"/> which represents the number of
        /// characters to return from the buffer.</param>
        /// <returns>A series of <see cref="Char"/> values relative to the current buffer.</returns>
        public char[] Flush(long length)
        {
            char[] result = new char[length > bufferSize ? bufferSize : length];
            bool inCrLf = false;
            long crLfStart = 0;
            for (long i = 0; i < this.bufferSize; i++)
            {
                char cB = this.buffer[i];
                result[i] = cB;
                if (inCrLf)
                {
                    if (cB == '\r' || cB == '\n')
                        crLfStart++;
                    if (!lineStarts.Contains(crLfStart))
                        lineStarts.Add(crLfStart + 1);
                    inCrLf = false;
                    continue;
                }
                else if (cB == '\r' || cB == '\n')
                {
                    inCrLf = true;
                    crLfStart = this.bufferStartLocale + i;
                    continue;
                }
            }
            this.bufferStartLocale += result.Length;
            return result;
        }

        #endregion
        
        #region IDisposable Members

        public void Dispose()
        {
            this.Disposed(true);
        }

        #endregion

        protected virtual void Disposed(bool disposing)
        {
            if (this.stream != null)
                this.stream = null;
            if (this.lineStarts != null)
            {
                this.lineStarts.Clear();
                this.lineStarts = null;
            }
            this.bufferSize = 0;
            if (this.buffer != null)
                this.buffer = null;
            this.bufferStartLocale = 0;
        }
    }
}
