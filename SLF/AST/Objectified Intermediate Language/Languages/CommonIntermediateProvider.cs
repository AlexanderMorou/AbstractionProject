using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class CommonIntermediateProvider :
        ICommonIntermediateProvider
    {
        #region ICommonIntermediateProvider Members

        public ICommonIntermediateLanguage Language
        {
            get { return CommonIntermediateLanguage.Singleton; }
        }

        #endregion

        #region ILanguageProvider Members

        ILanguage ILanguageProvider.Language
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
