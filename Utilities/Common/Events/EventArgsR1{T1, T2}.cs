using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Events
{
    
    /// <summary>
    /// Provides a base class for representing a pair of 
    /// event arguments, where the first argument is readonly and
    /// thus required.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first argument
    /// in the pair which is readonly.</typeparam>
    /// <typeparam name="TArg2">The type of the second argument
    /// in the pair which is mutable.</typeparam>
    #if CODE_ANALYSIS
    /* *
     * Naming convention by design: they all represent event arguments; however,
     * reversing the suffix notation would be counter-intuitive and misleading since the
     * R* suffix relates to the read-only nature of the elements contained within.
     * */
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    #endif
    public class EventArgsR1<TArg1, TArg2> :
        EventArgsR1<TArg1>
    {
        /// <summary>
        /// Creates a new <see cref="EventArgsR1{TArg1, TArg2}"/>
        /// with the <paramref name="arg1"/> and <paramref name="arg2"/> provided.
        /// </summary>
        /// <param name="arg1">The <typeparamref name="TArg1"/>
        /// as the required first parameter.</param>
        /// <param name="arg2">The <typeparamref name="TArg2"/>
        /// as the optional second parameter.</param>
        public EventArgsR1(TArg1 arg1, TArg2 arg2)
            : base(arg1)
        {
            this.Arg2 = arg2;
        }
        /// <summary>
        /// Creates a new <see cref="EventArgsR1"/>
        /// with the <paramref name="arg1"/>
        /// provided.
        /// </summary>
        /// <param name="arg1">The <typeparamref name="TArg1"/> 
        /// value associated to the required first parameter.</param>
        public EventArgsR1(TArg1 arg1)
            : base(arg1)
        {
            this.Arg2 = default(TArg2);
        }

        /// <summary>
        /// Returns/sets the second argument 
        /// for the <see cref="EventArgsR2{TArg1, TArg2}"/>.
        /// </summary>
        public TArg2 Arg2 { get; set; }
    }
}
