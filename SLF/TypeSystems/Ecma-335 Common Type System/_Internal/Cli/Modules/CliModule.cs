using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Modules
{
    internal class CliModule :
        ModuleBase,
        ICliModule
    {
        internal CliModule(CliAssembly owner, ICliMetadataModuleTableRow metadata)
            : base(owner)
        {
            this.Metadata = metadata;
        }

        #region ICliModule Members

        public ICliMetadataModuleTableRow Metadata { get; private set; }

        public ICliAssembly Parent { get { return (ICliAssembly) base.Parent; } }

        #endregion

        protected override IModuleGlobalFields InitializeFields()
        {
            throw new NotImplementedException();
        }

        protected override IModuleGlobalMethods InitializeMethods()
        {
            throw new NotImplementedException();
        }

        protected override string OnGetName()
        {
            return this.Metadata.Name;
        }

        public override IGeneralDeclarationUniqueIdentifier UniqueIdentifier
        {
            get
            {
                return AstIdentifier.Declaration(this.Metadata.Name);
            }
        }
    }
}
