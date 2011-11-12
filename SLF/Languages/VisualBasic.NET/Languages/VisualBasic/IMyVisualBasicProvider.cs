using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Defines properties and methods for working with a Visual
    /// Basic assembly which emits the My* series of supplemental 
    /// development aids.
    /// </summary>
    public interface IMyVisualBasicProvider :
        IVisualBasicProvider<IMyVisualBasicAssembly, IMyVisualBasicProvider>
    {
    }
}
