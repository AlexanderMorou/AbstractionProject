using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Events
{
    /// <summary>
    /// Provides a generic base for event arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type for the mutable first argument.</typeparam>
    /// <typeparam name="TArg2">The type for the required second argument.</typeparam>
    /// <remarks>Ensure that the event using the <see cref="EventArgsR2{TArg1, TArg2}"/>
    /// details the arguments and their intended usage.</remarks>
    #if CODE_ANALYSIS
    /* *
     * Naming convention by design: they all represent event arguments; however,
     * reversing the suffix notation would be counter-intuitive and misleading since the
     * R* suffix relates to the read-only nature of the elements contained within.
     * */
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    #endif
    public class EventArgsR2<TArg1, TArg2> :
        EventArgs<TArg1>
    {
        /// <summary>
        /// Creates a new <see cref="EventArgsR2{TArg1, TArg2}"/> with the
        /// <paramref name="arg1"/> and the required <paramref name="arg2"/>.
        /// </summary>
        /// <param name="arg1">The <typeparamref name="TArg1"/> optional 
        /// argument.</param>
        /// <param name="arg2">The required <typeparamref name="TArg2"/>
        /// argument that is immutable after instantiation.</param>
        public EventArgsR2(TArg1 arg1, TArg2 arg2)
            : base(arg1)
        {
            this.Arg2 = arg2;
        }
        /// <summary>
        /// Creates a new <see cref="EventArgsR2{TArg1, TArg2}"/> with the
        /// required <paramref name="arg2"/> only.
        /// </summary>
        /// <param name="arg2">The required <typeparamref name="TArg2"/>
        /// argument that is immutable after instantiation.</param>
        public EventArgsR2(TArg2 arg2)
        {
            this.Arg2 = arg2;
        }

        /// <summary>
        /// Returns the required second argument 
        /// for the <see cref="EventArgsR2{TArg1, TArg2}"/>.
        /// </summary>
        public TArg2 Arg2 { get; private set; }
    }
}
