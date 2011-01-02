using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
