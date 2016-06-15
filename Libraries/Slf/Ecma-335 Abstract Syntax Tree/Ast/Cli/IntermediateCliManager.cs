using AllenCopeland.Abstraction.Slf._Internal.Ast.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    internal class IntermediateCliManager :
        CliManager,
        IIntermediateCliManager
    {
        /// <summary>
        /// Data member to store the intermediate types within the current model.
        /// </summary>
        private ControlledDictionary<IAssemblyUniqueIdentifier, IIntermediateAssembly> intermediateAssemblies = new ControlledDictionary<IAssemblyUniqueIdentifier, IIntermediateAssembly>();
        private bool isGlobalManager;
        private object syncObject = new object();
        internal IntermediateCliManager(IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment)
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
                        var key = this.intermediateAssemblies.Keys[index];
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

        public override ICliMetadataTypeDefinitionTableRow ResolveScope(ICliMetadataTypeDefOrRefRow scope)
        {
            if (scope == null)
                return null;
            var assembly = this.GetRelativeAssembly(scope.MetadataRoot);
            if (assembly is IIntermediateAssembly)
                throw new NotImplementedException();
            else
                return base.ResolveScope(scope);
        }

        public new IIntermediateCliRuntimeEnvironmentInfo RuntimeEnvironment
        {
            get { return (IIntermediateCliRuntimeEnvironmentInfo)base.RuntimeEnvironment; }
        }

        internal IntermediateCliRuntimeEnvironmentInfo _IntermediateCliRuntimeEnvironmentInfo
        {
            get
            {
                return this.RuntimeEnvironment as IntermediateCliRuntimeEnvironmentInfo;
            }
        }

        protected override void OnDisposed()
        {
            try
            {
                lock (this.syncObject)
                {
                    if (this.isGlobalManager)
                    {
                        this.isGlobalManager = false;
                        IntermediateCliGateway.GlobalResolutionPathAdded    -= IntermediateCliGateway_GlobalResolutionPathAdded;
                        IntermediateCliGateway.GlobalResolutionPathRemoved  -= IntermediateCliGateway_GlobalResolutionPathRemoved;
                    }
                }
            }
            finally
            {
                base.OnDisposed();
            }
        }

        internal void RegisterForGlobalPaths()
        {
            lock (syncObject)
            {
                isGlobalManager = true;
                IntermediateCliGateway.GlobalResolutionPathAdded    += IntermediateCliGateway_GlobalResolutionPathAdded;
                IntermediateCliGateway.GlobalResolutionPathRemoved  += IntermediateCliGateway_GlobalResolutionPathRemoved;
            }
        }

        void IntermediateCliGateway_GlobalResolutionPathRemoved(object sender, EventArgs<string> e)
        {
            if (_IntermediateCliRuntimeEnvironmentInfo != null)
                _IntermediateCliRuntimeEnvironmentInfo.AddResolutionPath(e.Arg1);
        }

        void IntermediateCliGateway_GlobalResolutionPathAdded(object sender, EventArgs<string> e)
        {
            if (_IntermediateCliRuntimeEnvironmentInfo != null)
                _IntermediateCliRuntimeEnvironmentInfo.RemoveResolutionPath(e.Arg1);
        }
    }
}
