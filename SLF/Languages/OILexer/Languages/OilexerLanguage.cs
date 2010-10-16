using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class OilexerLanguage :
        IHighLevelLanguage<IGDFile>
    {
        internal static OilexerLanguage LanguageInstance = new OilexerLanguage();
        #region IHighLevelLanguage<IGDFile> Members

        public IHighLevelLanguageProvider<IGDFile> GetProvider()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ILanguage Members

        public string Name
        {
            get { return "OILexer Grammar Description Language"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        #endregion
    }
}
