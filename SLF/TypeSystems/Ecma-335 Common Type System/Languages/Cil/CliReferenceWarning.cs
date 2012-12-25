using AllenCopeland.Abstraction.Slf.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    internal class CliReferenceWarning :
        CompilerReferenceWarning,
        ICliReferenceWarning
    {
        public CliReferenceWarning(string messageBase, CliWarningLevel warningLevel, int messageID)
            : base(messageBase, (int)warningLevel, messageID) { }

        public new CliWarningLevel WarningLevel
        {
            get { return (CliWarningLevel)base.WarningLevel; }
        }
    }
}
