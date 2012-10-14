using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    internal class IntermediateCliManager :
        CliManager,
        IIntermediateCliManager
    {
        private IDictionary<IAssemblyUniqueIdentifier, IIntermediateAssembly> intermediateAssemblies = new Dictionary<IAssemblyUniqueIdentifier,IIntermediateAssembly>();

        internal IntermediateCliManager(ICliRuntimeEnvironmentInfo runtimeEnvironment)
            : base(runtimeEnvironment)
        {
        }

        public bool IsIntermediateAssembly(IAssemblyUniqueIdentifier assemblyIdentity)
        {
            return intermediateAssemblies.ContainsKey(assemblyIdentity);
        }

    }
}
