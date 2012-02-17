using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    internal class CSharpCompilerReferenceWarning :
        CompilerReferenceWarning,
        ICSharpCompilerReferenceWarning
    {
        public CSharpCompilerReferenceWarning(string messageBase, CSharpWarningLevels warningLevel, CSharpWarningIdentifiers messageId)
            : base(messageBase, (int) warningLevel, (int) messageId)
        {
        }

        #region ICSharpCompilerReferenceWarning Members

        public new CSharpWarningIdentifiers MessageIdentifier
        {
            get { return (CSharpWarningIdentifiers) base.MessageIdentifier; }
        }

        public new CSharpWarningLevels WarningLevel
        {
            get { return (CSharpWarningLevels) base.WarningLevel; }
        }

        #endregion
    }
}
