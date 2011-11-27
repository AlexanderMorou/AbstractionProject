using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface IIntermediateCodeTranslatorCompiler
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateCodeTranslatorCompilerAid"/> which helps prepare the 
        /// temporary files needed for a compilation operation.
        /// </summary>
        new IIntermediateCodeTranslatorCompilerAid Aid { get; }
    }
}