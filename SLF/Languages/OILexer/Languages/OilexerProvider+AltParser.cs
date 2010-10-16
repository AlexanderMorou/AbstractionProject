using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;
using System.IO;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    partial class OilexerProvider
    {
        private class AltParser :
            GDParser,
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
