using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliMethodSignatureBase<TSignature, TSignatureParent> :
        CliMethodSignatureBase<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>,
        _IGenericMethodSignatureRegistrar
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {
        /// <summary>
        /// Data member for maintaining a single-ton view of the generic
        /// closures of the generic series.
        /// </summary>
        private GenericMethodSignatureCache<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent> genericCache;

        protected CliMethodSignatureBase(ICliMetadataMethodDefinitionTableRow metadata, _ICliAssembly assembly, TSignatureParent parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(metadata, assembly, parent, uniqueIdentifier)
        {
        }

        protected override CliParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>> InitializeParameters()
        {
            return new ParameterMemberDictionary(this);
        }

        protected abstract IMethodSignatureParameterMember<TSignature, TSignatureParent> CreateParameter(int index, ICliMetadataParameterTableRow metadataEntry);

        internal class ParameterMemberDictionary :
            CliParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>>
        {
            private new CliMethodSignatureBase<TSignature, TSignatureParent> Parent { get { return ((CliMethodSignatureBase<TSignature, TSignatureParent>) (object) base.Parent); } }

            public ParameterMemberDictionary(CliMethodSignatureBase<TSignature, TSignatureParent> signature)
                : base(signature.IdentityManager, signature.MetadataEntry.Index, signature.MetadataEntry.MetadataRoot, (TSignature)(object)signature, signature)
            {
            }

            protected override IMethodSignatureParameterMember<TSignature, TSignatureParent> CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return this.Parent.CreateParameter(index, metadata);
            }
        }

        internal abstract class ParameterMember :
            CliParameterMember<TSignature, CliMethodSignatureBase<TSignature, TSignatureParent>>,
            IMethodSignatureParameterMember<TSignature, TSignatureParent>
        {
            internal ParameterMember(ICliMetadataParameterTableRow metadata, CliMethodSignatureBase<TSignature, TSignatureParent> parent, int index)
                : base(metadata, parent, index)
            {
            }

            #region ISignatureParameterMember Members

            ISignatureMember ISignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            #region IMethodSignatureParameterMember Members

            IMethodSignatureMember IMethodSignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            protected override IMethodSignatureMember ActiveMethod
            {
                get { return this.Parent; }
            }
            
            public override string ToString()
            {
                return this.UniqueIdentifier.ToString();
            }
        }


        private void CheckGenericCache()
        {
            if (this.genericCache == null)
                this.genericCache = new GenericMethodSignatureCache<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>();
        }


        #region _IGenericMethodSignatureRegistrar Members

        public void RegisterGenericChild(IMethodSignatureParent parent, IMethodSignatureMember genericChild)
        {
            this.CheckGenericCache();
            this.genericCache.RegisterGenericChild(parent, genericChild);
        }

        public void UnregisterGenericChild(IMethodSignatureParent parent)
        {
            this.CheckGenericCache();
            this.genericCache.UnregisterGenericChild(parent);
        }

        public void RegisterGenericMethod(IMethodSignatureMember targetSignature, IControlledTypeCollection typeParameters)
        {
            this.CheckGenericCache();
            this.genericCache.RegisterGenericMethod(targetSignature, typeParameters);
        }

        public void UnregisterGenericMethod(IControlledTypeCollection typeParameters)
        {
            this.CheckGenericCache();
            this.genericCache.UnregisterGenericMethod(typeParameters);
        }

        #endregion

        protected override bool ContainsGenericMethod(IControlledTypeCollection typeParameters, ref TSignature r)
        {
            this.CheckGenericCache();
            return this.genericCache.ContainsGenericMethod(typeParameters, ref r);
        }
    }

    internal partial class CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent>
    {

        internal class TypeParameterDictionary :
            CliGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>
        {
            public TypeParameterDictionary(CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent> parent)
                : base(parent.MetadataEntry.TypeParameters, parent) { }

            public new CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent> Parent { get { return (CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent>)base.Parent; } }

            protected override IGenericParameterUniqueIdentifier GetIdentifierFrom(int index, ICliMetadataGenericParameterTableRow metadata) { return TypeSystemIdentifiers.GetGenericParameterIdentifier(index, false); }

            protected override IMethodSignatureGenericTypeParameterMember CreateElementFrom(int index, ICliMetadataGenericParameterTableRow metadataEntry)
            {
                return this.Parent.GetTypeParameter(index, metadataEntry);
            }
        }

        internal abstract class TypeParameter :
            CliGenericParameterMember<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>,
            IMethodSignatureGenericTypeParameterMember
        {
            internal TypeParameter(IMethodSignatureMember parent, ICliMetadataGenericParameterTableRow metadataEntry, int position)
                : base(parent, metadataEntry, position)
            {
            }

            protected override IGenericParameterUniqueIdentifier OnGetUniqueIdentifier()
            {
                return TypeSystemIdentifiers.GetGenericParameterIdentifier((int)this.Position, false);
            }

        }
    }
}
