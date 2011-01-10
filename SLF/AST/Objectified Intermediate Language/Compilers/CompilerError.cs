using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerError :
        ICompilerSourceError
    {
        private string[] replacements;
        private ICompilerReferenceError message;

        public CompilerError(ICompilerReferenceError message, string fileName, int line, int column, params string[] replacements) 
        {
            if (message == null)
                throw new ArgumentNullException("message");
            this.FileName = fileName;
            this.replacements = replacements;
            this.message = message;
            this.Location = new LineColumnPair(line, column);
        }

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
