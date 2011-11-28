using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Instructions
{
    public interface IStackInstruction
    {
        /// <summary>
        /// Returns the <see cref="OpCode"/> that the <see cref="IStackInstruction"/> represents.
        /// </summary>
        OpCode OpCode { get; }
    }
}
