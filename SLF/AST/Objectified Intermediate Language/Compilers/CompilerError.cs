using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerError :
        ICompilerError
    {
        private string[] replacements;
        private ICompilerReferenceError message;

        public CompilerError(ICompilerReferenceError message, int column, int line, string fileName, params string[] replacements) 
        {
            if (message == null)
                throw new ArgumentNullException("message");
            this.FileName = fileName;
            this.replacements = replacements;
            this.message = message;
            this.Location = new LineColumnPair() { Column = column, Line = line };
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
