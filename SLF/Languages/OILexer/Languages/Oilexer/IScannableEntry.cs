using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright � 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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