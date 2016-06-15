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
    internal abstract partial class CliIndexerMember<TIndexer, TIndexerParent> :
        CliMemberBase<IGeneralSignatureMemberUniqueIdentifier, TIndexerParent, ICliMetadataPropertyTableRow>,
        IIndexerMember<TIndexer, TIndexerParent>,
        _ICliParameterParent
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IType,
            IIndexerParent<TIndexer, TIndexerParent>
    {
        private IPropertyMethodMember getMethod;
        private IPropertyMethodMember setMethod;
        private IParameterMemberDictionary<TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>> parameters;
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        private IMetadataCollection metadata;
        protected CliIndexerMember(TIndexerParent parent, ICliMetadataPropertyTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
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

        public IPropertyMethodMember GetMethod
        {
            get {
                if (!this.CanRead)
                    return null;
                if (this.getMethod == null)
                    this.getMethod = this.GetIndexerMethod(PropertyMethodType.GetMethod);
                return this.getMethod;
            }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.GetMethod { get { return this.GetMethod; } }

        IPropertySignatureMethodMember IPropertySignatureMember.SetMethod { get { return this.SetMethod; } }


        protected abstract IPropertyMethodMember GetIndexerMethod(PropertyMethodType methodType);

        public IPropertyMethodMember SetMethod
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

        public IParameterMemberDictionary<TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>> Parameters
        {
            get {
                if (this.parameters == null)
                    this.parameters = this.InitializeParameters();
                return this.parameters;
            }
        }

        protected IParameterMemberDictionary<TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>> InitializeParameters()
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


        public IEnumerable<IInterfaceType> Implementations
        {
            get
            {
                var results = new HashSet<IInterfaceType>();
                AddImplementations(results, this.GetMethod);
                AddImplementations(results, this.SetMethod);
                foreach (var type in results)
                    yield return type;
            }
        }

        private static void AddImplementations(HashSet<IInterfaceType> results, IMethodMember aM)
        {
            if (aM != null && aM is IExtendedInstanceMember)
            {
                var ieimAM = (IExtendedInstanceMember)aM;
                foreach (var t in ieimAM.Implementations)
                    results.Add(t);
            }
        }

        public ExtendedMemberAttributes Attributes
        {
            get
            {
                var methodSemantics = this.MetadataEntry.Methods.FirstOrDefault();
                if (methodSemantics == null)
                    return ExtendedMemberAttributes.None;
                var method = methodSemantics.Method;
                if (method == null)
                    return ExtendedMemberAttributes.None;
                ExtendedMemberAttributes flags = ExtendedMemberAttributes.None;
                if ((method.UsageDetails.VTableFlags & MethodVTableLayoutFlags.NewSlot) == MethodVTableLayoutFlags.NewSlot)
                    flags |= ExtendedMemberAttributes.HideByName;
                if ((method.UsageDetails.UsageFlags & MethodUseFlags.Abstract) == MethodUseFlags.Abstract)
                    flags |= ExtendedMemberAttributes.Abstract;

                if ((method.UsageDetails.UsageFlags & MethodUseFlags.Final) == MethodUseFlags.Final)
                    flags |= ExtendedMemberAttributes.Final;
                if ((method.UsageDetails.UsageFlags & MethodUseFlags.Static) == MethodUseFlags.Static)
                    flags |= ExtendedMemberAttributes.Static;
                if ((method.UsageDetails.UsageFlags & MethodUseFlags.Virtual) == MethodUseFlags.Virtual)
                    flags |= ExtendedMemberAttributes.Virtual;
                if (method.UsageDetails.VTableFlags == MethodVTableLayoutFlags.ReuseSlot)
                    flags |= ExtendedMemberAttributes.Override;
                return flags;
            }
        }

        public bool IsAbstract
        {
            get { return (Attributes & ExtendedMemberAttributes.Abstract) == ExtendedMemberAttributes.Abstract; }
        }

        public bool IsVirtual
        {
            get { return (Attributes & ExtendedMemberAttributes.Virtual) == ExtendedMemberAttributes.Virtual; }
        }

        public bool IsFinal
        {
            get { return (Attributes & ExtendedMemberAttributes.Final) == ExtendedMemberAttributes.Final; }
        }

        public bool IsOverride
        {
            get { return (Attributes & ExtendedMemberAttributes.Override) == ExtendedMemberAttributes.Override; }
        }

        InstanceMemberAttributes IInstanceMember.Attributes
        {
            get { return (InstanceMemberAttributes)this.Attributes & InstanceMemberAttributes.FlagsMask; }
        }

        public bool IsHideBySignature
        {
            get { return (this.Attributes & ExtendedMemberAttributes.HideBySignature) == ExtendedMemberAttributes.HideBySignature; }
        }

        public bool IsStatic
        {
            get { return (this.Attributes & ExtendedMemberAttributes.Static) == ExtendedMemberAttributes.Static; }
        }

        public override string ToString()
        {
            return string.Format("indexer {0}::{1}", this.Parent, this.uniqueIdentifier);
        }

        public bool HideByName
        {
            get { return (this.Attributes & ExtendedMemberAttributes.HideByName) == ExtendedMemberAttributes.HideByName; }
        }
    }
}
