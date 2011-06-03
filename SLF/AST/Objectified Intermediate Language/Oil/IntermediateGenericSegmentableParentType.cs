using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a generic base class for intermediate types which are generic,
    /// can span multiple instances, and can contain types of their own.
    /// </summary>
    /// <typeparam name="TType">The kind of type as it exists in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The kind of type as it exists in the 
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TInstanceIntermediateType">The direct kind of type
    /// used to instantiate the partial elements within the system.</typeparam>
    public abstract class IntermediateGenericSegmentableParentType<TType, TIntermediateType, TInstanceIntermediateType> :
        IntermediateGenericSegmentableType<TType, TIntermediateType, TInstanceIntermediateType>,
        IIntermediateTypeParent
        where TType :
            class,
            IGenericType<TType>,
            ITypeParent
        where TIntermediateType :
            class,
            IIntermediateGenericType<TType, TIntermediateType>,
            IIntermediateSegmentableType<TType, TIntermediateType>,
            IIntermediateTypeParent,
            TType
        where TInstanceIntermediateType :
            IntermediateGenericSegmentableType<TType, TIntermediateType, TInstanceIntermediateType>,
            TIntermediateType
    {
        #region IntermediateGenericSegmentableParentType data members
        private IScopeCoercionCollection scopeCoercions;
        #region Nested Type Data Members
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

        private bool lockMembersAndTypes;

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// instance with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique name
        /// of the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        protected IntermediateGenericSegmentableParentType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// instance with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        protected IntermediateGenericSegmentableParentType(IIntermediateTypeParent parent)
            : base(parent)
        {

        }
        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// with the <paramref name="rootType"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="rootType">The <typeparamref name="TInstanceIntermediateType"/> which
        /// represents the root instance of the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        protected IntermediateGenericSegmentableParentType(TInstanceIntermediateType rootType, IIntermediateTypeParent parent)
            : base(rootType, parent)
        {
        }


        #region IIntermediateTypeParent Members

        /// <summary>
        /// Returns the classes associated
        /// to the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.
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
        /// to the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.
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
        /// to the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.
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
        /// to the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.
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
        /// to the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.
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
        /// <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>
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
        /// the <see cref="IntermediateGenericSegmentableParentType{TType, TIntermediateType, TInstanceIntermediateType}"/>.
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

        private static void SuspendCheck<TNestedType, TIntermediateNestedType>(IntermediateTypeDictionary<TNestedType, TIntermediateNestedType> dictionary, int suspendLevel)
            where TNestedType :
                IType<TNestedType>
            where TIntermediateNestedType :
                class,
                IIntermediateType,
                TNestedType
        {
            if (suspendLevel <= 0)
                return;
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");
            for (int i = 0; i < suspendLevel; i++)
                dictionary.Suspend();
        }

        private void CheckClasses()
        {
            if (this.classes == null)
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this.classes = this.InitializeClasses();
                SuspendCheck(this.classes, this.suspendLevel);
            }
        }

        private void CheckDelegates()
        {
            if (this.delegates == null)
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this.delegates = this.InitializeDelegates();
                SuspendCheck(this.delegates, this.suspendLevel);
            }
        }

        private void CheckEnums()
        {
            if (this.enums == null)
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this.enums = this.InitializeEnums();
                SuspendCheck(this.enums, this.suspendLevel);
            }
        }

        private void CheckInterfaces()
        {
            if (this.interfaces == null)
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this.interfaces = this.InitializeInterfaces();
                SuspendCheck(this.interfaces, this.suspendLevel);
            }
        }

        private void CheckStructs()
        {
            if (this.structs == null)
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this.structs = this.InitializeStructs();
                SuspendCheck(this.structs, this.suspendLevel);
            }
        }

        private void Check_Types()
        {
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
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone class type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// class type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
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
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone delegate type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// delegate type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the current partial instance as the parent.</remarks>
        protected virtual IntermediateDelegateTypeDictionary InitializeDelegates()
        {
            IntermediateDelegateTypeDictionary result;
            if (this.IsRoot)
                result  = new IntermediateDelegateTypeDictionary(this, this._Types);
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
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone enum type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// enum type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
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
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone interface type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// interface type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the current partial instance as the parent.</remarks>
        protected virtual IntermediateInterfaceTypeDictionary InitializeInterfaces()
        {
            IntermediateInterfaceTypeDictionary result;
            if (this.IsRoot)
                result  = new IntermediateInterfaceTypeDictionary(this, this._Types);
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
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone struct type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// struct type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
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
                return new IntermediateFullTypeDictionary(this, ((IntermediateGenericSegmentableParentType<TType, TIntermediateType, TInstanceIntermediateType>)(object)this.GetRoot())._Types);
        }

        #endregion

        internal override void OnRearrangedInner(int from, int to)
        {
            /* *
             * Update the internal representations of the generic variants
             * of the intermediate types.  So if a type-parameter is rearranged
             * all of the references to the type are updated accordingly.
             * *
             * This assumes the types have been resolved to their proper form,
             * if they have not, this will obviously not work.
             * */
            int baseLine = -this.GenericParameters.Count;
            int realFrom = baseLine + from;
            int realTo = baseLine + to;
            foreach (var element in from subTypeEntry in this._Types.Values
                                    let subType = subTypeEntry.Entry
                                    let genericSubType = subType as _IIntermediateGenericType
                                    where genericSubType != null
                                    select genericSubType)
                element.Rearranged(realFrom, realTo);
            base.OnRearrangedInner(from, to);
        }
        protected override void Dispose(bool dispose)
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
                base.Dispose(dispose);
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

        public override IEnumerable<string> AggregateIdentifiers
        {
            get {
                return (from memberEntry in this.Members.Values
                        let member = memberEntry.Entry
                        where !((member is IConstructorMember) || (member is ICoercionMember))
                        select member.Name).Concat
                       (from type in this.Types.Values
                        select type.Entry.Name).Distinct();
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
    }
}
