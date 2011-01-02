using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;
using System.IO;
using AllenCopeland.Abstraction.Slf._Internal.Oilexer;
using System.Threading;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens
{
    public class RegularLanguageDFARootState :
        RegularLanguageDFAState
    {
        private ITokenEntry entry;

        public RegularLanguageDFARootState(ITokenEntry entry)
        {
            this.entry = entry;
        }

        internal void Reduce(RegularCaptureType captureType)
        {
            Reduce(this, captureType == RegularCaptureType.Recognizer);
        }

        internal ITokenEntry Entry
        {
            get
            {
                return this.entry;
            }
        }
    }
}
