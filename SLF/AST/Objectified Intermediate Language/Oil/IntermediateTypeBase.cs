using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base class for intermediate types.
    /// </summary>
    public abstract class IntermediateTypeBase<TType, TIntermediateType> :
        TypeBase<TType>,
        IIntermediateType
        where TType :
            class,
            IType<TType>
        where TIntermediateType :
            IIntermediateType,
            TType
    {
        /// <summary>
        /// Data member for <see cref="CustomAttributes"/>.
        /// </summary>
        private ICustomAttributeDefinitionCollectionSeries customAttributes;
        /// <summary>
        /// Data member for <see cref="AccessLevel"/>.
        /// </summary>
        private AccessLevelModifiers accessLevel;
        /// <summary>
        /// Data member for <see cref="DeclarationBase.Name"/>.
        /// </summary>
        private string name;
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private IIntermediateTypeParent parent;
        /// <summary>
        /// Data memer for <see cref="DeclaringModule"/>.
        /// </summary>
        private IIntermediateModule declaringModule;
        private bool isLocked;
        private byte isDisposed = 0;
        /// <summary>
        /// Returns the <see cref="String"/> name of the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> value indicating the name
        /// of the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>.</returns>
        protected override string OnGetName()
        {
            return this.name;
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>
        /// initialized to a default state.
        /// </summary>
        internal IntermediateTypeBase()
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the type's name.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which conatins the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/></param>
        /// <exception cref="System.ArgumentNullException"><paramref name="parent"/>
        /// is null.</exception>
        public IntermediateTypeBase(string name, IIntermediateTypeParent parent)
            : this(parent)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw new ArgumentException("name");
            this.name = name;
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which conatins the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/></param>
        /// <exception cref="System.ArgumentNullException"><paramref name="parent"/>
        /// is null.</exception>
        public IntermediateTypeBase(IIntermediateTypeParent parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            this.parent = parent;
        }

        #region IIntermediateType Members

        /// <summary>
        /// Returns the <see cref="IIntermediateNamespaceDeclaration"/>
        /// in which the current type (or its parent type) is declared.
        /// </summary>
        [DebuggerDisplay("{NamespaceName}")]
        public new IIntermediateNamespaceDeclaration Namespace
        {
            get
            {
                return (IIntermediateNamespaceDeclaration)base.Namespace;
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateType"/> in which the current
        /// <see cref="IntermediateTypeBase{TType, TIntermediateType}"/> is declared.
        /// </summary>
        public new IIntermediateType DeclaringType
        {
            get {
                if (this.Parent is IIntermediateType)
                    return ((IIntermediateType)(this.Parent));
                else
                    return null;
            }
        }

        public IIntermediateTypeParent Parent
        {
            get
            {
                return this.parent;
            }
            internal set
            {
                this.parent = value;
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> to which
        /// the current <see cref="IntermediateTypeBase{TType, TIntermediateType}"/> belongs.
        /// </summary>
        public virtual new IIntermediateAssembly Assembly
        {
            get {
                if (this.parent != null)
                    return this.Parent.Assembly;
                else
                    return null;
            }
        }

        /// <summary>
        /// Returns/sets the module which declared the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">thrown when a null value is assigned.</exception>
        public virtual IIntermediateModule DeclaringModule
        {
            get
            {
                if (this.declaringModule == null)
                {
                    if (this.Parent is IIntermediateType)
                        return ((IIntermediateType)(this.Parent)).DeclaringModule;
                    if (this.Assembly != null)
                        return this.Assembly.ManifestModule;
                }
                return this.declaringModule;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (this.declaringModule == value)
                    return;
                if (this.declaringModule != null &&
                    value.Parent != this.Assembly)
                    throw new ArgumentException("Cannot move the type to a new assembly.");
                this.declaringModule = value;
            }
        }

        #endregion

        #region IEquatable<IIntermediateType> Members

        /// <summary>
        /// Returns whether the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/> is equal to the
        /// <paramref name="other"/> type.
        /// </summary>
        /// <param name="other">The intermediate type to check against.</param>
        /// <returns>true if the current type is the <paramref name="other"/> type.</returns>
        public virtual bool Equals(IIntermediateType other)
        {
            return object.ReferenceEquals(this, other);
        }

        #endregion

        #region IIntermediateScopedDeclaration Members

        /// <summary>
        /// Returns/sets the access level of the
        /// <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>.
        /// </summary>
        public new AccessLevelModifiers AccessLevel
        {
            get
            {
                return this.accessLevel;
            }
            set
            {
                this.accessLevel = value;
            }
        }

        #endregion

        #region IIntermediateDeclaration Members

        /// <summary>
        /// Returns/sets the name of the
        /// <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>.
        /// </summary>
        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                OnSetName(value);
            }
        }

        protected virtual void OnSetName(string value)
        {
            if (value == this.name)
                return;
            DeclarationRenamingEventArgs renaming = new DeclarationRenamingEventArgs(this.name, value);
            this.OnRenaming(renaming);
            if (!renaming.Change)
                return;
            AssignName(value);
            this.OnRenamed(new DeclarationNameChangedEventArgs(renaming.OldName, renaming.NewName));
        }

        /// <summary>
        /// Raises the <see cref="Renaming"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DeclarationRenamingEventArgs"/> which
        /// indicate the old and new name of the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/> and
        /// whether the change should take place.</param>
        protected virtual void OnRenaming(DeclarationRenamingEventArgs e)
        {
            var renaming = this.Renaming;
            if (renaming != null)
                renaming(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renamed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DeclarationNameChangedEventArgs"/> which
        /// indicate the old and new name of the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>.</param>
        protected virtual void OnRenamed(DeclarationNameChangedEventArgs e)
        {
            var renamed = this.Renamed;
            if (renamed != null)
                renamed(this, e);
        }

        internal void AssignName(string value)
        {
            this.name = value;
        }

        /// <summary>
        /// Occurs when the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/> is renamed.
        /// </summary>
        public event EventHandler<DeclarationNameChangedEventArgs> Renamed;

        /// <summary>
        /// Occurs when the <see cref="IntermediateTypeBase{TType, TIntermediateType}"/> is being
        /// renamed.
        /// </summary>
        public event EventHandler<DeclarationRenamingEventArgs> Renaming;

        #endregion

        protected override IType OnGetDeclaringType()
        {
            return this.DeclaringType;
        }

        protected override bool CanCacheImplementsList
        {
            get { return false; }
        }

        protected override INamespaceDeclaration OnGetNameSpace()
        {
            if (this.DeclaringType != null)
                return this.DeclaringType.Namespace;
            if (this.Parent is IIntermediateNamespaceDeclaration)
                return ((IIntermediateNamespaceDeclaration)(this.Parent));
            return null;
        }

        protected override string OnGetNamespaceName()
        {
            var @namespace = this.Namespace;
            if (@namespace == null)
                return null;
            else
                return @namespace.FullName;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return this.AccessLevel;
        }

        protected override IAssembly OnGetAssembly()
        {
            return this.Assembly;
        }

        protected override IArrayType OnMakeArray(int rank)
        {
            return new ArrayType(this, rank);
        }

        protected override IArrayType OnMakeArray(params int[] lowerBounds)
        {
            return new ArrayType(this, lowerBounds);
        }

        protected override IType OnMakeByReference()
        {
            return new ByRefType(this);
        }

        protected override IType OnMakePointer()
        {
            return new PointerType(this);
        }

        protected sealed override IFullMemberDictionary OnGetMembers()
        {
            return this.OnGetIntermediateMembers();
        }

        protected override IType OnMakeNullable()
        {
            if (this is IReferenceType)
                throw new InvalidOperationException("Cannot make a reference type a nullable type.");
            return new NullableType(this);
        }

        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            return new IntermediateCustomAttributesBaseCollection(this);
        }


        #region IIntermediateCustomAttributedDeclaration Members

        public virtual new ICustomAttributeDefinitionCollectionSeries CustomAttributes
        {
            get
            {
                CheckCustomAttributes();
                return this.customAttributes;
            }
        }

        private void CheckCustomAttributes()
        {
            if (this.customAttributes == null)
                this.customAttributes = new CustomAttributeDefinitionCollectionSeries(this);
        }

        #endregion

        protected override void Dispose(bool dispose)
        {
            try
            {
                if (this.customAttributes != null)
                {
                    this.customAttributes.Dispose();
                    this.customAttributes = null;
                }
                this.parent = null;
                this.name = null;
                this.declaringModule = null;
            }
            finally
            {
                this.isDisposed = 1;
                base.Dispose(dispose);
            }
        }

        /// <summary>
        /// Returns the dictionary of full members which denotes the
        /// verbatim order listing of all members within the current
        /// <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>.
        /// </summary>
        /// <returns>An <see cref="IIntermediateFullMemberDictionary"/> 
        /// instance which denotes the verbatim order listing of all 
        /// members within the current <see cref="IntermediateTypeBase{TType, TIntermediateType}"/>.
        /// </returns>
        protected abstract IIntermediateFullMemberDictionary OnGetIntermediateMembers();

        public new IIntermediateFullMemberDictionary Members
        {
            get
            {
                return this.OnGetIntermediateMembers();
            }
        }

        public override string UniqueIdentifier
        {
            get
            {
                return base.UniqueIdentifier;
            }
        }

        #region IIntermediateType Members


        public abstract void Visit(IIntermediateTypeVisitor visitor);

        #endregion


        internal void Lock()
        {
            this.OnLocked();
        }

        internal void Unlock()
        {
            this.OnUnlocked();
        }

        internal virtual void OnLocked()
        {
            this.isLocked = true;
        }

        internal virtual void OnUnlocked()
        {
            this.isLocked = false;
        }

        internal bool IsLocked
        {
            get
            {
                return this.isLocked;
            }
        }
        public bool IsDisposed { get { return this.isDisposed == 1; } }
    }
}
