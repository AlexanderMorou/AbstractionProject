using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public abstract class VersionedHighLevelLanguageProvider<TLanguage, TProvider, TVersion, TRootNode> :
        LanguageProvider<TLanguage, TProvider>,
        IVersionedHighLevelLanguageProvider<TVersion, TRootNode>
        where TLanguage :
            ILanguage<TLanguage, TProvider>,
            IHighLevelLanguage<TRootNode>
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>,
            IHighLevelLanguageProvider<TRootNode>
        where TRootNode :
            IConcreteNode
    {

        public VersionedHighLevelLanguageProvider(TVersion version)
        {
            this.Version = version;
        }

        #region IHighLevelLanguageProvider<TRootNode> Members

        public ILanguageParser<TRootNode> Parser
        {
            get { return this.OnGetParser(); }
        }

        protected abstract ILanguageParser<TRootNode> OnGetParser();

        public ILanguageCSTTranslator<TRootNode> CSTTranslator { get { return this.OnGetCSTTranslator(); } }

        protected abstract ILanguageCSTTranslator<TRootNode> OnGetCSTTranslator();

        public IIntermediateCodeTranslator Translator
        {
            get { return this.OnGetTranslator(); }
        }

        protected abstract IIntermediateCodeTranslator OnGetTranslator();

        IHighLevelLanguage<TRootNode> IHighLevelLanguageProvider<TRootNode>.Language
        {
            get { return this.Language; }
        }

        public IAnonymousTypePatternAid AnonymousTypePattern
        {
            get { return this.OnGetAnonymousTypePattern(); }
        }

        protected abstract IAnonymousTypePatternAid OnGetAnonymousTypePattern();

        #endregion


        #region IVersionedHighLevelLanguageProvider<TVersion,TRootNode> Members

        IVersionedHighLevelLanguage<TVersion, TRootNode> IVersionedHighLevelLanguageProvider<TVersion,TRootNode>.Language
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IVersionedLanguageProvider<TVersion> Members

        /// <summary>
        /// Returns the language <typeparamref name="TVersion"/>
        /// associated to the current provider.
        /// </summary>
        public TVersion Version { get; private set; }

        #endregion
    }
}
