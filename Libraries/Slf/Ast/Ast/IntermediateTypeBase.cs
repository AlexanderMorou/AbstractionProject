﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using System.ComponentModel;
using AllenCopeland.Abstraction.Utilities;
//using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Utilities.Events;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base class for intermediate types.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class IntermediateTypeBase<TTypeIdentifier, TType, TIntermediateType> :
        TypeBase<TTypeIdentifier, TType>,
        IIntermediateType<TTypeIdentifier, TType, TIntermediateType>,
        IIntermediateDeclaration<TTypeIdentifier>
        where TTypeIdentifier :
            ITypeUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            class,
            IType<TTypeIdentifier, TType>
        where TIntermediateType :
            IIntermediateType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {
        /// <summary>
        /// Data member for <see cref="Metadata"/>.
        /// </summary>
        private IMetadataDefinitionCollection _metadata;
        /// <summary>
        /// Data member for <see cref="AccessLevel"/>.
        /// </summary>
        private AccessLevelModifiers accessLevel;
        /// <summary>
        /// Data member for <see cref="DeclarationBase{TIdentifier}.Name"/>.
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
        /// Returns the <see cref="String"/> name of the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> value indicating the name
        /// of the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.</returns>
        protected override string OnGetName()
        {
            return this.name;
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// initialized to a default state.
        /// </summary>
        internal IntermediateTypeBase()
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the type's name.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which conatins the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/></param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// or <paramref name="parent"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is
        /// <see cref="String.Empty"/>.</exception>
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
        /// Creates a new <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which conatins the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/></param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parent"/>
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
        /// Returns the <see cref="IIntermediateTypeParent"/> which
        /// contains the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        public new IIntermediateTypeParent Parent
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
        /// the current <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> belongs.
        /// </summary>
        public virtual new IIntermediateAssembly Assembly
        {
            get
            {
                if (this.parent != null)
                    return this.Parent.Assembly;
                else
                    return null;
            }
        }

        /// <summary>
        /// Returns/sets the module which declared the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
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
                if (!(this.Parent is IIntermediateNamespaceDeclaration ||
                      this.Parent is IIntermediateAssembly))
                    throw new InvalidOperationException("Cannot move type hierarchy to a different module, move the top-most type to achieve this affect.");
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
        /// Returns whether the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> is equal to the
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
        /// <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
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
        /// <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        public new string Name
        {
            get
            {
                return this.OnGetName();
            }
            set
            {
                OnSetName(value);
            }
        }

        /// <summary>
        /// Implementation which sets the name of the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value representing the unique
        /// name of the type within the current context.</param>
        protected virtual void OnSetName(string value)
        {
            if (value == this.name)
                return;
            if (this.IsNameLocked)
                throw new InvalidOperationException("Name is read-only.");
            var uniqueIdentifier = this.UniqueIdentifier;
            DeclarationRenamingEventArgs renaming = new DeclarationRenamingEventArgs(this.name, value);
            this.OnRenaming(renaming);
            if (!renaming.Change)
                return;
            AssignName(value);
            this.OnRenamed(new DeclarationNameChangedEventArgs(renaming.OldName, renaming.NewName));
            this.OnIdentifierChanged(uniqueIdentifier, DeclarationChangeCause.Name);
        }

        /// <summary>
        /// Raises the <see cref="Renaming"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DeclarationRenamingEventArgs"/> which
        /// indicate the old and new name of the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> and
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
        /// indicate the old and new name of the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.</param>
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
        /// Occurs when the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> is renamed.
        /// </summary>
        public event EventHandler<DeclarationNameChangedEventArgs> Renamed;

        /// <summary>
        /// Occurs when the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> is being
        /// renamed.
        /// </summary>
        public event EventHandler<DeclarationRenamingEventArgs> Renaming;

        #endregion

        /// <summary>
        /// Returns the <see cref="ITypeParent"/> from which the current
        /// <see cref="IntermediateTypeBase{TIdentifier, TType, TIntermediateType}"/> is declared.
        /// </summary>
        /// <returns>An <see cref="ITypeParent"/> instance denoting
        /// the current <see cref="IntermediateTypeBase{TIdentifier, TType, TIntermediateType}"/>'s point 
        /// of declaration.</returns>
        protected override ITypeParent OnGetParent()
        {
            return this.Parent;
        }

        /// <summary>
        /// Returns whether the set of interfaces implemented by
        /// the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// can be cached.
        /// </summary>
        protected sealed override bool CanCacheImplementsList
        {
            get { return false; }
        }

        protected override INamespaceDeclaration OnGetNamespace()
        {
            return this.GetNamespace();
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

        protected sealed override IFullMemberDictionary OnGetMembers()
        {
            return this.OnGetIntermediateMembers();
        }

        protected override IMetadataCollection InitializeMetadata()
        {
            return new IntermediateTypeMetadataCollection(this);
        }


        #region IIntermediateMetadataEntity Members

        public virtual new IMetadataDefinitionCollection Metadata
        {
            get
            {
                CheckCustomAttributes();
                return this._metadata;
            }
        }

        private void CheckCustomAttributes()
        {
            if (this._metadata == null)
                this._metadata = new MetadataDefinitionCollection(this, this.Assembly);
        }

        #endregion

        public override void Dispose()
        {
            try
            {
                if (this._metadata != null)
                {
                    this._metadata.Dispose();
                    this._metadata = null;
                }
                this.parent = null;
                this.name = null;
                this.declaringModule = null;
            }
            finally
            {
                this.isDisposed = 1;
                base.Dispose();
            }
        }

        /// <summary>
        /// Returns the dictionary of full members which denotes the
        /// verbatim order listing of all members within the current
        /// <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        /// <returns>An <see cref="IIntermediateFullMemberDictionary"/> 
        /// instance which denotes the verbatim order listing of all 
        /// members within the current <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </returns>
        protected abstract IIntermediateFullMemberDictionary OnGetIntermediateMembers();

        public new IIntermediateFullMemberDictionary Members
        {
            get
            {
                return this.OnGetIntermediateMembers();
            }
        }

        #region IIntermediateType Members

        public abstract void Accept(IIntermediateTypeVisitor visitor);
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return for the visitor.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <param name="visitor">The <see cref="IIntermediateTypeVisitor"/> to
        /// receive the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> as a visitor.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        public abstract TResult Accept<TResult, TContext>(IIntermediateTypeVisitor<TResult, TContext> visitor, TContext context);

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


        #region IIntermediateDeclaration<TTypeIdentifier> Members

        public event EventHandler<DeclarationIdentifierChangeEventArgs<TTypeIdentifier>> IdentifierChanged;

        #endregion

        #region IIntermediateDeclaration Members
        private event EventHandler<DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>> _IdentifierChanged;

        event EventHandler<DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>> IIntermediateDeclaration.IdentifierChanged
        {
            add { this._IdentifierChanged += value; }
            remove { this._IdentifierChanged -= value; }
        }

        #endregion

        protected virtual void OnIdentifierChanged(TTypeIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            this.ClearIdentifier();
            var newIdentifier = this.UniqueIdentifier;
            var _identifierChanged = this._IdentifierChanged;
            if (_identifierChanged != null)
                _identifierChanged(this, new DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>(oldIdentifier, newIdentifier, cause));
            var identifierChanged = this.IdentifierChanged;
            if (identifierChanged != null)
                identifierChanged(this, new DeclarationIdentifierChangeEventArgs<TTypeIdentifier>(oldIdentifier, newIdentifier, cause));
        }

        protected abstract void ClearIdentifier();

        //protected override bool IsAttributeInheritable(IType attribute)
        //{
        //    if (attribute is ICompiledType)
        //    {
        //        var cType = attribute as ICompiledType;
        //        return CliAssist.GetAttributeUsage(cType.UnderlyingSystemType).AllowMultiple;
        //    }
        //    else
        //        return CliAssist.GetAttributeUsage(attribute).AllowMultiple;
        //}

        /// <summary>
        /// Occurs when the base type of the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// has changed.
        /// </summary>
        public event EventHandler<EventArgs<IType, IType>> BaseTypeChanged;

        protected override sealed IIdentityManager OnGetManager()
        {
            return this.OnGetIntermediateManager();
        }

        protected abstract IIntermediateIdentityManager OnGetIntermediateManager();


        public new IIntermediateIdentityManager IdentityManager
        {
            get
            {
                return this.OnGetIntermediateManager();
            }
        }

        internal virtual bool IsNameLocked { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="String"/> value denoting
        /// the Summary text of the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        public virtual string SummaryText { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="String"/> value denoting
        /// the remarks text of the <see cref="IntermediateTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        public virtual string RemarksText { get; set; }

        public abstract bool HasMembers { get; }
    }
}
