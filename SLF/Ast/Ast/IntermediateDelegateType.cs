using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Events;
using System.ComponentModel;

/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base class for delegate types.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Always)]
    public partial class IntermediateDelegateType :
        IntermediateGenericTypeBase<IGeneralGenericTypeUniqueIdentifier, IDelegateType, IIntermediateDelegateType>,
        IIntermediateDelegateType
    {
        private IntermediateFullMemberDictionary members;
        private ParameterDictionary parameters;
        private IType returnType;

        internal protected IntermediateDelegateType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
            this.returnType = this.Parent.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
        }

        public ParameterDictionary Parameters
        {
            get
            {
                this.CheckParameters();
                return this.parameters;
            }
        }

        #region IIntermediateDelegateType Members

        /// <summary>
        /// Returns the <see cref="IType"/> that the <see cref="IDelegateType"/> returns.
        /// </summary>
        public IType ReturnType
        {
            get
            {
                return this.returnType;
            }
            set
            {
                this.returnType = value;
            }
        }

        IIntermediateDelegateTypeParameterDictionary IIntermediateDelegateType.Parameters
        {
            get
            {
                return this.Parameters;
            }
        }

        #endregion

        #region IIntermediateParameterParent<IDelegateType,IIntermediateDelegateType,IDelegateTypeParameterMember,IIntermediateDelegateTypeParameterMember> Members

        IIntermediateParameterMemberDictionary<IDelegateType, IIntermediateDelegateType, IDelegateTypeParameterMember, IIntermediateDelegateTypeParameterMember> IIntermediateParameterParent<IDelegateType, IIntermediateDelegateType, IDelegateTypeParameterMember, IIntermediateDelegateTypeParameterMember>.Parameters
        {
            get
            {
                return this.Parameters;
            }
        }

        #endregion

        #region IIntermediateParameterParent Members

        IIntermediateParameterMemberDictionary IIntermediateParameterParent.Parameters
        {
            get
            {
                return this.Parameters;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateDelegateType"/>'s last parameter
        /// is a parameter array.
        /// </summary>
        public bool LastIsParams
        {
            get
            {
                if (this.parameters == null ||
                    this.parameters.Count == 0)
                    return false;
                var cliManager = Parent.IdentityManager as ICliManager;
                if (cliManager == null)
                    return false;
                else
                    return this.Parameters.Values[this.Parameters.Count - 1].IsDefined(cliManager.ObtainTypeReference(cliManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.ParamArrayMetadatum)));
            }
            set
            {
                if (value == this.LastIsParams)
                    return;
                if (this.parameters == null)
                    throw new InvalidOperationException("Cannot set last params on a delegate with zero parameters.");
                this.CheckParameters();
                var lastParameter = this.Parameters.Values[this.Parameters.Count - 1];
                var cliManager = Parent.IdentityManager as ICliManager;
                if (cliManager != null){
                    var paramArrayMetadatum = cliManager.ObtainTypeReference(cliManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.ParamArrayMetadatum));
                    if (value)
                        lastParameter.Metadata.Add(new MetadatumDefinitionParameterValueCollection(paramArrayMetadatum));
                    else
                    {
                        if (lastParameter.IsDefined(paramArrayMetadatum))
                        {
                            var customAttrDef = lastParameter.Metadata[paramArrayMetadatum];
                            lastParameter.Metadata.Remove(customAttrDef);
                        }
                    }}
            }
        }

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        #region IParameterParent<IDelegateType,IDelegateTypeParameterMember> Members

        IParameterMemberDictionary<IDelegateType, IDelegateTypeParameterMember> IParameterParent<IDelegateType, IDelegateTypeParameterMember>.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        #region IDelegateType Members

        IType IDelegateType.ReturnType
        {
            get { return this.ReturnType; }
        }

        IDelegateTypeParameterDictionary IDelegateType.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        private void CheckParameters()
        {
            if (this.parameters == null)
            {
                this.parameters = new ParameterDictionary(this);
                this.parameters.ItemAdded += new EventHandler<EventArgsR1<IIntermediateDelegateTypeParameterMember>>(parameters_ItemAdded);
                this.parameters.ItemRemoved += new EventHandler<EventArgsR1<IIntermediateDelegateTypeParameterMember>>(parameters_ItemRemoved);
            }
        }


        protected override IDelegateType OnMakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return new _DelegateTypeBase(this, typeParameters);
        }

        protected override IIntermediateFullMemberDictionary OnGetIntermediateMembers()
        {
            return this._Members;
        }

        protected override bool Equals(IDelegateType other)
        {
            return object.ReferenceEquals(other, this);
        }

        protected override TypeKind TypeImpl
        {
            get
            {
                return TypeKind.Delegate;
            }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        public override bool IsGenericConstruct
        {
            get
            {
                if (!base.TypeParametersInitialized)
                    return false;
                return this.TypeParameters.Count > 0;
            }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return this.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType).Equals(other)
#if TYPESYSTEM_CLI
 || this.IdentityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "Delegate", 0)).Equals(other)
                || this.IdentityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "MulticastDelegate", 0)).Equals(other);
#else
            ;
#endif
        }

        protected override IType BaseTypeImpl
        {
            get
            {
#if TYPESYSTEM_CLI
                return this.IdentityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "MulticastDelegate", 0));
