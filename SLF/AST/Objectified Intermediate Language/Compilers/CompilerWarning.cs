using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerWarning : 
        ICompilerWarning
    {
        private string[] replacements;
        private ICompilerReferenceWarning message;

        public CompilerWarning(ICompilerReferenceWarning message, int column, int line, string fileName, params string[] replacements) 
        {
            if (message == null)
                throw new ArgumentNullException("message");
            this.FileName = fileName;
            this.replacements = replacements;
            this.message = message;
            this.Location = new LineColumnPair() { Column = column, Line = line };
        }
        #region ICompilerWarning Members

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
