using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Compilers.Oilexer
{
    internal interface IUnicodeCollectiveTargetGraph :
        IControlledStateCollection<IUnicodeTargetGraph>
    {
        void Add(IUnicodeTargetGraph graph);

        IUnicodeTargetGraph Find(IUnicodeTargetGraph duplicate);
    }
}
