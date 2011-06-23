using System;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a root partial implementation of <see cref="IDeclaration"/>.
    /// </summary>
    public abstract class DeclarationBase :
        IDeclaration
    {
        /// <summary>
        /// Creates a new <see cref="DeclarationBase"/> initialized to a default state.
        /// </summary>
        protected DeclarationBase()
        {
        }

        /// <summary>
        /// Occurs when the <see cref="DeclarationBase"/> is
        /// disposed.
        /// </summary>
        public event EventHandler Disposed;

        #region IDeclaration Members

        /// <summary>
        /// Returns the name of the <see cref="DeclarationBase"/>.
        /// </summary>
        public string Name
        {
            get { return this.OnGetName(); }
        }
        /// <summary>
        /// Obtains the <see cref="Name"/> for the <see cref="DeclarationBase"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that contains the name of the <see cref="DeclarationBase"/>.</returns>
        protected abstract string OnGetName();

        /// <summary>
        /// Returns the unique identifier for the current <see cref="DeclarationBase"/> where 
        /// <see cref="Name"/> is not enough to distinguish between two <see cref="IDeclaration"/> entities.
        /// </summary>
        public abstract string UniqueIdentifier { get; }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes the resources associated to the current
        /// <see cref="DeclarationBase"/>.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Occurs when the <see cref="DeclarationBase"/> is disposed.
        /// </summary>
        /// <remarks>Inheritors must remember to invoke this if they 
        /// override its functionality to ensure that <see cref="Disposed"/>
        /// is fired.</remarks>
        protected void OnDisposed()
        {
            var disposeCopy = this.Disposed;
            if (disposeCopy != null)
                disposeCopy(this, EventArgs.Empty);
            this.Disposed = null;
        }

        #endregion
    }
}
