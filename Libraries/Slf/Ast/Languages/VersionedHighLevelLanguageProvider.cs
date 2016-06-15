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
    public abstract class VersionedLanguageProvider<TLanguage, TProvider, TVersion, TIdentityManager, TTypeIdentity, TAssemblyIdentity> :
        LanguageProvider<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity>,
        IVersionedLanguageProvider<TVersion>
        where TLanguage :
            IVersionedLanguage<TVersion>,
            ILanguage<TLanguage, TProvider>
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>
        where TIdentityManager : 
            IIdentityManager<TTypeIdentity, TAssemblyIdentity>,
            IIntermediateIdentityManager
    {

        public VersionedLanguageProvider(TVersion version, TIdentityManager identityManager)
            : base(identityManager)
        {
            this.Version = version;
        }

        #region IVersionedLanguageProvider<TVersion> Members

        /// <summary>
        /// Returns the language <typeparamref name="TVersion"/>
        /// associated to the current provider.
        /// </summary>
        public TVersion Version { get; private set; }

        #endregion
    }
}
