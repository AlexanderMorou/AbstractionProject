using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// <summary>
        /// Returns the <see cref="ICompilerAid"/> which
        /// contains functionality specific methods towards aiding
        /// the compile process based upon the kind of compiler.
        /// </summary>
        ICompilerAid Aid { get; }
        /// <summary>
        /// The <see cref="ICompilerOpetions"/> which designates information
        /// about the resulted compile.
        /// </summary>
        ICompilerOptions Options { get; }
        /// <summary>
        /// Returns the <see cref="ILanguageProvider"/> associated to the 
        /// <see cref="ICompiler"/>.
        /// </summary>
        ILanguageProvider Provider { get; }
    }

    public interface ICompiler<TAid> :
        ICompiler
        where TAid :
            ICompilerAid
    {
        new TAid Aid { get; }
    }

}