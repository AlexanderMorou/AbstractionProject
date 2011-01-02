using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using AllenCopeland.Abstraction.Slf.Oil.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal
{
    internal class ResourceFormatter
    {
        public static string FormatException_InvalidOperation_CompilerState(string reason)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.Exception_InvalidOperation_CompilerState, reason);
        }
    }
}
