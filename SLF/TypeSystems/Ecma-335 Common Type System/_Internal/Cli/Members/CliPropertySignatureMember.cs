﻿using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliPropertySignatureMember<TProperty, TPropertyParent> :
        CliMemberBase<IGeneralMemberUniqueIdentifier, TPropertyParent, ICliMetadataPropertyTableRow>,
        IPropertySignatureMember<TProperty, TPropertyParent>
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParent<TProperty, TPropertyParent>
    {
        private IPropertySignatureMethodMember getMethod;
        private IPropertySignatureMethodMember setMethod;
        protected CliPropertySignatureMember(TPropertyParent parent, ICliMetadataPropertyTableRow metadataEntry)
            : base(parent, metadataEntry)
        {
        }

        protected override string OnGetName()
        {
            return this.MetadataEntry.Name;
        }

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get { return AstIdentifier.GetMemberIdentifier(this.Name); }
        }

        public IPropertySignatureMethodMember GetMethod
        {
            get {
                if (this.getMethod == null)
                    return this.GetPropertyMethod(PropertyMethodType.GetMethod);
                return this.getMethod;
            }
        }

        protected abstract IPropertySignatureMethodMember GetPropertyMethod(PropertyMethodType propertyMethodType);

        public IPropertySignatureMethodMember SetMethod
        {
            get
            {
                if (this.setMethod == null)
                    return this.GetPropertyMethod(PropertyMethodType.SetMethod);
                return this.setMethod;
            }
        }

        public IType PropertyType
        {
            get {
                return this.OnGetPropertyType();
            }
        }

        protected abstract IType OnGetPropertyType();

        public bool CanRead
        {
            get { return this.MetadataEntry.GetMethod != null; }
        }

        public bool CanWrite
        {
            get { return this.MetadataEntry.SetMethod != null; }
        }

        public IMetadataCollection Metadata
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsDefined(IType metadatumType)
        {
            return Metadata.Contains(metadatumType);
        }

        public AccessLevelModifiers AccessLevel
        {
            get 
            {
                return this.MetadataEntry.Methods.ObtainAccessLevelModifiers();
            }
        }

    }
}