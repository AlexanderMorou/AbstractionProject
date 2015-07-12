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
        private class ClassService :
            IIntermediateTypeCtorLanguageService<IIntermediateClassType>
        {

            internal ClassService(MyVisualBasicProvider provider)
            {
                this.Provider = provider;
            }

            #region IIntermediateTypeCtorLanguageService<IIntermediateClassType> Members

            public IIntermediateClassType New(string name, IIntermediateTypeParent parent)
            {
                return new MyVisualBasicClass(name, parent);
            }

            #endregion

            #region ILanguageService Members

            public ILanguageProvider Provider { get; private set; }

            public ILanguage Language
            {
                get {
                    return VisualBasicLanguage.Singleton;
                }
            }

            public Guid ServiceGuid
            {
                get
                {
                    return LanguageGuids.Services.ClassServices.ClassCreatorService;
                }
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
