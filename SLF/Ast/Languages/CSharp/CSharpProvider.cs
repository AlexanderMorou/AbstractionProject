using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    internal partial class CSharpProvider :
        VersionedHighLevelLanguageProvider<ICSharpLanguage, ICSharpProvider, CSharpLanguageVersion, ICSharpCompilationUnit, IIntermediateCliManager, Type, Assembly>,
        ICSharpProvider
    {
        internal CSharpProvider(CSharpLanguageVersion version, IIntermediateCliManager identityManager)
            : base(version, identityManager)
        {
            this.RegisterService<IIntermediateAssemblyCtorLanguageService<ICSharpProvider, ICSharpLanguage, ICSharpCompilationUnit, ICSharpAssembly>>(LanguageGuids.Services.IntermediateAssemblyCreatorService, new AssemblyService(this));
        }

        protected override ILanguageParser<ICSharpCompilationUnit> OnGetParser()
        {
            return this.Parser;
        }

        protected override ILanguageCSTTranslator<ICSharpCompilationUnit> OnGetCSTTranslator()
        {
            return this.CSTTranslator;
        }

        protected override IIntermediateCodeTranslator OnGetTranslator()
        {
            throw new NotImplementedException();
        }

        protected override IAnonymousTypePatternAid OnGetAnonymousTypePattern()
        {
            return IntermediateGateway.CSharpPattern;
        }

        protected override ICSharpLanguage OnGetLanguage()
        {
            return CSharpLanguage.Singleton;
        }

        //#region ICSharpProvider Members

        public new ICSharpParser Parser
        {
            get { throw new NotImplementedException(); }
        }

        public new ICSharpCSTTranslator CSTTranslator
        {
            get { throw new NotImplementedException(); }
        }

        public new ICSharpAssembly CreateAssembly(string name)
        {
            return (ICSharpAssembly)base.CreateAssembly(name);
        }

        //#endregion

        //#region IHighLevelLanguageProvider<ICSharpCompilationUnit> Members


        IHighLevelLanguage<ICSharpCompilationUnit> IHighLevelLanguageProvider<ICSharpCompilationUnit>.Language
        {
            get { return this.Language; }
        }

        //#endregion

        /// <summary>
        /// Returns the <see cref="IIntermediateCliManager"/> which marshalls
        /// the identities of intermediate and non-intermediate (compiled)
        /// assemblies and types.
        /// </summary>
        public new IIntermediateCliManager IdentityManager
        {
            get { return (IIntermediateCliManager)base.IdentityManager; }
        }
    }
}
