using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract class CliTypeBase<TIdentifier> :
        TypeBase<TIdentifier>,
        ICliType
        where TIdentifier :
            ITypeUniqueIdentifier
    {
        private CliAssembly assembly;
        private ICliMetadataTypeDefinitionTableRow metadataEntry;

        internal CliTypeBase(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadataEntry)
        {
            this.assembly = assembly;
            this.metadataEntry = metadataEntry;
        }

        protected override IType OnGetDeclaringType()
        {
            if (this.MetadataEntry.DeclaringType == null)
                return null;
            else
                return this.assembly.IdentityManager.ObtainTypeReference(this.MetadataEntry.DeclaringType);
        }

        protected internal override bool CanCacheImplementsList
        {
            get { return true; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            throw new NotImplementedException();
        }

        protected override INamespaceDeclaration OnGetNamespace()
        {
            return this.assembly.GetNamespace(this.NamespaceName);
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            throw new NotImplementedException();
        }

        protected override IAssembly OnGetAssembly()
        {
            return this.assembly;
        }

        public override bool IsGenericConstruct
        {
            get
            {
                return this.MetadataEntry.TypeParameters.Count > 0;
            }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            throw new NotImplementedException();
        }

        protected override string OnGetNamespaceName()
        {
            return this.metadataEntry.Namespace;
        }

        protected override IType BaseTypeImpl
        {
            get { throw new NotImplementedException(); }
        }

        protected override TIdentifier OnGetUniqueIdentifier()
        {
            throw new NotImplementedException();
        }

        protected override IMetadataCollection InitializeCustomAttributes()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { throw new NotImplementedException(); }
        }

        protected override ITypeIdentityManager OnGetManager()
        {
            return this.assembly.IdentityManager;
        }

        protected override string OnGetName()
        {
            //if (this.metadata.DeclaringType!=null)
            //    if (this.metadata.NamespaceIndex > 0)
            //        return this.metadata.Name
            return this.UniqueIdentifier.Name;
        }

        #region ICliType Members

        public new ICliAssembly Assembly
        {
            get { return this.assembly; }
        }

        public ICliMetadataTypeDefinitionTableRow MetadataEntry
        {
            get { return this.metadataEntry; }
        }

        #endregion

        #region ICliDeclaration Members

        ICliMetadataTableRow ICliDeclaration.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }

        #endregion
    }
}
