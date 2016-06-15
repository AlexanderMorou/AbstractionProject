using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Services;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    partial class MyVisualBasicProvider
    {
        private class AssemblyService :
            IIntermediateAssemblyCtorLanguageService<IMyVisualBasicProvider, IVisualBasicLanguage, IMyVisualBasicAssembly>
        {
            internal AssemblyService(MyVisualBasicProvider provider)
            {
                this.Provider = provider;
            }

            #region IIntermediateAssemblyCtorLanguageService<IMyVisualBasicProvider,IVisualBasicLanguage,IVisualBasicStart,IMyVisualBasicAssembly> Members

            public IMyVisualBasicAssembly New(string name)
            {
                var result = new MyVisualBasicAssembly(name, this.Provider, this.Provider.IdentityManager.RuntimeEnvironment);
                this.Provider.IdentityManager.AssemblyCreated(result);
                return result;
            }

            #endregion

            #region ILanguageService<IVisualBasicLanguage,IMyVisualBasicProvider> Members

            public IMyVisualBasicProvider Provider { get; private set; }

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
                get { return LanguageGuids.Services.IntermediateAssemblyCreatorService; }
            }

            #endregion

            #region IIntermediateAssemblyCtorLanguageService Members

            IIntermediateAssembly IIntermediateAssemblyCtorLanguageService.New(string name)
            {
                return this.New(name);
            }

            #endregion

            IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
            {
                get
                {
                    return this.Provider;
                }
            }


        }
    }
}
