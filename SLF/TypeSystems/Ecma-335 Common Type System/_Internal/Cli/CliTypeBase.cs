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
using AllenCopeland.Abstraction.Slf.Cli.Metadata;

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
        private ILockedTypeCollection implInterfaces;
        internal CliTypeBase(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadataEntry)
        {
            this.assembly = assembly;
            this.metadataEntry = metadataEntry;
        }

        protected override ITypeParent OnGetParent()
        {
            if (this.MetadataEntry.DeclaringType == null)
                return this.Namespace;
            else
                return (ITypeParent)this.assembly.IdentityManager.ObtainTypeReference(this.MetadataEntry.DeclaringType);
        }

        internal CliManager IdentityManager { get { return (CliManager)base.IdentityManager; } }

        protected override bool CanCacheImplementsList
        {
            get { return true; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return implInterfaces ?? (implInterfaces = this.InitializeImplementedInterfaces());
        }

        private ILockedTypeCollection InitializeImplementedInterfaces()
        {
            if (this.metadataEntry.ImplementedInterfaces == null)
                return new LockedTypeCollection();
            return new LockedTypeCollection(from interfaceImplRow in this.metadataEntry.ImplementedInterfaces
                                            select this.IdentityManager.ObtainTypeReference(interfaceImplRow.Interface, this, null));
        }

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
        {
            if (this.metadataEntry.ImplementedInterfaces == null)
                return new LockedTypeCollection();
            var implInterfaces = from interfaceImplRow in this.metadataEntry.ImplementedInterfaces
                                 select this.IdentityManager.ObtainTypeReference(interfaceImplRow.Interface, this, null);
            var subInterfaces = (from interfaceImpl in implInterfaces
                                 from subInterfaceImpl in interfaceImpl.ImplementedInterfaces
                                 select subInterfaceImpl).Distinct();
            var baseInterfaces = (this.BaseType == null ? (IEnumerable<IType>)new IType[0] : this.BaseType.ImplementedInterfaces);
            return new LockedTypeCollection(implInterfaces.Except(subInterfaces).Except(baseInterfaces));
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
                if (this.metadataEntry.TypeParameters == null)
                    return false;
                return this.MetadataEntry.TypeParameters.Count > 0;
            }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return false;
        }

        protected override string OnGetNamespaceName()
        {
            return this.metadataEntry.Namespace;
        }

        protected override IType BaseTypeImpl
        {
            get
            {
                var nullRef = (this.metadataEntry.ExtendsSource == CliMetadataTypeDefOrRefTag.TypeDefinition) && this.metadataEntry.ExtendsIndex == 0 ? true : false;
                if (nullRef)
                    return null;
                switch (this.metadataEntry.ExtendsSource)
                {
                    case AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag.TypeDefinition:
                    case AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag.TypeReference:
                        return this.IdentityManager.ObtainTypeReference(this.metadataEntry.Extends);
                    case AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag.TypeSpecification:
                        return this.IdentityManager.ObtainTypeReference((ICliMetadataTypeSpecificationTableRow)this.metadataEntry.Extends, this, null);
                }
                throw new InvalidOperationException();
            }
        }

        protected override IMetadataCollection InitializeCustomAttributes()
        {
            return new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this.IdentityManager);
        }


        protected override ITypeIdentityManager OnGetManager()
        {
            return this.assembly.IdentityManager;
        }

        protected override string OnGetName()
        {
            //if (this.metadata.Parent!=null)
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
