using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
