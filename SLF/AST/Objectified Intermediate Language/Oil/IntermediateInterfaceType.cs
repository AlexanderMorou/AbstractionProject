﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
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
    public abstract partial class IntermediateInterfaceType<TInstanceType> :
        IntermediateGenericSegmentableParentType<IInterfaceType, IIntermediateInterfaceType, TInstanceType>,
        IIntermediateInterfaceType
        where TInstanceType :
            IntermediateInterfaceType<TInstanceType>
    {
        #region IntermediateInterfaceType Data members

        #region Member Data Members

        /// <summary>
        /// Data member for <see cref="Events"/>.
        /// </summary>
        private IIntermediateEventSignatureMemberDictionary<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType> events;
        /// <summary>
        /// Data member for <see cref="Indexers"/>.
        /// </summary>
        private IIntermediateIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType> indexers;
        /// <summary>
        /// Data member for <see cref="Properties"/>.
        /// </summary>
        private IIntermediatePropertySignatureMemberDictionary<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType> properties;
        /// <summary>
        /// Data member for <see cref="Methods"/>.
        /// </summary>
        private IIntermediateMethodSignatureMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType> methods;

        private IntermediateFullMemberDictionary members;

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
        protected override IInterfaceType OnMakeGenericType(ITypeCollectionBase typeParameters)
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
            throw new NotImplementedException();
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

        #region IIntermediatePropertySignatureParentType<IInterfacePropertyMember,IIntermediateInterfacePropertyMember,IInterfaceType,IIntermediateInterfaceType> Members

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
            get {
                this.CheckMethods();
                return this.methods;
            }
        }

        #endregion

        #region Simpler intermediate and Abstract Type System Event, Indexer, Property and Method Defs

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

        #region IIntermediatePropertySignatureParentType Members

        IIntermediatePropertySignatureMemberDictionary IIntermediatePropertySignatureParentType.Properties
        {
            get { return (IIntermediatePropertySignatureMemberDictionary)this.Properties; }
        }

        #endregion

        #region IPropertySignatureParentType Members

        IPropertySignatureMemberDictionary IPropertySignatureParentType.Properties
        {
            get { return (IPropertySignatureMemberDictionary)this.Properties; }
        }

        #endregion

        #region IPropertySignatureParentType<IInterfacePropertyMember,IInterfaceType> Members

        IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> IPropertySignatureParentType<IInterfacePropertyMember, IInterfaceType>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IIntermediateEventSignatureParent<IInterfaceEventMember,IIntermediateInterfaceEventMember,IEventSignatureParameterMember<IInterfaceEventMember,IInterfaceType>,IIntermediateEventSignatureParameterMember<IInterfaceEventMember,IIntermediateInterfaceEventMember,IInterfaceType,IIntermediateInterfaceType>,IInterfaceType,IIntermediateInterfaceType> Members

        IIntermediateEventSignatureMemberDictionary<IInterfaceEventMember, IIntermediateInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IIntermediateEventSignatureParameterMember<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType>, IInterfaceType, IIntermediateInterfaceType> IIntermediateEventSignatureParent<IInterfaceEventMember,IIntermediateInterfaceEventMember,IEventSignatureParameterMember<IInterfaceEventMember,IInterfaceType>,IIntermediateEventSignatureParameterMember<IInterfaceEventMember,IIntermediateInterfaceEventMember,IInterfaceType,IIntermediateInterfaceType>,IInterfaceType,IIntermediateInterfaceType>.Events
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
            if (this.events == null)
                this.events = this.InitializeEvents();
        }

        private void CheckIndexers()
        {
            if (this.indexers == null)
                this.indexers = this.InitializeIndexers();
        }

        private void CheckProperties()
        {
            if (this.properties == null)
                this.properties = this.InitializeProperties();
        }

        private void CheckMethods()
        {
            if (this.methods == null)
                this.methods = this.InitializeMethods();
        }

        #endregion

        #region Initializers

        protected virtual IIntermediateEventSignatureMemberDictionary<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType> InitializeEvents()
        {
            if (this.IsRoot)
                return new EventMembers(this._Members, ((TInstanceType)(this)));
            else
                return new EventMembers(this._Members, ((TInstanceType)(this)), (EventMembers)(this.GetRoot().Events));
        }

        protected virtual IIntermediateIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType> InitializeIndexers()
        {
            if (this.IsRoot)
                return new IndexerMembers(this._Members, ((TInstanceType)(this)));
            else
                return new IndexerMembers(this._Members, ((TInstanceType)(this)), (IndexerMembers)(this.GetRoot().Indexers));
        }

        protected virtual IIntermediatePropertySignatureMemberDictionary<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType> InitializeProperties()
        {
            if (this.IsRoot)
                return new PropertyMembers(this._Members, ((TInstanceType)(this)));
            else
                return new PropertyMembers(this._Members, ((TInstanceType)(this)), (PropertyMembers)(this.GetRoot().Properties));
        }

        protected virtual IIntermediateMethodSignatureMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType> InitializeMethods()
        {
            if (this.IsRoot)
                return new MethodMembers(this._Members, (TInstanceType)this);
            else
                return new MethodMembers(this._Members, (TInstanceType)this, (MethodMembers)this.GetRoot().Methods);
        }
        #endregion

    }
}