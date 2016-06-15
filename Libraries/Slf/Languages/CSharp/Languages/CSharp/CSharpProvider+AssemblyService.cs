using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Services;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    partial class CSharpProvider
    {
        internal class AssemblyService :
            IIntermediateAssemblyCtorLanguageService<ICSharpProvider, ICSharpLanguage, ICSharpAssembly>
        {
            internal AssemblyService(CSharpProvider provider)
            {
                this.Provider = provider;
            }

            #region IIntermediateAssemblyCtorLanguageService<ICSharpProvider,ICSharpLanguage,ICSharpCompilationUnit,ICSharpAssembly> Members

            public ICSharpAssembly New(string name)
            {
                var result = new CSharpAssembly(name, this.Provider, this.Provider.IdentityManager.RuntimeEnvironment);
                this.Provider.IdentityManager.AssemblyCreated(result);
                return result;
            }

            #endregion

            #region ILanguageService<ICSharpLanguage,ICSharpProvider> Members

            public ICSharpProvider Provider { get; private set; }

            public ICSharpLanguage Language
            {
                get { return CSharpLanguage.Singleton; }
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