#else
                throw new NotImplementedException();
#endif
            }
        }

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                Check_Members();
                return this.members;
            }
        }

        private void Check_Members()
        {
            if (this.members == null)
                this.members = new IntermediateFullMemberDictionary();
        }

        void parameters_ItemRemoved(object sender, EventArgsR1<IIntermediateDelegateTypeParameterMember> e)
        {
            this.OnParameterRemoved(e);
        }

        protected virtual void OnParameterAdded(EventArgsR1<IIntermediateDelegateTypeParameterMember> e)
        {
            var _parameterAdded = this._ParameterAdded;
            if (_parameterAdded != null)
                _parameterAdded(this, new EventArgsR1<IIntermediateParameterMember>(e.Arg1));
            var parameterAdded = this.ParameterAdded;
            if (parameterAdded != null)
                parameterAdded(this, new EventArgsR1<IIntermediateDelegateTypeParameterMember>(e.Arg1));
        }

        void parameters_ItemAdded(object sender, EventArgsR1<IIntermediateDelegateTypeParameterMember> e)
        {
            this.OnParameterAdded(e);
        }

        protected virtual void OnParameterRemoved(EventArgsR1<IIntermediateDelegateTypeParameterMember> e)
        {
            var _parameterRemoved = this._ParameterRemoved;
            if (_parameterRemoved != null)
                _parameterRemoved(this, new EventArgsR1<IIntermediateParameterMember>(e.Arg1));
            var parameterRemoved = this.ParameterRemoved;
            if (parameterRemoved != null)
                parameterRemoved(this, new EventArgsR1<IIntermediateDelegateTypeParameterMember>(e.Arg1));
        }


        #region IIntermediateParameterParent<TParent,TIntermediateParent,TParameter,IIntermediateDelegateTypeParameterMember> Members


        public event EventHandler<EventArgsR1<IIntermediateDelegateTypeParameterMember>> ParameterAdded;

        public event EventHandler<EventArgsR1<IIntermediateDelegateTypeParameterMember>> ParameterRemoved;

        #endregion

        #region IIntermediateParameterParent Members
        private EventHandler<EventArgsR1<IIntermediateParameterMember>> _ParameterAdded;
        private EventHandler<EventArgsR1<IIntermediateParameterMember>> _ParameterRemoved;
        private IGeneralGenericTypeUniqueIdentifier uniqueIdentifier;

        event EventHandler<EventArgsR1<IIntermediateParameterMember>> IIntermediateParameterParent.ParameterAdded
        {
            add { this._ParameterAdded += value; }
            remove { this._ParameterAdded -= value; }
        }

        event EventHandler<EventArgsR1<IIntermediateParameterMember>> IIntermediateParameterParent.ParameterRemoved
        {
            add { this._ParameterRemoved += value; }
            remove { this._ParameterRemoved -= value; }
        }

        #endregion
        public override void Dispose()
        {
            try
            {
                if (this.parameters != null)
                {
                    this.parameters.ItemAdded -= new EventHandler<EventArgsR1<IIntermediateDelegateTypeParameterMember>>(parameters_ItemAdded);
                    this.parameters.ItemRemoved -= new EventHandler<EventArgsR1<IIntermediateDelegateTypeParameterMember>>(parameters_ItemRemoved);
                    this.parameters.Dispose();
                    this.parameters = null;
                }
            }
            finally
            {
                base.Dispose();
            }
        }

        public override void Visit(IIntermediateTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateTypeVisitor"/> to
        /// receive the <see cref="IntermediateDelegateType"/> as a visitor.</param>
        public override TResult Visit<TResult>(IIntermediateTypeVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get
            {
                return TypeBase<IGeneralGenericTypeUniqueIdentifier>.EmptyIdentifiers;
            }
        }

        protected override IGeneralGenericTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier == null)
                {
                    if (this.Parent is IType)
                    {
                        if (this.TypeParametersInitialized)
                            this.uniqueIdentifier = ((IType)this.Parent).UniqueIdentifier.GetNestedIdentifier(this.Name, this.TypeParameters.Count);
                        else
                            this.uniqueIdentifier = ((IType)this.Parent).UniqueIdentifier.GetNestedIdentifier(this.Name, 0);
                    }
                    else if (this.Parent is INamespaceDeclaration)
                    {
                        if (this.TypeParametersInitialized)
                            this.uniqueIdentifier = TypeSystemIdentifiers.GetTypeIdentifier(((INamespaceDeclaration)this.Parent).FullName, this.Name, this.TypeParameters.Count);
                        else
                            this.uniqueIdentifier = TypeSystemIdentifiers.GetTypeIdentifier(((INamespaceDeclaration)this.Parent).FullName, this.Name, 0);

                    }
                    else if (this.TypeParametersInitialized)
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier)null, this.Name, this.TypeParameters.Count);
                    else
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier)null, this.Name, 0);
                }
            return this.uniqueIdentifier;
        }

        /// <summary>
        /// Returns whether the <see cref="Parameters"/> have been
        /// initialized.
        /// </summary>
        protected bool AreParametersInitialized { get { return this.parameters != null; } }

        protected override ITypeIdentityManager OnGetManager()
        {
            return this.Parent.IdentityManager;
        }
    }
}
