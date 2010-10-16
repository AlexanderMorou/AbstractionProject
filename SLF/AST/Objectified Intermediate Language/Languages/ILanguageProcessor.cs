using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
