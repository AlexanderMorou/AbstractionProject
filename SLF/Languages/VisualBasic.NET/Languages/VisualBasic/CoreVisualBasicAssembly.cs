using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    public sealed class CoreVisualBasicAssembly :
        VisualBasicAssembly<ICoreVisualBasicAssembly, ICoreVisualBasicProvider, CoreVisualBasicAssembly>,
        ICoreVisualBasicAssembly
    {
        private CoreVisualBasicAssembly(CoreVisualBasicAssembly root)
            : base(root)
        {
        }

        public CoreVisualBasicAssembly(string name)
            : base(name, VisualBasicLanguage.Singleton.GetProvider())
        {

        }
        public CoreVisualBasicAssembly(string name, ICoreVisualBasicProvider provider)
            : base(name, provider)
        {

        }

        protected override CoreVisualBasicAssembly GetNewPart()
        {
            return new CoreVisualBasicAssembly(this);
        }
    }
}
