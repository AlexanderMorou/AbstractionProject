using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Ast.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Documentation;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    public abstract class IntermediateCliAssembly<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity> :
        IntermediateAssembly<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity>,
        IIntermediateCliAssembly
        where TLanguage :
            ILanguage
        where TProvider :
            ILanguageProvider
        where TAssembly :
            IntermediateAssembly<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity>
        where TIdentityManager :
            ICliManager,
            IIdentityManager<TTypeIdentity, TAssemblyIdentity>,
            IIntermediateIdentityManager
    {
        private IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment;

        public CliFrameworkVersion FrameworkVersion
        {
            get
            {
                return this.RuntimeEnvironment.Version;
            }
            set
            {
                this.RuntimeEnvironment.Version = value;
            }
        }

        public new IIntermediateCliManager IdentityManager
        {
            get { return (IIntermediateCliManager)base.IdentityManager; }
        }

        public IIntermediateCliRuntimeEnvironmentInfo RuntimeEnvironment
        {
            get
            {
                return this.runtimeEnvironment;
            }
        }


        ICliRuntimeEnvironmentInfo ICliAssembly.RuntimeEnvironment
        {
            get { return this.RuntimeEnvironment; }
        }

        public ICliMetadataMutableRoot MetadataRoot
        {
            get { throw new NotImplementedException(); }
        }

        public ICliMetadataAssemblyMutableTableRow MetadataEntry
        {
            get { throw new NotImplementedException(); }
        }

        ICliMetadataRoot ICliAssembly.MetadataRoot { get { return this.MetadataRoot; } }

        ICliMetadataAssemblyTableRow ICliDeclaration<IAssemblyUniqueIdentifier, ICliMetadataAssemblyTableRow>.MetadataEntry { get { return this.MetadataEntry; } }

        ICliMetadataAssemblyTableRow ICliAssembly.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }

        public IControlledDictionary<ICliMetadataAssemblyRefTableRow, ICliAssembly> CliReferences
        {
            get { throw new NotImplementedException(); }
        }

        ICliManager ICliAssembly.IdentityManager
        {
            get { return this.IdentityManager; }
        }

        public new ICliAssemblyUniqueIdentifier UniqueIdentifier
        {
            get { return (ICliAssemblyUniqueIdentifier)base.UniqueIdentifier; }
        }

        ICliMetadataTableRow ICliDeclaration.MetadataEntry
        {
            get { throw new NotImplementedException(); }
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name)
        {
            throw new NotImplementedException();
        }

        public ICliMetadataTypeDefinitionTableRow FindType(IGeneralTypeUniqueIdentifier uniqueIdentifier)
        {
            throw new NotImplementedException();
        }

        protected override IAssemblyUniqueIdentifier OnGetUniqueIdentifier()
        {
            return base.OnGetUniqueIdentifier();
        }

        protected IntermediateCliAssembly(TAssembly rootAssembly) :
            base(rootAssembly)
        {
        }

        protected IntermediateCliAssembly(string name, IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment)
            : base(name)
        {
            this.runtimeEnvironment = runtimeEnvironment;
        }

    }
}
