using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with a general case compiler.
    /// </summary>
    public interface ICompiler
    {
        ICompilerAid Aid { get; }
        ICompilerOptions Options { get; }
        ICompilerResults Compile(IIntermediateAssembly assembly);
    }

    public interface ICompiler<TAid> :
        ICompiler
        where TAid :
            ICompilerAid
    {
        new TAid Aid { get; }
    }

}
