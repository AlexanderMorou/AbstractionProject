using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    public class FiniteAutomataTransitionNode<TCheck, TTarget> :
        IFiniteAutomataTransitionNode<TCheck, TTarget>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
    {
        #region IFiniteAutomataTransitionNode<TCheck,TTarget> Members

        public TCheck Check { get; internal set; }

        public TTarget Target { get; internal set; }

        #endregion
    }
}
