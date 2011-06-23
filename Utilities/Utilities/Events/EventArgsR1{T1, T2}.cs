using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Events
{
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
        public EventArgsR1(TArg1 arg1, TArg2 arg2)
            : base(arg1)
        {
            this.Arg2 = arg2;
        }
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
