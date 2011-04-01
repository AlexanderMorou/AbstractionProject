using System;
using System.Collections.Generic;
using System.Text;
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

        internal void AssignName(string value)
        {
            this.name = value;
        }

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
            var renaming = this.Renaming;
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
            var renamed = this.Renamed;
            if (renamed != null)
                renamed(this, e);
        }

        /// <summary>
        /// Occurs when the name of the <see cref="IntermediateDeclarationBase"/>
        /// has changed.
        /// </summary>
        public event EventHandler<DeclarationNameChangedEventArgs> Renamed;

        /// <summary>
        /// Occurs when the name of the <see cref="IntermediateDeclarationBase"/>
        /// is in the process of being changed.
        /// </summary>
        public event EventHandler<DeclarationRenamingEventArgs> Renaming;

        #endregion

        #region IDeclaration Members


        public abstract string UniqueIdentifier { get; }

        /// <summary>
        /// Invoked when the <see cref="IntermediateDeclarationBase"/> is disposed.
        /// </summary>
        public event EventHandler Disposed;

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
                var disposeTemp = this.Disposed;
                if (disposeTemp != null)
                    disposeTemp(this, EventArgs.Empty);
                this.Disposed = null;
            }
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Renamed = null;
                this.Renaming = null;
                this.name = null;
            }
        }

        public override string ToString()
        {
            return this.UniqueIdentifier;
        }
    }
}
