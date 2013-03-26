using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CliIndexerSignatureMember<TIndexer, TIndexerParent> :
        CliMemberBase<IGeneralSignatureMemberUniqueIdentifier, TIndexerParent, ICliMetadataPropertyTableRow>,
        IIndexerSignatureMember<TIndexer, TIndexerParent>,
        _ICliParameterParent
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IType,
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {
        private IPropertySignatureMethodMember getMethod;
        private IPropertySignatureMethodMember setMethod;
        private IParameterMemberDictionary<TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>> parameters;
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        private IMetadataCollection metadata;
        protected CliIndexerSignatureMember(TIndexerParent parent, ICliMetadataPropertyTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(parent, metadataEntry)
        {
            this.uniqueIdentifier = uniqueIdentifier;
        }

        protected override string OnGetName()
        {
            return this.MetadataEntry.Name;
        }

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                return this.uniqueIdentifier;
            }
        }

        public IPropertySignatureMethodMember GetMethod
        {
            get {
                if (!this.CanRead)
                    return null;
                if (this.getMethod == null)
                    this.getMethod = this.GetIndexerMethod(PropertyMethodType.GetMethod);
                return this.getMethod;
            }
        }

        protected abstract IPropertySignatureMethodMember GetIndexerMethod(PropertyMethodType propertyMethodType);

        public IPropertySignatureMethodMember SetMethod
        {
            get
            {
                if (!this.CanWrite)
                    return null;
                if (this.setMethod == null)
                    this.setMethod = this.GetIndexerMethod(PropertyMethodType.SetMethod);
                return this.setMethod;
            }
        }

        public IType PropertyType
        {
            get {
                if (this.CanRead)
                    return this.GetMethod.ReturnType;
                else if (this.CanWrite)
                    return ((IParameterMember)this.SetMethod.Parameters[this.SetMethod.Parameters.Count - 1]).ParameterType;
                else
                    throw new NotSupportedException();
            }
        }

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
            get {
                if (this.metadata == null)
                    this.metadata = new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, (_ICliManager)this.Parent.IdentityManager);
                return this.metadata;
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return Metadata.Contains(metadatumType);
        }

        public AccessLevelModifiers AccessLevel
        {
            get 
            {
                return ObtainAccessLevelModifiers(from semantics in this.MetadataEntry.Methods
                                                  select semantics.Method);
            }
        }

        private static AccessLevelModifiers ObtainAccessLevelModifiers(IEnumerable<ICliMetadataMethodDefinitionTableRow> methods)
        {
            AccessLevelModifiers resultModifiers = AccessLevelModifiers.PrivateScope;
            foreach (var method in methods)
            {
                AccessLevelModifiers currentModifiers;
                switch (method.UsageDetails.Accessibility)
                {
                    case MethodMemberAccessibility.Private:
                        currentModifiers = AccessLevelModifiers.Private;
                        break;
                    case MethodMemberAccessibility.FamilyAndAssembly:
                        currentModifiers = AccessLevelModifiers.ProtectedAndInternal;
                        break;
                    case MethodMemberAccessibility.Assembly:
                        currentModifiers = AccessLevelModifiers.Internal;
                        break;
                    case MethodMemberAccessibility.Family:
                        currentModifiers = AccessLevelModifiers.Protected;
                        break;
                    case MethodMemberAccessibility.FamilyOrAssembly:
                        currentModifiers = AccessLevelModifiers.ProtectedOrInternal;
                        break;
                    case MethodMemberAccessibility.Public:
                        currentModifiers = AccessLevelModifiers.Public;
                        break;
                    default:
                        currentModifiers = AccessLevelModifiers.PrivateScope;
                        break;
                }
                if (resultModifiers.CompareTo(currentModifiers) < 0)
                    resultModifiers = currentModifiers;
            }
            return resultModifiers;
        }

        public abstract bool LastIsParams { get; }

        public IParameterMemberDictionary<TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>> Parameters
        {
            get {
                if (this.parameters == null)
                    this.parameters = this.InitializeParameters();
                return this.parameters;
            }
        }

        protected IParameterMemberDictionary<TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>> InitializeParameters()
        {
            if (!(CanRead || this.CanWrite))
                throw new InvalidOperationException();
            bool fromWrite = ((!this.CanRead) && this.CanWrite);
            return new ParameterDictionary((_ICliManager)this.Parent.IdentityManager, (fromWrite ? this.MetadataEntry.SetMethod : this.MetadataEntry.GetMethod).Index, this.MetadataEntry.MetadataRoot, (TIndexer)(object)this, fromWrite);

        }

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return (IParameterMemberDictionary)this.Parameters; }
        }

        public _ICliManager IdentityManager
        {
            get { return (_ICliManager)this.Parent.IdentityManager; }
        }

        public ICliMetadataMethodSignature Signature
        {
            get {
                if (!(CanRead || this.CanWrite))
                    throw new InvalidOperationException();
                bool fromWrite = ((!this.CanRead) && this.CanWrite);
                return (fromWrite ? this.MetadataEntry.SetMethod : this.MetadataEntry.GetMethod).Signature;
            }
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
            return string.Format("indexer {0}::{1}", this.Parent, this.UniqueIdentifier);
        }
    }
}
