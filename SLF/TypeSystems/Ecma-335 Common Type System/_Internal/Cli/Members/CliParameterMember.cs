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
        private ICliMetadataParameterTableRow metadata;
        private object syncObject = new object();
        private int index;

        private IType parameterType;

        public CliParameterMember(ICliMetadataParameterTableRow metadata, TCliParent parent, int index)
        {
            this.parent = parent;
            this.metadata = metadata;
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

        public ParameterDirection Direction
        {
            get {
                if ((this.metadata.Flags & ParameterAttributes.Out) == ParameterAttributes.Out)
                    return ParameterDirection.Out;
                else if (this.ParameterType.ElementClassification == TypeElementClassification.Reference)
                    return ParameterDirection.Reference;
                return ParameterDirection.In;
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
            get { return this.metadata.Name; }
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
            get { throw new NotImplementedException(); }
        }

        public bool IsDefined(IType metadatumType)
        {
            throw new NotImplementedException();
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
            get { return AstIdentifier.GetMemberIdentifier(this.Name); }
        }

        #endregion

        public ICliMetadataTableRow MetadataEntry
        {
            get { return this.metadata; }
        }

        protected abstract IMethodSignatureMember ActiveMethod { get; }

        protected abstract IType ActiveType { get; }
    }
}
