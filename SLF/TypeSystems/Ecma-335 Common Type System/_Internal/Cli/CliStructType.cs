﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliStructType :
        CliGenericInstantiableTypeBase<IStructCtorMember, IStructEventMember, IStructFieldMember,
             IStructIndexerMember, IStructMethodMember, IStructPropertyMember, IStructType>,
        IStructType
    {
        internal CliStructType(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadata)
            : base(assembly, metadata)
        {

        }

        protected override IStructCtorMember GetConstructor(ICliMetadataMethodDefinitionTableRow metadataEntry)
        {
            throw new NotImplementedException();
        }

        protected override IStructEventMember GetEvent(ICliMetadataEventTableRow metadataEntry)
        {
            throw new NotImplementedException();
        }

        protected override IStructFieldMember GetField(ICliMetadataFieldTableRow metadataEntry)
        {
            return new FieldMember(this, metadataEntry);
        }

        protected override IStructIndexerMember GetIndexer(ICliMetadataPropertyTableRow metadataEntry)
        {
            throw new NotImplementedException();
        }

        protected override IStructMethodMember GetMethod(ICliMetadataMethodDefinitionTableRow metadataEntry)
        {
            throw new NotImplementedException();
        }

        protected override IStructPropertyMember GetProperty(ICliMetadataPropertyTableRow metadataEntry)
        {
            throw new NotImplementedException();
        }

        protected override IStructType OnMakeGenericClosure(LockedTypeCollection lockedTypeParameters)
        {
            throw new NotImplementedException();
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Struct; }
        }

        public new IStructInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

    }
}