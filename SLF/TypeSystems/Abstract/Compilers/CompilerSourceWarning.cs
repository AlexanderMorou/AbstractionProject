using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerSourceWarning : 
        ICompilerSourceWarning
    {
        private string[] replacements;
        private ICompilerReferenceWarning message;

        /// <summary>
        /// Creates a new <see cref="CompilerSourceWarning"/> instance with the <paramref name="message"/>, <paramref name="fileName"/>, <paramref name="line"/>,
        /// <paramref name="column"/>, and <paramref name="replacements"/> provided.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> which denotes
        /// the base message text to use along with <paramref name="replacements"/>.</param>
        /// <param name="fileName">The <see cref="String"/> value representing the location of the 
        /// warning in source.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="CompilerSourceWarning"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="CompilerSourceWarning"/> which results.</param>
        /// <param name="replacements"></param>
        public CompilerSourceWarning(ICompilerReferenceWarning message, Uri source, LineColumnPair start, LineColumnPair end, params string[] replacements) 
        {
            if (message == null)
                throw new ArgumentNullException("message");
            this.Source = source;
            this.replacements = replacements;
            this.message = message;
            this.Start = start;
            this.End = end;
        }

        public override string ToString()
        {
            bool startNull;
            bool endNull;
            if ((startNull = (Start.Line == 0 && Start.Column == 0)) |
                (endNull = (End.Line == 0 && End.Column == 0)))
            {
                if (startNull && endNull)
                    if (this.Source == null)
                        return this.Message;
                    else
                        return string.Format("{0} in {1}", this.Message, this.Source);
                else if (endNull)
                    return string.Format("{0} in {1}:({2})", this.Message, this.Source, this.Start.ToString());
                else
                    return string.Format("{0} in {1}:({2})", this.Message, this.Source, this.End.ToString());
            }
            else if (this.Source == null)
                return string.Format("{0} at ({1})", this.Message, this.Start.ToString());
            return string.Format("{0} in {1}:({2})", this.Message, this.Source, this.Start.ToString());
        }

        #region ICompilerSourceWarning Members

        public int Level { get { return this.message.WarningLevel; } }

        #endregion

        #region ISourceRelatedMessage Members

        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="CompilerSourceWarning"/>.
        /// </summary>
        public string Message
        {
            get
            {
                return string.Format(this.message.MessageBase, replacements);
            }
        }

        /// <summary>
        /// Returns the <see cref="LineColumnPair"/> which denotes the position, within <see cref="FileName"/>,
        /// in which the <see cref="CompilerSourceWarning"/> starts.
        /// </summary>
        public LineColumnPair Start { get; private set; }
        /// <summary>
        /// Returns the <see cref="LineColumnPair"/> which denotes the position, within the <see cref="FileName"/>,
        /// at which the <see cref="CompilerSourceWarning"/> ends.
        /// </summary>
        public LineColumnPair End { get; private set; }

        /// <summary>
        /// Returns the <see cref="Uri"/> which denotes the specific file
        /// in which the <see cref="CompilerSourceWarning"/> pertains to.
        /// </summary>
        public Uri Source { get; private set; }

        public int MessageIdentifier
        {
            get { return this.message.MessageIdentifier; }
        }

        #endregion
    }
}
