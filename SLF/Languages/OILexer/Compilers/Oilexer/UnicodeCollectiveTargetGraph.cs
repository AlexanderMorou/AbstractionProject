using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Compilers.Oilexer
{
    internal class UnicodeCollectiveTargetGraph :
        ControlledStateCollection<IUnicodeTargetGraph>,
        IUnicodeCollectiveTargetGraph
    {

        #region IUnicodeCollectiveTargetGraph Members

        public void Add(IUnicodeTargetGraph graph)
        {
            base.baseCollection.Add(graph);
        }

        public IUnicodeTargetGraph Find(IUnicodeTargetGraph duplicate)
        {
            return this.FirstOrDefault(p => p.Equals(duplicate));
        }

        #endregion

    }
}
