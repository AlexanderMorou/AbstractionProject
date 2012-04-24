using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract class CliTypeBase<TIdentifier> :
        TypeBase<TIdentifier>,
        ICliType
        where TIdentifier :
            ITypeUniqueIdentifier
    {
        private CliAssembly assembly;
        private ICliMetadataTypeDefinitionTableRow metadata;

        internal CliTypeBase(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadata)
        {
            this.assembly = assembly;
            this.metadata = metadata;
        }

        protected override IType OnGetDeclaringType()
        {
            return this.assembly.IdentityManager.ObtainTypeReference(this.DeclaringType);
        }

        protected override bool CanCacheImplementsList
        {
            get { return true; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            throw new NotImplementedException();
        }

        protected override IFullMemberDictionary OnGetMembers()
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

        protected override IArrayType OnMakeArray(int rank)
        {
            throw new NotImplementedException();
        }

        protected override IArrayType OnMakeArray(params int[] lowerBounds)
        {
            throw new NotImplementedException();
        }

        protected override IType OnMakeByReference()
        {
            throw new NotImplementedException();
        }

        protected override IType OnMakePointer()
        {
            throw new NotImplementedException();
        }

        protected override IType OnMakeNullable()
        {
            throw new NotImplementedException();
        }

        public override bool IsGenericConstruct
        {
            get { throw new NotImplementedException(); }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            throw new NotImplementedException();
        }

        protected override string OnGetNamespaceName()
        {
            return this.metadata.Namespace;
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

            return this.metadata.Name;
        }

        #region ICliType Members

        public new ICliAssembly Assembly
        {
            get { return this.assembly; }
        }

        public ICliMetadataTypeDefinitionTableRow Metadata
        {
            get { return this.metadata; }
        }

        #endregion

        #region ICliDeclaration Members

        ICliMetadataTableRow ICliDeclaration.Metadata
        {
            get { return this.Metadata; }
        }

        #endregion
    }
}
