using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Events
{
    /// <summary>
    /// Provides a generic base for event arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type for the required first argument.</typeparam>
    /// <typeparam name="TArg2">The type for the second argument.</typeparam>
    /// <typeparam name="TArg3">The type for the required third argument.</typeparam>
    /// <remarks>Ensure that the event using the <see cref="EventArgsR1R2{TArg1, TArg2}"/>
    /// details the arguments and their intended usage.</remarks>
    #if CODE_ANALYSIS
    /* *
     * Event argument series have as many elements as necessary;
     * so the quantity of type-parameters is relative to individual 
     * use and therefore: by design.
     * */
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    /* *
     * Naming convention by design: they all represent event arguments; however,
     * reversing the suffix notation would be counter-intuitive and misleading since the
     * R* suffix relates to the read-only nature of the elements contained within.
     * */
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    #endif
    public class EventArgsR1R3<TArg1, TArg2, TArg3> :
        EventArgsR1<TArg1, TArg2>
    {
        /// <summary>
        /// Creates a new <see cref="EventArgsR1R2R3{TArg1, TArg2, TArg3}"/> with the
        /// required <paramref name="arg1"/>,
        /// the <paramref name="arg2"/>, and the required
        /// <paramref name="arg3"/>.
        /// </summary>
        /// <param name="arg1">The required <typeparamref name="TArg1"/> 
        /// argument that is immutable after instantiation.</param>
        /// <param name="arg2">The <typeparamref name="TArg2"/>
        /// argument that is mutable after instantiation.</param>
        /// <param name="arg3">The required <typeparamref name="TArg3"/>
        /// argument that is immutable after instantiation.</param>
        public EventArgsR1R3(TArg1 arg1, TArg2 arg2, TArg3 arg3)
            : base(arg1, arg2)
        {
            this.Arg3 = arg3;
        }

        /// <summary>
        /// Creates a new <see cref="EventArgsR1R2R3{TArg1, TArg2, TArg3}"/> with the
        /// required <paramref name="arg1"/>, and the required
        /// <paramref name="arg3"/>.
        /// </summary>
        /// <param name="arg1">The required <typeparamref name="TArg1"/> 
        /// argument that is immutable after instantiation.</param>
        /// <param name="arg3">The required <typeparamref name="TArg3"/>
        /// argument that is immutable after instantiation.</param>
        public EventArgsR1R3(TArg1 arg1, TArg3 arg3)
            : base(arg1)
        {
            this.Arg3 = arg3;
        }

        /// <summary>
        /// Returns the required third argument 
        /// for the <see cref="EventArgsR1R2R3{TArg1, TArg2}"/>.
        /// </summary>
        public TArg3 Arg3 { get; private set; }
    }
}
