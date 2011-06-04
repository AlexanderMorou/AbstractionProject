using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

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
            get { return this.Language; }
        }

        public IIntermediateAssembly CreateAssembly(string name)
        {
            return new IntermediateAssembly(name, this);
        }

        #endregion
    }
}
