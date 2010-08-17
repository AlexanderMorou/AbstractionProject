using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Defines properties and methods for working with a series of 
    /// compiled types.
    /// </summary>
    interface ICompiledTypeDictionary :
        IDisposable
    {
        Type[] FilteredSeries { get; }
    }
}
