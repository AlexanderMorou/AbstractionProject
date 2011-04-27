using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;
using AllenCopeland.Abstraction.Slf.Compilers;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class OilexerLanguage :
        IHighLevelLanguage<IGDFile>
    {
        private OilexerLanguage() {
        }
        /// <summary>
        /// The single-ton instance which which provides information about the language.
        /// </summary>
        internal static OilexerLanguage LanguageInstance = new OilexerLanguage();
        #region IHighLevelLanguage<IGDFile> Members

        public IHighLevelLanguageProvider<IGDFile> GetProvider()
        {
            return OilexerProvider.ProviderInstance;
        }

        #endregion

        #region ILanguage Members

        public string Name
        {
            get { return "OILexer Grammar Description Language"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        #endregion

        #region ILanguage Members

        public CompilerSupport CompilerSupport
        {
            get { return CompilerSupport.FullSupport ^ (CompilerSupport.DebuggerSupport | CompilerSupport.COMInterop | CompilerSupport.Unsafe | CompilerSupport.Win32Resources); }
        }

        #endregion
    }
}
