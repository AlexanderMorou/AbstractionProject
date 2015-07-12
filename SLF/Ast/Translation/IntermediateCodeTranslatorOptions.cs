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
    }
}
