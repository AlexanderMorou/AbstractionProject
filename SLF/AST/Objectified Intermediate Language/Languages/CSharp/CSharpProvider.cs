using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    internal class CSharpProvider :
        ICSharpProvider
    {
        private CSharpLanguageVersion version;
        internal CSharpProvider(CSharpLanguageVersion version)
        {
            this.version = version;
        }

        #region ICSharpProvider Members

        public ICSharpParser Parser
        {
            get { throw new NotImplementedException(); }
        }

        public ICSharpCSTTranslator CSTTranslator
        {
            get { throw new NotImplementedException(); }
        }

        public ICSharpLanguage Language
        {
            get {
                return CSharpLanguage.Singleton;
            }
        }

        #endregion

        #region IHighLevelLanguageProvider<ICSharpCompilationUnit> Members

        ILanguageParser<ICSharpCompilationUnit> IHighLevelLanguageProvider<ICSharpCompilationUnit>.Parser
        {
            get { return this.Parser; }
        }

        ILanguageCSTTranslator<ICSharpCompilationUnit> IHighLevelLanguageProvider<ICSharpCompilationUnit>.CSTTranslator
        {
            get { return this.CSTTranslator; }
        }

        public IIntermediateCodeTranslator Translator
        {
            get { return new Translation.CSharpCodeTranslator(); }
        }

        IHighLevelLanguage<ICSharpCompilationUnit> IHighLevelLanguageProvider<ICSharpCompilationUnit>.Language
        {
            get { return this.Language; }
        }

        #endregion

        #region IVersionedLanguageProvider<CSharpLanguageVersion> Members

        public CSharpLanguageVersion Version
        {
            get { return this.version; }
        }

        #endregion

        #region IVersionedHighLevelLanguageProvider<CSharpLanguageVersion,ICSharpCompilationUnit> Members

        IVersionedHighLevelLanguage<CSharpLanguageVersion, ICSharpCompilationUnit> IVersionedHighLevelLanguageProvider<CSharpLanguageVersion, ICSharpCompilationUnit>.Language
        {
            get { return this.Language; }
        }

        #endregion

        #region IHighLevelLanguageProvider<ICSharpCompilationUnit> Members

        public IAnonymousTypePatternAid AnonymousTypePattern
        {
            get { return IntermediateGateway.CSharpPattern; }
        }

        #endregion


        #region ICSharpProvider Members

        /// <summary>
        /// Creates a new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public ICSharpAssembly CreateAssembly(string name)
        {
            return new CSharpAssembly(name);
        }

        #endregion

        #region IHighLevelLanguageProvider<ICSharpCompilationUnit> Members


        IIntermediateAssembly ILanguageProvider.CreateAssembly(string name)
        {
            return this.CreateAssembly(name);
        }

        #endregion

        #region ILanguageProvider Members

        ILanguage ILanguageProvider.Language
        {
            get { return this.Language; }
        }

        #endregion
    }
}
