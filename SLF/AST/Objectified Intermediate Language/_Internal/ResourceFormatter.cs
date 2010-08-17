using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using AllenCopeland.Abstraction.Slf.Oil.Properties;

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
