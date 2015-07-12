using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// a finite automata section
    /// </summary>
    public interface IFiniteAutomataSection<TCheck, TState> 
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of the
        /// states contained within the current section.
        /// </summary>
        IEnumerable<TState> States { get; }
        /// <summary>
        /// Returns the <typeparamref name="TState"/> which
        /// roots the section.
        /// </summary>
        TState Root { get; }
        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of the
        /// edge states contained within the current section.
        /// </summary>
        IEnumerable<TState> Edges { get; }
    }
}
