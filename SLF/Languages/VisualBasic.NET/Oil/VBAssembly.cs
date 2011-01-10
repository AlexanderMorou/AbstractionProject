using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// 
    /// </summary>
    public class VBAssembly :
        IntermediateAssembly<VBAssembly>
    {
        public VBAssembly(string name)
            : base(name)
        {
        }

        public VBAssembly(VBAssembly root)
            : base(root)
        {
            
        }

        protected override VBAssembly GetNewPart()
        {
            return new VBAssembly(this);
        }

    }
}
