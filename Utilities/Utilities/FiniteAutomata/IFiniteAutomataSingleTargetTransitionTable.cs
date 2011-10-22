using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Defines properties and methods for a single-target transition
    /// table for a finite state automation.
    /// </summary>
    /// <typeparam name="TCheck"></typeparam>
    /// <typeparam name="TState"></typeparam>
    public interface IFiniteAutomataSingleTargetTransitionTable<TCheck, TState> :
        IFiniteAutomataTransitionTable<TCheck, TState, TState>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
    }
}
