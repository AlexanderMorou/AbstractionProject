using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base implementation of 
    /// <see cref="IIntermediateSegmentableDeclaration{TDeclaration}"/>
    /// which provides a series of <typeparamref name="TDeclaration"/>
    /// instances which represent a single whole instance.
    /// </summary>
    /// <typeparam name="TDeclaration">The type of <see cref="IIntermediateSegmentableDeclaration"/>
    /// which needs partialalbe functionality</typeparam>
    public abstract class IntermediateSegmentableDeclarationBase<TDeclaration, [GenericParamDataTarget(typeof(IntermediateSegmentableDeclarationBase<,>.TInstDeclarationData))] TInstDeclaration> :
        IntermediateDeclarationBase,
        IIntermediateSegmentableDeclaration<TDeclaration>
        where TDeclaration :
            class,
            IIntermediateSegmentableDeclaration<TDeclaration>
        where TInstDeclaration :
            class,
            TDeclaration/*,
            new (TDeclaration rootDeclaration)*/
    {
        /* *
         * TInstDeclarationData's data structure which defines the special extensions to type-parameters.
         * */
        private struct TInstDeclarationData
        {
            private TInstDeclarationData(TDeclaration rootDeclaration) { }
        }
        /// <summary>
        /// Data member for <see cref="GetRootDeclaration()"/>.
        /// </summary>
        private TDeclaration rootDeclaration;
        /// <summary>
        /// Data member for <see cref="Parts"/>.
        /// </summary>
        private IIntermediateSegmentableDeclarationPartCollection<TDeclaration> parts;
        /// <summary>
        /// Creates a new <see cref="IntermediateSegmentableDeclarationBase{TDeclaration}"/>
        /// instance with the <paramref name="rootDeclaration"/> 
        /// provided.
        /// </summary>
        /// <param name="rootDeclaration">The <typeparamref name="TDeclaration"/>
        /// instance that represents the root element of the
        /// <see cref="IntermediateSegmentableDeclarationBase"/>.</param>
        public IntermediateSegmentableDeclarationBase(TDeclaration rootDeclaration)
        {
            this.rootDeclaration = rootDeclaration;
        }

        public IntermediateSegmentableDeclarationBase()
        {
        }

        #region IIntermediateSegmentableDeclaration<TDeclaration> Members

        /// <summary>
        /// Returns the <typeparamref name="TDeclaration"/> that is the root declaration.
        /// </summary>
        /// <returns>An instance of <typeparamref name="TDeclaration"/> relative to the root instance that spawned
        /// the current <see cref="IntermediateSegmentableDeclarationBase"/>.</returns>
        public TDeclaration GetRoot()
        {
            if (this.rootDeclaration == null)
                throw new InvalidOperationException();
            return this.rootDeclaration;
        }

        public IIntermediateSegmentableDeclarationPartCollection<TDeclaration> Parts
        {
            get {
                if (this.IsRoot)
                {
                    if (this.parts == null)
                        this.parts = new IntermediateSegmentableDeclarationParts<TDeclaration, TInstDeclaration>(((TDeclaration)((object)(this))), GetNewPartial);
                    return this.parts;
                }
                else
                {
                    return this.GetRoot().Parts;
                }
            }
        }

        #endregion

        #region IIntermediateSegmentableDeclaration Members

        IIntermediateSegmentableDeclaration IIntermediateSegmentableDeclaration.GetRoot()
        {
            return this.GetRoot();
        }

        public bool IsRoot
        {
            get { return this.rootDeclaration == null; }
        }

        IIntermediateSegmentableDeclarationPartCollection IIntermediateSegmentableDeclaration.Parts
        {
            get { return ((IIntermediateSegmentableDeclarationPartCollection)(this.Parts)); }
        }

        #endregion

        protected abstract TInstDeclaration GetNewPartial();

    }
}
