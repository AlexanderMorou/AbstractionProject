using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Events
{
    /// <summary>
    /// Provides a generic event arguments class for use with standard
    /// <see cref="EventHandler{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the first, read-only, argument.</typeparam>
    #if CODE_ANALYSIS
    /* *
     * Naming convention by design: they all represent event arguments; however,
     * reversing the suffix notation would be counter-intuitive and misleading since the
     * R* suffix relates to the read-only nature of the elements contained within.
     * */
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    #endif
    public class EventArgsR1<T> :
        EventArgs
    {
        /// <summary>
        /// Creates a new <see cref="EventArgsR1{T}"/> class
        /// with the <paramref name="arg1"/> provided.
        /// </summary>
        /// <param name="arg1">The value of the first read-only 
        /// argument.</param>
        public EventArgsR1(T arg1)
            : base()
        {
            this.Arg1 = arg1;
        }
        /// <summary>
        /// Returns the <typeparamref name="T"/> instance
        /// associated with the first argument of the
        /// event.
        /// </summary>
        public T Arg1 { get; private set; }
    }
}
