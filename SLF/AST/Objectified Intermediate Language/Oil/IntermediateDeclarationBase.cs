using System;
using System.Collections.Generic;
using System.Text;
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
    /// Provides a base for intermediate declarations.
    /// </summary>
    public abstract class IntermediateDeclarationBase :
        IIntermediateDeclaration
    {
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private string name;
        private int isDisposed = 0;

        #region IIntermediateDeclaration Members

        /// <summary>
        /// Returns/sets the name of the <see cref="IntermediateDeclarationBase"/>.
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
        /// Instructs the <see cref="IntermediateDeclarationBase"/>
        /// that the <paramref name="name"/> has been changed.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// value representing the new name of the 
        /// <see cref="IntermediateDeclarationBase"/>.</param>
        protected virtual void OnSetName(string name)
        {
            if (name == this.name)
                return;
            DeclarationRenamingEventArgs renaming = new DeclarationRenamingEventArgs(this.name, name);
            this.OnRenaming(renaming);
            if (!renaming.Change)
                return;
            AssignName(name);
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
        /// <see cref="IntermediateDeclarationBase"/>.
        /// </summary>
        /// <returns>A string value representing the name of the 
        /// <see cref="IntermediateDeclarationBase"/>.</returns>
        protected virtual string OnGetName()
        {
            return this.name;
        }

        /// <summary>
        /// Raises the <see cref="Renaming"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DeclarationRenamingEventArgs"/> which
        /// indicate the old and new name of the <see cref="IntermediateDeclarationBase"/> and
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
        /// indicate the old and new name of the <see cref="IntermediateDeclarationBase"/>.</param>
        protected virtual void OnRenamed(DeclarationNameChangedEventArgs e)
        {
            var renamed = this._Renamed;
            if (renamed != null)
                renamed(this, e);
        }

        /// <summary>
        /// Occurs when the name of the <see cref="IntermediateDeclarationBase"/>
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
        /// Occurs when the name of the <see cref="IntermediateDeclarationBase"/>
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

        #region IDeclaration Members


        public abstract string UniqueIdentifier { get; }

        private event EventHandler _Disposed;

        /// <summary>
        /// Invoked when the <see cref="IntermediateDeclarationBase"/> is disposed.
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
        /// Disposes the <see cref="IntermediateDeclarationBase"/>.
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
        /// Disposes the <see cref="IntermediateDeclarationBase"/>
        /// </summary>
        /// <param name="disposing">whether to dispose the managed 
        /// resources as well as the unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._Renamed = null;
                this._Renaming = null;
                this.name = null;
            }
        }

        public override string ToString()
        {
            return this.UniqueIdentifier;
        }

        /// <summary>
        /// Returns whether the <see cref="IntermediateDeclarationBase"/> has
        /// been disposed.
        /// </summary>
        public bool IsDisposed { get { return this.isDisposed == 1; } }
    }
}
