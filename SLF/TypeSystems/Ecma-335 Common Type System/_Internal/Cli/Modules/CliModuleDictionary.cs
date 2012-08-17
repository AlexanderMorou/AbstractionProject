using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Modules
{
    internal class CliModuleDictionary :
        CliMetadataDrivenDictionary<IGeneralDeclarationUniqueIdentifier, ICliMetadataModuleTableRow, IModule>,
        IModuleDictionary
    {
        private CliAssembly owner;
        private ICliMetadataModuleReferenceTableRow[] modules;

        public CliModuleDictionary(CliAssembly owner)
            : base()
        {
            this.modules = GetReferenceModules(owner);
            base.Initialize(this.modules.Length + 1);
            this.owner = owner;
        }

        private static ICliMetadataModuleReferenceTableRow[] GetReferenceModules(CliAssembly owner)
        {
            var mrTable = owner.MetadataRoot.TableStream.ModuleReferenceTable;
            var fTable = owner.MetadataRoot.TableStream.FileTable;
            if (mrTable != null && fTable != null)
            {
                mrTable.Read();
                fTable.Read();
                var mtFQuery = from moduleRef in mrTable
                               join file in fTable on moduleRef.NameIndex equals file.NameIndex
                               select moduleRef;
                return mtFQuery.ToArray();
            }
            return new ICliMetadataModuleReferenceTableRow[0];
        }

        private static int GetModuleCount(CliAssembly owner)
        {
            if (owner.MetadataRoot.TableStream.ModuleReferenceTable != null)
                return GetReferenceModules(owner).Count() + 1;
            return 1;
        }

        protected override ICliMetadataModuleTableRow GetMetadataAt(int index)
        {
            if (index == 0)
            {
                if (this.owner.MetadataRoot.TableStream.ModuleTable == null)
                    return null;
                return this.owner.MetadataRoot.TableStream.ModuleTable[1];
            }
            else
            {
                if (modules == null)
                    modules = GetReferenceModules(this.owner);
                if (index < 0 || index >= modules.Length + 1)
                    throw new ArgumentOutOfRangeException("index");
                return this.owner.IdentityManager.LoadModule(this.modules[index - 1]);
            }
        }

        protected override IModule CreateElementFrom(int index, ICliMetadataModuleTableRow metadata)
        {
            return new CliModule(this.owner, metadata);
        }

        #region IModuleDictionary Members

        public IAssembly Parent
        {
            get { return this.owner; }
        }

        #endregion

        protected override IGeneralDeclarationUniqueIdentifier GetIdentifierFrom(int index, ICliMetadataModuleTableRow metadata)
        {
            return AstIdentifier.GetDeclarationIdentifier(metadata.Name);
        }
    }
}
