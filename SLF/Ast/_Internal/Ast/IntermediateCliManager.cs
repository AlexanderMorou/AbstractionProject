using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
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
        private ControlledDictionary<IAssemblyUniqueIdentifier, IIntermediateAssembly> intermediateAssemblies = new ControlledDictionary<IAssemblyUniqueIdentifier, IIntermediateAssembly>();

        internal IntermediateCliManager(ICliRuntimeEnvironmentInfo runtimeEnvironment)
            : base(runtimeEnvironment)
        {
        }

        public bool IsIntermediateAssembly(IAssemblyUniqueIdentifier assemblyIdentity)
        {
            return intermediateAssemblies.ContainsKey(assemblyIdentity);
        }

        public void AssemblyCreated(IIntermediateAssembly assembly)
        {
            if (this.intermediateAssemblies.ContainsKey(assembly.UniqueIdentifier))
                throw new InvalidOperationException("A similarly named assembly within the current scope already exists.");
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            this.intermediateAssemblies._Add(assembly.UniqueIdentifier, assembly);
            assembly.IdentifierChanged += assembly_IdentifierChanged;
            assembly.Disposed += assembly_Disposed;
        }

        void assembly_Disposed(object sender, EventArgs e)
        {
            if (sender is IIntermediateAssembly)
            {
                var assembly = ((IIntermediateAssembly)(sender));
                lock (intermediateAssemblies)
                {
                    var index = this.intermediateAssemblies.Values.IndexOf(assembly);
                    if (index > -1)
                    {
                        var key = this.intermediateAssemblies.Keys[this.intermediateAssemblies.Values.IndexOf(assembly)];
                        this.intermediateAssemblies._Remove(key);
                    }
                }
                assembly.IdentifierChanged -= assembly_IdentifierChanged;
                assembly.Disposed -= assembly_Disposed;
            }
        }

        void assembly_IdentifierChanged(object sender, DeclarationIdentifierChangeEventArgs<IAssemblyUniqueIdentifier> e)
        {
            lock (this.intermediateAssemblies)
                if (this.intermediateAssemblies.ContainsKey(e.OldIdentifier))
                    this.intermediateAssemblies.Keys[this.intermediateAssemblies.Keys.IndexOf(e.OldIdentifier)] = e.NewIdentifier;
        }

        public override IAssembly ObtainAssemblyReference(IAssemblyUniqueIdentifier assemblyIdentity)
        {
            if (this.intermediateAssemblies.ContainsKey(assemblyIdentity))
                return this.intermediateAssemblies[assemblyIdentity];
            return base.ObtainAssemblyReference(assemblyIdentity);
        }

    }
}
