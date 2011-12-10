using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Ast;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    partial class CoreVisualBasicProvider
    {
        private class AssemblyService :
            IIntermediateAssemblyCtorLanguageService<ICoreVisualBasicProvider, IVisualBasicLanguage, IVisualBasicStart, ICoreVisualBasicAssembly>
        {
            internal AssemblyService(CoreVisualBasicProvider provider)
            {
                this.Provider = provider;
            }

            #region IIntermediateAssemblyCtorLanguageService<ICoreVisualBasicProvider,IVisualBasicLanguage,IVisualBasicStart,ICoreVisualBasicAssembly> Members

            public ICoreVisualBasicAssembly New(string name)
            {
                return new CoreVisualBasicAssembly(name, this.Provider);
            }

            #endregion

            #region ILanguageService<IVisualBasicLanguage,ICoreVisualBasicProvider> Members

            public ICoreVisualBasicProvider Provider { get; private set; }

            public IVisualBasicLanguage Language
            {
                get { return VisualBasicLanguage.Singleton; }
            }

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
                get { return LanguageGuids.ConstructorServices.IntermediateAssemblyCreatorService; }
            }

            #endregion

            #region IIntermediateAssemblyCtorLanguageService Members

            IIntermediateAssembly IIntermediateAssemblyCtorLanguageService.New(string name)
            {
                return this.New(name);
            }

            #endregion
        }
    }
}
