using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;
using System.IO;
using AllenCopeland.Abstraction.Slf._Internal.Oilexer;
using System.Threading;

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
