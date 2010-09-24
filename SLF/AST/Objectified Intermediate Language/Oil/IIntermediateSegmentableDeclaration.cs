using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines generic properties and methods for working with a declaration which can
    /// span multiple instances.
    /// </summary>
    /// <typeparam name="TDeclaration">The type of <see cref="IIntermediateSegmentableDeclaration{TDeclaration}"/>
    /// which needs segmentable functionality.</typeparam>
    public interface IIntermediateSegmentableDeclaration<TDeclaration> :
        IIntermediateSegmentableDeclaration
        where TDeclaration :
            IIntermediateSegmentableDeclaration<TDeclaration>
    {
        /// <summary>
        /// Returns the parts collection of the
        /// <see cref="IIntermediateSegmentableDeclaration{TDeclaration}"/>.
        /// </summary>
        new IIntermediateSegmentableDeclarationPartCollection<TDeclaration> Parts { get; }
        /// <summary>
        /// Returns the root <typeparamref name="TDeclaration"/> of the
        /// segmentable declaration.
        /// </summary>
        /// <returns>A <typeparamref name="TDeclaration"/> instance
        /// relative to the root.</returns>
        new TDeclaration GetRoot();
    }

    /// <summary>
    /// Defines properties and methods for working with a declaration which can
    /// span multiple instances.
    /// </summary>
    public interface IIntermediateSegmentableDeclaration :
        IIntermediateDeclaration
    {
        /// <summary>
        /// Returns whether the current <see cref="IIntermediateSegmentableDeclaration"/> instance
        /// is the root instance.
        /// </summary>
        /// <returns>true if the <see cref="IIntermediateSegmentableDeclaration"/> is the root,
        /// false if it is one of the root declaration's parts.</returns>
        bool IsRoot { get; }
        /// <summary>
        /// Returns the root <see cref="IIntermediateSegmentableDeclaration"/> of the
        /// segmentable declaration.
        /// </summary>
        /// <returns>A <see cref="IIntermediateSegmentableDeclaration"/> instance
        /// relative to the root.</returns>
        IIntermediateSegmentableDeclaration GetRoot();
        /// <summary>
        /// Returns the parts collection of the
        /// <see cref="IIntermediateSegmentableDeclaration"/>.
        /// </summary>
        IIntermediateSegmentableDeclarationPartCollection Parts { get; }
    }
}
