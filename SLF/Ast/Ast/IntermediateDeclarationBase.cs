using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Properties;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base for intermediate declarations.
    /// </summary>
    public abstract class IntermediateDeclarationBase<TIdentifier> :
        IIntermediateDeclaration<TIdentifier>
        where TIdentifier :
            IDeclarationUniqueIdentifier, 
            IGeneralDeclarationUniqueIdentifier
    {
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private string name;
        private int isDisposed = 0;
        private object syncObject = new object();

        #region IIntermediateDeclaration Members

        /// <summary>
        /// Returns/sets the name of the <see cref="IntermediateDeclarationBase{TIdentifier}"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return OnGetName();
            }
            set
            {
                OnSetName(value);
            }
        }

        /// <summary>
        /// Instructs the <see cref="IntermediateDeclarationBase{TIdentifier}"/>
        /// that the <paramref name="name"/> has been changed.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// value representing the new name of the 
        /// <see cref="IntermediateDeclarationBase{TIdentifier}"/>.</param>
        protected virtual void OnSetName(string name)
        {
            if (name == this.name)
                return;
            DeclarationRenamingEventArgs renaming = new DeclarationRenamingEventArgs(this.name, name);
            this.OnRenaming(renaming);
            if (!renaming.Change)
                return;
            var oldIdentifier = this.UniqueIdentifier;
            AssignName(name);
            this.OnIdentifierChanged(oldIdentifier, DeclarationChangeCause.Name);
            this.OnRenamed(new DeclarationNameChangedEventArgs(renaming.OldName, renaming.NewName));
        }

        internal void AssignName(string value)
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
            this.name = value;
        }

        /// <summary>
        /// Returns the <see cref="String"/> value representing part or
        /// all of the unique identifier that makes up the 
        /// <see cref="IntermediateDeclarationBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A string value representing the name of the 
        /// <see cref="IntermediateDeclarationBase{TIdentifier}"/>.</returns>
        protected virtual string OnGetName()
        {
            return this.name;
        }

        /// <summary>
        /// Raises the <see cref="Renaming"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DeclarationRenamingEventArgs"/> which
        /// indicate the old and new name of the <see cref="IntermediateDeclarationBase{TIdentifier}"/> and
        /// whether the change should take place.</param>
        protected virtual void OnRenaming(DeclarationRenamingEventArgs e)
        {
            var renaming = this._Renaming;
            if (renaming != null)
                renaming(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renamed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DeclarationNameChangedEventArgs"/> which
        /// indicate the old and new name of the <see cref="IntermediateDeclarationBase{TIdentifier}"/>.</param>
        protected virtual void OnRenamed(DeclarationNameChangedEventArgs e)
        {
            var renamed = this._Renamed;
            if (renamed != null)
                renamed(this, e);
        }

        /// <summary>
        /// Occurs when the name of the <see cref="IntermediateDeclarationBase{TIdentifier}"/>
        /// has changed.
        /// </summary>
        public event EventHandler<DeclarationNameChangedEventArgs> Renamed
        {
            add
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this._Renamed += value;
            }
            remove
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this._Renamed -= value;
            }
        }

        public event EventHandler<DeclarationNameChangedEventArgs> _Renamed;

        /// <summary>
        /// Occurs when the name of the <see cref="IntermediateDeclarationBase{TIdentifier}"/>
        /// is in the process of being changed.
        /// </summary>
        public event EventHandler<DeclarationRenamingEventArgs> Renaming
        {
            add
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this._Renaming += value;
            }
            remove
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this._Renaming -= value;
            }
        }
        private event EventHandler<DeclarationRenamingEventArgs> _Renaming;

        #endregion
        #region IDeclaration<TIdentifier> Members

        /// <summary>
        /// Occurs after the 
        /// <see cref="IntermediateDeclarationBase{TIdentifier}"/>
        /// has changed in a way which invalidates the previous unique
        /// identifier.
        /// </summary>
        public event EventHandler<DeclarationIdentifierChangeEventArgs<TIdentifier>> IdentifierChanged;

        /// <summary>
        /// Returns the <typeparamref name="TIdentifier"/> which 
        /// differentiates the <see cref="IntermediateDeclarationBase{TIdentifier}"/>
        /// from others within its local scope.
        /// </summary>
        public abstract TIdentifier UniqueIdentifier { get; }

        #endregion

        /// <summary>
        /// Raises the <see cref="IdentifierChanged"/> event handler with the <paramref name="oldIdentifier"/> provided
        /// and the <paramref name="cause"/> which resulted in the call.
        /// </summary>
        /// <param name="oldIdentifier">The <typeparamref name="TIdentifier"/> which represents the 
        /// uniqueness information about the declaration before the change.</param>
        /// <param name="cause">The <see cref="DeclarationChangeCause"/> which denotes the
        /// aspect of the declaration that changed which caused the event.</param>
        /// <remarks>The new, current, identifier will be obtained in the default implementation.</remarks>
        protected virtual void OnIdentifierChanged(TIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            this.ClearIdentifier();
            var newIdentifier = this.UniqueIdentifier;
            var _identifierChanged = this._IdentifierChanged;
            if (_identifierChanged != null)
                _identifierChanged(this, new DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>(oldIdentifier, newIdentifier, cause));
            var identifierChanged = this.IdentifierChanged;
            if (identifierChanged != null)
                identifierChanged(this, new DeclarationIdentifierChangeEventArgs<TIdentifier>(oldIdentifier, newIdentifier, cause));
        }

        protected abstract void ClearIdentifier();

        #region IDeclaration Members
        private event EventHandler<DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>> _IdentifierChanged;

        event EventHandler<DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>> IIntermediateDeclaration.IdentifierChanged
        {
            add { this._IdentifierChanged += value; }
            remove { this._IdentifierChanged -= value; }
        }

        IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
        {
            get { return this.UniqueIdentifier; }
        }

        private event EventHandler _Disposed;

        /// <summary>
        /// Invoked when the <see cref="IntermediateDeclarationBase{TIdentifier}"/> is disposed.
        /// </summary>
        public event EventHandler Disposed
        {
            add
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this._Disposed += value;
            }
            remove
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this._Disposed -= value;
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes the <see cref="IntermediateDeclarationBase{TIdentifier}"/>.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.Dispose(true);
            }
            finally
            {
                var disposeTemp = this._Disposed;
                if (disposeTemp != null)
                    disposeTemp(this, EventArgs.Empty);
                this.isDisposed = 1;
                this._Disposed = null;
            }
        }

        #endregion

        /// <summary>
        /// Disposes the <see cref="IntermediateDeclarationBase{TIdentifier}"/>
        /// </summary>
        /// <param name="disposing">whether to dispose the managed 
        /// resources as well as the unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.IdentifierChanged = null;
                this._IdentifierChanged = null;
                this._Renamed = null;
                this._Renaming = null;
                this.name = null;
            }
        }

        /// <summary>
        /// Converts the current intermediate declaration into a <see cref="String"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> which represents the current
        /// intermediate declaration.</returns>
        public override string ToString()
        {
            return this.UniqueIdentifier.ToString();
        }

        /// <summary>
        /// Returns whether the <see cref="IntermediateDeclarationBase{TIdentifier}"/> has
        /// been disposed.
        /// </summary>
        public bool IsDisposed { get { return this.isDisposed == 1; } }

        protected object SyncObject { get { return this.syncObject; } }
    }
}
