using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
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
    /// <typeparam name="T">The type of the first argument.</typeparam>
    public class EventArgs<T> :
        EventArgs
    {
        /// <summary>
        /// Creates a new <see cref="EventArgs{T}"/> instance
        /// with the <paramref name="arg1"/> provided.
        /// </summary>
        /// <param name="arg1">The parameter for the first argument.</param>
        public EventArgs(T arg1)
            : base()
        {
            this.Arg1 = arg1;
        }

        /// <summary>
        /// Creates a new <see cref="EventArgs{T}"/> with the
        /// <see cref="Arg1"/> initialized to a default state.
        /// </summary>
        protected EventArgs()
            : base()
        {
            this.Arg1 = default(T);
        }

        /// <summary>
        /// Returns/sets the first argument for the <see cref="EventArgs{T}"/>.
        /// </summary>
        public T Arg1 { get; set; }
    }
}
