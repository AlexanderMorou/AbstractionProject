using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Translation
{
    public class IntermediateCodeTranslatorOptions :
        IIntermediateCodeTranslatorOptions
    {
        public IntermediateCodeTranslatorOptions(IIntermediateCodeTranslatorFormatterProvider formatProvider)
        {
            this.FormatProvider = formatProvider;
            this.TranslationOrder = new HashSet<DeclarationTranslationOrder>();
            this.ElementOrderingMethod = TranslationOrderKind.Verbatim;
        }
        #region IIntermediateCodeTranslatorOptions Members

        public HashSet<DeclarationTranslationOrder> TranslationOrder { get; private set; }

        public TranslationOrderKind ElementOrderingMethod { get; set; }

        public bool AutoScope { get; set; }

        public bool AllowPartials { get; set; }

        public IIntermediateCodeTranslatorFormatterProvider FormatProvider { get; private set; }

        #endregion

        /// <summary>
        /// Returns/sets the <see cref="Boolean"/> value which denotes whether to emit the generation time at the end of the files generated.
        /// </summary>
        public bool EmitGenerationTime { get; set; }

        public bool ShortenFilenames { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="Byte"/> which determines how many spaces are used in place of each
        /// indentation used to represent control-flow.
        /// </summary>
        public byte? IndentationSpaceCount { get; set; }
    }
}
