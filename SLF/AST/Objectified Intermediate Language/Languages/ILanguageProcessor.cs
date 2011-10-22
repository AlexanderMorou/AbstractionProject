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

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a processor 
    /// of a language which provides a certain kind of output.
    /// </summary>
    public interface ILanguageProcessor<TResult, TInput>
    {
        TResult Process(TInput input);
    }
    /// <summary>
    /// Defines properties and methods for working with a processor 
    /// of a language which provides a certain kind of output.
    /// </summary>
    public interface ILanguageProcessor<TResult, TInput, TInputContext>
    {
        TResult Process(TInput input, TInputContext context);
    }
}
