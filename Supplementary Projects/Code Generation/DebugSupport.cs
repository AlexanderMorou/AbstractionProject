using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen
{
    public enum DebugSupport
    {
        /// <summary>
        /// Provides no debugger support.
        /// </summary>
        None,
        /// <summary>
        /// Provides debugger pdb support.
        /// </summary>
        PDB,
        /// <summary>
        /// Provides full debugger support.
        /// </summary>
        Full
    }
}
