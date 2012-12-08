using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliDelegateType :
        CliGenericTypeBase<IGeneralGenericTypeUniqueIdentifier, IDelegateType>,
        _ICliParameterParent,
        ICliDelegateType
    {
        private uint? invokeMethodIndex;
        private uint? beginInvokeMethodIndex;
        private uint? endInvokeMethodIndex;
        private ParameterDictionary parameters;
        internal CliDelegateType(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadataEntry)
            : base(assembly, metadataEntry)
        {
        }

        private CliAssembly _Assembly { get { return (CliAssembly)base.Assembly; } }

        private CliManager IdentityManager { get { return this._Assembly.IdentityManager; } }

        protected override IDelegateType OnMakeGenericClosure(LockedTypeCollection lockedTypeParameters)
        {
            return new _DelegateTypeBase(this, lockedTypeParameters);
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Delegate; }
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            return LockedFullMembersBase.Empty;
        }

        private ICliMetadataMethodDefinitionTableRow SignatureMetadataEntry
        {
            get
            {
                if ((((object)this.MetadataEntry) ?? (object)this.MetadataEntry.MetadataRoot ?? (object)this.MetadataEntry.MetadataRoot.TableStream) == null)
                    throw new InvalidOperationException();
                return this.MetadataEntry.MetadataRoot.TableStream.MethodDefinitionTable[(int)this.InvokeMethodIndex];
            }
        }

        public IType ReturnType
        {
            get
            {
                var method = this.SignatureMetadataEntry;
                if (method == null)
                    throw new InvalidOperationException();
                return this._Assembly.IdentityManager.ObtainTypeReference(method.Signature.ReturnType, this, null);
            }
        }

        public IDelegateTypeParameterDictionary Parameters
        {
            get
            {
                if (this.parameters == null)
                    this.parameters = new ParameterDictionary(this);
                return this.parameters;
            }
        }

        IParameterMemberDictionary<IDelegateType, IDelegateTypeParameterMember> IParameterParent<IDelegateType, IDelegateTypeParameterMember>.Parameters
        {
            get { return this.Parameters; }
        }

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return (IParameterMemberDictionary)this.Parameters; }
        }

        public bool LastIsParams
        {
            get
            {
                return this.IsLastParams(this.Assembly, this._Assembly.IdentityManager);
            }
        }

        public uint InvokeMethodIndex
        {
            get
            {
                if (this.invokeMethodIndex == null)
                    this.invokeMethodIndex = this.MetadataEntry.Methods.First(invokeMethod => invokeMethod.Name == "Invoke").Index;
                return this.invokeMethodIndex.Value;
            }
        }

        public uint BeginInvokeMethodIndex
        {
            get
            {
                if (this.beginInvokeMethodIndex == null)
                    this.beginInvokeMethodIndex = this.MetadataEntry.Methods.First(beginInvokeMethod => beginInvokeMethod.Name == "BeginInvoke").Index;
                return this.beginInvokeMethodIndex.Value;
            }
        }

        public uint EndInvokeMethodIndex
        {
            get
            {
                if (this.endInvokeMethodIndex == null)
                    this.endInvokeMethodIndex = this.MetadataEntry.Methods.First(endInvokeMethod => endInvokeMethod.Name == "EndInvoke").Index;
                return this.endInvokeMethodIndex.Value;
            }
        }


        _ICliManager _ICliParameterParent.IdentityManager
        {
            get { return this.IdentityManager; }
        }

        public ICliMetadataMethodSignature Signature
        {
            get { return this.SignatureMetadataEntry.Signature; }
        }

        ICliMetadataTableRow ICliDeclaration.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }


    }
}
