using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerSourceError :
        ICompilerSourceError
    {
        private string[] replacements;
        private ICompilerReferenceError message;

        public CompilerSourceError(ICompilerReferenceError message, Uri source, LineColumnPair start, LineColumnPair end, params string[] replacements) 
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

        #region ISourceRelatedMessage Members

        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="CompilerSourceError"/>.
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
        /// in which the <see cref="CompilerSourceError"/> starts.
        /// </summary>
        public LineColumnPair Start { get; private set; }
        /// <summary>
        /// Returns the <see cref="LineColumnPair"/> which denotes the position, within the <see cref="FileName"/>,
        /// at which the <see cref="CompilerSourceError"/> ends.
        /// </summary>
        public LineColumnPair End { get; private set; }
        /// <summary>
        /// Returns the <see cref="Uri"/> which denotes the specific file
        /// in which the <see cref="CompilerSourceError"/> pertains to.
        /// </summary>
        public Uri Source { get; private set; }

        /// <summary>
        /// Returns the <see cref="Int32"/> value which is language specific
        /// that denotes the unique identifier of the message.
        /// </summary>
        public int MessageIdentifier
        {
            get { return this.message.MessageIdentifier; }
        }

        #endregion
    }
}
