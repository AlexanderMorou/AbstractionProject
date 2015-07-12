using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Translation
{
    /// <summary>
    /// Defines properties and methods for working with a code
    /// translator formatter provider.
    /// </summary>
    public interface IIntermediateCodeTranslatorFormatterProvider 
    {
        /// <summary>
        /// Constructs a <see cref="IIntermediateCodeTranslatorFormatter"/> 
        /// for the <paramref name="translator"/> provided.
        /// </summary>
        /// <param name="translator">The <see cref="IIntermediateCodeTranslator"/>
        /// to obtain the <see cref="IIntermediateCodeTranslatorFormatter"/>
        /// for.</param>
        /// <returns>A new <see cref="IIntermediateCodeTranslatorFormatter"/> linked to the
        /// <paramref name="translator"/> provided.</returns>
        IIntermediateCodeTranslatorFormatter GetFormatterFor(IIntermediateCodeTranslator translator);
    }
}
