﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
//using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using System.ComponentModel;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base implementation of a <see cref="IntermediateInterfaceType{TInstanceType}"/>.
    /// </summary>
    public sealed class IntermediateInterfaceType :
        IntermediateInterfaceType<IntermediateInterfaceType>
    {
        internal IntermediateInterfaceType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        internal IntermediateInterfaceType(IntermediateInterfaceType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        protected override IntermediateInterfaceType GetNewPartial(IntermediateInterfaceType root, IIntermediateTypeParent parent)
        {
            return new IntermediateInterfaceType(root, parent);
        }
    }

    /// <summary>
    /// Provides a base class for an intermediate interface type 
    /// declaration.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Always)]
    public abstract partial class IntermediateInterfaceType<TInstanceType> :
        IntermediateGenericSegmentableParentType<IGeneralGenericTypeUniqueIdentifier, IInterfaceType, IIntermediateInterfaceType, TInstanceType>,
        IIntermediateInterfaceType
        where TInstanceType :
            IntermediateInterfaceType<TInstanceType>
    {
        #region IntermediateInterfaceType Data members

        #region Member Data Members
        private IGeneralGenericTypeUniqueIdentifier uniqueIdentifier;
        private ImplementedInterfacesCollection _implementedInterfaces;
        private ITypeCollection implementedInterfaces;

        /// <summary>
        /// Data member for <see cref="Events"/>.
        /// </summary>
        private IntermediateEventSignatureMemberDictionary<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType> events;
        /// <summary>
        /// Data member for <see cref="Indexers"/>.
        /// </summary>
        private IntermediateIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType> indexers;
        /// <summary>
        /// Data member for <see cref="Properties"/>.
        /// </summary>
        private IntermediatePropertySignatureMemberDictionary<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType> properties;
        /// <summary>
        /// Data member for <see cref="Methods"/>.
        /// </summary>
        private IntermediateGroupedMethodSignatureMemberDictionary<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>, IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType> methods;

        private IntermediateFullMemberDictionary members;
        private int suspendLevel;

        #endregion

        #endregion

        /// <summary>
        /// Creates a new <see cref="IntermediateInterfaceType{TInstanceType}"/> 
        /// declaration  with the <paramref name="name"/> and 
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing
        /// the semi-unique identifier associated to the 
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which owns the current <see cref="IntermediateInterfaceType{TInstanceType}"/>
        /// </param>
        internal protected IntermediateInterfaceType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        internal protected IntermediateInterfaceType(TInstanceType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        /// <summary>
        /// Obtains the <see cref="IInterfaceType"/> relative to the
        /// <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/>
        /// series from which to create the generic type.</param>
        /// <returns>A <see cref="IInterfaceType"/>
        /// instance which replaces the type-parameters
        /// contained within the <see cref="IntermediateInterfaceType{TInstanceType}"/>.</returns>
        /// <remarks>Performs no type-parameter check.</remarks>
        protected override IInterfaceType OnMakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return new _InterfaceTypeBase(this, typeParameters);
        }

        /// <summary>
        /// Returns the dictionary of full members which denotes the
        /// verbatim order listing of all members within the current
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/>.
        /// </summary>
        /// <returns>An <see cref="IIntermediateFullMemberDictionary"/> 
        /// instance which denotes the verbatim order listing of all 
        /// members within the current <see cref="IntermediateInterfaceType{TInstanceType}"/>.
        /// </returns>
        protected override IIntermediateFullMemberDictionary OnGetIntermediateMembers()
        {
            this.CheckEvents();
            this.CheckIndexers();
            this.CheckProperties();
            this.CheckMethods();
            return this._Members;
        }

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                this.Check_Members();
                return this.members;
            }
        }

        private void Check_Members()
        {
            if (this.members == null)
                this.members = Initialize_Members();
        }


        private IntermediateFullMemberDictionary Initialize_Members()
        {
            if (this.IsRoot)
                return new IntermediateFullMemberDictionary();
            else
                return new IntermediateFullMemberDictionary(((TInstanceType)this.GetRoot())._Members);
        }

        protected override bool Equals(IInterfaceType other)
        {
            return object.ReferenceEquals(other, this);
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Interface; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            Check_ImplementedInterfaces();
            return this._implementedInterfaces.GetLocked();
        }

        private void Check_ImplementedInterfaces()
        {
            if (this._implementedInterfaces == null)
                this._implementedInterfaces = new ImplementedInterfacesCollection(this);
        }

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
        {
            this.Check_ImplementedInterfaces();

            return ((ImplementedInterfacesCollection)(_implementedInterfaces)).GetLocalLocked();
        }

        /// <summary>
        /// The <see cref="ITypeCollection"/> which represents the interfaces implemented by the
        /// <see cref="IntermediateInterfaceType"/>.
        /// </summary>
        public new ITypeCollection ImplementedInterfaces
        {
            get
            {
                if (this.implementedInterfaces == null)
                {
                    this.Check_ImplementedInterfaces();
                    this.implementedInterfaces = ((ImplementedInterfacesCollection)(_implementedInterfaces)).Copy;
                }
                return this.implementedInterfaces;
            }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return object.ReferenceEquals(other, this);
        }

        protected override IType BaseTypeImpl
        {
            get { return null; }
        }

        #region IIntermediateEventSignatureParent<IInterfaceEventMember,IIntermediateInterfaceEventMember,IInterfaceType,IIntermediateInterfaceType> Members

        /// <summary>
        /// Returns the events associated to the current
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/> declaration.
        /// </summary>
        public IIntermediateEventSignatureMemberDictionary<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType> Events
        {
            get
            {
                this.CheckEvents();
                return this.events;
            }
        }

        #endregion

        #region IIntermediateIndexerSignatureParent<IInterfaceIndexerMember,IIntermediateInterfaceIndexerMember,IInterfaceType,IIntermediateInterfaceType> Members

        /// <summary>
        /// Returns the indexers associated to the current
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/> declaration.
        /// </summary>
        public IIntermediateIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType> Indexers
        {
            get
            {
                this.CheckIndexers();
                return this.indexers;
            }
        }

        #endregion

        #region IIntermediatePropertySignatureParent<IInterfacePropertyMember,IIntermediateInterfacePropertyMember,IInterfaceType,IIntermediateInterfaceType> Members

        /// <summary>
        /// Returns the properties associated to the current
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/> declaration.
        /// </summary>
        public IIntermediatePropertySignatureMemberDictionary<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType> Properties
        {
            get
            {
                this.CheckProperties();
                return this.properties;
            }
        }

        #endregion

        #region IIntermediateMethodSignatureParent<IInterfaceMethodMember,IIntermediateInterfaceMethodMember,IInterfaceType,IIntermediateInterfaceType> Members

        /// <summary>
        /// Returns the methods associated to the current
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/> declaration.
        /// </summary>
        public IIntermediateMethodSignatureMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType> Methods
        {
            get
            {
                this.CheckMethods();
                return (IIntermediateMethodSignatureMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>)this.methods;
            }
        }

        #endregion

        #region Simpler intermediate and Abstract GenericParameter System Event, Indexer, Property and Method Defs

        #region IIntermediateMethodSignatureParent<IMethodSignatureParameterMember<IInterfaceMethodMember,IInterfaceType>,IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember,IIntermediateInterfaceMethodMember,IInterfaceType,IIntermediateInterfaceType>,IInterfaceMethodMember,IIntermediateInterfaceMethodMember,IInterfaceType,IIntermediateInterfaceType> Members

        IIntermediateMethodSignatureMemberDictionary<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>, IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType> IIntermediateMethodSignatureParent<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>, IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        #region IIntermediateMethodSignatureParent Members

        IIntermediateMethodSignatureMemberDictionary IIntermediateMethodSignatureParent.Methods
        {
            get { return ((IIntermediateMethodSignatureMemberDictionary)(this.Methods)); }
        }

        #endregion

        #region IMethodSignatureParent Members

        IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
        {
            get { return ((IMethodSignatureMemberDictionary)(this.Methods)); }
        }

        #endregion

        #region IMethodSignatureParent<IInterfaceMethodMember,IInterfaceType> Members

        IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> IMethodSignatureParent<IInterfaceMethodMember, IInterfaceType>.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        #region IIntermediatePropertySignatureParent Members

        IIntermediatePropertySignatureMemberDictionary IIntermediatePropertySignatureParent.Properties
        {
            get { return (IIntermediatePropertySignatureMemberDictionary)this.Properties; }
        }

        #endregion

        #region IPropertySignatureParent Members

        IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
        {
            get { return (IPropertySignatureMemberDictionary)this.Properties; }
        }

        #endregion

        #region IPropertySignatureParent<IInterfacePropertyMember,IInterfaceType> Members

        IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> IPropertySignatureParent<IInterfacePropertyMember, IInterfaceType>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IIntermediateEventSignatureParent<IInterfaceEventMember,IIntermediateInterfaceEventMember,IEventSignatureParameterMember<IInterfaceEventMember,IInterfaceType>,IIntermediateEventSignatureParameterMember<IInterfaceEventMember,IIntermediateInterfaceEventMember,IInterfaceType,IIntermediateInterfaceType>,IInterfaceType,IIntermediateInterfaceType> Members

        IIntermediateEventSignatureMemberDictionary<IInterfaceEventMember, IIntermediateInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IIntermediateEventSignatureParameterMember<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType>, IInterfaceType, IIntermediateInterfaceType> IIntermediateEventSignatureParent<IInterfaceEventMember, IIntermediateInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IIntermediateEventSignatureParameterMember<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType>, IInterfaceType, IIntermediateInterfaceType>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IEventSignatureParent<IInterfaceEventMember,IEventSignatureParameterMember<IInterfaceEventMember,IInterfaceType>,IInterfaceType> Members

        IEventSignatureMemberDictionary<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType> IEventSignatureParent<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { return (IEventSignatureMemberDictionary)this.Events; }
        }

        #endregion

        #region IIntermediateEventSignatureParent Members

        IIntermediateEventSignatureMemberDictionary IIntermediateEventSignatureParent.Events
        {
            get { return (IIntermediateEventSignatureMemberDictionary)this.Events; }
        }

        #endregion

        #region IEventSignatureParent<IInterfaceEventMember,IInterfaceType> Members

        IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> IEventSignatureParent<IInterfaceEventMember, IInterfaceType>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IIndexerSignatureParent<IInterfaceIndexerMember,IInterfaceType> Members

        IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> IIndexerSignatureParent<IInterfaceIndexerMember, IInterfaceType>.Indexers
        {
            get { return this.Indexers; }
        }

        #endregion

        #region IIndexerSignatureParent Members

        IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
        {
            get { return (IIndexerSignatureMemberDictionary)this.Indexers; }
        }

        #endregion

        #region IIntermediateIndexerSignatureParent Members

        IIntermediateIndexerSignatureMemberDictionary IIntermediateIndexerSignatureParent.Indexers
        {
            get { return (IIntermediateIndexerSignatureMemberDictionary)this.Indexers; }
        }

        #endregion
        #endregion

        #region Member Check Methods

        private void CheckEvents()
        {
            lock (this.SyncObject)
                if (this.events == null)
                {
                    this.events = this.InitializeEvents();
                    for (int i = 0; i < this.suspendLevel; i++)
                        this.events.Suspend();
                }
        }

        private void CheckIndexers()
        {
            lock (this.SyncObject)
                if (this.indexers == null)
                {
                    this.indexers = this.InitializeIndexers();
                    for (int i = 0; i < this.suspendLevel; i++)
                        this.indexers.Suspend();
                }
        }

        private void CheckProperties()
        {
            lock (this.SyncObject)
                if (this.properties == null)
                {
                    this.properties = this.InitializeProperties();
                    for (int i = 0; i < this.suspendLevel; i++)
                        this.properties.Suspend();
                }
        }

        private void CheckMethods()
        {
            lock (this.SyncObject)
                if (this.methods == null)
                {
                    this.methods = this.InitializeMethods();
                    for (int i = 0; i < this.suspendLevel; i++)
                        this.methods.Suspend();
                }
        }

        #endregion

        protected override void OnIdentifierChanged(IGeneralGenericTypeUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
            base.OnIdentifierChanged(oldIdentifier, cause);
        }

        #region Initializers

        protected virtual IntermediateEventSignatureMemberDictionary<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType> InitializeEvents()
        {
            if (this.IsRoot)
                return new EventMembers(this._Members, ((TInstanceType)(this)));
            else
                return new EventMembers(this._Members, ((TInstanceType)(this)), (EventMembers)(this.GetRoot().Events));
        }

        protected virtual IntermediateIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType> InitializeIndexers()
        {
            if (this.IsRoot)
                return new IndexerMembers(this._Members, ((TInstanceType)(this)));
            else
                return new IndexerMembers(this._Members, ((TInstanceType)(this)), (IndexerMembers)(this.GetRoot().Indexers));
        }

        protected virtual IntermediatePropertySignatureMemberDictionary<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType> InitializeProperties()
        {
            if (this.IsRoot)
                return new PropertyMembers(this._Members, ((TInstanceType)(this)));
            else
                return new PropertyMembers(this._Members, ((TInstanceType)(this)), (PropertyMembers)(this.GetRoot().Properties));
        }

        protected virtual IntermediateGroupedMethodSignatureMemberDictionary<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>, IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType> InitializeMethods()
        {
            if (this.IsRoot)
                return new MethodMembers(this._Members, (TInstanceType)this);
            else
                return new MethodMembers(this._Members, (TInstanceType)this, (MethodMembers)this.GetRoot().Methods);
        }
        #endregion

        public override void Accept(IIntermediateTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return for the visitor.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <param name="visitor">The <see cref="IIntermediateTypeVisitor"/> to
        /// receive the <see cref="IntermediateInterfaceType{TInstanceType}"/> as a visitor.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        public override TResult Accept<TResult, TContext>(IIntermediateTypeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        /// <summary>
        /// Returns whether the events of the 
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreEventsInitialized { get { return this.events != null; } }

        /// <summary>
        /// Returns whether the indexers of the 
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreIndexersInitialized { get { return this.indexers != null; } }

        /// <summary>
        /// Returns whether the methods of the 
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreMethodsInitialized { get { return this.methods != null; } }

        /// <summary>
        /// Returns whether the properties of the 
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/>
        /// have been initialized.
        /// </summary>
        protected bool ArePropertiesInitialized { get { return this.properties != null; } }

        /// <summary>
        /// Returns whether the members of the 
        /// <see cref="IntermediateInterfaceType{TInstanceType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreMembersInitialized { get { return this.members != null; } }

        /// <summary>
        /// Returns the aggregate identifiers of the elements within the active scope
        /// of the <see cref="IntermediateInterfaceType{TInstanceType}"/>.
        /// </summary>
        /// <remarks>Purely for compliance with </remarks>
        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get
            {
                if (this.AreMembersInitialized)
                    if (this.AreTypesInitialized)
                        if (this.TypeParametersInitialized)
                            return this._Members.Keys.Concat(this._Types.Keys.Cast<IGeneralDeclarationUniqueIdentifier>()).Concat(this.TypeParameters.Keys);
                        else
                            return this._Members.Keys.Concat(this._Types.Keys.Cast<IGeneralDeclarationUniqueIdentifier>());
                    else
                        if (this.TypeParametersInitialized)
                            return this._Members.Keys.Concat(this.TypeParameters.Keys);
                        else
                            return this._Members.Keys;
                else
                    if (this.AreTypesInitialized)
                        if (this.TypeParametersInitialized)
                            return this._Types.Keys.Concat(this.TypeParameters.Keys);
                        else
                            return this._Types.Keys;
                    else
                        if (this.TypeParametersInitialized)
                            return this.TypeParameters.Keys;
                        else
                            return TypeBase<IGeneralGenericTypeUniqueIdentifier>.EmptyIdentifiers;
            }
        }

        protected override string OnGetIdentityName()
        {
            return ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.@interface);
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

        public override void Dispose()
        {
            try
            {
                if (this.events != null)
                {
                    this.events.Dispose();
                    this.events = null;
                }

                if (this.indexers != null)
                {
                    this.indexers.Dispose();
                    this.indexers = null;
                }
                if (this.properties != null)
                {
                    this.properties.Dispose();
                    this.properties = null;
                }

                if (this.methods != null)
                {
                    this.methods.Dispose();
                    this.methods = null;
                }
                if (this.members != null)
                {
                    if (this.IsRoot)
                        this.members.Dispose();
                    else
                        this.members.ConditionalRemove(this);
                    this.members = null;
                }
            }
            finally
            {
                base.Dispose();
            }
        }

        public void SuspendDualLayout()
        {
            this.suspendLevel++;
            base.SuspendTypeContainers();
            if (this.events != null)
                this.events.Suspend();
            if (this.indexers != null)
                this.indexers.Suspend();
            if (this.methods != null)
                this.methods.Suspend();
            if (this.properties != null)
                this.properties.Suspend();
        }

        public void ResumeDualLayout()
        {
            if (this.suspendLevel == 0)
                return;
            this.suspendLevel--;
            base.ResumeTypeContainers();
            if (this.events != null)
                this.events.Resume();
            if (this.indexers != null)
                this.indexers.Resume();
            if (this.methods != null)
                this.methods.Resume();
            if (this.properties != null)
                this.properties.Resume();

        }
        public bool HasProperties { get { return this.properties != null && this.properties.Count > 0; } }
        public bool HasIndexers { get { return this.indexers != null && this.indexers.Count > 0; } }
        public bool HasMethods { get { return this.methods != null && this.methods.Count > 0; } }
        public bool HasEvents { get { return this.events != null && this.events.Count > 0; } }

        protected override void ClearIdentifier()
        {
            this.uniqueIdentifier = null;
        }

        protected override TInstanceType GetNewPartial(TInstanceType root, IIntermediateTypeParent parent)
        {
            throw new NotImplementedException();
        }


        public override bool HasMembers
        {
            get
            {
                return this.members != null && this.members.Count > 0;
            }
        }
    }
}
