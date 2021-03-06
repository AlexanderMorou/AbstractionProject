﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
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
    /// Provides a generic base class for intermediate types which are generic,
    /// can span multiple instances, and can contain types of their own.
    /// </summary>
    /// <typeparam name="TTypeIdentifier">The kind of type identifier used
    /// to differentiate the <typeparamref name="TIntermediateType"/>
    /// instance from its siblings.</typeparam>
    /// <typeparam name="TType">The kind of type as it exists in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The kind of type as it exists in the 
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TInstanceIntermediateType">The direct kind of type
    /// used to instantiate the partial elements within the system.</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class IntermediateGenericSegmentableParentType<TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType> :
        IntermediateGenericSegmentableType<TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType>,
        IIntermediateTypeParent
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            class,
            IGenericType<TTypeIdentifier, TType>,
            ITypeParent
        where TIntermediateType :
            class,
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            IIntermediateSegmentableType<TTypeIdentifier, TType, TIntermediateType>,
            IIntermediateTypeParent,
            TType
        where TInstanceIntermediateType :
            IntermediateGenericSegmentableParentType<TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType>,
            TIntermediateType
    {
        #region IntermediateGenericSegmentableParentType data members
        private IScopeCoercionCollection scopeCoercions;
        #region Nested GenericParameter Data Members
        /// <summary>
        /// Data member fro <see cref="Classes"/>.
        /// </summary>
        private IntermediateClassTypeDictionary classes;
        /// <summary>
        /// Data member for <see cref="Delegates"/>.
        /// </summary>
        private IntermediateDelegateTypeDictionary delegates;
        /// <summary>
        /// Data member for <see cref="Enums"/>.
        /// </summary>
        private IntermediateEnumTypeDictionary enums;
        /// <summary>
        /// Data member for <see cref="Interfaces"/>.
        /// </summary>
        private IntermediateInterfaceTypeDictionary interfaces;
        /// <summary>
        /// Data member for <see cref="Structs"/>.
        /// </summary>
        private IntermediateStructTypeDictionary structs;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private IntermediateFullTypeDictionary types;
        #endregion

        #endregion

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// instance with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique name
        /// of the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        protected IntermediateGenericSegmentableParentType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// instance with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        protected IntermediateGenericSegmentableParentType(IIntermediateTypeParent parent)
            : base(parent)
        {

        }
        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// with the <paramref name="rootType"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="rootType">The <typeparamref name="TInstanceIntermediateType"/> which
        /// represents the root instance of the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        protected IntermediateGenericSegmentableParentType(TInstanceIntermediateType rootType, IIntermediateTypeParent parent)
            : base(rootType, parent)
        {
        }


        #region IIntermediateTypeParent Members

        /// <summary>
        /// Returns the classes associated
        /// to the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public IIntermediateClassTypeDictionary Classes
        {
            get
            {
                this.CheckClasses();
                return this.classes;
            }
        }

        /// <summary>
        /// Returns the delegates associated
        /// to the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public IIntermediateDelegateTypeDictionary Delegates
        {
            get
            {
                this.CheckDelegates();
                return this.delegates;
            }
        }

        /// <summary>
        /// Returns the enumerations associated
        /// to the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public IIntermediateEnumTypeDictionary Enums
        {
            get
            {
                this.CheckEnums();
                return this.enums;
            }
        }

        /// <summary>
        /// Returns the interfaces associated
        /// to the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public IIntermediateInterfaceTypeDictionary Interfaces
        {
            get
            {
                this.CheckInterfaces();
                return this.interfaces;
            }
        }

        /// <summary>
        /// Returns the data structures associated
        /// to the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public IIntermediateStructTypeDictionary Structs
        {
            get
            {
                this.CheckStructs();
                return this.structs;
            }
        }

        /// <summary>
        /// Returns the full set of types associated to the 
        /// <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// which doesn't check the individual sub-type dictionaries
        /// prior to retrieval.
        /// </summary>
        protected IntermediateFullTypeDictionary _Types
        {
            get
            {
                this.Check_Types();
                return this.types;
            }
        }

        /// <summary>
        /// Returns the full set of types associated to
        /// the <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public IIntermediateFullTypeDictionary Types
        {
            get
            {
                this.CheckClasses();
                this.CheckDelegates();
                this.CheckEnums();
                this.CheckInterfaces();
                this.CheckStructs();
                return this._Types;
            }
        }

        public bool HasScopeCoercions
        {
            get
            { 
                return this.scopeCoercions != null && this.scopeCoercions.Count > 0;
            }
        }

        public bool HasClasses
        {
            get 
            {
                return this.classes != null && this.classes.Count > 0;
            }
        }

        public bool HasDelegates
        {
            get
            {
                return this.delegates != null && this.delegates.Count > 0;
            }
        }

        public bool HasEnums
        {
            get
            {
                return this.enums != null && this.enums.Count > 0;
            }
        }

        public bool HasInterfaces
        {
            get
            {
                return this.interfaces != null && this.interfaces.Count > 0;
            }
        }

        public bool HasStructs
        {
            get 
            {
                return this.structs != null && this.structs.Count > 0;
            }
        }

        public bool HasTypes
        {
            get
            {
                return this.types != null && this.types.Count > 0;
            }
        }

        #endregion

        #region ITypeParent Members

        IClassTypeDictionary ITypeParent.Classes
        {
            get { return this.Classes; }
        }

        IDelegateTypeDictionary ITypeParent.Delegates
        {
            get { return this.Delegates; }
        }

        IEnumTypeDictionary ITypeParent.Enums
        {
            get { return this.Enums; }
        }

        IInterfaceTypeDictionary ITypeParent.Interfaces
        {
            get { return this.Interfaces; }
        }

        IStructTypeDictionary ITypeParent.Structs
        {
            get { return this.Structs; }
        }

        IFullTypeDictionary ITypeParent.Types
        {
            get { return this.Types; }
        }

        #endregion

        #region Member Check Methods

        private static void SuspendCheck<TNestedTypeIdentifier, TNestedType, TIntermediateNestedType>(object syncObject, IntermediateTypeDictionary<TNestedTypeIdentifier, TNestedType, TIntermediateNestedType> dictionary, int suspendLevel)
            where TNestedTypeIdentifier :
                ITypeUniqueIdentifier,
                IGeneralTypeUniqueIdentifier
            where TNestedType :
                IType<TNestedTypeIdentifier, TNestedType>
            where TIntermediateNestedType :
                class,
                IIntermediateType,
                TNestedType
        {
            lock (syncObject)
            {
                if (suspendLevel <= 0)
                    return;
                if (dictionary == null)
                    throw new ArgumentNullException("dictionary");
                for (int i = 0; i < suspendLevel; i++)
                    dictionary.Suspend();
            }
        }

        private void CheckClasses()
        {
            lock (this.SyncObject)
                if (this.classes == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.classes = this.InitializeClasses();
                    SuspendCheck(this.SyncObject, this.classes, this.suspendLevel);
                }
        }

        private void CheckDelegates()
        {
            lock (this.SyncObject)
                if (this.delegates == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.delegates = this.InitializeDelegates();
                    SuspendCheck(this.SyncObject, this.delegates, this.suspendLevel);
                }
        }

        private void CheckEnums()
        {
            lock (this.SyncObject)
                if (this.enums == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.enums = this.InitializeEnums();
                    SuspendCheck(this.SyncObject, this.enums, this.suspendLevel);
                }
        }

        private void CheckInterfaces()
        {
            lock (this.SyncObject)
                if (this.interfaces == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.interfaces = this.InitializeInterfaces();
                    SuspendCheck(this.SyncObject, this.interfaces, this.suspendLevel);
                }
        }

        private void CheckStructs()
        {
            lock (this.SyncObject)
                if (this.structs == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.structs = this.InitializeStructs();
                    SuspendCheck(this.SyncObject, this.structs, this.suspendLevel);
                }
        }

        private void Check_Types()
        {
            lock (this.SyncObject)
                if (this.types == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.types = this.InitializeTypes();
                }
        }
        #endregion

        #region Initializers

        /// <summary>
        /// Initializes the <see cref="Classes"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateClassTypeDictionary"/> instance.</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone class type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// class type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the current partial instance as the parent.</remarks>
        protected virtual IntermediateClassTypeDictionary InitializeClasses()
        {
            IntermediateClassTypeDictionary result;
            if (this.IsRoot)
                result = new IntermediateClassTypeDictionary(this, this._Types);
            else
                result = new IntermediateClassTypeDictionary(this, this._Types, (IntermediateClassTypeDictionary)this.GetRoot().Classes);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        /// <summary>
        /// Initializes the <see cref="Delegates"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateDelegateTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone delegate type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// delegate type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the current partial instance as the parent.</remarks>
        protected virtual IntermediateDelegateTypeDictionary InitializeDelegates()
        {
            IntermediateDelegateTypeDictionary result;
            if (this.IsRoot)
                result = new IntermediateDelegateTypeDictionary(this, this._Types);
            else
                result = new IntermediateDelegateTypeDictionary(this, this._Types, (IntermediateDelegateTypeDictionary)this.GetRoot().Delegates);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        /// <summary>
        /// Initializes the <see cref="Enums"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateEnumTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone enum type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// enum type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the current partial instance as the parent.</remarks>
        protected virtual IntermediateEnumTypeDictionary InitializeEnums()
        {
            IntermediateEnumTypeDictionary result;
            if (this.IsRoot)
                result = new IntermediateEnumTypeDictionary(this, this._Types);
            else
                result = new IntermediateEnumTypeDictionary(this, this._Types, (IntermediateEnumTypeDictionary)this.GetRoot().Enums);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        /// <summary>
        /// Initializes the <see cref="Interfaces"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateInterfaceTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone interface type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// interface type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the current partial instance as the parent.</remarks>
        protected virtual IntermediateInterfaceTypeDictionary InitializeInterfaces()
        {
            IntermediateInterfaceTypeDictionary result;
            if (this.IsRoot)
                result = new IntermediateInterfaceTypeDictionary(this, this._Types);
            else
                result = new IntermediateInterfaceTypeDictionary(this, this._Types, (IntermediateInterfaceTypeDictionary)this.GetRoot().Interfaces);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        /// <summary>
        /// Initializes the <see cref="Structs"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateStructTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone struct type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// struct type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the current partial instance as the parent.</remarks>
        protected virtual IntermediateStructTypeDictionary InitializeStructs()
        {
            IntermediateStructTypeDictionary result;
            if (this.IsRoot)
                result = new IntermediateStructTypeDictionary(this, this._Types);
            else
                result = new IntermediateStructTypeDictionary(this, this._Types, (IntermediateStructTypeDictionary)this.GetRoot().Structs);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        /// <summary>
        /// Initializes the full types container to a default state if the 
        /// current <see cref="IntermediateNamespaceDeclaration"/> is 
        /// the root instance; otherwise, initializes the full types
        /// container as a sibling to the root instance's full types.
        /// </summary>
        /// <returns>A new <see cref="IntermediateFullTypeDictionary"/> instance</returns>
        protected virtual IntermediateFullTypeDictionary InitializeTypes()
        {
            if (this.IsRoot)
                return new IntermediateFullTypeDictionary(this);
            else
                return new IntermediateFullTypeDictionary(this, ((IntermediateGenericSegmentableParentType<TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType>)(object)this.GetRoot())._Types);
        }

        #endregion

        internal override void OnRearrangedInner(int from, int to)
        {
            /* *
             * Update the internal representations of the generic variants
             * of the intermediate types.  So if a type-parameter is rearranged
             * all of the references to the type are updated accordingly.
             * *
             * This assumes the types have been resolved to their linked
             * form from their symbol state, if they have not, this will
             * obviously not work.
             * */
            int gpC = this.GenericParameters.Count;
            int baseLine = (gpC - this.TypeParameters.Count);
            int realFrom = baseLine + from;
            int realTo = baseLine + to;
            foreach (var element in from subTypeEntry in this._Types.Values
                                    let subType = subTypeEntry.Entry
                                    let genericSubType = subType as _IIntermediateGenericType<TTypeIdentifier>
                                    where genericSubType != null
                                    select genericSubType)
                element.Rearranged(realFrom, realTo);
            base.OnRearrangedInner(from, to);
        }

        /// <summary>
        /// Frees managed resources used by the 
        /// <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public override void Dispose()
        {
            try
            {
                if (this.classes != null)
                {
                    this.classes.Dispose();
                    this.classes = null;
                }
                if (this.enums != null)
                {
                    this.enums.Dispose();
                    this.enums = null;
                }
                if (this.delegates != null)
                {
                    this.delegates.Dispose();
                    this.delegates = null;
                }
                if (this.interfaces != null)
                {
                    this.interfaces.Dispose();
                    this.interfaces = null;
                }
                if (this.structs != null)
                {
                    this.structs.Dispose();
                    this.structs = null;
                }
                if (this.types != null)
                {
                    if (this.IsRoot)
                        this.types.Dispose();
                    else
                        this.types.ConditionalRemove(this);
                    this.types = null;
                }
                if (this.scopeCoercions != null)
                    this.scopeCoercions = null;
            }
            finally
            {
                base.Dispose();
            }
        }
        protected override IEnumerable<IDeclaration> OnGetDeclarations()
        {
            return GetTypeParentDeclarations(this);
        }
        private int suspendLevel = 0;

        internal void SuspendTypeContainers()
        {
            this.suspendLevel++;
            if (this.classes != null)
                this.classes.Suspend();
            if (this.delegates != null)
                this.delegates.Suspend();
            if (this.enums != null)
                this.enums.Suspend();
            if (this.interfaces != null)
                this.interfaces.Suspend();
            if (this.structs != null)
                this.structs.Suspend();
        }

        internal void ResumeTypeContainers()
        {
            if (this.suspendLevel == 0)
                return;
            this.suspendLevel--;
            if (this.classes != null)
                this.classes.Resume();
            if (this.delegates != null)
                this.delegates.Resume();
            if (this.enums != null)
                this.enums.Resume();
            if (this.interfaces != null)
                this.interfaces.Resume();
            if (this.structs != null)
                this.structs.Resume();
        }

        public IScopeCoercionCollection ScopeCoercions
        {
            get
            {
                if (this.scopeCoercions == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.scopeCoercions = new ScopeCoercionCollection();
                }
                return this.scopeCoercions;
            }
        }

        internal override void OnLocked()
        {
            try
            {
                if (this.classes != null)
                    this.classes.Lock();
                if (this.delegates != null)
                    this.delegates.Lock();
                if (this.enums != null)
                    this.enums.Lock();
                if (this.interfaces != null)
                    this.interfaces.Lock();
                if (this.structs != null)
                    this.structs.Lock();
            }
            finally
            {
                base.OnLocked();
            }
        }

        internal override void OnUnlocked()
        {
            try
            {
                if (this.classes != null)
                    this.classes.Unlock();
                if (this.delegates != null)
                    this.delegates.Unlock();
                if (this.enums != null)
                    this.enums.Unlock();
                if (this.interfaces != null)
                    this.interfaces.Unlock();
                if (this.structs != null)
                    this.structs.Unlock();
            }
            finally
            {
                base.OnUnlocked();
            }
        }

        /// <summary>
        /// Returns whether the classes of the 
        /// <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreClassesInitialized { get { return this.classes != null; } }

        /// <summary>
        /// Returns whether the delegates of the 
        /// <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreDelegatesInitialized { get { return this.delegates != null; } }

        /// <summary>
        /// Returns whether the enums of the 
        /// <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreEnumsInitialized { get { return this.enums != null; } }

        /// <summary>
        /// Returns whether the interfaces of the 
        /// <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreInterfacesInitialized { get { return this.interfaces != null; } }

        /// <summary>
        /// Returns whether the structs of the 
        /// <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreStructsInitialized { get { return this.structs != null; } }

        /// <summary>
        /// Returns whether the group type dictionary of the 
        /// <see cref="IntermediateGenericSegmentableParentType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// has been initialized
        /// </summary>
        protected bool AreTypesInitialized { get { return this.types != null; } }

        protected override sealed IIntermediateIdentityManager OnGetIntermediateManager()
        {
            return this.Parent.IdentityManager;
        }

        public virtual IEnumerable<IType> GetTypes()
        {
            return this.types.GetTypes();
        }

    }
}
