using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;
using System.IO;
using AllenCopeland.Abstraction.Slf.Parsers;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    partial class OilexerProvider
    {
        private class AltParser :
            OILexerParser,
            ILanguageParser<IGDFile>
        {
            internal AltParser(bool parseIncludes = true, bool captureRegions = false, IList<IToken> originalFormTokens = null)
                : base(parseIncludes, captureRegions, originalFormTokens)
            {
                
            }


            #region ILanguageProcessor<IParserResults<IGDFile>,Stream,string> Members

            public Parsers.IParserResults<IGDFile> Process(Stream input, string context)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region ILanguageProcessor<IParserResults<IGDFile>,FileInfo> Members

            public Parsers.IParserResults<IGDFile> Process(FileInfo input)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }
}
