using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    /// <summary>
    /// Defines properties and methods for working with a series of production rule
    /// template preprocessor items.
    /// </summary>
    public interface IPreprocessorDirectives :
        IReadOnlyCollection<IPreprocessorDirective>
    {
    }
}
