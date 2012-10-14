using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    partial class CommonIntermediateProvider
    {
        private class AssemblyService :
            IIntermediateAssemblyCtorLanguageService<ICommonIntermediateLanguage, ICommonIntermediateProvider>
        {
            internal AssemblyService(CommonIntermediateProvider provider)
            {
                this.Provider = provider;
            }

            #region ILanguageService<ICommonIntermediateLanguage,ICommonIntermediateProvider> Members

            public ICommonIntermediateProvider Provider { get; private set;}
            public ICommonIntermediateLanguage Language { get { return CommonIntermediateLanguage.Singleton; } }

            #endregion

            #region ILanguageService Members

            ILanguageProvider ILanguageService.Provider
            {
                get { return this.Provider; }
            }

            ILanguage ILanguageService.Language
            {
                get { return this.Language; }
            }

            public Guid ServiceGuid
            {
                get
                {
                    return LanguageGuids.Services.IntermediateAssemblyCreatorService;
                }
            }

            #endregion

            #region IIntermediateAssemblyCtorLanguageService Members

            public IIntermediateAssembly New(string name)
            {
                return new CommonIntermediateAssembly(name, this.Provider);
            }

            #endregion
        }
    }
}
