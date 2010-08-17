using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base implementation of a precompiler which 
    /// processes the intermediate code base for compilation
    /// preparation.
    /// </summary>
    public class Precompiler
    {
        public Precompiler(ILanguageProvider provider)
        {

        }

        public ILanguageProvider Provider { get; private set; }

        /// <summary>
        /// Prepares an assembly for compilation into the target language
        /// provided by the <see cref="ILanguageProvider"/>.
        /// </summary>
        public void PrepareAssembly()
        {
        }
    }
}
