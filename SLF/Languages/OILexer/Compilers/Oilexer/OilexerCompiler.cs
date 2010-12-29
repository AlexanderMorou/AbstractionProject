using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;

namespace AllenCopeland.Abstraction.Slf.Compilers.Oilexer
{
    public class OilexerCompiler :
        IntermediateCompiler<IGDFile>
    {
        private OilexerProvider provider;

        internal OilexerCompiler(OilexerProvider provider)
            : base()
        {
            // TODO: Complete member initialization
            this.provider = provider;
        }


        public override IHighLevelLanguageProvider<IGDFile> Provider
        {
            get { return this.provider; }
        }
    }
}
