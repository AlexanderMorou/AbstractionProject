using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Utilities.Properties;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base implementation of 
    /// <see cref="IIntermediateSegmentableDeclaration{TIdentifier, TDeclaration}"/>
    /// which provides a series of <typeparamref name="TDeclaration"/>
    /// instances which represent a single whole instance.
    /// </summary>
    /// <typeparam name="TIdentifier">The kind of identifier used to differentiate the
    /// <typeparamref name="TInstDeclaration"/> instances from one another.</typeparam>
    /// <typeparam name="TDeclaration">The type of <see cref="IIntermediateSegmentableDeclaration"/>
    /// which needs partialalbe functionality</typeparam>
    /// <typeparam name="TInstDeclaration">The specific <typeparamref name="TInstDeclaration"/>
    /// which represents the implementation of the <typeparamref name="TDeclaration"/>
    /// provided.</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class IntermediateSegmentableDeclarationBase<TIdentifier, TDeclaration, TInstDeclaration> :
        IntermediateDeclarationBase<TIdentifier>,
        IIntermediateSegmentableDeclaration<TIdentifier, TDeclaration>
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>,
            IGeneralDeclarationUniqueIdentifier
        where TDeclaration :
            class,
            IIntermediateSegmentableDeclaration<TIdentifier, TDeclaration>
        where TInstDeclaration :
            IntermediateSegmentableDeclarationBase<TIdentifier, TDeclaration, TInstDeclaration>,
            TDeclaration/*,
            new (TDeclaration rootDeclaration)*/
    {
        /// <summary>
        /// Data member for <see cref="GetRoot()"/>.
        /// </summary>
        private TInstDeclaration rootDeclaration;
        /// <summary>
        /// Data member for <see cref="Parts"/>.
        /// </summary>
        private IntermediateSegmentableDeclarationParts<TIdentifier, TDeclaration, TInstDeclaration> parts;
        /// <summary>
        /// Creates a new <see cref="IntermediateSegmentableDeclarationBase{TIdentifier, TDeclaration, TInstDeclaration}"/>
        /// instance with the <paramref name="rootDeclaration"/> 
        /// provided.
        /// </summary>
        /// <param name="rootDeclaration">The <typeparamref name="TDeclaration"/>
        /// instance that represents the root element of the
        /// <see cref="IntermediateSegmentableDeclarationBase{TIdentifier, TDeclaration, TInstDeclaration}"/>.</param>
        public IntermediateSegmentableDeclarationBase(TInstDeclaration rootDeclaration)
        {
            this.rootDeclaration = rootDeclaration;
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateSegmentableDeclarationBase{TIdentifier, TDeclaration, TInstDeclaration}"/>
        /// initialized to a default state.
        /// </summary>
        /// <remarks>Typically reserved for the root instance of a set.</remarks>
        public IntermediateSegmentableDeclarationBase()
        {
        }

        /// <summary>
        /// Returns the <typeparamref name="TDeclaration"/> that is the root declaration.
        /// </summary>
        /// <returns>An instance of <typeparamref name="TDeclaration"/> relative to the root instance that spawned
        /// the current <see cref="IntermediateSegmentableDeclarationBase{TIdentifier, TDeclaration, TInstDeclaration}"/>.</returns>
        public TInstDeclaration GetRoot()
        {
            if (this.rootDeclaration == null)
                throw new InvalidOperationException();
            return this.rootDeclaration;
        }



        #region IIntermediateSegmentableDeclaration<TDeclaration> Members

        TDeclaration IIntermediateSegmentableDeclaration<TIdentifier, TDeclaration>.GetRoot()
        {
            return this.GetRoot();
        }

        /// <summary>
        /// Returns the parts collection of the
        /// <see cref="IntermediateSegmentableDeclarationBase{TIdentifier, TDeclaration, TInstDeclaration}"/>.
        /// </summary>
        public IIntermediateSegmentableDeclarationPartCollection<TIdentifier, TDeclaration> Parts
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    if (this.parts == null)
                        this.parts = new IntermediateSegmentableDeclarationParts<TIdentifier, TDeclaration, TInstDeclaration>((TInstDeclaration)this, GetNewPartial, this.OnGetIdentityName());
                    return this.parts;
                }
                else
                {
                    return this.GetRoot().Parts;
                }
            }
        }

        /// <summary>
        /// Obtains a the <see cref="String"/> value that identifies the 
        /// sequence as a concept.
        /// </summary>
        /// <returns>A <see cref="String"/> value that identifies the 
        /// sequence as a concept.</returns>
        protected abstract string OnGetIdentityName();

        #endregion

        #region IIntermediateSegmentableDeclaration Members

        IIntermediateSegmentableDeclaration IIntermediateSegmentableDeclaration.GetRoot()
        {
            return this.GetRoot();
        }

        /// <summary>
        /// Returns whether the current <see cref="IntermediateSegmentableDeclarationBase{TIdentifier, TDeclaration, TInstDeclaration}"/>
        /// is the root (first) instance.
        /// </summary>
        public bool IsRoot
        {
            get { return this.rootDeclaration == null; }
        }

        IIntermediateSegmentableDeclarationPartCollection IIntermediateSegmentableDeclaration.Parts
        {
            get { return ((IIntermediateSegmentableDeclarationPartCollection)(this.Parts)); }
        }

        #endregion

        /// <summary>
        /// Obtains a new <typeparamref name="TInstDeclaration"/>
        /// associated to the partial instance being created.
        /// </summary>
        /// <returns>A new <typeparamref name="TInstDeclaration"/>
        /// associated to the partial instance being created.</returns>
        protected abstract TInstDeclaration GetNewPartial();

        /// <summary>
        /// Disposes the <see cref="IntermediateSegmentableDeclarationBase{TIdentifier, TDeclaration, TInstDeclaration}"/>
        /// </summary>
        /// <param name="disposing">whether to dispose the managed resources as well as
        /// the unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                    if (this.parts != null)
                    {
                        this.parts.AsParallel().ForAll(
                            part =>
                                part.Dispose());
                        if (this.parts.baseList != null)
                            this.parts.baseList.Clear();
                        this.parts = null;
                    }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

    }
}
