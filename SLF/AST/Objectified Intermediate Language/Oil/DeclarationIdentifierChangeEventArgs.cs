using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// A series of values representing the  kind of change within
    /// a declaration which invalidated its previous identifier.
    /// </summary>
    public enum DeclarationChangeCause
    {
        /// <summary>
        /// The declaration's name changed.
        /// </summary>
        Name,
        /// <summary>
        /// The declaration represents an indexed element
        /// in a fixed series, and its index changed.
        /// </summary>
        OrdinalNumber,
        /// <summary>
        /// The declaration represents an element which contains
        /// a fixed series, and the number of elements in the
        /// series changed.
        /// </summary>
        /// <remarks>
        /// The series defined is necessary in differentiating like
        /// elements, from one another, should other facets of their
        /// identity be equal.</remarks>
        IdentityCardinality,
        /// <summary>
        /// The declaration represents an item which contains a series
        /// of elements, a facet of the elements aid in describing the 
        /// owning declaration's identity, the identifiable facet
        /// changed.
        /// </summary>
        Signature,
    }
    /// <summary>
    /// Provides a set of event arguments for a declaration's identifier
    /// change.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of identifier used to 
    /// represent the declaration relative to its local scope.</typeparam>
    /// <remarks>Used to aid in updating dictionaries when intermediate
    /// declarations have their structure changed.</remarks>
    public class DeclarationIdentifierChangeEventArgs<TIdentifier> :
            EventArgs
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
    {

        /// <summary>
        /// Returns the <typeparamref name="TIdentifier"/> of the
        /// old identifier.
        /// </summary>
        public TIdentifier OldIdentifier { get; private set; }
        /// <summary>
        /// Returns the <typeparamref name="TIdentifier"/> of the
        /// new identifier.
        /// </summary>
        public TIdentifier NewIdentifier { get; private set; }
        /// <summary>
        /// Returns the <see cref="DeclarationChangeCause"/>
        /// which describes what changed within the identifier to cause
        /// the event.
        /// </summary>
        public DeclarationChangeCause Cause { get; private set; }

        public DeclarationIdentifierChangeEventArgs(TIdentifier oldIdentifier, TIdentifier newIdentifier, DeclarationChangeCause cause)
        {
            this.OldIdentifier = oldIdentifier;
            this.NewIdentifier = newIdentifier;
            this.Cause = cause;
        }
    }
}
