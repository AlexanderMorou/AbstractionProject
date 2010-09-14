using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface IScannableEntry :
        INamedEntry
    {
        /// <summary>
        /// Returns the means to which the <see cref="IScannableEntry"/> handles new lines.
        /// </summary>
        EntryScanMode ScanMode { get; }
    }
}
