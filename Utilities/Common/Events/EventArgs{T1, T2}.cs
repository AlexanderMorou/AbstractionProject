using System;
using System.Collections.Generic;
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
    /// Provides a generic event argument set which
    /// contains two values.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first argument of the event 
    /// arguments.</typeparam>
    /// <typeparam name="TArg2">The type fo the second argument of the event
    /// arguments.</typeparam>
    public class EventArgs<TArg1, TArg2> :
        EventArgs<TArg1>
    {
        /// <summary>
        /// Creates a new <see cref="EventArgs{TArg1, TArg2}"/> with the 
        /// <paramref name="arg1"/> and <paramref name="arg2"/> 
        /// provided.
        /// </summary>
        /// <param name="arg1">The <typeparamref name="TArg1"/>
        /// element for the first argument.</param>
        /// <param name="arg2">The <typeparamref name="TArg2"/>
        /// element for the second argument.</param>
        public EventArgs(TArg1 arg1, TArg2 arg2)
            : base(arg1)
        {
            this.Arg2 = arg2;
        }
        /// <summary>
        /// Creates a new <see cref="EventArgs{TArg1, TArg2}"/> with the 
        /// <paramref name="arg1"/> provided.
        /// </summary>
        /// <param name="arg1">The <typeparamref name="TArg1"/>
        /// element for the first argument.</param>
        public EventArgs(TArg1 arg1) : base(arg1) { this.Arg2 = default(TArg2); }
        /// <summary>
        /// Creates a new <see cref="EventArgs{TArg1, TArg2}"/> with the 
        /// <paramref name="arg2"/> provided.
        /// </summary>
        /// <param name="arg2">The <typeparamref name="TArg2"/>
        /// element for the second argument.</param>
        public EventArgs(TArg2 arg2) : base() { this.Arg2 = arg2; }
        /// <summary>
        /// Returns/sets the second argument for the <see cref="EventArgs{TArg1, TArg2}"/>.
        /// </summary>
        public TArg2 Arg2 { get; set; }
    }
}
