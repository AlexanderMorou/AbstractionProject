using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Provides a base implementation of a 
    /// <see cref="IFiniteAutomataTransitionNode{TCheck, TTarget}"/>
    /// for working with a finite automation's transition node.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used in the
    /// automation.</typeparam>
    /// <typeparam name="TTarget">The type of target pointed to
    /// by the <see cref="FiniteAutomataTransitionNode{TCheck, TTarget}"/>.
    /// </typeparam>
    public class FiniteAutomataTransitionNode<TCheck, TTarget> :
        IFiniteAutomataTransitionNode<TCheck, TTarget>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
    {
        #region IFiniteAutomataTransitionNode<TCheck,TTarget> Members

        /// <summary>
        /// Returns the <typeparamref name="TCheck"/>
        /// value used to define the constraint for transitioning
        /// into the <see cref="Target"/>.
        /// </summary>
        public TCheck Check { get; internal set; }

        /// <summary>
        /// Returns the <typeparamref name="TTarget"/> 
        /// of the current <see cref="IFiniteAutomataTransitionNode{TCheck, TTarget}"/>.
        /// </summary>
        public TTarget Target { get; internal set; }

        #endregion
    }
}
