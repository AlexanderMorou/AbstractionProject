using AllenCopeland.Abstraction.Slf.Ast.Cli;
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

        public CoreVisualBasicAssembly(string name, IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment)
            : base(name, VisualBasicLanguage.Singleton.GetProvider(), runtimeEnvironment)
        {

        }
        public CoreVisualBasicAssembly(string name, ICoreVisualBasicProvider provider, IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment)
            : base(name, provider, runtimeEnvironment)
        {

        }

        protected override CoreVisualBasicAssembly GetNewPart()
        {
            return new CoreVisualBasicAssembly(this);
        }
    }
}
