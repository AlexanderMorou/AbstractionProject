using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerReferenceError :
        ICompilerReferenceError
    {
        public CompilerReferenceError(string messageBase, int messageId)
        {
            this.MessageBase = messageBase;
            this.MessageIdentifier = messageId;
        }
        #region ICompilerReferenceMessage Members

        public int MessageIdentifier { get; private set; }

        public string MessageBase { get; private set; }

        #endregion
    }
}
