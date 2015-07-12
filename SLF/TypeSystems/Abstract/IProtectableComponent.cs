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

namespace AllenCopeland.Abstraction.Slf
{

    public interface IProtectableComponent
    {
        /// <summary>
        /// Instructs the <see cref="IProtectableComponent"/> to enter a 
        /// protected state wherein no changes may occur to the object state.
        /// </summary>
        /// <remarks>Used to guarantee changeless state during an identity 
        /// sensitive operation.</remarks>
        void EnterProtectedState();
        /// <summary>
        /// Instructs the <see cref="IProtectableComponent"/> to exit
        /// a previously entered protected state, allowing state changes to occur.
        /// </summary>
        void ExitProtectedState();

        /// <summary>
        /// Returns whether the <see cref="IProtectableComponent"/> is within a protected state.
        /// </summary>
        bool InProtectedState { get; }
    }
}
