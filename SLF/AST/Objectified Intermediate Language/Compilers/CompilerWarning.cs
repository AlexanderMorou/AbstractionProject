using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// <param name="line">The <see cref="Int32"/> value representing the line within file at <paramref name="fileName"/>
        /// on which the warning is located.</param>
        /// <param name="column">The <see cref="Int32"/> value representing the column on th <paramref name="line"/>
        /// within the file at <paramref name="fileName"/> on which the warning is located.</param>
        /// <param name="replacements"></param>
        public CompilerSourceWarning(ICompilerReferenceWarning message, string fileName, int line, int column, params string[] replacements) 
        {
            if (message == null)
                throw new ArgumentNullException("message");
            this.FileName = fileName;
            this.replacements = replacements;
            this.message = message;
            this.Location = new LineColumnPair(line,column);
        }

        #region ICompilerSourceWarning Members

        public int Level { get { return this.message.WarningLevel; } }

        #endregion

        #region ISourceRelatedMessage Members

        public string Message
        {
            get
            {
                return string.Format(this.message.MessageBase, replacements);
            }
        }

        public LineColumnPair Location { get; private set; }

        public string FileName { get; private set; }

        public int MessageIdentifier
        {
            get { return this.message.MessageIdentifier; }
        }

        #endregion
    }
}
