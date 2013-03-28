using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliParameterMember<TParent, TCliParent> :
        IParameterMember<TParent>,
        ICliMetadataMember
        where TParent :
            IParameterParent
        where TCliParent :
            IParameterParent,
            _ICliParameterParent
    {
        private TCliParent parent;
        private ICliMetadataParameterTableRow metadataEntry;
        private object syncObject = new object();
        private int index;
        private IMetadataCollection metadata;
        private IType parameterType;

        public CliParameterMember(ICliMetadataParameterTableRow metadataEntry, TCliParent parent, int index)
        {
            this.parent = parent;
            this.metadataEntry = metadataEntry;
            this.index = index;
        }

        public TCliParent Parent { get { return this.parent; } }

        #region IParameterMember Members

        IParameterParent IParameterMember.Parent
        {
            get { return this.Parent; }
        }

        public IType ParameterType
        {
            get {
                if (this.parameterType == null)
                {
                    var sigParameter = this.parent.Signature.Parameters[this.index];
                    if (sigParameter.CustomModifiers.Count > 0)
                        this.parameterType = this.parent.IdentityManager.ObtainTypeReference(sigParameter.ParameterType).MakeModified(sigParameter.CustomModifiers.Resolve(this.parent.IdentityManager).ToArray());
                    else
                        this.parameterType = this.parent.IdentityManager.ObtainTypeReference(sigParameter.ParameterType, this.ActiveType,this.ActiveMethod);
                }
                return this.parameterType;
            }
        }

        public ParameterCoercionDirection Direction
        {
            get {
                if ((this.metadataEntry.Flags & ParameterAttributes.Out) == ParameterAttributes.Out)
                    return ParameterCoercionDirection.Out;
                else if (this.ParameterType.ElementClassification == TypeElementClassification.Reference)
                    return ParameterCoercionDirection.Reference;
                return ParameterCoercionDirection.In;
            }
        }

        #endregion

        #region IMember Members

        IMemberParent IMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IDeclaration Members

        public string Name
        {
            get { return this.metadataEntry.Name; }
        }

        IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
        {
            get { return this.UniqueIdentifier; }
        }

        public event EventHandler Disposed;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            lock (syncObject)
            {
                var dispCopy = this.Disposed;
                this.Disposed = null;
                if (dispCopy != null)
                    dispCopy(this, EventArgs.Empty);
            }
        }

        #endregion

        #region IMetadataEntity Members

        public IMetadataCollection Metadata
        {
            get {
                if (this.metadata == null)
                    this.metadata = new CliMetadataCollection(this.metadataEntry.CustomAttributes, this, this.Parent.IdentityManager);
                return this.metadata;
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return this.Metadata.Contains(metadatumType);
        }

        #endregion

        #region IMember<IGeneralMemberUniqueIdentifier,TParent> Members

        TParent IMember<IGeneralMemberUniqueIdentifier,TParent>.Parent
        {
            get { return (TParent)(object)this.Parent; }
        }

        #endregion

        #region IDeclaration<IGeneralMemberUniqueIdentifier> Members

        public IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get { return TypeSystemIdentifiers.GetMemberIdentifier(this.Name); }
        }

        #endregion

        public ICliMetadataTableRow MetadataEntry
        {
            get { return this.metadataEntry; }
        }

        protected abstract IMethodSignatureMember ActiveMethod { get; }

        protected abstract IType ActiveType { get; }
    }
}
