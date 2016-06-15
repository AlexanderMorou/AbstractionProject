using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    internal class CSharpCompilerReferenceError : 
        CompilerReferenceError,
        ICSharpCompilerReferenceError
    {
        public CSharpCompilerReferenceError(string messageBase, CSharpErrorIdentifiers messageId)
            : base(messageBase, (int)messageId)
        {
        }

        #region ICSharpCompilerReferenceError Members

        public new CSharpErrorIdentifiers MessageIdentifier
        {
            get { return (CSharpErrorIdentifiers) base.MessageIdentifier; }
        }

        #endregion
    }
}
