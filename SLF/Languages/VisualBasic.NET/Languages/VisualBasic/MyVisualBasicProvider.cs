using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Compilers;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    internal partial class MyVisualBasicProvider :
        VersionedHighLevelLanguageProvider<IVisualBasicLanguage, IMyVisualBasicProvider, VisualBasicVersion, IVisualBasicStart>,
        //IVisualBasicProvider<TAssembly, TProvider>
        //VisualBasicProvider<IMyVisualBasicAssembly, IMyVisualBasicProvider>,
        IMyVisualBasicProvider
    {
        internal MyVisualBasicProvider(VisualBasicVersion version)
            : base(version)
        {
            this.RegisterService<IIntermediateAssemblyCtorLanguageService<IMyVisualBasicProvider, IVisualBasicLanguage, IVisualBasicStart, IMyVisualBasicAssembly>>(LanguageGuids.ConstructorServices.IntermediateAssemblyCreatorService, new AssemblyService(this));
            this.RegisterService<IIntermediateTypeCtorLanguageService<IIntermediateClassType>>(LanguageGuids.ConstructorServices.IntermediateClassCreatorService, new ClassService(this));
        }


        protected override ILanguageParser<IVisualBasicStart> OnGetParser()
        {
            throw new NotImplementedException();
        }

        protected override ILanguageCSTTranslator<IVisualBasicStart> OnGetCSTTranslator()
        {
            throw new NotImplementedException();
        }

        protected override IIntermediateCodeTranslator OnGetTranslator()
        {
            throw new NotImplementedException();
        }

        protected override IAnonymousTypePatternAid OnGetAnonymousTypePattern()
        {
            return IntermediateGateway.VBPattern;
        }

        protected override IVisualBasicLanguage OnGetLanguage()
        {
            return VisualBasicLanguage.Singleton;
        }

        #region IMyVisualBasicProvider Members

        /// <summary>
        /// Creates a new <see cref="IMyVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="IMyVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public new IMyVisualBasicAssembly CreateAssembly(string name)
        {
            return (IMyVisualBasicAssembly)base.CreateAssembly(name);
        }

        #endregion

    }
}
