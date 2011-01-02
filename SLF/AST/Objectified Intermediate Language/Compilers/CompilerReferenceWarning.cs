using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerReferenceWarning : 
        ICompilerReferenceWarning
    {
        public CompilerReferenceWarning(string messageBase, int warningLevel, int messageID)
        {
            this.MessageBase = messageBase;
            this.MessageIdentifier = messageID;
            this.WarningLevel = warningLevel;
        }

        #region ICompilerReferenceWarning Members

        public int WarningLevel { get; private set; }

        #endregion

        #region ICompilerReferenceMessage Members

        public int MessageIdentifier { get; private set; }

        public string MessageBase { get; private set; }

        #endregion
    }
}
