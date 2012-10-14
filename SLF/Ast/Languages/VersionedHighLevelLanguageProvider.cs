using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public abstract class VersionedHighLevelLanguageProvider<TLanguage, TProvider, TVersion, TRootNode, TIdentityManager, TTypeIdentity, TAssemblyIdentity> :
        LanguageProvider<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity>,
        IVersionedHighLevelLanguageProvider<TVersion, TRootNode>
        where TLanguage :
            IVersionedHighLevelLanguage<TVersion, TRootNode>,
            ILanguage<TLanguage, TProvider>,
            IHighLevelLanguage<TRootNode>
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>,
            IHighLevelLanguageProvider<TRootNode>
        where TRootNode :
            IConcreteNode
        where TIdentityManager : 
            IIdentityManager<TTypeIdentity, TAssemblyIdentity>
    {

        public VersionedHighLevelLanguageProvider(TVersion version, ITypeIdentityManager identityManager)
            : base(identityManager)
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
            get { return this.Language; }
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
