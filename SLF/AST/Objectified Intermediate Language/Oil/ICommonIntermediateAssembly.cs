using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface ICommonIntermediateAssembly :
        IIntermediateAssembly<ICommonIntermediateLanguage, ICommonIntermediateProvider>
    {
    }
}
