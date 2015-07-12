using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal partial class CliConstructorMember<TCtor, TCtorParent> :
        CliMemberBase<IGeneralSignatureMemberUniqueIdentifier, TCtorParent, ICliMetadataMethodDefinitionTableRow>,
        IConstructorMember<TCtor, TCtorParent>,
        _ICliParameterParent
        where TCtor :
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent : 
            IType,
            ICreatableParent<TCtor, TCtorParent>
    {
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        private _ICliManager identityManager;
        private ParameterDictionary parameters;
        private CliMetadataCollection metadata;
        protected CliConstructorMember(TCtorParent parent, ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliManager identityManager, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(parent, metadataEntry)
        {
            this.identityManager = identityManager;
            this.uniqueIdentifier = uniqueIdentifier;
        }

        protected override string OnGetName()
        {
            return this.MetadataEntry.Name;
        }

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get { return this.uniqueIdentifier; }
        }

        public IParameterMemberDictionary<TCtor, IConstructorParameterMember<TCtor, TCtorParent>> Parameters
        {
            get {
                if (this.parameters == null)
                    this.parameters = new ParameterDictionary(this.identityManager, this.MetadataEntry.Index, this.MetadataEntry.MetadataRoot, (TCtor)(object)this);
                return this.parameters;
            }
        }

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return (IParameterMemberDictionary)this.Parameters; }
        }

        public bool LastIsParams
        {
            get {
                if (this.Signature.Parameters.Count == 0)
                    return false;
                return this.Parameters.Values.Last().IsDefined(this.IdentityManager.ObtainTypeReference(this.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.ParamArrayMetadatum), this.Assembly));
            }
        }

        public AccessLevelModifiers AccessLevel
        {
            get {
                switch (this.MetadataEntry.UsageDetails.Accessibility)
                {
                    case MethodMemberAccessibility.Private:
                        return AccessLevelModifiers.Private;
                    case MethodMemberAccessibility.FamilyAndAssembly:
                        return AccessLevelModifiers.ProtectedAndInternal;
                    case MethodMemberAccessibility.Assembly:
                        return AccessLevelModifiers.Internal;
                    case MethodMemberAccessibility.Family:
                        return AccessLevelModifiers.Protected;
                    case MethodMemberAccessibility.FamilyOrAssembly:
                        return AccessLevelModifiers.ProtectedOrInternal;
                    case MethodMemberAccessibility.Public:
                        return AccessLevelModifiers.Public;
                    default:
                        return AccessLevelModifiers.PrivateScope;
                }
            }
        }

        public _ICliManager IdentityManager
        {
            get { return this.identityManager; }
        }

        public ICliMetadataMethodSignature Signature
        {
            get { return this.MetadataEntry.Signature; }
        }

        public ICliAssembly Assembly
        {
            get { return (ICliAssembly)this.Parent.Assembly; }
        }

        ICliMetadataTableRow ICliDeclaration.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }

        public override string ToString()
        {
            return string.Format("constructor {0}::{1}", this.Parent, this.UniqueIdentifier);
        }

        #region IMetadataEntity Members

        public IMetadataCollection Metadata
        {
            get
            {
                if (this.metadata == null)
                    this.metadata = new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this.IdentityManager);
                return this.metadata;
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return this.Metadata.Contains(metadatumType);
        }

        #endregion

    }
}
